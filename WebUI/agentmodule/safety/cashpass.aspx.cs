using System;
using System.Globalization;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.Security;

namespace viviAPI.WebUI7uka.agentmodule.safety
{
    public partial class Cashpass : AgentPageBase
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

            if (CurrentUser != null)
            {
                txtuserid.Value = CurrentUser.ID.ToString(CultureInfo.InvariantCulture);
                txtusername.Value = CurrentUser.UserName;
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;

            string loginpass = txtloginpwd.Value;
            string email = txtmail.Value;
            string newpass = txtcashpass.Value;
            string repass = txtrecashpass.Value;

            if (Cryptography.MD5(loginpass) != this.CurrentUser.Password)
            {
                msg = "登录密码不正确";
            }
            if (email != CurrentUser.Email)
            {
                msg = "安全邮箱不正确";
            }
            if (newpass != repass)
            {
                msg = "两次密码输入不一致";
            }
            if (string.IsNullOrEmpty(msg))
            {
                CurrentUser.Password2 = Cryptography.MD5(newpass);
                if (CurrentUser.Password2 == CurrentUser.Password)
                {
                    msg = "登录密码与提现密码不能相同";
                }
                else
                {
                    if (Factory.Update(CurrentUser, null))
                    {
                        msg = "设置成功!";
                    }
                    else
                    {
                        msg = "更新失败";
                    }
                }
            }
            lblMessage.Visible = true;
            lblMessage.Text = msg;
        }
    }
}
