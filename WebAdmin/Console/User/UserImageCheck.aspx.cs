using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using viviapi.WebComponents;
using viviLib.Web;
using viviapi.Model;
using viviapi.BLL;
using viviapi.Model.User;
using viviapi.BLL.User;
using viviapi.Model.Payment;
using System.Collections.Generic;

namespace viviapi.web.Manage.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserImageCheck : viviapi.WebComponents.Web.ManagePageBase
    {
        /// <summary>
        /// 
        /// </summary>
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }
        public usersIdImageInfo _ItemInfo = null;
        public usersIdImageInfo ItemInfo
        {
            get
            {
                if (_ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        viviapi.BLL.User.usersIdImage bll = new usersIdImage();
                        _ItemInfo = bll.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        _ItemInfo = new usersIdImageInfo();
                    }
                }
                return _ItemInfo;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public UserInfo _userInfo = null;
        public UserInfo model
        {
            get
            {
                if (_userInfo == null && ItemInfo != null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        _userInfo = Factory.GetModel(this.ItemInfo.userId.Value);
                    }
                    else
                    {
                        _userInfo = new UserInfo();
                    }
                }
                return _userInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (this.ItemInfo.status != IdImageStatus.审核中)
            {
                this.btnFail.Enabled = false;
                this.btnOK.Enabled = false;
            }
            if (!this.IsPostBack)
            {
                ShowInfo();
            }
        }

        #region ShowInfo
        /// <summary>
        /// 
        /// </summary>
        void ShowInfo()
        {
            if (model != null)
            {
                this.lblid.Text = model.ID.ToString();
                this.txtuserName.Text = model.UserName;
                txtRealName.Text = model.PayeeName;
                this.txtidCard.Text = model.IdCard;
                this.txtWhy.Text = ItemInfo.why;
            }
        }
        #endregion

        #region Save
        void Save(string cmd)
        {
            usersIdImageInfo update = new usersIdImageInfo();
            update.id = ItemInfoId;
            if (cmd == "ok")
            {
                update.status = IdImageStatus.审核成功;
            }
            if (cmd == "fail")
            {
                update.status = IdImageStatus.审核失败;
            }
            update.why = this.txtWhy.Text.Trim();
            update.checktime = DateTime.Now;
            update.admin = this.ManageId;
            update.userId = ItemInfo.userId;
            BLL.User.usersIdImage bll = new viviapi.BLL.User.usersIdImage();
            if (bll.Check(update))
            {
                WebUtility.AlertAndClose(this, "操作成功");
                //AlertAndRedirect("操作成功", "UserIdImgList.aspx?s=1");
            }
            else
            {
                WebUtility.AlertAndClose(this, "操作失败");
            }
        }
        #endregion

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Merchant);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Save("ok");
        }
        protected void btnFail_Click(object sender, EventArgs e)
        {
            Save("fail");
        }
    }
}