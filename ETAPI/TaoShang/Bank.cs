using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.Payment;
using viviapi.BLL.Payment;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.Model;
using viviapi.SysInterface.Bank;
using viviLib.Web;
using viviLib.Logging;

namespace viviapi.ETAPI.TaoShang
{
    /// <summary>
    /// ��Ѷ�ӿ�
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.TaoShang;

        public Bank()
            : base(SuppId)
        {

        }

        internal string Returnurl { get { return this.SiteDomain + "/return/taoshang/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/taoshang/bank.aspx"; } }

        #region PayBank
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid">������</param>
        /// <param name="money">��ֵ</param>       
        /// <returns></returns>
        public string PayBank(string orderid, decimal orderAmt, string BankID)
        {
            string payUrl = PostBankUrl;
            if (string.IsNullOrEmpty(payUrl))
                return string.Empty;

            orderAmt = Decimal.Round(orderAmt, 2);

            string plain = string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", SuppAccount, BankID, orderAmt, orderid, NotifyUrl) + SuppKey;
            string sign = viviLib.Security.Cryptography.MD5(plain, "GB2312");

            return string.Format("{0}?parter={1}&type={2}&value={3}&orderid={4}&callbackurl={5}&hrefbackurl={6}&sign={7}"
                , payUrl
                , SuppAccount
                , BankID
                , orderAmt
                , orderid
                , NotifyUrl
                , Returnurl
                , sign);
        }
        #endregion

        #region GetPayForm
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="BankID"></param>
        /// <param name="autoSubmit"></param>
        /// <returns></returns>
        public string GetPayForm(string orderid, decimal orderAmt, string BankID, bool autoSubmit)
        {
            string payUrl = PostBankUrl;
            if (string.IsNullOrEmpty(payUrl))
                return string.Empty;

            orderAmt = Decimal.Round(orderAmt, 2);

            string plain = string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", SuppAccount, BankID, orderAmt, orderid, NotifyUrl) + SuppKey;
            string sign = viviLib.Security.Cryptography.MD5(plain, "GB2312");

            //return string.Format("{0}?parter={1}&type={2}&value={3}&orderid={4}&callbackurl={5}&hrefbackurl={6}&sign={7}"
            //    , payUrl
            //    , SuppAccount
            //    , BankID
            //    , orderAmt
            //    , orderid
            //    , NotifyUrl
            //    , Returnurl
            //    , sign);

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"get\" target='_blank' action=\"" + payUrl + "\"> \n";
            postForm += "<input type=\"hidden\" name=\"parter\" value=\"" + SuppAccount + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"type\" value=\"" + BankID + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"value\" value=\"" + orderAmt + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"orderid\" value=\"" + orderid + "\" /> \n";

            postForm += "<input type=\"hidden\" name=\"callbackurl\" value=\"" + NotifyUrl + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"hrefbackurl\" value=\"" + Returnurl + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" /> \n";
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

        #region ReturnBank
        /// <summary>
        /// 
        /// </summary>
        public void ReturnBank()
        {
            HttpRequest req = HttpContext.Current.Request;

            string _orderid = req.QueryString["orderid"];
            string _opstate = req.QueryString["opstate"];
            string _ovalue = req.QueryString["ovalue"];
            string _sign = req.QueryString["sign"];
            string sysorderid = req.QueryString["sysorderid"];
            string systime = req.QueryString["systime"];

            string plain = string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", _orderid, _opstate, _ovalue, SuppKey);
            String localSign = viviLib.Security.Cryptography.MD5(plain);

            try
            {
                if (localSign == _sign)
                {
                    string opstate = "-1";
                    int status = 4;
                    decimal realAmt = 0M;
                    if (_opstate.ToLower() == "0")
                    {
                        opstate = "0";
                        status = 2;
                        realAmt = decimal.Parse(_ovalue);
                    }

                    OrderBankUtils.SuppPageReturn(SuppId
                        , _orderid
                        , sysorderid
                        , status
                        , opstate
                        , ""
                        , realAmt, 0M);

                    //BankUtils bll = new BankUtils();
                    //bll.DoBankComplete(suppId, _orderid, _ekaorderid, status, opstate, string.Empty, decimal.Parse(_ovalue), 0M, false, true);
                    //HttpContext.Current.Response.Write("opstate=0");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }
        }
        #endregion

        String GetErrorInfo(String ErrorCode)
        {
            String info = ErrorCode;
            switch (ErrorCode)
            {
                case "-1":
                    info = "ϵͳæ";
                    break;
                case "1":
                    info = "�̻���������Ч";
                    break;
                case "2":
                    info = "���б������";
                    break;
                case "3":
                    info = "�̻�������";
                    break;
                case "4":
                    info = "��֤ǩ��ʧ��";
                    break;
                case "5":
                    info = "�̻���ֵ�ر�";
                    break;
                case "6":
                    info = "�����޶�";
                    break;
            }
            return info;
        }

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            HttpRequest req = HttpContext.Current.Request;

            string _orderid = req.QueryString["orderid"];
            string _opstate = req.QueryString["opstate"];
            string _ovalue = req.QueryString["ovalue"];
            string _sign = req.QueryString["sign"];
            string sysorderid = req.QueryString["sysorderid"];
            string systime = req.QueryString["systime"];

            string plain = string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", _orderid, _opstate, _ovalue, SuppKey);
            String localSign = viviLib.Security.Cryptography.MD5(plain);

            try
            {
                if (localSign == _sign)
                {
                    string opstate = "-1";
                    int status = 4;
                    decimal realAmt = 0M;
                    if (_opstate.ToLower() == "0")
                    {
                        opstate = "0";
                        status = 2;
                        realAmt = decimal.Parse(_ovalue);
                    }
                    OrderBankUtils.SuppNotify(SuppId
             , _orderid
             , sysorderid
             , status
             , opstate
             , ""
             , realAmt
             , 0M
             , "opstate=0"
             , "opstate=-1");
                    //BankUtils bll = new BankUtils();
                    //bll.DoBankComplete(suppId, _orderid, _ekaorderid, status, opstate, string.Empty, decimal.Parse(_ovalue), 0M, true, false);
                    HttpContext.Current.Response.Write("opstate=0");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }
        }
        #endregion

        #region GetBankCode
        /// <summary>
        /// WZCB	��������
        /// HKBCHINA	��������
        /// ZHNX	�麣��ũ�����ú�������
        /// SDE	˳��ũ����
        /// YDXH	Ң�����ú�������
        /// CZCB	�㽭������ҵ����
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
                    code = "SDB"; //���ڷ�չ����
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
                case "1015":
                    code = "GZCB"; //��������
                    break;
                //case "1016":
                //    code = "CUPS"; //�й�����
                //    break;
                case "976":
                    code = "SHRCB"; //�Ϻ�ũ����ҵ����
                    break;
                //case "971":
                //    code = "POST"; //�й�����
                //    break;
                case "989":
                    code = "BCCB"; //��������
                    break;
                case "988":
                    code = "CBHB"; //��������
                    break;
                case "990":
                    code = "BJRCB"; //����ũ������
                    break;
                case "979":
                    code = "NJCB"; //�Ͼ�����
                    break;
                case "986":
                    code = "CEB"; //�й��������
                    break;
                case "987":
                    code = "HKBEA"; //��������
                    break;
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
                //    code = "1000"; //��������
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
                default:
                    code = "ICBC";
                    break;
            }
            return code;
        }
        #endregion
    }
}
