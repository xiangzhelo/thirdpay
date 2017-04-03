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


namespace viviapi.ETAPI.Card51
{
    public class SynchResponse
    {
        public SynchResponse()
        {
            State = "";
            Errcode = "";
            Errmsg = "";
            Mark = "";
        }

        public string State { get; set; }
        public string Errcode { get; set; }
        public string Errmsg { get; set; }
        public string Mark { get; set; }
    }

    public class QueryResponse
    {

        public QueryResponse(string sysOrderNo)
        {
            SysOrderNo = sysOrderNo;
            State = "";
            Errcode = "";
            Errmsg = "";
            Mark = "";
        }
        //cardno ordermoney mark
        public string State { get; set; }
        public string SysOrderNo { get; set; }
        public string Sd51No { get; set; }
        public string Ordermoney { get; set; }
        public string CardNo { get; set; }
        public string Errcode { get; set; }
        public string Errmsg { get; set; }
        public string Mark { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Card : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Card51;

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

        internal string NotifyUrl { get { return this.SiteDomain + "/receive/card51/card.aspx"; } }
        internal string Succflag = "<result>1</result>";

        #region CardSend
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public CardSynchCallBack CardSend(CardOrderSummitArgs o)
        {
            var callBack = new CardSynchCallBack();

            string commitUrl = this.PostCardUrl + "?";
            if (string.IsNullOrEmpty(PostCardUrl))
            {
                commitUrl = "http://it.51esales.net/gateway/zfgateway.asp?";
            }
            string cardno = GetCardNo(o.CardTypeId);
            string faceno = cardno + o.FaceValue.ToString("000");
            if (o.FaceValue >= 1000)
                faceno = cardno + "000";

            string remarks = string.Empty;
            string mark = string.Empty;

            string md5Str = "customerid=" + SuppAccount + "&sdcustomno=" + o.SysOrderNo + "&noticeurl=" + NotifyUrl + "&mark=" + mark + "&key=" + SuppKey;
            string sign = Cryptography.MD5(md5Str, "GB2312").ToUpper();

            try
            {
                var postData = new StringBuilder();
                postData.AppendFormat("&customerid={0}", SuppAccount);
                postData.AppendFormat("&sdcustomno={0}", o.SysOrderNo);
                postData.AppendFormat("&ordermoney={0}", o.FaceValue);
                postData.AppendFormat("&cardno={0}", cardno);
                postData.AppendFormat("&faceno={0}", faceno);
                postData.AppendFormat("&cardnum={0}", o.CardNo);
                postData.AppendFormat("&cardpass={0}", o.CardPass);
                postData.AppendFormat("&noticeurl={0}", NotifyUrl);
                postData.AppendFormat("&remarks={0}", remarks);
                postData.AppendFormat("&mark={0}", mark);
                postData.AppendFormat("&remoteip={0}", ServerVariables.TrueIP);
                postData.AppendFormat("&sign={0}", sign);

                callBack.Success = 0;

                SynsSummitLogger(commitUrl);

                SynsSummitLogger("postData:" + postData.ToString());

                string retXml = WebClientHelper.GetString(commitUrl, postData.ToString(), "GET", Encoding.GetEncoding("GB2312"), 10000);

                SynsSummitLogger(retXml);

                var response = new SynchResponse();
                if (!string.IsNullOrEmpty(retXml))
                {
                    response = GetSynchResponse(retXml);
                }

                if (!string.IsNullOrEmpty(response.State))
                {
                    callBack.Success = 1;
                    callBack.SuppTransNo = "";
                    callBack.SuppCallBackText = retXml;
                    callBack.SuppErrorCode = response.Errcode;
                    callBack.SuppErrorMsg = response.Errmsg;

                    if (response.State == "1")
                    {
                        callBack.SummitStatus = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                callBack.Success = 0;
                ExceptionHandler.HandleException(ex);
            }
            return callBack;
        }
        #endregion

        #region GetMsgInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardstatus"></param>
        /// <returns></returns>
        public string GetMsgInfo(string cardstatus)
        {
            switch (cardstatus)
            {
                case "ERROR001":
                    return "商户编号不能为空";
                case "ERROR002":
                    return "无效的用户名或用户名没有被启用";
                case "ERROR003":
                    return "商户验证KEY不能为空";
                case "ERROR004":
                    return "MD5验证失败";
                case "ERROR005":
                    return "商户订单号不能为空";
                case "ERROR006":
                    return "充值卡类型不能为空";
                case "ERROR007":
                    return "充值卡号不能为空";
                case "1009":
                    return "其他游戏专用卡";
            }
            return cardstatus;
        }
        #endregion

        #region CardNotify
        /// <summary>
        /// 
        /// </summary>
        public void CardNotify()
        {
            HttpRequest req = HttpContext.Current.Request;
            
            AsynsRetLogger(req.RawUrl);

            if (req.Form.Count > 0)
            {
                string data = WebClientHelper.FormatRequestData(req.Form, System.Text.Encoding.Default);
                AsynsRetLogger(data);
            }

            String state = req["state"].Trim(); //商户在51支付网关上的订单状态，0为失败，1为成功
            String customerid = req["customerid"].Trim(); //商户在51支付网关上的ID
            String sd51No = req["sd51no"].Trim(); //51支付订单流水号
            String sdcustomno = req["sdcustomno"].Trim();     //商户提交的订单流水号
            String ordermoney = req["ordermoney"].Trim();   //商户订单金额，注意可能与商户时提交不一致，51支付结果是以卡的面值来结算的
            String mark = req["mark"].Trim();  //商户自定义原样返回
            String sign = req["sign"].Trim();  //51支付提交过来的MD5签名            
            String des = req["des"].Trim(); //
            String signText = "customerid=" + customerid + "&sd51no=" + sd51No + "&sdcustomno=" + sdcustomno + "&mark=" + mark + "&key=" + SuppKey;//'生成MD5签名
            String localSign = Cryptography.MD5(signText);

            try
            {
                if (localSign == sign)
                {
                    int status = state == "1" ? 2: 4;
                    string opstate = "0";
                    string viewMsg = "支付成功";
                    if (status == 4)
                    {
                        opstate = "99";
                        viewMsg = "其它错误";
                    }

                    var response = new CardOrderSupplierResponse()
                    {
                        SupplierId = SuppId,
                        SuppTransNo = sd51No,
                        SysOrderNo = sdcustomno,
                        OrderAmt = decimal.Parse(ordermoney),
                        SuppAmt = 0M,
                        OrderStatus = status,
                        SuppErrorCode = state,
                        SuppErrorMsg = des,
                        Opstate = opstate,
                        ViewMsg = viewMsg,
                        Method = 1
                    };

                    OrderCardUtils.SuppNotify(response, Succflag);
                }
                else
                {
                    HttpContext.Current.Response.Write(signText);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
        #endregion

        #region Query
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderamt"></param>
        /// <returns></returns>
        public QueryResponse Query(string orderid, int orderamt)
        {
            string commitUrl = "http://it.51esales.net/gateway/orderquery.asp";

            string customerid = SuppAccount;
            string sdcustomno = orderid;
            string ordermoney = decimal.ToInt32(orderamt).ToString(CultureInfo.InvariantCulture);

            string plain = string.Format("customerid={0}&sdcustomno={1}&mark={2}&key={3}", customerid, sdcustomno, "",
                SuppKey);

            string sign = Cryptography.MD5(plain, "GB2312").ToUpper();

            var postData = new StringBuilder();
            postData.AppendFormat("&customerid={0}", SuppAccount);
            postData.AppendFormat("&sdcustomno={0}", sdcustomno);
            postData.AppendFormat("&ordermoney={0}", ordermoney);
            postData.AppendFormat("&sign={0}", sign);

            var response = new QueryResponse(orderid);

            string retXml = WebClientHelper.GetString(commitUrl, postData.ToString(), "GET", Encoding.GetEncoding("GB2312"), 10000);

            if (!string.IsNullOrEmpty(retXml))
            {
                response = GetQueryResponse(orderid,retXml);
            }
            return response;
        }
        #endregion

        #region GetCardNo
        /// <summary>
        /// wmk 魔兽卡
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public string GetCardNo(int typeid)
        {
            string cardType = typeid.ToString(CultureInfo.InvariantCulture);
            if (typeid == 103)
            {
                cardType = "szx";
            }
            else if (typeid == 104)
            {
                cardType = "sdk";//盛大一卡通
            }
            else if (typeid == 105)
            {
                cardType = "ztk";//征途卡
            }
            else if (typeid == 106) //
            {
                cardType = "jwk";//骏网一卡通
            }
            else if (typeid == 107)
            {
                cardType = "qqb";
            }
            else if (typeid == 108)
            {
                cardType = "ltk";//联通充值卡             
            }
            else if (typeid == 109)
            {
                cardType = "jyk";//久游一卡通                
            }
            else if (typeid == 110)//
            {
                cardType = "wyk";//网易一卡通
            }
            else if (typeid == 111)
            {
                cardType = "wmk";//完美一卡通                       
            }
            else if (typeid == 112)
            {
                cardType = "shk";//搜狐一卡通                
            }
            else if (typeid == 113)
            {
                cardType = "dxk";//电信充值卡              
            }
            else if (typeid == 114)
            {
                cardType = "sxk";//声讯卡            
            }
            else if (typeid == 117)
            {
                cardType = "zyk";//纵游一卡通            
            }
            else if (typeid == 204)
            {
                cardType = "msk";//魔兽卡              
            }
            return cardType;
        }
        #endregion

        #region GetCardType
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paytype"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public string GetCardType(int paytype, int money)
        {
            string _CardType = paytype.ToString();
            if (paytype == 103)
            {
                #region 神州行
                switch (money)
                {
                    case 10:
                        _CardType = "szx010";//神州行10元卡
                        break;
                    case 20:
                        _CardType = "szx020";//神州行20元卡
                        break;
                    case 30:
                        _CardType = "szx030";//神州行30元卡
                        break;
                    case 50:
                        _CardType = "szx050";//神州行50元卡
                        break;
                    case 100:
                        _CardType = "szx100";//神州行100元卡
                        break;
                    case 300:
                        _CardType = "szx300";//神州行300元卡
                        break;
                    case 500:
                        _CardType = "szx500";//神州行500元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 104)
            {
                #region 盛大一卡通
                switch (money)
                {
                    case 5:
                        _CardType = "sdk005";//盛大一卡通5元卡
                        break;
                    case 10:
                        _CardType = "sdk010";//盛大一卡通10元卡
                        break;
                    case 15:
                        _CardType = "sdk015";//盛大一卡通15元卡
                        break;
                    case 25:
                        _CardType = "sdk025";//盛大一卡通25元卡
                        break;
                    case 30:
                        _CardType = "sdk030";//盛大一卡通30元卡
                        break;
                    case 35:
                        _CardType = "sdk035";//盛大一卡通35元卡
                        break;
                    case 45:
                        _CardType = "sdk045";//盛大一卡通45元卡
                        break;
                    case 50:
                        _CardType = "sdk050";//盛大一卡通50元卡
                        break;
                    case 100:
                        _CardType = "sdk100";//盛大一卡通100元卡
                        break;
                    case 300:
                        _CardType = "sdk300";//盛大一卡通300元卡
                        break;
                    case 350:
                        _CardType = "sdk350";//盛大一卡通350元卡
                        break;
                    case 1000:
                        _CardType = "sdk000";//盛大一卡通1000元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 105)
            {
                #region 征途支付卡
                switch (money)
                {
                    case 10:
                        _CardType = "ztk010";//征途支付卡10元卡
                        break;
                    case 15:
                        _CardType = "ztk015";//征途支付卡15元卡
                        break;
                    case 20:
                        _CardType = "ztk020";//征途支付卡20元卡
                        break;
                    case 25:
                        _CardType = "ztk025";//征途支付卡25元卡
                        break;
                    case 30:
                        _CardType = "ztk030";//征途支付卡30元卡
                        break;
                    case 50:
                        _CardType = "ztk050";//征途支付卡50元卡
                        break;
                    case 60:
                        _CardType = "ztk060";//征途支付卡60元卡
                        break;
                    case 100:
                        _CardType = "ztk100";//征途支付卡100元卡
                        break;
                    case 300:
                        _CardType = "ztk300";//征途支付卡300元卡
                        break;
                    case 468:
                        _CardType = "ztk468";//征途支付卡468元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 106)
            {
                #region 骏网一卡通
                switch (money)
                {
                    case 5:
                        _CardType = "jwk005";//骏网一卡通5元卡
                        break;
                    case 6:
                        _CardType = "jwk006";//骏网一卡通6元卡
                        break;
                    case 10:
                        _CardType = "jwk010";//骏网一卡通10元卡
                        break;
                    case 15:
                        _CardType = "jwk015";//骏网一卡通15元卡
                        break;
                    case 30:
                        _CardType = "jwk030";//骏网一卡通30元卡
                        break;
                    case 50:
                        _CardType = "jwk050";//骏网一卡通50元卡
                        break;
                    case 100:
                        _CardType = "jwk100";//骏网一卡通100元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 107)
            {
                #region 腾讯Q币卡
                switch (money)
                {
                    case 5:
                        _CardType = "qqb005";//腾讯Q币5元卡
                        break;
                    case 10:
                        _CardType = "qqb010";//腾讯Q币10元卡
                        break;
                    case 15:
                        _CardType = "qqb015";//腾讯Q币15元卡
                        break;
                    case 30:
                        _CardType = "qqb030";//腾讯Q币30元卡
                        break;
                    case 60:
                        _CardType = "qqb060";//腾讯Q币60元卡
                        break;
                    case 100:
                        _CardType = "qqb100";//腾讯Q币100元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 108)
            {
                #region 联通充值卡
                switch (money)
                {
                    case 20:
                        _CardType = "ltk020";//联通充值卡5元卡
                        break;
                    case 30:
                        _CardType = "ltk030";//联通充值卡10元卡
                        break;
                    case 50:
                        _CardType = "ltk050";//联通充值卡15元卡
                        break;
                    case 100:
                        _CardType = "ltk100";//联通充值卡30元卡
                        break;
                    case 300:
                        _CardType = "ltk300";//联通充值卡60元卡
                        break;
                    case 500:
                        _CardType = "ltk500";//联通充值卡100元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 109)
            {
                #region 久游一卡通
                switch (money)
                {
                    case 5:
                        _CardType = "jyk005";//久游一卡通5元卡
                        break;
                    case 10:
                        _CardType = "jyk010";//久游一卡通10元卡
                        break;
                    case 15:
                        _CardType = "jyk020";//久游一卡通15元卡
                        break;
                    case 20:
                        _CardType = "jyk015";//久游一卡通20元卡
                        break;
                    case 25:
                        _CardType = "jyk025";//久游一卡通25元卡
                        break;
                    case 30:
                        _CardType = "jyk030";//久游一卡通30元卡
                        break;
                    case 50:
                        _CardType = "jyk050";//久游一卡通50元卡
                        break;
                    case 100:
                        _CardType = "jyk100";//久游一卡通100元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 110)
            {
                #region 网易一卡通
                switch (money)
                {
                    case 5:
                        _CardType = "wyk005";//网易一卡通5元卡
                        break;
                    case 10:
                        _CardType = "wyk010";//网易一卡通10元卡
                        break;
                    case 15:
                        _CardType = "wyk015";//网易一卡通15元卡
                        break;
                    case 20:
                        _CardType = "wyk020";//网易一卡通20元卡
                        break;
                    case 30:
                        _CardType = "wyk030";//网易一卡通30元卡
                        break;
                    case 50:
                        _CardType = "wyk050";//网易一卡通50元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 111)
            {
                #region 完美一卡通
                switch (money)
                {
                    case 15:
                        _CardType = "wmk015";//完美一卡通15元卡
                        break;
                    case 30:
                        _CardType = "wmk030";//完美一卡通30元卡
                        break;
                    case 50:
                        _CardType = "wmk050";//完美一卡通50元卡
                        break;
                    case 100:
                        _CardType = "wmk100";//完美一卡通100元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 112)
            {
                #region 搜狐一卡通
                switch (money)
                {
                    case 5:
                        _CardType = "shk005";//搜狐一卡通5元卡
                        break;
                    case 10:
                        _CardType = "shk010";//搜狐一卡通10元卡
                        break;
                    case 15:
                        _CardType = "shk015";//搜狐一卡通15元卡
                        break;
                    case 30:
                        _CardType = "shk030";//搜狐一卡通30元卡
                        break;
                    case 40:
                        _CardType = "shk040";//搜狐一卡通40元卡
                        break;
                    case 100:
                        _CardType = "shk100";//搜狐一卡通100元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 113)
            {
                #region 电信充值卡
                switch (money)
                {
                    case 50:
                        _CardType = "dxk050";//电信50元充值卡
                        break;
                    case 100:
                        _CardType = "dxk100";//电信100元充值卡
                        break;
                }
                #endregion
            }
            else if (paytype == 114)
            {
                #region 声讯卡
                switch (money)
                {
                    case 5:
                        _CardType = "sxk005";//声讯卡5元充值卡 全国声讯卡5元面值 3005
                        break;
                    case 10:
                        _CardType = "sxk010";//电信10元充值卡  全国声讯卡10元面值 3010
                        break;
                    case 15:
                        _CardType = "sxk015";//电信10元充值卡 全国声讯卡15元面值 3015
                        break;
                }
                #endregion
            }
            else if (paytype == 117)
            {
                #region 纵游卡
                switch (money)
                {
                    case 10:
                        _CardType = "zyk010";//纵游一卡通10元卡
                        break;
                    case 15:
                        _CardType = "zyk015";//纵游一卡通15元卡
                        break;
                    case 30:
                        _CardType = "zyk030";//纵游一卡通30元卡
                        break;
                    case 50:
                        _CardType = "zyk050";//纵游一卡通50元卡
                        break;
                    case 100:
                        _CardType = "zyk100";//纵游一卡通100元卡
                        break;
                }
                #endregion
            }
            return _CardType;
        }
        #endregion

        #region GetSynchResponse
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseText"></param>
        /// <returns></returns>
        public SynchResponse GetSynchResponse(string responseText)
        {
            var response = new SynchResponse();

            try
            {
                responseText = responseText.Replace("<fill version=\"1.0\">", "");
                responseText = responseText.Replace("</fill>", "");

                Stream inStream = new MemoryStream(Encoding.Default.GetBytes(responseText));
                var document = new XmlDocument();
                document.Load(inStream);

                var selectSingleNode = document.SelectSingleNode("items");
                if (selectSingleNode != null)
                {
                    XmlNodeList childNodes = selectSingleNode.ChildNodes;
                    if (childNodes.Count != 0)
                    {
                        foreach (XmlNode node2 in childNodes)
                        {
                            if (node2.Attributes == null) continue;

                            string avalue = node2.Attributes["value"].InnerText;
                            switch (node2.Attributes["name"].InnerText)
                            {
                                case "state":
                                    response.State = avalue;
                                    break;
                                case "errcode":
                                    response.Errcode = avalue;
                                    break;
                                case "errmsg":
                                    response.Errmsg = avalue;
                                    break;
                                case "mark":
                                    response.Mark = avalue;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return response;
        }
        #endregion

        #region GetQueryResponse
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="responseText"></param>
        /// <returns></returns>
        public QueryResponse GetQueryResponse(string orderid,string responseText)
        {
            var response = new QueryResponse(orderid);

            try
            {
                responseText = responseText.Replace("<fill version=\"1.0\">", "");
                responseText = responseText.Replace("</fill>", "");

                Stream inStream = new MemoryStream(Encoding.Default.GetBytes(responseText));
                var document = new XmlDocument();
                document.Load(inStream);

                var selectSingleNode = document.SelectSingleNode("items");
                if (selectSingleNode != null)
                {
                    XmlNodeList childNodes = selectSingleNode.ChildNodes;
                    if (childNodes.Count != 0)
                    {
                        foreach (XmlNode node2 in childNodes)
                        {
                            if (node2.Attributes == null) continue;

                            string avalue = node2.Attributes["value"].InnerText;
                            switch (node2.Attributes["name"].InnerText)
                            {
                                case "state":
                                    response.State = avalue;
                                    break;
                                case "sd51no":
                                    response.Sd51No = avalue;
                                    break;
                                case "cardno":
                                    response.CardNo = avalue;
                                    break;
                                case "ordermoney":
                                    response.Ordermoney = avalue;
                                    break;
                                case "errcode":
                                    response.Errcode = avalue;
                                    break;
                                case "errmsg":
                                    response.Errmsg = avalue;
                                    break;
                                case "mark":
                                    response.Mark = avalue;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return response;
        }
        #endregion

        #region Finish
        public bool Finish(QueryResponse response)
        {
            bool success = false;

            if (response.State != "0")
            {
                int status = response.State == "1" ? 2 : 4;
                string opstate = "0";
                string viewMsg = "支付成功";
                if (status == 4)
                {
                    opstate = "99";
                    viewMsg = "其它错误";
                }

                var resp = new CardOrderSupplierResponse()
                {
                    SupplierId = SuppId,
                    SuppTransNo = response.Sd51No,
                    SysOrderNo = response.SysOrderNo,
                    OrderAmt = decimal.Parse(response.Ordermoney),
                    SuppAmt = 0M,
                    OrderStatus = status,
                    SuppErrorCode = response.State,
                    SuppErrorMsg = viewMsg,
                    Opstate = opstate,
                    ViewMsg = viewMsg,
                    Method = 1
                };

                OrderCardUtils.Finish(resp);

                success = true;
            }

            return success;
        }
        #endregion
    }
}

