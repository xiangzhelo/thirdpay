using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviapi.SysConfig;

///
namespace viviapi.ETAPI.Alipay
{
    /// <summary>
    /// 首先大家有个疑问，技术文档中的输入参数列表中给出了诸多参数，而手上拿到的代码里只写了一部分参数来进行传递信息，这究竟是为什么？那么我们先带着这个疑问往下看。
    ///以下讨论的参数不涵盖网关gateway、加密参数sign、加密类型sign_type，因为这些都是必须的。
    ///以实物标准双接口为例，可把参数看做几个功能部分组成
    ///a) 不可缺少的参数
    ///i. service服务参数，这个是用来区别这个接口是用的什么接口，所以绝对不能修改。
    ///ii. partner合作身份者ID、key安全校验码或称私钥这一组参数是签约合同生效后才能拿的到，partner是来鉴别是哪个商家与支付宝签约，而这个Key它如同钥匙般相当重要。
    ///iii. seller_email收款人支付宝账号，支付宝中有手机类型、电子邮件类型的支付宝账号是都可以用这个参数的。
    ///iv. subject在支付宝的收银台里是直接与商品名称关联在一起的，但是说的更准确些的话，这个参数是这笔交易的名称，因为这笔交易不一定只买一件商品。它的作用不仅是在收银台里可以清晰的显示出来，而且在支付宝的账
    ///户的交易明细的列表里，它也是排在第一列，由此可推测出，它有财务对账、作为交易查询的筛选条件等诸多作用。非常重要。
    ///v. out_trade_no技术文档中给出的是商户交易号（确保在商户系统中唯一），顾名思义这个就是我们大家自己网站的订单系统里的唯一订单号，而非支付宝的。这里需要强调的，这个订单号必须得是唯一的，如何唯一法？自己网站
    ///里订单系统的订单号是绝对唯一的吧，支付宝要求的唯一就是这个，为什么非要唯一？支付宝会根据订单号来判定这笔订单对于这个商家的所有交易中是否是唯一的。
    ///vi. price金额、quantity数量，这里设置有两种方式一种商品的单价金额，多个数量（即大于等于1）。另种是数量为1，金额代表总额，甚至是包含了运费。为什么大部分的客户要这么做？原因很简单，第一，购物车里的东西不一定是单纯的
    ///一件或者多件相同的商品，那么为商品设置金额时就有困难了，因此这里用总额是最好的，而数量就默认为1。第二，运费的设置很多客户是与各家快递公司签约、每件物品的快递费用也不尽相同，为了省去麻烦，在程序计算的
    ///时候干脆把运费也加进去。因此我们只需要记住一件事，这个price的金额就是所谓的总额了。
    ///vii. payment_type支付类型，没什么可说的直接写成1，无需改动。
    ///viii. 物流信息logistics_type、logistics_fee、logistics_payment这是一组物流信息，实物标准双接口中必须得至少有一组物流信息，也就是指这三个参数了，最多可有三组，哪三组呢？logistics_type_1、logistics_fee_1、logistics_paymen
    ///t_1（第二组）；logistics_type_2、logistics_fee_2、logistics_payment_2（第三组）。后两组为可选项。一般前面有说Price已经是总额了且包含了运费，那
    ///么这里物流运费就直接设置成0即可，即logistics_fee=”0”，其他两个的信息可参考技术文档来填写，因为要从技术文档中的枚举列表里来选择，所以绝不可乱填写。
    ///b) 可增加的有用参数
    ///i. 物流信息最多三组，最少一组，这已经在前部分有所提及，这里就不再细说。
    ///ii. _input_charset，当是UTF-8的编码格式时必须得用到且不允许为空的，即_input_charset=”utf-8”
    ///iii. notify_url、return_url，return_url代表支付完毕后可以自动从支付宝的官方页面跳转回来，notify_url这个是防止调单的首选最佳工具。
    ///iv. body，在支付宝收银台中的商品描述里显示，如果subject是订单名称的话，那么这个body则最准确的称之为订单描述，其实个人认为它作为备注之类的更为恰当。很多人都很郁闷支付宝为何不能像其他公司
    ///的接口有个自定义的参数来存放客户想要的东西，其实body也具有类似的这种功能，它不仅容纳的信息是所有参数里最大的，而且还是以字符串的形式储存，个人认为它其实也是非常重要的不可缺少的参数之一呢。
    ///v. discount折扣，顾名思义如果小于0，则是用原金额Price*quantity+(discount)，实际金额便比原总额小了。现在有些商户有支付宝的优惠卷，而优惠卷的用途也是在这个参数中体现，具体做法与前
    ///面无异。
    ///vi. show_url商品展示地址，这个链接的作用是在支付宝收银台的商品链接旁边有个下划线“详情”的链接，而点链接弹出的一个新页面便是这个商品展示地址的页面。
    ///vii. 收货信息receive_name、receive_address、receive_zip、receive_phone、receive_mobile，这些信息若也设置为传递给支付宝的参数之一的话，那么在支付宝收银台点选下一步的时候，本该出现的填写收货信息页面不见踪影，而直接跳到了收货信息页面的下一个页面去了。很多商户在自己的网站的购物
    ///流程中都有一个填写收货信息的选项卡，为了省去到支付宝收银台中还要填写一次收货信息的麻烦，那么这些收货信息的参数就派上用场了。值得注意的是，收货人姓名和地址是必填项，不然还是会出现收货信息填写页。
    ///viii. buyer_email买家支付宝账号，这个设置好后呈现的效果便是，原本是空的支付宝账号的输入框此时已经有个支付宝账号在里面放置。
    /// </summary>
    public class AliPay : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Alipay;

