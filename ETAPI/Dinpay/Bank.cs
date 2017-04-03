using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
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

namespace viviapi.ETAPI.Dinpay
{
    /// <summary>
    /// ��Ѷ�ӿ�
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Dinpay;

        public Bank()
            : base(SuppId)
        {

        }

        internal string Returnurl { get { return this.SiteDomain + "/return/dinpay/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/dinpay/bank.aspx"; } }

        #region PayBank
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid">������</param>
        /// <param name="orderAmt"></param>
        /// <param name="bankcode"></param>
        /// <param name="autoSubmit"></param>
        /// <returns></returns>
        public string PayBank(string orderid, decimal orderAmt, string bankcode, bool autoSubmit)
        {
            string service_type = "direct_pay";
            string merchant_code = SuppAccount;
            string input_charset = "GB2312";
            string notify_url = NotifyUrl;
            string return_url = Returnurl;
            string client_ip = ServerVariables.TrueIP;
            string interface_version = "V3.0";
            string sign_type = "MD5";

            string order_no = orderid;
            string order_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string order_amount = orderAmt.ToString("f2");
            string product_name = orderid; //��Ʒ����
            string show_url = "";//��ƷչʾURL
            string product_code = "";//��Ʒ���
            string product_num = "1";//��Ʒ����
            string product_desc = ""; //��Ʒ����
            string bank_code = GetBankCode(bankcode);
            string extra_return_param = "";
            string extend_param = "";


            string signSrc = "";
            #region
            //��֯������Ϣ
            if (bank_code != "")
            {
                signSrc = signSrc + "bank_code=" + bank_code + "&";
            }
            if (client_ip != "")
            {
                signSrc = signSrc + "client_ip=" + client_ip + "&";
            }
            if (extend_param != "")
            {
                signSrc = signSrc + "extend_param=" + extend_param + "&";
            }
            if (extra_return_param != "")
            {
                signSrc = signSrc + "extra_return_param=" + extra_return_param + "&";
            }
            if (input_charset != "")
            {
                signSrc = signSrc + "input_charset=" + input_charset + "&";
            }
            if (interface_version != "")
            {
                signSrc = signSrc + "interface_version=" + interface_version + "&";
            }
            if (merchant_code != "")
            {
                signSrc = signSrc + "merchant_code=" + merchant_code + "&";
            }
            if (notify_url != "")
            {
                signSrc = signSrc + "notify_url=" + notify_url + "&";
            }
            if (order_amount != "")
            {
                signSrc = signSrc + "order_amount=" + order_amount + "&";
            }
            if (order_no != "")
            {
                signSrc = signSrc + "order_no=" + order_no + "&";
            }
            if (order_time != "")
            {
                signSrc = signSrc + "order_time=" + order_time + "&";
            }
            if (product_code != "")
            {
                signSrc = signSrc + "product_code=" + product_code + "&";
            }
            if (product_desc != "")
            {
                signSrc = signSrc + "product_desc=" + product_desc + "&";
            }
            if (product_name != "")
            {
                signSrc = signSrc + "product_name=" + product_name + "&";
            }
            if (product_num != "")
            {
                signSrc = signSrc + "product_num=" + product_num + "&";
            }
            if (return_url != "")
            {
                signSrc = signSrc + "return_url=" + return_url + "&";
            }
            if (service_type != "")
            {
                signSrc = signSrc + "service_type=" + service_type + "&";
            }
            if (show_url != "")
            {
                signSrc = signSrc + "show_url=" + show_url + "&";
            }
            signSrc = signSrc + "key=" + SuppKey;
            #endregion
            string singInfo = signSrc;
            string sign = viviLib.Security.Cryptography.MD5(singInfo, input_charset);

            string gateway = "https://pay.dinpay.com/gateway?input_charset=" + input_charset;

            if (!string.IsNullOrEmpty(PostBankUrl))
            {
                gateway = PostBankUrl;

                if (PostBankUrl.IndexOf("?input_charset", StringComparison.Ordinal) < 0)
                {
                    gateway += "?input_charset=" + input_charset;
                }
            }

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + gateway + "\">\n";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"merchant_code\" value=\"" + merchant_code + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"bank_code\" value=\"" + bank_code + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"order_no\" value=\"" + order_no + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"order_amount\" value=\"" + order_amount + "\" />\n";

            postForm += "<input type=\"hidden\" name=\"service_type\" value=\"" + service_type + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"input_charset\" value=\"" + input_charset + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"notify_url\" value=\"" + notify_url + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"interface_version\" value=\"" + interface_version + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"sign_type\" value=\"" + sign_type + "\" />\n";

            postForm += "<input type=\"hidden\" name=\"order_time\" value=\"" + order_time + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"product_name\" value=\"" + product_name + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"client_ip\" value=\"" + client_ip + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"extend_param\" value=\"" + extend_param + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"extra_return_param\" value=\"" + extra_return_param + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"product_code\" value=\"" + product_code + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"product_desc\" value=\"" + product_desc + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"product_num\" value=\"" + product_num + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"return_url\" value=\"" + return_url + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"show_url\" value=\"" + show_url + "\" />\n";
            postForm += "</form>";

            if (autoSubmit)
            {
                postForm +=
                    "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            }

            return postForm;
        }
        #endregion

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            try
            {

                //��ȡ�Ǹ�GET����������Ϣ
                //�̺ź�
                string merchant_code = GetValue("merchant_code");
                //֪ͨ����
                string notify_type = GetValue("notify_type");
                //֪ͨУ��ID
                string notify_id = GetValue("notify_id");
                //�ӿڰ汾
                string interface_version = GetValue("interface_version");
                //ǩ����ʽ
                string sign_type = GetValue("sign_type");
                //ǩ��
                string dinpaySign = GetValue("sign");
                //�̼Ҷ�����
                string order_no = GetValue("order_no");
                //�̼Ҷ���ʱ��
                string order_time = GetValue("order_time");
                //�̼Ҷ������
                string order_amount = GetValue("order_amount");
                //�ش�����
                string extra_return_param = GetValue("extra_return_param");
                //�Ǹ����׶�����
                string trade_no = GetValue("trade_no");
                //�Ǹ�����ʱ��
                string trade_time = GetValue("trade_time");
                //����״̬ SUCCESS �ɹ�  FAILED ʧ��
                string trade_status = GetValue("trade_status");
                //���н�����ˮ��
                string bank_seq_no = GetValue("bank_seq_no");
                /**
                 *ǩ��˳���ղ�����a��z��˳��������������ͬ����ĸ���򿴵ڶ�����ĸ���Դ����ƣ�
                *ͬʱ���̼�֧����Կkey����������ǩ������ɹ������£�
                *������1=����ֵ1&������2=����ֵ2&����&������n=����ֵn&key=keyֵ
                **/
                //��֯������Ϣ
                string signStr = "";

                if (bank_seq_no != "")
                {
                    signStr = signStr + "bank_seq_no=" + bank_seq_no + "&";
                }

                if (!string.IsNullOrEmpty(extra_return_param))
                {
                    signStr = signStr + "extra_return_param=" + extra_return_param + "&";
                }
                signStr = signStr + "interface_version=V3.0" + "&";
                signStr = signStr + "merchant_code=" + merchant_code + "&";


                if (notify_id != "")
                {
                    signStr = signStr + "notify_id=" + notify_id + "&notify_type=" + notify_type + "&";
                }

                signStr = signStr + "order_amount=" + order_amount + "&";
                signStr = signStr + "order_no=" + order_no + "&";
                signStr = signStr + "order_time=" + order_time + "&";
                signStr = signStr + "trade_no=" + trade_no + "&";
                signStr = signStr + "trade_status=" + trade_status + "&";

                if (trade_time != "")
                {
                    signStr = signStr + "trade_time=" + trade_time + "&";
                }

                string key = SuppKey;

                signStr = signStr + "key=" + key;
                string signInfo = signStr;

                //����װ�õ���ϢMD5ǩ��
                string sign = viviLib.Security.Cryptography.MD5(signInfo).ToLower(); //ע����֧��ǩ����ͬ  �˴���String���м���

                bool page_notify = (notify_type == "page_notify");
                //�Ƚ��Ǹ����ص�ǩ�������̼������װ��ǩ�����Ƿ�һ��

                if (dinpaySign == sign)
                {
                    //��ǩ�ɹ�   
                    /**
		
                    �˴������̻�ҵ�����
		
                    ҵ�����
                    */

                    //bank_seq_no=201404184666177223&interface_version=V3.0&merchant_code=2060010306&notify_id=edc44e7cf77c47d38c26cb823dc05667&notify_type=offline_notify&order_amount=1&order_no=14041817245151020625&order_time=2014-04-18 17:25:09&trade_no=1002006070&trade_status=SUCCESS&trade_time=2014-04-18 17:25:05&key=www_longpay_13128888806





                    int status = 4;
                    string opstate = "1";
                    decimal tranAMT = 0M;
                    if (trade_status == "SUCCESS")
                    {
                        status = 2;
                        opstate = "0";
                        tranAMT = decimal.Parse(order_amount);
                    }


                    OrderBankUtils.SuppNotify(SuppId
                                     , order_no
                                     , trade_no
                                     , status
                                     , opstate
                                     , string.Empty
                                     , tranAMT
                                     , 0M

                                     , "SUCCESS"
                                     , "Fail");


                }
                else
                {
                    //��ǩʧ�� ҵ�����
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
        string GetValue(string key)
        {
            string val = HttpContext.Current.Request.Form[key];

            if (string.IsNullOrEmpty(val))
            {
                val = HttpContext.Current.Request.QueryString[key];
            }
            if (!string.IsNullOrEmpty(val))
            {
                return val.Trim();
            }
            return "";
        }
        #endregion

        #region ReturnBank
        /// <summary>
        /// 
        /// </summary>
        public void ReturnBank()
        {
            //��ȡ�Ǹ�GET����������Ϣ
            //�̺ź�
            string merchant_code = GetValue("merchant_code");

            //֪ͨ����
            string notify_type = GetValue("notify_type");

            //֪ͨУ��ID
            string notify_id = GetValue("notify_id");

            //�ӿڰ汾
            string interface_version = GetValue("interface_version");

            //ǩ����ʽ
            string sign_type = GetValue("sign_type");

            //ǩ��
            string dinpaySign = GetValue("sign");

            //�̼Ҷ�����
            string order_no = GetValue("order_no");

            //�̼Ҷ���ʱ��
            string order_time = GetValue("order_time");

            //�̼Ҷ������
            string order_amount = GetValue("order_amount");

            //�ش�����
            string extra_return_param = GetValue("extra_return_param");

            //�Ǹ����׶�����
            string trade_no = GetValue("trade_no");

            //�Ǹ�����ʱ��
            string trade_time = GetValue("trade_time");

            //����״̬ SUCCESS �ɹ�  FAILED ʧ��
            string trade_status = GetValue("trade_status");

            //���н�����ˮ��
            string bank_seq_no = GetValue("bank_seq_no");

            /**
             *ǩ��˳���ղ�����a��z��˳��������������ͬ����ĸ���򿴵ڶ�����ĸ���Դ����ƣ�
            *ͬʱ���̼�֧����Կkey����������ǩ������ɹ������£�
            *������1=����ֵ1&������2=����ֵ2&����&������n=����ֵn&key=keyֵ
            **/


            //��֯������Ϣ
            string signStr = "";

            if (bank_seq_no != "")
            {
                signStr = signStr + "bank_seq_no=" + bank_seq_no + "&";
            }

            if (extra_return_param != "")
            {
                signStr = signStr + "extra_return_param=" + extra_return_param + "&";
            }
            signStr = signStr + "interface_version=V3.0" + "&";
            signStr = signStr + "merchant_code=" + merchant_code + "&";


            if (notify_id != "")
            {
                signStr = signStr + "notify_id=" + notify_id + "&notify_type=" + notify_type + "&";
            }

            signStr = signStr + "order_amount=" + order_amount + "&";
            signStr = signStr + "order_no=" + order_no + "&";
            signStr = signStr + "order_time=" + order_time + "&";
            signStr = signStr + "trade_no=" + trade_no + "&";
            signStr = signStr + "trade_status=" + trade_status + "&";

            if (trade_time != "")
            {
                signStr = signStr + "trade_time=" + trade_time + "&";
            }

            string key = SuppKey;

            signStr = signStr + "key=" + key;
            string signInfo = signStr;

            //����װ�õ���ϢMD5ǩ��
            string sign = viviLib.Security.Cryptography.MD5(signInfo).ToLower(); //ע����֧��ǩ����ͬ  �˴���String���м���

            //�Ƚ��Ǹ����ص�ǩ�������̼������װ��ǩ�����Ƿ�һ��
            if (dinpaySign == sign)
            {
                //��ǩ�ɹ�   
                /**
		
                �˴������̻�ҵ�����
		
                ҵ�����
                */
                int status = 4;
                string opstate = "1";
                decimal tranAMT = 0M;

                if (trade_status == "SUCCESS")
                {
                    status = 2;
                    opstate = "0";
                    tranAMT = decimal.Parse(order_amount);
                }

                OrderBankUtils.SuppPageReturn(SuppId
                                       , order_no
                                       , trade_no
                                       , status
                                       , opstate
                                       , string.Empty
                                       , tranAMT
                                       , 0M);
            }
            else
            {
                //��ǩʧ�� ҵ�����
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
                    code = "BCOM"; //�й���ͨ����
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
                    code = "ECITIC"; //��������
                    break;
                case "982":
                    code = "HXB"; //��������
                    break;
                case "972":
                    code = "CIB"; //��ҵ����
                    break;
                case "971":
                    code = "PSBC"; //�й�����
                    break;
                case "986":
                    code = "CEBB"; //�й��������
                    break;
                case "987":
                    code = "BEA"; //��������
                    break;
                case "978":
                    code = "SPABANK"; //ƽ������
                    break;
            }
            return code;
        }
        #endregion
    }
}
