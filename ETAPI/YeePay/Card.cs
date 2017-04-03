using System;
using System.Globalization;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.ETAPI.YeePay.Lib;
using viviapi.ETAPI.YeePay.Lib.com.yeepay.cmbn;
using viviapi.Model.Order.Card;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.Model.Order;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.YeePay
{

    public class Card : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.YeePay;

        public Card()
            : base(SuppId)
        {

        }

        public static Card Default
        {
            get
            {
                var instance = new Card();
                return instance;
            }
        }

        public string NotifyUrl
        {
            get
            {
                return SiteDomain + "/receive/yeepay/card.aspx"; ;
            }
        }

        public string CardTypeNo(int code)
        {
            switch (code)
            {
                case 103:
                    return "SZX";

                case 104:
                    return "SNDACARD";

                case 105:
                    return "ZHENGTU";

                case 106:
                    return "JUNNET";

                case 107:
                    return "QQCARD";

                case 108:
                    return "UNICOM";

                case 109:
                    return "JIUYOU";

                case 110:
                    return "NETEASE";

                case 111:
                    return "WANMEI";

                case 112:
                    return "SOHU";

                case 113:
                    return "TELECOM";

                case 115://光宇
                    return "GUANGYU";

                case 117://纵游
                    return "ZONGYOU";

                case 118://天下
                    return "TIANXIA";

                case 119://天宏
                    return "TIANHONG";

                default:
                    return string.Empty;
            }
        }
        internal string Succflag = "SUCCESS";
        internal string Failflag = "FAIL";

        //public string GetPayUrlProfessional(OrderCardInfo order)
        //{ 

        //}
        private string p2_Order, p3_Amt, p4_verifyAmt, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, pa8_cardNo, pa9_cardPwd, pa_MP, pa7_cardAmt, pd_FrpId, pr_NeedResponse, pz_userId, pz1_userRegTime;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string GetPayUrl(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string errmsg)
        {
            errmsg = string.Empty;
            string puserid = this.SuppAccount;
            string puserkey = this.SuppKey;

            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");

            p2_Order = _orderid;
            p3_Amt = decimal.Parse(cardvalue.ToString()).ToString("0.00");
            p4_verifyAmt = "false";
            p5_Pid = "product";
            p6_Pcat = "producttype";
            p7_Pdesc = "productdesc";
            p8_Url = this.SiteDomain + "/notify/YeePay_Card_Return.aspx";
            pa_MP = "";
            pa7_cardAmt = decimal.Parse(cardvalue.ToString()).ToString("0.00");
            pa8_cardNo = _cardno;
            pa9_cardPwd = _cardpwd;
            pd_FrpId = CardTypeNo(_typeId);
            pr_NeedResponse = "1";
            pz_userId = puserid;
            pz1_userRegTime = "";

            try
            {
                SZXResult result = SZX.AnnulCard(SuppKey, SuppAccount, p2_Order, p3_Amt, p4_verifyAmt, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url,
              pa_MP, pa7_cardAmt, pa8_cardNo, pa9_cardPwd, pd_FrpId, pr_NeedResponse, pz_userId, pz1_userRegTime);

                if (result.R1_Code == "1")
                {
                    return "0";//success
                }
                if (result.R1_Code == "11")
                {
                    errmsg = "订单号重复";
                    return "-1";//订单号重复
                }
                if (result.R1_Code == "7")
                {
                    errmsg = "卡密无效";
                    return "-1";//卡密无效
                }
                if (result.R1_Code == "61")
                {
                    errmsg = "卡密无效";
                    return "-1";//卡密无效
                }
                errmsg = "未知错误，错误编码：" + result.R1_Code;
                return "-1";// ("未知错误，错误编码：" + result.R1_Code);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return "-1";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public CardSynchCallBack CardSend(CardOrderSummitArgs o)
        {
            var callBack = new CardSynchCallBack();

            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");

            p2_Order = o.SysOrderNo;
            p3_Amt = decimal.Parse(o.FaceValue.ToString(CultureInfo.InvariantCulture)).ToString("0.00");
            p4_verifyAmt = "false";
            p5_Pid = "product";
            p6_Pcat = "producttype";
            p7_Pdesc = "productdesc";
            p8_Url = NotifyUrl;
            pa_MP = "";
            pa7_cardAmt = decimal.Parse(o.FaceValue.ToString(CultureInfo.InvariantCulture)).ToString("0.00");
            pa8_cardNo = o.CardNo;
            pa9_cardPwd = o.CardPass;
            pd_FrpId = CardTypeNo(o.CardTypeId);
            pr_NeedResponse = "1";
            pz_userId = SuppAccount;
            pz1_userRegTime = "";

            try
            {
                SZXResult result = SZX.AnnulCard(SuppKey, SuppAccount, p2_Order, p3_Amt, p4_verifyAmt, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url,
              pa_MP, pa7_cardAmt, pa8_cardNo, pa9_cardPwd, pd_FrpId, pr_NeedResponse, pz_userId, pz1_userRegTime);

                callBack.Success = 1;

                callBack.SuppErrorCode = result.R1_Code;
                callBack.SuppErrorMsg  = result.Rq_ReturnMsg;

                if (result.R1_Code == "1")
                {
                    callBack.SummitStatus = 1;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                callBack.Success = 0;
            }
            return callBack;
        }


        public void Return(HttpContext context)
        {
            //SZX.logURL(context.Request.RawUrl);

            // 校验返回数据包
            SZXCallbackResult result = SZX.VerifyCallback(SuppKey
                , FormatQueryString.GetQueryString("r0_Cmd")
                , FormatQueryString.GetQueryString("r1_Code")
                , FormatQueryString.GetQueryString("p1_MerId")
                , FormatQueryString.GetQueryString("p2_Order")
                , FormatQueryString.GetQueryString("p3_Amt")
                , FormatQueryString.GetQueryString("p4_FrpId")
                , FormatQueryString.GetQueryString("p5_CardNo")
                , FormatQueryString.GetQueryString("p6_confirmAmount")
                , FormatQueryString.GetQueryString("p7_realAmount")
                , FormatQueryString.GetQueryString("p8_cardStatus")
                , FormatQueryString.GetQueryString("p9_MP")
                , FormatQueryString.GetQueryString("pb_BalanceAmt")
                , FormatQueryString.GetQueryString("pc_BalanceAct")
                , FormatQueryString.GetQueryString("hmac"));

            if (string.IsNullOrEmpty(result.ErrMsg))
            {
                //使用应答机制时 必须回写success
                string viewMsg = "";
                string msg = GetMsgInfo(result.P8_cardStatus);
                string opstate = "-1";
                /*成功还是失败*/
                int status = (result.R1_Code == "1") ? 2 : 4;
                if (status == 2)
                {
                    opstate = "0";
                    viewMsg = "支付成功";
                }
                else
                {
                    viewMsg = msg;
                }

                var response = new CardOrderSupplierResponse()
                {
                    SupplierId = SuppId,
                    SuppTransNo = "",
                    SysOrderNo = result.P2_Order,
                    OrderAmt = decimal.Parse(result.P3_Amt),
                    SuppAmt = 0M,
                    OrderStatus = status,
                    SuppErrorCode = result.P8_cardStatus,
                    Opstate = opstate,
                    SuppErrorMsg = result.ErrMsg,
                    ViewMsg = viewMsg,
                    Method = 1
                };

                OrderCardUtils.SuppNotify(response, Succflag);
            }
            else
            {
                context.Response.Write("交易签名无效!");
                context.Response.Write("<BR>YeePay-HMAC:" + result.Hmac);
                context.Response.Write("<BR>LocalHost:" + result.ErrMsg);
            }
        }

        public string GetMsgInfo(string cardstatus)
        {
            switch (cardstatus)
            {
                case "0":
                    return "支付成功";// "销卡成功 订单成功";
                case "1":
                    return "支付成功"; //"销卡成功，订单失败";
                case "7":
                    return "卡号卡密或卡面额不符合规则";
                case "1002":
                    return "本张卡密您提交过于频繁，请您稍后再试";
                case "1003":
                    return "不支持的卡类型（比如电信地方卡）";
                case "1004":
                    return "密码错误或充值卡无效";
                case "1006":
                    return "充值卡无效";
                case "1007":
                    return "卡内余额不足";
                case "1008":
                    return "余额卡过期（有效期1个月）";
                case "1010":
                    return "此卡正在处理中";
                case "10000":
                    return "未知错误";
                case "2005":
                    return "此卡已使用";
                case "2006":
                    return "卡密在系统处理中";
                case "2007":
                    return "该卡为假卡";
                case "2008":
                    return "该卡种正在维护";
                case "2009":
                    return "浙江省移动维护";
                case "2010":
                    return "江苏省移动维护";
                case "2011":
                    return "福建省移动维护";
                case "2012":
                    return "辽宁省移动维护";
                case "2013":
                    return "该卡已被锁定";
                case "2014":
                    return "系统繁忙，请稍后再试";
                default:
                    return cardstatus;
            }
            return cardstatus;
        }
    }
}

