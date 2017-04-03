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
    public partial class RegisterSettings : ManagePageBase
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
            DataTable levData = viviapi.BLL.User.UserLevel.Instance.GetAllList().Tables[0];
            ddlDefaultUserLevel.Items.Add("--商户等级--");
            foreach (DataRow row in levData.Rows)
            {
                ddlDefaultUserLevel.Items.Add(new ListItem(row["levName"].ToString(), row["level"].ToString()));
            }

            DataTable data = viviapi.BLL.Sys.RegisterSettings.GetKeyValues();

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    string key = row["key"].ToString();
                    string value = row["value"].ToString();

                    if (key == "RegisterOpen")
                        this.ddlRegisterOpen.SelectedValue = value;

                    else if (key == "RequiredAudit")
                        this.ddlRequiredAudit.SelectedValue = value;

                    else if (key == "AllowUserloginByEmail")
                        this.rbl_AllowUserloginByEmail.SelectedValue = value;

                    else if (key == "AllowUserloginByPhone")
                        this.rbl_AllowUserloginByPhone.SelectedValue = value;

                    else if (key == "ActivationByEmail")
                        this.rbl_ActivationByEmail.SelectedValue = value;

                    else if (key == "LoginMsgForlock")
                        this.txtLoginMsgForlock.Text = value;
                    else if (key == "LoginMsgForUnCheck")
                        this.txtLoginMsgForUnCheck.Text = value;
                    else if (key == "LoginMsgForCheckfail")
                        this.txtLoginMsgForCheckfail.Text = value;

                    else if (key == "PhoneAuthenticate")
                        this.rbl_PhoneAuthenticate.SelectedValue = value;

                    else if (key == "SmsMaxSendTimes")
                        this.txtSmsMaxSendTimes.Text = value;
                    else if (key == "DefaultUserLevel")
                        this.ddlDefaultUserLevel.SelectedValue = value;
                    else if (key == "DefaultCPSDrate")
                        this.txtDefaultCPSDrate.Text = value;
                    else if (key == "DefaultCardVersion")
                        this.ddlcardversion.SelectedValue = value;
                }
            }
        }
        #endregion

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int _tempnum;

            SysConfig.Instance.Update("RegisterOpen", ddlRegisterOpen.SelectedValue);
            SysConfig.Instance.Update("RequiredAudit", ddlRequiredAudit.SelectedValue);
            SysConfig.Instance.Update("AllowUserloginByEmail", rbl_AllowUserloginByEmail.SelectedValue);
            SysConfig.Instance.Update("AllowUserloginByPhone", rbl_AllowUserloginByPhone.SelectedValue);
            SysConfig.Instance.Update("ActivationByEmail", rbl_ActivationByEmail.SelectedValue);

            SysConfig.Instance.Update("LoginMsgForlock", this.txtLoginMsgForlock.Text.Trim());
            SysConfig.Instance.Update("LoginMsgForUnCheck", this.txtLoginMsgForUnCheck.Text.Trim());
            SysConfig.Instance.Update("LoginMsgForCheckfail", this.txtLoginMsgForCheckfail.Text.Trim());

            SysConfig.Instance.Update("PhoneAuthenticate", rbl_PhoneAuthenticate.SelectedValue);

            if (int.TryParse(txtSmsMaxSendTimes.Text.Trim(), out _tempnum))
            {
                SysConfig.Instance.Update("SmsMaxSendTimes", txtSmsMaxSendTimes.Text.Trim());
            }

            SysConfig.Instance.Update("DefaultUserLevel", ddlDefaultUserLevel.SelectedValue);
            SysConfig.Instance.Update("DefaultCardVersion", ddlcardversion.SelectedValue);

            if (int.TryParse(txtDefaultCPSDrate.Text.Trim(), out _tempnum))
            {
                SysConfig.Instance.Update("DefaultCPSDrate", txtDefaultCPSDrate.Text.Trim());
            }

            WebUtility.ClearCache("SYSCONFIG");

            ShowMessageBox("操作成功");
        }

    }
}