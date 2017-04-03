using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.ETAPI.Ebatong.Lib;
using viviapi.Model.Payment;
using viviapi.BLL.Payment;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.Model;
using viviLib.Web;
using viviLib.Logging;

namespace viviapi.ETAPI.Ebatong
{
    /// <summary>
    /// �����֤����� user1����
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Ebatong;

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


        internal string returnurl { get { return this.SiteDomain + "/return/ebatong/bank.aspx"; } }
        internal string notifyUrl { get { return this.SiteDomain + "/receive/ebatong/bank.aspx"; } }

        #region PayBank
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankCode"></param>
        /// <returns></returns>
        public string PayBank(string orderid, decimal orderAmt, string bankCode, bool autosumit)
        {
            string service = "create_direct_pay_by_user"; // �������ƣ���ʱ���ף���ݣ�
            string partner = SuppAccount; // �������̻�ID
            string input_charset = Config.Input_charset; // �ַ���
            string sign_type = Config.Sign_type; // ǩ���㷨
            string notify_url = notifyUrl; // �������첽֪ͨҳ��·��
            string return_url = returnurl; // ҳ����תͬ��֪ͨҳ��·��
            string error_notify_url = ""; // �������ʱ��֪ͨҳ��·�����ɿ�

            // �������ò���
            string anti_phishing_key = AskForTimestamp.askFor(); // ͨ��ʱ�����ѯ�ӿڣ���AskForTimestamp������ȡ�ļ����װ�ͨϵͳʱ���
            string exter_invoke_ip = ServerVariables.TrueIP; // �û����ⲿϵͳ��������ʱ�����ⲿϵͳ��¼���û�IP��ַ

            // ����Ϊҵ�����
            //string out_trade_no = "xxx-123456-noaa"; 

            // �װ�ͨ�����̻���վΨһ������
            string out_trade_no = orderid;

            string subject = orderid; // ��Ʒ����
            string payment_type = "1"; // ֧�����ͣ�Ĭ��ֵΪ��1����Ʒ����

            /**
             * �������װ�ͨ�û�ID�������ڡ������װ�ͨ�û�����
             * ���߲���ͬʱΪ��
             */
            string seller_email = ""; // �����װ�ͨ�û���
            string seller_id = SuppAccount; // �����װ�ͨ�û�ID

            string buyer_email = ""; // ����װ�ͨ�û������ɿ�
            string buyer_id = ""; // ����װ�ͨ�û�ID���ɿ�

            string price = ""; // ��Ʒ����
            string total_fee = decimal.Round(orderAmt, 2).ToString(); // ���׽��
            string quantity = ""; // ��������

            string body = ""; // ��Ʒ�������ɿ�
            string show_url = ""; // ��Ʒչʾ��ַ���ɿ�
            string pay_method = "bankPay"; // ֧����ʽ��directPay(���֧��)��bankPay(����֧��)���ɿ�
            string default_bank = GetBankCode(bankCode);                                    // Ĭ������ ,���֧������
            /**
             ABC_B2C=ũ��
             BJRCB_B2C=����ũ����ҵ����
             BOC_B2C=�й�����
             CCB_B2C=����
             CEBBANK_B2C=�й��������
             CGB_B2C=�㶫��չ����
             CITIC_B2C=��������
             CMB_B2C=��������
             CMBC_B2C=�й���������
             COMM_B2C=��ͨ����
             FDB_B2C=��������
             HXB_B2C=��������
             HZCB_B2C_B2C=��������
             ICBC_B2C=����������
             NBBANK_B2C=��������
             PINGAN_B2C=ƽ������
             POSTGC_B2C=�й�������������
             SDB_B2C=���ڷ�չ����
             SHBANK_B2C=�Ϻ�����
             SPDB_B2C=�Ϻ��ֶ���չ����
             */
            string royalty_parameters = ""; // ���10�������ϸ��ʾ����100001=0.01|100002=0.02 ��ʾidΪ100001���û�Ҫ����0.01Ԫ��idΪ100002���û�Ҫ����0.02Ԫ��
            string royalty_type = ""; // ������ͣ�Ŀǰֻ֧��һ�����ͣ�10����ʾ���Ҹ���������ɣ�

            // ����������
            // ������������
            string[] oriStr = { 
                          "service=" + service,
                          "partner=" + partner,
                          "input_charset=" + input_charset,
                          "sign_type=" + sign_type,
                          "notify_url=" + notify_url,
                          "return_url=" + return_url,
                          "error_notify_url=" + error_notify_url,
                          "anti_phishing_key=" + anti_phishing_key,
                          "exter_invoke_ip=" + exter_invoke_ip,
                          "out_trade_no=" + out_trade_no,
                          "subject=" + subject,
                          "payment_type=" + payment_type,
                          "seller_email=" + seller_email,
                          "seller_id=" + seller_id,
                          "buyer_email=" + buyer_email,
                          "buyer_id=" + buyer_id,
                          "price=" + price,
                          "total_fee=" + total_fee,
                          "quantity=" + quantity,
                          "body=" + body,
                          "show_url=" + show_url,
                          "pay_method=" + pay_method,
                          "default_bank=" + default_bank,
                          "royalty_parameters=" + royalty_parameters,
                          "royalty_type=" + royalty_type
                          };
            // ��������
            string[] sortedParamArray = CommonHelper.BubbleSort(oriStr);

            string paramStr = CommonHelper.BuildParamString(sortedParamArray);

            SynsSummitLogger(paramStr + Config.Key);

            //�����в������м�ǩ
            string sign = CommonHelper.md5(input_charset, paramStr + Config.Key).ToLower();

            SynsSummitLogger(sign);

            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", partner);
            sParaTemp.Add("input_charset", input_charset);
            sParaTemp.Add("service", service);
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("seller_email", seller_email);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);
            sParaTemp.Add("error_notify_url", error_notify_url);
            sParaTemp.Add("seller_id", seller_id);
            sParaTemp.Add("buyer_email", buyer_email);
            sParaTemp.Add("buyer_id", buyer_id);
            sParaTemp.Add("price", price);
            sParaTemp.Add("quantity", quantity);
            sParaTemp.Add("pay_method", pay_method);
            sParaTemp.Add("default_bank", default_bank);
            sParaTemp.Add("royalty_parameters", royalty_parameters);
            sParaTemp.Add("royalty_type", royalty_type);
            sParaTemp.Add("sign", sign);
            sParaTemp.Add("sign_type", sign_type);