        public AliPay()
            : base(SuppId)
        {
            
        }

        public static AliPay Default
        {
            get
            {
                var instance = new AliPay();
                return instance;
            }
        }

        private string _input_charset = "gb2312";
        //https://www.alipay.com/cooperate/gateway.do
        //https://mapi.alipay.com/gateway.do

        private string alipayNotifyURL = "https://mapi.alipay.com/cooperate/gateway.do?";// "https://www.alipay.com/cooperate/gateway.do?";
        private string body = PaymentSetting.alipay_body;
        private string subject = PaymentSetting.alipay_subject;
        private string sign_type = "MD5";
        private string payment_type = "1";
        private string service = "create_direct_pay_by_user";

        internal string notify_url { get { return this.SiteDomain + "/receive/alipay/result.aspx"; } }
        internal string return_url { get { return this.SiteDomain + "/return/alipay/result.aspx"; } }
        internal string show_url { get { return this.SiteDomain + "/showurl.aspx"; } }

        internal string Succflag = "success";
        internal string Failflag = "fail";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string GetPayUrl(string out_trade_no, double amount)
        {
            string puserid = SuppAccount;
            string pusername = SuppUserName;
            string puserkey = SuppKey;

            string alipayNotifyURL = this.alipayNotifyURL;
            string str5 = amount.ToString();
            return this.CreatUrl(alipayNotifyURL, this.service, puserid, this.sign_type, out_trade_no, this.subject, this.body, this.payment_type, str5, this.show_url, pusername, puserkey, this.return_url, this.notify_url);
        }

        #region CreatUrl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gateway"></param>
        /// <param name="service"></param>
        /// <param name="partner"></param>
        /// <param name="sign_type"></param>
        /// <param name="out_trade_no"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="payment_type"></param>
        /// <param name="total_fee"></param>
        /// <param name="show_url"></param>
        /// <param name="seller_email"></param>
        /// <param name="key"></param>
        /// <param name="return_url"></param>
        /// <param name="notify_url"></param>
        /// <returns></returns>
        public string CreatUrl(string gateway, string service, string partner, string sign_type, string out_trade_no, string subject, string body, string payment_type, string total_fee, string show_url, string seller_email, string key, string return_url, string notify_url)
        {
            int num;
            string[] strArray2 = BubbleSort(new string[] { "service=" + service, "partner=" + partner, "subject=" + subject, "body=" + body, "out_trade_no=" + out_trade_no, "total_fee=" + total_fee, "show_url=" + show_url, "payment_type=" + payment_type, "seller_email=" + seller_email, "notify_url=" + notify_url, "return_url=" + return_url });
            StringBuilder builder = new StringBuilder();
            for (num = 0; num < strArray2.Length; num++)
            {
                if (num == (strArray2.Length - 1))
                {
                    builder.Append(strArray2[num]);
                }
                else
                {
                    builder.Append(strArray2[num] + "&");
                }
            }
            builder.Append(key);
            string str = GetMD5(builder.ToString(), this._input_charset);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(gateway);
            for (num = 0; num < strArray2.Length; num++)
            {
                builder2.Append(strArray2[num] + "&");
            }
            builder2.Append("sign=" + str + "&sign_type=" + sign_type);
            return builder2.ToString();
        }
        #endregion

