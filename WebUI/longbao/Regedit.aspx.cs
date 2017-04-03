using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Sys;
using viviapi.BLL.User;
using viviapi.Model.Promotion;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib.Security;
using viviLib.Web;


namespace viviAPI.WebUI2015.longbao
{
    public partial class Regedit : PageBase
    {
        protected EmailCheck EmailCheckBll = new EmailCheck();

        protected string UserNameRegex = "^\\w+$";
        protected string EmailRegex = "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$";
        protected string PasswordRegex = "^(?![a-zA-Z]+$)(?![0-9]+$)[a-zA-Z0-9]*$";//登录密码，8-20位数字加字母，不能全部是数字或者全部是字母
        protected string PhoneRegex = "^(13|14|15|18)[0-9]{9}$";
        protected string QQRegex = "^[1-9]*[1-9][0-9]*$";
        protected string ChineseRegex = "^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$";

        #region 参数
        /// <summary>
        /// 业务员
        /// </summary>
        public int SalesmanId
        {
            get
            {
                return WebBase.GetQueryStringInt32("s", 0);

            }
        }

        /// <summary>
        /// 代理商
        /// </summary>
        public int AgentId
        {
            get
            {
                return WebBase.GetQueryStringInt32("agent", 0);

            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!viviapi.BLL.Sys.RegisterSettings.RegisterOpen)
            {
                AlertAndRedirect("接口暂不开放注册", "index.aspx");
            }
        }
        string GetParamValue(string paramName)
        {
            return viviLib.Web.WebBase.GetFormString(paramName, string.Empty);
        }

        void ShowError(string errMsg)
        {
            litError.Visible = true;
            litError.Text = string.Format("<br>&nbsp;&nbsp;<span class=\"txt_ERROR\">{0}</span><br>", errMsg);
        }

        #region SendMail
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        string SendMail(string username, int userId, string email)
        {
            string message = "";
            try
            {
                string tempcontent = viviapi.WebComponents.Template.Helper.GetEmailRegisterTemp();
                if (!string.IsNullOrEmpty(tempcontent))
                {
                    var itemInfo = new EmailCheckInfo
                    {
                        userid = userId,
                        status = EmailCheckStatus.提交中,
                        addtime = DateTime.Now,
                        checktime = DateTime.Now,
                        email = email,
                        typeid = EmailCheckType.注册,
                        Expired = DateTime.Now.AddDays(7)
                    };

                    int result = EmailCheckBll.Add(itemInfo);
                    if (result > 0)
                    {
                        string parms = string.Format("id={0}&", result);
                        string securityKey = HttpUtility.UrlEncode(Cryptography.RijndaelEncrypt(parms));
                        string verifyurl = GetVerifyUrl(securityKey);

                        tempcontent = tempcontent.Replace("{#useremail#}", email);
                        tempcontent = tempcontent.Replace("{#username#}", username);
                        tempcontent = tempcontent.Replace("{#sitename#}", webInfo.Name);
                        tempcontent = tempcontent.Replace("{#sitedomain#}", webInfo.Domain);
                        tempcontent = tempcontent.Replace("{#verify_email#}", verifyurl);

                        var emailcom = new EmailHelper(email
                       , email + "账号激活"
                       , tempcontent
                       , true
                       , System.Text.Encoding.GetEncoding("gbk"));

                        if (emailcom.Send2())
                        {
                            message = "注册成功，请到注册邮箱完成账号激活！";
                        }
                        else
                        {
                            message = "注册成功，激活邮件发送失败！";
                        }
                    }
                }
                else
                {
                    message = "未找到模板，请联系商务";
                }
            }
            catch (Exception ex)
            {
                message = "系统错误，请联系商务!";
            }

            return message;
        }

        #region GetVerifyUrl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public string GetVerifyUrl(string parms)
        {
            return WebUtility.GetCurrentHost() + "/merchant/selfservice.aspx?parms=" + parms;
        }
        #endregion


        #endregion

        protected void iBtnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            string vCode = GetPostValue("txtvcode");
            ;
            string errMsg = WebUtility.CheckValiDateCode(vCode);
            if (!string.IsNullOrEmpty(errMsg))
            {
                ShowError(errMsg);
                return;
            }

            //用户类型
            int userclassid = 0;

            string username = newusername.Value.Trim();
            string password1 = this.password1.Value.Trim();
            string password2 = this.password2.Value.Trim();
            string email = newemail.Value.Trim();
            string sitename = "";
            string siteurl = "";
            string mobile = "";// GetPostValue("newmobile").Trim();
            string idcard = string.Empty;
            string fullname = newfullname.Value.Trim();
            string qq = newqq.Value.Trim();
            string question = string.Empty;
            string answer = string.Empty;

            string fBank = GetPostValue("fBank").Trim();
            string fAccountName = GetPostValue("fAccountName").Trim();
            string fAccount = GetPostValue("fAccount").Trim();
            string fProvince = GetPostValue("fProvince").Trim();
            string fCity = GetPostValue("fCity").Trim();
            string fSubBranch = GetPostValue("fSubBranch").Trim();

            #region check
            if (string.IsNullOrEmpty(username))
            {
                errMsg = "用户名不能为空!";
            }
            else if (!Regex.IsMatch(username, UserNameRegex))
            {
                errMsg = "用户名格式不正确!";
            }
            else if (Factory.Exists(username))
            {
                errMsg = "用户名已注册!";
            }

