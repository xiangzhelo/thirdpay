using System;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Finance;
using viviapi.BLL.Sys;
using viviapi.Model;
using viviapi.WebComponents;
using viviLib;

namespace viviAPI.WebAdmin.Console
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SiteInfo : viviapi.WebComponents.Web.ManagePageBase
    {
        /// <summary>
        /// 
        /// </summary>
        private WebInfo _objectInfo = null;
        public WebInfo ObjectInfo
        {
            get
            {
                if (_objectInfo == null)
                {
                    _objectInfo = WebInfoFactory.GetWebInfoByDomain(XRequest.GetHost());
                }
                return _objectInfo;
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
                ddlDefaultScheme.Items.Add(new ListItem("--默认--", ""));
                DataTable data = TocashScheme.GetList("type=1").Tables[0];
                foreach (DataRow dr in data.Rows)
                {
                    ddlDefaultScheme.Items.Add(new ListItem(dr["schemename"].ToString(), dr["id"].ToString()));
                }

                this.InitForm();

                data = viviapi.BLL.Sys.SiteSettings.GetKeyValues();

                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        string key = row["key"].ToString();
                        string value = row["value"].ToString();

                        if (key == "WebSiteTitleSuffix")
                            this.txtTitleSuffix.Text = value;

                        else if (key == "KeyWords")
                            this.txtWebSiteKey.Text = value;

                        else if (key == "Description")
                            this.txtWebSitedescription.Text = value;
                    }
                }

                data = viviapi.BLL.Sys.SettleSettings.GetKeyValues();
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        string key = row["key"].ToString();
                        string value = row["value"].ToString();

                        if (key == "OpenWithdraw")
                            this.rbl_isopenCash.SelectedValue = value;

                        else if (key == "ColseWithdrawReason")
                            this.txtclosecashReason.Text = value;

                        else if (key == "DefaultScheme")
                            this.ddlDefaultScheme.SelectedValue = value;
                    }
                }



            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.System);

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
        protected void InitForm()
        {
            this.jsqqpanle.InnerHtml = "";
            this.kefu.InnerHtml = "";
            this.txtDomain.Text = this.ObjectInfo.Domain;
            this.txtPayUrl.Text = this.ObjectInfo.PayUrl;
            this.txtFooter.Text = this.ObjectInfo.Footer;
            this.txtName.Text = this.ObjectInfo.Name;
            this.txtPhone.Text = this.ObjectInfo.Phone;
            this.hdTemplate.Value = this.ObjectInfo.TemplateId.ToString();
            this.txtCode.Text = this.ObjectInfo.Code;
            this.txtJSQQ.Text = this.ObjectInfo.Jsqq;
            this.txtKFQQ.Text = this.ObjectInfo.Kfqq;

            this.txtapibankname.Text = this.ObjectInfo.apibankname;
            this.txtapibankversion.Text = this.ObjectInfo.apibankversion;
            this.txtapicardname.Text = this.ObjectInfo.apicardname;
            this.txtapicardversion.Text = this.ObjectInfo.apicardversion;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjectInfo.Domain = (this.txtDomain.Text.ToLower());
            ObjectInfo.PayUrl = (this.txtPayUrl.Text.ToLower());
            ObjectInfo.Footer = (this.txtFooter.Text);
            ObjectInfo.Name = (this.txtName.Text);
            ObjectInfo.Phone = (this.txtPhone.Text);
            ObjectInfo.Jsqq = (this.txtJSQQ.Text);
            ObjectInfo.Kfqq = (this.txtKFQQ.Text);
            ObjectInfo.Code = (this.txtCode.Text);

            ObjectInfo.apibankname = this.txtapibankname.Text.Trim();
            ObjectInfo.apibankversion = this.txtapibankversion.Text.Trim();
            ObjectInfo.apicardname = this.txtapicardname.Text.Trim();
            ObjectInfo.apicardversion = this.txtapicardversion.Text.Trim();


            if (this.txtName.Text == "")
            {
                AlertAndRedirect("网站名称不能为空!");
                return;
            }
            if (this.txtDomain.Text == "")
            {
                AlertAndRedirect("域名不能为空!");
                return;
            }
            if (this.txtPhone.Text == "")
            {
                AlertAndRedirect("联系电话不能为空!");
                return;
            }
            if (WebInfoFactory.Update(ObjectInfo))
            {
                SysConfig.Instance.Update("WebSiteTitleSuffix", this.txtTitleSuffix.Text.Trim());
                SysConfig.Instance.Update("KeyWords", this.txtWebSiteKey.Text.Trim());
                SysConfig.Instance.Update("Description", this.txtWebSitedescription.Text.Trim());

                SysConfig.Instance.Update("OpenWithdraw", rbl_isopenCash.SelectedValue);
                SysConfig.Instance.Update("ColseWithdrawReason", this.txtclosecashReason.Text.Trim());
                SysConfig.Instance.Update("DefaultScheme", ddlDefaultScheme.SelectedValue);

                int times = 0;

                WebUtility.ClearCache("SYSCONFIG");
                AlertAndRedirect("更新成功!", "siteinfo.aspx");
            }
        }
    }
}