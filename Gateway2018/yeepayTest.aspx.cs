
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.ETAPI.ebao2;

namespace viviAPI.Gateway2018
{
    public partial class ECPay : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //加载页面时初始化支付需要的业务明文参数
            if (!IsPostBack)
            {
                orderAmount.Text = "0.1";//默认的交易金额，单位元

                Random ra = new Random();
                traderOrderID.Text = "1234567" + 50 * ra.Next();//商户订单号


                productCatalog.Text = "30";
                productName.Text = "玉米加农炮";//商品名称
                productDesc.Text = "植物大战僵尸道具";//商品描述
                bankId.Text = "CMBCHINA-NET-B2C";//银行编码
                fcallbackURL.Text = "http://218.5.9.29:1058/Zgt/CallBack.aspx";
                callbackURL.Text = "http://218.5.9.29:1058/Zgt/CallBack.aspx";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //商户账户编号
            string customernumber = "1080";
            string hmacKey = "b9fc4b3d1c4a4e3b9fdc94cc4faa6e9a";
            string AesKey = "1234567890123456";

            //日志字符串
            StringBuilder logsb = new StringBuilder();
            logsb.Append(DateTime.Now.ToString() + "\n");

            Random ra = new Random();
            string payproducttype = "SALES"; // "支付方式";
            string amount = (orderAmount.Text);//支付金额为，单位元    
            string requestid = traderOrderID.Text;//订单号
            string productcat = productCatalog.Text;//商品类别码，商户支持的商品类别码由易宝支付运营人员根据商务协议配置
            string productdesc = productDesc.Text;//商品描述
            string productname = productName.Text;//商品名称
            string assure = "0";//是否需要担保,1是，0否
            string divideinfo = "";//分账信息，格式”ledgerNo:分账比
            string bankid = bankId.Text;//银行编码
            string period = "";//担保有效期，单位 ：天；当assure=1 时必填，最大值：30
            string memo = "";//商户备注

            //商户提供的商户后台系统异步支付回调地址
            string callbackurl = callbackURL.Text;
            //商户提供的商户前台系统异步支付回调地址
            string webcallbackurl = fcallbackURL.Text;
            string hmac = "";


            hmac = Digest.GetHMAC(customernumber, requestid, amount, assure, productname, productcat, productdesc, divideinfo, callbackurl, webcallbackurl, bankid, period, memo, hmacKey);

            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("customernumber", customernumber);
            sd.Add("amount", amount);
            sd.Add("requestid", requestid);
            sd.Add("assure", assure);
            sd.Add("productname", productname);
            sd.Add("productcat", productcat);
            sd.Add("productdesc", productdesc);
            sd.Add("divideinfo", divideinfo);
            sd.Add("callbackurl", callbackurl);
            sd.Add("webcallbackurl", webcallbackurl);
            sd.Add("bankid", bankid);
            sd.Add("period", period);
            sd.Add("memo", memo);
            sd.Add("payproducttype", payproducttype);
            sd.Add("hmac", hmac);



            //将网页支付对象转换为json字符串
            string wpinfo_json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
            logsb.Append("网银支付明文数据json格式为：" + wpinfo_json + "\n");

            string datastring = AESUtil.Encrypt(wpinfo_json, AesKey);

            logsb.Append("网银支付业务数据经过AES加密后的值为：" + datastring + "\n");



            //打开浏览器访问一键支付网页支付链接地址，请求方式为get
            string postParams = "data=" + HttpUtility.UrlEncode(datastring) + "&customernumber=" + customernumber;
            string url = "/yeepayNewApi.aspx?" + postParams;
            Response.Redirect(url);
        }
    }
}