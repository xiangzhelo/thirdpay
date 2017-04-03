using System;
using System.Globalization;
using System.Text;
using System.Web;
using viviapi.BLL.Channel;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviLib.Security;

namespace viviapi.SysInterface.Card.Eka
{
    /// <summary>
    /// 
    /// </summary>
    public class Utility
    {
        public static string EnName = "vc.ek";
        public static string ChineseName = "亿卡";
        public static string NotifySuccessflag = "opstate=0";

        #region CheckParameter
        /// <summary>
        /// -1 提交成功
        /// 7 数据非法
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <returns></returns>
        public static string CheckParameter(CardInfo cardInfo)
        {
            string errCode = "";

            if (cardInfo == null)
            {
                return "-1";
            }

            errCode = CheckParamsEmpty(cardInfo);
            if (!string.IsNullOrEmpty(errCode))
                return "-1";

            errCode = CheckParamsLength(cardInfo);
            if (!string.IsNullOrEmpty(errCode))
                return "-1";

            if (!Card.Utility.VerifyCardNoFormat(cardInfo.cardno))
            {
                cardInfo.Msg = "参数 [cardno] 格式不正确";
                return "-1";
            }
            cardInfo.CardNo = cardInfo.cardno;
            if (!Card.Utility.VerifyCardPwdFormat(cardInfo.cardpwd))
            {
                cardInfo.Msg = "参数 [cardpwd] 格式不正确";
                return "-1";
            }
            cardInfo.CardPwd = cardInfo.cardpwd;


            #region cardType
            int cardType = 0;
            if (!int.TryParse(cardInfo.type, out cardType))
            {
                cardInfo.Msg = "类型[type] 格式不正确";
                return "-1";
            }
            cardInfo.CardType = cardType;
            #endregion

            #region typeId
            int typeId = GetChannelTypeId(cardInfo.CardType, cardInfo.cardno);
            if (typeId == 0)
            {
                cardInfo.Msg = "支付通道不存在";
                return "-1";//不支持该类卡或者该面值的卡
            }
            cardInfo.TypeId = typeId;
            #endregion

            #region userId
            int userId = 0;
            if (!int.TryParse(cardInfo.parter, out userId))
            {
                cardInfo.Msg = "商户ID[parter] 格式不正确";
                return "-1";
            }
            cardInfo.UserId = userId;
            #endregion

            #region cardValue
            decimal cardValue = 0M;
            if (!decimal.TryParse(cardInfo.value, out cardValue))
            {
                cardInfo.Msg = "金额[value] 格式不正确";
                return "-1";
            }
            cardInfo.OrderAmt = decimal.ToInt32(cardValue);
            #endregion

            #region userInfo
            var userInfo = BLL.User.Factory.GetCacheUserBaseInfo(userId);
            if (userInfo == null)
            {
                cardInfo.Msg = "商户不存在";
                return "-1";//
            }
            if (userInfo.Status != 2)
            {
                cardInfo.Msg = "商户状态不正常";
                return "-1";//
            }
            cardInfo.APIkey = userInfo.APIKey;
            cardInfo.ManageId = userInfo.manageId;
            #endregion

            if (!CheckSign(cardInfo, userInfo.APIKey))
            {
                cardInfo.Msg = "签名失败";
                return "-2";//
            }

            #region chanelInfo
            string chanelNo = cardType.ToString("0000") +
               cardInfo.OrderAmt.ToString(CultureInfo.InvariantCulture);

            var chanelInfo = Channel.GetModel(chanelNo, userId, true);
            if (chanelInfo == null)
            {
                cardInfo.Msg = chanelNo + "通道不存在";
                return "-1";//不支持该类卡或者该面值的卡
            }
            else if (chanelInfo.isOpen != 1)
            {
                cardInfo.Msg = chanelNo + "暂时停止该类卡或者该面值的卡交易";
                return "-1";//业务状态不可用，未开通此类卡业务
            }
            else if (!chanelInfo.supplier.HasValue)
            {
                cardInfo.Msg = "未设置销卡接口";
                return "-999";//
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
                return "-999";
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
                                            return "-1"; //卡余额不足
                                        }
                                    }
                                    else
                                    {
                                        cardInfo.Msg = "充值卡无效";
                                        return "-1";//卡余额不足
                                    }
                                    #endregion
                                }
                                else
                                {
                                    cardInfo.Msg = "充值卡无效";
                                    return "-1";//不可以继续 补充了
                                }
                            }
                            else
                            {
                                cardInfo.Msg = "卡密码不正确";
                                return "-1";//卡密不对
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
                            return "-4";
                        }
                        break;
                    case 6:
                        cardInfo.Msg = "订单号已经存在";
                        return "-1";
                    case 7:
                        cardInfo.Msg = "提交次数过多";
                        return "-1";
                }
            }
            #endregion

            return "0";
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
        /// 
        /// </summary>
        /// <param name="cardtype"></param>
        /// <param name="cardno"></param>
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
        public static bool CheckSign(CardInfo cardInfo, string apikey)
        {
            try
            {
                string plain =
                    string.Format(
                        "type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}{8}"
                        , cardInfo.type
                        , cardInfo.parter
                        , cardInfo.cardno
                        , cardInfo.cardpwd
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

        #region 同步代码转化

        /// <summary>
        /// 
        /// </summary>
        private const string DefaultSysCode = "-4";

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
                syscode = errcode;
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
                case "1006"://系统关闭了该类卡支付
                case "2007"://系统正在维护中
                    syscode = "-999"; //运营商维护
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
                case "2009":
                    syscode = "-999"; //运营商维护
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
                case "66"://支付金额有误
                    syscode = "-999"; //运营商维护
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
                        sysCode = "-4";//充值卡无效
                    }
                    break;
                case "-1"://创单失败（具体可参考返回的ret_msg）
                    sysCode = "-1";
                    break;
                case "1":
                    sysCode = "-1"; //传入参数有误（具体可参考返回的ret_msg）
                    break;
                case "2":
                    sysCode = "-1"; //代理商ID错误 或 未开通该服务
                    break;
                case "3":
                    sysCode = "-1"; //IP验证错误
                    break;
                case "4":
                    sysCode = "-1"; //签名验证错误
                    break;
                case "5":
                    sysCode = "-1"; //重复的订单号
                    break;
                case "8":
                    sysCode = "-1"; //单据不存在
                    break;
                case "22":
                    sysCode = "-1"; //卡号卡密格式加密错误
                    break;
                case "98"://接口维中
                    sysCode = "-999";
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
                case "0":
                    message = "已经记录该卡，正在等待被使用";
                    break;
                case "-1":
                    message = "请求参数无效";
                    break;
                case "-2":
                    message = "签名错误";
                    break;
                case "-3":
                    message = "卡密为重复提交，系统不进行消耗且不进入下行流程";
                    break;
                case "-4":
                    message = "卡密不符合定义的卡号密码面值规则，系统不进行消耗且不进入下行流程";
                    break;
                case "-999":
                    message = "接口维护中";
                    break;
            }

            return message;
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
                return string.Empty;

            decimal facevalue = 0M;
            if (orderinfo.realvalue.HasValue)
                facevalue = decimal.Round(orderinfo.realvalue.Value, 0);

            string systime = "";
            if (orderinfo.completetime.HasValue)
                systime = orderinfo.completetime.Value.ToString("yyyy/MM/dd HH:mm:ss");

            string opstate = "";

            if (orderinfo.withhold_type == 2)
            {
                opstate = "-4";
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
                                  , apiKey);

            string sign = Cryptography.MD5(plain);

            var parms = new StringBuilder();

            parms.AppendFormat("orderid={0}", HttpUtility.UrlEncode(orderinfo.userorder));

         
            if (orderinfo.status == 2)
            {
                if (facevalue >= orderinfo.refervalue)
                {
                    opstate = "0";
                }
                else
                {
                    opstate = "-3";
                }
            }
            else
            {
                opstate = "-1";
            }
            //if (orderinfo.method == 2)
            //{
            //    opstate = GetReturnOpstate(orderinfo.errtype);
            //}

            parms.AppendFormat("&opstate={0}", HttpUtility.UrlEncode(opstate));
            parms.AppendFormat("&ovalue={0}", HttpUtility.UrlEncode(facevalue.ToString(CultureInfo.InvariantCulture)));
            parms.AppendFormat("&ekaorderid={0}", HttpUtility.UrlEncode(orderinfo.orderid));
            parms.AppendFormat("&ekatime={0}", HttpUtility.UrlEncode(systime));

            parms.AppendFormat("&attach={0}", UrlEncode(orderinfo.attach));
            parms.AppendFormat("&msg={0}", UrlEncode(orderinfo.userViewMsg));
            parms.AppendFormat("&sign={0}", UrlEncode(sign));

            return notifyUrl = notifyUrl + "?" + parms.ToString();
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
                return "-4";
            }
            else
            {
                var supp = (SupplierCode)suppId;

                string syscode = "-1";

                if (supp == SupplierCode.System) //
                {
                    syscode = errcode;
                }
                if (supp == SupplierCode.OfCard) //欧飞
                {
                    #region 欧飞

                    if (errcode == "2010")
                    {
                        syscode = "-1"; //充值卡无效
                    }
                    else if (errcode == "2011")
                    {
                        syscode = "-3"; //支付成功,实际面值{0}元
                    }
                    else if (errcode == "2012") //处理失败卡密未使用
                    {
                        syscode = "-1";
                    }
                    else if (errcode == "2013") //系统繁忙
                    {
                        syscode = "-5";
                    }
                    else if (errcode == "2016") //密码错误
                    {
                        syscode = "-1";
                    }
                    else if (errcode == "2017") //匹配订单失败
                    {
                        syscode = "-5";
                    }
                    else if (errcode == "2018") //余额不足
                    {
                        syscode = "-1";
                    }
                    else if (errcode == "2019") //运营商维护
                    {
                        syscode = "-5";
                    }

                    #endregion
                }
                else if (supp == SupplierCode.HuiYuan) //汇元
                {
                    #region HuiYuan

                    if (errcode == "9")
                    {
                        syscode = "-1";
                    }
                    else if (errcode == "10")
                    {
                        syscode = "-1"; //卡中余额不足
                    }
                    else if (errcode == "98") //接口维中
                    {
                        syscode = "-5"; //
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
                            syscode = "-5";//
                            break;
                        case "202"://不支持此卡种
                            syscode = "-5";//
                            break;
                        case "203"://暂停此卡种
                            syscode = "-5";//
                            break;
                        case "204"://不支持此面值
                            syscode = "-5";//
                            break;
                        case "205"://暂停此面值
                            syscode = "-5";//卡余额不足
                            break;

                        case "206"://运营商维护
                            syscode = "-5";//
                            break;
                        case "207"://用户订单号重复
                            syscode = "-5";//
                            break;
                        case "208"://卡有处理记录
                            syscode = "-5";//
                            break;
                        case "209"://卡频繁提交
                            syscode = "-5";//
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
                        syscode = "-1";
                    }
                    else if (errcode == "1003")
                    {
                        syscode = "-1"; //卡中余额不足
                    }
                    else
                    {
                        syscode = "-5";
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
                            syscode = "-1";
                            break;
                        case "F1032": //充值卡处理中
                            syscode = "-5"; //订单内容重复
                            break;
                        case "E1007": //订单参数错误
                        case "F0601": //订单参数错误
                        case "F1021": //金额为数字
                            syscode = "-5"; //数据非法
                            break;
                        case "F0501": //商户没开通
                        case "F1034": //商户没开通
                            syscode = "-5"; //非法用户
                            break;
                        case "F1003": //不支持此种卡
                        case "F1023": //不支持此种卡
                            syscode = "-5"; //暂停该类卡
                            break;
                        case "E1001": //系统繁忙，请稍后
                            syscode = "-5";
                            break;
                        case "B053070":
                        case "B053051":
                        case "B053050":
                        case "B053049":
                        case "F1022": //卡失效
                            syscode = "-4"; //充值卡无效
                            break;
                        //case "2011":
                        //    syscode = "11"; //支付成功,实际面值{0}元
                        //    break;
                        case "F1054":
                        case "F1055":
                            syscode = "-5"; //支付失败卡密未使用
                            break;
                        case "F1100": //系统繁忙，请稍后
                            syscode = "-5"; //系统繁忙
                            break;
                        case "F1038": //卡未激活
                            syscode = "-1";
                            break;
                        case "B053071":
                        case "F0304": //卡号或者卡密不对
                        case "F0401": //卡号或者卡密不对
                        case "F1039": //卡号或者卡密不对
                            syscode = "-1";
                            break;
                        //case "2017":
                        //    syscode = "17";
                        //    break;
                        case "S0513009":
                        case "S0513008":
                        case "B0513009":
                        case "B053052":
                        case "F0201": //余额不足
                            syscode = "-4";
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
                            syscode = "-5";
                            break;
                        case "F0203": //面值有误
                        case "E0001": //IP地址不安全
                        case "E1006": //网络通讯故障
                        case "F1020": //面值有误
                        case "F1050": //金额有误
                            syscode = "-5";
                            break;
                    }

                    #endregion
                }
                return syscode;
            }
        }



        #endregion

        public static string UrlEncode(string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
                return string.Empty;

            return HttpUtility.UrlEncode(paramValue, Encoding.GetEncoding("gb2312"));
        }

        public static string GetReturnOpstate(string errtype)
        {
            if (errtype == "0")
            {
                return "0";
            }
            return "-1";
        }
    }
}
