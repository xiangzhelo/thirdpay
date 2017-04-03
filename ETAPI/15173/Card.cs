using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using viviapi.Model.Payment;
using viviapi.BLL.Payment;
using viviapi.Model.Order;
using viviapi.SysConfig;
using viviapi.Model;
using viviLib.Web;
using viviLib.Logging;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviapi.Model.Order.Card;
using viviLib.ExceptionHandling;
using System.Globalization;


namespace viviapi.ETAPI.Card15173
{
    /// <summary>
    /// 
    /// </summary>
    public class Card : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Card15173;
        public Card()
            : base(SuppId)
        {

        }

        public static Card15173.Card Default
        {
            get
            {
                var card = new Card15173.Card();
                return card;
            }
        }

        public string NotifyUrl
        {
            get
            {
                //return this.SiteDomain + "/notify/Card15173_Notify.aspx";
                return SiteDomain + "/receive/Card15173/card.aspx"; ;
            }
        }

        internal string Succflag = "OK";

        #region CardSend
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_orderid"></param>
        /// <param name="_cardno"></param>
        /// <param name="_cardpwd"></param>
        /// <param name="_typeId"></param>
        /// <param name="cardvalue"></param>
        /// <param name="supporderid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public CardSynchCallBack CardSend(CardOrderSummitArgs o)
        {
            //string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string errmsg
            var callBack = new CardSynchCallBack();

            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");            
            

            string commitUrl = this.PostCardUrl + "?";
            if (string.IsNullOrEmpty(PostCardUrl))
            {
                commitUrl = "http://zl.15173.net/AdvancedInterface.aspx?";
            }
            string payType = GetPayType(o.CardTypeId);
            string ka_type1 = GetCardType(o.CardTypeId, o.FaceValue);
            string attach = string.Empty;
            string zidy_code = string.Empty;

            string plain = String.Format("bargainor_id={0}&sp_billno={1}&pay_type={2}&return_url={3}&attach={4}&key={5}"
                , SuppAccount
                , o.SysOrderNo
                , payType
                , NotifyUrl
                , attach
                , SuppKey
                );
            SynsSummitLogger("Card15173明文:" + plain);
            string sign = viviLib.Security.Cryptography.MD5(plain).ToUpper();
            SynsSummitLogger("Card15173密文:" + sign);
            try
            {
                System.Text.StringBuilder postData = new StringBuilder();
                postData.AppendFormat("&bargainor_id={0}", SuppAccount);//商户ID
                postData.AppendFormat("&sp_billno={0}", o.SysOrderNo);//商户订单号
                postData.AppendFormat("&total_fee={0}", o.FaceValue.ToString(CultureInfo.InvariantCulture));//交易金额
                postData.AppendFormat("&pay_type={0}", payType);
                postData.AppendFormat("&return_url={0}", NotifyUrl);
                postData.AppendFormat("&select_url={0}", NotifyUrl);
                //postData.AppendFormat("&ka_type1={0}", ka_type1);
                postData.AppendFormat("&ka_number1={0}", o.CardNo);
                postData.AppendFormat("&ka_password1={0}", o.CardPass);
                postData.AppendFormat("&attach={0}", attach);
                postData.AppendFormat("&zidy_code={0}", zidy_code);                
                postData.AppendFormat("&sign={0}", sign);

                callBack.Success = 0;

                SynsSummitLogger(commitUrl);
                SynsSummitLogger("postData:" + postData.ToString());

                string retCode = viviLib.Web.WebClientHelper.GetString(commitUrl, postData.ToString(), "GET", System.Text.Encoding.GetEncoding("GB2312"), 10000);
                SynsSummitLogger("retCode:" + retCode);
               

                if (retCode.ToUpper() == "OK")
                {
                    callBack.Success = 1;
                    callBack.SuppTransNo = "";
                    callBack.SuppCallBackText = retCode;
                    callBack.SuppErrorCode = "";
                    callBack.SuppErrorMsg = "";
                    //opstate = "0";
                }
                else
                {
                    callBack.SuppTransNo = "";
                    callBack.SuppCallBackText = retCode;
                    callBack.SuppErrorCode = retCode;
                    callBack.SuppErrorMsg = GetMsgInfo(retCode);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);

                callBack.Success = 0;
                callBack.Message = ex.Message;
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
                case "Err001":
                    return "您的商户不存在！";
                case "Err002":
                    return "您的密钥有误！";
                case "Err003":
                    return "您的选择的支付类型不存在！";
                case "Err004":
                    return "您的选择的支付类型暂时停止使用！";
                case "Err005":
                    return "您的选择的面值不存在！";
                case "Err006":
                    return "写入失败";
                case "Err007":
                    return "商户单号重复";
                case "Err008":
                    return "卡号错误";
                default:
                    return "部分地区维护";
            }
            return cardstatus;
        }
        #endregion

