using DBAccess;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviapi.SysConfig;

///
namespace viviapi.ETAPI.ZFuPay
{
    public class PostPay : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.ZFuPay;

        public PostPay()
            : base(SuppId)
        {

        }

        public static PostPay Default
        {
            get
            {
                var instance = new PostPay();
                return instance;
            }
        }


        #region GetPayForm

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outTradeNO"></param>
        /// <param name="amount"></param>
        /// <param name="autosumit"></param>
        /// <returns></returns>
        internal string return_url { get { return this.SiteDomain + "/return/zfupay/result.aspx"; } }
        internal string notify_url { get { return this.SiteDomain + "/receive/zfupay/notify.aspx"; } }
        internal string show_url { get { return this.SiteDomain + "/success.htm"; } }
        internal string Succflag = "success";
        internal string Failflag = "fail";
        internal string merchant_code = "2010151156";//商户号
        internal string merchant_private_key = "MIICeAIBADANBgkqhkiG9w0BAQEFAASCAmIwggJeAgEAAoGBANDKDWzfBk61GXMo+WizVq32BNNuQLjnmB4tEoXfCq1NtsugB7nGSNmxvyewy+dVB4hrGLeKiK7pv3JoRzvxMONdiGRNk2R15u8CTDXFr68XBCO6zCRURl4Cr2ugP89yca8XwioUa/XAtJGKWkcUyw+GwjCX9tlBcowKiJ5FS/afAgMBAAECgYEAsqrVBj9r1FqhJqz/kRs2p7MJuix08kYtJFWJrkmJh3gjXujY8568pJ24aKygMJvQ0GplQlsoUBXzIGIf4ymonLxr8xImRj34Mjq07v008aMc9c7nIOA9QGGpm9WGW5jsBge/uJp+5hTDYFpxuLAzCFrcEfZnFPVeOdecwKAkbjECQQDwHgavU6spUA8nzywRlZxjJ/EIC7wObHy+APxSyDFdGjNRL0QzIg8mI6X7JyfhVtXvI0KqTr5t4nkly6ePdzU9AkEA3pmKQsrtsTQVVHP98bCPSIf5a6NCA5kKHATIpdJSr6sYnLmyWipWZ7CfLHtwxfl1TLPJWnwKJ2cRV5yd6awxCwJBAK2gOJs8t+6Gmn7hum74rP2yGwMDYTdY0RafJdVCNxeoY2UX9Yu33BZq/pFOLfMuVEG4UHNPvzk74vgmfAIsY80CQQDNEfZmAj1n+uuLmjBg8J+P2nTVzNmMJvlBRsbyvQif/af2+rxshISFkhmSCUajnGWL/DWLvqo8Ep/PbuGr3I5xAkAa8I9CJa0NdlMcg4R32cecojEEOo8SxtKTo+8sjFK5uEfisylTzOnhHcBy7NTlgoySEN+ys6mTaIPZhM5DO4fI";//私钥
        //internal string dinpay_public_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCWOq5aHSTvdxGPDKZWSl6wrPpnMHW+8lOgVU71jB2vFGuA6dwa/RpJKnz9zmoGryZlgUmfHANnN0uztkgwb+5mpgmegBbNLuGqqHBpQHo2EsiAhgvgO3VRmWC8DARpzNxknsJTBhkUvZdy4GyrjnUrvsARg4VrFzKDWL0Yu3gunQIDAQAB";//公钥
          internal string dinpay_public_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCAvo7Er+3DAxFoEync7+h+jNH7ioS6sQ9folOhR97qIJKOHGQnRo27Dlzt1nQ+hBL3CqWSsjPkQf2+O2diKipEtFG4dQp6V3bogLX6zfa8qvlq1ylfcmRDhHTi7zCdsYc6bOFOAtPO3m0chWty0DU68wCZJnmphzcUAqpJtUFXewIDAQAB";

        public String GetPayForm(String orderid, Decimal orderAmt, String bankcode, Boolean autosumit)
        {
            /////////////////////////////////接收表单提交参数//////////////////////////////////////
            ////////////////////////To receive the parameter form HTML form//////////////////////
            var strSql = new StringBuilder();
            strSql.Append("select modeEnName");
            strSql.Append(" FROM channel ");
            strSql.Append(" where code='" + bankcode + "'");
            DataSet Channel = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());

