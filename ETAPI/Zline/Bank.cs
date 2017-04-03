using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;
using Demo.Class;
using System.Collections.Specialized;
using System.IO;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.Zline
{
    /// <summary>
    /// �����ӿ�
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Zline;

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


        internal string Returnurl { get { return this.SiteDomain + "/return/zline/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/zline/bank.aspx"; } }
        //internal string NotifyUrl { get { return "http://pay.leshouka.com/receive/zline/bank.aspx"; } }
        //�践�ظ��ӿڵ���Ϣ
        internal string Succflag = "{'code':'00'}";
        internal string Failflag = "{'code':'01'}";

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
            //        <form id="form1" method="post" action="https://payment.kklpay.com/ebank/pay.do">
            //    <input type="text" name="merchantCode" style=" display:none;" value="1000000183"/>
            //    <input type="text" name="outOrderId" style=" display:none;" value="123123321123yhg"/>
            //    <input type="text" name="totalAmount" style=" display:none;" value="1"/>
            //    <input type="text" name="goodsName" style=" display:none;" value="goodsName"/>
            //    <input type="text" name="goodsExplain" style=" display:none;" value="goodsExplain"/>
            //    <input type="text" name="orderCreateTime" style=" display:none;" value="20150210213410"/>
            //    <input type="text" name="lastPayTime" style=" display:none;" value=""/>
            //    <input type="text" name="merUrl" style=" display:none;" value="http://www.baidu.com/Demo/Rec.aspx"/>
            //    <input type="text" name="noticeUrl" style=" display:none;" value="http://www.baidu.com/Demo/SynNotice.aspx"/>
            //    <input type="text" name="bankCode" style=" display:none;" value="BOC"/>
            //    <input type="text" name="bankCardType" style=" display:none;" value="00"/>
            //<input type="text" name="sign"  style=" display:none;" value="3C72DF74572665A22EA0E190E65869EA"/>
            //</form>
            //<script type="text/javascript"> document.getElementById("form1").submit();</script>


            //�ύ��ַ
            string form_url = PostBankUrl;
            //�̻���
            string Mer_code = SuppAccount;
            string Mer_key = SuppKey;
            //�̻��������
            string Billno = orderid;
            //�������(��)
            string Amount = decimal.Round(orderAmt * 100, 0).ToString();

            string keyValue = string.Empty;
            string key = string.Empty;
            string sign = string.Empty;
            string signValue = string.Empty;
            RSAOperate Rdaop = new RSAOperate(SuppId);
            NameValueCollection collection = new NameValueCollection();
            collection.Add("merchantCode", Mer_code);
            collection.Add("outOrderId", Billno);
            collection.Add("totalAmount", Amount);
            collection.Add("orderCreateTime", DateTime.Now.ToString("yyyyMMddhhmmss"));
            collection.Add("lastPayTime", DateTime.Now.ToString("yyyyMMddhhmmss"));
            collection.Add("merUrl", Returnurl);
            collection.Add("noticeUrl", NotifyUrl);
            collection.Add("bankCode", GetBankCode(bankcode));
            collection.Add("bankCardType", "00");
            collection.Add("goodsName", "goodsname");
            collection.Add("goodsExplain", "explain");
            signValue = Rdaop.GetUrlParamString(collection, RSASign.GetPayRSAParamSort());
            sign = RSASign.GetMD5RSA(signValue +"&KEY="+ SuppKey);
            collection.Add("sign", sign);
            SynsSummitLogger("plain: " + signValue);
            SynsSummitLogger("SignMD5: " + sign);

            string postForm = "<form name=\"form1\" id=\"form1\" method=\"post\" action=\"" + PostBankUrl + "\">";

            for (int i = 0; i < RSASign.GetPayParamSort().Length; i++)
            {
                key = RSASign.GetPayParamSort()[i];
                if (key == "merchantCode")
                    keyValue = Mer_code;
                else
                    keyValue = collection[RSASign.GetPayParamSort()[i]];

                postForm += "<input type=\"text\" name=\"" + key + "\" style=\" display:none;\" value=\"" + keyValue + "\"/>";

            }

            postForm += "<input type=\"text\" name=\"sign\"  style=\" display:none;\" value=\"" + sign + "\"/></form>";


            if (autoSubmit == true)
            {
                //�Զ��ύ�ñ�����������
                postForm +=
                    "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('form1').submit();\",100);</script>";
            }

            SynsSummitLogger("SignMD5: " + postForm);
            return postForm;
        }
        #endregion

        #region ReturnBank
        /// <summary>
        /// ͬ��������Ϣ
        /// </summary>
        public void ReturnBank()
        {
            //<form action="http://pay.wengpay.com/return/zline/bank.aspx" id="form1" method="post">

            //<input type="hidden" name="sign" id="sign" value="CF83653CA3C0CC2DFD79C58E0C718C9D" />//ǩ��

            //<input type="hidden" name="result" id="result" value="" />

            //<input type="hidden" name="transType" id="transType" value="00200" />//��������

            //<input type="hidden" name="instructCode" id="instructCode" value="11001455960" />//���׶�����

            //<input type="hidden" name="waitTime" id="waitTime" value="" />

            //<input type="hidden" name="autoJump" id="autoJump" value="1" />

            //<input type="hidden" name="transTime" id="transTime" value="20151007221549" />//����ʱ��

            //<input type="hidden" name="totalAmount" id="totalAmount" value="100" />//���ѽ��

            //<input type="hidden" name="merchantCode" id="merchantCode" value="1000000183" />//�̻���

            //<input type="hidden" name="outOrderId" id="outOrderId" value="B4687724001572971251" />//�̻�����
            try
            {
                RSAOperate Rdaop = new RSAOperate(SuppId);

                NameValueCollection collection = new NameValueCollection();
                collection.Add("sign", HttpContext.Current.Request.Form["sign"]);
                collection.Add("transType", HttpContext.Current.Request.Form["transType"]);
                collection.Add("instructCode", HttpContext.Current.Request.Form["instructCode"]);
                collection.Add("transTime", HttpContext.Current.Request.Form["transTime"]);
                collection.Add("totalAmount", HttpContext.Current.Request.Form["totalAmount"]);
                collection.Add("merchantCode", HttpContext.Current.Request.Form["merchantCode"]);
                collection.Add("outOrderId", HttpContext.Current.Request.Form["outOrderId"]);
                string sign = HttpContext.Current.Request.Form["sign"];
                string RSAChar = Rdaop.GetUrlParamString(collection, Demo.Class.RSASign.GetNoticeRSAParamSort()) + "&KEY=" + SuppKey;
                if (sign == RSASign.GetMD5RSA(RSAChar))//�ж��Ƿ��ļ��ܺ��ܹ�ƥ��
                {

                    string billno = HttpContext.Current.Request.Form["outOrderId"];
                    //����������
                    string zlineBillno = HttpContext.Current.Request.Form["instructCode"];
                    //���ѽ��
                    string amount = HttpContext.Current.Request.Form["totalAmount"];//
                    string msg = "";
                    string opstate = "-1";
                    int status = 4;
                    //�жϽ����Ƿ�ɹ�
                    if (Convert.ToDecimal(amount) > 0)
                    {
                        opstate = "0";
                        status = 2;
                    }

                    OrderBankUtils.SuppPageReturn(SuppId
                               , billno
                               , zlineBillno
                               , status
                               , opstate
                               , string.Empty
                               , decimal.Parse(amount) / 100m, 0M);

                }
                else
                {

                    HttpContext.Current.Response.Write("���ر��ļ�����Ϣ�����쳣");//00��ʾ�Ѿ��յ�����
                }

            }
            catch (Exception eh)
            {
                HttpContext.Current.Response.Write("�����쳣" + eh.Message);//00��ʾ�Ѿ��յ�����
            }
        }
        #endregion
        protected Object lockobject = new Object();
        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            try
            {
                RSAOperate Rdaop = new RSAOperate(SuppId);
                //�������������
                Stream responseStream = HttpContext.Current.Request.InputStream;
                StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                string RequestStream = readStream.ReadToEnd();
                readStream.Close();
                if (!string.IsNullOrEmpty(RequestStream))
                {
                    string RSAChar = Rdaop.GetUrlParamString(RequestStream, Demo.Class.RSASign.GetNoticeRSAParamSort()) + "&KEY=" + SuppKey;
                    if (Rdaop.GetIsSafty(Rdaop.GetjosnValue(RequestStream, "sign"), RSAChar))//�ж��Ƿ��ļ��ܺ��ܹ�ƥ��
                    {
                        lock (lockobject)//�˴�����ʹ��lock�����ƣ����в������ƣ���ֹ�ظ����ݻ���
                        {
                            //�˴����û��Լ����з��ص�����Ϣ���д���
                            //������Rdaop.GetjosnValue(RequestStream, "sign")����   ��ȡ��Ӧ������Ϣ�����е�����Ϣ�ڿͻ��˷���������
                            //todo:������Ϣ���ش����
                            string billno = Rdaop.GetjosnValue(RequestStream, "outOrderId");
                            string zlineBillno = Rdaop.GetjosnValue(RequestStream, "instructCode");
                            string amount = Rdaop.GetjosnValue(RequestStream, "totalAmount");//
                            string msg = "";
                            string opstate = "-1";
                            int status = 4;
                            //�жϽ����Ƿ�ɹ�
                            if (Convert.ToDecimal(amount) > 0)
                            {
                                opstate = "0";
                                status = 2;
                            }
                            OrderBankUtils.SuppNotify(SuppId
                              , billno
                              , zlineBillno
                              , status
                              , opstate
                              , msg
                              , decimal.Parse(amount) / 100m
                              , 0M
                              , Succflag
                              , Failflag);
                        }
                    }
                    else
                    {

                        HttpContext.Current.Response.Write("{\"code\":\"00\",\"msg\":\"���ر��ļ�����Ϣ�����쳣\"}");//00��ʾ�Ѿ��յ�����
                    }
                }
                else
                {
                    HttpContext.Current.Response.Write("{\"code\":\"00\",\"msg\":\"���ر���Ϊ�մ����쳣\"}");//00��ʾ�Ѿ��յ�����
                }
            }
            catch (Exception eh)
            {
                HttpContext.Current.Response.Write("{\"code\":\"00\",\"msg\":\"�����쳣\"}");//00��ʾ�Ѿ��յ�����
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
                    code = "BCM"; //�й���ͨ����
                    break;
                case "980":
                    code = "CMBC"; //�й���������
                    break;
                case "974":
                    code = "PAB"; //���ڷ�չ����
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
                    code = "GDB"; //����ũ����ҵ����
                    break;

                case "971":
                    code = "PCSB"; //�й�����
                    break;
                case "989":
                    code = "BCCB"; //��������
                    break;

                //case "990":
                //    code = "00056"; //����ũ������
                //    break;
                //case "979":
                //    code = "00055"; //�Ͼ�����
                //    break;
                case "986":
                    code = "CEB"; //�й��������
                    break;

                //case "983":
                //    code = "00081"; //��������
                //    break;
                case "978":
                    code = "PAB"; //ƽ������
                    break;

                //case "968":
                //    code = ""; //��������
                //    break;
                case "975":
                    code = "BOS"; //�Ϻ�����
                    break;
                default:
                    code = "BOC";
                    break;
            }
            return code;
        }
        #endregion

        public string ReJson = string.Empty;
        public string ReJsonKeyValue = string.Empty;
        public string ReJsonArrayValue = string.Empty;
        #region OrderQuery
        /// <summary>
        /// https://paygate.baofoo.com/Check/OrderQuery.aspx
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public string OrderQuery(string orderid)
        {

            string responseText = "";
            try
            {
                Demo.Class.RSAOperate Rdaop = new Demo.Class.RSAOperate(SuppId);
                string postDataRSA = string.Format("merchantCode={0}&outOrderId={1}&KEY={2}", SuppAccount, orderid, SuppKey);//�����������Ҫǩ���ı��Ĵ�
                postDataRSA = Demo.Class.RSASign.GetMD5RSA(postDataRSA);//���ܺ�RSA��Ϣ
                NameValueCollection collection = new NameValueCollection();
                collection.Add("outOrderId", orderid);
                collection.Add("merchantCode", SuppAccount);
                collection.Add("sign", postDataRSA);
                string postData = Rdaop.GetPostJson(collection, Demo.Class.RSASign.GetQueryParamSort(), postDataRSA);//���ش��䱨�Ĵ�
                ReJson = Rdaop.GetPostWeb(Demo.Class.ProperConst.queryUrl, postData);//�˲���ƴдParam������֤���ݣ��������sign��������������֤��ȫ��
                //if (Rdaop.GetjosnValue(ReJson, "code") == "00")
                //{
                //    string RSAChar = Rdaop.GetParamJosnString(ReJson, Demo.Class.RSASign.GetQueryRetuenMD5RSAParamSort()) + Demo.Class.ProperConst.Key;
                //    if (Rdaop.GetIsSafty(Rdaop.GetjosnValue(ReJson, "sign"), RSAChar))
                //    {
                //        //String[] RsaArray = Demo.Class.RSASign.GetQueryReturnParamSort();
                //        //for (int i = 1; i < Demo.Class.RSASign.GetQueryReturnParamSort().Length - 1; i++)//���鳤��-1���ų�sign�ֶ���ʾ
                //        //{
                //        //    ReJsonKeyValue = Rdaop.GetjosnValue(ReJson, RsaArray[i].ToString());
                //        //    ReJsonArrayValue = RsaArray[i].ToString();
                //        //}
                //        responseText = ReJson;
                //    }
                //    else
                //    {
                //        HttpContext.Current.Response.Write("���ر��İ�ȫ��֤ʧ��");
                //    }
                //}
                //else
                //{
                //    HttpContext.Current.Response.Write("<strong color=\"red\">" + Rdaop.GetjosnValue(ReJson, "msg") + "</strong>");
                //}
                return responseText;

            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }

            return string.Empty;
        }
        #endregion
    }
}
