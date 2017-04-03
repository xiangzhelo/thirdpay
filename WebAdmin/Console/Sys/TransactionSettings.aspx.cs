using System;
using System.Data;
using viviapi.BLL;
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
    public partial class TransactionSettings : ManagePageBase
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
            DataTable data = viviapi.BLL.Sys.TransactionSettings.GetKeyValues();

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    string key = row["key"].ToString();
                    string value = row["value"].ToString();

                    if (key == "MinTranATM")
                        this.txtMinTranATM.Text = value;
                    else if(key == "MaxTranATM")
                        this.txtMaxTranATM.Text = value;
                    else if (key == "ExpiresTime")
                        this.txtExpiresTime.Text = value;
                    else if (key == "CheckUrlReferrer")
                        this.rbl_CheckUrlReferrer.SelectedValue = value;
                    else if (key == "OrderPrefix")
                        this.txtOrderPrefix.Text = value;
                    else if (key == "CheckUserOrderNo")
                        this.rbl_CheckUserOrderNo.SelectedValue = value;

                    else if (key == "RiskWarning_Bank")
                        this.ckb_rw_bank.Checked = value=="1";
                    else if (key == "RiskWarning_Alipay")
                        this.ckb_rw_alipay.Checked = value == "1";
                    else if (key == "RiskWarning_AliCode")
                        this.ckb_rw_alicode.Checked = value == "1";
                    else if (key == "RiskWarning_WXpay")
                        this.ckb_rw_wxpay.Checked = value == "1";

                    else if (key == "Debuglog")
                        this.rbl_Debuglog.SelectedValue = value;
                    else if (key == "RefCount")
                        this.txtRefCount.Text = value;
                    else if (key == "WithoutRef")
                        this.rbl_WithoutRef.SelectedValue = value;
                    else if (key == "OpenDeduct")
                        this.rblOpenDeduct.SelectedValue = value;
                    else
                    {
                        continue;
                    }
                }
            }
        }
        #endregion

        #region btnUpdate_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            decimal _tempdt;
            int _tempnum;

            if (decimal.TryParse(txtMinTranATM.Text.Trim(), out _tempdt))
            {
                SysConfig.Instance.Update("MinTranATM", this.txtMinTranATM.Text.Trim());
            }
            if (decimal.TryParse(txtMaxTranATM.Text.Trim(), out _tempdt))
            {
                SysConfig.Instance.Update("MaxTranATM", this.txtMaxTranATM.Text.Trim());
            }

            if (int.TryParse(txtExpiresTime.Text.Trim(), out _tempnum))
            {
                SysConfig.Instance.Update("ExpiresTime", this.txtExpiresTime.Text.Trim());
            }

            SysConfig.Instance.Update("CheckUrlReferrer", rbl_CheckUrlReferrer.SelectedValue);
            SysConfig.Instance.Update("OrderPrefix", this.txtOrderPrefix.Text.Trim());
            SysConfig.Instance.Update("CheckUserOrderNo", this.rbl_CheckUserOrderNo.SelectedValue);

           

            SysConfig.Instance.Update("RiskWarning_Bank", this.ckb_rw_bank.Checked?"1":"0");
            SysConfig.Instance.Update("RiskWarning_Alipay", this.ckb_rw_alipay.Checked?"1":"0");
            SysConfig.Instance.Update("RiskWarning_AliCode", this.ckb_rw_alicode.Checked?"1":"0");
            SysConfig.Instance.Update("RiskWarning_WXpay", this.ckb_rw_wxpay.Checked?"1":"0");

            SysConfig.Instance.Update("Debuglog", this.rbl_Debuglog.SelectedValue);

            if (int.TryParse(txtRefCount.Text.Trim(), out _tempnum))
            {
                SysConfig.Instance.Update("RefCount", this.txtRefCount.Text.Trim());
            }
            SysConfig.Instance.Update("WithoutRef", this.rbl_WithoutRef.SelectedValue);
            SysConfig.Instance.Update("OpenDeduct", this.rblOpenDeduct.SelectedValue);

            WebUtility.ClearCache("SYSCONFIG");

            ShowMessageBox("操作成功");
        }
        #endregion

    }
}