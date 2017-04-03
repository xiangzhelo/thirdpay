using System;
using System.Collections;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.ETAPI.Tenpay.lib;
using viviapi.Model.supplier;

namespace viviapi.ETAPI.WeiXin
{
    /// <summary>
    /// 
    /// </summary>
    public class WeiXinRMB : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Tenpay;

        public WeiXinRMB()
            : base(SuppId)
        {

        }

        public static WeiXinRMB Default
        {
            get
            {
                var instance = new WeiXinRMB();
                return instance;
            }
        }

        internal string return_url { get { return this.SiteDomain + "/return/tenpay/result.aspx"; } }
        internal string notify_url { get { return this.SiteDomain + "/receive/tenpay/notify.aspx"; } }
        internal string Succflag = "success";
        internal string Failflag = "fail";

        public static string getRealIp()
        {
            string UserIP;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) //得到穿过代理服务器的ip地址
            {
                UserIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                UserIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return UserIP;
        }

        public string GetPayForm(string out_trade_no, decimal amount, string bankcode, bool autosumit, HttpContext Context)
        {
            //商户号
            string partner = SuppAccount;
            //密钥
            string key = SuppKey;

            //当前时间 yyyyMMdd
            string date = DateTime.Now.ToString("yyyyMMdd");
            //订单号，此处用时间和随机数生成，商户根据自己调整，保证唯一
            //string out_trade_no = "" + DateTime.Now.ToString("HHmmss") + TenpayUtil.BuildRandomStr(4);

            string _bank_type = GetBankCode(bankcode);

            //创建RequestHandler实例
            RequestHandler reqHandler = new RequestHandler(Context);
            //初始化
            reqHandler.init();
            //设置密钥
            reqHandler.setKey(key);

            string gateUrl = this.PostBankUrl;
            if (_bank_type == "DEFAULT")
            {
                gateUrl = "https://gw.tenpay.com/gateway/pay.htm";
            }

            if (!string.IsNullOrEmpty(this.SuppInfo.jumpUrl))
            {
                gateUrl = this.SuppInfo.jumpUrl + "/switch/tenpay.aspx";
            }

            reqHandler.setGateUrl(gateUrl);//this.postBankUrl"https://gw.tenpay.com/gateway/pay.htm"


            //-----------------------------
            //设置支付参数
            //-----------------------------
            reqHandler.setParameter("partner", partner);		        //商户号
            reqHandler.setParameter("out_trade_no", out_trade_no);		//商家订单号
            reqHandler.setParameter("total_fee", decimal.Round(amount * 100M, 0).ToString());			        //商品金额,以分为单位
            reqHandler.setParameter("return_url", return_url);		    //交易完成后跳转的URL
            reqHandler.setParameter("notify_url", notify_url);		    //接收财付通通知的URL
            reqHandler.setParameter("body", "商城储值");	                    //商品描述


            //viviLib.Logging.LogHelper.Write(_bank_type);

            reqHandler.setParameter("bank_type", _bank_type);		    //银行类型(中介担保时此参数无效)
            reqHandler.setParameter("spbill_create_ip", Context.Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP
            reqHandler.setParameter("fee_type", "1");                    //币种，1人民币
            reqHandler.setParameter("subject", "goodname");              //商品名称(中介交易时必填)


            //系统可选参数
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("service_version", "1.0");
            reqHandler.setParameter("input_charset", "GBK");
            reqHandler.setParameter("sign_key_index", "1");

            //业务可选参数
            reqHandler.setParameter("attach", "");                      //附加数据，原样返回
            reqHandler.setParameter("product_fee", "0");                 //商品费用，必须保证transport_fee + product_fee=total_fee
            reqHandler.setParameter("transport_fee", "0");               //物流费用，必须保证transport_fee + product_fee=total_fee
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));            //订单生成时间，格式为yyyymmddhhmmss
            reqHandler.setParameter("time_expire", "");                 //订单失效时间，格式为yyyymmddhhmmss
            reqHandler.setParameter("buyer_id", "");                    //买方财付通账号
            reqHandler.setParameter("goods_tag", "");                   //商品标记
            reqHandler.setParameter("trade_mode", "1");                 //交易模式，1即时到账(默认)，2中介担保，3后台选择（买家进支付中心列表选择）
            reqHandler.setParameter("transport_desc", "");              //物流说明
            reqHandler.setParameter("trans_type", "1");                  //交易类型，1实物交易，2虚拟交易
            reqHandler.setParameter("agentid", "");                     //平台ID
            reqHandler.setParameter("agent_type", "");                  //代理模式，0无代理(默认)，1表示卡易售模式，2表示网店模式
            reqHandler.setParameter("seller_id", "");                   //卖家商户号，为空则等同于partner


