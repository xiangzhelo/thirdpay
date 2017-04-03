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
    /// 身份验证码放在 user1上面
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
            string service = "create_direct_pay_by_user"; // 服务名称：即时交易（快捷）
            string partner = SuppAccount; // 合作者商户ID
            string input_charset = Config.Input_charset; // 字符集
            string sign_type = Config.Sign_type; // 签名算法
            string notify_url = notifyUrl; // 服务器异步通知页面路径
            string return_url = returnurl; // 页面跳转同步通知页面路径
            string error_notify_url = ""; // 请求出错时的通知页面路径，可空

            // 反钓鱼用参数
            string anti_phishing_key = AskForTimestamp.askFor(); // 通过时间戳查询接口（见AskForTimestamp），获取的加密易八通系统时间戳
            string exter_invoke_ip = ServerVariables.TrueIP; // 用户在外部系统创建交易时，由外部系统记录的用户IP地址

            // 以下为业务参数
            //string out_trade_no = "xxx-123456-noaa"; 

            // 易八通合作商户网站唯一订单号
            string out_trade_no = orderid;

            string subject = orderid; // 商品名称
            string payment_type = "1"; // 支付类型，默认值为：1（商品购买）

            /**
             * ”卖家易八通用户ID“优先于”卖家易八通用户名“
             * 两者不可同时为空
             */
            string seller_email = ""; // 卖家易八通用户名
            string seller_id = SuppAccount; // 卖家易八通用户ID

            string buyer_email = ""; // 买家易八通用户名，可空
            string buyer_id = ""; // 买家易八通用户ID，可空

            string price = ""; // 商品单价
            string total_fee = decimal.Round(orderAmt, 2).ToString(); // 交易金额
            string quantity = ""; // 购买数量

            string body = ""; // 商品描述，可空
            string show_url = ""; // 商品展示网址，可空
            string pay_method = "bankPay"; // 支付方式，directPay(余额支付)、bankPay(网银支付)，可空
            string default_bank = GetBankCode(bankCode);                                    // 默认网银 ,快捷支付必填
            /**
             ABC_B2C=农行
             BJRCB_B2C=北京农村商业银行
             BOC_B2C=中国银行
             CCB_B2C=建行
             CEBBANK_B2C=中国光大银行
             CGB_B2C=广东发展银行
             CITIC_B2C=中信银行
             CMB_B2C=招商银行
             CMBC_B2C=中国民生银行
             COMM_B2C=交通银行
             FDB_B2C=富滇银行
             HXB_B2C=华夏银行
             HZCB_B2C_B2C=杭州银行
             ICBC_B2C=工商银行网
             NBBANK_B2C=宁波银行
             PINGAN_B2C=平安银行
             POSTGC_B2C=中国邮政储蓄银行
             SDB_B2C=深圳发展银行
             SHBANK_B2C=上海银行
             SPDB_B2C=上海浦东发展银行
             */
            string royalty_parameters = ""; // 最多10组分润明细。示例：100001=0.01|100002=0.02 表示id为100001的用户要分润0.01元，id为100002的用户要分润0.02元。
            string royalty_type = ""; // 提成类型，目前只支持一种类型：10，表示卖家给第三方提成；

            // 将参数排序
            // 构造排序数组
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
            // 进行排序
            string[] sortedParamArray = CommonHelper.BubbleSort(oriStr);

            string paramStr = CommonHelper.BuildParamString(sortedParamArray);

            SynsSummitLogger(paramStr + Config.Key);

            //对所有参数进行加签
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

            string gate_way = "https://www.ebatong.com/direct/gateway.htm"; // 交易网关
            string sHtmlText = CommonHelper.BuildRequest(sParaTemp, "post", "确认", gate_way, autosumit);
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
            string sign_ebatong = System.Web.HttpContext.Current.Request.QueryString["sign"]; // 取出签名

            // 从Request中得到的原始请求参数字符串，按参数名自然排序；注：不包括sign
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

            //去除最后一个“&”
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
                string trade_status = System.Web.HttpContext.Current.Request.QueryString["trade_status"];//交易状态
                if ("TRADE_FINISHED" == trade_status)
                {  //交易成功
                    //判断该笔订单是否在商户网站中已经做过处理
                    //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    //如果有做过处理，不执行商户的业务程序

                    _info = "支付成功";
                    opstate = "0";
                    status = 2;
                    tranAmt = Convert.ToDecimal(total_fee);

                }
                else
                {    //支付失败 订单为待处理，可继续支付

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
            string sign_ebatong = System.Web.HttpContext.Current.Request.QueryString["sign"]; // 取出签名
            string notify_id = System.Web.HttpContext.Current.Request.QueryString["notify_id"];


            // 从Request中得到的原始请求参数字符串，按参数名自然排序；注：不包括sign
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

            //去除最后一个“&”
            int nLen = oriParamStr.Length;
            oriParamStr = oriParamStr.Remove(nLen - 1, 1);

            string sign = CommonHelper.md5(Config.Input_charset, oriParamStr + Config.Key);
            sign = sign.ToLower();

            string opstate = "-1";
            int status = 4;
            decimal tranAmt = 0M;
            string _info = string.Empty;

            //
            if (sign.Equals(sign_ebatong))//验证成功默认交易成功通知
            {
                string out_trade_no = System.Web.HttpContext.Current.Request.QueryString["out_trade_no"];
                string trade_no = System.Web.HttpContext.Current.Request.QueryString["trade_no"];
                string total_fee = System.Web.HttpContext.Current.Request.QueryString["total_fee"];
                string trade_status = System.Web.HttpContext.Current.Request.QueryString["trade_status"];//交易状态

                if ("TRADE_FINISHED" == trade_status)
                {
                    _info = "支付成功";
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

                //签名一致，回传通知ID
                //商户进行后续处理
                System.Web.HttpContext.Current.Response.Write(notify_id);
            }

        }
        #endregion

        #region GetBankCode
        /// <summary>
        ///  ABC_B2C=农行
        //BJRCB_B2C=北京农村商业银行
        //BOC_B2C=中国银行
        //CCB_B2C=建行
        //CEBBANK_B2C=中国光大银行
        //CGB_B2C=广东发展银行
        //CITIC_B2C=中信银行
        //CMB_B2C=招商银行
        //CMBC_B2C=中国民生银行
        //COMM_B2C=交通银行
        //FDB_B2C=富滇银行
        //HXB_B2C=华夏银行
        //HZCB_B2C_B2C=杭州银行
        //ICBC_B2C=工商银行网
        //NBBANK_B2C=宁波银行
        //PINGAN_B2C=平安银行
        //POSTGC_B2C=中国邮政储蓄银行
        //SDB_B2C=深圳发展银行
        //SHBANK_B2C=上海银行
        //SPDB_B2C=上海浦东发展银行
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
                    code = "CMB_B2C";  //招商银行
                    break;
                case "967":
                    code = "ICBC_B2C"; //中国工商银行
                    break;
                case "964":
                    code = "ABC_B2C"; //中国农业银行
                    break;
                case "965":
                    code = "CCB_B2C"; //中国建设银行
                    break;
                case "963":
                    code = "BOCSH_B2C"; //中国银行
                    break;
                case "977":
                    code = "SPDB_B2C"; //浦发银行
                    break;
                case "981":
                    code = "COMM_B2C"; //中国交通银行
                    break;
                case "980":
                    code = "CMBCD_B2C"; //中国民生银行
                    break;
                case "974":
                    code = "SDB_B2C"; //深圳发展银行
                    break;
                case "985":
                    code = "GDB_B2C"; //广东发展银行
                    break;
                case "962":
                    code = "CNCB_B2C"; //中信银行
                    break;
                case "982":
                    code = "HXB_B2C"; //华夏银行
                    break;
                case "972":
                    code = "CIB_B2C"; //兴业银行
                    break;
                case "984":
                    code = "GZCB_B2C"; //广州农村商业银行
                    break;
                case "1015":
                    code = "GZCB_B2C"; //广州银行
                    break;

                case "976":
                    code = "SRCB_B2C"; //上海农村商业银行
                    break;
                case "989":
                    code = "BOB_B2C"; //北京银行
                    break;
                case "988":
                    code = "CBHB_B2C"; //渤海银行
                    break;
                case "990":
                    code = "BJRCB_B2C"; //北京农商银行
                    break;
                case "979":
                    code = "BON_B2C"; //南京银行
                    break;
                case "986":
                    code = "CEB_B2C"; //中国光大银行
                    break;
                case "987":
                    code = "BEA_B2C"; //东亚银行
                    break;
                case "1025":
                    code = "NBCB_B2C"; //宁波银行
                    break;
                case "983":
                    code = "HZCB_B2C"; //杭州银行
                    break;
                case "978":
                    code = "PINGAN_B2C"; //平安银行
                    break;
                case "1028":
                    code = "HSB_B2C"; //徽商银行
                    break;
                case "968":
                    code = "CZB_B2C"; //浙商银行
                    break;
                case "975":
                    code = "BOS_B2C"; //上海银行
                    break;
                case "971":
                    code = "POSTGC_B2C"; //中国邮政储蓄银行
                    break;
                //case "1032":
                //    code = "UPOP"; //银联在线支付
                //    break;

                //WZCB_B2C 温州银行
            }
            return code;
        }

        #endregion
    }
}
