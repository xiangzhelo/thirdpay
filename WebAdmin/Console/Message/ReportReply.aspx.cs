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

namespace viviapi.web.Manage.Jubao
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ItemModi : viviapi.WebComponents.Web.ManagePageBase
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
        public JuBaoInfo _ItemInfo = null;
        public JuBaoInfo ItemInfo
        {
            get
            {
                if (_ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        viviapi.BLL.JuBao bll = new viviapi.BLL.JuBao();
                        _ItemInfo = bll.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        _ItemInfo = new JuBaoInfo();
                    }
                }
                return _ItemInfo;
            }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();

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
            if (ItemInfo != null)
            {
                this.txtname.Text = ItemInfo.name;
                this.txtemail.Text = ItemInfo.email;
                this.txttel.Text = ItemInfo.tel;
                this.txturl.Text = ItemInfo.url;
                this.ddltype.SelectedValue = ItemInfo.type.ToString();
                this.txtremark.Text = ItemInfo.remark;
                this.txtaddtime.Text = ItemInfo.addtime.ToString();
                this.ddlstatus.SelectedValue = ItemInfo.status.ToString();
                this.txtchecktime.Text = ItemInfo.checktime.ToString();
                this.txtcheck.Text = ItemInfo.check.ToString();
                this.txtcheckremark.Text = ItemInfo.checkremark;
                this.txtpwd.Text = ItemInfo.pwd;
                this.txtfield1.Text = ItemInfo.field1;

            }
        }
        #endregion

        #region Save
        void Save()
        {
            ItemInfo.status = (JuBaoStatusEnum)Convert.ToInt32(this.ddlstatus.SelectedValue);
            ItemInfo.checktime = DateTime.Now;
            ItemInfo.check = this.ManageId;
            ItemInfo.checkremark = txtcheckremark.Text;

            BLL.JuBao bll = new viviapi.BLL.JuBao();
            if (bll.Update(ItemInfo))
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
            Save();
        }
    }
}