            string input_charset1 = "GBK";
            string interface_version1 = "V3.0";//Request.Form["interface_version"].ToString().Trim();
            string merchant_code1 = merchant_code;// Request.Form["merchant_code"].ToString().Trim();
            string notify_url1 = notify_url;// Request.Form["notify_url"].ToString().Trim();
            string order_amount1 = orderAmt.ToString().Trim();// Request.Form["order_amount"].ToString().Trim();
            string order_no1 = orderid;//Request.Form["order_no"].ToString().Trim();
            string order_time1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//Request.Form["order_time"].ToString().Trim();
            string sign_type1 = "RSA-S";// Request.Form["sign_type"].ToString().Trim();
            string product_code1 = "商城充值";//Request.Form["product_code"].ToString().Trim();
            string product_desc1 = "商城充值";//Request.Form["product_desc"].ToString().Trim();
            string product_name1 = "商城充值";//Request.Form["product_name"].ToString().Trim();
            string product_num1 = "1";// Request.Form["product_num"].ToString().Trim();
            string return_url1 = return_url;//Request.Form["return_url"].ToString().Trim();
            string service_type1 = "direct_pay";// Request.Form["service_type"].ToString().Trim();
            string show_url1 = "";//Request.Form["show_url"].ToString().Trim();
            string extend_param1 = "";// Request.Form["extend_param"].ToString().Trim();
            string extra_return_param1 = "";// Request.Form["extra_return_param"].ToString().Trim();
            string client_ip1 = "";// Request.Form["client_ip"].ToString().Trim();
            string client_ip_check1 = "0";// Request.Form["client_ip_check"].ToString().Trim();
            string redo_flag1 = "0";// Request.Form["redo_flag"].ToString().Trim();
            string pay_type1 = "b2c";//Request.Form["pay_type"].ToString().Trim();
            string signData = "";

            ////////////////组装签名参数//////////////////
            string bank_code1 = Channel.Tables[0].Rows[0][0].ToString();// Request.Form["bank_code"].ToString().Trim();
            string signSrc = "";

            //组织订单信息
            if (bank_code1 != "")
            {
                signSrc = signSrc + "bank_code=" + bank_code1 + "&";
            }
            if (client_ip1 != "")
            {
                signSrc = signSrc + "client_ip=" + client_ip1 + "&";
            }
            if (client_ip_check1 != "")
            {
                signSrc = signSrc + "client_ip_check=" + client_ip_check1 + "&";
            }
            if (extend_param1 != "")
            {
                signSrc = signSrc + "extend_param=" + extend_param1 + "&";
            }
            if (extra_return_param1 != "")
            {
                signSrc = signSrc + "extra_return_param=" + extra_return_param1 + "&";
            }
            if (input_charset1 != "")
            {
                signSrc = signSrc + "input_charset=" + input_charset1 + "&";
            }
            if (interface_version1 != "")
            {
                signSrc = signSrc + "interface_version=" + interface_version1 + "&";
            }
            if (merchant_code1 != "")
            {
                signSrc = signSrc + "merchant_code=" + merchant_code1 + "&";
            }
            if (notify_url1 != "")
            {
                signSrc = signSrc + "notify_url=" + notify_url1 + "&";
            }
            if (order_amount1 != "")
            {
                signSrc = signSrc + "order_amount=" + order_amount1 + "&";
            }
            if (order_no1 != "")
            {
                signSrc = signSrc + "order_no=" + order_no1 + "&";
            }
            if (order_time1 != "")
            {
                signSrc = signSrc + "order_time=" + order_time1 + "&";
            }
            if (pay_type1 != "")
            {
                signSrc = signSrc + "pay_type=" + pay_type1 + "&";
            }
            if (product_code1 != "")
            {
                signSrc = signSrc + "product_code=" + product_code1 + "&";
            }
            if (product_desc1 != "")
            {
                signSrc = signSrc + "product_desc=" + product_desc1 + "&";
            }
            if (product_name1 != "")
            {
                signSrc = signSrc + "product_name=" + product_name1 + "&";
            }
            if (product_num1 != "")
            {
                signSrc = signSrc + "product_num=" + product_num1 + "&";
            }
            if (redo_flag1 != "")
            {
                signSrc = signSrc + "redo_flag=" + redo_flag1 + "&";
            }
            if (return_url1 != "")
            {
                signSrc = signSrc + "return_url=" + return_url1 + "&";
            }
            if (service_type1 != "")
            {
                signSrc = signSrc + "service_type=" + service_type1;
            }
            if (show_url1 != "")
            {
                signSrc = signSrc + "&show_url=" + show_url1;
            }

