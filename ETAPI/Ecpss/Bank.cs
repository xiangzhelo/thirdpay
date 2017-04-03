using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;

namespace viviapi.ETAPI.Ecpss
{
    /// <summary>
    /// �㳱�ӿ�
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Ecpss;

        public Bank()
            : base(SuppId)
        {

        }

        internal string Returnurl { get { return this.SiteDomain + "/return/ecpss/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/ecpss/bank.aspx"; } }

        #region GetPayForm
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid">������</param>
        /// <param name="orderAmt"></param>
        /// <param name="bankcode"></param>
        /// <returns></returns>
        public string GetPayForm(string orderid, decimal orderAmt, string bankcode, bool autoSubmit)
        {
            String OrderDesc = "";
            String Remark = "";
            String AdviceURL = NotifyUrl;
            String ReturnURL = Returnurl;
            String BillNo = orderid;
            String MerNo = SuppAccount;
            String Amount = orderAmt.ToString("f2");
            String md5src = MerNo + "&" + BillNo + "&" + Amount + "&" + ReturnURL + "&" + SuppKey;
            String SignInfo = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5src, "MD5");
            String defaultBankNumber = GetBankCode(bankcode);
            String orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            String products = "products info";

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + PostBankUrl + "\"> \n";
            postForm += "<input type=\"hidden\" name=\"OrderDesc\" value=\"" + OrderDesc + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"Remark\" value=\"" + Remark + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"AdviceURL\" value=\"" + AdviceURL + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"ReturnURL\" value=\"" + ReturnURL + "\" /> \n";

            postForm += "<input type=\"hidden\" name=\"BillNo\" value=\"" + BillNo + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"MerNo\" value=\"" + MerNo + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"Amount\" value=\"" + Amount + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"SignInfo\" value=\"" + SignInfo + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"defaultBankNumber\" value=\"" + defaultBankNumber + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"orderTime\" value=\"" + orderTime + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"products\" value=\"" + products + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"B1\" value=\"Payment\" /> \n ";
            postForm += "</form>";

            if (autoSubmit == true)
            {
                //�Զ��ύ�ñ�����������
                postForm +=
                    "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            }

            return postForm;
        }
        #endregion

        #region Return
        /// <summary>
        /// 
        /// </summary>
        public void Return()
        {
            String MD5key = SuppKey;
            String BillNo = System.Web.HttpContext.Current.Request.Params["BillNo"].ToString();
            String Amount = System.Web.HttpContext.Current.Request.Params["Amount"].ToString();
            String Succeed = System.Web.HttpContext.Current.Request.Params["Succeed"].ToString();
            String Result = System.Web.HttpContext.Current.Request.Params["Result"].ToString();
            String SignMD5info = System.Web.HttpContext.Current.Request.Params["SignMD5info"].ToString();


            String md5src = BillNo + "&" + Amount + "&" + Succeed + "&" + MD5key;

            String md5sign;
            md5sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5src, "MD5");

            if (SignMD5info == md5sign)
            {
                string opstate = "-1";
                decimal realAmt = 0M;
                int status = 4;
                if (Succeed == "88")
                {
                    opstate = "0";
                    status = 2;

                    realAmt = decimal.Parse(Amount);
                }

                OrderBankUtils.SuppPageReturn(SuppId
                    , BillNo
                    , ""
                    , status
                    , opstate
                    , ""
                    , realAmt, 0M);
            }
            else
            {
                HttpContext.Current.Response.Write("ǩ������ȷ��");
            }
        }
        #endregion

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            String MD5key = SuppKey;
            String BillNo = System.Web.HttpContext.Current.Request.Params["BillNo"].ToString();
            String Amount = System.Web.HttpContext.Current.Request.Params["Amount"].ToString();
            String Succeed = System.Web.HttpContext.Current.Request.Params["Succeed"].ToString();
            String Result = System.Web.HttpContext.Current.Request.Params["Result"].ToString();
            String SignMD5info = System.Web.HttpContext.Current.Request.Params["SignMD5info"].ToString();


            String md5src = BillNo + "&" + Amount + "&" + Succeed + "&" + MD5key;

            String md5sign;
            md5sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5src, "MD5");

            if (SignMD5info == md5sign)
            {

                if (Succeed == "88")
                {
                    string opstate = "-1";
                    decimal realAmt = 0M;
                    int status = 4;
                    opstate = "0";
                    status = 2;

                    realAmt = decimal.Parse(Amount);
                    OrderBankUtils.SuppNotify(SuppId
              , BillNo
              , ""
              , status
              , opstate
              , Result
              , realAmt
              , 0M
              , "ok"
              , "Fail");
                }
                else
                {
                    HttpContext.Current.Response.Write("Fail");
                }


            }
            else
            {
                HttpContext.Current.Response.Write("ǩ������ȷ��");
            }
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
                case "962":
                    code = "CNCB"; //��������
                    break;
                case "963":
                    code = "BOCSH"; //�й�����
                    break;
                case "964":
                    code = "ABC"; //�й�ũҵ����
                    break;
                case "965":
                    code = "CCB"; //�й���������
                    break;
                case "967":
                    code = "ICBC"; //�й���������
                    break;
                case "968":
                    code = ""; //��������
                    break;
                case "970":
                    code = "CMB";  //��������
                    break;
                case "971":
                    code = "PSBC"; //�й�������������
                    break;
                case "972":
                    code = "CIB"; //��ҵ����
                    break;
                case "974":
                    code = "PAB"; //���ڷ�չ����
                    break;
                case "975":
                    code = "BOS"; //�Ϻ�����
                    break;
                case "976":
                    code = "SRCB"; //�Ϻ�ũ����ҵ����
                    break;
                case "977":
                    code = "SPDB"; //�ַ�����
                    break;
                case "978":
                    code = "PAB"; //ƽ������
                    break;
                case "979":
                    code = ""; //�Ͼ�����
                    break;
                case "980":
                    code = "CMBC"; //�й���������
                    break;
                case "981":
                    code = "BOCOM"; //�й���ͨ����
                    break;
                case "982":
                    code = "HXB"; //��������
                    break;
                case "983":
                    code = ""; //��������
                    break;
                case "984":
                    code = ""; //����ũ����ҵ����
                    break;

                case "985":
                    code = "GDB"; //�㶫��չ����
                    break;
                case "986":
                    code = "CEB"; //�й��������
                    break;
                case "989":
                    code = "BCCB"; //��������
                    break;

                case "990":
                    code = ""; //����ũ������
                    break;

                case "1000":
                    code = "UNIONPAY"; //��������֧��
                    break;

            }
            return code;
        }
        #endregion
    }
}
