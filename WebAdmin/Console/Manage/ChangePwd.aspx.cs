using System;
using System.Globalization;
using viviapi.BLL;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Security;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Manage
{
    public partial class ChangePwd : ManagePageBase
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
                if (_itemInfo == null && ItemInfoId> 0)
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
                this.lblMessage.Text = "请输入当前登录密码";
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
            if (oldpwd != ItemInfo.password)
            {
                this.lblMessage.Text = "旧密码输入错误！请重试。";
                return;
            }
            newpwd = Cryptography.MD5(newpwd);
            ItemInfo.password = newpwd;

            if (ManageFactory.Update(ItemInfo))
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