            if (sign_type1 == "RSA-S")//RSA-S签名方法
            {
                /**  merchant_private_key，商户私钥，商户按照《密钥对获取工具说明》操作并获取商户私钥。获取商户私钥的同时，也要
                    获取商户公钥（merchant_public_key）并且将商户公钥上传到智付商家后台"公钥管理"（如何获取和上传请看《密钥对获取工具说明》），
                    不上传商户公钥会导致调试的时候报错“签名错误”。
               */

                //demo提供的merchant_private_key是测试商户号1111110166的商户私钥，请自行获取商户私钥并且替换。
                 // string merchant_private_key = "MIICeAIBADANBgkqhkiG9w0BAQEFAASCAmIwggJeAgEAAoGBANDKDWzfBk61GXMo+WizVq32BNNuQLjnmB4tEoXfCq1NtsugB7nGSNmxvyewy+dVB4hrGLeKiK7pv3JoRzvxMONdiGRNk2R15u8CTDXFr68XBCO6zCRURl4Cr2ugP89yca8XwioUa/XAtJGKWkcUyw+GwjCX9tlBcowKiJ5FS/afAgMBAAECgYEAsqrVBj9r1FqhJqz/kRs2p7MJuix08kYtJFWJrkmJh3gjXujY8568pJ24aKygMJvQ0GplQlsoUBXzIGIf4ymonLxr8xImRj34Mjq07v008aMc9c7nIOA9QGGpm9WGW5jsBge/uJp+5hTDYFpxuLAzCFrcEfZnFPVeOdecwKAkbjECQQDwHgavU6spUA8nzywRlZxjJ/EIC7wObHy+APxSyDFdGjNRL0QzIg8mI6X7JyfhVtXvI0KqTr5t4nkly6ePdzU9AkEA3pmKQsrtsTQVVHP98bCPSIf5a6NCA5kKHATIpdJSr6sYnLmyWipWZ7CfLHtwxfl1TLPJWnwKJ2cRV5yd6awxCwJBAK2gOJs8t+6Gmn7hum74rP2yGwMDYTdY0RafJdVCNxeoY2UX9Yu33BZq/pFOLfMuVEG4UHNPvzk74vgmfAIsY80CQQDNEfZmAj1n+uuLmjBg8J+P2nTVzNmMJvlBRsbyvQif/af2+rxshISFkhmSCUajnGWL/DWLvqo8Ep/PbuGr3I5xAkAa8I9CJa0NdlMcg4R32cecojEEOo8SxtKTo+8sjFK5uEfisylTzOnhHcBy7NTlgoySEN+ys6mTaIPZhM5DO4fI";//私钥

                //string merchant_private_key = "MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBALf/+xHa1fDTCsLYPJLHy80aWq3djuV1T34sEsjp7UpLmV9zmOVMYXsoFNKQIcEzei4QdaqnVknzmIl7n1oXmAgHaSUF3qHjCttscDZcTWyrbXKSNr8arHv8hGJrfNB/Ea/+oSTIY7H5cAtWg6VmoPCHvqjafW8/UP60PdqYewrtAgMBAAECgYEAofXhsyK0RKoPg9jA4NabLuuuu/IU8ScklMQIuO8oHsiStXFUOSnVeImcYofaHmzIdDmqyU9IZgnUz9eQOcYg3BotUdUPcGgoqAqDVtmftqjmldP6F6urFpXBazqBrrfJVIgLyNw4PGK6/EmdQxBEtqqgXppRv/ZVZzZPkwObEuECQQDenAam9eAuJYveHtAthkusutsVG5E3gJiXhRhoAqiSQC9mXLTgaWV7zJyA5zYPMvh6IviX/7H+Bqp14lT9wctFAkEA05ljSYShWTCFThtJxJ2d8zq6xCjBgETAdhiH85O/VrdKpwITV/6psByUKp42IdqMJwOaBgnnct8iDK/TAJLniQJABdo+RodyVGRCUB2pRXkhZjInbl+iKr5jxKAIKzveqLGtTViknL3IoD+Z4b2yayXg6H0g4gYj7NTKCH1h1KYSrQJBALbgbcg/YbeU0NF1kibk1ns9+ebJFpvGT9SBVRZ2TjsjBNkcWR2HEp8LxB6lSEGwActCOJ8Zdjh4kpQGbcWkMYkCQAXBTFiyyImO+sfCccVuDSsWS+9jrc5KadHGIvhfoRjIj2VuUKzJ+mXbmXuXnOYmsAefjnMCI6gGtaqkzl527tw=";
                //私钥转换成C#专用私钥
                Functions fun = new Functions();
                merchant_private_key = fun.RSAPrivateKeyJava2DotNet(merchant_private_key);

                //签名
                signData = fun.RSASign(signSrc, merchant_private_key);
            }
            /*else  //RSA签名方法
            {
                RSAWithHardware rsa = new RSAWithHardware();
                string merPubKeyDir = "D:/1111110166.pfx";   //证书路径
                string password = "87654321";                //证书密码
                RSAWithHardware rsaWithH = new RSAWithHardware();
                rsaWithH.Init(merPubKeyDir, password, "D:/dinpayRSAKeyVersion");//初始化
                signData = rsaWithH.Sign(signSrc);    //签名
                
            }*/

