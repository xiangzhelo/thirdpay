using System;
using viviapi.WebComponents.Web;
using viviLib.Security;

namespace viviAPI.WebUI7uka.agent
{
    public partial class ChangePwd : AgentPageBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string oldpwd = this.old_password.Value;
            if (string.IsNullOrEmpty(oldpwd))
            {
                this.lblMessage.Text = "请输入旧密码";
                return;
            }
            string newpwd = this.pas.Value;
            if (string.IsNullOrEmpty(newpwd))
            {
                this.lblMessage.Text = "请输入新密码";
                return;
            }
            if (newpwd != this.re_password.Value)
            {
                this.lblMessage.Text = "二次密码不一致";
                return;
            }
            oldpwd = Cryptography.MD5(oldpwd);
            if (oldpwd != this.CurrentUser.Password)
            {
                this.lblMessage.Text = "旧密码输入错误！请重试。";
                return;
            }
            newpwd = Cryptography.MD5(newpwd);
            CurrentUser.Password = Cryptography.MD5(newpwd);

            if (viviapi.BLL.User.Factory.Update(CurrentUser, null))
            {
                this.lblMessage.Text = "修改成功！";
                return;
            }
            else
            {
                this.lblMessage.Text = "修改失败！请重试。";
                return;
            }          
        }
    }
}