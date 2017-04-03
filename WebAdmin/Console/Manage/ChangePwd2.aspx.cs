using System;
using System.Globalization;
using viviapi.BLL;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Security;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Manage
{
    public partial class ChangePwd2 : ManagePageBase
    {

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        private viviapi.Model.Manage _itemInfo = null;
        public viviapi.Model.Manage ItemInfo
        {
            get
            {
                if (_itemInfo == null && ItemInfoId > 0)
                {
                    _itemInfo = ManageFactory.GetModel(ItemInfoId);
                }
                return _itemInfo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();

            if (!this.IsPostBack)
            {
                if (ItemInfo == null)
                {
                    Response.Write("err");
                }
                else
                {
                    this.lblManage.Text = ItemInfo.username + " " + ItemInfo.relname + "(" + ItemInfo.id.ToString(CultureInfo.InvariantCulture) +
                                          ")";
                }
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(true
  , ManageRole.None);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
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
                this.lblMessage.Text = "请输入您的登录密码";
                return;
            }

            oldpwd = Cryptography.MD5(oldpwd);
            if (oldpwd != ItemInfo.password)
            {
                this.lblMessage.Text = "登录密码输入错误！请重试。";
                return;
            }

            //二级密码
            oldpwd = this.oldsedpwd.Value;
            if (!string.IsNullOrEmpty(oldpwd))
            {
                oldpwd = Cryptography.MD5(oldpwd);
                if (oldpwd != ItemInfo.secondpwd)
                {
                    this.lblMessage.Text = "您的二级密码输入错误！请重试。";
                    return;
                }
            }

            string newpwd = this.newsedpwd.Value;
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
            ItemInfo.secondpwd = newpwd;


            if (viviapi.BLL.ManageFactory.Update(ItemInfo))
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