            //获取请求带参数的url
            string requestUrl = reqHandler.getRequestURL();
            //viviLib.Logging.LogHelper.Write(requestUrl);
            //return requestUrl;

            ////Get的实现方式
            //string a_link = "<a target=\"_blank\" href=\"" + requestUrl + "\">" + "财付通支付" + "</a>";
            //Context.Response.Write(a_link);
            //return string.Empty;


            ////post实现方式

            StringBuilder postForm = new StringBuilder("<form id=\"frm1\" method=\"post\" action=\"" + reqHandler.getGateUrl() + "\" >");
            Hashtable ht = reqHandler.getAllParameters();
            foreach (DictionaryEntry de in ht)
            {
                postForm.Append("<input type=\"hidden\" name=\"" + de.Key + "\" value=\"" + de.Value + "\" >");
            }
            postForm.Append("</form>");

            if (autosumit)
            {
                postForm.Append(
                    "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>");
            }
            //viviLib.Logging.LogHelper.Write(postForm.ToString());
            return postForm.ToString();

            // Response.Write("<input type=\"submit\" value=\"财付通支付\" >\n</form>\n");*/


            ////获取debug信息,建议把请求和debug信息写入日志，方便定位问题
            //string debuginfo = reqHandler.getDebugInfo();
            //Response.Write("<br/>requestUrl:" + requestUrl + "<br/>");
            //Response.Write("<br/>debuginfo:" + debuginfo + "<br/>");
        }

        public string GetPayForm(string out_trade_no, decimal amount, string bankcode, HttpContext Context)
        {
            //商户号
            string partner = SuppAccount;
            //密钥
            string key = SuppKey;

            //当前时间 yyyyMMdd
            string date = DateTime.Now.ToString("yyyyMMdd");
            //订单号，此处用时间和随机数生成，商户根据自己调整，保证唯一
            //string out_trade_no = "" + DateTime.Now.ToString("HHmmss") + TenpayUtil.BuildRandomStr(4);


            //创建RequestHandler实例
            RequestHandler reqHandler = new RequestHandler(Context);
            //初始化
            reqHandler.init();
            //设置密钥
            reqHandler.setKey(key);
            reqHandler.setGateUrl("https://www.tenpay.com/cgi-bin/v1.0/pay_gate.cgi");

            //-----------------------------
            //设置支付参数
            //-----------------------------
            reqHandler.setParameter("partner", partner);		        //商户号
            reqHandler.setParameter("out_trade_no", out_trade_no);		//商家订单号
            reqHandler.setParameter("total_fee", decimal.Round(amount * 100M, 0).ToString());			        //商品金额,以分为单位
            reqHandler.setParameter("return_url", return_url);		    //交易完成后跳转的URL
            reqHandler.setParameter("notify_url", notify_url);		    //接收财付通通知的URL
            reqHandler.setParameter("body", "商城储值");	                    //商品描述
            reqHandler.setParameter("bank_type", "DEFAULT");		    //银行类型(中介担保时此参数无效)
            reqHandler.setParameter("spbill_create_ip", Context.Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP
            reqHandler.setParameter("fee_type", "1");                    //币种，1人民币
            reqHandler.setParameter("subject", "goodname");              //商品名称(中介交易时必填)


            //系统可选参数
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("service_version", "1.0");
            reqHandler.setParameter("input_charset", "GBK");
            reqHandler.setParameter("sign_key_index", "1");

            //业务可选参数
            reqHandler.setParameter("attach", "");                      //附加数据，原样返回
            reqHandler.setParameter("product_fee", "0");                 //商品费用，必须保证transport_fee + product_fee=total_fee
            reqHandler.setParameter("transport_fee", "0");               //物流费用，必须保证transport_fee + product_fee=total_fee
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));            //订单生成时间，格式为yyyymmddhhmmss
            reqHandler.setParameter("time_expire", "");                 //订单失效时间，格式为yyyymmddhhmmss
            reqHandler.setParameter("buyer_id", "");                    //买方财付通账号
            reqHandler.setParameter("goods_tag", "");                   //商品标记
            reqHandler.setParameter("trade_mode", "1");                 //交易模式，1即时到账(默认)，2中介担保，3后台选择（买家进支付中心列表选择）
            reqHandler.setParameter("transport_desc", "");              //物流说明
            reqHandler.setParameter("trans_type", "1");                  //交易类型，1实物交易，2虚拟交易
            reqHandler.setParameter("agentid", "");                     //平台ID
            reqHandler.setParameter("agent_type", "");                  //代理模式，0无代理(默认)，1表示卡易售模式，2表示网店模式
            reqHandler.setParameter("seller_id", "");                   //卖家商户号，为空则等同于partner


            //获取请求带参数的url
            string requestUrl = reqHandler.getRequestURL();
            //viviLib.Logging.LogHelper.Write(requestUrl);
            //return requestUrl;

            ////Get的实现方式
            //string a_link = "<a target=\"_blank\" href=\"" + requestUrl + "\">" + "财付通支付" + "</a>";
            //Context.Response.Write(a_link);
            //return string.Empty;


            ////post实现方式

            StringBuilder postForm = new StringBuilder("<form id=\"frm1\" method=\"post\" action=\"https://www.tenpay.com/cgi-bin/v1.0/pay_gate.cgi\" >");
            Hashtable ht = reqHandler.getAllParameters();
            foreach (DictionaryEntry de in ht)
            {
                postForm.Append("<input type=\"hidden\" name=\"" + de.Key + "\" value=\"" + de.Value + "\" >");
            }
            postForm.Append("</form>");
            //postForm.Append("<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>");
            return postForm.ToString();

            // Response.Write("<input type=\"submit\" value=\"财付通支付\" >\n</form>\n");*/


            ////获取debug信息,建议把请求和debug信息写入日志，方便定位问题
            //string debuginfo = reqHandler.getDebugInfo();
            //Response.Write("<br/>requestUrl:" + requestUrl + "<br/>");
            //Response.Write("<br/>debuginfo:" + debuginfo + "<br/>");
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="okxrorderid"></param>
        ///// <param name="amount"></param>
        ///// <param name="Context"></param>
        ///// <returns></returns>
        //public string GetPayUrl(string sysorderid, decimal amount, HttpContext Context)
        //{
        //    string puserid = suppAccount;
        //    string puserkey = suppKey;

        //    string str3 = DateTime.Now.ToString("yyyyMMdd");
        //    string str4 = "" + DateTime.Now.ToString("HHmmss") + tenpay.TenpayUtil.BuildRandomStr(4);

        //    string parameterValue = sysorderid;
        //    string str6 = puserid + str3 + str4;
        //    string str7 = notify_url;
        //    string str8 = ServerVariables.TrueIP;

        //    tenpay.PayRequestHandler handler = new tenpay.PayRequestHandler(Context);
        //    handler.setKey(puserkey);
        //    handler.init();
        //    handler.setParameter("bargainor_id", puserid);
        //    handler.setParameter("sp_billno", parameterValue);
        //    handler.setParameter("transaction_id", str6);
        //    handler.setParameter("return_url", str7);
        //    handler.setParameter("desc", "orderNo：" + str6);
        //    handler.setParameter("total_fee", Convert.ToString((double)(amount * 100.0M)));
        //    handler.setParameter("spbill_create_ip", getRealIp());
        //    string url = postBankUrl;
        //    if (string.IsNullOrEmpty(url))
        //    {
        //        url = "https://www.tenpay.com/cgi-bin/v1.0/pay_gate.cgi";
        //    }
        //    url = handler.getRequestURL().Replace("http://service.tenpay.com/cgi-bin/v3.0/payservice.cgi", url);
        //    viviLib.Logging.LogHelper.Write(url);
        //    return url;
        //}

        //#region Return
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="Context"></param>
        //public void Return(bool isnotify, HttpContext Context)
        //{
        //    string puserkey = suppKey;
        //    PayResponseHandler handler = new PayResponseHandler(Context);
        //    handler.setKey(puserkey);

        //    if (handler.isTenpaySign())
        //    {
        //        string transaction_id = handler.getParameter("transaction_id");
        //        string total_fee = handler.getParameter("total_fee");
        //        string pay_result = handler.getParameter("pay_result");
        //        string out_trade_no = handler.getParameter("sp_billno");

        //        string opstate = "-1";
        //        int status = 4;
        //        if (pay_result == "0")
        //        {
        //            status = 2;
        //            opstate = "0";
        //        }

        //        string debuginfo = handler.getDebugInfo();
        //        if (!string.IsNullOrEmpty(debuginfo))
        //        {
        //            LogHelper.Write(LogHelper.GetTenPayLogPath(), debuginfo);
        //        }
        //        viviapi.BLL.OrderBank bll = new viviapi.BLL.OrderBank();
        //        bll.DoBankComplete(suppId, out_trade_no, transaction_id, status, opstate, debuginfo, decimal.Parse(total_fee) / 100M, 0M, true,false);
        //    }
        //}
        //#endregion


        public void Return(HttpContext Context)
        {
            //密钥
            string key = SuppKey;

            //创建ResponseHandler实例
            ResponseHandler resHandler = new ResponseHandler(Context);
            resHandler.setKey(key);

            //判断签名
            if (resHandler.isTenpaySign())
            {

                ///通知id
                string notify_id = resHandler.getParameter("notify_id");
                //商户订单号
                string out_trade_no = resHandler.getParameter("out_trade_no");
                //财付通订单号
                string transaction_id = resHandler.getParameter("transaction_id");
                //金额,以分为单位
                string total_fee = resHandler.getParameter("total_fee");
                //如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
                string discount = resHandler.getParameter("discount");
                //支付结果
                string trade_state = resHandler.getParameter("trade_state");
                //交易模式，1即时到账，2中介担保
                string trade_mode = resHandler.getParameter("trade_mode");

                if ("1".Equals(trade_mode))
                {
                    string debuginfo = resHandler.getDebugInfo();
                    string _info = "支付失败" + debuginfo;
                    string opstate = "-1";
                    int status = 4;

                    //即时到账 
                    if ("0".Equals(trade_state))
                    {
                        _info = "支付成功";
                        status = 2;
                        opstate = "0";

                        //------------------------------
                        //即时到账处理业务开始
                        //------------------------------

                        //处理数据库逻辑
                        //注意交易单不要重复处理
                        //注意判断返回金额

                        //------------------------------
                        //即时到账处理业务完毕
                        //------------------------------

                        //Response.Write("即时到帐付款成功");
                        //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知

                    }
                    else
                    {
                        //Response.Write("即时到账支付失败");
                    }

                    //viviapi.BLL.OrderBank bll = new viviapi.BLL.OrderBank();
                    //bll.DoBankComplete(suppId, out_trade_no, transaction_id, status, opstate, _info, decimal.Parse(total_fee) / 100M, 0M, false, true);

                    decimal tranAmt = decimal.Parse(total_fee) / 100M;

                    OrderBankUtils.SuppPageReturn(SuppId
                    , out_trade_no
                    , transaction_id
                    , status
                    , opstate
                    , _info
                    , tranAmt, 0M);

                }
                else if ("2".Equals(trade_mode))
                {    //中介担保
                    if ("0".Equals(trade_state))
                    {
                        //------------------------------
                        //中介担保处理业务开始
                        //------------------------------

                        //处理数据库逻辑
                        //注意交易单不要重复处理
                        //注意判断返回金额

                        //------------------------------
                        //中介担保处理业务完毕
                        //------------------------------


                        //Response.Write("中介担保付款成功");
                        //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知

                    }
                    else
                    {
                        //Response.Write("trade_state=" + trade_state);
                    }
                }
            }
            else
            {
                //Response.Write("认证签名失败");
            }

            ////获取debug信息,建议把debug信息写入日志，方便定位问题
            //string debuginfo = resHandler.getDebugInfo();
            //Response.Write("<br/>debuginfo:" + debuginfo + "<br/>");
        }

        public void Notify(HttpContext Context)
        {
            //商户号
            string partner = SuppAccount;
            //密钥
            string key = SuppKey;
            //创建ResponseHandler实例
            ResponseHandler resHandler = new ResponseHandler(Context);
            resHandler.setKey(key);

            //判断签名
            if (resHandler.isTenpaySign())
            {
                ///通知id
                string notify_id = resHandler.getParameter("notify_id");
                //通过通知ID查询，确保通知来至财付通
                //创建查询请求
                RequestHandler queryReq = new RequestHandler(Context);
                queryReq.init();
                queryReq.setKey(key);
                queryReq.setGateUrl("https://gw.tenpay.com/gateway/verifynotifyid.xml");
                queryReq.setParameter("partner", partner);
                queryReq.setParameter("notify_id", notify_id);

                //通信对象
                TenpayHttpClient httpClient = new TenpayHttpClient();
                httpClient.setTimeOut(5);
                //设置请求内容
                httpClient.setReqContent(queryReq.getRequestURL());
                //后台调用
                if (httpClient.call())
                {
                    //设置结果参数
                    ClientResponseHandler queryRes = new ClientResponseHandler();
                    queryRes.setContent(httpClient.getResContent());
                    queryRes.setKey(key);
                    //判断签名及结果
                    //只有签名正确,retcode为0，trade_state为0才是支付成功
                    if (queryRes.isTenpaySign())
                    {
                        //取结果参数做业务处理
                        string out_trade_no = queryRes.getParameter("out_trade_no");
                        //财付通订单号
                        string transaction_id = queryRes.getParameter("transaction_id");
                        //金额,以分为单位
                        string total_fee = queryRes.getParameter("total_fee");
                        //如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
                        string discount = queryRes.getParameter("discount");
                        //支付结果
                        string trade_state = resHandler.getParameter("trade_state");
                        //交易模式，1即时到帐 2中介担保
                        string trade_mode = resHandler.getParameter("trade_mode");
                        #region
                        //判断签名及结果
                        if ("0".Equals(queryRes.getParameter("retcode")))
                        {
                            //Response.Write("id验证成功");


                            if ("1".Equals(trade_mode))
                            {
                                string opstate = "-1";
                                int status = 4;

                                //即时到账 
                                if ("0".Equals(trade_state))
                                {
                                    status = 2;
                                    opstate = "0";

                                    //------------------------------
                                    //即时到账处理业务开始
                                    //------------------------------

                                    //处理数据库逻辑
                                    //注意交易单不要重复处理
                                    //注意判断返回金额

                                    //------------------------------
                                    //即时到账处理业务完毕
                                    //------------------------------

                                    //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知
                                    //Context.Response.Write("success");
                                }
                                else
                                {
                                    //Context.Response.Write("即时到账支付失败");
                                }

                                decimal tranAmt = decimal.Parse(total_fee) / 100M;

                                OrderBankUtils.SuppNotify(SuppId
                                                        , out_trade_no
                                                        , transaction_id
                                                        , status
                                                        , opstate
                                                        , string.Empty
                                                        , tranAmt, tranAmt
                                                        , Succflag
                                                        , Failflag);

                                //viviapi.BLL.OrderBank bll = new viviapi.BLL.OrderBank();
                                //bll.DoBankComplete(suppId, out_trade_no, transaction_id, status, opstate, string.Empty, decimal.Parse(total_fee) / 100M, 0M, true, false);

                            }
                            else if ("2".Equals(trade_mode))
                            { //中介担保
                                //------------------------------
                                //中介担保处理业务开始
                                //------------------------------
                                //处理数据库逻辑
                                //注意交易单不要重复处理
                                //注意判断返回金额

                                int iStatus = Convert.ToInt32(trade_state);
                                switch (iStatus)
                                {
                                    case 0:		//付款成功

                                        break;
                                    case 1:		//交易创建

                                        break;
                                    case 2:		//收获地址填写完毕

                                        break;
                                    case 4:		//卖家发货成功

                                        break;
                                    case 5:		//买家收货确认，交易成功

                                        break;
                                    case 6:		//交易关闭，未完成超时关闭

                                        break;
                                    case 7:		//修改交易价格成功

                                        break;
                                    case 8:		//买家发起退款

                                        break;
                                    case 9:		//退款成功

                                        break;
                                    case 10:	//退款关闭

                                        break;

                                }


                                //------------------------------
                                //中介担保处理业务开始
                                //------------------------------


                                //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知
                                //Response.Write("success");
                            }
                        }
                        else
                        {
                            //错误时，返回结果可能没有签名，写日志trade_state、retcode、retmsg看失败详情。
                            //通知财付通处理失败，需要重新通知
                            //Response.Write("查询验证签名失败或id验证失败");
                            //Response.Write("retcode:" + queryRes.getParameter("retcode"));
                        }
                        #endregion
                    }
                    else
                    {
                        //Response.Write("通知ID查询签名验证失败");
                    }
                }
                else
                {
                    //通知财付通处理失败，需要重新通知
                    // Response.Write("后台调用通信失败");
                    //写错误日志
                    //Response.Write("call err:" + httpClient.getErrInfo() + "<br>" + httpClient.getResponseCode() + "<br>");

                }
            }
            else
            {
                //Response.Write("签名验证失败");
            }
            //Response.End();
        }

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
                    code = "1001";  //招商银行
                    break;
                case "967":
                    code = "1002"; //中国工商银行
                    break;
                case "964":
                    code = "1005"; //中国农业银行
                    break;
                case "965":
                    code = "1003"; //中国建设银行
                    break;
                case "963":
                    code = "1052"; //中国银行
                    break;
                case "977":
                    code = "1004"; //浦发银行
                    break;
                case "981":
                    code = "1020"; //中国交通银行
                    break;
                case "980":
                    code = "1006"; //中国民生银行
                    break;
                case "974":
                    code = "1008"; //深圳发展银行
                    break;
                case "985":
                    code = "1027"; //广东发展银行
                    break;
                case "962":
                    code = "1021"; //中信银行
                    break;
                case "982":
                    code = "1025"; //华夏银行
                    break;
                case "972":
                    code = "1009"; //兴业银行
                    break;
                //case "984":
                //    code = "00011"; //广州农村商业银行
                //    break;
                //case "1015":
                //    code = "GZCB"; //广州银行
                //    break;
                //case "1016":
                //    code = "CUPS"; //中国银联
                //    break;
                //case "976":
                //    code = "00030"; //上海农村商业银行
                //    break;
                //case "971":
                //    code = "POST"; //中国邮政
                //    break;
                case "989":
                    code = "1032"; //北京银行
                    break;
                //case "988":
                //    code = "CBHB"; //渤海银行
                //    break;
                //case "990":
                //    code = "00056"; //北京农商银行
                //    break;
                //case "979":
                //    code = "00055"; //南京银行
                //break;
                case "986":
                    code = "1022"; //中国光大银行
                    break;
                //case "987":
                //    code = "BEA"; //东亚银行
                //    break;
                //case "1025":
                //    code = "NBCB"; //宁波银行
                //    break;
                //case "983":
                //    code = "00081"; //杭州银行
                //    break;
                case "978":
                    code = "1010"; //平安银行
                    break;
                //case "1028":
                //    code = "HSB"; //徽商银行
                //    break;
                //case "968":
                //    code = "00086"; //浙商银行
                //    break;
                case "975":
                    code = "1024"; //上海银行
                    break;
                case "971":
                    code = "1028"; //中国邮政储蓄银行
                    break;
                //case "1032":
                //    code = "UPOP"; //银联在线支付
                //    break;
                default:
                    code = "DEFAULT";
                    break;
            }
            return code;
        }
        #endregion

    }
}

