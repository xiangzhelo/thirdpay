using System;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Finance;
using viviapi.BLL.Sys;
using viviapi.Model;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib;

namespace viviAPI.WebAdmin.Console.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OtherSettings : ManagePageBase
    {
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
               

                InitForm();
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

        #region InitForm
        /// <summary>
        /// 
        /// </summary>
        private void InitForm()
        {
            DataTable data = viviapi.BLL.Sys.OtherSettings.GetKeyValues();

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    string key = row["key"].ToString();
                    string value = row["value"].ToString();

                    if (key == "AppKey")
                        this.txtAppKey.Text = value;

                    else if (key == "AppSecret")
                        this.txtAppSecret.Text = value;

                    else if (key == "AliDCode_logo_name")
                        this.txtlogo_name.Text = value;

                    else if (key == "AliDCode_goods_info_name")
                        this.txtgoods_info_name.Text = value;

                    else if (key == "AliDCode_goods_info_desc")
                        this.txtgoods_info_desc.Text = value;

                }
            }
        }
        #endregion

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SysConfig.Instance.Update("AppKey", this.txtAppKey.Text.Trim());
            SysConfig.Instance.Update("AppSecret", this.txtAppSecret.Text.Trim());

            SysConfig.Instance.Update("AliDCode_logo_name", this.txtlogo_name.Text.Trim());
            SysConfig.Instance.Update("AliDCode_goods_info_name", this.txtgoods_info_name.Text.Trim());
            SysConfig.Instance.Update("AliDCode_goods_info_desc", this.txtgoods_info_desc.Text.Trim());
          

            WebUtility.ClearCache("SYSCONFIG");

            ShowMessageBox("操作成功");
        }

    }
}