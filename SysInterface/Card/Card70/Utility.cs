using System;
using System.Globalization;
using System.Text;
using System.Web;
using viviapi.BLL.Channel;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviLib.Security;

namespace viviapi.SysInterface.Card.Card70
{
    /// <summary>
    /// 
    /// </summary>
    public class Utility
    {
        #region 70卡类
        public static string EnName = "vc70.21";
        public static string ChineseName = "70Card-版本号2.1";

        public static string Vc7021CardReceiveVerifyStr = "userid={0}&orderid={1}&typeid={2}&productid={3}&cardno={4}&cardpwd={5}&money={6}&url={7}";
        public static string Vc7021CardSynchronousNotifyVerifyStr = "returncode={0}&returnorderid={1}&keyvalue={2}";
        public static string Vc7021CardNotifyVerifyStr = "returncode={0}&userid={1}&orderid={2}&typeid={3}&productid={4}&cardno={5}&cardpwd={6}&money={7}&realmoney={8}&cardstatus={9}&keyvalue={10}";
        public static string NotifySuccessflag = "ok";
        #endregion

        #region CheckParameter
        /// <summary>
        /// 5：ip验证错误
        /// 6：数字签名错误
        /// 7：无效的商户号
        /// 8：无效单据号
        /// 9：无效的产品类型ID
        /// 10：无效的产品ID
        /// 11：无效的卡
        /// 12：系统错误
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <returns></returns>
        public static string CheckParameter(CardInfo cardInfo)
        {
            string errCode = "";

            if (cardInfo == null)
            {
                cardInfo = new CardInfo();
                cardInfo.Msg = "参数有误";
                return "12";
            }

            errCode = CheckParamsEmpty(cardInfo);
            if (!string.IsNullOrEmpty(errCode))
                return "12";

            errCode = CheckParamsLength(cardInfo);
            if (!string.IsNullOrEmpty(errCode))
                return "12";

            if (!Card.Utility.VerifyCardNoFormat(cardInfo.cardno))
            {
                cardInfo.Msg = "参数 [cardno] 格式不正确";
                return "11";
            }
            cardInfo.CardNo = cardInfo.cardno.Trim();
            if (!Card.Utility.VerifyCardPwdFormat(cardInfo.cardpwd))
            {
                cardInfo.Msg = "参数 [cardpwd] 格式不正确";
                return "11";
            }
            cardInfo.CardPwd = cardInfo.cardpwd.Trim();

            if (string.IsNullOrEmpty(errCode))
            {
                cardInfo.CardNo = cardInfo.cardno.Trim();
                cardInfo.CardPwd = cardInfo.CardPwd.Trim();
            }

            #region typeId
            int typeId = GetChannelTypeId(cardInfo.typeid, cardInfo.cardno);
            if (typeId == 0)
            {
                cardInfo.Msg = "[typeid]: 无效的产品类型ID";
                return "9";
            }
            cardInfo.TypeId = typeId;
            int cardType = Card.Utility.CodeMapping(typeId);
            cardInfo.CardType = cardType;
            #endregion

            #region userId
            int userId = 0;
            if (!int.TryParse(cardInfo.userid, out userId))
            {
                cardInfo.Msg = "商户ID[userid] 格式不正确";
                return "7";
            }
            cardInfo.UserId = userId;
            #endregion

            #region cardValue
            decimal cardValue = 0M;
            if (!decimal.TryParse(cardInfo.money, out cardValue))
            {
                cardInfo.Msg = "金额[value] 格式不正确";
                return "12";
            }
            cardInfo.OrderAmt = decimal.ToInt32(cardValue);
            #endregion

            #region userInfo
            var userInfo = BLL.User.Factory.GetCacheUserBaseInfo(userId);
            if (userInfo == null)
            {
                cardInfo.Msg = "商户不存在";
                return "7";//
            }
            if (userInfo.Status != 2)
            {
                cardInfo.Msg = "商户状态不正常";
                return "7";//
            }
            cardInfo.APIkey = userInfo.APIKey;
            cardInfo.ManageId = userInfo.manageId;
            #endregion

            if (!CheckSign(cardInfo, userInfo.APIKey))
            {
                cardInfo.Msg = "签名失败";
                return "6";//
            }

            #region chanelInfo
            string chanelNo = cardType.ToString("0000") +
               cardInfo.OrderAmt.ToString(CultureInfo.InvariantCulture);

            var chanelInfo = Channel.GetModel(chanelNo, userId, true);
            if (chanelInfo == null)
            {
                cardInfo.Msg = chanelNo + "通道不存在";
                return "10";//不支持该类卡或者该面值的卡
            }
            else if (chanelInfo.isOpen != 1)
            {
                cardInfo.Msg = chanelNo + "暂时停止该类卡或者该面值的卡交易";
                return "10";//业务状态不可用，未开通此类卡业务
            }
            else if (!chanelInfo.supplier.HasValue)
            {
                cardInfo.Msg = "未设置销卡接口";
                return "12";//
            }
            cardInfo.ChanelNo = chanelNo;
            cardInfo.SupplierId = chanelInfo.supplier.Value;
            #endregion

            #region 数据库 检查
            var chkresult = BLL.Order.Card.Factory.Instance.CheckCardInfo(userId
        , cardInfo.orderid
        , typeId
        , cardInfo.cardno
        , cardInfo.cardpwd
        , cardInfo.OrderAmt);

            if (chkresult == null)
            {
                cardInfo.Msg = "系统故障，服务器忙";
                return "12";
            }
            else
            {
                cardInfo.ProcessMode = 1;

                switch (chkresult.IsRepeat)
                {
                    case 1:
                        if (chkresult.Makeup == 1)
                        {
                            cardInfo.SupplierId = chkresult.Supplierid;
                            cardInfo.ProcessMode = 2;//自身处理

                            #region 补单
                            if (String.Equals(chkresult.Cardpwd, cardInfo.CardPwd, StringComparison.CurrentCultureIgnoreCase))
                            {
                                if (chkresult.Isclose == 0)
                                {
                                    #region 继续补充
                                    int balance = decimal.ToInt32(chkresult.CardBalance);
                                    if (balance > 0)
                                    {
                                        if (cardInfo.OrderAmt <= balance)
                                        {
                                            cardInfo.ProcessMode = 2;//自身处理
                                        }
                                        else if (cardInfo.OrderAmt > balance)
                                        {
                                            cardInfo.Msg = "卡内余额不足";
                                            return "11"; //卡余额不足
                                        }
                                    }
                                    else
                                    {
                                        cardInfo.Msg = "充值卡无效";
                                        return "11";//卡余额不足
                                    }
                                    #endregion
                                }
                                else
                                {
                                    cardInfo.Msg = "充值卡无效";
                                    return "11";//不可以继续 补充了
                                }
                            }
                            else
                            {
                                cardInfo.Msg = "卡密码不正确";
                                return "11";//卡密不对
                            }
                            #endregion
                        }
                        else
                        {
                            cardInfo.ProcessMode = 1;//通过接口处理
                        }
                        break;
                    case 4:
                    case 5:
                        if (String.Equals(chkresult.Cardpwd, cardInfo.CardPwd, StringComparison.CurrentCultureIgnoreCase))
                        {
                            cardInfo.Msg = "订单内容重复";
                            return "8";
                        }
                        break;
                    case 6:
                        cardInfo.Msg = "订单号已经存在";
                        return "12";
                    case 7:
                        cardInfo.Msg = "提交次数过多";
                        return "12";
                    case 8:
                        cardInfo.Msg = "充值卡无效";
                        return "11";
                }
            }
            #endregion

            return "1";
        }
        #endregion

