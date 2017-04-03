using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using viviapi.ETAPI.Alipay.Lib;
using viviapi.ETAPI.Common;
using viviapi.ETAPI.YeePay.Lib.com.yeepay.cmbn;
using viviapi.Model.Finance;
using viviapi.Model.supplier;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.Alipay
{
    /// <summary>
    /// 
    /// </summary>
    public class BatchPay : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Alipay;

        internal string NotifyUrl { get { return this.SiteDomain + "/receive/alipay/batchpay.aspx"; } }

        public BatchPay()
            : base(SuppId)
        {

        }

        #region PayReq
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool PayReq(WithdrawSuppTranLog info)
        {
            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //服务器异步通知页面路径
            string notify_url = NotifyUrl;
            //需http://格式的完整路径，不允许加?id=123这类自定义参数

            //付款账号
            string email = SuppUserName;
            //必填

            //付款账户名
            string account_name = SuppInfo.puserid1;
            //必填，个人支付宝账号是真实姓名公司支付宝账号是公司名称

            //付款当天日期
            string pay_date = DateTime.Now.ToString("yyyyMMdd");
            //必填，格式：年[4位]月[2位]日[2位]，如：20100801

            //批次号
            string batch_no = info.trade_no;
            //必填，格式：当天日期[8位]+序列号[3至16位]，如：201008010000001

            //付款总金额
            string batch_fee = info.amount.ToString("f2");
            //必填，即参数detail_data的值中所有金额的总和

            //付款笔数
            string batch_num = "1";
            //必填，即参数detail_data的值中，“|”字符出现的数量加1，最大支持1000笔（即“|”字符出现的数量999个）


            //付款详细数据
            string detail_data = string.Format("{0}^{1}^{2}^{3:f2}^7uka"
                , info.trade_no
                , info.bankAccount
                , info.bankAccountName
                , info.amount);
            //必填，格式：流水号1^收款方帐号1^真实姓名^付款金额1^备注说明1|流水号2^收款方帐号2^真实姓名^付款金额2^备注说明2....


            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", "batch_trans_notify");
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("email", email);
            sParaTemp.Add("account_name", account_name);
            sParaTemp.Add("pay_date", pay_date);
            sParaTemp.Add("batch_no", batch_no);
            sParaTemp.Add("batch_fee", batch_fee);
            sParaTemp.Add("batch_num", batch_num);
            sParaTemp.Add("detail_data", detail_data);

            //建立请求
            string retText = Submit.BuildRequest(sParaTemp);


            info.supp_message = retText;
            LogWrite("retText=>" + retText);

            return retText.Contains("批量付款提交成功");
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
                SortedDictionary<string, string> sPara = GetRequestPost();

                if (sPara.Count > 0) //判断是否有带返回参数
                {
                    Notify aliNotify = new Notify();

                    bool verifyResult = aliNotify.Verify(sPara
                        , HttpContext.Current.Request.Form["notify_id"]
                        , HttpContext.Current.Request.Form["sign"]);

                    if (verifyResult) //验证成功
                    {
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //请在这里加上商户的业务逻辑程序代码


                        //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                        //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                        //批量付款数据中转账成功的详细信息

                        string batchNo = HttpContext.Current.Request.Form["batch_no"];

                        string success_details = HttpContext.Current.Request.Form["success_details"];

                        //批量付款数据中转账失败的详细信息
                        string fail_details = HttpContext.Current.Request.Form["fail_details"];

                        string[] success_arr = success_details.Split('|');
                        foreach (string item in success_arr)
                        {
                            string[] arr = item.Split('^');
                            int code = Withdrawal.Complete(SuppId
                                , arr[0]
                                , false
                                , 2
                                , arr[3]
                                , batchNo
                                , "AlipayBatchPayAPI");
                        }

                        string[] fail_arr = fail_details.Split('|');
                        foreach (string item in fail_arr)
                        {
                            string[] arr = item.Split('^');
                            int code = Withdrawal.Complete(SuppId
                                , arr[0]
                                , false
                                , 4
                                , "0"
                                , batchNo
                                , "AlipayBatchPayAPI");
                        }

                        //判断是否在商户网站中已经做过了这次通知返回的处理
                        //如果没有做过处理，那么执行商户的业务程序
                        //如果有做过处理，那么不执行商户的业务程序

                        HttpContext.Current.Response.Write("success"); //请不要修改或删除

                        //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }
                    else //验证失败
                    {
                        HttpContext.Current.Response.Write("fail");
                    }
                }
                else
                {
                    HttpContext.Current.Response.Write("无通知参数");
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        #endregion

        #region GetRequestPost
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = HttpContext.Current.Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
            }

            return sArray;
        }
        #endregion

    }
}
