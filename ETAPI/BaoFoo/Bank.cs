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
    /// �����֤����� user1����
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

            string strMerchantID = this.SuppAccount;//�̻���
            string strPayID = GetBankCode(bankCode);//�����п���ֵ��1
            string strTradeDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string strTransID = orderid;//�̻������ţ�������ˮ�ţ�(����ʹ���̻������ż��Ϲ󷽵�Ψһ��ʶ��)
            string strOrderMoney = decimal.Round((orderAmt * 100), 0).ToString(CultureInfo.InvariantCulture);//��������Ҫ�Ϳ����һ��(�˴��Է�Ϊ��λ)
            string strProductName = "";//��Ʒ����
            string strAmount = "1";//��Ʒ������Ϊ1
            string strProductLogo = "";//��ƷͼƬ��ַ
            string strUsername = "";
            string strEmail = "";
            string strMobile = "";
            string strAdditionalInfo = "";
            string strMerchant_url = Returnurl;//�ͻ�����ת��ַ
            string strReturn_url = NotifyUrl;//�������˷��ص�ַ
            string strNoticeType = "1";//0 ����ת 1 ����ת

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
                //�Զ��ύ�ñ�����������
                postForm += "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            }

            return postForm;
        }
        #endregion

        #region GetMd5Sign
        //md5ǩ��
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
            string MerchantID = HttpContext.Current.Request.Params["MerchantID"];//�̻���
            string TransID = HttpContext.Current.Request.Params["TransID"];//�̻���ˮ��
            string Result = HttpContext.Current.Request.Params["Result"];//֧�����(1:�ɹ�,0:ʧ��)
            string resultDesc = HttpContext.Current.Request.Params["resultDesc"];//֧���������
            string factMoney = HttpContext.Current.Request.Params["factMoney"];//ʵ�ʳɽ����
            string additionalInfo = HttpContext.Current.Request.Params["additionalInfo"];//����������Ϣ
            string SuccTime = HttpContext.Current.Request.Params["SuccTime"];//���׳ɹ�ʱ��
            string Md5Sign = HttpContext.Current.Request.Params["Md5Sign"].ToLower();//md5ǩ��

            string _Md5Key = this.SuppKey;
            string _WaitSign = MerchantID + TransID + Result + resultDesc + factMoney + additionalInfo + SuccTime + _Md5Key;

            if (Md5Sign.ToLower() == Md5Encrypt(_WaitSign).ToLower())
            {
                string _info = "֧��ʧ�� ԭ��" + GetErrorInfo(Result, resultDesc);
                string opstate = "-1";
                int status = 4;
                decimal tranAmt = 0M;

                if (Result.Equals("1"))
                {
                    _info = "֧���ɹ�";
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
                return "֧���ɹ�";
            else
            {
                switch (resultDesc)
                {
                    case "0000":
                        retInfo = "��ֵʧ��";
                        break;
                    case "0001":
                        retInfo = "ϵͳ����";
                        break;
                    case "0002":
                        retInfo = "������ʱ";
                        break;
                    case "0003":
                        retInfo = "����״̬�쳣";
                        break;
                    case "0004":
                        retInfo = "��Ч�̻�";
                        break;
                    case "0015":
                        retInfo = "���Ż��ܴ���";
                        break;
                    case "0016":
                        retInfo = "���Ϸ���IP��ַ";
                        break;
                    case "0018":
                        retInfo = "�����ѱ�ʹ��";
                        break;
                    case "0019":
                        retInfo = "����������";
                        break;
                    case "0020":
                        retInfo = "֧�������ʹ���";
                        break;
                    case "0021":
                        retInfo = "����������";
                        break;
                    case "0022":
                        retInfo = "����Ϣ������";
                        break;
                    case "0023":
                        retInfo = "���ţ����ܣ�����ȷ";
                        break;
                    case "0024":
                        retInfo = "�����ô˿�����������";
                        break;
                    case "0025":
                        retInfo = "������Ч";
                        break;
                    default:
                        retInfo = "֧��ʧ��";
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
            string MerchantID = HttpContext.Current.Request.Params["MerchantID"];//�̻���
            string TransID = HttpContext.Current.Request.Params["TransID"];//�̻���ˮ��
            string Result = HttpContext.Current.Request.Params["Result"];//֧�����(1:�ɹ�,0:ʧ��)
            string resultDesc = HttpContext.Current.Request.Params["resultDesc"];//֧���������
            string factMoney = HttpContext.Current.Request.Params["factMoney"];//ʵ�ʳɽ����
            string additionalInfo = HttpContext.Current.Request.Params["additionalInfo"];//����������Ϣ
            string SuccTime = HttpContext.Current.Request.Params["SuccTime"];//���׳ɹ�ʱ��
            string Md5Sign = HttpContext.Current.Request.Params["Md5Sign"].ToLower();//md5ǩ��

            string _Md5Key = this.SuppKey;
            string _WaitSign = MerchantID + TransID + Result + resultDesc + factMoney + additionalInfo + SuccTime + _Md5Key;

            if (Md5Sign.ToLower() == Md5Encrypt(_WaitSign).ToLower())
            {
                decimal tranAmt = 0M;
                string _info = "֧��ʧ�� ԭ��" + GetErrorInfo(Result, resultDesc);
                string opstate = "-1";
                int status = 4;

                if (Result.Equals("1"))
                {
                    _info = "֧���ɹ�";
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
        /// 1044 --��������
        /// 1046 --��������
        /// 1047 --��������
        /// 1048 --�ӱ�����
        /// 1049 --����ʡũ��������������
        /// 1051 --��������ҵ����
        /// 1054 --����ũ����ҵ����
        /// 1055 --��������
        /// 1056 --��ݸ����
        /// 1057 --��������
        /// 1058 --פ�����ҵ����
        /// </summary>
        /// <param name="paymodeId"></param>
        /// <returns></returns>
        public string GetBankCode(string paymodeId)
        {
            string code = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    code = "1001";  //��������
                    break;
                case "967":
                    code = "1002"; //�й���������
                    break;
                case "964":
                    code = "1005"; //�й�ũҵ����
                    break;
                case "965":
                    code = "1003"; //�й���������
                    break;
                case "963":
                    code = "1026"; //�й�����
                    break;
                case "977":
                    code = "1004"; //�ַ�����
                    break;
                case "981":
                    code = "1020"; //�й���ͨ����
                    break;
                case "980":
                    code = "1006"; //�й���������
                    break;
                case "974":
                    code = "1008"; //���ڷ�չ����
                    break;
                case "985":
                    code = "1036"; //�㶫��չ����
                    break;
                case "962":
                    code = "1039"; //��������
                    break;
                //case "982":
                //    code = "HXBC"; //��������
                //    break;
                case "972":
                    code = "1009"; //��ҵ����
                    break;
                //case "984":
                //    code = "00011"; //����ũ����ҵ����
                //    break;
                //case "1015":
                //    code = "GZCB"; //��������
                //    break;
                case "1016":
                    code = "1080"; //�й�����
                    break;
                case "976":
                    code = "1037"; //�Ϻ�ũ����ҵ����
                    break;
                case "971":
                    code = "1038"; //�й�����
                    break;
                case "989":
                    code = "1032"; //��������
                    break;
                case "988":
                    code = "1034"; //��������
                    break;
                //case "990":
                //    code = "00056"; //����ũ������
                //    break;
                //case "979":
                //    code = "00055"; //�Ͼ�����
                //    break;
                case "986":
                    code = "1022"; //�й��������
                    break;
                case "987":
                    code = "1033"; //��������
                    break;
                //case "1025":
                //    code = "NBCB"; //��������
                //    break;
                //case "983":
                //    code = "00081"; //��������
                //    break;
                case "978":
                    code = "1035"; //ƽ������
                    break;
                //case "1028":
                //    code = "HSB"; //��������
                //    break;
                //case "968":
                //    code = "00086"; //��������
                //    break;
                //case "975":
                //    code = "00084"; //�Ϻ�����
                //    break;
                //case "971":
                //    code = "PSBC"; //�й�������������
                //    break;
                //case "1032":
                //    code = "UPOP"; //��������֧��
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
                        //����
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