            #region //构造提交表单
            StringBuilder sbPayForm = new StringBuilder();
            sbPayForm.Append("<form id=\"frmSubmit\" method=\"post\" name=\"frmSubmit\" action='https://pay.dinpay.com/gateway?input_charset=GBK'>");
            sbPayForm.Append("<input type='hidden' name='merchant_code' value='" + merchant_code1 + "' />");
            sbPayForm.Append("<input type='hidden' name='bank_code' value='" + bank_code1 + "' />");
            sbPayForm.Append("<input type='hidden' name='order_no' value='" + order_no1 + "' />");
            sbPayForm.Append("<input type='hidden' name='order_amount' value='" + order_amount1 + "' />");
            sbPayForm.Append("<input type='hidden' name='service_type' value='" + service_type1 + "' />");
            sbPayForm.Append("<input type='hidden' name='input_charset' value='" + input_charset1 + "' />");
            sbPayForm.Append("<input type='hidden' name='notify_url' value='" + notify_url1 + "' />");
            sbPayForm.Append("<input type='hidden' name='interface_version' value='" + interface_version1 + "' />");
            sbPayForm.Append("<input type='hidden' name='sign_type' value='" + sign_type1 + "' />");
            sbPayForm.Append("<input type='hidden' name='order_time' value='" + order_time1 + "' />");
            sbPayForm.Append("<input type='hidden' name='product_name' value='" + product_name1 + "' />");
            sbPayForm.Append("<input type='hidden' name='client_ip' value='" + client_ip1 + "' />");
            sbPayForm.Append("<input type='hidden' name='client_ip_check' value='" + client_ip_check1 + "' />");
            sbPayForm.Append("<input type='hidden' name='extend_param' value='" + extend_param1 + "' />");
            sbPayForm.Append("<input type='hidden' name='extra_return_param' value='" + extra_return_param1 + "' />");
            sbPayForm.Append("<input type='hidden' name='product_code' value='" + product_code1 + "' />");
            sbPayForm.Append("<input type='hidden' name='product_desc' value='" + product_desc1 + "' />");
            sbPayForm.Append("<input type='hidden' name='product_num' value='" + product_num1 + "' />");
            sbPayForm.Append("<input type='hidden' name='return_url' value='" + return_url1 + "' />");
            sbPayForm.Append("<input type='hidden' name='show_url' value='" + show_url1 + "' />");
            sbPayForm.Append("<input type='hidden' name='redo_flag' value='" + redo_flag1 + "' />");
            sbPayForm.Append("<input type='hidden' name='pay_type' value='" + pay_type1 + "' />");
            sbPayForm.Append("<input type='hidden' name='sign' value='" + signData + "' />");
            sbPayForm.Append("</form>");
            if (autosumit)
                sbPayForm.Append("<script type='text/javascript'>setTimeout(document.frmSubmit.submit(), 100);</script>");
            #endregion
            return sbPayForm.ToString();
        }
        #endregion
        public void Return(HttpContext Context)
        {
            //获取智付反馈信息
            //string merchant_code = Context.Request.Form["merchant_code"].ToString().Trim();
            string notify_type = Context.Request.Form["notify_type"].ToString().Trim();
            string notify_id = Context.Request.Form["notify_id"].ToString().Trim();
            string interface_version = Context.Request.Form["interface_version"].ToString().Trim();
            string sign_type = Context.Request.Form["sign_type"].ToString().Trim();
            string dinpaysign = Context.Request.Form["sign"].ToString().Trim();
            string order_no = Context.Request.Form["order_no"].ToString().Trim();
            string order_time = Context.Request.Form["order_time"].ToString().Trim();
            string order_amount = Context.Request.Form["order_amount"].ToString().Trim();
            string extra_return_param = Context.Request.Form["extra_return_param"];
            string trade_no = Context.Request.Form["trade_no"].ToString().Trim();
            string trade_time = Context.Request.Form["trade_time"].ToString().Trim();
            string trade_status = Context.Request.Form["trade_status"].ToString().Trim();
            string bank_seq_no = Context.Request.Form["bank_seq_no"];

            /**
             *签名顺序按照参数名a到z的顺序排序，若遇到相同首字母，则看第二个字母，以此类推，
            *同时将商家支付密钥key放在最后参与签名，组成规则如下：
            *参数名1=参数值1&参数名2=参数值2&……&参数名n=参数值n&key=key值
            **/
            //组织订单信息
            string signStr = "";

            if (null != bank_seq_no && bank_seq_no != "")
            {
                signStr = signStr + "bank_seq_no=" + bank_seq_no.ToString().Trim() + "&";
            }

            if (null != extra_return_param && extra_return_param != "")
            {
                signStr = signStr + "extra_return_param=" + extra_return_param + "&";
            }
            signStr = signStr + "interface_version=V3.0" + "&";
            signStr = signStr + "merchant_code=" + merchant_code + "&";


            if (null != notify_id && notify_id != "")
            {
                signStr = signStr + "notify_id=" + notify_id + "&notify_type=" + notify_type + "&";
            }

            signStr = signStr + "order_amount=" + order_amount + "&";
            signStr = signStr + "order_no=" + order_no + "&";
            signStr = signStr + "order_time=" + order_time + "&";
            signStr = signStr + "trade_no=" + trade_no + "&";
            signStr = signStr + "trade_status=" + trade_status + "&";

            if (null != trade_time && trade_time != "")
            {
                signStr = signStr + "trade_time=" + trade_time;
            }

            if (sign_type == "RSA-S") //RSA-S的验签方法
            {
                /**
                1)dinpay_public_key，智付公钥，每个商家对应一个固定的智付公钥（不是使用工具生成的密钥merchant_public_key，不要混淆），
                即为智付商家后台"公钥管理"->"智付公钥"里的绿色字符串内容
                2)demo提供的dinpay_public_key是测试商户号1111110166的智付公钥，请自行复制对应商户号的智付公钥进行调整和替换。
                */
                Functions fun = new Functions();
                //string dinpay_public_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCWOq5aHSTvdxGPDKZWSl6wrPpnMHW+8lOgVU71jB2vFGuA6dwa/RpJKnz9zmoGryZlgUmfHANnN0uztkgwb+5mpgmegBbNLuGqqHBpQHo2EsiAhgvgO3VRmWC8DARpzNxknsJTBhkUvZdy4GyrjnUrvsARg4VrFzKDWL0Yu3gunQIDAQAB";
                //将智付公钥转换成C#专用格式
                dinpay_public_key = fun.RSAPublicKeyJava2DotNet(dinpay_public_key);
                //验签
                bool result = fun.ValidateRsaSign(signStr, dinpay_public_key, dinpaysign);
                if (result == true)
                {

                    string _info = "支付失败-智付";
                    string opstate = "-1";
                    int status = 4;

                    decimal tranAmt = decimal.Parse(order_amount);
                    if ("SUCCESS".Equals(trade_status))
                    {
                        _info = "支付成功";
                        status = 2;
                        opstate = "0";
                    }
                    OrderBankUtils.SuppPageReturn(SuppId
                    , order_no
                    , trade_no
                    , status
                    , opstate
                    , _info
                    , tranAmt, 0M);

                    //注：建议页面同步通知一般只做订单支付成功提示，而不做订单支付状态更新，更新订单支付状态在notify_url指定的地址处理，请做好订单是否重复修改状态的判断，以免虚拟充值重复到账！！
                    // Response.Write("验签成功");
                    pageWrite("result验签成功");
                    pageWrite("result"+ _info+ status.ToString()+opstate);
                }
                else
                {
                    //验签失败
                    //Response.Write("验签失败");
                    HttpContext.Current.Response.Write("<script>alert('出现异常！请查看充值是否到帐。若未到帐，请与管理员联系。');</script>");

                    pageWrite("result验签失败");
                }

            }
        }

        public void Notify(HttpContext Context)
        {
            /////////////////////////////////接收表单提交参数//////////////////////////////////////

            //string merchant_code = Context.Request.Form["merchant_code"].ToString().Trim();

            string service_type = Context.Request.Form["service_type"].ToString().Trim();

            string sign_type = Context.Request.Form["sign_type"].ToString().Trim();

            string interface_version = Context.Request.Form["interface_version"].ToString().Trim();

            string order_no = Context.Request.Form["order_no"].ToString().Trim();


            /////////////////////////////   数据签名  /////////////////////////////////

            string signStr = "interface_version=" + interface_version + "&merchant_code=" + merchant_code + "&order_no=" + order_no + "&service_type=" + service_type;

            if (sign_type == "RSA-S") //RSA-S签名方法
            {
                /**  merchant_private_key，商户私钥，商户按照《密钥对获取工具说明》操作并获取商户私钥。获取商户私钥的同时，也要
                     获取商户公钥（merchant_public_key）并且将商户公钥上传到智付商家后台"公钥管理"（如何获取和上传请看《密钥对获取工具说明》），
                     不上传商户公钥会导致调试的时候报错“签名错误”。
                */
                Functions fun = new Functions();
                //demo提供的merchant_private_key是测试商户号1111110166的商户私钥，请自行获取商户私钥并且替换。
                //string merchant_private_key = "MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBALf/+xHa1fDTCsLYPJLHy80aWq3djuV1T34sEsjp7UpLmV9zmOVMYXsoFNKQIcEzei4QdaqnVknzmIl7n1oXmAgHaSUF3qHjCttscDZcTWyrbXKSNr8arHv8hGJrfNB/Ea/+oSTIY7H5cAtWg6VmoPCHvqjafW8/UP60PdqYewrtAgMBAAECgYEAofXhsyK0RKoPg9jA4NabLuuuu/IU8ScklMQIuO8oHsiStXFUOSnVeImcYofaHmzIdDmqyU9IZgnUz9eQOcYg3BotUdUPcGgoqAqDVtmftqjmldP6F6urFpXBazqBrrfJVIgLyNw4PGK6/EmdQxBEtqqgXppRv/ZVZzZPkwObEuECQQDenAam9eAuJYveHtAthkusutsVG5E3gJiXhRhoAqiSQC9mXLTgaWV7zJyA5zYPMvh6IviX/7H+Bqp14lT9wctFAkEA05ljSYShWTCFThtJxJ2d8zq6xCjBgETAdhiH85O/VrdKpwITV/6psByUKp42IdqMJwOaBgnnct8iDK/TAJLniQJABdo+RodyVGRCUB2pRXkhZjInbl+iKr5jxKAIKzveqLGtTViknL3IoD+Z4b2yayXg6H0g4gYj7NTKCH1h1KYSrQJBALbgbcg/YbeU0NF1kibk1ns9+ebJFpvGT9SBVRZ2TjsjBNkcWR2HEp8LxB6lSEGwActCOJ8Zdjh4kpQGbcWkMYkCQAXBTFiyyImO+sfCccVuDSsWS+9jrc5KadHGIvhfoRjIj2VuUKzJ+mXbmXuXnOYmsAefjnMCI6gGtaqkzl527tw=";
                //私钥转换成C#专用私钥
                merchant_private_key = fun.RSAPrivateKeyJava2DotNet(merchant_private_key);
                //签名
                string signData = fun.RSASign(signStr, merchant_private_key);
                //将signData进行UrlEncode编码
                signData = HttpUtility.UrlEncode(signData);
                //组装字符串
                string para = signStr + "&sign_type=" + sign_type + "&sign=" + signData;
                //用HttpPost方式提交
                string _xml = fun.httppost("https://query.dinpay.com/query", para, "GBK");
                //将返回的xml中的参数提取出来
                var el = XElement.Load(new StringReader(_xml));
                //提取参数
                var is_success1 = el.XPathSelectElement("/response/is_success");
                var merchantcode1 = el.XPathSelectElement("/response/trade/merchant_code");
                var orderno1 = el.XPathSelectElement("/response/trade/order_no");
                var ordertime1 = el.XPathSelectElement("/response/trade/order_time");
                var orderamount1 = el.XPathSelectElement("/response/trade/order_amount");
                var trade_no1 = el.XPathSelectElement("/response/trade/trade_no");
                var trade_time1 = el.XPathSelectElement("/response/trade/trade_time");
                var dinpaysign1 = el.XPathSelectElement("/response/sign");
                var trade_status1 = el.XPathSelectElement("/response/trade/trade_status");
                //去掉首尾的标签并转换成string
                string is_success = Regex.Match(is_success1.ToString(), "(?<=>).*?(?=<)").Value; //不参与验签
                if (is_success == "F")
                {
                    //Response.Write("查询失败:" + _xml + "<br/>");
                    //Response.End();
                    return;
                }
                string merchantcode = Regex.Match(merchantcode1.ToString(), "(?<=>).*?(?=<)").Value;
                string orderno = Regex.Match(orderno1.ToString(), "(?<=>).*?(?=<)").Value;
                string ordertime = Regex.Match(ordertime1.ToString(), "(?<=>).*?(?=<)").Value;
                string orderamount = Regex.Match(orderamount1.ToString(), "(?<=>).*?(?=<)").Value;
                string trade_no = Regex.Match(trade_no1.ToString(), "(?<=>).*?(?=<)").Value;
                string trade_time = Regex.Match(trade_time1.ToString(), "(?<=>).*?(?=<)").Value;
                string trade_status = Regex.Match(trade_status1.ToString(), "(?<=>).*?(?=<)").Value;
                string dinpaysign = Regex.Match(dinpaysign1.ToString(), "(?<=>).*?(?=<)").Value;
                //组装字符串
                string signsrc = "merchant_code=" + merchantcode + "&order_amount=" + orderamount + "&order_no=" + orderno + "&order_time=" + ordertime + "&trade_no=" + trade_no + "&trade_status=" + trade_status + "&trade_time=" + trade_time;

                /**
                 1)dinpay_public_key，智付公钥，每个商家对应一个固定的智付公钥（不是使用工具生成的密钥merchant_public_key，不要混淆），
                 即为智付商家后台"公钥管理"->"智付公钥"里的绿色字符串内容
                 2)demo提供的dinpay_public_key是测试商户号1111110166的智付公钥，请自行复制对应商户号的智付公钥进行调整和替换。
                 */

                //string dinpay_public_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCWOq5aHSTvdxGPDKZWSl6wrPpnMHW+8lOgVU71jB2vFGuA6dwa/RpJKnz9zmoGryZlgUmfHANnN0uztkgwb+5mpgmegBbNLuGqqHBpQHo2EsiAhgvgO3VRmWC8DARpzNxknsJTBhkUvZdy4GyrjnUrvsARg4VrFzKDWL0Yu3gunQIDAQAB";

                //将智付公钥转换成C#专用格式
                dinpay_public_key = fun.RSAPublicKeyJava2DotNet(dinpay_public_key);
                //验签
                bool validateResult = fun.ValidateRsaSign(signsrc, dinpay_public_key, dinpaysign);
                if (validateResult == false)
                {
                    //Response.Write("验签失败");
                    // Response.End();
                    pageWrite("Notify验签失败");
                }
                else
                {
                    string opstate = "-1";
                    int status = 4;
                    if ("SUCCESS".Equals(trade_status))
                    {
                        status = 2;
                        opstate = "0";
                    }
                    decimal tranAmt = decimal.Parse(orderamount);

                    OrderBankUtils.SuppNotify(SuppId
                                            , order_no
                                            , trade_no
                                            , status
                                            , opstate
                                            , string.Empty
                                            , tranAmt, tranAmt
                                            , Succflag
                                            , Failflag);

                    pageWrite("Notify验签成功");

                    pageWrite("Notify"+ status.ToString()+ opstate);
                }
                //Response.Write("验签成功");
            }
        }
        protected void pageWrite(string str)
        {
            str = str + "%%%%%%%%%%%%";
            FileStream fs = File.Open("D:\\post.txt", FileMode.Append, FileAccess.Write);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }

    }
}

