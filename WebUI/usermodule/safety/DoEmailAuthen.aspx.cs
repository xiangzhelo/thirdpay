using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib.Text;

namespace viviAPI.WebUI7uka.usermodule.safety
{
    public partial class DoEmailAuthen : UserPageBase
    {
        readonly EmailCheck _emailCheck = new EmailCheck();

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (CurrentUser.IsEmailPass == 0)
                {
                    this.txtnewemail.Value = CurrentUser.Email;
                }
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            string email = this.txtnewemail.Value.Trim();
            if (string.IsNullOrEmpty(email))
            {
                msg = "请输入邮件";
            }
            else if (!PageValidate.IsEmail(email))
            {
                msg = "邮件格式不正确";
            }
            else if (CurrentUser.IsEmailPass == 1)
            {
                msg = "邮件已通过认证";
            }
            else
            {
                msg = SendMail(email);
            }
            ShowMessageBox(msg);
        }

        #region SendMail
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        string SendMail(string email)
        {
            string msg = string.Empty;
            try
            {
                string tempcontent = viviapi.WebComponents.Template.Helper.GetEmailAuthenticateTemp();
                if (!string.IsNullOrEmpty(tempcontent))
                {
                    var itemInfo = new EmailCheckInfo
                    {
                        userid = UserId,
                        status = EmailCheckStatus.提交中,
                        addtime = DateTime.Now,
                        checktime = DateTime.Now,
                        email = email,
                        typeid = EmailCheckType.认证,
                        Expired = DateTime.Now.AddDays(7)
                    };


                    int result = _emailCheck.Add(itemInfo);
                    if (result > 0)
                    {
                        string parms = string.Format("id={0}&", result);
                        string securityKey = HttpUtility.UrlEncode(viviLib.Security.Cryptography.RijndaelEncrypt(parms));
                        string verifyurl = GetVerifyUrl(securityKey);

                        //Session["ReverifyEmail"] = email;
                        //Session["ReverifyParms"] = securityKey;

                        tempcontent = tempcontent.Replace("{#personName#}", CurrentUser.full_name);
                        tempcontent = tempcontent.Replace("{#useremail#}", email);
                        tempcontent = tempcontent.Replace("{#sitename#}", SiteName);
                        tempcontent = tempcontent.Replace("{#sitedomain#}", webInfo.Domain);
                        tempcontent = tempcontent.Replace("{#verify_email#}", verifyurl);

                        var emailcom = new EmailHelper(email
                       , email + "绑定邮箱"
                       , tempcontent
                       , true
                       , System.Text.Encoding.Default);

                        emailcom.Send2();

                        msg = "操作已提交，请查收邮件。";

                    }
                }
                else
                {
                    msg = "未找到模板，请联系客服。";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            //AlertAndRedirect(msg);
            return msg;
        }
        #endregion

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

    }
}
