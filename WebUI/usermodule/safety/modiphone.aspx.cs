using System;
using System.Globalization;
using viviapi.BLL.Sys;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.usermodule.safety
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Modiphone : UserPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        void InitForm()
        {
            IsPhonePass.Value = CurrentUser.IsPhonePass.ToString(CultureInfo.InvariantCulture);

            if (CurrentUser.IsPhonePass == 0)
            {
                this.litphone.Text = "<input name=\"phone\" type=\"text\" id=\"phone\" value=\"\" maxlength=\"11\" class=\"txt_01\" size=\"30\"/><input name=\"getdate\" value=\"new\" type=\"hidden\" />";
                this.litphone.Text += "<em class=\"txtr b_m_l\">* <a href=\"javascript:;\" id=\"sendmsg\" class=\"btn btn-primary\">发送验证码</a></em>    ";
            }
            else
            {
                this.litphone.Text = string.Format("<em id=\"phoneinput\">{0} <a href=\"javascript:;\" class=\"btn btn-primary\"> 修改</a></em>", UserViewTel);
                this.litphone.Text += "<span id=\"phoneputbox\"><input name=\"phone\" type=\"text\" id=\"phone\" value=\"\" maxlength=\"11\" class=\"txt_01\" size=\"30\"/><em class=\"txtr b_m_l b_m_r\">* <a href=\"javascript:;\" id=\"sendmsg\" class=\"btn btn-primary\">发送验证码</a></em>&nbsp;&nbsp;<em id=\"phoneinput_close\"><a href=\"javascript:;\" class=\"btn btn-primary\"> 取消</a></em></span>";

            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;

            string action = GetPostValue("action");
            string yphoneStr = GetPostValue("yphone");
            string phone = GetPostValue("phone");
            string getdate = GetPostValue("getdate");
            string phonecodeStr = GetPostValue("phonecode");

            string cacheKey = string.Format(Constant.PhoneVerificationCacheKey, phone);

            string validcode = (string)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

            if (CurrentUser.IsPhonePass == 0)
            {
                if (string.IsNullOrEmpty(phone))
                {
                    msg = "请输入手机号码!";
                }
                else if (string.IsNullOrEmpty(phone))
                {
                    msg = "请输入手机认验码!";
                }
                else if (phonecodeStr != validcode)
                {
                    msg = "手机验证码不正确!";
                }
                else
                {
                    CurrentUser.IsPhonePass = 1;
                    CurrentUser.Tel = phone;
                }
            }
            else 
            {
                if (string.IsNullOrEmpty(yphoneStr))
                {
                    msg = "请输入原手机号码!";
                }
                else if (yphoneStr != CurrentUser.Tel)
                {
                    msg = "原手机号码输入错误!";
                }
                else if (string.IsNullOrEmpty(phone))
                {
                    msg = "请输入新手机号码!";
                }
                else if (yphoneStr == phone)
                {
                    msg = "原手机号码和新手机号码一样!";
                }
                else if (string.IsNullOrEmpty(phonecodeStr))
                {
                    msg = "请输入手机认验码!";
                }
                else if (phonecodeStr != validcode)
                {
                    msg = "手机验证码不正确!";
                }
                else
                {
                    CurrentUser.Tel = phone;
                }
            }
            if (string.IsNullOrEmpty(msg))
            {
                if (viviapi.BLL.User.Factory.Update(CurrentUser, null))
                {
                    msg = "操作成功";
                }
                else
                {
                    CurrentUser.IsPhonePass = 0;
                    CurrentUser.Tel = string.Empty;
                    msg = "修改失败";
                }
            }

            AlertAndRedirect(msg);

            //if (!string.IsNullOrEmpty(msg))
            //{
            //    this.lblMessage.Visible = true;
            //    this.lblMessage.Text = msg;
            //}
        }
    }
}
