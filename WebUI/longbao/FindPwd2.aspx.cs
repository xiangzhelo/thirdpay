using System;
using System.Web;
using System.Web.UI.WebControls;
using viviapi.Model.User;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI2015.longbao
{
    public partial class FindPwd2 : PageBase
    {
        public string username
        {
            get
            {
                if (Session["findpwduser"] != null)
                {
                    return Session["findpwduser"].ToString();
                }
                return string.Empty;
            }
        }

        private UserInfo _user = null;
        public UserInfo userInfo
        {
            get
            {
                if (!string.IsNullOrEmpty(username) && _user == null)
                {
                    _user = viviapi.BLL.User.Factory.GetModelByName(username);
                }
                return _user;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (userInfo == null)
                    Response.Redirect("FindPwd.aspx");

                //this.Title = getTitle("自助找回密码- ");
                InitForm();
                changeCtrolStat();
            }
        }
        void InitForm()
        {
            if (ddlfindmode.Items.Count == 0)
            {
                string selectValue = string.Empty;
                if (userInfo != null)
                {
                    ListItem Item = new ListItem();
                    if (userInfo.IsPhonePass == 1)
                    {
                        Item = new ListItem("用手机找回", "1");
                        selectValue = "1";
                        ddlfindmode.Items.Add(Item);
                    }
                    if (!string.IsNullOrEmpty(userInfo.question))
                    {
                        litquestion.Text = userInfo.question;

                        Item = new ListItem("用密保找回", "3");
                        selectValue = "3";
                        ddlfindmode.Items.Add(Item);
                    }
                    if (userInfo.IsEmailPass == 1)
                    {
                        Item = new ListItem("用邮件找回", "2");
                        selectValue = "2";
                        ddlfindmode.Items.Add(Item);
                    }
                    lituserName.Text = userInfo.UserName;
                    ddlfindmode.SelectedValue = selectValue;
                }
            }
            ddlfindmode.AutoPostBack = true;
            ddlfindmode.SelectedIndexChanged += new EventHandler(ddlfindmode_SelectedIndexChanged);
        }

        void ddlfindmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeCtrolStat();
        }

        void changeCtrolStat()
        {
            string findmode = ddlfindmode.SelectedValue;
            p1.Visible = (findmode == "3");
            p2.Visible = (findmode == "3");
            p3.Visible = (findmode == "2");
            p4.Visible = (findmode == "1");
            this.showmsg.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string CheckValiDateNo(string str)
        {
            object obj2 = HttpContext.Current.Session["CCode"];
            if (obj2 == null)
            {
                return "验证码失效";
            }
            if (obj2.ToString().ToUpper().Equals(str.ToUpper()))
            {
                return "";
            }
            return "验证码不正确，请重新输入！";
        }

        protected void btnbacksubmit_Click(object sender, EventArgs e)
        {
            string errmsg = CheckValiDateNo(this.txtcheckCode.Value);
            if (!string.IsNullOrEmpty(errmsg))
            {
                this.showmsg.Attributes["class"] = "showno";
                this.showmsg.Visible = true;
                this.showmsg.InnerText = errmsg;
                return;
            }
            string findmode = ddlfindmode.SelectedValue;

            #region 手机找回
            if (findmode == "1")
            {
                if (this.txtphone.Value != userInfo.Tel)
                {
                    this.showmsg.Attributes["class"] = "showno";
                    this.showmsg.Visible = true;
                    this.showmsg.InnerText = "您输入的手机号码错误";
                    return;
                }
                else
                {
                    string phone = this.txtphone.Value;
                    string validcode = new Random().Next(100000, 999999).ToString();

                    string smscontext = string.Format("您正在请求重置账号密码,新密码为{0},[{1}]", validcode, viviapi.BLL.WebInfoFactory.CurrentWebInfo.Name);

                    if (viviapi.BLL.Tools.SMS.Send(phone, smscontext, ""))
                    {
                        userInfo.Password = viviLib.Security.Cryptography.MD5(validcode);
                        viviapi.BLL.User.Factory.Update(userInfo, null);

                        viviapi.Model.PhoneValidLog validlog = new viviapi.Model.PhoneValidLog();
                        validlog.phone = phone;
                        validlog.sendTime = DateTime.Now;
                        validlog.code = validcode;
                        validlog.clientIP = viviLib.Web.ServerVariables.TrueIP;

                        viviapi.BLL.PhoneValidFactory.Add(validlog);
                        if (viviapi.BLL.PhoneValidFactory.Add(validlog) > 0)
                        {
                            this.showmsg.Attributes["class"] = "showok";
                            this.showmsg.Visible = true;
                            this.showmsg.InnerText = "发送成功";
                            return;

                        }
                        else
                        {
                            this.showmsg.Attributes["class"] = "showno";
                            this.showmsg.Visible = true;
                            this.showmsg.InnerText = "发送失败";
                            return;
                        }
                    }
                    else
                    {
                        this.showmsg.Attributes["class"] = "showno";
                        this.showmsg.Visible = true;
                        this.showmsg.InnerText = "发送失败";
                        return;
                    }
                }
            }
            #endregion
            else if (findmode == "2")
            {
                if (this.txtuseremail.Value != userInfo.Email)
                {
                    this.showmsg.Attributes["class"] = "showno";
                    this.showmsg.Visible = true;
                    this.showmsg.InnerText = "邮件地址不正确";
                    return;
                }
                else
                {
                    string validcode = new Random().Next(100000, 999999).ToString();
                    userInfo.Password = viviLib.Security.Cryptography.MD5(validcode);
                    viviapi.BLL.User.Factory.Update(userInfo, null);

                    this.showmsg.InnerText = SendMail(validcode);
                    this.showmsg.Visible = true;
                    return;
                }
            }
            else if (findmode == "3")
            {
                if (this.txtanswer1.Value != userInfo.answer)
                {
                    this.showmsg.Attributes["class"] = "showno";
                    this.showmsg.Visible = true;
                    this.showmsg.InnerText = "问题答案不正确";
                    return;
                }
                else
                {
                    string validcode = new Random().Next(100000, 999999).ToString();
                    userInfo.Password = viviLib.Security.Cryptography.MD5(validcode);
                    viviapi.BLL.User.Factory.Update(userInfo, null);

                    this.showmsg.Attributes["class"] = "showok";
                    this.showmsg.InnerText = "操作成功！新密码为" + validcode;
                    this.showmsg.Visible = true;
                    return;
                }
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        string SendMail(string newpwd)
        {
            string msg = string.Empty;

            System.Text.StringBuilder HTML = new System.Text.StringBuilder();
            HTML.AppendFormat("<p>亲爱的{0}:<p>", userInfo.UserName);
            HTML.AppendFormat("<p style=\"font-size:14px\">您的密码已经被重置为：<font style=\"font-size:14px;font-weight:bold;color:blue\">{0}</font>，请立即登录后修改！</p>", newpwd);
            //HTML.AppendFormat("<p><a href=\"{0}user/verify_email.ashx?parms={1}\" style=\"color:#003300\">{0}user/verify_email.ashx?parms={1}</a></p>", webInfo.Domain, securityKey);
            //HTML.Append("<p style=\"color:#999;font-size:12px\">如果无法点击该URL链接地址，请将它复制并粘帖到浏览器的地址输入框，然后单击回车即可。");
            HTML.Append("<p><p>————————————————————————————————");
            //HTML.AppendFormat("<p style=\"font-size:14px;line-height:150%\">需要了解我们的最新动态请关注我们的腾讯微博：<a href=\"http://t.qq.com/cqbatian\">http://t.qq.com/cqbatian</a>&nbsp;或者新浪微博：<a href=\"http://weibo.com/cqbatian\">http://weibo.com/cqbatian</a><br  />祝您使用愉快！<br  />{0} <a href=\"{1}\">{1}</a>", SiteName, webInfo.Domain);
            HTML.AppendFormat("<p style=\"font-size:14px;line-height:150%\"> {0} 企业QQ：{1} 7x24咨询电话：{2}", SiteName, webInfo.Kfqq, webInfo.Phone);
            HTML.AppendFormat("<p><img src=\"{0}images/logo.gif\"  />  <!-- --><style>#mailContentContainer .txt {{height:auto;}}</style>", webInfo.Domain);

            viviapi.WebComponents.EmailHelper emailcom = new viviapi.WebComponents.EmailHelper(userInfo.Email
                    , userInfo.Email + "重置密码"
                    , HTML.ToString()
                    , true
                    , System.Text.Encoding.GetEncoding("gbk"));
            if (emailcom.Send())
            {
                msg = "操作成功 请登录邮箱查看新密码!";
            }
            else
            {
                msg = "邮件发送失败，请联系管理员";
            }
            return msg;
        }
        protected void type1_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeCtrolStat();
        }
    }
}