            if (string.IsNullOrEmpty(errMsg))
            {
                if (string.IsNullOrEmpty(password1))
                {
                    errMsg = "密码不能为空!";
                }
                else if (password1.Length < 6 || password1.Length > 32)
                {
                    errMsg = "密码长度不正确!";
                }
                else if (!password1.Equals(password2))
                {
                    errMsg = "密码与重复密码输入不一致!";
                }
            }

            if (string.IsNullOrEmpty(errMsg))
            {
                if (string.IsNullOrEmpty(email))
                {
                    errMsg = "安全邮箱不能为空!";
                }
                else if (!Regex.IsMatch(email, EmailRegex))
                {
                    errMsg = "安全邮箱格式不正确";
                }
                else if (Factory.EmailExists(email) != 999)
                {
                    errMsg = "安全邮箱已注册";
                }
            }

            if (string.IsNullOrEmpty(errMsg))
            {
                if (string.IsNullOrEmpty(fullname))
                {
                    errMsg = "真实姓名不能不空!";
                }
                else if (!Regex.IsMatch(fullname, ChineseRegex))
                {
                    errMsg = "真实姓名格式不正确";
                }
                else if (string.IsNullOrEmpty(qq))
                {
                    errMsg = "联系QQ不能不空!";
                }
                else if (!Regex.IsMatch(qq, QQRegex))
                {
                    errMsg = "QQ格式不正确";
                }
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                ShowError(errMsg);
                return;
            }
            #endregion

            var userinfo = new UserInfo
            {
                UserName = username,
                Password = Cryptography.MD5(password1),
                Email = email,
                QQ = qq,
                Tel = mobile,
                SiteName = sitename,
                SiteUrl = siteurl,
                IdCard = idcard,
                CPSDrate = RegisterSettings.DefaultCPSDrate,
                PMode = 1,
                PayeeBank = fBank,
                LinkMan = fullname,
                full_name = fullname,
                Status = RegisterSettings.RequiredAudit ? 1 : 2,
                LastLoginIp = ServerVariables.TrueIP,
                LastLoginTime = DateTime.Now,
                RegTime = DateTime.Now,
                Settles = 0
            };

            userinfo.UserLevel = RegisterSettings.DefaultUserLevel;
            //提现方案
            userinfo.MaxDayToCashTimes = SettleSettings.DefaultScheme;

            //默认结算模式 0:T+0 1:T+1
            //默认扣量比例
            userinfo.CPSDrate = RegisterSettings.DefaultCPSDrate;

            userinfo.AgentId = 0;
            userinfo.APIAccount = 0;
            userinfo.APIKey = viviapi.BLL.User.Factory.GenerateAPIKey(); //WebUtility.GenerateAPIKey();
            userinfo.question = question;
            userinfo.answer = answer;
            userinfo.classid = userclassid;

            if (this.SalesmanId > 0)
                userinfo.manageId = SalesmanId;

            userinfo.cardversion = RegisterSettings.DefaultCardVersion;

            string area = "";
            string province = "";
            string city = "";

            WebUtility.GetAreaInfo(out area, out province, out city);

            userinfo.province = province;
            userinfo.city = city;
            userinfo.Desc = area;

            int userId = Factory.Add(userinfo);
            if (userId > 0)
            {
                var config = new UserSettingInfo
                {
                    userid = userId,
                    RiskWarning = (byte)(TransactionSettings.RiskWarning ? 1 : 0),
                    AlipayRiskWarning = (byte)(TransactionSettings.RiskWarning_Alipay ? 1 : 0),
                    AliCodeRiskWarning = (byte)(TransactionSettings.RiskWarning_AliCode ? 1 : 0),
                    WxPayRiskWarning = (byte)(TransactionSettings.RiskWarning_WXpay ? 1 : 0),

                };



                viviapi.BLL.User.UserSetting.Instance.Insert(config);

                //UserPayBankAppInfo model = new UserPayBankAppInfo();
                //model.pmode = 1;
                //model.payeeBank = fBank;
                //model.bankProvince = fProvince;
                //model.bankCity = fCity;
                //model.bankAddress = fSubBranch;
                //model.account = fAccount;
                //model.payeeName = fAccountName;
                //model.AddTime = DateTime.Now;
                //model.userid = userId;
                //model.status = AcctChangeEnum.审核成功;
                //int infoId = BLL.User.UserPayBankApp.Add(model);
                //if (infoId > 0)
                //{
                //    model.id = infoId;
                //    model.status = AcctChangeEnum.审核成功;
                //    model.SureTime = DateTime.Now;
                //    model.SureUser = 0;

                //    BLL.User.UserPayBankApp.Check(model);
                //}

                if (AgentId > 0)
                {
                    #region
                    var promUser = new Promoter
                    {
                        PID = AgentId,
                        Prices = 0.5M,
                        RegId = userId,
                        PromTime = DateTime.Now,
                        PromStatus = 1
                    };

                    viviapi.BLL.Promotion.Factory.Insert(promUser);
                    #endregion
                }
                if (RegisterSettings.ActivationByEmail)
                {
                    errMsg = SendMail(username, userId, email);

                    AlertAndRedirect(errMsg, "/index.aspx");
                }
                else
                {
                    if (userinfo.Status == 2)
                    {
                        AlertAndRedirect("注册成功,无须审核请登陆后直接使用", "/index.aspx");
                    }
                    else
                    {
                        AlertAndRedirect("注册成功,请等待管理员审核", "/service.aspx");
                    }
                }
            }
            else
            {
                ShowError("注册失败");
            }
        }
    }
}