        #region CheckParamsEmpty
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <returns></returns>
        public static string CheckParamsEmpty(CardInfo cardInfo)
        {
            string errCode = "";

            if (string.IsNullOrEmpty(cardInfo.userid))
            {
                errCode = "1001";
                cardInfo.Msg = "参数 [userid] 商户ID为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.orderid))
            {
                errCode = "1002";
                cardInfo.Msg = "参数 [orderid] 商户订单号为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.typeid))
            {
                errCode = "1003";
                cardInfo.Msg = "参数 [typeid] 产品类型ID为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.productid))
            {
                errCode = "1004";
                cardInfo.Msg = "参数 [productid] 产品ID为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.cardno))
            {
                errCode = "1005";
                cardInfo.Msg = "参数 [cardno] 卡号为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.cardpwd))
            {
                errCode = "1006";
                cardInfo.Msg = "参数 [cardpwd] 限制为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.orderid))
            {
                errCode = "1007";
                cardInfo.Msg = "参数 [orderid]卡号为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.money))
            {
                errCode = "1008";
                cardInfo.Msg = "参数 [money] 订单金额为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.url))
            {
                errCode = "1009";
                cardInfo.Msg = "参数 [url] 商户接收售卡结果数据的地址为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.sign))
            {
                errCode = "1010";
                cardInfo.Msg = "参数 [sign] MD5 签名为空";
            }

            return errCode;
        }
        #endregion

        #region CheckParamsLength
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <returns></returns>
        public static string CheckParamsLength(CardInfo cardInfo)
        {
            string errCode = "";

            if (cardInfo.userid.Length > 10)
            {
                errCode = "2000";
                cardInfo.Msg = "参数 [userid] 长度超过最长限制2";
            }
            else if (cardInfo.orderid.Length > 30)
            {
                errCode = "2001";
                cardInfo.Msg = "参数 [orderid] 长度超过最长限制30";
            }
            else if (cardInfo.typeid.Length > 10)
            {
                errCode = "2002";
                cardInfo.Msg = "参数 [typeid] 长度超过最长限制10";
            }
            else if (cardInfo.productid.Length > 10)
            {
                errCode = "2003";
                cardInfo.Msg = "参数 [productid] 长度超过最长限制10";
            }
            else if (cardInfo.cardno.Length > 30)
            {
                errCode = "2003";
                cardInfo.Msg = "参数 [cardno] 长度超过最长限制30";
            }
            else if (cardInfo.cardpwd.Length > 30)
            {
                errCode = "2004";
                cardInfo.Msg = "参数 [cardpwd] 长度超过最长限制30";
            }
            else if (cardInfo.money.Length > 10)
            {
                errCode = "2005";
                cardInfo.Msg = "参数 [money] 长度超过最长限制10";
            }
            else if (cardInfo.url.Length > 255)
            {
                errCode = "2006";
                cardInfo.Msg = "参数 [url] 长度超过最长限制255";
            }
            else if (cardInfo.ext.Length > 255)
            {
                errCode = "2007";
                cardInfo.Msg = "参数 [attach] 长度超过最长限制255";
            }
            else if (cardInfo.sign.Length != 32)
            {
                errCode = "2008";
                cardInfo.Msg = "参数 [sign] 长度不对";
            }
            return errCode;
        }
        #endregion

        #region GetChannelTypeId
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardtype"></param>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public static int GetChannelTypeId(string cardtype, string cardno)
        {
            int sysChannelTypeId = 0;

            try
            {
                cardtype = ConverCardCode(cardtype);
                if (!string.IsNullOrEmpty(cardtype))
                {
                    sysChannelTypeId = Convert.ToInt32(cardtype);
                }
                if (sysChannelTypeId == 104)
                {
                    if (Card.Utility.IsShengFuTong(cardno))
                    {
                        sysChannelTypeId = 210;
                    }
                }
            }
            catch
            {

            }
            return sysChannelTypeId;
        }

        /// <summary>
        /// 通道大类型
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static string ConverCardCode(string typeid)
        {
            string code = string.Empty;

            switch (typeid)
            {
                case "cm":
                    code = "103"; //神州行充值卡
                    break;
                case "sd":
                    code = "104"; //盛大一卡通
                    break;
                case "zt":
                    code = "105"; //征途支付卡
                    break;
                case "jw":
                    code = "106"; //骏网一卡通
                    break;
                case "qq":
                    code = "107"; //腾讯Q币卡
                    break;
                case "cc":
                    code = "108"; //联通充值卡
                    break;
                case "jy":
                    code = "109"; //久游一卡通
                    break;
                case "wy":
                    code = "110"; //网易一卡通
                    break;
                case "wm":
                    code = "111"; //完美一卡通
                    break;
                case "sh":
                    code = "112"; //搜狐一卡通
                    break;
                case "dx":
                    code = "113"; //电信充值卡
                    break;
                case "gy":
                    code = "115"; //光宇一卡通
                    break;
                case "zy":
                    code = "117"; //纵游一卡通
                    break;
                case "tx":
                    code = "118"; //天下一卡通
                    break;
                case "th":
                    code = "119"; //天宏一卡通
                    break;
            }
            return code;

        }
        #endregion

        #region CheckSign
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <param name="apikey"></param>
        /// <returns></returns>
        public static bool CheckSign(CardInfo cardInfo, string apikey)
        {
            try
            {
                bool result = false;

                string plain = string.Format("userid={0}&orderid={1}&typeid={2}&productid={3}&cardno={4}&cardpwd={5}&money={6}&url={7}"
                    , cardInfo.userid
                    , cardInfo.orderid
                    , cardInfo.typeid
                    , cardInfo.productid
                    , cardInfo.cardno
                    , cardInfo.cardpwd
                    , cardInfo.money
                    , cardInfo.url).ToLower()
                    + string.Format("&keyvalue={0}", apikey);



                string localsign = Cryptography.MD5(plain, "UTF-8");



                if (localsign == cardInfo.sign)
                {
                    result = true;
                }
                else
                {
                    //参数不最小化 也行
                    plain = string.Format("userid={0}&orderid={1}&typeid={2}&productid={3}&cardno={4}&cardpwd={5}&money={6}&url={7}"
                    , cardInfo.userid
                    , cardInfo.orderid
                    , cardInfo.typeid
                    , cardInfo.productid
                    , cardInfo.cardno
                    , cardInfo.cardpwd
                    , cardInfo.money
                    , cardInfo.url)
                    + string.Format("&keyvalue={0}", apikey);

                    localsign = Cryptography.MD5(plain, "UTF-8").ToLower();

                    if (localsign == cardInfo.sign)
                    {
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        #endregion

        #region GetResponseText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static string GetResponseText(SyncResult info, string apiKey)
        {
            string plain = string.Format("returncode={0}&returnorderid={1}&keyvalue={2}"
                , info.returncode
                , info.returnorderid
                , apiKey);

            string localSign = Cryptography.MD5(plain).ToLower();

            return string.Format("returncode={0}&returnorderid={1}&sign={2}", info.returncode, info.returnorderid, localSign);
        }
        #endregion

        #region GetQueryString
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="strParaName"></param>
        /// <returns></returns>
        public static string GetQueryString(HttpContext context, string strParaName)
        {
            if (context == null)
                return "";

            string value = context.Request[strParaName];

            if (String.IsNullOrEmpty(value))
                return "";


            return HttpUtility.UrlDecode(value, Encoding.GetEncoding("GB2312"));
        }
        #endregion

        #region CreateNotifyUrl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderinfo"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static string CreateNotifyUrl(OrderCardInfo orderinfo, string apiKey)
        {
            if (orderinfo == null)
                return string.Empty;

            string notifyUrl = orderinfo.notifyurl;

            if (string.IsNullOrEmpty(notifyUrl))
                return notifyUrl;

            string returncode;

            decimal facevalue = 0M;
            string cardstatus = "0";
            if (orderinfo.status == 2)
            {
                cardstatus = "1";
                returncode = "1";

                if (orderinfo.realvalue.HasValue)
                    facevalue = decimal.Round(orderinfo.realvalue.Value, 0);
            }
            else
            {
                returncode = "11";
            }

            if (orderinfo.method == 2)
            {
                returncode = Get70CardReturnCode(orderinfo.errtype);
                if (returncode == "1")
                {
                    cardstatus = "1";
                }
            }

            string refervalue = decimal.Round(orderinfo.refervalue, 0).ToString(CultureInfo.InvariantCulture);

            string typeid = orderinfo.cus_field1;
            string productid = orderinfo.cus_field2;

            string plain = string.Format("returncode={0}&userid={1}&orderid={2}&typeid={3}&productid={4}&cardno={5}&cardpwd={6}&money={7}&realmoney={8}&cardstatus={9}&keyvalue={10}"
                           , returncode
                           , orderinfo.userid
                           , orderinfo.userorder
                           , typeid
                           , productid
                           , orderinfo.cardNo
                           , orderinfo.cardPwd
                           , refervalue
                           , facevalue
                           , cardstatus
                           , apiKey);
            string sign = Cryptography.MD5(plain).ToLower();

            var parms = new StringBuilder();

            parms.AppendFormat("returncode={0}", UrlEncode(returncode));
            parms.AppendFormat("&userid={0}", UrlEncode(orderinfo.userid.ToString(CultureInfo.InvariantCulture)));
            parms.AppendFormat("&orderid={0}", UrlEncode(orderinfo.userorder));
            parms.AppendFormat("&typeid={0}", UrlEncode(typeid));
            parms.AppendFormat("&productid={0}", UrlEncode(productid));
            parms.AppendFormat("&cardno={0}", UrlEncode(orderinfo.cardNo));
            parms.AppendFormat("&cardpwd={0}", UrlEncode(orderinfo.cardPwd));
            parms.AppendFormat("&money={0}", UrlEncode(refervalue));
            parms.AppendFormat("&realmoney={0}", UrlEncode(facevalue.ToString(CultureInfo.InvariantCulture)));
            parms.AppendFormat("&cardstatus={0}", UrlEncode(cardstatus.ToString(CultureInfo.InvariantCulture)));
            parms.AppendFormat("&sign={0}", UrlEncode(sign));
            parms.AppendFormat("&ext={0}", UrlEncode(orderinfo.attach));
            if (returncode == "0")
            {
                parms.AppendFormat("&errtype={0}", string.Empty);
            }
            else
            {
                parms.AppendFormat("&errtype={0}", Get70Errtype(orderinfo.method,orderinfo.supplierId, orderinfo.errtype));
            }

            if (notifyUrl.IndexOf("?", System.StringComparison.Ordinal) > 0)
            {
                notifyUrl = notifyUrl + "&" + parms.ToString();
            }
            else
            {
                notifyUrl = notifyUrl + "?" + parms.ToString();
            }
            return notifyUrl;
        }
        #endregion

        

        public static string UrlEncode(string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
                return string.Empty;
            return System.Web.HttpUtility.UrlEncode(paramValue, System.Text.Encoding.GetEncoding("GB2312"));
        }

        public static string Get70CardReturnCode(string errtype)
        {
            if (errtype == "0")
            {
                return "1";
            }
            return "11";
        }



        #region Get70Errtype
        /// <summary>
        /// 
        /// </summary>
        /// <param name="suppid"></param>
        /// <param name="errtype"></param>
        /// <returns></returns>
        public static string Get70Errtype(int method, int suppid, string errtype)
        {
            if (method == 2)
            {
                return "1003";//卡余额不足
            }

            string result = "1001";

            if (suppid == 60866 || suppid == 70)
            {
                result = errtype;
            }
            else if (suppid == 80)
            {
                #region 欧飞
                /*
                 2000	支付成功
2001	数据接收成功
2002	不支持该类卡或者
该面值的卡
2003	签名验证失败
2004	订单内容重复
2005	该卡密已经有被使
用的记录
2006	订单号已经存在
2007	数据非法
2008	非法用户
2009	暂时停止该类卡或
者该面值的卡交易
2010	充值卡无效
2011	支付成功,实际面值
{0}元
2012	处理失败卡密未使
用
2013	系统繁忙
2014	不存在该笔订单
2015	未知请求
2016	密码错误
2017	匹配订单失败
2018	余额不足
2019	运营商维护
2020	提交次数过多
2999	其他错误
                 */
                switch (errtype)
                {
                    case "7"://卡号卡密或卡面额不符合规则

                        result = "1001";//卡号或密码错误
                        break;

                    case "1008"://余额卡过期（有效期1个月）
                        result = "1002";//卡号过期                      
                        break;

                    case "2018"://余额不足
                        result = "1003";//卡余额不足   
                        break;

                    case "2005"://
                        result = "1005";//卡已使用过
                        break;

                    case "2009"://不支持该类卡或者该面值的卡                    
                        result = "1007";//不支持的卡类型或金额
                        break;

                    case "2013"://该卡已被锁定                    
                        result = "1006";//卡号被冻结
                        break;

                    case "2019"://运营商维护
                        result = "1008";//不支持的卡类型或金额
                        break;

                    case "10000"://
                        result = "1009";//其他游戏专用卡
                        break;
                }
                #endregion
            }
            else if (suppid == 102)
            {
                #region 易宝
                switch (errtype)
                {
                    case "7"://卡号卡密或卡面额不符合规则
                        result = "1001";//卡号或密码错误
                        break;

                    case "1008"://余额卡过期（有效期1个月）
                        result = "1002";//卡号过期                      
                        break;

                    case "1007"://卡内余额不足
                        result = "1003";//卡余额不足   
                        break;

                    case "1002"://本张卡密您提交过于频繁，请您稍后再试
                    case "1010"://此卡正在处理中                    
                    case "2005"://此卡已使用
                    case "2006"://此卡已使用
                        result = "1005";//卡已使用过
                        break;

                    case "2007"://该卡为假卡                    
                        result = "1007";//卡未激活
                        break;

                    case "2013"://该卡已被锁定                    
                        result = "1006";//卡号被冻结
                        break;

                    case "1006"://充值卡无效
                    case "1003"://不支持的卡类型（比如电信地方卡）
                    case "2008"://该卡种正在维护
                    case "2009"://浙江省移动维护
                    case "2010"://江苏省移动维护
                    case "2011"://福建省移动维护
                    case "2012"://辽宁省移动维护
                    case "2014"://系统繁忙，请稍后再试
                        result = "1008";//不支持的卡类型或金额
                        break;

                    case "10000"://
                        result = "1009";//其他游戏专用卡
                        break;
                }
                #endregion
            }
            else if (suppid == 851)
            {
                #region HuiSu
                //
                switch (errtype)
                {
                    case "201"://卡不符合规则
                        result = "1008";//
                        break;
                    case "202"://不支持此卡种
                        result = "1008";//
                        break;
                    case "203"://暂停此卡种
                        result = "1008";//
                        break;
                    case "204"://不支持此面值
                        result = "1008";//
                        break;
                    case "205"://暂停此面值
                        result = "1008";//卡余额不足
                        break;

                    //case "206"://运营商维护
                    //    result = "19";//
                    //    break;
                    //case "207"://用户订单号重复
                    //    result = "6";//
                    //    break;
                    //case "208"://卡有处理记录
                    //    result = "5";//
                    //    break;
                    //case "209"://卡频繁提交
                    //    result = "20";//
                    //    break;
                }

                #endregion
            }
            return result;
        }
        #endregion

        #region 同步提交转化代码
        private const string DefaultSysCode = "12";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supp"></param>
        /// <param name="errcode"></param>
        /// <returns></returns>
        public static string ConvertSynchronousErrorCode(SupplierCode supp, string errcode)
        {
            string syscode = DefaultSysCode;

            if (supp == SupplierCode.HuiSu) //汇速通
            {
                syscode = ConvetSyncCodeForHuiSu(errcode);
            }
            else if (supp == SupplierCode.Card51) //51
            {
                syscode = ConvetSyncCodeForOf51Esales(errcode);
            }
            else if (supp == SupplierCode.OfCard) //欧飞
            {
                syscode = ConvetSyncCodeForOfCard(errcode);
            }
            else if (supp == SupplierCode.YeePay) //易宝
            {
                syscode = DefaultSysCode;
            }
            return syscode;
        }

        #region ConvetSyncCodeForOf51Esales

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errcode"></param>
        /// <returns></returns>
        public static string ConvetSyncCodeForOf51Esales(string errcode)
        {
            string syscode = DefaultSysCode;

            switch (errcode)
            {
                case "2008"://此卡系统已受理，正在处理中，请勿重复提交
                    syscode = "12"; //
                    break;
                case "1007"://卡片或面值类型不正确
                case "1009"://卡号和密码不符合规范
                case "2000"://卡片信息不完整
                case "3000"://系统不能受理的卡
                case "3001"://系统检测到您的访问IP非法！
                case "3002"://系统检测不到您的访问IP！
                case "3003"://神州行充值卡密码不正确！
                    syscode = "11"; //充值卡无效
                    break;
                case "1010":
                    syscode = "12"; //系统繁忙稍后重试
                    break;
                case "1006"://系统关闭了该类卡支付
                case "2007"://系统正在维护中
                    syscode = "12"; //运营商维护
                    break;
                case "2001": //请正确请填写卡号密码
                case "2004": //您提交的卡号或密码有误
                    syscode = "11"; //密码错误
                    break;
                case "2005"://您的卡号密码已使用
                case "2009"://您的卡号密码重复使用
                    syscode = "12"; //该卡密已经有被使用的记录
                    break;
                case "2010"://系统检测到此卡重复提交失败三次，请一小时后重试
                case "2011":
                    syscode = "12"; //提交次数过多
                    break;

                case "1000"://参数不完整
                case "1001"://商户没通过审核
                case "1002"://订单号重复
                case "1003"://MD5签名不正确
                case "1004"://商户支付帐户没有通过审核
                case "1005"://上级商户支付帐户没有通过审核
                case "1008"://商户没有开通该支付接口
                case "2002"://不支持的卡片类型编码
                case "2003"://卡片类型和面值类型不符
                case "2006"://其他
                case "2012"://你的参数提交非法，请勿越权使用！
                    syscode = "12";
                    break;
            }

            return syscode;
        }

        #endregion

        #region ConvetSyncCodeForOfCard

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errcode"></param>
        /// <returns></returns>
        public static string ConvetSyncCodeForOfCard(string errcode)
        {
            string syscode = DefaultSysCode;

            #region 欧飞

            switch (errcode)
            {
                case "2001":
                    syscode = "12";
                    break;
                case "2002":
                    syscode = "12"; //不支持该类卡或者该面值的卡
                    break;
                case "2005":
                    syscode = "12"; //
                    break;
                case "2009":
                    syscode = "12"; //运营商维护
                    break;
                case "2010":
                    syscode = "11"; //充值卡无效
                    break;
                case "2012":
                    syscode = "12"; //处理失败卡密未使用
                    break;
                case "2016":
                    syscode = "11"; //密码错误
                    break;
            }

            #endregion

            return syscode;
        }

        #endregion

        #region ConvetSyncCodeForHuiSu
        /// <summary>
        /// 汇速通
        /// </summary>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public static string ConvetSyncCodeForHuiSu(string errCode)
        {
            string sysCode = DefaultSysCode;
            string billStatus = "";

            string[] arr = errCode.Split('|');

            string retCode = arr[0];
            if (arr.Length > 1)
                billStatus = arr[1];

            #region 汇速通
            switch (retCode)
            {
                case "0"://
                    if (billStatus == "-1")
                    {
                        sysCode = "11";//充值卡无效
                    }
                    break;
                case "-1"://创单失败（具体可参考返回的ret_msg）
                    sysCode = "12";
                    break;
                case "1":
                    sysCode = "12"; //传入参数有误（具体可参考返回的ret_msg）
                    break;
                case "2":
                    sysCode = "12"; //代理商ID错误 或 未开通该服务
                    break;
                case "3":
                    sysCode = "12"; //IP验证错误
                    break;
                case "4":
                    sysCode = "12"; //签名验证错误
                    break;
                case "5":
                    sysCode = "12"; //重复的订单号
                    break;
                case "8":
                    sysCode = "12"; //单据不存在
                    break;
                case "22":
                    sysCode = "12"; //卡号卡密格式加密错误
                    break;
                case "98"://接口维中
                    sysCode = "12";
                    break;
                //case "99":
                //    sysCode = "-1"; //系统错误,未知（需要查询后在处理单据状态）
                //    break;
            }

            #endregion

            return sysCode;
        }

        #endregion
        #endregion

        #region GetMessageByCode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public static string GetMessageByCode(string errCode)
        {
            string message = errCode;

            switch (errCode)
            {
                case "1":
                    message = "提交成功";
                    break;
                case "5":
                    message = "ip验证错误";
                    break;
                case "6":
                    message = "数字签名错误";
                    break;
                case "7":
                    message = "无效的商户号";
                    break;
                case "8":
                    message = "无效单据号";
                    break;
                case "9":
                    message = "无效的产品类型ID";
                    break;
                case "10":
                    message = "无效的产品ID";
                    break;
                case "11":
                    message = "无效的卡";
                    break;
                case "12":
                    message = "系统错误";
                    break;
            }

            return message;
        }
        #endregion

        #region CardReceiveVerify
        /// <summary>
        /// 接收时 验证商户卡类
        /// </summary>
        /// <param name="version"></param>
        /// <param name="key"></param>
        /// <param name="sign"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool CardReceiveVerify(string version, string sign, params object[] arg)
        {
            bool result = false;

            string plain = string.Format(Vc7021CardReceiveVerifyStr, arg).ToLower() + string.Format("&keyvalue={0}", arg[8]);

            viviLib.Logging.LogHelper.Write(plain);

            string localsign = Cryptography.MD5(plain, "UTF-8");

            viviLib.Logging.LogHelper.Write(localsign);

            if (localsign == sign)
            {
                result = true;
            }
            else
            {
                //参数不最小化 也行
                plain = string.Format(Vc7021CardReceiveVerifyStr, arg) + string.Format("&keyvalue={0}", arg[8]);
                localsign = Cryptography.MD5(plain).ToLower();
                if (localsign == sign)
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion

        #region CreateSynchronousNotifySign
        /// <summary>
        /// 同步时产生签名
        /// </summary>
        /// <param name="version"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static String CreateSynchronousNotifySign(string version, params object[] arg)
        {
            string plain = string.Format(Vc7021CardSynchronousNotifyVerifyStr, arg);
            string localsign = Cryptography.MD5(plain).ToLower();
            return localsign;
        }
        #endregion
    }
}
