using DinpayRSAAPI.COM.Dinpay.RsaUtils;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using ThoughtWorks.QRCode.Codec;

namespace ZFuPay
{
    public class PostPay 
    {
        public String GetPayForm(String orderid, Decimal orderAmt, String bankcode, Boolean autosumit)
        {

            /////////////////////////////////接收表单提交参数//////////////////////////////////////
            ////////////////////////To receive the parameter form HTML form//////////////////////

            string input_charset1 = "UTF-8";
            string interface_version1 = "V3.0";//Request.Form["interface_version"].ToString().Trim();
            string merchant_code1 = "1111110166";// Request.Form["merchant_code"].ToString().Trim();
            string notify_url1 = "";// Request.Form["notify_url"].ToString().Trim();
            string order_amount1 = orderAmt.ToString().Trim();// Request.Form["order_amount"].ToString().Trim();
            string order_no1 = orderid;//Request.Form["order_no"].ToString().Trim();
            string order_time1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//Request.Form["order_time"].ToString().Trim();
            string sign_type1 = "RSA-S";// Request.Form["sign_type"].ToString().Trim();
            string product_code1 = "product_code";//Request.Form["product_code"].ToString().Trim();
            string product_desc1 = "product_desc";//Request.Form["product_desc"].ToString().Trim();
            string product_name1 = "product_name";//Request.Form["product_name"].ToString().Trim();
            string product_num1 = "1";// Request.Form["product_num"].ToString().Trim();
            string return_url1 = "";//Request.Form["return_url"].ToString().Trim();
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

            string bank_code1 = "";// Request.Form["bank_code"].ToString().Trim();
            switch (bankcode)
            {
                case "967":
                    bank_code1 = "ICBC"; break;
                case "970":
                    bank_code1 = "CMB"; break;
                case "965":
                    bank_code1 = "CCB"; break;
                case "964":
                    bank_code1 = "ABC"; break;
                case "980":
                    bank_code1 = "CMBC"; break;
                case "978":
                    bank_code1 = "SPABANK"; break;
                case "981":
                    bank_code1 = "BCOM"; break;
                case "986":
                    bank_code1 = "CEBB"; break;
                case "971":
                    bank_code1 = "PSBC"; break;
                case "972":
                    bank_code1 = "CIB"; break;
                case "963":
                    bank_code1 = "BOC"; break;
            }

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

                string merchant_private_key = "MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBALf/+xHa1fDTCsLYPJLHy80aWq3djuV1T34sEsjp7UpLmV9zmOVMYXsoFNKQIcEzei4QdaqnVknzmIl7n1oXmAgHaSUF3qHjCttscDZcTWyrbXKSNr8arHv8hGJrfNB/Ea/+oSTIY7H5cAtWg6VmoPCHvqjafW8/UP60PdqYewrtAgMBAAECgYEAofXhsyK0RKoPg9jA4NabLuuuu/IU8ScklMQIuO8oHsiStXFUOSnVeImcYofaHmzIdDmqyU9IZgnUz9eQOcYg3BotUdUPcGgoqAqDVtmftqjmldP6F6urFpXBazqBrrfJVIgLyNw4PGK6/EmdQxBEtqqgXppRv/ZVZzZPkwObEuECQQDenAam9eAuJYveHtAthkusutsVG5E3gJiXhRhoAqiSQC9mXLTgaWV7zJyA5zYPMvh6IviX/7H+Bqp14lT9wctFAkEA05ljSYShWTCFThtJxJ2d8zq6xCjBgETAdhiH85O/VrdKpwITV/6psByUKp42IdqMJwOaBgnnct8iDK/TAJLniQJABdo+RodyVGRCUB2pRXkhZjInbl+iKr5jxKAIKzveqLGtTViknL3IoD+Z4b2yayXg6H0g4gYj7NTKCH1h1KYSrQJBALbgbcg/YbeU0NF1kibk1ns9+ebJFpvGT9SBVRZ2TjsjBNkcWR2HEp8LxB6lSEGwActCOJ8Zdjh4kpQGbcWkMYkCQAXBTFiyyImO+sfCccVuDSsWS+9jrc5KadHGIvhfoRjIj2VuUKzJ+mXbmXuXnOYmsAefjnMCI6gGtaqkzl527tw=";
                //私钥转换成C#专用私钥
                Functions fun= new Functions();
                merchant_private_key = fun.RSAPrivateKeyJava2DotNet(merchant_private_key);
                //签名
                signData = fun.RSASign(signSrc, merchant_private_key);
            }
            else  //RSA签名方法
            {
                RSAWithHardware rsa = new RSAWithHardware();
                string merPubKeyDir = "D:/1111110166.pfx";   //证书路径
                string password = "87654321";                //证书密码
                RSAWithHardware rsaWithH = new RSAWithHardware();
                rsaWithH.Init(merPubKeyDir, password, "D:/dinpayRSAKeyVersion");//初始化
                signData = rsaWithH.Sign(signSrc);    //签名
            }


            /* merchant_code.Value = merchant_code1;
             bank_code.Value = bank_code1;
             order_no.Value = order_no1;
             order_amount.Value = order_amount1;
             service_type.Value = service_type1;
             input_charset.Value = input_charset1;
             notify_url.Value = notify_url1;
             interface_version.Value = interface_version1;
             sign_type.Value = sign_type1;
             order_time.Value = order_time1;
             product_name.Value = product_name1;
             client_ip.Value = client_ip1;
             client_ip_check.Value = client_ip_check1;
             extend_param.Value = extend_param1;
             extra_return_param.Value = extra_return_param1;
             product_code.Value = product_code1;
             product_desc.Value = product_desc1;
             product_num.Value = product_num1;
             return_url.Value = return_url1;
             show_url.Value = show_url1;
             redo_flag.Value = redo_flag1;
             pay_type.Value = pay_type1;*/
            #region //构造提交表单
            StringBuilder sbPayForm = new StringBuilder();
            sbPayForm.Append("<form id=\"frmSubmit\" method=\"post\" name=\"frmSubmit\" action='https://pay.dinpay.com/gateway?input_charset=UTF-8'>");
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
    }
}
