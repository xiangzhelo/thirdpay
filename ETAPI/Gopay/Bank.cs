using System;
using System.Globalization;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;

namespace viviapi.ETAPI.Gopay
{
    /// <summary>
    /// �����֤����� user1����
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Gopay;

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

        internal string Returnurl { get { return this.SiteDomain + "/return/gopay/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/gopay/bank.aspx"; } }
        internal string ShowUrl { get { return this.SiteDomain + "/return/gopay/showUrl.aspx"; } }


        internal string Succflag = "RespCode=0000|JumpURL=";
        internal string Failflag = "RespCode=9999|JumpURL=";

        #region PayBank
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankCode"></param>
        /// <param name="autoSubmit"></param>
        /// <returns></returns>
        public string PayBank(string orderid, decimal orderAmt, string bankCode, bool autoSubmit)
        {
            string version = "2.1";//���ذ汾��
            string charset = "1";//�ַ���
            string language = "1"; //�������԰汾
            string signType = "1";//���ļ��ܷ�ʽ 1 MD5 2 SHA
            string tranCode = "8888";//����ָ���˽��׵����ͣ�֧�����ؽӿڱ���Ϊ8888
            string merchantID = SuppAccount;//ǩԼ�������̻�Ψһ�û�ID
            string merOrderNum = orderid; //������ ��ʽ�����֣���ĸ���»��ߣ������ߣ��л��� ���ڴ����̻���������Ϣ��ÿ���µĽ���������һ���µĶ����ţ����isRepeatSubmitΪ1����δ֧���Ķ����ſ����ظ��ύ����Ҫ��֤���׽��һ�¡�
            string tranAmt = decimal.Round(orderAmt, 2).ToString(CultureInfo.InvariantCulture);
            string feeAmt = "0"; //�̻���ȡӶ����
            string currencyType = "156";
            string frontMerUrl = string.Empty;//returnurl;
            string backgroundMerUrl = NotifyUrl;
            string tranDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string virCardNoIn = SuppUserName;//����ָ�����ڹ�����ƽ̨����Ĺ������˻��š�
            string tranIP = "127.0.0.1";
            string isRepeatSubmit = "1";
            string goodsName = "";
            string goodsDetail = "";
            string buyerName = "";
            string buyerContact = "";
            string merRemark1 = "";
            string merRemark2 = "";
            bankCode = GetBankCode(bankCode);
            string gopayServerTime = GetGopayServerTime();
            string verficationCode = SuppKey;

            // ��֯��������
            string plain = "version=[" + version + "]tranCode=[" + tranCode + "]merchantID=[" + merchantID + "]merOrderNum=[" + merOrderNum + "]tranAmt=[" + tranAmt + "]feeAmt=[" + feeAmt + "]tranDateTime=[" + tranDateTime + "]frontMerUrl=[" + frontMerUrl + "]backgroundMerUrl=[" + backgroundMerUrl + "]orderId=[]gopayOutOrderId=[]tranIP=[" + tranIP + "]respCode=[]gopayServerTime=[" + gopayServerTime + "]VerficationCode=[" + verficationCode + "]";

            SynsSummitLogger("����������:" + plain);

            string signValue = Cryptography.MD5(plain);

            string formUrl = PostBankUrl;

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + formUrl + "\">";
            postForm += "<input type=\"hidden\" name=\"version\" value=\"" + version + "\" />";
            postForm += "<input type=\"hidden\" name=\"charset\" value=\"" + charset + "\" />";
            postForm += "<input type=\"hidden\" name=\"language\" value=\"" + language + "\" />";
            postForm += "<input type=\"hidden\" name=\"signType\" value=\"" + signType + "\" />";
            postForm += "<input type=\"hidden\" name=\"tranCode\" value=\"" + tranCode + "\" />";
            postForm += "<input type=\"hidden\" name=\"merchantID\" value=\"" + merchantID + "\" />";
            postForm += "<input type=\"hidden\" name=\"merOrderNum\" value=\"" + merOrderNum + "\" />";
            postForm += "<input type=\"hidden\" name=\"tranAmt\" value=\"" + tranAmt + "\" />";
            postForm += "<input type=\"hidden\" name=\"feeAmt\" value=\"" + feeAmt + "\" />";
            postForm += "<input type=\"hidden\" name=\"currencyType\" value=\"" + currencyType + "\" />";
            postForm += "<input type=\"hidden\" name=\"frontMerUrl\" value=\"" + frontMerUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"backgroundMerUrl\" value=\"" + backgroundMerUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"tranDateTime\" value=\"" + tranDateTime + "\" />";
            postForm += "<input type=\"hidden\" name=\"virCardNoIn\" value=\"" + virCardNoIn + "\" />";
            postForm += "<input type=\"hidden\" name=\"tranIP\" value=\"" + tranIP + "\" />";
            postForm += "<input type=\"hidden\" name=\"isRepeatSubmit\" value=\"" + isRepeatSubmit + "\" />";
            postForm += "<input type=\"hidden\" name=\"goodsName\" value=\"" + goodsName + "\" />";
            postForm += "<input type=\"hidden\" name=\"goodsDetail\" value=\"" + goodsDetail + "\" />";
            postForm += "<input type=\"hidden\" name=\"buyerName\" value=\"" + buyerName + "\" />";
            postForm += "<input type=\"hidden\" name=\"buyerContact\" value=\"" + buyerContact + "\" />";
            postForm += "<input type=\"hidden\" name=\"merRemark1\" value=\"" + merRemark1 + "\" />";
            postForm += "<input type=\"hidden\" name=\"merRemark2\" value=\"" + merRemark2 + "\" />";
            postForm += "<input type=\"hidden\" name=\"bankCode\" value=\"" + bankCode + "\" />";
            postForm += "<input type=\"hidden\" name=\"userType\" value=\"" + viviapi.SysConfig.PaymentSetting.Gopay_userType + "\" />";
            postForm += "<input type=\"hidden\" name=\"VerficationCode\" value=\"" + verficationCode + "\" />";
            postForm += "<input type=\"hidden\" name=\"signValue\" value=\"" + signValue + "\" />";
            postForm += "<input type=\"hidden\" name=\"gopayServerTime\" value=\"" + gopayServerTime + "\" />";
            postForm += "</form>";

            if (autoSubmit == true)
                //�Զ��ύ�ñ�����������
                postForm += "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";

            return postForm;
        }
        #endregion

