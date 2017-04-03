using System;
using viviapi.WebComponents.Web;
using viviLib.Security;

namespace viviAPI.WebAdmin.Console
{
    public partial class ChangePwd : ManagePageBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
           
        }
        #endregion

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
            if (oldpwd != currentManage.password)
            {
                this.lblMessage.Text = "旧密码输入错误！请重试。";
                return;
            }
            newpwd = Cryptography.MD5(newpwd);
            currentManage.password = newpwd;

            //二级密码
            oldpwd = this.oldsedpwd.Value;
            if (!string.IsNullOrEmpty(oldpwd))
            {
                oldpwd = Cryptography.MD5(oldpwd);
                if (oldpwd != currentManage.secondpwd)
                {
                    this.lblMessage.Text = "旧二级密码输入错误！请重试。";
                    return;
                }

                newpwd = this.newsedpwd.Value;
                if (string.IsNullOrEmpty(newpwd))
                {
                    this.lblMessage.Text = "请输入新二级密码";
                    return;
                }
                if (newpwd != this.newsedpwd2.Value)
                {
                    this.lblMessage.Text = "二次二级密码不一致";
                    return;
                }
                newpwd = Cryptography.MD5(newpwd);
                currentManage.secondpwd = newpwd;
            }
           

            if (viviapi.BLL.ManageFactory.Update(currentManage))
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