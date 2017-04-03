using System;
using System.Globalization;
using viviapi.BLL.User;
using viviapi.WebComponents.Web;
using viviLib.Security;

namespace viviAPI.WebUI7uka.usermodule.safety
{
    public partial class Repassword : UserPageBase
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
            txtuserid.Attributes["readonly"] = "true";
            txtusername.Attributes["readonly"] = "true";


            txtuserid.Value = CurrentUser.ID.ToString(CultureInfo.InvariantCulture);
            txtusername.Value = CurrentUser.UserName;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;

            //string email = viviLib.XRequest.GetString("email");
            string oldpass = this.txtoldpassword.Value.Trim();
            string newpass = this.txtnewpassword.Value.Trim();
            string repass = this.txtrepassword.Value.Trim();

            if (Cryptography.MD5(oldpass) != this.CurrentUser.Password)
            {
                msg = "旧密码不正确";
            }
            //else if (email != viviapi.BLL.User.Login.CurrentMember.Email)
            //{
            //    msg = "邮件地址不正确";
            //}
            else if (newpass != repass)
            {
                msg = "两次密码不一致";
            }
            else if (newpass == oldpass)
            {
                msg = "新密码不能为新一样";
            }
            if (string.IsNullOrEmpty(msg))
            {
                CurrentUser.Password = Cryptography.MD5(newpass);
                if (Factory.Update(CurrentUser, null))
                {
                    msg = "true";
                }
                else
                {
                    msg = "更新失败";
                }
            }

            if (msg.Equals("true"))
            {
                ShowMessageBox("修改成功");
            }
            else
            {
                ShowMessageBox(msg);
            }
        }
    }
}