            string gate_way = "https://www.ebatong.com/direct/gateway.htm"; // ��������
            string sHtmlText = CommonHelper.BuildRequest(sParaTemp, "post", "ȷ��", gate_way, autosumit);
            SynsSummitLogger(sHtmlText);
            return sHtmlText;
        }
        #endregion

        #region ReturnBank
        /// <summary>
        /// 
        /// </summary>
        public void ReturnBank()
        {
            string sign_ebatong = System.Web.HttpContext.Current.Request.QueryString["sign"]; // ȡ��ǩ��

            // ��Request�еõ���ԭʼ��������ַ���������������Ȼ����ע��������sign
            int i = 0;
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = System.Web.HttpContext.Current.Request.QueryString;
            string[] requestItem = coll.AllKeys;
            string[] sortedParamArray = CommonHelper.BubbleSort(requestItem);
            string oriParamStr = "";
            for (i = 0; i < requestItem.Length; i++)
            {
                if (sortedParamArray[i] == "sign") { continue; }
                oriParamStr += sortedParamArray[i] + "=" + System.Web.HttpContext.Current.Request.QueryString[sortedParamArray[i]] + "&";
            }

            //ȥ�����һ����&��
            int nLen = oriParamStr.Length;
            oriParamStr = oriParamStr.Remove(nLen - 1, 1);

            string sign = CommonHelper.md5(Config.Input_charset, oriParamStr + Config.Key);
            sign = sign.ToLower();
            //
            if (sign.Equals(sign_ebatong))
            {
                string opstate = "-1";
                int status = 4;
                decimal tranAmt = 0M;

                string _info = string.Empty;

                string out_trade_no = System.Web.HttpContext.Current.Request.QueryString["out_trade_no"];
                string trade_no = System.Web.HttpContext.Current.Request.QueryString["trade_no"];
                string total_fee = System.Web.HttpContext.Current.Request.QueryString["total_fee"];
                string trade_status = System.Web.HttpContext.Current.Request.QueryString["trade_status"];//����״̬
                if ("TRADE_FINISHED" == trade_status)
                {  //���׳ɹ�
                    //�жϸñʶ����Ƿ����̻���վ���Ѿ���������
                    //���û�������������ݶ����ţ�out_trade_no�����̻���վ�Ķ���ϵͳ�в鵽�ñʶ�������ϸ����ִ���̻���ҵ�����
                    //���������������ִ���̻���ҵ�����

                    _info = "֧���ɹ�";
                    opstate = "0";
                    status = 2;
                    tranAmt = Convert.ToDecimal(total_fee);

                }
                else
                {    //֧��ʧ�� ����Ϊ�������ɼ���֧��

                    _info = trade_status;
                }

                OrderBankUtils.SuppPageReturn(SuppId
                                     , out_trade_no
                                     , trade_no
                                     , status
                                     , opstate
                                     , _info
                                     , tranAmt, 0M);

            }
        }
        #endregion

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            string sign_ebatong = System.Web.HttpContext.Current.Request.QueryString["sign"]; // ȡ��ǩ��
            string notify_id = System.Web.HttpContext.Current.Request.QueryString["notify_id"];


            // ��Request�еõ���ԭʼ��������ַ���������������Ȼ����ע��������sign
            int i = 0;
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = System.Web.HttpContext.Current.Request.QueryString;
            string[] requestItem = coll.AllKeys;
            string[] sortedParamArray = CommonHelper.BubbleSort(requestItem);
            string oriParamStr = "";
            for (i = 0; i < requestItem.Length; i++)
            {
                if (sortedParamArray[i] == "sign") { continue; }
                oriParamStr += sortedParamArray[i] + "=" + System.Web.HttpContext.Current.Request.QueryString[sortedParamArray[i]] + "&";
            }

            //ȥ�����һ����&��
            int nLen = oriParamStr.Length;
            oriParamStr = oriParamStr.Remove(nLen - 1, 1);

            string sign = CommonHelper.md5(Config.Input_charset, oriParamStr + Config.Key);
            sign = sign.ToLower();

            string opstate = "-1";
            int status = 4;
            decimal tranAmt = 0M;
            string _info = string.Empty;

            //
            if (sign.Equals(sign_ebatong))//��֤�ɹ�Ĭ�Ͻ��׳ɹ�֪ͨ
            {
                string out_trade_no = System.Web.HttpContext.Current.Request.QueryString["out_trade_no"];
                string trade_no = System.Web.HttpContext.Current.Request.QueryString["trade_no"];
                string total_fee = System.Web.HttpContext.Current.Request.QueryString["total_fee"];
                string trade_status = System.Web.HttpContext.Current.Request.QueryString["trade_status"];//����״̬

                if ("TRADE_FINISHED" == trade_status)
                {
                    _info = "֧���ɹ�";
                    opstate = "0";
                    status = 2;
                    tranAmt = Convert.ToDecimal(total_fee);
                }
               
                OrderBankUtils.SuppNotify(SuppId
                                      , out_trade_no
                                      , trade_no
                                      , status
                                      , opstate
                                      , string.Empty
                                      , tranAmt,0M
                                      , "" 
                                      , "fail");

                //ǩ��һ�£��ش�֪ͨID
                //�̻����к�������
                System.Web.HttpContext.Current.Response.Write(notify_id);
            }

        }
        #endregion

        #region GetBankCode
        /// <summary>
        ///  ABC_B2C=ũ��
        //BJRCB_B2C=����ũ����ҵ����
        //BOC_B2C=�й�����
        //CCB_B2C=����
        //CEBBANK_B2C=�й��������
        //CGB_B2C=�㶫��չ����
        //CITIC_B2C=��������
        //CMB_B2C=��������
        //CMBC_B2C=�й���������
        //COMM_B2C=��ͨ����
        //FDB_B2C=��������
        //HXB_B2C=��������
        //HZCB_B2C_B2C=��������
        //ICBC_B2C=����������
        //NBBANK_B2C=��������
        //PINGAN_B2C=ƽ������
        //POSTGC_B2C=�й�������������
        //SDB_B2C=���ڷ�չ����
        //SHBANK_B2C=�Ϻ�����
        //SPDB_B2C=�Ϻ��ֶ���չ����
        /// </summary>
        /// 
        /// FDB_B2C
        /// <param name="paymodeId"></param>
        /// <returns></returns>
        public string GetBankCode(string paymodeId)
        {
            string code = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    code = "CMB_B2C";  //��������
                    break;
                case "967":
                    code = "ICBC_B2C"; //�й���������
                    break;
                case "964":
                    code = "ABC_B2C"; //�й�ũҵ����
                    break;
                case "965":
                    code = "CCB_B2C"; //�й���������
                    break;
                case "963":
                    code = "BOCSH_B2C"; //�й�����
                    break;
                case "977":
                    code = "SPDB_B2C"; //�ַ�����
                    break;
                case "981":
                    code = "COMM_B2C"; //�й���ͨ����
                    break;
                case "980":
                    code = "CMBCD_B2C"; //�й���������
                    break;
                case "974":
                    code = "SDB_B2C"; //���ڷ�չ����
                    break;
                case "985":
                    code = "GDB_B2C"; //�㶫��չ����
                    break;
                case "962":
                    code = "CNCB_B2C"; //��������
                    break;
                case "982":
                    code = "HXB_B2C"; //��������
                    break;
                case "972":
                    code = "CIB_B2C"; //��ҵ����
                    break;
                case "984":
                    code = "GZCB_B2C"; //����ũ����ҵ����
                    break;
                case "1015":
                    code = "GZCB_B2C"; //��������
                    break;

                case "976":
                    code = "SRCB_B2C"; //�Ϻ�ũ����ҵ����
                    break;
                case "989":
                    code = "BOB_B2C"; //��������
                    break;
                case "988":
                    code = "CBHB_B2C"; //��������
                    break;
                case "990":
                    code = "BJRCB_B2C"; //����ũ������
                    break;
                case "979":
                    code = "BON_B2C"; //�Ͼ�����
                    break;
                case "986":
                    code = "CEB_B2C"; //�й��������
                    break;
                case "987":
                    code = "BEA_B2C"; //��������
                    break;
                case "1025":
                    code = "NBCB_B2C"; //��������
                    break;
                case "983":
                    code = "HZCB_B2C"; //��������
                    break;
                case "978":
                    code = "PINGAN_B2C"; //ƽ������
                    break;
                case "1028":
                    code = "HSB_B2C"; //��������
                    break;
                case "968":
                    code = "CZB_B2C"; //��������
                    break;
                case "975":
                    code = "BOS_B2C"; //�Ϻ�����
                    break;
                case "971":
                    code = "POSTGC_B2C"; //�й�������������
                    break;
                //case "1032":
                //    code = "UPOP"; //��������֧��
                //    break;

                //WZCB_B2C ��������
            }
            return code;
        }

        #endregion
    }
}