        #region ReturnBank
        /// <summary>
        /// 
        /// </summary>
        public void ReturnBank()
        {
            string version = HttpContext.Current.Request.Form["version"];
            string charset = HttpContext.Current.Request.Form["charset"];
            string language = HttpContext.Current.Request.Form["language"];
            string signType = HttpContext.Current.Request.Form["signType"];
            string tranCode = HttpContext.Current.Request.Form["tranCode"];
            string merchantID = HttpContext.Current.Request.Form["merchantID"];
            string merOrderNum = HttpContext.Current.Request.Form["merOrderNum"];
            string tranAmt = HttpContext.Current.Request.Form["tranAmt"];
            string feeAmt = HttpContext.Current.Request.Form["feeAmt"];
            string frontMerUrl = HttpContext.Current.Request.Form["frontMerUrl"];
            string backgroundMerUrl = HttpContext.Current.Request.Form["backgroundMerUrl"];
            string tranDateTime = HttpContext.Current.Request.Form["tranDateTime"];
            string tranIP = HttpContext.Current.Request.Form["tranIP"];
            string respCode = HttpContext.Current.Request.Form["respCode"];
            string msgExt = HttpContext.Current.Request.Form["msgExt"];
            string orderId = HttpContext.Current.Request.Form["orderId"];
            string gopayOutOrderId = HttpContext.Current.Request.Form["gopayOutOrderId"];
            string bankCode = HttpContext.Current.Request.Form["bankCode"];
            string tranFinishTime = HttpContext.Current.Request.Form["tranFinishTime"];
            string merRemark1 = HttpContext.Current.Request.Form["merRemark1"];
            string merRemark2 = HttpContext.Current.Request.Form["merRemark2"];
            string verficationCode = SuppKey;
            string signValueFromGopay = HttpContext.Current.Request.Form["signValue"];

            // ��֯��������
            string plain = "version=[" + version + "]tranCode=[" + tranCode + "]merchantID=[" + merchantID + "]merOrderNum=[" + merOrderNum + "]tranAmt=[" + tranAmt + "]feeAmt=[" + feeAmt + "]tranDateTime=[" + tranDateTime + "]frontMerUrl=[" + frontMerUrl + "]backgroundMerUrl=[" + backgroundMerUrl + "]orderId=[" + orderId + "]gopayOutOrderId=[" + gopayOutOrderId + "]tranIP=[" + tranIP + "]respCode=[" + respCode + "]gopayServerTime=[]VerficationCode=[" + verficationCode + "]";
            AsynsRetLogger("ReturnBank plain:" + plain);

            string signValue = viviLib.Security.Cryptography.MD5(plain, "gbk");

            AsynsRetLogger("ReturnBank signValue:" + signValue);

            AsynsRetLogger("ReturnBank signValueFromGopay:" + signValueFromGopay);

            if (signValue.Equals(signValueFromGopay))
            {
                string info = "֧��ʧ��" + msgExt;
                string opstate = "-1";
                int status = 4;
                decimal dtranAmt = 0M;

                if (respCode.Equals("0000"))
                {
                    info = "֧���ɹ�";
                    opstate = "0";
                    status = 2;

                    dtranAmt = decimal.Parse(tranAmt);
                }

                OrderBankUtils.SuppNotify(SuppId
                                        , merOrderNum
                                        , gopayOutOrderId
                                        , status
                                        , opstate
                                        , string.Empty
                                        , dtranAmt,0M
                                        , Succflag + ShowUrl
                                        , Failflag + ShowUrl);
            }
            else
            {
                HttpContext.Current.Response.Write("RespCode=9999|JumpURL=" + ShowUrl);
            }
        }
        #endregion