        #region CardNotify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            HttpRequest req = HttpContext.Current.Request;

            AsynsRetLogger(req.RawUrl);
            if (req.Form.Count > 0)
            {
                string data = WebClientHelper.FormatRequestData(req.Form, System.Text.Encoding.Default);
                AsynsRetLogger(data);
            }

            String pay_result = req["pay_result"].Trim(); //交易结果0为成功，3为失败
            String bargainor_id = req["bargainor_id"].Trim(); //15173交易订单号
            String transaction_id = req["transaction_id"].Trim(); //商户ID
            String sp_billno = req["sp_billno"].Trim();     //商户请求交易时候提交的订单号
            String total_fee = req["total_fee"].Trim();   //实际交易交易金额（注意：可能和商户请求交易提交过来的total_fee不一样！）
            String sign = req["sign"].Trim(); 
            String attach = req["attach"].Trim();
            String pay_info = req["pay_info"].Trim();//交易类型中文说明

            //'生成MD5签名
            String sign_text = String.Format("pay_result={0}&bargainor_id={1}&sp_billno={2}&total_fee={3}&attach={4}&key={5}"
                , pay_result
                , bargainor_id
                , sp_billno
                , total_fee
                , attach
                , SuppKey);

            String localSign = viviLib.Security.Cryptography.MD5(sign_text).ToUpper();
            try
            {
                if (localSign == sign)
                {
                    string viewMsg = string.Empty;
                    string opstate = "-1";
                    int status = 4;

                    if (pay_result == "0")
                    {
                        opstate = "0";
                        status = 2;
                        viewMsg = "支付成功";
                    }
                    else
                    {
                        viewMsg = "支付失败";
                    }
                    var response = new CardOrderSupplierResponse()
                    {
                        SupplierId = SuppId,
                        SuppTransNo = bargainor_id,
                        SysOrderNo = sp_billno,
                        OrderAmt = decimal.Parse(total_fee),
                        SuppAmt = 0M,
                        OrderStatus = status,
                        SuppErrorCode = status.ToString(),
                        Opstate = opstate,
                        SuppErrorMsg = viewMsg,
                        ViewMsg = viewMsg,
                        Method = 1
                    };
                    OrderCardUtils.SuppNotify(response, Succflag);
                    //BLL.OrderCard bll = new viviapi.BLL.OrderCard();
                    //bll.ReceiveSuppResult(SuppId, sp_billno, transaction_id, status, opstate, pay_result, decimal.Parse(total_fee), 0M, pay_result);
                    //HttpContext.Current.Response.Write("OK");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                HttpContext.Current.Response.Write("error");
            }
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
                        _CardType = "92";//神州行10元卡
                        break;
                    case 20:
                        _CardType = "150";//神州行20元卡
                        break;
                    case 30:
                        _CardType = "52";//神州行30元卡
                        break;
                    case 50:
                        _CardType = "4";//神州行50元卡
                        break;
                    case 100:
                        _CardType = "5";//神州行100元卡
                        break;
                    case 300:
                        _CardType = "161";//神州行300元卡
                        break;
                    case 500:
                        _CardType = "0107";//神州行500元卡
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
                        _CardType = "38";//盛大一卡通5元卡
                        break;
                    case 10:
                        _CardType = "6";//盛大一卡通10元卡
                        break;
                    case 15:
                        _CardType = "93";//盛大一卡通15元卡
                        break;
                    case 25:
                        _CardType = "110";//盛大一卡通25元卡
                        break;
                    case 30:
                        _CardType = "7";//盛大一卡通30元卡
                        break;
                    case 35:
                        _CardType = "111";//盛大一卡通35元卡
                        break;
                    case 45:
                        _CardType = "8";//盛大一卡通45元卡
                        break;
                    case 50:
                        _CardType = "47";//盛大一卡通50元卡
                        break;
                    case 100:
                        _CardType = "9";//盛大一卡通100元卡
                        break;
                    case 300:
                        _CardType = "112";//盛大一卡通300元卡
                        break;
                    case 350:
                        _CardType = "113";//盛大一卡通350元卡
                        break;
                    case 1000:
                        _CardType = "48";//盛大一卡通1000元卡
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
                        _CardType = "39";//征途支付卡10元卡
                        break;
                    case 15:
                        _CardType = "121";//征途支付卡15元卡
                        break;
                    case 20:
                        _CardType = "40";//征途支付卡20元卡
                        break;
                    case 25:
                        _CardType = "122";//征途支付卡25元卡
                        break;
                    case 30:
                        _CardType = "41";//征途支付卡30元卡
                        break;
                    case 50:
                        _CardType = "42";//征途支付卡50元卡
                        break;
                    case 60:
                        _CardType = "43";//征途支付卡60元卡
                        break;
                    case 100:
                        _CardType = "44";//征途支付卡100元卡
                        break;
                    case 300:
                        _CardType = "45";//征途支付卡300元卡
                        break;
                    case 468:
                        _CardType = "70";//征途支付卡468元卡
                        break;
                    case 500:
                        _CardType = "198";//征途支付卡468元卡
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
                        _CardType = "49";//骏网一卡通5元卡
                        break;
                    case 6:
                        _CardType = "66";//骏网一卡通6元卡
                        break;
                    case 9:
                        _CardType = "205";//骏网一卡通9元卡
                        break;
                    case 10:
                        _CardType = "10";//骏网一卡通10元卡
                        break;
                    case 14:
                        _CardType = "206";//骏网一卡通14元卡
                        break;
                    case 15:
                        _CardType = "11";//骏网一卡通15元卡
                        break;
                    case 30:
                        _CardType = "12";//骏网一卡通30元卡
                        break;
                    case 50:
                        _CardType = "13";//骏网一卡通50元卡
                        break;
                    case 100:
                        _CardType = "14";//骏网一卡通100元卡
                        break;
                    case 200:
                        _CardType = "202";//骏网一卡通200元卡
                        break;
                    case 300:
                        _CardType = "199";//骏网一卡通300元卡
                        break;
                    case 500:
                        _CardType = "200";//骏网一卡通500元卡
                        break;
                    case 1000:
                        _CardType = "201";//骏网一卡通1000元卡
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
                        _CardType = "78";//腾讯Q币5元卡
                        break;
                    case 10:
                        _CardType = "79";//腾讯Q币10元卡
                        break;
                    case 15:
                        _CardType = "80";//腾讯Q币15元卡
                        break;
                    case 30:
                        _CardType = "81";//腾讯Q币30元卡
                        break;
                    case 60:
                        _CardType = "82";//腾讯Q币60元卡
                        break;
                    case 100:
                        _CardType = "86";//腾讯Q币100元卡
                        break;
                    case 200:
                        _CardType = "134";//腾讯Q币100元卡
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
                        _CardType = "129";//联通充值卡5元卡
                        break;
                    case 30:
                        _CardType = "130";//联通充值卡10元卡
                        break;
                    case 50:
                        _CardType = "135";//联通充值卡15元卡
                        break;
                    case 100:
                        _CardType = "136";//联通充值卡30元卡
                        break;
                    case 300:
                        _CardType = "137";//联通充值卡60元卡
                        break;
                    case 500:
                        _CardType = "138";//联通充值卡100元卡
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
                        _CardType = "71";//久游一卡通5元卡
                        break;
                    case 10:
                        _CardType = "72";//久游一卡通10元卡
                        break;
                    case 15:
                        _CardType = "73";//久游一卡通15元卡
                        break;
                    case 20:
                        _CardType = "147";//久游一卡通20元卡
                        break;
                    case 25:
                        _CardType = "76";//久游一卡通25元卡
                        break;
                    case 30:
                        _CardType = "74";//久游一卡通30元卡
                        break;
                    case 50:
                        _CardType = "75";//久游一卡通50元卡
                        break;
                    case 100:
                        _CardType = "145";//久游一卡通100元卡
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
                        _CardType = "158";//网易一卡通5元卡
                        break;
                    case 10:
                        _CardType = "146";//网易一卡通10元卡
                        break;
                    case 15:
                        _CardType = "53";//网易一卡通15元卡
                        break;
                    case 20:
                        _CardType = "159";//网易一卡通20元卡
                        break;
                    case 30:
                        _CardType = "54";//网易一卡通30元卡
                        break;
                    case 50:
                        _CardType = "160";//网易一卡通50元卡
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
                        _CardType = "115";//完美一卡通15元卡
                        break;
                    case 30:
                        _CardType = "116";//完美一卡通30元卡
                        break;
                    case 50:
                        _CardType = "117";//完美一卡通50元卡
                        break;
                    case 100:
                        _CardType = "118";//完美一卡通100元卡
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
                        _CardType = "55";//搜狐一卡通5元卡
                        break;
                    case 10:
                        _CardType = "56";//搜狐一卡通10元卡
                        break;
                    case 15:
                        _CardType = "57";//搜狐一卡通15元卡
                        break;
                    case 30:
                        _CardType = "58";//搜狐一卡通30元卡
                        break;
                    case 40:
                        _CardType = "63";//搜狐一卡通40元卡
                        break;
                    case 100:
                        _CardType = "77";//搜狐一卡通100元卡
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
                        _CardType = "148";//电信50元充值卡
                        break;
                    case 100:
                        _CardType = "149";//电信100元充值卡
                        break;
                }
                #endregion
            }
            else if (paytype == 115)
            {
                #region 光宇一卡通
                switch (money)
                {
                    case 5:
                        _CardType = "154";//光宇一卡通10元卡
                        break;
                    case 15:
                        _CardType = "186";//光宇一卡通15元卡
                        break;
                    case 10:
                        _CardType = "155";//光宇一卡通10元卡
                        break;
                    case 30:
                        _CardType = "156";//光宇一卡通30元卡
                        break;
                    case 100:
                        _CardType = "157";//光宇一卡通100元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 116)
            {
                #region 金山一卡通
                switch (money)
                {
                    case 15:
                        _CardType = "151";//金山一卡通10元卡
                        break;
                    case 30:
                        _CardType = "152";//金山一卡通15元卡
                        break;
                    case 50:
                        _CardType = "153";//金山一卡通10元卡
                        break;
                    case 100:
                        _CardType = "185";//金山一卡通30元卡
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
                        _CardType = "162";//纵游一卡通10元卡
                        break;
                    case 15:
                        _CardType = "163";//纵游一卡通15元卡
                        break;
                    case 30:
                        _CardType = "164";//纵游一卡通30元卡
                        break;
                    case 50:
                        _CardType = "165";//纵游一卡通50元卡
                        break;
                    case 100:
                        _CardType = "166";//纵游一卡通100元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 118)
            {
                #region 天下通一卡通
                switch (money)
                {
                    case 5:
                        _CardType = "207";//天下通一卡通5元卡
                        break;
                    case 10:
                        _CardType = "208";//天下通一卡通10元卡
                        break;
                    case 15:
                        _CardType = "209";//天下通一卡通15元卡
                        break;
                    case 30:
                        _CardType = "210";//天下通一卡通30元卡
                        break;
                    case 50:
                        _CardType = "211";//天下通一卡通50元卡
                        break;
                    case 100:
                        _CardType = "212";//天下通一卡通100元卡
                        break;
                }
                #endregion
            }
            else if (paytype == 119)
            {
                #region 天宏一卡通
                switch (money)
                {
                    case 5:
                        _CardType = "216";//天下通一卡通5元卡
                        break;
                    case 10:
                        _CardType = "213";//天下通一卡通10元卡
                        break;
                    case 15:
                        _CardType = "217";//天下通一卡通15元卡
                        break;
                    case 30:
                        _CardType = "218";//天下通一卡通30元卡
                        break;
                    case 50:
                        _CardType = "219";//天下通一卡通50元卡
                        break;
                    case 100:
                        _CardType = "220";//天下通一卡通100元卡
                        break;
                }
                #endregion
            }
            return _CardType;
        }
        #endregion

        #region GetPayType
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
        public string GetPayType(int _type)
        {
            string _code = string.Empty;
            switch (_type)
            {
                case 103:
                    _code = "a";//神州行卡
                    break;
                case 104:
                    _code = "c";//盛大一卡通
                    break;
                case 105:
                    _code = "d";//征途支付卡
                    break;
                case 106:
                    _code = "b";//骏网一卡通
                    break;
                case 107:
                    _code = "r";//腾讯Q币卡
                    break;
                case 108:
                    _code = "x";//联通充值卡
                    break;
                case 109:
                    _code = "q";//久游一卡通
                    break;
                case 110:
                    _code = "m";//网易一卡通
                    break;
                case 111:
                    _code = "u";//完美一卡通
                    break;
                case 112:
                    _code = "n";//搜狐一卡通
                    break;
                case 113:
                    _code = "y";//电信充值卡
                    break;
                case 115:
                    _code = "g";//光宇一卡通
                    break;
                case 116:
                    _code = "f";//金山一卡通
                    break;
                case 117:
                    _code = "e";//纵游一卡通
                    break;
                case 118:
                    _code = "i";//天下一卡通
                    break;
                case 119:
                    _code = "L";//天宏一卡通
                    break;
            }
            return _code;
        }

        #endregion
    }
}

