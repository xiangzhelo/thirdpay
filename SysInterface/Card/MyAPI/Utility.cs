using System;
using System.Globalization;
using System.Text;
using System.Web;
using viviapi.BLL.Channel;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviLib.Security;

namespace viviapi.SysInterface.Card.MyAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Utility
    {
        public static string EnName = "vc1.0";
        public static string ChineseName
        {
            get
            {
                if (BLL.WebInfoFactory.CurrentWebInfo != null)
                {
                    return BLL.WebInfoFactory.CurrentWebInfo.apicardname + "[" +
                           BLL.WebInfoFactory.CurrentWebInfo.apicardversion + "]";
                }
                return string.Empty;
            }
        }

        public static string ReceiveSuccessflag = "opstate=0";
        public static string NotifySuccessflag = "opstate=0";

        #region CheckParameter
        /// <summary>
        /// 1 提交成功
        /// 7 数据非法
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <returns></returns>
        public static string CheckParameter(CardInfo cardInfo, HttpContext context)
        {
            string errCode = "";


            if (cardInfo == null)
            {
                return "7";
            }

            errCode = CheckParamsEmpty(cardInfo);
            if (!string.IsNullOrEmpty(errCode))
                return "7";


            errCode = CheckParamsLength(cardInfo);
            if (!string.IsNullOrEmpty(errCode))
                return "7";
            //if (cardInfo.cardno.IndexOf(';') > 0)
            //{
            //    string[] cardnoArray = cardInfo.cardno.Split(';');
            //    foreach (string cardno in cardnoArray)
            //    {
            //        if (!Card.Utility.VerifyCardNoFormat(cardno))
            //        {
            //            cardInfo.Msg = "参数 [cardno] 格式不正确";
            //            return "7";
            //        }
            //    }
            //}
            // else 
            if (!Card.Utility.VerifyCardNoFormat(cardInfo.cardno))
            {
                cardInfo.Msg = "参数 [cardno] 格式不正确";
                return "7";
            }
            cardInfo.CardNo = cardInfo.cardno;


            //if (cardInfo.cardpwd.IndexOf(';') > 0)
            //{
            //    string[] cardnoArray = cardInfo.cardpwd.Split(';');
            //    foreach (string cardno in cardnoArray)
            //    {
            //        if (!Card.Utility.VerifyCardNoFormat(cardno))
            //        {
            //            cardInfo.Msg = "参数 [cardno] 格式不正确";
            //            return "7";
            //        }
            //    }
            //}
            //else 
            if (!Card.Utility.VerifyCardPwdFormat(cardInfo.cardpwd))
            {
                cardInfo.Msg = "参数 [cardpwd] 格式不正确";
                return "7";
            }
            cardInfo.CardPwd = cardInfo.cardpwd;

            #region cardType
            int cardType = 0;
            if (!int.TryParse(cardInfo.type, out cardType))
            {
                cardInfo.Msg = "类型[type] 格式不正确";
                return "7";
            }
            cardInfo.CardType = cardType;
            #endregion

            #region typeId
            int typeId = GetChannelTypeId(cardInfo.CardType, cardInfo.cardno);
            if (typeId == 0)
            {
                cardInfo.Msg = "支付通道不存在";
                return "2";//不支持该类卡或者该面值的卡
            }
            cardInfo.TypeId = typeId;
            #endregion

            #region userId
            int userId = 0;
            if (!int.TryParse(cardInfo.parter, out userId))
            {
                cardInfo.Msg = "商户ID[parter] 格式不正确";
                return "7";
            }
            cardInfo.UserId = userId;
            #endregion

            #region cardValue
            decimal cardValue = 0M;
            if (!decimal.TryParse(cardInfo.value, out cardValue))
            {
                cardInfo.Msg = "金额[value] 格式不正确";
                return "7";
            }
            cardInfo.OrderAmt = decimal.ToInt32(cardValue);

            #endregion
            //#region totalValue
            //decimal totalValue = 0M;
            //if (!decimal.TryParse(cardInfo.totalvalue, out totalValue))
            //{
            //    cardInfo.Msg = "金额[totalvalue] 格式不正确";
            //    return "7";
            //}
            //if (Convert.ToInt32(cardInfo.totalvalue) != Convert.ToInt32(cardInfo.value))
            //{
            //    string[] cardnoArray = cardInfo.cardpwd.Split(';');
            //    int total = cardnoArray.Length * Convert.ToInt32(cardInfo.value);
            //    if (total != Convert.ToInt32(cardInfo.totalvalue))
            //    {
            //        cardInfo.Msg = "总金额[totalvalue] 不正确";
            //        return "7";
            //    }
            //}
            //cardInfo.OrderAmt = decimal.ToInt32(totalValue);
            //#endregion

            #region userInfo
            var userInfo = BLL.User.Factory.GetCacheUserBaseInfo(userId);
            if (userInfo == null)
            {
                cardInfo.Msg = "商户不存在";
                return "8";//
            }
            if (userInfo.Status != 2)
            {
                cardInfo.Msg = "商户状态不正常";
                return "8";//
            }
            cardInfo.APIkey = userInfo.APIKey;
            cardInfo.ManageId = userInfo.manageId;
            #endregion

            if (!CheckSign(cardInfo, userInfo.APIKey, context))
            {
                cardInfo.Msg = "签名失败";
                return "3";//
            }

            #region chanelInfo
            string chanelNo = cardType.ToString("0000") +
               cardInfo.OrderAmt.ToString(CultureInfo.InvariantCulture);

            var chanelInfo = Channel.GetModel(chanelNo, userId, true);
            if (chanelInfo == null)
            {
                cardInfo.Msg = chanelNo + "通道不存在";
                return "2";//不支持该类卡或者该面值的卡
            }
            else if (chanelInfo.isOpen != 1)
            {
                cardInfo.Msg = chanelNo + "暂时停止该类卡或者该面值的卡交易";
                return "9";//业务状态不可用，未开通此类卡业务
            }
            else if (!chanelInfo.supplier.HasValue)
            {
                cardInfo.Msg = "未设置销卡接口";
                return "99";//
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
                return "13";
            }
            else
            {
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
                                            return "10"; //卡余额不足
                                        }
                                    }
                                    else
                                    {
                                        cardInfo.Msg = "充值卡无效";
                                        return "10";//卡余额不足
                                    }
                                    #endregion
                                }
                                else
                                {
                                    cardInfo.Msg = "充值卡无效";
                                    return "10";//不可以继续 补充了
                                }
                            }
                            else
                            {
                                cardInfo.Msg = "卡密码不正确";
                                return "16";//卡密不对
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
                            return "4";
                        }
                        break;
                    case 6:
                        cardInfo.Msg = "订单号已经存在";
                        return "6";
                    case 7:
                        cardInfo.Msg = "提交次数过多";
                        return "20";
                    case 8:
                        cardInfo.Msg = "卡密已被使用记录";
                        return "10";
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

            if (string.IsNullOrEmpty(cardInfo.type))
            {
                errCode = "1001";
                cardInfo.Msg = "参数 [type] 卡类型为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.parter))
            {
                errCode = "1002";
                cardInfo.Msg = "参数 [parter] 商户ID为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.cardno))
            {
                errCode = "1003";
                cardInfo.Msg = "参数 [cardno] 卡号为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.cardpwd))
            {
                errCode = "1004";
                cardInfo.Msg = "参数 [cardpwd] 卡密为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.value))
            {
                errCode = "1005";
                cardInfo.Msg = "参数 [cardInfo] 金额为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.restrict))
            {
                errCode = "1006";
                cardInfo.Msg = "参数 [restrict] 限制为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.orderid))
            {
                errCode = "1007";
                cardInfo.Msg = "参数 [orderid] 商户系统订单号为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.callbackurl))
            {
                errCode = "1008";
                cardInfo.Msg = "参数 [callbackurl] 异步通知地址为空";
            }
            else if (string.IsNullOrEmpty(cardInfo.sign))
            {
                errCode = "1009";
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

            if (cardInfo.type.Length > 2)
            {
                errCode = "2000";
                cardInfo.Msg = "参数 [type] 长度超过最长限制2";
            }
            else if (cardInfo.parter.Length > 8)
            {
                errCode = "2001";
                cardInfo.Msg = "参数 [parter] 长度超过最长限制8";
            }
            else if (cardInfo.cardno.Length > 30)
            {
                errCode = "2002";
                cardInfo.Msg = "参数 [cardno] 长度超过最长限制30";
            }
            else if (cardInfo.cardpwd.Length > 30)
            {
                errCode = "2003";
                cardInfo.Msg = "参数 [cardpwd] 长度超过最长限制30";
            }
            else if (cardInfo.value.Length > 4)
            {
                errCode = "2004";
                cardInfo.Msg = "参数 [value] 长度超过最长限制4";
            }
            else if (cardInfo.restrict.Length > 4)
            {
                errCode = "2005";
                cardInfo.Msg = "参数 [restrict] 长度超过最长限制4";
            }
            else if (cardInfo.orderid.Length > 32)
            {
                errCode = "2006";
                cardInfo.Msg = "参数 [orderid] 长度超过最长限制32";
            }
            else if (cardInfo.callbackurl.Length > 255)
            {
                errCode = "2007";
                cardInfo.Msg = "参数 [callbackurl] 长度超过最长限制255";
            }
            else if (cardInfo.attach.Length > 255)
            {
                errCode = "2008";
                cardInfo.Msg = "参数 [attach] 长度超过最长限制255";
            }
            else if (cardInfo.sign.Length != 32)
            {
                errCode = "2009";
                cardInfo.Msg = "参数 [sign] 长度不对";
            }
            return errCode;
        }
        #endregion

        #region GetChannelTypeId
        /// <summary>
        /// 获取通道类型Id
        /// </summary>
        /// <param name="cardtype">卡类型</param>
        /// <param name="cardno">卡号</param>
        /// <returns></returns>
        public static int GetChannelTypeId(int cardtype, string cardno)
        {
            int sysChannelTypeId = 0;

            try
            {
                sysChannelTypeId = ConverCardCode(cardtype);
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
        /// <param name="type"></param>
        /// <returns></returns>
        public static int ConverCardCode(int type)
        {
            int typeId = 0;
            switch (type)
            {
                case 1:
                    typeId = 107;//QQ卡
                    break;
                case 2:
                    typeId = 104;//盛大卡
                    break;
                case 3:
                    typeId = 106;//骏网卡
                    break;
                case 4:
                    typeId = 117;//亿卡通 =>纵游一卡通
                    break;
                case 5:
                    typeId = 111;//完美一卡通
                    break;
                case 6:
                    typeId = 112;//搜狐一卡通
                    break;
                case 7:
                    typeId = 105;//征途游戏卡
                    break;
                case 8:
                    typeId = 109;//久游一卡通
                    break;
                case 9:
                    typeId = 110;//网易一卡通
                    break;
                case 10:
                    typeId = 118;//魔兽卡=>天下一卡通
                    break;
                case 11:
                    typeId = 119;//联华卡=>天宏一卡通
                    break;
                case 12:
                    typeId = 113;//电信充值卡
                    break;
                case 13:
                    typeId = 103;//神州行充值卡
                    break;
                case 14:
                    typeId = 108;//联通充值卡
                    break;
                case 15:
                    typeId = 116;//金山一卡通
                    break;
                case 16:
                    typeId = 115;//光宇一卡通
                    break;
                case 17:
                    typeId = 200;//神州行浙江卡
                    break;
                case 18:
                    typeId = 201;//神州行江苏卡
                    break;
                case 19:
                    typeId = 202;//神州行辽宁卡
                    break;
                case 20:
                    typeId = 203;//神州行福建卡
                    break;
                case 21:
                    typeId = 118;//天下一卡通
                    break;
                case 22:
                    typeId = 119;//天宏一卡通
                    break;
                case 23:
                    typeId = 117;//天宏一卡通
                    break;
                case 26:
                    typeId = 208;//殴飞一卡通
                    break;
                case 27:
                    typeId = 209;//天下一卡通专项
                    break;
                case 28:
                    typeId = 210;//盛付通卡
                    break;

            }
            return typeId;
        }
        #endregion

        #region CheckSign
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <param name="apikey"></param>
        /// <returns></returns>
        public static bool CheckSign(CardInfo cardInfo, string apikey, HttpContext context)
        {
            try
            {
                string plain =
                    string.Format(
                        "type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}{8}"
                        , cardInfo.type
                        , cardInfo.parter
                        , context.Request.QueryString["cardno"]
                        , context.Request.QueryString["cardpwd"]
                        , cardInfo.value
                        , cardInfo.restrict
                        , cardInfo.orderid
                        , cardInfo.callbackurl
                        , apikey);

                string locationsign = Cryptography.MD5(plain).ToLower();

                if (locationsign == cardInfo.sign)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        #endregion

        #region CreateNotifyUrl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderinfo"></param>
        /// <param name="apikey"></param>
        /// <returns></returns>
        public static string CreateNotifyUrl(OrderCardInfo orderinfo, string apikey)
        {
            string notifyUrl = string.Empty;
            if (orderinfo == null || string.IsNullOrEmpty(apikey))
            {
                return notifyUrl;
            }
            notifyUrl = orderinfo.notifyurl;

            decimal facevalue = 0M;
            if (orderinfo.realvalue.HasValue)
                facevalue = decimal.Round(orderinfo.realvalue.Value, 0);

            string opstate = "";
            if (orderinfo.withhold_type == 2)
            {
                opstate = "18";
                orderinfo.userViewMsg = "余额不足";
            }
            else if (orderinfo.status == 2)
            {
                opstate = "0";
                orderinfo.userViewMsg = "支付成功";
            }
            else
            {
                opstate = ConvertErrorCode(
                orderinfo.method
                , orderinfo.supplierId
                , orderinfo.errtype
                , orderinfo.refervalue
                , facevalue);
            }

            string plain = string.Format("orderid={0}&opstate={1}&ovalue={2}{3}"
                , orderinfo.userorder
                , opstate
                , facevalue
                , apikey);

            string sign = Cryptography.MD5(plain);

            var parms = new StringBuilder();

            parms.AppendFormat("orderid={0}", UrlEncode(orderinfo.userorder));
            parms.AppendFormat("&opstate={0}", UrlEncode(opstate));
            parms.AppendFormat("&ovalue={0}", UrlEncode(facevalue.ToString()));
            parms.AppendFormat("&sysorderid={0}", UrlEncode(orderinfo.orderid));
            parms.AppendFormat("&systime={0}", UrlEncode(orderinfo.completetime.Value.ToString("yyyy/MM/dd HH:mm:ss")));
            parms.AppendFormat("&attach={0}", UrlEncode(orderinfo.attach));
            parms.AppendFormat("&msg={0}", UrlEncode(orderinfo.userViewMsg));
            parms.AppendFormat("&sign={0}", UrlEncode(sign));

            notifyUrl = notifyUrl + "?" + parms.ToString();

            return notifyUrl;
        }

        #endregion

        #region UrlEncode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public static string UrlEncode(string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
                return string.Empty;
            return System.Web.HttpUtility.UrlEncode(paramValue, System.Text.Encoding.GetEncoding("gb2312"));
        }
        #endregion

        #region ConvertErrorCode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="suppId"></param>
        /// <param name="errcode"></param>
        /// <param name="refervalue"></param>
        /// <param name="realvalue"></param>
        /// <returns></returns>
        public static string ConvertErrorCode(int method, int suppId, string errcode, decimal refervalue,
            decimal realvalue)
        {
            if (method == 2)
            {
                return errcode;
            }
            else
            {
                var supp = (SupplierCode)suppId;

                string syscode = "99";

                if (supp == SupplierCode.System) //
                {
                    syscode = errcode;
                }
                if (supp == SupplierCode.OfCard) //欧飞
                {
                    #region 欧飞

                    if (errcode == "2010")
                    {
                        syscode = "10"; //充值卡无效
                    }
                    else if (errcode == "2011")
                    {
                        syscode = "11"; //支付成功,实际面值{0}元
                    }
                    else if (errcode == "2012") //处理失败卡密未使用
                    {
                        syscode = "12";
                    }
                    else if (errcode == "2013") //系统繁忙
                    {
                        syscode = "13";
                    }
                    else if (errcode == "2016") //密码错误
                    {
                        syscode = "16";
                    }
                    else if (errcode == "2017") //匹配订单失败
                    {
                        syscode = "17";
                    }
                    else if (errcode == "2018") //余额不足
                    {
                        syscode = "18";
                    }
                    else if (errcode == "2019") //运营商维护
                    {
                        syscode = "19";
                    }

                    #endregion
                }
                else if (supp == SupplierCode.HuiYuan) //汇元
                {
                    #region HuiYuan

                    if (errcode == "9")
                    {
                        syscode = "16";
                    }
                    else if (errcode == "10")
                    {
                        syscode = "18"; //卡中余额不足
                    }
                    else if (errcode == "98") //接口维中
                    {
                        syscode = "19"; //
                    }

                    #endregion
                }
                else if (supp == SupplierCode.HuiSu) //汇速通
                {
                    #region HuiSu
                    //
                    switch (errcode)
                    {
                        case "201"://卡不符合规则
                            syscode = "2";//
                            break;
                        case "202"://不支持此卡种
                            syscode = "2";//
                            break;
                        case "203"://暂停此卡种
                            syscode = "9";//
                            break;
                        case "204"://不支持此面值
                            syscode = "2";//
                            break;
                        case "205"://暂停此面值
                            syscode = "9";//卡余额不足
                            break;

                        case "206"://运营商维护
                            syscode = "19";//
                            break;
                        case "207"://用户订单号重复
                            syscode = "6";//
                            break;
                        case "208"://卡有处理记录
                            syscode = "5";//
                            break;
                        case "209"://卡频繁提交
                            syscode = "20";//
                            break;
                    }

                    #endregion
                }
                else if (supp == SupplierCode.Card60866) //60866
                {
                    #region 60866

                    /*
                 1001	卡号或密码错误
1002	卡号过期
1003	卡余额不足
1004	卡号不存在
1005	卡已使用过
1006	卡号被冻结
1007	卡未激活
1008	不支持的卡类型或金额
1009	其他游戏专用卡

                 */
                    if (errcode == "1001")
                    {
                        syscode = "16";
                    }
                    else if (errcode == "1003")
                    {
                        syscode = "13"; //卡中余额不足
                    }
                    else
                    {
                        syscode = "10";
                    }

                    #endregion
                }
                if (supp == SupplierCode.LongBaoPay) //longbao
                {
                    syscode = errcode;
                }
                if (supp == SupplierCode.Shengpay) //Shengpay
                {
                    #region Shengpay

                    switch (errcode)
                    {
                        case "E1004": //充值卡类型有误
                        case "F1011": //卡类型有误
                            syscode = "2";
                            break;
                        case "F1032": //充值卡处理中
                            syscode = "4"; //订单内容重复
                            break;
                        case "E1007": //订单参数错误
                        case "F0601": //订单参数错误
                        case "F1021": //金额为数字
                            syscode = "7"; //数据非法
                            break;
                        case "F0501": //商户没开通
                        case "F1034": //商户没开通
                            syscode = "8"; //非法用户
                            break;
                        case "F1003": //不支持此种卡
                        case "F1023": //不支持此种卡
                            syscode = "9"; //暂停该类卡
                            break;
                        case "E1001": //系统繁忙，请稍后
                            syscode = "13";
                            break;
                        case "B053070":
                        case "B053051":
                        case "B053050":
                        case "B053049":
                        case "F1022": //卡失效
                            syscode = "10"; //充值卡无效
                            break;
                        //case "2011":
                        //    syscode = "11"; //支付成功,实际面值{0}元
                        //    break;
                        case "F1054":
                        case "F1055":
                            syscode = "12"; //支付失败卡密未使用
                            break;
                        case "F1100": //系统繁忙，请稍后
                            syscode = "13"; //系统繁忙
                            break;
                        case "F1038": //卡未激活
                            syscode = "15";
                            break;
                        case "B053071":
                        case "F0304": //卡号或者卡密不对
                        case "F0401": //卡号或者卡密不对
                        case "F1039": //卡号或者卡密不对
                            syscode = "16";
                            break;
                        //case "2017":
                        //    syscode = "17";
                        //    break;
                        case "S0513009":
                        case "S0513008":
                        case "B0513009":
                        case "B053052":
                        case "F0201": //余额不足
                            syscode = "18";
                            break;
                        case "E1002": //系统维护中
                        case "E1003": //系统维护中
                        case "E1005": //系统维护中
                        case "E1008": //系统维护中
                        case "F0101": //系统维护中
                        case "F1043": //系统维护中
                            syscode = "19";
                            break;
                        case "F0205": //操作过于频繁
                        case "F0402": //操作过于频繁
                        case "F1040": //请不要重复递交错误信息
                            syscode = "20";
                            break;
                        case "F0203": //面值有误
                        case "E0001": //IP地址不安全
                        case "E1006": //网络通讯故障
                        case "F1020": //面值有误
                        case "F1050": //金额有误
                            syscode = "99";
                            break;
                    }

                    #endregion
                }
                return syscode;
            }
        }



        #endregion

        #region 同步代码转化

        /// <summary>
        /// 
        /// </summary>
        private const string DefaultSysCode = "99";

        /// <summary>
        /// 转换接口商错误代码为系统统一错误代码
        /// </summary>
        /// <param name="supp">接口商</param>
        /// <param name="errcode">错误代码</param>
        /// <returns>系统标准错误代码</returns>
        public static string ConvertSynchronousErrorCode(SupplierCode supp, string errcode)
        {
            string syscode = DefaultSysCode;

            //if (supp == SupplierCode.HuiSu) //汇速通
            //{
            //    syscode = ConvetSyncCodeForHuiSu(errcode);
            //}
            //else 
            //if (supp == SupplierCode.Card51) //51
            //{
            //    syscode = ConvetSyncCodeForOf51Esales(errcode);
            //}
            //else 
            if (supp == SupplierCode.OfCard) //欧飞
            {
                syscode = ConvetSyncCodeForOfCard(errcode);
            }
            else if (supp == SupplierCode.Cared70) //70卡
            {
                syscode = ConvetSyncCodeFor70Card(errcode);
            }
            //else if (supp == SupplierCode.YeePay) //易宝
            //{
            //    syscode = errcode;
            //}
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
                    syscode = "4"; //
                    break;
                case "1007"://卡片或面值类型不正确
                case "1009"://卡号和密码不符合规范
                case "2000"://卡片信息不完整
                case "3000"://系统不能受理的卡
                case "3001"://系统检测到您的访问IP非法！
                case "3002"://系统检测不到您的访问IP！
                case "3003"://神州行充值卡密码不正确！
                    syscode = "10"; //充值卡无效
                    break;
                case "1010":
                    syscode = "13"; //系统繁忙稍后重试
                    break;
                case "1006"://系统关闭了该类卡支付
                case "2007"://系统正在维护中
                    syscode = "19"; //运营商维护
                    break;
                case "2001": //请正确请填写卡号密码
                case "2004": //您提交的卡号或密码有误
                    syscode = "16"; //密码错误
                    break;
                case "2005"://您的卡号密码已使用
                case "2009"://您的卡号密码重复使用
                    syscode = "5"; //该卡密已经有被使用的记录
                    break;
                case "2010"://系统检测到此卡重复提交失败三次，请一小时后重试
                case "2011":
                    syscode = "20"; //提交次数过多
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
                    syscode = "99";
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
                case "2000":
                    syscode = "0";
                    break;
                case "2001":
                    syscode = "1";
                    break;
                case "2002":
                    syscode = "2"; //不支持该类卡或者该面值的卡
                    break;
                case "2003":
                    syscode = "3";
                    break;
                case "2004":
                    syscode = "4";
                    break;
                case "2005":
                    syscode = "5"; //
                    break;
                case "2006":
                    syscode = "6";
                    break;
                case "2007":
                    syscode = "7";
                    break;
                case "2008":
                    syscode = "8";
                    break;
                case "2009":
                    syscode = "9"; //运营商维护
                    break;
                case "2010":
                    syscode = "10"; //充值卡无效
                    break;
                case "2011":
                    syscode = "11"; //充值卡无效
                    break;
                case "2012":
                    syscode = "12"; //处理失败卡密未使用
                    break;
                case "2013":
                    syscode = "13";
                    break;
                case "2014":
                    syscode = "14";
                    break;
                case "2015":
                    syscode = "15";
                    break;
                case "2016":
                    syscode = "16"; //密码错误
                    break;
                case "2017":
                    syscode = "17";
                    break;
                case "2018":
                    syscode = "18";
                    break;
                case "2019":
                    syscode = "19";
                    break;
                case "2020":
                    syscode = "20";
                    break;
                case "2999":
                    syscode = "99";
                    break;

            }

            #endregion

            return syscode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errcode"></param>
        /// <returns></returns>
        public static string ConvetSyncCodeFor70Card(string errcode)
        {
            string syscode = DefaultSysCode;

            #region 欧飞

            switch (errcode)
            {
                case "0": //未知
                    syscode = "99";
                    break;
                case "1": //卡号或密码错误
                    syscode = "16";
                    break;
                case "2": //卡号过期
                    syscode = "1";
                    break;
                case "3": //卡余额不足
                case "4": //卡号不存在
                case "5": //卡已使用过
                case "6": //卡号被冻结
                case "7": //卡未激活
                case "8": //卡不支持的类型或金额
                    syscode = "2";
                    break;
                case "9": //其它游戏专用卡
                case "10"://卡面值错误
                case "1000":  //卡未使用请重新提交
                case "2001":  //参数为空
                case "2002":  //无效的商户
                case "2003":  //签名错误
                case "2008":  //订单号已存在
                    syscode = "6";
                    break;
                case "2009":  //产品被用户关闭或维护
                    syscode = "19";
                    break;
                case "2011":  //卡号或卡密长度错误或金额不正确
                case "2014":  //该卡已超过系统规定的失败次数
                    syscode = "20";
                    break;
                case "2015":  //该卡提交前已使用
                case "2016":  //该卡已失败（我们数据库里有失败原因的记录）
                    syscode = "1";
                    break;
                case "2017"://该卡正在处理中
                    syscode = "2"; //不支持该类卡或者该面值的卡
                    break;
                case "3000":
                    syscode = "99"; //
                    break;
            }

            #endregion

            return syscode;
        }
        #endregion

        #region ConvetSyncCodeForChargeCardDirect
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errcode"></param>
        /// <returns></returns>
        public static string ConvetSyncCodeForChargeCardDirect(string errcode)
        {
            string syscode = DefaultSysCode;

            switch (errcode)
            {
                case "-1"://签名较验失败或未知错误
                case "5"://卡数量过多，目前最多支持10张卡
                case "11"://订单号重复
                case "95"://支付方式未开通
                case "112"://业务状态不可用，未开通此类卡业务
                    syscode = DefaultSysCode;
                    break;
                case "2"://卡密成功处理过或者提交卡号过于频繁
                    syscode = "5"; //该卡密已经有被使用的记录
                    break;
                case "66"://支付金额有误
                    syscode = "8"; //运营商维护
                    break;
            }

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
                        sysCode = "10";//充值卡无效
                    }
                    break;
                case "-1"://创单失败（具体可参考返回的ret_msg）
                    sysCode = "13";
                    break;
                case "1":
                    sysCode = "99"; //传入参数有误（具体可参考返回的ret_msg）
                    break;
                case "2":
                    sysCode = "99"; //代理商ID错误 或 未开通该服务
                    break;
                case "3":
                    sysCode = "99"; //IP验证错误
                    break;
                case "4":
                    sysCode = "99"; //签名验证错误
                    break;
                case "5":
                    sysCode = "99"; //重复的订单号
                    break;
                case "8":
                    sysCode = "99"; //单据不存在
                    break;
                case "22":
                    sysCode = "99"; //卡号卡密格式加密错误
                    break;
                case "98"://接口维中
                    sysCode = "19";
                    break;
                    //case "99":
                    //    sysCode = "99"; //系统错误,未知（需要查询后在处理单据状态）
                    //    break;
            }

            #endregion

            return sysCode;
        }

        #endregion

        #endregion

        #region GetMessageByCode
        /// <summary>
        /// 系统错误代码定义（与接口文档中相对应）
        /// </summary>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public static string GetMessageByCode(string errCode)
        {
            string message = errCode;

            switch (errCode)
            {
                case "0":
                    message = "支付成功";
                    break;
                case "1":
                    message = "数据接收成功";
                    break;
                case "2":
                    message = "不支持该类卡或者该面值的卡";
                    break;
                case "3":
                    message = "签名验证失败";
                    break;
                case "4":
                    message = "订单内容重复";
                    break;
                case "5":
                    message = "该卡密已经有被使用的记录";
                    break;
                case "6":
                    message = "订单号已经存在";
                    break;
                case "7":
                    message = "数据非法";
                    break;
                case "8":
                    message = "非法用户";
                    break;
                case "9":
                    message = "暂时停止该类卡或者该面值的卡交易";
                    break;
                case "10":
                    message = "充值卡无效";
                    break;
                case "11":
                    message = "支付成功,实际面值{0}元";
                    break;
                case "12":
                    message = "处理失败卡密未使用";
                    break;
                case "13":
                    message = "系统繁忙";
                    break;
                case "14":
                    message = "不存在该笔订单";
                    break;
                case "15":
                    message = "未知请求";
                    break;
                case "16":
                    message = "密码错误";
                    break;
                case "17":
                    message = "匹配订单失败";
                    break;
                case "18":
                    message = "余额不足";
                    break;
                case "19":
                    message = "运营商维护";
                    break;
                case "20":
                    message = "提交次数过多";
                    break;
                case "99":
                    message = "其他错误";
                    break;
            }

            return message;
        }
        #endregion

        #region SeachMD5Check
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="userid"></param>
        /// <param name="key"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool SeachMD5Check(string orderid, string userid, string key, string sign)
        {
            string md5Str = string.Format("orderid={0}&parter={1}{2}"
               , orderid, userid, key);

            md5Str = viviLib.Security.Cryptography.MD5(md5Str).ToLower();
            if (md5Str == sign)
            {
                return true;
            }
            return false;
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

            string value = string.Empty;
            try
            {
                value = context.Request[strParaName];
            }
            catch
            {

            }

            if (String.IsNullOrEmpty(value))
                return "";


            return HttpUtility.UrlDecode(value, Encoding.GetEncoding("GB2312"));
        }
        #endregion
    }
}
