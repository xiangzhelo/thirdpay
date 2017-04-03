namespace viviapi.ETAPI.Cared70
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Xml;
    using System.Text;
    using System.Web;
    using System.Web.Security;
    using viviapi.ETAPI.Common;
    using viviapi.Model.Order.Card;
    using viviapi.Model.Payment;
    using viviapi.BLL.Payment;
    using viviapi.Model.Order;
    using viviapi.Model.supplier;
    using viviapi.SysConfig;
    using viviapi.Model;
    using viviLib.ExceptionHandling;
    using viviLib.Security;
    using viviLib.Web;
    using viviLib.Logging;
    using viviapi.BLL;

    public class Card : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.OfCard;

        public static Card Default
        {
            get
            {
                var ofpay = new Card();
                return ofpay;
            }
        }

        public string NotifyUrl
        {
            get
            {
                return SiteDomain + "/receive/cared70/card.aspx"; ;
            }
        }

        internal string Succflag = "OK";

        private string version;

        public Card()
            : base(SuppId)
        {
            this.version = "v2.5";
        }

        public static string[] BubbleSort(string[] r)
        {
            for (int i = 0; i < r.Length; i++)
            {
                bool flag = false;
                for (int j = r.Length - 2; j >= i; j--)
                {
                    if (string.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        string str = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = str;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return r;
                }
            }
            return r;
        }

        public void CardNotify()
        {
            if (this.version == "v1.0")
            {
                this.CardNotifyV10();
            }
            else if (this.version == "v2.5")
            {
                this.CardNotifyV25();
            }
        }

        public void CardNotifyV10()
        {
            string str = HttpContext.Current.Request.QueryString["returncode"].ToString().Trim().ToLower();
            string str2 = HttpContext.Current.Request.QueryString["userid"].ToString().Trim().ToLower();
            string orderId = HttpContext.Current.Request.QueryString["orderid"].ToString().Trim().ToLower();
            string str4 = HttpContext.Current.Request.QueryString["typeid"].ToString().Trim().ToLower();
            string str5 = HttpContext.Current.Request.QueryString["productid"].ToString().Trim().ToLower();
            string supplierOrderId = HttpContext.Current.Request.QueryString["cardno"].ToString().Trim().ToLower();
            string str7 = HttpContext.Current.Request.QueryString["cardpwd"].ToString().Trim().ToLower();
            string str8 = HttpContext.Current.Request.QueryString["money"].ToString().Trim().ToLower();
            string s = HttpContext.Current.Request.QueryString["realmoney"].ToString().Trim().ToLower();
            string cardstatus = HttpContext.Current.Request.QueryString["cardstatus"].ToString().Trim().ToLower();
            string sign = HttpContext.Current.Request.QueryString["sign"].ToString().Trim().ToLower();
            string str12 = HttpContext.Current.Request.QueryString["ext"].ToString().Trim().ToLower();
            string errtype = HttpContext.Current.Request.QueryString["errtype"].ToString().Trim();
            string SuppKey = base.SuppKey;
            try
            {
                string data = string.Format("returncode={0}&userid={1}&orderid={2}&typeid={3}&productid={4}&cardno={5}&cardpwd={6}&money={7}&realmoney={8}&cardstatus={9}&keyvalue={10}", new object[] { str, str2, orderId, str4, str5, supplierOrderId, str7, str8, s, cardstatus, SuppKey });
                string md5 = MD5(data);
                if (md5.ToLower() == sign.ToLower())
                {

                    //status = (result == "2000" || result == "2011") ? 2 : 4;
                    //if (status == 2)
                    //    opstate = "0";
                    //else
                    //    opstate = ConvertCode(result);

                    //string viewMsg = info;

                    //var response = new CardOrderSupplierResponse()
                    //{
                    //    SupplierId = SuppId,
                    //    SuppTransNo = billid,
                    //    SysOrderNo = orderno,
                    //    OrderAmt = decimal.Parse(value),
                    //    SuppAmt = 0M,
                    //    OrderStatus = status,
                    //    SuppErrorCode = result,
                    //    Opstate = opstate,
                    //    SuppErrorMsg = info,
                    //    ViewMsg = viewMsg,
                    //    Method = 1
                    //};

                    //OrderCardUtils.SuppNotify(response, Succflag);

                    //string suppcode = "-1";
                    //if (str == "1")
                    //{
                    //    suppcode = "0";
                    //}
                    //else
                    //{
                    //    suppcode = this.ConvertCode(suppcode);
                    //}
                    //int status = (str == "1") ? 2 : 4;
                    //new OrderCard().ReceiveSuppResult(suppId, orderId, supplierOrderId, status, suppcode, this.GetMsgInfo(cardstatus), decimal.Parse(s), 0M, errtype);
                }
                else
                {
                    LogHelper.Write(string.Format("70card 原字串{0} 本地sign{1},接口商sign:{2}", data, md5, sign));
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            HttpContext.Current.Response.Write("ok");
        }

        public void CardNotifyV25()
        {
            HttpContext.Current.Request.ContentEncoding = Encoding.UTF8;
            string returncode = HttpContext.Current.Request.QueryString["returncode"].ToString().Trim();
            string supplierOrderId = HttpContext.Current.Request.QueryString["yzchorderno"].ToString().Trim();
            string userid = HttpContext.Current.Request.QueryString["userid"].ToString().Trim();
            string orderId = HttpContext.Current.Request.QueryString["orderno"].ToString().Trim();
            string money = HttpContext.Current.Request.QueryString["money"].ToString().Trim();
            string realmoney = HttpContext.Current.Request.QueryString["realmoney"].ToString().Trim();
            string sign = HttpContext.Current.Request.QueryString["sign"].ToString().Trim();
            string ext = HttpContext.Current.Request.QueryString["ext"].ToString().Trim();
            string errtype = HttpContext.Current.Request.QueryString["errtpe"].ToString().Trim();
            string msg = HttpContext.Current.Request.QueryString["message"].ToString();
            string SuppKey = base.SuppKey;
            try
            {
                string data = string.Format("returncode={0}&yzchorderno={1}&userid={2}&orderno={3}&money={4}&realmoney={5}&keyvalue={6}", new object[] { returncode, supplierOrderId, userid, orderId, money, realmoney, SuppKey });
                string str12 = MD5(data);
                if (str12 == sign)
                {
                    string opstate = "-1";
                    //returncode:“1”代表售卡成功，
                    //“0”代表售卡失败

                    if (returncode == "1")
                    {
                        opstate = "0";
                    }
                    int status = (returncode == "1") ? 2 : 4;
                    OrderCard card = new OrderCard();
                    string info = "";
                    if (!string.IsNullOrEmpty(msg))
                    {
                        info = msg.Replace("message=", "").Split(new char[] { ',' })[0];
                    }
                    if (msg == "成功")
                    {
                        info = "支付成功";
                    }
                    status = (returncode == "1" || returncode == "2") ? 2 : 4;
                    if (status == 2)
                        opstate = "0";
                    else
                        opstate = ConvertCode(returncode);

                    string viewMsg = msg;

                    var response = new CardOrderSupplierResponse()
                    {
                        SupplierId = SuppId,
                        SuppTransNo = supplierOrderId,
                        SysOrderNo = orderId,
                        OrderAmt = decimal.Parse(money),
                        SuppAmt = 0M,
                        OrderStatus = status,
                        SuppErrorCode = errtype,
                        Opstate = opstate,
                        SuppErrorMsg = msg,
                        ViewMsg = viewMsg,
                        Method = 1
                    };

                    OrderCardUtils.SuppNotify(response, Succflag);
                }
                else
                {
                    LogHelper.Write(string.Format("70card 原字串{0} 本地sign{1},接口商sign:{2}", data, str12, sign));
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            HttpContext.Current.Response.Write("ok");
            HttpContext.Current.Response.End();

        }

        public CardSynchCallBack CardSend(CardOrderSummitArgs o)
        {

            string supporderid = string.Empty;
            string errmsg = string.Empty;

            string _orderid = o.SysOrderNo;
            string _cardno = o.CardNo;
            string _cardpwd = o.CardPass;
            int _typeId = o.CardTypeId;
            int cardvalue = o.FaceValue;

            //if (this.version == "v1.0")
            //{
            //    return this.CardSendV10(_orderid, _cardno, _cardpwd, _typeId, cardvalue, out supporderid, out errmsg);
            //}
            //if (this.version == "v2.5")
            //{
            return this.CardSendV25(_orderid, _cardno, _cardpwd, _typeId, cardvalue, out supporderid, out errmsg);
            //}
            //return "-1";
        }
        //public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string errmsg)
        //{
        //    supporderid = string.Empty;
        //    errmsg = string.Empty;
        //    if (this.version == "v1.0")
        //    {
        //        return this.CardSendV10(_orderid, _cardno, _cardpwd, _typeId, cardvalue, out supporderid, out errmsg);
        //    }
        //    if (this.version == "v2.5")
        //    {
        //        return this.CardSendV25(_orderid, _cardno, _cardpwd, _typeId, cardvalue, out supporderid, out errmsg);
        //    }
        //    return "-1";
        //}

        public string CardSendV10(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string errmsg)
        {
            errmsg = string.Empty;
            supporderid = string.Empty;
            string SuppAccount = base.SuppAccount;
            string SuppKey = base.SuppKey;
            string str3 = base.PostCardUrl + "?";
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            string str4 = _cardno;
            string str5 = _cardpwd;
            string str6 = _orderid;
            string paycardno = this.GetPaycardno(_typeId);
            string cardType = this.GetCardType(_typeId, cardvalue);
            string str9 = cardvalue.ToString();
            string str10 = MD5(string.Format("userid={0}&orderid={1}&typeid={2}&productid={3}&cardno={4}&cardpwd={5}&money={6}&url={7}&keyvalue={8}", new object[] { SuppAccount, str6, paycardno, cardType, str4, str5, str9, this.notify_url, SuppKey }).ToLower());
            string str11 = string.Format("userid={0}&orderid={1}&typeid={2}&productid={3}&cardno={4}&cardpwd={5}&money={6}&url={7}&sign={8}&ext={9}", new object[] { SuppAccount, str6, paycardno, cardType, str4, str5, str9, this.notify_url, str10, string.Empty });
            string str12 = string.Empty;
            string str13 = string.Empty;
            string str14 = string.Empty;
            try
            {
                string str15 = "-1";
                string[] strings = GetStrings(SendRequest(str3.ToLower(), str11.ToLower()), "&");
                str12 = strings[0].Replace("returncode=", string.Empty);
                str13 = strings[1].Replace("returnorderid=", string.Empty);
                str14 = strings[2].Replace("sign=", string.Empty);
                if (MD5(string.Format("returncode={0}&returnorderid={1}&keyvalue={2}", str12, str13, SuppKey).ToLower()) == str14)
                {
                    errmsg = this.GetMsgInfo(str12);
                    supporderid = str13;
                    if (!(string.IsNullOrEmpty(str12) || !(str12 == "1")))
                    {
                        str15 = "0";
                    }
                }
                else
                {
                    errmsg = "签名失败";
                }
                return str15;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return string.Empty;
            }
        }

        public CardSynchCallBack CardSendV25(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string errmsg)
        {
            var callBack = new CardSynchCallBack();

            errmsg = string.Empty;
            supporderid = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("userid={0}", base.SuppAccount);
            builder.AppendFormat("&orderno={0}", _orderid);
            builder.AppendFormat("&typeid={0}", this.GetPaycardno(_typeId));
            builder.AppendFormat("&cardno={0}", _cardno);
            builder.AppendFormat("&encpwd={0}", 0);
            builder.AppendFormat("&cardpwd={0}", _cardpwd);
            builder.AppendFormat("&cardpwdenc={0}", string.Empty);
            builder.AppendFormat("&money={0}", cardvalue);
            builder.AppendFormat("&url={0}", this.notify_url);
            string str2 = Cryptography.MD5(builder.ToString() + string.Format("&keyvalue={0}", base.SuppKey)).ToLower();
            builder.AppendFormat("&sign={0}", str2);
            builder.AppendFormat("&ext={0}", string.Empty);
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            string responseText = WebClientHelper.GetString(base.PostCardUrl + "?" + builder.ToString(), null, "GET", Encoding.GetEncoding("utf-8"), 0x2710);
            string[] strArray = responseText.Split(new char[] { '&' });
            //if (strArray.Length >= 2)
            //{
            //    if (strArray[0] == "returncode=1")
            //    {
            //        return "0";
            //    }
            //    errmsg = strArray[1].Replace("message=", "");
            //    string[] strArray2 = errmsg.Split(new char[] { ',' });
            //    errmsg = strArray2[0];
            //}
            //return "-1";
            SynsSummitLogger(strArray.ToString());
            if (strArray.Length >= 2)
            {
                if (strArray[0] == "returncode=1")
                {
                    callBack.Success = 1;
                    callBack.SuppTransNo = "";
                    callBack.SuppCallBackText = responseText;
                    callBack.SuppErrorCode = "0";
                    callBack.SuppErrorMsg = strArray[1];
                    callBack.SummitStatus = 1;
                }
            }
            return callBack;
        }

        public string ConvertCode(string suppcode)
        {
            string str = string.Empty;
            if (suppcode == "1001")
            {
                str = "-1";
            }
            else if (suppcode == "1002")
            {
                str = "-2";
            }
            else if (suppcode == "1003")
            {
                str = "-10";
            }
            else if (suppcode == "1004")
            {
                str = "-4";
            }
            else if (suppcode == "1005")
            {
                str = "-5";
            }
            else if (suppcode == "1006")
            {
                str = "-6";
            }
            else if (suppcode == "1007")
            {
                str = "-7";
            }
            else if (suppcode == "1008")
            {
                str = "-8";
            }
            else if (suppcode == "1009")
            {
                str = "-9";
            }
            if (string.IsNullOrEmpty(suppcode))
            {
                str = "-1";
            }
            return str;
        }

        public bool Finish(string orderid, string callback)
        {
            bool flag = false;
            try
            {
                if (string.IsNullOrEmpty(callback))
                {
                    return flag;
                }
                string[] strArray = callback.Split(new char[] { '&' });
                if (strArray.Length != 3)
                {
                    return flag;
                }
                string str = "-1";
                int status = 4;
                //returncode={}&realmoney={}&usermoney={}&message={}&errtype={}
                //“-1”代表售卡无记录
                //“0”代表售卡失败，
                //“1”代表售卡成功，
                //“2”代表售卡处理中
                string returncode = strArray[0].Replace("returncode=", "");
                string realmoney = strArray[1].Replace("realmoney=", "");
                string usermoney = strArray[2].Replace("usermoney=", "");
                string msg = strArray[3].Replace("message=", "");
                string errtype2 = strArray[4].Replace("errtype=", "");

                if (returncode == "1")
                {
                    str = "0";
                    status = 2;
                }
                else if ((returncode == "-1") || (returncode == "2"))
                {
                    str = string.Empty;
                }
                if (!string.IsNullOrEmpty(str))
                {
                    var response = new CardOrderSupplierResponse()
                    {
                        SupplierId = SuppId,
                        SuppTransNo = "",
                        SysOrderNo = orderid,
                        OrderAmt = decimal.Parse(realmoney),
                        SuppAmt = 0M,
                        OrderStatus = status,
                        SuppErrorCode = errtype2,
                        Opstate = str,
                        SuppErrorMsg = msg,
                        ViewMsg = GetMsgInfo(errtype2),
                        Method = 1
                    };

                    OrderCardUtils.Finish(response);
                    //new OrderCard().ReceiveSuppResult(suppId, orderid, string.Empty, status, str, msg, decimal.Parse(s), 0M, errtype);
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            return flag;
        }

        public static string Get_Http(string a_strUrl, int timeout)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(a_strUrl);
                request.Timeout = timeout;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                StringBuilder builder = new StringBuilder();
                while (-1 != reader.Peek())
                {
                    builder.Append(reader.ReadLine());
                }
                return builder.ToString();
            }
            catch (Exception exception)
            {
                return ("错误：" + exception.Message);
            }
        }

        public string GetCardType(int paytype, int money)
        {
            int num = paytype;
            switch (num)
            {
                case 0x67:
                    num = money;
                    if (num > 30)
                    {
                        switch (num)
                        {
                            case 50:
                                return "cm50";

                            case 100:
                                return "cm100";

                            case 300:
                                return "cm300";

                            case 500:
                                return "cm500";
                        }
                        break;
                    }
                    switch (num)
                    {
                        case 10:
                            return "cm10";

                        case 20:
                            return "cm20";

                        case 30:
                            return "cm30";
                    }
                    break;

                case 0x68:
                    num = money;
                    if (num > 0x23)
                    {
                        switch (num)
                        {
                            case 0x2d:
                                return "sd45";

                            case 50:
                                return "sd50";

                            case 100:
                                return "sd100";

                            case 350:
                                return "sd350";

                            case 0x3e8:
                                return "sd1000";
                        }
                    }
                    else if (num > 10)
                    {
                        switch (num)
                        {
                            case 0x19:
                                return "sd25";

                            case 30:
                                return "sd30";

                            case 0x23:
                                return "sd35";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 5:
                                return "sd5";

                            case 10:
                                return "sd10";
                        }
                    }
                    return ("sd" + money.ToString());

                case 0x69:
                    num = money;
                    if (num > 50)
                    {
                        switch (num)
                        {
                            case 60:
                                return "zt60";

                            case 100:
                                return "zt100";

                            case 300:
                                return "zt300";

                            case 500:
                                return "zt500";

                            case 0x3e8:
                                return "zt1000";
                        }
                    }
                    else if (num > 20)
                    {
                        switch (num)
                        {
                            case 30:
                                return "zt30";

                            case 50:
                                return "zt50";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 10:
                                return "zt10";

                            case 20:
                                return "zt20";
                        }
                    }
                    return ("zt" + money.ToString());

                case 0x6a:
                    num = money;
                    if (num > 30)
                    {
                        switch (num)
                        {
                            case 50:
                                return "jw50";

                            case 100:
                                return "jw100";

                            case 200:
                                return "jw200";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 4:
                                return "jw4";

                            case 5:
                                return "jw5";

                            case 6:
                                return "jw6";

                            case 10:
                                return "jw10";

                            case 15:
                                return "jw15";

                            case 30:
                                return "jw30";
                        }
                    }
                    return ("jw" + money.ToString());

                case 0x6b:
                    num = money;
                    if (num > 15)
                    {
                        switch (num)
                        {
                            case 30:
                                return "qq30";

                            case 60:
                                return "qq60";

                            case 100:
                                return "qq100";

                            case 200:
                                return "qq200";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 5:
                                return "qq5";

                            case 10:
                                return "qq10";

                            case 15:
                                return "qq15";
                        }
                    }
                    return ("qq" + money.ToString());

                case 0x6c:
                    num = money;
                    if (num > 50)
                    {
                        switch (num)
                        {
                            case 100:
                                return "cc100";

                            case 300:
                                return "cc300";

                            case 500:
                                return "cc500";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 20:
                                return "cc20";

                            case 30:
                                return "cc30";

                            case 50:
                                return "cc50";
                        }
                    }
                    return ("cc" + money.ToString());

                case 0x6d:
                    num = money;
                    if (num > 10)
                    {
                        switch (num)
                        {
                            case 30:
                                return "jy30";

                            case 50:
                                return "jy50";

                            case 100:
                                return "jy100";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 5:
                                return "jy5";

                            case 10:
                                return "jy10";
                        }
                    }
                    return ("jy" + money.ToString());

                case 110:
                    switch (money)
                    {
                        case 10:
                            return "wy10";

                        case 15:
                            return "wy15";

                        case 30:
                            return "wy30";
                    }
                    return ("wy" + money.ToString());

                case 0x6f:
                    num = money;
                    if (num > 30)
                    {
                        switch (num)
                        {
                            case 50:
                                return "wm50";

                            case 100:
                                return "wm100";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 15:
                                return "wm15";

                            case 30:
                                return "wm30";
                        }
                    }
                    return ("wm" + money.ToString());

                case 0x70:
                    num = money;
                    if (num > 15)
                    {
                        switch (num)
                        {
                            case 30:
                                return "sh30";

                            case 40:
                                return "sh40";

                            case 100:
                                return "sh100";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 5:
                                return "sh5";

                            case 10:
                                return "sh10";

                            case 15:
                                return "sh15";
                        }
                    }
                    return ("sh" + money.ToString());

                case 0x71:
                    switch (money)
                    {
                        case 50:
                            return "dx50";

                        case 100:
                            return "dx100";
                    }
                    return ("dx" + money.ToString());

                case 0x73:
                    num = money;
                    if (num > 20)
                    {
                        switch (num)
                        {
                            case 30:
                                return "gy30";

                            case 50:
                                return "gy50";

                            case 100:
                                return "gy100";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 10:
                                return "gy10";

                            case 20:
                                return "gy15";
                        }
                    }
                    return ("gy" + money.ToString());

                case 0x75:
                    num = money;
                    if (num > 30)
                    {
                        switch (num)
                        {
                            case 50:
                                return "zy50";

                            case 100:
                                return "zy100";

                            case 500:
                                return "zy500";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 10:
                                return "zy10";

                            case 15:
                                return "zy15";

                            case 30:
                                return "zy30";
                        }
                    }
                    return ("zy" + money.ToString());

                case 0x76:
                    num = money;
                    if (num > 50)
                    {
                        switch (num)
                        {
                            case 60:
                                return "tx60";

                            case 70:
                                return "tx70";

                            case 80:
                                return "tx80";

                            case 90:
                                return "tx90";

                            case 100:
                                return "tx100";
                        }
                    }
                    else if (num > 20)
                    {
                        switch (num)
                        {
                            case 30:
                                return "tx30";

                            case 40:
                                return "tx40";

                            case 50:
                                return "tx50";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 10:
                                return "tx10";

                            case 20:
                                return "tx15";
                        }
                    }
                    return ("tx" + money.ToString());

                case 0x77:
                    num = money;
                    if (num > 15)
                    {
                        switch (num)
                        {
                            case 30:
                                return "th50";

                            case 50:
                                return "th100";

                            case 100:
                                return "th100";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 5:
                                return "th10";

                            case 10:
                                return "th15";

                            case 15:
                                return "th30";
                        }
                    }
                    return ("th" + money.ToString());

                case 200:
                case 0xc9:
                case 0xca:
                case 0xcb:
                    num = money;
                    if (num > 30)
                    {
                        switch (num)
                        {
                            case 50:
                                return "cd50";

                            case 100:
                                return "cd100";

                            case 300:
                                return "cd300";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 10:
                                return "cd10";

                            case 20:
                                return "cd20";

                            case 30:
                                return "cd30";
                        }
                    }
                    return ("cd" + money.ToString());

                case 0xd1:
                    num = money;
                    if (num > 50)
                    {
                        switch (num)
                        {
                            case 60:
                                return "txzx60";

                            case 70:
                                return "txzx70";

                            case 80:
                                return "txzx80";

                            case 90:
                                return "txzx90";

                            case 100:
                                return "txzx100";
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case 30:
                                return "txzx30";

                            case 40:
                                return "txzx40";

                            case 50:
                                return "txzx50";

                            case 10:
                                return "txzx10";

                            case 20:
                                return "txzx15";
                        }
                    }
                    return ("txzx" + money.ToString());

                default:
                    return "0";
            }
            return ("cm" + money.ToString());
        }

        public static string GetMD5(string s, string _input_charset)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }

        public string GetMsgInfo(string cardstatus)
        {
            switch (cardstatus)
            {
                case "1001":
                    return "卡号或密码错误";

                case "1002":
                    return "卡号过期";

                case "1003":
                    return "卡余额不足";

                case "1004":
                    return "卡号不存在";

                case "1005":
                    return "卡已使用过";

                case "1006":
                    return "卡号被冻结";

                case "1007":
                    return "卡未激活";

                case "1008":
                    return "不支持的卡类型或金额";

                case "1009":
                    return "其他游戏专用卡";
            }
            return cardstatus;
        }

        private string GetParamValue(string PName)
        {
            return HttpContext.Current.Request.Form[PName].ToString().Trim().ToLower();
        }

        public string GetPaycardno(int _type)
        {
            string str = string.Empty;
            switch (_type)
            {
                case 0x67:
                    return "cm";

                case 0x68:
                    return "sd";

                case 0x69:
                    return "zt";

                case 0x6a:
                    return "jw";

                case 0x6b:
                    return "qq";

                case 0x6c:
                    return "cc";

                case 0x6d:
                    return "jy";

                case 110:
                    return "wy";

                case 0x6f:
                    return "wm";

                case 0x70:
                    return "sh";

                case 0x71:
                    return "dx";

                case 0x72:
                case 0x74:
                    return str;

                case 0x73:
                    return "gy";

                case 0x75:
                    return "zy";

                case 0x76:
                    return "tx";

                case 0x77:
                    return "th";

                case 200:
                case 0xc9:
                case 0xca:
                case 0xcb:
                    return "cd";

                case 0xd1:
                    return "txzx";
            }
            return str;
        }

        public static string[] GetStrings(string str, string sign)
        {
            return str.Split(new string[] { sign }, StringSplitOptions.None);
        }

        public static string MD5(string data)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(data, "md5").ToLower();
        }

        public string Query(string orderid)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("userid={0}", base.SuppAccount);
                builder.AppendFormat("&orderno={0}", orderid);
                string str3 = Cryptography.MD5(builder.ToString() + string.Format("&keyvalue={0}", base.SuppKey)).ToLower();
                builder.AppendFormat("&sign={0}", str3);
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
                return WebClientHelper.GetString("http://tong.yzch.net/query.ashx?" + builder.ToString(), null, "GET", Encoding.GetEncoding("utf-8"), 0x2710);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public static string SendRequest(string url, string paramesmes)
        {
            return SendRequest(url, paramesmes, "GET");
        }

        public static string SendRequest(string url, string parames, string method)
        {
            string str = "";
            if ((url == null) || (url == ""))
            {
                return null;
            }
            if ((method == null) || (method == ""))
            {
                method = "GET";
            }
            if (method.ToUpper() == "GET")
            {
                try
                {
                    WebRequest request = WebRequest.Create(url + parames);
                    request.Method = "GET";
                    str = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("gb2312")).ReadToEnd();
                }
                catch (Exception exception)
                {
                    return exception.Message;
                }
            }
            return str;
        }
        /// <summary>
        /// 售卡成功后，70card向该网址发送两次成功通知，该地址可以带参数，如:“www.yzch.net/callback.aspx?para=para”
        /// </summary>
        internal string notify_url
        {
            get
            {
                return (base.SiteDomain + "/receive/cared70/card.aspx");
            }
        }
    }
}