        #region Show
        /// <summary>
        /// 
        /// </summary>
        public void Show()
        {
            string version = HttpContext.Current.Request["version"];
            string charset = HttpContext.Current.Request["charset"];
            string language = HttpContext.Current.Request["language"];
            string signType = HttpContext.Current.Request["signType"];
            string tranCode = HttpContext.Current.Request["tranCode"];
            string merchantID = HttpContext.Current.Request["merchantID"];
            string merOrderNum = HttpContext.Current.Request["merOrderNum"];
            string tranAmt = HttpContext.Current.Request["tranAmt"];
            string feeAmt = HttpContext.Current.Request["feeAmt"];
            string frontMerUrl = HttpContext.Current.Request["frontMerUrl"];
            string backgroundMerUrl = HttpContext.Current.Request["backgroundMerUrl"];
            string tranDateTime = HttpContext.Current.Request["tranDateTime"];
            string tranIP = HttpContext.Current.Request["tranIP"];
            string respCode = HttpContext.Current.Request["respCode"];
            string msgExt = HttpContext.Current.Request["msgExt"];
            string orderId = HttpContext.Current.Request["orderId"];
            string gopayOutOrderId = HttpContext.Current.Request["gopayOutOrderId"];
            string bankCode = HttpContext.Current.Request["bankCode"];
            string tranFinishTime = HttpContext.Current.Request["tranFinishTime"];
            string merRemark1 = HttpContext.Current.Request["merRemark1"];
            string merRemark2 = HttpContext.Current.Request["merRemark2"];
            string verficationCode = SuppKey;
            string signValueFromGopay = HttpContext.Current.Request["signValue"];

            // ��֯��������
            string plain = "version=[" + version + "]tranCode=[" + tranCode + "]merchantID=[" + merchantID + "]merOrderNum=[" + merOrderNum + "]tranAmt=[" + tranAmt + "]feeAmt=[" + feeAmt + "]tranDateTime=[" + tranDateTime + "]frontMerUrl=[" + frontMerUrl + "]backgroundMerUrl=[" + backgroundMerUrl + "]orderId=[" + orderId + "]gopayOutOrderId=[" + gopayOutOrderId + "]tranIP=[" + tranIP + "]respCode=[" + respCode + "]gopayServerTime=[]VerficationCode=[" + verficationCode + "]";
            AsynsRetLogger("Show plain:" + plain);

            string signValue = viviLib.Security.Cryptography.MD5(plain, "gbk");

            AsynsRetLogger("Show signValue:" + signValue);

            AsynsRetLogger("Show signValueFromGopay:" + signValueFromGopay);

            if (signValue.Equals(signValueFromGopay))
            {
                string info = "֧��ʧ��" + msgExt;
                string opstate = "-1";
                int status = 4;
                decimal dtranAmt = 0M;

                if (respCode.Equals("0000"))
                {
                    info = "֧���ɹ�";
                    opstate = "0";
                    status = 2;

                    dtranAmt = decimal.Parse(tranAmt);
                }

                OrderBankUtils.SuppPageReturn(SuppId
                                        , merOrderNum
                                        , gopayOutOrderId
                                        , status
                                        , opstate
                                        , string.Empty
                                        , dtranAmt,0M);
            }
            else
            {
                HttpContext.Current.Response.Write("RespCode=9999|JumpURL=" + ShowUrl);
            }
        }
        #endregion

