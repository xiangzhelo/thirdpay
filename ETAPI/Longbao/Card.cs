namespace vivipai.ETAPI.Longbao
{
    using vivipai.ETAPI;
    using viviLib.ExceptionHandling;
    using viviLib.Security;
    using viviLib.Web;
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;
    using viviapi.ETAPI.Common;
    using viviapi.BLL;

    public class Card : ETAPIBase
    {
        private static int suppId = 700;

        public Card() : base(suppId)
        {
        }

        public void CardNotify()
        {
            HttpRequest request = HttpContext.Current.Request;
            string orderId = request.QueryString["orderid"];
            string str2 = request.QueryString["opstate"];
            string str3 = request.QueryString["ovalue"];
            string str4 = request.QueryString["sign"];
            string supplierOrderId = request.QueryString["sysorderid"];
            string str6 = request.QueryString["systime"];
            string str8 = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", new object[] { orderId, str2, str3, base.SuppKey }));
            try
            {
                if (str8 == str4)
                {
                    string opstate = "-1";
                    int status = 4;
                    if (str2.ToLower() == "0")
                    {
                        opstate = "0";
                        status = 2;
                    }
                    OrderCard card = new OrderCard();
                    string lBMsgInfo = this.GetLBMsgInfo("opstate=" + opstate, str3);
                    string userviewmsg = lBMsgInfo;
                    //card.ReceiveSuppResult(suppId, orderId, supplierOrderId, status, opstate, lBMsgInfo, userviewmsg, decimal.Parse(str3), 0M, opstate, 1);
                    //HttpContext.Current.Response.Write("opstate=0");
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string supperrorcode, out string errmsg)
        {
            errmsg = string.Empty;
            supporderid = string.Empty;
            supperrorcode = string.Empty;
            string str = "-1";
            string SuppKey = base.SuppKey;
            if (!string.IsNullOrEmpty(base.PostCardUrl) && !string.IsNullOrEmpty(SuppKey))
            {
                string url = base.PostCardUrl + "?";
                string str4 = this.GetCardType(_typeId).ToString();
                string str5 = "0";
                string str6 = string.Format("type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}", new object[] { str4, base.SuppAccount, _cardno, _cardpwd, cardvalue, str5, _orderid, this.notify_url });
                string str7 = Cryptography.MD5(str6 + base.SuppKey);
                try
                {
                    StringBuilder builder = new StringBuilder(str6);
                    builder.AppendFormat("&sign={0}", str7);
                    string retcode = WebClientHelper.GetString(url, builder.ToString(), "GET", Encoding.GetEncoding("GB2312"), 0x2710);
                    supperrorcode = retcode.Replace("opstate=", "");
                    errmsg = this.GetLBMsgInfo(retcode, string.Empty);
                    if (retcode == "opstate=1")
                    {
                        str = "0";
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
            }
            return str;
        }

        public bool Finish(string retText)
        {
            bool flag = false;
            try
            {
                if (string.IsNullOrEmpty(retText))
                {
                    return flag;
                }
                string[] strArray = retText.Split(new char[] { '&' });
                string str = strArray[0].Replace("orderid=", "");
                string str2 = strArray[1].Replace("opstate=", "");
                string str3 = strArray[2].Replace("ovalue=", "");
                string str4 = strArray[3].Replace("sign=", "");
                if (!(Cryptography.MD5(string.Format("orderid={0}&opstate={0}&ovalue={0}", str, str2, str3) + base.SuppKey) == str4))
                {
                    return flag;
                }
                string str6 = string.Empty;
                int status = 4;
                if (!(str2 != "1"))
                {
                    return flag;
                }
                if (str2 == "2")
                {
                    str6 = "0";
                }
                else
                {
                    status = 4;
                    str6 = str2;
                }
                if (!string.IsNullOrEmpty(str6))
                {
                    //new OrderCard().ReceiveSuppResult(suppId, str, string.Empty, status, str6, str2, decimal.Parse(str3), 0M, str2);
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            return flag;
        }

        public int GetCardType(int paytype)
        {
            int num = paytype;
            if (paytype == 0x67)
            {
                return 13;
            }
            if (paytype == 0x68)
            {
                return 2;
            }
            if (paytype == 0x69)
            {
                return 7;
            }
            if (paytype == 0x6a)
            {
                return 3;
            }
            if (paytype == 0x6b)
            {
                return 1;
            }
            if (paytype == 0x6c)
            {
                return 14;
            }
            if (paytype == 0x6d)
            {
                return 8;
            }
            if (paytype == 110)
            {
                return 9;
            }
            if (paytype == 0x6f)
            {
                return 5;
            }
            if (paytype == 0x70)
            {
                return 6;
            }
            if (paytype == 0x71)
            {
                return 12;
            }
            if (paytype == 200)
            {
                return 0x11;
            }
            if (paytype == 0xc9)
            {
                return 0x12;
            }
            if (paytype == 0xca)
            {
                return 0x13;
            }
            if (paytype == 0xcb)
            {
                return 20;
            }
            if (paytype == 0xcc)
            {
                return 10;
            }
            if (paytype == 0xcd)
            {
                return 11;
            }
            if (paytype == 210)
            {
                num = 0x1c;
            }
            return num;
        }

        public string GetLBMsgInfo(string retcode, string _ovalue)
        {
            switch (retcode)
            {
                case "opstate=0":
                    return "支付成功";

                case "opstate=1":
                    return "数据接收成功";

                case "opstate=2":
                    return "不支持该类卡或者该面值的卡";

                case "opstate=3":
                    return "签名验证失败";

                case "opstate=4":
                    return "订单内容重复";

                case "opstate=5":
                    return "该卡密已经有被使用的记录";

                case "opstate=6":
                    return "订单号已经存在";

                case "opstate=7":
                    return "数据非法";

                case "opstate=8":
                    return "非法用户";

                case "opstate=9":
                    return "暂时停止该类卡或者该面值的卡交易";

                case "opstate=10":
                    return "充值卡无效";

                case "opstate=11":
                    return string.Format("支付成功,实际面值{0}元", _ovalue);

                case "opstate=12":
                    return "处理失败卡密未使用";

                case "opstate=13":
                    return "系统繁忙";

                case "opstate=14":
                    return "不存在该笔订单";

                case "opstate=15":
                    return "未知请求";

                case "opstate=16":
                    return "密码错误";

                case "opstate=17":
                    return "匹配订单失败";

                case "opstate=18":
                    return "余额不足";

                case "opstate=19":
                    return "运营商维护";

                case "opstate=20":
                    return "提交次数过多";

                case "opstate=99":
                    return "其他错误";
            }
            return retcode;
        }

        public string Query(string orderid)
        {
            string queryCardUrl = base.SuppInfo.queryCardUrl;
            if (string.IsNullOrEmpty(queryCardUrl))
            {
                return string.Empty;
            }
            orderid = orderid.Trim();
            string str4 = Cryptography.MD5(string.Format("orderid={0}&parter={1}{2}", orderid, base.SuppAccount, base.SuppKey));
            queryCardUrl = string.Format("{0}?orderid={1}&parter={2}&sign={3}", new object[] { queryCardUrl, orderid, base.SuppAccount, str4 });
            try
            {
                return WebClientHelper.GetString(queryCardUrl, null, "GET", Encoding.Default);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return exception.Message;
            }
        }

        internal string notify_url
        {
            get
            {
                return (base.SiteDomain + "/notify/longbao/Card_Notify.aspx");
            }
        }
    }
}

