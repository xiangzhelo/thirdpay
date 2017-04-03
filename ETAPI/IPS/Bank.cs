using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;

namespace viviapi.ETAPI.IPS
{
    /// <summary>
    /// ��Ѷ�ӿ�
    /// </summary>
    public class Bank: ETAPIBase
    {
        private const int SuppId = (int) SupplierCode.IPS;

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


        internal string Returnurl { get { return this.SiteDomain + "/return/ips/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/ips/bank.aspx"; } }


        internal string Succflag = "ipscheckok";
        internal string Failflag = "fail";

        #region PayBank
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankcode"></param>
        /// <param name="autoSubmit"></param>
        /// <returns></returns>
        public string PayBank(string orderid,decimal orderAmt,string bankcode,bool autoSubmit)
        {
            //�ύ��ַ
            string form_url = PostBankUrl;

            //�̻���
            string Mer_code = SuppAccount; 
            string Mer_key = SuppKey; 
            //�̻��������
            string Billno = orderid;
            //�������(����2λС��)
            string Amount = decimal.Round(orderAmt, 2).ToString("0.00");            

            //��������
            string BillDate = DateTime.Now.ToString("yyyyMMdd");

            //���� 01--��ǿ�
            //04--IPS�˻�֧��
            //08--IB֧��
            //16--�绰֧��
            //64--��ֵ��֧��
            string Gateway_Type = "01";

            //֧������ 
            //RMB �����
            //HKD �۱�
            //MYR ���
            //USD USD
            string  Currency_Type = "RMB";

            //����
            //GB--GB����
            //EN--Ӣ��
            //BIG5--BIG����
            //JP--����
            //FR--����
           // string Lang = "GB";

            //֧������ɹ����ص��̻�URL
            string Merchanturl = Returnurl;

            //֧�����ʧ�ܷ��ص��̻�URL
            string FailUrl = string.Empty; 

            //֧��������󷵻ص��̻�URL
            string ErrorUrl = string.Empty; 

            //�̻����ݰ�
            string Attach = string.Empty; 

            //��ʾ���
            string DispAmount = Amount;

            //����֧���ӿڼ��ܷ�ʽ
            //0--������
            //2--md5ժҪ
            //9--����
            string OrderEncodeType = "5"; 

            //���׷��ؽӿڼ��ܷ�ʽ 
              //<option value="10">�Ͻӿ�</option>
              //<option value="11">md5withRsa</option>
              //<option value="12" selected="selected">md5ժҪ</option>
              //<option value="9">����</option>
            string RetEncodeType = "17"; //Request.Form["RetEncodeType"];

            //���ط�ʽ
          //<option value="0">��Server to Server</option>
          //<option value="1" selected="selected">��Server to Server</option>
          //<option value="9">����</option>
            //string Rettype = "0"; //Request.Form["Rettype"];

            //Server to Server ����ҳ��URL
            string ServerUrl = NotifyUrl;// Request.Form["ServerUrl"];
            string RetType = "1";
            string DoCredit = "1";
            string Bankco = GetBankCode(bankcode);

            string plain = "billno{0}currencytype{1}amount{2}date{3}orderencodetype{4}{5}";
            plain = string.Format(plain, Billno, Currency_Type, Amount, BillDate, OrderEncodeType, Mer_key);
            SynsSummitLogger("plain: " + plain);
            string SignMD5 = viviLib.Security.Cryptography.MD5(plain);
            SynsSummitLogger("SignMD5: " + SignMD5);

            //����֧���ӿڵ�Md5ժҪ��ԭ��=������+���+����+֧������+�̻�֤�� 
            //string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Billno + Amount + BillDate + Currency_Type+Mer_key, "MD5").ToLower();

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + form_url + "\">";

            postForm += "<input type=\"hidden\" name=\"Mer_code\" value=\"" + Mer_code + "\" />";
            postForm += "<input type=\"hidden\" name=\"Billno\" value=\"" + Billno + "\" />";
            postForm += "<input type=\"hidden\" name=\"Amount\" value=\"" + Amount + "\" />";
            postForm += "<input type=\"hidden\" name=\"Date\" value=\"" + BillDate + "\" />";
            postForm += "<input type=\"hidden\" name=\"Currency_Type\" value=\"" + Currency_Type + "\" />";
            postForm += "<input type=\"hidden\" name=\"Gateway_Type\" value=\"" + Gateway_Type + "\" />";
           // postForm += "<input type=\"hidden\" name=\"Lang\" value=\"" + Lang + "\" />";
            postForm += "<input type=\"hidden\" name=\"Merchanturl\" value=\"" + Merchanturl + "\" />";
            postForm += "<input type=\"hidden\" name=\"FailUrl\" value=\"" + FailUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"ErrorUrl\" value=\"" + ErrorUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"Attach\" value=\"" + Attach + "\" />";
            postForm += "<input type=\"hidden\" name=\"DispAmount\" value=\"" + DispAmount + "\" />";
            postForm += "<input type=\"hidden\" name=\"OrderEncodeType\" value=\"" + OrderEncodeType + "\" />";
            postForm += "<input type=\"hidden\" name=\"RetEncodeType\" value=\"" + RetEncodeType + "\" />";            
            postForm += "<input type=\"hidden\" name=\"ServerUrl\" value=\"" + ServerUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"SignMD5\" value=\"" + SignMD5 + "\" />";
            postForm += "<input type=\"hidden\" name=\"DoCredit\" value=\"" + DoCredit + "\" />";
            postForm += "<input type=\"hidden\" name=\"Bankco\" value=\"" + Bankco + "\" />";
            postForm += "<input type=\"hidden\" name=\"RetType\" value=\"" + RetType + "\" />";
            postForm += "</form>";

            if (autoSubmit == true)
            {
                //�Զ��ύ�ñ�����������
                postForm +=
                    "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            }

            SynsSummitLogger("SignMD5: " + postForm);
            return postForm;
        }
        #endregion

        #region ReturnBank
        /// <summary>
        /// 
        /// </summary>
        public void ReturnBank()
        {
            //��������
            string billno = HttpContext.Current.Request["billno"];
            string amount = HttpContext.Current.Request["amount"];
            string currency_type = HttpContext.Current.Request["Currency_type"];
            string mydate = HttpContext.Current.Request["date"];
            string succ = HttpContext.Current.Request["succ"];
            string msg = HttpContext.Current.Request["msg"];
            string attach = HttpContext.Current.Request["attach"];
            string ipsbillno = HttpContext.Current.Request["ipsbillno"];
            string retEncodeType = HttpContext.Current.Request["retencodetype"];
            string signature = HttpContext.Current.Request["signature"];

            //ǩ��ԭ��
            string content = billno + amount + mydate + succ + ipsbillno + currency_type;

            //ǩ���Ƿ���ȷ
            Boolean verify = false;
            if (retEncodeType == "17")
            {
                string plain = "billno{0}currencytype{1}amount{2}date{3}succ{4}ipsbillno{5}retencodetype{6}{7}";
                plain = string.Format(plain, billno, currency_type, amount, mydate, succ, ipsbillno, retEncodeType, SuppKey);
                string signature1 = Cryptography.MD5(plain);
                if (signature1 == signature)
                {
                    verify = true;
                }
            }

            string info = "֧��ʧ��" + msg;
            //�ж�ǩ����֤�Ƿ�ͨ��
            if (verify == true)
            {
                string opstate = "-1";
                int status = 4;
                //�жϽ����Ƿ�ɹ�
                if (succ == "Y")
                {
                    info = "֧���ɹ�";
                    opstate = "0";
                    status = 2;
                }
               
                OrderBankUtils.SuppPageReturn(SuppId
                    , billno
                    , ipsbillno
                    , status
                    , opstate
                    , info
                    , decimal.Parse(amount),0M);
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
            //��������
            string billno = HttpContext.Current.Request["billno"];
            string amount = HttpContext.Current.Request["amount"];
            string currency_type = HttpContext.Current.Request["Currency_type"];
            string mydate = HttpContext.Current.Request["date"];
            string succ = HttpContext.Current.Request["succ"];
            string msg = HttpContext.Current.Request["msg"];
            string attach = HttpContext.Current.Request["attach"];
            string ipsbillno = HttpContext.Current.Request["ipsbillno"];
            string retEncodeType = HttpContext.Current.Request["retencodetype"];
            string signature = HttpContext.Current.Request["signature"];
            string ipsbanktime = HttpContext.Current.Request["ipsbanktime"];
            //ǩ��ԭ��
            string content = billno + amount + mydate + succ + ipsbillno + currency_type;

            //ǩ���Ƿ���ȷ
            Boolean verify = false;

            //��֤��ʽ��11-md5withRSA  12-md5
            if (retEncodeType == "17")
            {
                string merchant_key = this.SuppKey; //"GDgLwwdK270Qj1w4xho8lyTpRQZV9Jm5x4NwWOTThUa4fMhEBK9jOXFrKRT6xhlJuU2FEa89ov0ryyjfJuuPkcGzO5CeVx5ZIrkkt1aBlZV36ySvHOMcNv8rncRiy3DQ";

                string plain = "billno{0}currencytype{1}amount{2}date{3}succ{4}ipsbillno{5}retencodetype{6}{7}";
                plain = string.Format(plain, billno, currency_type, amount, mydate, succ, ipsbillno, retEncodeType, SuppKey);

                string signature1 = viviLib.Security.Cryptography.MD5(plain);
                //Md5ժҪ
                //string signature1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(content + merchant_key, "MD5").ToLower();

                if (signature1 == signature)
                {
                    verify = true;
                }
            }
            //�ж�ǩ����֤�Ƿ�ͨ��
            if (verify == true)
            {
                string opstate = "-1";
                int status = 4;
                //�жϽ����Ƿ�ɹ�
                if (succ == "Y")
                {
                    opstate = "0";
                    status = 2;
                }

                //viviapi.BLL.OrderBank bll = new viviapi.BLL.OrderBank();
                //bll.DoBankComplete(suppId, billno, ipsbillno, status, opstate, string.Empty, decimal.Parse(amount), 0M, true, false);
                //HttpContext.Current.Response.Write("ipscheckok");

                OrderBankUtils.SuppNotify(SuppId
                  , billno
                  , ipsbillno
                  , status
                  , opstate
                  , succ
                  , decimal.Parse(amount),0M
                  , Succflag
                  , Failflag);
            }
            else
            {
                HttpContext.Current.Response.Write("fail");
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
                case "970":
                    code = "00042";  //��������
                    break;
                case "967":
                    code = "00004"; //�й���������
                    break;
                case "964":
                    code = "00017"; //�й�ũҵ����
                    break;
                case "965":
                    code = "00012"; //�й���������
                    break;
                case "963":
                    code = "00083"; //�й�����
                    break;
                case "977":
                    code = "00032"; //�ַ�����
                    break;
                case "981":
                    code = "00005"; //�й���ͨ����
                    break;
                case "980":
                    code = "00013"; //�й���������
                    break;
                case "974":
                    code = "00023"; //���ڷ�չ����
                    break;
                case "985":
                    code = "00052"; //�㶫��չ����
                    break;
                case "962":
                    code = "00092"; //��������
                    break;
                case "982":
                    code = "00041"; //��������
                    break;
                case "972":
                    code = "00016"; //��ҵ����
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
                    code = "00057"; //�й��������
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
                    code = "00051"; //�й�������������
                    break;
                //case "1032":
                //    code = "UPOP"; //��������֧��
                //    break;
            }
            return code;
        }
        #endregion
    }
}