        #region GetPayForm

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outTradeNO"></param>
        /// <param name="amount"></param>
        /// <param name="autosumit"></param>
        /// <returns></returns>
        public string GetPayForm(string outTradeNO, decimal amount,bool autosumit)
        {
            string alipayNotifyURL = this.alipayNotifyURL;
            string[] strArray2 = BubbleSort(new string[] { "service=" + service, "partner=" + SuppAccount, "subject=" + subject, "body=" + body, "out_trade_no=" + outTradeNO, "total_fee=" + amount.ToString(), "show_url=" + show_url, "payment_type=" + payment_type, "seller_email=" + SuppUserName, "notify_url=" + notify_url, "return_url=" + return_url });
            StringBuilder plain = new StringBuilder();
            for (int num = 0; num < strArray2.Length; num++)
            {
                if (num == (strArray2.Length - 1))
                {
                    plain.Append(strArray2[num]);
                }
                else
                {
                    plain.Append(strArray2[num] + "&");
                }
            }
            plain.Append(SuppKey);
            string sign = GetMD5(plain.ToString(), this._input_charset);

            StringBuilder formHtml = new StringBuilder();
            formHtml.AppendFormat("<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"{0}\">", alipayNotifyURL);
            for (int num = 0; num < strArray2.Length; num++)
            {
                string[] parm = strArray2[num].Split('=');
                formHtml.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />",parm[0],parm[1]);
            }
            formHtml.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "sign", sign);
            formHtml.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "sign_type", sign_type);
            formHtml.Append("</form>");

            if (autosumit)
            {
                formHtml.Append("<script>document.forms[0].submit();</script> ");
            }

            LogWrite("formHtml=>" + formHtml);

            return formHtml.ToString();
        }
        #endregion

        #region Return
        /// <summary>
        /// 
        /// </summary>
        public void Return()
        {
            string puserid = SuppAccount;
            string pusername = SuppUserName;
            string puserkey = SuppKey;

            this.alipayNotifyURL = this.alipayNotifyURL + "service=notify_verify&partner=" + puserid + "&notify_id=" + HttpContext.Current.Request.QueryString["notify_id"];
            string str4 = this.Get_Http(this.alipayNotifyURL, 0x1d4c0);
            string[] strArray2 = BubbleSort(HttpContext.Current.Request.QueryString.AllKeys);


            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < strArray2.Length; i++)
            {
                if (((HttpContext.Current.Request.Form[strArray2[i]] != "") && (strArray2[i] != "sign")) && (strArray2[i] != "sign_type"))
                {
                    if (i == (strArray2.Length - 1))
                    {
                        builder.Append(strArray2[i] + "=" + HttpContext.Current.Request.QueryString[strArray2[i]]);
                    }
                    else
                    {
                        builder.Append(strArray2[i] + "=" + HttpContext.Current.Request.QueryString[strArray2[i]] + "&");
                    }
                }
            }
            builder.Append(puserkey);
            string str5 = GetMD5(builder.ToString(), this._input_charset);
            string str6 = HttpContext.Current.Request.QueryString["sign"];
            string str7 = HttpContext.Current.Request.QueryString["trade_status"];
            string s = string.Empty;
            string outorderid = string.Empty;
            decimal result = 0M;
            if (HttpContext.Current.Request.QueryString["out_trade_no"] != null)
            {
                s = HttpContext.Current.Request.QueryString["out_trade_no"].Trim();
            }
            if ((HttpContext.Current.Request.QueryString["trade_no"] != null) && (HttpContext.Current.Request.QueryString["trade_no"].Trim().Length > 1))
            {
                outorderid = HttpContext.Current.Request.QueryString["trade_no"].Trim();
            }
            if (((HttpContext.Current.Request.QueryString["total_fee"] != null) && (HttpContext.Current.Request.QueryString["total_fee"].Trim().Length > 0)) && !decimal.TryParse(HttpContext.Current.Request.QueryString["total_fee"].Trim(), out result))
            {
                result = 0M;
            }

            string opstate = "-1";
            int status = 4;
            string msg = string.Empty;
            if (str5 == str6)
            {
                switch (str7)
                {
                    case "TRADE_FINISHED":
                    case "TRADE_SUCCESS":
                        opstate = "0";
                        status = 2;
                        break;
                    default:
                        msg = str7;
                        break;
                }

                OrderBankUtils.SuppPageReturn(SuppId
                       , s
                       , outorderid
                       , status
                       , opstate
                       , msg
                       , result, 0M);
            }
            else
            {
                HttpContext.Current.Response.Write("<script>alert('出现异常！请查看充值是否到帐。若未到帐，请与管理员联系。');</script>");
            }
        }
        #endregion

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            string puserid = SuppAccount;
            string pusername = SuppUserName;
            string puserkey = SuppKey;


            this.alipayNotifyURL = this.alipayNotifyURL + "service=notify_verify&partner=" + puserid + "&notify_id=" + HttpContext.Current.Request.Form["notify_id"];
            string str4 = this.Get_Http(this.alipayNotifyURL, 0x1d4c0);
            string[] strArray2 = BubbleSort(HttpContext.Current.Request.Form.AllKeys);
            string str5 = "";
            for (int i = 0; i < strArray2.Length; i++)
            {
                if (((HttpContext.Current.Request.Form[strArray2[i]] != "") && (strArray2[i] != "sign")) && (strArray2[i] != "sign_type"))
                {
                    if (i == (strArray2.Length - 1))
                    {
                        str5 = str5 + strArray2[i] + "=" + HttpContext.Current.Request.Form[strArray2[i]];
                    }
                    else
                    {
                        str5 = str5 + strArray2[i] + "=" + HttpContext.Current.Request.Form[strArray2[i]] + "&";
                    }
                }
            }
            string str6 = GetMD5(str5 + puserkey, this._input_charset);
            string str7 = HttpContext.Current.Request.Form["sign"];
            if (str6 == str7)
            {
                string opstate = "-1";
                int status = 4;
                string msg = "支付失败";
                
                string s = string.Empty;
                string outorderid = string.Empty;
                decimal result = 0M;
                if ((HttpContext.Current.Request.Form["out_trade_no"] != null) && (HttpContext.Current.Request.Form["out_trade_no"].Trim().Length > 1))
                {
                    s = HttpContext.Current.Request.Form["out_trade_no"].Trim();
                }
                if ((HttpContext.Current.Request.Form["trade_no"] != null) && (HttpContext.Current.Request.Form["trade_no"].Trim().Length > 1))
                {
                    outorderid = HttpContext.Current.Request.Form["trade_no"].Trim();
                }
                if (((HttpContext.Current.Request.Form["total_fee"] != null) && (HttpContext.Current.Request.Form["total_fee"].Trim().Length > 0)) && !decimal.TryParse(HttpContext.Current.Request.Form["total_fee"].Trim(), out result))
                {
                    result = 0M;
                }
                if ((HttpContext.Current.Request.Form["trade_status"] == "TRADE_FINISHED") || (HttpContext.Current.Request.Form["trade_status"] == "TRADE_SUCCESS"))
                {
                    msg = "失败成功";
                    opstate = "0";
                    status = 2;
                }
                else
                {
                    msg = "支付失败 状态号：" + HttpContext.Current.Request.Form["trade_status"];
                }

                OrderBankUtils.SuppNotify(SuppId
                      , s
                      , outorderid
                      , status
                      , opstate
                      , msg
                      , result,result
                      , Succflag
                      , Failflag);
            }
        }
        #endregion

        #region 方法
        #region BubbleSort
        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static string[] BubbleSort(string[] r)
        {
            for (int i = 0; i < r.Length; i++)
            {
                bool flag = false;
                for (int j = r.Length - 2; j >= i; j--)
                {
                    if (string.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        string str = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = str;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return r;
                }
            }
            return r;
        }
        #endregion

        #region GetMD5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="_input_charset"></param>
        /// <returns></returns>
        public static string GetMD5(string s, string _input_charset)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }
        #endregion

        #region Get_Http
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strUrl"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public string Get_Http(string a_strUrl, int timeout)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(a_strUrl);
                request.Timeout = timeout;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                StringBuilder builder = new StringBuilder();
                while (-1 != reader.Peek())
                {
                    builder.Append(reader.ReadLine());
                }
                return builder.ToString();
            }
            catch (Exception exception)
            {
                return ("错误：" + exception.Message);
            }
        }
        #endregion
        #endregion
    }
}

