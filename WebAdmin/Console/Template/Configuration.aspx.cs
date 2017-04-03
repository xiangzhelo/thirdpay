using System;
using System.Data;
using viviapi.BLL.Sys;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.Template
{
    public partial class Configuration : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        #region InitForm
        /// <summary>
        /// 
        /// </summary>
        private void InitForm()
        {
            DataTable data = viviapi.BLL.Sys.SMSTempSettings.GetKeyValues();

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    string key = row["key"].ToString();
                    string value = row["value"].ToString();

                    if (key == "SMS_Temp_Register")
                        this.txtRegister.Text = value;
                    else if (key == "SMS_Temp_Authenticate")
                        this.txtAuthenticate.Text = value;
                    else if (key == "SMS_Temp_Modify")
                        this.txtModify.Text = value;
                    else if (key == "SMS_Temp_FindPwd")
                        this.txtFindPwd.Text = value;
                    else if (key == "SMS_Temp_Withdraw")
                        this.txttoCash.Text = value;
                    else if (key == "SMS_Temp_1")
                        txtKey.Text = value;
                    else if (key == "SMS_Temp_2")
                        txtSN.Text = value;
                    else if (key == "SMS_Temp_3")
                        txtWebSite.Text = value;
                    else if (key == "SMS_SendSuccessCode")
                        txtSuccessCode.Text = value;
                }
            }
        }
        #endregion

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SysConfig.Instance.Update("SMS_Temp_Register", this.txtRegister.Text.Trim());
            SysConfig.Instance.Update("SMS_Temp_Authenticate", this.txtAuthenticate.Text.Trim());
            SysConfig.Instance.Update("SMS_Temp_Modify", this.txtModify.Text.Trim());
            SysConfig.Instance.Update("SMS_Temp_FindPwd", this.txtFindPwd.Text.Trim());
            SysConfig.Instance.Update("SMS_Temp_Withdraw", this.txttoCash.Text.Trim());
            SysConfig.Instance.Update("SMS_Temp_1", this.txtKey.Text.Trim());
            SysConfig.Instance.Update("SMS_Temp_2", this.txtSN.Text.Trim());
            SysConfig.Instance.Update("SMS_Temp_3", this.txtWebSite.Text.Trim());
            SysConfig.Instance.Update("SMS_Temp_SendSuccessCode", this.txtSuccessCode.Text.Trim());
            ShowMessageBox("操作成功");
        }
    }
}