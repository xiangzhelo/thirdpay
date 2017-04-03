using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;

namespace viviapi.ETAPI.Card51
{
    /// <summary>
    /// 51Card΢�Žӿ�
    /// </summary>
    public class WeiXin : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Card51;

        public WeiXin()
            : base(SuppId)
        {

        }

        public static WeiXin Instance
        {
            get
            {
                var instance = new WeiXin();
                return instance;
            }
        }


        internal string Returnurl { get { return this.SiteDomain + "/return/card51/weixin.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/card51/weixin.aspx"; } }


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
            string form_url = "http://www.51card.cn/gateway/weixin/weixinpay.asp";
            //�̻���
            string customerid = SuppAccount;
            string Mer_key = SuppKey;
            //�̻��������
            string sdcustomno = orderid;
            //�������
            string ordermoney = decimal.Round(orderAmt * 100, 0).ToString();
            string cardno = "32";

            string noticeurl = NotifyUrl;
            string backurl = Returnurl;
            string mark = "chongzhi";

            string plain = "customerid={0}&sdcustomno={1}&orderAmount={2}&cardno={3}&noticeurl={4}&backurl={5}{6}";
            plain = string.Format(plain, customerid, sdcustomno, ordermoney, cardno, noticeurl, backurl, Mer_key);
            SynsSummitLogger("plain: " + plain);
            string sign = viviLib.Security.Cryptography.MD5(plain).ToUpper();
            SynsSummitLogger("SignMD5: " + sign);
            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"" + form_url + "\">";
            postForm += "<input type=\"hidden\" name=\"customerid\" value=\"" + customerid + "\" />";
            postForm += "<input type=\"hidden\" name=\"sdcustomno\" value=\"" + sdcustomno + "\" />";
            postForm += "<input type=\"hidden\" name=\"orderAmount\" value=\"" + ordermoney + "\" />";
            postForm += "<input type=\"hidden\" name=\"cardno\" value=\"" + cardno + "\" />";

            postForm += "<input type=\"hidden\" name=\"noticeurl\" value=\"" + noticeurl + "\" />";
            postForm += "<input type=\"hidden\" name=\"backurl\" value=\"" + backurl + "\" />";

            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />";
            postForm += "<input type=\"hidden\" name=\"mark\" value=\"" + mark + "\" />";
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
                OrderBankUtils.SuppPageReturn(
                    SuppId
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
                  , des
                  , decimal.Parse(ordermoney), decimal.Parse(ordermoney)
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
