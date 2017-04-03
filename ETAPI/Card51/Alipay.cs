using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;

namespace viviapi.ETAPI.Card51
{
    /// <summary>
    /// 51�ӿ�
    /// </summary>
    public class AliPay : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Card51;
        protected readonly string REQ_CUSTOMER_IP = null;//�ֶ���������������IP��ַ����Ҫ�����ڲ��Ի�������������������Ϊnull
        public AliPay()
            : base(SuppId)
        {

        }

        public static AliPay Instance
        {
            get
            {
                var instance = new AliPay();
                return instance;
            }
        }


        internal string Returnurl { get { return this.SiteDomain + "/return/card51/alipay.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/card51/alipay.aspx"; } }


        internal string Succflag = "<result>1</result>";
        internal string Failflag = "fail";
        private string ClientIp
        {
            get
            {
                string ip = null;
                string ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ipList))
                {
                    ip = ipList.Split(',')[0];
                }
                else
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (ip.IndexOf("::1") != -1)
                {
                    ip = "127.0.0.1";
                }
                if (REQ_CUSTOMER_IP != null)
                    ip = REQ_CUSTOMER_IP;
                return ip;
            }
        }
        #region PayBank
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankcode"></param>
        /// <param name="autoSubmit"></param>
        /// <returns></returns>
        public string PayBank(string orderid, decimal orderAmt, string bankcode, bool autoSubmit)
        {
            //�ύ��ַ
            string form_url = "http://www.51card.cn/gateway/alipay/alipay.asp";
            //�̻���
            string customerid = SuppAccount;
            string Mer_key = SuppKey;
            //�̻��������
            string sdcustomno = orderid;
            //�������(����2λС��)
            string ordermoney = decimal.Round(orderAmt, 2).ToString("0.00");
            string cardno = "34";
            string faceno = "zfb";
            string noticeurl = NotifyUrl;
            string backurl = Returnurl;
            string endcustomer = "";
            string endip = ClientIp;
            string remarks = "goodsName";
            string mark = "goodsDesc";

            string plain = "customerid={0}&sdcustomno={1}&ordermoney={2}&cardno={3}&faceno={4}&noticeurl={5}&endcustomer={6}&endip={7}&remarks={8}&mark={9}&key={10}";
            plain = string.Format(customerid, sdcustomno, ordermoney, cardno, faceno, noticeurl, endcustomer, endip, remarks, mark, Mer_key);
            SynsSummitLogger("plain: " + plain);
            string sign = viviLib.Security.Cryptography.MD5(plain).ToUpper();
            SynsSummitLogger("SignMD5: " + sign);
            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"" + form_url + "\">";
            postForm += "<input type=\"hidden\" name=\"customerid\" value=\"" + customerid + "\" />";
            postForm += "<input type=\"hidden\" name=\"sdcustomno\" value=\"" + sdcustomno + "\" />";
            postForm += "<input type=\"hidden\" name=\"ordermoney\" value=\"" + ordermoney + "\" />";
            postForm += "<input type=\"hidden\" name=\"cardno\" value=\"" + cardno + "\" />";
            postForm += "<input type=\"hidden\" name=\"faceno\" value=\"" + faceno + "\" />";
            postForm += "<input type=\"hidden\" name=\"noticeurl\" value=\"" + noticeurl + "\" />";
            postForm += "<input type=\"hidden\" name=\"backurl\" value=\"" + Returnurl + "\" />";
            postForm += "<input type=\"hidden\" name=\"endcustomer\" value=\"" + endcustomer + "\" />";
            postForm += "<input type=\"hidden\" name=\"endip\" value=\"" + endip + "\" />";
            postForm += "<input type=\"hidden\" name=\"remarks\" value=\"" + remarks + "\" />";
            postForm += "<input type=\"hidden\" name=\"mark\" value=\"" + mark + "\" />";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />";
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
            string orderId = HttpContext.Current.Request["sdcustomno"];
            string status = HttpContext.Current.Request["state"];//1:�ɹ� 2��ʧ��
            string tradeNo = HttpContext.Current.Request["sd51no"];
            string sign = HttpContext.Current.Request["sign"];

            //ǩ���Ƿ���ȷ
            Boolean verify = false;

            string plain = "sdcustomno={0}&state={1}&sd51no={2}&key={3}";
            plain = string.Format(plain, orderId, status, tradeNo, SuppKey);
            string signature1 = Cryptography.MD5(plain).ToUpper();
            if (signature1 == sign)
            {
                verify = true;
            }


            string info = "֧��ʧ��";
            //�ж�ǩ����֤�Ƿ�ͨ��
            if (verify == true)
            {
                string opstate = "-1";
                int status1 = 4;
                //�жϽ����Ƿ�ɹ�
                if (status == "1")
                {
                    info = "֧���ɹ�";
                    opstate = "0";
                    status1 = 2;
                }
                return;
                OrderBankUtils.SuppPageReturn(SuppId
                    , orderId
                    , tradeNo
                    , status1
                    , opstate
                    , info
                    , 0m, 0M);
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

            string state = HttpContext.Current.Request["state"];
            string customerid = HttpContext.Current.Request["customerid"];
            string sd51no = HttpContext.Current.Request["sd51no"];
            string sdcustomno = HttpContext.Current.Request["sdcustomno"];
            string ordermoney = HttpContext.Current.Request["ordermoney"];
            string cardno = HttpContext.Current.Request["cardno"];
            string mark = HttpContext.Current.Request["mark"];//1:�ɹ� 2��ʧ��
            string sign = HttpContext.Current.Request["sign"];
            string resign = HttpContext.Current.Request["resign"];
            string des = HttpContext.Current.Request["des"];
            //ǩ���Ƿ���ȷ
            Boolean verify = false;
            string plain = "customerid={0}&sd51no={1}&sdcustomno={2}&key={3}";
            plain = string.Format(plain, customerid, sd51no, sdcustomno, SuppKey);
            string sign1 = Cryptography.MD5(plain).ToUpper();
            this.LogWrite("51Alipay_abcУ�飺plain=" + plain + "\t sign1:" + sign1 + "\t sign:" + sign);
            if (sign1 == sign)
            {
                verify = true;
            }
            //�ж�ǩ����֤�Ƿ�ͨ��
            this.LogWrite("51Alipay_abcУ�飺" + verify);
            if (verify == true)
            {
                string opstate = "-1";
                int status = 4;
                //�жϽ����Ƿ�ɹ�
                if (state == "1")
                {
                    opstate = "0";
                    status = 2;
                }
                this.LogWrite("51Alipay_abc��" + ordermoney);
                OrderBankUtils.SuppNotify(SuppId
                  , sdcustomno
                  , sd51no
                  , status
                  , opstate
                  , state
                  , Convert.ToDecimal(ordermoney), Convert.ToDecimal(ordermoney)
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
                    code = "COMM"; //�й���ͨ����
                    break;
                case "980":
                    code = "CMBC"; //�й���������
                    break;
                case "974":
                    code = "SZPAB"; //���ڷ�չ����
                    break;
                case "985":
                    code = "GDB"; //�㶫��չ����
                    break;
                case "962":
                    code = "CITIC"; //��������
                    break;
                case "982":
                    code = "HXB"; //��������
                    break;
                case "972":
                    code = "CIB"; //��ҵ����
                    break;
                case "984":
                    code = "GNXS"; //����ũ����ҵ����
                    break;
                //case "1015":
                //    code = "GZCB"; //��������
                //    break;
                //case "1016":
                //    code = "CUPS"; //�й�����
                //    break;
                case "976":
                    code = "SHRCB"; //�Ϻ�ũ����ҵ����
                    break;
                //case "971":
                //    code = "POST"; //�й�����
                //    break;
                //case "989":
                //    code = "00050"; //��������
                //    break;
                //case "988":
                //    code = "CBHB"; //��������
                //    break;
                //case "990":
                //    code = "00056"; //����ũ������
                //    break;
                case "979":
                    code = "NJCB"; //�Ͼ�����
                    break;
                case "986":
                    code = "CEB"; //�й��������
                    break;
                //case "987":
                //    code = "BEA"; //��������
                //    break;
                case "1025":
                    code = "NBCB"; //��������
                    break;
                case "983":
                    code = "HCCB"; //��������
                    break;
                case "978":
                    code = "SZPAB"; //ƽ������
                    break;
                //case "1028":
                //    code = "HSB"; //��������
                //    break;
                //case "968":
                //    code = "00086"; //��������
                //    break;
                case "975":
                    code = "BOS"; //�Ϻ�����
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
    }
}
