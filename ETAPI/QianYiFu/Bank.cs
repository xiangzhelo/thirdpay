using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;
using System.Web.Security;

namespace viviapi.ETAPI.QianYiFu
{
    /// <summary>
    /// Ǫ�׸��ӿ�
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.qianyifu;

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


        internal string Returnurl { get { return this.SiteDomain + "/return/qianyifu/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/qianyifu/bank.aspx"; } }


        internal string Succflag = "<result>1</result>";
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
        public string PayBank(string orderid, decimal orderAmt, string bankcode, bool autoSubmit)
        {
            //�ύ��ַ
            string form_url = PostBankUrl;
            //�̻���
            string customerid = SuppAccount;
            string key = SuppKey;
            string banktype =GetBankCode(bankcode);
            string amount = orderAmt.ToString("0.00");
            //���п�֧��
            String param = String.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}{5}", customerid, bankcode, amount, orderid, NotifyUrl,key);
            string sign = viviLib.Security.Cryptography.MD5(param, "GB2312").ToLower();
            SynsSummitLogger("plain: " + param);
            
            SynsSummitLogger("SignMD5: " + sign);
            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"" + form_url + "\">";
            postForm += "<input type=\"hidden\" name=\"parter\" value=\"" + customerid + "\" />";
            postForm += "<input type=\"hidden\" name=\"type\" value=\"" + bankcode + "\" />";
            postForm += "<input type=\"hidden\" name=\"value\" value=\"" + amount + "\" />";
            postForm += "<input type=\"hidden\" name=\"orderid\" value=\"" + orderid + "\" />";
            postForm += "<input type=\"hidden\" name=\"callbackurl\" value=\"" + NotifyUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />";
            postForm += "<input type=\"hidden\" name=\"agent\" value=\"\" />";
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
            String key = SuppKey;//�����ļ���Կ
            //���ز���
            String orderid = HttpContext.Current.Request["orderid"];//���ض�����
            String opstate = HttpContext.Current.Request["opstate"];//���ش�����
            String ovalue = HttpContext.Current.Request["ovalue"];//����ʵ�ʳ�ֵ���
            String sign = HttpContext.Current.Request["sign"];//����ǩ��
            String ekaorderID = HttpContext.Current.Request["ekaorderid"];//¼��ʱ������ˮ�š�
            String ekatime = HttpContext.Current.Request["ekatime"];//����ʱ�䡣
            String attach = HttpContext.Current.Request["attach"];//���и�����Ϣ
            String msg = HttpContext.Current.Request["msg"];//���ض���������Ϣ

            String param = String.Format("orderid={0}&opstate={1}&ovalue={2}{3}", orderid, opstate, ovalue, key);//��֯����
            //�ȶ�ǩ���Ƿ���Ч
            if (sign.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(param, "MD5").ToLower()))
            {
                string _info = "֧��ʧ��";
                string opstate1 = "-1";
                int status = 4;


                if (opstate.Equals("0") || opstate.Equals("-3"))
                {
                    _info = "֧���ɹ�";
                    opstate1 = "0";
                    status = 2;

                }

                string returnUrl = string.Empty;

                OrderBankUtils.SuppPageReturn(SuppId
                                        , SuppKey
                                        , orderid
                                        , status
                                        , opstate1
                                        , string.Empty
                                        , Convert.ToDecimal(ovalue), 0M);
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
            plain = string.Format(customerid, sd51no, sdcustomno, SuppKey);
            string sign1 = Cryptography.MD5(plain).ToString();

            if (sign1 == sign)
            {
                verify = true;
            }
            //�ж�ǩ����֤�Ƿ�ͨ��
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

                OrderBankUtils.SuppNotify(SuppId
                  , sdcustomno
                  , sd51no
                  , status
                  , opstate
                  , state
                  , decimal.Parse(ordermoney), 0M
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
                    code = "970";  //��������
                    break;
                case "967":
                    code = "967"; //�й���������
                    break;
                case "964":
                    code = "964"; //�й�ũҵ����
                    break;
                case "965":
                    code = "965"; //�й���������
                    break;
                case "963":
                    code = "963"; //�й�����
                    break;
                case "977":
                    code = "977"; //�ַ�����
                    break;
                case "981":
                    code = "981"; //�й���ͨ����
                    break;
                case "980":
                    code = "980"; //�й���������
                    break;
                case "974":
                    code = "974"; //���ڷ�չ����
                    break;
                case "985":
                    code = "985"; //�㶫��չ����
                    break;
                case "962":
                    code = "962"; //��������
                    break;
                case "982":
                    code = "982"; //��������
                    break;
                case "972":
                    code = "972"; //��ҵ����
                    break;
                case "984":
                    code = "984"; //����ũ����ҵ����
                    break;
                //case "1015":
                //    code = "GZCB"; //��������
                //    break;
                //case "1016":
                //    code = "CUPS"; //�й�����
                //    break;
                case "976":
                    code = "976"; //�Ϻ�ũ����ҵ����
                    break;
                //case "971":
                //    code = "POST"; //�й�����
                //    break;
                case "989":
                    code = "989"; //��������
                    break;
                //case "988":
                //    code = "CBHB"; //��������
                //    break;
                case "990":
                    code = "990"; //����ũ������
                    break;
                case "979":
                    code = "979"; //�Ͼ�����
                    break;
                case "986":
                    code = "986"; //�й��������
                    break;
                //case "987":
                //    code = "BEA"; //��������
                //    break;
                //case "1025":
                //    code = "NBCB"; //��������
                //    break;
                case "983":
                    code = "983"; //��������
                    break;
                case "978":
                    code = "978"; //ƽ������
                    break;
                //case "1028":
                //    code = "HSB"; //��������
                //    break;
                case "968":
                    code = "968"; //��������
                    break;
                case "975":
                    code = "975"; //�Ϻ�����
                    break;
                case "971":
                    code = "971"; //�й�������������
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