        #region Functions
        #region sign
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string sign(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
        #endregion

        #region GetBankCode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymodeId"></param>
        /// <returns></returns>
        public string GetBankCode(string paymodeId)
        {
            string code = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    code = "CMB";  //��������
                    break;
                case "967":
                    code = "ICBC"; //�й���������
                    break;
                case "964":
                    code = "ABC"; //�й�ũҵ����
                    break;
                case "965":
                    code = "CCB"; //�й���������
                    break;
                case "963":
                    code = "BOC"; //�й�����
                    break;
                case "977":
                    code = "SPDB"; //�ַ�����
                    break;
                case "981":
                    code = "BOCOM"; //�й���ͨ����
                    break;
                case "980":
                    code = "CMBC"; //�й���������
                    break;
                case "974":
                    code = "SDB"; //���ڷ�չ����
                    break;
                case "985":
                    code = "GDB"; //�㶫��չ����
                    break;
                case "962":
                    code = "CITIC"; //��������
                    break;
                case "982":
                    code = "HXBC"; //��������
                    break;
                case "972":
                    code = "CIB"; //��ҵ����
                    break;
                case "984":
                    code = "00011"; //����ũ����ҵ����
                    break;
                //case "1015":
                //    code = "GZCB"; //��������
                //    break;
                //case "1016":
                //    code = "CUPS"; //�й�����
                //    break;
                case "976":
                    code = "00030"; //�Ϻ�ũ����ҵ����
                    break;
                //case "971":
                //    code = "POST"; //�й�����
                //    break;
                case "989":
                    code = "00050"; //��������
                    break;
                //case "988":
                //    code = "CBHB"; //��������
                //    break;
                case "990":
                    code = "00056"; //����ũ������
                    break;
                case "979":
                    code = "00055"; //�Ͼ�����
                    break;
                case "986":
                    code = "CEB"; //�й��������
                    break;
                //case "987":
                //    code = "BEA"; //��������
                //    break;
                //case "1025":
                //    code = "NBCB"; //��������
                //    break;
                case "983":
                    code = "00081"; //��������
                    break;
                case "978":
                    code = "00087"; //ƽ������
                    break;
                //case "1028":
                //    code = "HSB"; //��������
                //    break;
                case "968":
                    code = "00086"; //��������
                    break;
                case "975":
                    code = "00084"; //�Ϻ�����
                    break;
                case "971":
                    code = "PSBC"; //�й�������������
                    break;
                //case "1032":
                //    code = "UPOP"; //��������֧��
                //    break;
            }
            return code;
        }
        #endregion

        #region GetGopayServerTime
        /// <summary>
        /// ��ȡ������������ʱ�� ����ʱ��� https://www.gopay.com.cn/PGServer/time https://211.88.7.30/PGServer/time
        /// </summary>
        /// <returns>��ʽYYYYMMDDHHMMSS</returns>
        public static String GetGopayServerTime()
        {
            try
            {
                return viviLib.Web.WebClientHelper.GetString("https://www.gopay.com.cn/PGServer/time", null, "get", System.Text.Encoding.UTF8, 10000).Trim();
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
        #endregion

    }
}
