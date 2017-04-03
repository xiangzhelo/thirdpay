using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Demo.Class;
using System.Text;
using System.Net;
using System.IO;
namespace Demo
{
    public partial class PayDo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string orderid = Guid.NewGuid().ToString().Substring(0, 20).Replace("-", "");
            string callbackurl = "http://" + Request.Url.Host + ":" + Request.Url.Port + "/Receive.aspx";
            Eka365pay(orderid, callbackurl);

        }

        /// <summary>
        /// 翁贝支付
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <param name="callBackurl">返回地址</param>
        private void Eka365pay(String orderid, String callBackurl)
        {
            //商户信息
            String shop_id = ConfigurationManager.AppSettings["userid"]; //商户ID
            String key = ConfigurationManager.AppSettings["userkey"]; //商户密钥


            //组织接口发送。
            if (Request.Form["bankCardType"] == "00")
            {
                //银行提交获取信息
                String bank_Type = Request.Form["bankCode"];//银行类型
                //String bank_gameAccount = Request.Form["txtUserName"];//充值账号
                String bank_payMoney = Request.Form["totalAmount"];//充值金额
                //银行卡支付
                String param = String.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", shop_id, bank_Type, bank_payMoney, orderid, callBackurl);

                String PostUrl = String.Format("http://" + Request.Url.Host + ":" + Request.Url.Port + "/chargebank.aspx?{0}&sign={1}", param, FormsAuthentication.HashPasswordForStoringInConfigFile(param + key, "MD5").ToLower());
                Response.Redirect(PostUrl);//转发URL地址
            }
            else if (Request.Form["bankCardType"] == "01")
            {
                //获取卡类提交信息
                String card_No = Request.Form["cardNo"];//卡号
                String card_pwd = Request.Form["cardPwd"];//卡密
                //String card_account = Request.Form["txtUserNameCard"];//充值账号
                String card_type = Request.Form["bankCode"].Split('，')[0];//卡类型
                String card_payMoney = Request.Form["totalAmount"];//充值金额
                String restrict = "0";//使用范围
                String attach = "test";//附加内容，下行原样返回
                //if (Request.Form["bankCode"].Split(',').Length > 1)
                //{
                //    restrict = Request.Form["totalAmount"].Split(',')[1];
                //}
                //卡类支付
                String param = String.Format("type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}", card_type, shop_id, card_No, card_pwd, card_payMoney, restrict, orderid, callBackurl);
                String PostUrl = String.Format("http://" + Request.Url.Host + ":" + Request.Url.Port + "/cardReceive.aspx?{0}&attach={1}&sign={2}", param, attach, FormsAuthentication.HashPasswordForStoringInConfigFile(param + key, "MD5").ToLower());

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(PostUrl);
                //获取响应结果 此过程大概需要5秒
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //获取响应流
                Stream stream = httpWebResponse.GetResponseStream();
                //用指定的字符编码为指定的流初始化 StreamReader 类的一个新实例。
                using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    string useResult = streamReader.ReadToEnd();
                    streamReader.Dispose();
                    streamReader.Close();
                    httpWebResponse.Close();

                    if (useResult.Trim() == "opstate=0")
                    {
                        Response.Write("支付成功.");
                    }
                    if (useResult.Trim() == "opstate=-1")
                    {
                        Response.Write("请求参数无效。");
                    }
                    if (useResult.Trim() == "opstate=-2")
                    {
                        Response.Write("签名错误。");
                    }
                    if (useResult.Trim() == "opstate=-3")
                    {
                        Response.Write("提交的卡密为重复提交，系统不进行消耗并进入下行流程。");
                    }
                    if (useResult.Trim() == "opstate=-4")
                    {
                        Response.Write("提交的卡密不符合乐收卡定义的卡号密码面值规则。");
                        //提交的卡密不符合乐收卡定义的卡号密码面值规则。
                    }
                    if (useResult.Trim() == "opstate=-999")
                    {
                        Response.Write("接口维护中。");
                        ////这里把定单状态接口维护中。
                    }
                }
            }
        }
    }
}
