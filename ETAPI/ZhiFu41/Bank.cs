using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;

namespace viviapi.ETAPI.ZhiFu41
{
    /// <summary>
    /// 41֧���ӿ�
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.zhifu41;
        protected readonly string REQ_CUSTOMER_IP = "116.226.209.138";//�ֶ���������������IP��ַ����Ҫ�����ڲ��Ի�������������������Ϊnull
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


        internal string Returnurl { get { return this.SiteDomain + "/return/zhifu41/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/zhifu41/bank.aspx"; } }
        internal string Succflag = "success";
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
        /// <param name="orderid">����ID</param>
        /// <param name="orderAmt">�������</param>
        /// <param name="bankcode">���б��</param>
        /// <param name="autoSubmit">�Զ��ύ����������</param>
        /// <returns></returns>
        public string PayBank(string orderid, decimal orderAmt, string bankcode, bool autoSubmit)
        {
            string form_url = PostBankUrl;
            string input_charset = "UTF-8";
            string notify_url = NotifyUrl;
            string return_url = Returnurl;
            string pay_type = "1";
            string bank_code = GetBankCode(bankcode);
            string merchant_code = SuppAccount;
            string order_no = orderid;
            string order_amount = decimal.Round(orderAmt, 2).ToString("0.00");
            string order_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string req_referer = "www.baidu.com";
            string customer_ip = ClientIp;
            string sign = "";


            string plain = "bank_code={0}&customer_ip={1}&input_charset={2}&merchant_code={3}&notify_url={4}&order_amount={5}&order_no={6}&order_time={7}&pay_type={8}&req_referer={9}&return_url={10}&key={11}";
            plain = string.Format(bank_code, customer_ip, input_charset, merchant_code, notify_url, order_amount, order_no, order_no, pay_type, req_referer, return_url, SuppKey);
            SynsSummitLogger("plain: " + plain);
            string SignMD5 = viviLib.Security.Cryptography.MD5(plain, "UTF-8");
            SynsSummitLogger("SignMD5: " + SignMD5);

            //����֧���ӿڵ�Md5ժҪ��ԭ��=������+���+����+֧������+�̻�֤�� 
            //string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Billno + Amount + BillDate + Currency_Type+Mer_key, "MD5").ToLower();

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + form_url + "\">";

            postForm += "<input type=\"hidden\" name=\"bank_code\" value=\"" + bank_code + "\" />";
            postForm += "<input type=\"hidden\" name=\"customer_ip\" value=\"" + customer_ip + "\" />";
            postForm += "<input type=\"hidden\" name=\"input_charset\" value=\"" + input_charset + "\" />";
            postForm += "<input type=\"hidden\" name=\"merchant_code\" value=\"" + merchant_code + "\" />";
            postForm += "<input type=\"hidden\" name=\"notify_url\" value=\"" + notify_url + "\" />";
            postForm += "<input type=\"hidden\" name=\"order_amount\" value=\"" + order_amount + "\" />";
            // postForm += "<input type=\"hidden\" name=\"Lang\" value=\"" + Lang + "\" />";
            postForm += "<input type=\"hidden\" name=\"order_no\" value=\"" + order_no + "\" />";
            postForm += "<input type=\"hidden\" name=\"order_time\" value=\"" + order_time + "\" />";
            postForm += "<input type=\"hidden\" name=\"pay_type\" value=\"" + pay_type + "\" />";
            postForm += "<input type=\"hidden\" name=\"req_referer\" value=\"" + req_referer + "\" />";
            postForm += "<input type=\"hidden\" name=\"return_url\" value=\"" + return_url + "\" />";
            postForm += "<input type=\"hidden\" name=\"SuppKey\" value=\"" + SuppKey + "\" />";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + SignMD5 + "\" />";
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
        public static readonly string MERCHANT_CODE = "merchant_code";
        public static readonly string NOTIFY_TYPE = "notify_type";
        public static readonly string ORDER_NO = "order_no";
        public static readonly string ORDER_AMOUNT = "order_amount";
        public static readonly string ORDER_TIME = "order_time";
        public static readonly string RETURN_PARAMS = "return_params";
        public static readonly string TRADE_NO = "trade_no";
        public static readonly string TRADE_TIME = "trade_time";
        public static readonly string TRADE_STATUS = "trade_status";
        public static readonly string SIGN = "sign";
        /// <summary>
        /// 
        /// </summary>
        public void ReturnBank()
        {
            string merchantCode = HttpContext.Current.Request.QueryString[MERCHANT_CODE];
            string notifyType = HttpContext.Current.Request.QueryString[NOTIFY_TYPE];
            string orderNo = HttpContext.Current.Request.QueryString[ORDER_NO];
            string orderAmount = HttpContext.Current.Request.QueryString[ORDER_AMOUNT];
            string orderTime = HttpContext.Current.Request.QueryString[ORDER_TIME];
            string returnParams = HttpContext.Current.Request.QueryString[RETURN_PARAMS];
            string tradeNo = HttpContext.Current.Request.QueryString[TRADE_NO];
            string tradeTime = HttpContext.Current.Request.QueryString[TRADE_TIME];
            string tradeStatus = HttpContext.Current.Request.QueryString[TRADE_STATUS];
            string sign = HttpContext.Current.Request.QueryString[SIGN];

            KeyValues kvs = new KeyValues();
            kvs.add(new KeyValue(MERCHANT_CODE, merchantCode));
            kvs.add(new KeyValue(NOTIFY_TYPE, notifyType));
            kvs.add(new KeyValue(ORDER_NO, orderNo));
            kvs.add(new KeyValue(ORDER_AMOUNT, orderAmount));
            kvs.add(new KeyValue(ORDER_TIME, orderTime));
            kvs.add(new KeyValue(RETURN_PARAMS, returnParams));
            kvs.add(new KeyValue(TRADE_NO, tradeNo));
            kvs.add(new KeyValue(TRADE_TIME, tradeTime));
            kvs.add(new KeyValue(TRADE_STATUS, tradeStatus));

            String _sign = kvs.sign(THConfig.merchantKey, THConfig.charset);
            string info = "֧��ʧ��";
            if (_sign == sign)
            {
                string opstate = "-1";
                int status = 4;
                if ("success" == tradeStatus)
                {
                    info = "֧���ɹ�";
                    opstate = "0";
                    status = 2;
                }
                OrderBankUtils.SuppPageReturn(SuppId
                    , orderNo
                    , tradeNo
                    , status
                    , opstate
                    , info
                    , decimal.Parse(orderAmount), 0M);

                //���success�ַ�����֧���ɹ�������±������룬��Ϊ����ƽ̨�ص��̻��ĺ�̨֪ͨ��ַ�󣬻�ͨ�����ص������а���success���б��̻��Ƿ��յ�֪ͨ�����ɹ���֪����ƽ̨��
                //���success�ַ���ֻ�����̻���̨֪ͨʱ������д��ҳ��֪ͨ�ɲ���д��

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
            string merchantCode = HttpContext.Current.Request.QueryString[MERCHANT_CODE];
            string notifyType = HttpContext.Current.Request.QueryString[NOTIFY_TYPE];
            string orderNo = HttpContext.Current.Request.QueryString[ORDER_NO];
            string orderAmount = HttpContext.Current.Request.QueryString[ORDER_AMOUNT];
            string orderTime = HttpContext.Current.Request.QueryString[ORDER_TIME];
            string returnParams = HttpContext.Current.Request.QueryString[RETURN_PARAMS];
            string tradeNo = HttpContext.Current.Request.QueryString[TRADE_NO];
            string tradeTime = HttpContext.Current.Request.QueryString[TRADE_TIME];
            string tradeStatus = HttpContext.Current.Request.QueryString[TRADE_STATUS];
            string sign = HttpContext.Current.Request.QueryString[SIGN];

            KeyValues kvs = new KeyValues();
            kvs.add(new KeyValue(MERCHANT_CODE, merchantCode));
            kvs.add(new KeyValue(NOTIFY_TYPE, notifyType));
            kvs.add(new KeyValue(ORDER_NO, orderNo));
            kvs.add(new KeyValue(ORDER_AMOUNT, orderAmount));
            kvs.add(new KeyValue(ORDER_TIME, orderTime));
            kvs.add(new KeyValue(RETURN_PARAMS, returnParams));
            kvs.add(new KeyValue(TRADE_NO, tradeNo));
            kvs.add(new KeyValue(TRADE_TIME, tradeTime));
            kvs.add(new KeyValue(TRADE_STATUS, tradeStatus));

            String _sign = kvs.sign(THConfig.merchantKey, THConfig.charset);
            if (_sign == sign)
            {
                string opstate = "-1";
                int status = 4;
                if ("success" == tradeStatus)
                {
                    opstate = "0";
                    status = 2;
                }

                OrderBankUtils.SuppNotify(SuppId
                  , orderNo
                  , tradeNo
                  , status
                  , opstate
                  , tradeStatus
                  , decimal.Parse(orderAmount), decimal.Parse(orderAmount)
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
                    code = "CMBC";  //��������
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
                    code = "CMBCS"; //�й���������
                    break;
                case "974":
                    code = "PINGAN"; //���ڷ�չ����
                    break;
                case "985":
                    code = "CGB"; //�㶫��չ����
                    break;
                case "962":
                    code = "ECITIC"; //��������
                    break;
                case "982":
                    code = "HXB"; //��������
                    break;
                case "972":
                    code = "CIB"; //��ҵ����
                    break;
                //case "984":
                //    code = "00011"; //����ũ����ҵ����
                //    break;
                //case "1015":
                //    code = "GZCB"; //��������
                //    break;
                //case "1016":
                //    code = "CUPS"; //�й�����
                //    break;
                //case "976":
                //    code = "00030"; //�Ϻ�ũ����ҵ����
                //    break;
                //case "971":
                //    code = "POST"; //�й�����
                //    break;
                case "989":
                    code = "BCCB"; //��������
                    break;
                //case "988":
                //    code = "CBHB"; //��������
                //    break;
                case "990":
                    code = "BRCB"; //����ũ������
                    break;
                case "979":
                    code = "00055"; //�Ͼ�����
                    break;
                case "986":
                    code = "CEBBANK"; //�й��������
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
                    code = "PINGAN"; //ƽ������
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
