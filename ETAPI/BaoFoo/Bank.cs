using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.Payment;
using viviapi.BLL.Payment;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.Model;
using viviLib.ExceptionHandling;
using viviLib.Web;
using viviLib.Logging;
using System.Security;
using System.Security.Cryptography;

namespace viviapi.ETAPI.Baofoo
{
    /// <summary>
    /// 身份验证码放在 user1上面
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Baofoo;

        public Bank()
            : base(SuppId)
        {

        }

        public static Bank Instance
        {
            get
            {
                var instance = new Bank();
                return instance;
            }
        }


        internal string Returnurl { get { return this.SiteDomain + "/return/baofoo/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/baofoo/bank.aspx"; } }

        internal string Succflag = "OK";
        internal string Failflag = "fail";

        #region PayBank
        /// <summary>
        /// http://paygate.baofoo.com/PayReceive/payindex.aspx
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankCode"></param>
        /// <param name="autoSubmit"></param>
        /// <returns></returns>
        public string PayBank(string orderid, decimal orderAmt, string bankCode, bool autoSubmit)
        {
            string formUrl = "http://paygate.baofoo.com/PayReceive/payindex.aspx";

            if (!string.IsNullOrEmpty(SuppInfo.postBankUrl))
            {
                formUrl = SuppInfo.postBankUrl;
            }

            string strMerchantID = this.SuppAccount;//商户号
            string strPayID = GetBankCode(bankCode);//神州行卡充值是1
            string strTradeDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string strTransID = orderid;//商户订单号（交易流水号）(建议使用商户订单号加上贵方的唯一标识号)
            string strOrderMoney = decimal.Round((orderAmt * 100), 0).ToString(CultureInfo.InvariantCulture);//订单金额，需要和卡面额一致(此处以分为单位)
            string strProductName = "";//商品名称
            string strAmount = "1";//商品数量，为1
            string strProductLogo = "";//商品图片地址
            string strUsername = "";
            string strEmail = "";
            string strMobile = "";
            string strAdditionalInfo = "";
            string strMerchant_url = Returnurl;//客户端跳转地址
            string strReturn_url = NotifyUrl;//服务器端返回地址
            string strNoticeType = "1";//0 不跳转 1 会跳转

            string strMd5Sign = GetMd5Sign(strMerchantID, strPayID, strTradeDate,
                 strTransID, strOrderMoney, strMerchant_url, strReturn_url, strNoticeType, this.SuppKey);

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + formUrl + "\"> \n";
            postForm += "<input type=\"hidden\" name=\"MerchantID\" value=\"" + strMerchantID + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"PayID\" value=\"" + strPayID + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"TradeDate\" value=\"" + strTradeDate + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"TransID\" value=\"" + strTransID + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"OrderMoney\" value=\"" + strOrderMoney + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"ProductName\" value=\"" + strProductName + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"Amount\" value=\"" + strAmount + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"ProductLogo\" value=\"" + strProductLogo + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"Username\" value=\"" + strUsername + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"Email\" value=\"" + strEmail + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"Mobile\" value=\"" + strMobile + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"AdditionalInfo\" value=\"" + strAdditionalInfo + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"Merchant_url\" value=\"" + strMerchant_url + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"Return_url\" value=\"" + strReturn_url + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"Md5Sign\" value=\"" + strMd5Sign + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"NoticeType\" value=\"" + strNoticeType + "\" />\n";
            postForm += "</form>";

            if (autoSubmit == true)
            {
                //自动提交该表单到测试网关
                postForm += "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            }

            return postForm;
        }
        #endregion

        #region GetMd5Sign
        //md5签名
        private string GetMd5Sign(string _MerchantID, string _PayID, string _TradeDate, string _TransID,
            string _OrderMoney, string _Merchant_url, string _Return_url, string _NoticeType, string _Md5Key)
        {
            string str = _MerchantID + _PayID + _TradeDate + _TransID + _OrderMoney + _Merchant_url + _Return_url + _NoticeType + _Md5Key;
            return Md5Encrypt(str);

        }

        public static string Md5Encrypt(string strToBeEncrypt)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] FromData = System.Text.Encoding.GetEncoding("gb2312").GetBytes(strToBeEncrypt);
            Byte[] TargetData = md5.ComputeHash(FromData);
            string Byte2String = "";
            for (int i = 0; i < TargetData.Length; i++)
            {
                Byte2String += TargetData[i].ToString("x2");
            }
            return Byte2String.ToLower();
        }
        #endregion

        #region ReturnBank
        /// <summary>
        /// 
        /// </summary>
        public void ReturnBank()
        {
            string MerchantID = HttpContext.Current.Request.Params["MerchantID"];//商户号
            string TransID = HttpContext.Current.Request.Params["TransID"];//商户流水号
            string Result = HttpContext.Current.Request.Params["Result"];//支付结果(1:成功,0:失败)
            string resultDesc = HttpContext.Current.Request.Params["resultDesc"];//支付结果描述
            string factMoney = HttpContext.Current.Request.Params["factMoney"];//实际成交金额
            string additionalInfo = HttpContext.Current.Request.Params["additionalInfo"];//订单附加消息
            string SuccTime = HttpContext.Current.Request.Params["SuccTime"];//交易成功时间
            string Md5Sign = HttpContext.Current.Request.Params["Md5Sign"].ToLower();//md5签名

            string _Md5Key = this.SuppKey;
            string _WaitSign = MerchantID + TransID + Result + resultDesc + factMoney + additionalInfo + SuccTime + _Md5Key;

            if (Md5Sign.ToLower() == Md5Encrypt(_WaitSign).ToLower())
            {
                string _info = "支付失败 原因" + GetErrorInfo(Result, resultDesc);
                string opstate = "-1";
                int status = 4;
                decimal tranAmt = 0M;

                if (Result.Equals("1"))
                {
                    _info = "支付成功";
                    opstate = "0";
                    status = 2;
                    tranAmt = decimal.Parse(factMoney) / 100M;
                }

                string returnUrl = string.Empty;

                OrderBankUtils.SuppPageReturn(SuppId
                                        , TransID
                                        , ""
                                        , status
                                        , opstate
                                        , string.Empty
                                        , tranAmt, 0M);
            }
            else
            {

            }
        }
        #endregion

        #region GetErrorInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="resultDesc"></param>
        /// <returns></returns>
        public string GetErrorInfo(string result, string resultDesc)
        {
            string retInfo = "";
            if (result == "1")
                return "支付成功";
            else
            {
                switch (resultDesc)
                {
                    case "0000":
                        retInfo = "充值失败";
                        break;
                    case "0001":
                        retInfo = "系统错误";
                        break;
                    case "0002":
                        retInfo = "订单超时";
                        break;
                    case "0003":
                        retInfo = "订单状态异常";
                        break;
                    case "0004":
                        retInfo = "无效商户";
                        break;
                    case "0015":
                        retInfo = "卡号或卡密错误";
                        break;
                    case "0016":
                        retInfo = "不合法的IP地址";
                        break;
                    case "0018":
                        retInfo = "卡密已被使用";
                        break;
                    case "0019":
                        retInfo = "订单金额错误";
                        break;
                    case "0020":
                        retInfo = "支付的类型错误";
                        break;
                    case "0021":
                        retInfo = "卡类型有误";
                        break;
                    case "0022":
                        retInfo = "卡信息不完整";
                        break;
                    case "0023":
                        retInfo = "卡号，卡密，金额不正确";
                        break;
                    case "0024":
                        retInfo = "不能用此卡继续做交易";
                        break;
                    case "0025":
                        retInfo = "订单无效";
                        break;
                    default:
                        retInfo = "支付失败";
                        break;
                }
                return retInfo;
            }
        }
        #endregion

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            string MerchantID = HttpContext.Current.Request.Params["MerchantID"];//商户号
            string TransID = HttpContext.Current.Request.Params["TransID"];//商户流水号
            string Result = HttpContext.Current.Request.Params["Result"];//支付结果(1:成功,0:失败)
            string resultDesc = HttpContext.Current.Request.Params["resultDesc"];//支付结果描述
            string factMoney = HttpContext.Current.Request.Params["factMoney"];//实际成交金额
            string additionalInfo = HttpContext.Current.Request.Params["additionalInfo"];//订单附加消息
            string SuccTime = HttpContext.Current.Request.Params["SuccTime"];//交易成功时间
            string Md5Sign = HttpContext.Current.Request.Params["Md5Sign"].ToLower();//md5签名

            string _Md5Key = this.SuppKey;
            string _WaitSign = MerchantID + TransID + Result + resultDesc + factMoney + additionalInfo + SuccTime + _Md5Key;

            if (Md5Sign.ToLower() == Md5Encrypt(_WaitSign).ToLower())
            {
                decimal tranAmt = 0M;
                string _info = "支付失败 原因" + GetErrorInfo(Result, resultDesc);
                string opstate = "-1";
                int status = 4;

                if (Result.Equals("1"))
                {
                    _info = "支付成功";
                    opstate = "0";
                    status = 2;
                    tranAmt = decimal.Parse(factMoney) / 100M;
                }

                string returnUrl = string.Empty;


                OrderBankUtils.SuppNotify(SuppId
                                        , TransID
                                        , SuccTime
                                        , status
                                        , opstate
                                        , string.Empty
                                        , tranAmt,0M
                                        , Succflag
                                        , Failflag);
            }
            else
            {
                HttpContext.Current.Response.Write("Md5CheckFail");
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        #region GetBankCode
        /// <summary>
        /// 1044 --晋城银行
        /// 1046 --宁波银行
        /// 1047 --日照银行
        /// 1048 --河北银行
        /// 1049 --湖南省农村信用社联合社
        /// 1051 --威海市商业银行
        /// 1054 --重庆农村商业银行
        /// 1055 --大连银行
        /// 1056 --东莞银行
        /// 1057 --富滇银行
        /// 1058 --驻马店商业银行
        /// </summary>
        /// <param name="paymodeId"></param>
        /// <returns></returns>
        public string GetBankCode(string paymodeId)
        {
            string code = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    code = "1001";  //招商银行
                    break;
                case "967":
                    code = "1002"; //中国工商银行
                    break;
                case "964":
                    code = "1005"; //中国农业银行
                    break;
                case "965":
                    code = "1003"; //中国建设银行
                    break;
                case "963":
                    code = "1026"; //中国银行
                    break;
                case "977":
                    code = "1004"; //浦发银行
                    break;
                case "981":
                    code = "1020"; //中国交通银行
                    break;
                case "980":
                    code = "1006"; //中国民生银行
                    break;
                case "974":
                    code = "1008"; //深圳发展银行
                    break;
                case "985":
                    code = "1036"; //广东发展银行
                    break;
                case "962":
                    code = "1039"; //中信银行
                    break;
                //case "982":
                //    code = "HXBC"; //华夏银行
                //    break;
                case "972":
                    code = "1009"; //兴业银行
                    break;
                //case "984":
                //    code = "00011"; //广州农村商业银行
                //    break;
                //case "1015":
                //    code = "GZCB"; //广州银行
                //    break;
                case "1016":
                    code = "1080"; //中国银联
                    break;
                case "976":
                    code = "1037"; //上海农村商业银行
                    break;
                case "971":
                    code = "1038"; //中国邮政
                    break;
                case "989":
                    code = "1032"; //北京银行
                    break;
                case "988":
                    code = "1034"; //渤海银行
                    break;
                //case "990":
                //    code = "00056"; //北京农商银行
                //    break;
                //case "979":
                //    code = "00055"; //南京银行
                //    break;
                case "986":
                    code = "1022"; //中国光大银行
                    break;
                case "987":
                    code = "1033"; //东亚银行
                    break;
                //case "1025":
                //    code = "NBCB"; //宁波银行
                //    break;
                //case "983":
                //    code = "00081"; //杭州银行
                //    break;
                case "978":
                    code = "1035"; //平安银行
                    break;
                //case "1028":
                //    code = "HSB"; //徽商银行
                //    break;
                //case "968":
                //    code = "00086"; //浙商银行
                //    break;
                //case "975":
                //    code = "00084"; //上海银行
                //    break;
                //case "971":
                //    code = "PSBC"; //中国邮政储蓄银行
                //    break;
                //case "1032":
                //    code = "UPOP"; //银联在线支付
                //    break;
                default:
                    code = "1000";
                    break;
            }
            return code;
        }
        #endregion

        #region OrderQuery
        /// <summary>
        /// https://paygate.baofoo.com/Check/OrderQuery.aspx
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public string OrderQuery(string orderid)
        {
            try
            {
                string queryUrl = "https://paygate.baofoo.com/Check/OrderQuery.aspx";

                string strMerchantID = this.SuppAccount;
                string strTransID = orderid;

                string strMd5Sign = Md5Encrypt(strMerchantID + strTransID + SuppKey);

                string postData = string.Format("MerchantID={0}&TransID={1}&Md5Sign={2}", strMerchantID, strTransID,
                    strMd5Sign);

                string responseText = WebClientHelper.GetString(queryUrl
                    , postData
                    , "POST"
                    , Encoding.GetEncoding("utf-8")
                    , 10000);

                return responseText;

            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }

            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseText"></param>
        /// <returns></returns>
        public OrderQueryResult Analyze(string responseText)
        {
            if (!string.IsNullOrEmpty(responseText))
            {
                string[] arr = responseText.Split('|');

                var result = new OrderQueryResult
                {
                    MerchantID = arr[0],
                    TransID = arr[1],
                    CheckResult = arr[2],
                    FactMoney = arr[3],
                    SuccTime = arr[4],
                    Md5Sign = arr[5],
                    CheckOk = false
                };

                string strMd5Sign =
                    Md5Encrypt(result.MerchantID + result.TransID + result.CheckResult + result.FactMoney +
                               result.SuccTime + SuppKey);

                result.CheckOk = (result.Md5Sign == strMd5Sign);

                return result;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderAmt"></param>
        public void Reconcilie(OrderQueryResult info, decimal orderAmt)
        {
            if (info != null)
            {
                if (info.CheckOk)
                {
                    byte result = 0;
                    decimal factMoney = 0M;

                    if (info.CheckResult == "Y")
                    {
                        result = 2;
                        factMoney = decimal.Parse(info.FactMoney);
                        if (orderAmt == factMoney)
                        {
                            result = 1;
                        }
                    }
                    else if (info.CheckResult == "F")
                    {
                        result = 4;
                    }

                    if (result > 0)
                    {
                        //对账
                        BLL.Order.Bank.Factory.Instance.Reconcilie(info.TransID, result, factMoney);
                    }
                  
                }
            }
        }

        public void Reconcilie(string orderid, decimal orderAmt)
        {
            string responseText = OrderQuery(orderid);

            if (!string.IsNullOrEmpty(responseText))
            {
                var info = Analyze(responseText);

                if (info != null)
                {
                    Reconcilie(info, orderAmt);
                }
            }
        }

        #endregion
    }
}
