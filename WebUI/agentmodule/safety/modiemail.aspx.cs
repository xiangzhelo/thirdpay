using System;
using System.Globalization;
using System.Web;
using viviapi.BLL.User;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviapi.WebComponents.Template;
using viviapi.WebComponents.Web;
using viviLib.Security;

namespace viviAPI.WebUI7uka.agentmodule.safety
{
    public partial class Modiemail : AgentPageBase
    {
        public bool Isoldmail = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        void InitForm()
        {
            IsEmail.Value = CurrentUser.IsEmailPass.ToString(CultureInfo.InvariantCulture);
        }


        #region Save
        /// <summary>
        /// 
        /// </summary>
        private void Save()
        {
            string msg = "";
            #region email
            if (CurrentUser != null)
            {
                string email    = txtemail.Value.Trim();
                string newemail = txtnewemail.Value.Trim();

                if (string.IsNullOrEmpty(email))
                {
                    msg = "请输入当前邮箱。";
                }
                else if (email != CurrentUser.Email)
                {
                    msg = "当前邮件账号输入不正确 请重新输入";
                }
                else if (newemail == email)
                {
                    msg = "新邮箱不能原邮箱一样;";
                }
                else if (!viviLib.Text.PageValidate.IsEmail(email))
                {
                    msg = "新邮箱格式不正确;";
                }

                if (string.IsNullOrEmpty(msg))
                {
                    if (CurrentUser.IsEmailPass == 1)
                    {
                        msg = SendChange_email(newemail);
                    }
                    else
                    {
                        CurrentUser.Email = newemail;
                        CurrentUser.IsEmailPass = 0;

                        if (Factory.Update(CurrentUser, null))
                        {
                            msg = "修改成功";
                        }
                        else
                        {
                            msg = "修改失败";
                        }
                    }
                }
            }
            else
            {
                msg = "用户不存在";
            }
            #endregion

            this.lblMessage.Text = msg;
        }
        #endregion

        #region SendChange_email
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newemail"></param>
        /// <returns></returns>
        public string SendChange_email(string newemail)
        {
            string message;

            try
            {
                string tempcontent = Helper.GetEmailChangeTemp();
                if (!string.IsNullOrEmpty(tempcontent))
                {
                    var itemInfo = new EmailCheckInfo
                    {
                        userid = CurrentUser.ID,
                        status = EmailCheckStatus.提交中,
                        addtime = DateTime.Now,
                        checktime = DateTime.Now,
                        email = newemail,
                        typeid = EmailCheckType.修改,
                        Expired = DateTime.Now.AddDays(7)
                    };

                    var bll = new EmailCheck();
                    int result = bll.Add(itemInfo);
                    if (result > 0)
                    {
                        string parms = string.Format("id={0}&", result);
                        string securityKey = HttpUtility.UrlEncode(Cryptography.RijndaelEncrypt(parms));
                        string verifyurl = GetVerifyUrl(securityKey);

                        tempcontent = tempcontent.Replace("{#personName#}", CurrentUser.full_name);
                        tempcontent = tempcontent.Replace("{#useremail#}", newemail);
                        string sitename = "";
                        string sitedomain = "";
                        if (webInfo != null)
                        {
                            sitename = webInfo.Name;
                            sitedomain = webInfo.Domain;
                        }
                        tempcontent = tempcontent.Replace("{#sitename#}", sitename);
                        tempcontent = tempcontent.Replace("{#sitedomain#}", sitedomain);
                        tempcontent = tempcontent.Replace("{#verify_email#}", verifyurl);

                        var emailcom = new EmailHelper(CurrentUser.Email
                       , CurrentUser.Email + "修改邮箱"
                       , tempcontent
                       , true
                       , System.Text.Encoding.Default);

                        emailcom.Send2();

                        message = "操作成功";
                    }
                    else
                    {
                        message = "系统出错，请联系管理员";
                    }
                }
                else
                {
                    message = "系统出错,未找到邮件模版!";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
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

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
