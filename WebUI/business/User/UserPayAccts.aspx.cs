using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.Model.User;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;

namespace viviapi.web.business.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserPayAccts : viviapi.WebComponents.Web.BusinessPageBase
    {
        public string InfoStatus
        {
            get
            {
                return WebBase.GetQueryStringString("status", "");
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
                this.StimeBox.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                if (!string.IsNullOrEmpty(InfoStatus))
                {
                    this.StatusList.SelectedValue = InfoStatus;
                }
                this.LoadData();
            }
        }


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

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();

            if (!string.IsNullOrEmpty(this.StatusList.SelectedValue))
                listParam.Add(new viviLib.Data.SearchParam("status", int.Parse(this.StatusList.SelectedValue)));
            int userId = 0;
            if (int.TryParse(txtUserId.Text.Trim(), out userId))
            {
                listParam.Add(new viviLib.Data.SearchParam("userid", userId));
            }

            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(StimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(StimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("stime", StimeBox.Text.Trim()));
                    }
                }
            }

            if (!string.IsNullOrEmpty(EtimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(EtimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("etime", tempdt.AddDays(1)));
                    }
                }
            }

            string orderby = string.Empty;

            DataSet pageData = BLL.User.SettlementAccountApply.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptApps.DataSource = pageData.Tables[1];
            this.rptApps.DataBind();
        }
        #endregion


        protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void rptAppsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string _status = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                Label lblStat = (Label)e.Item.FindControl("lblUserStat");
                Button btn_pass = (Button)e.Item.FindControl("btn_pass");
                Button btn_fail = (Button)e.Item.FindControl("btn_fail");

                lblStat.Text = Enum.GetName(typeof(AcctChangeEnum), int.Parse(_status));
                string userId = DataBinder.Eval(e.Item.DataItem, "userid").ToString();


                if (_status == "1")
                {
                    btn_pass.Attributes["onclick"] = "return confirm('确定要通过此审核吗')";
                    btn_pass.Enabled = true;

                    btn_fail.Attributes["onclick"] = "return confirm('确定要不同意些申请吗')";
                    btn_fail.Enabled = true;
                }
                else
                {
                    btn_pass.Enabled = false;
                    btn_fail.Enabled = false;
                }
            }
        }

        protected void rptApps_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int appId = int.Parse(e.CommandArgument.ToString());
            var info = BLL.User.SettlementAccountApply.GetModel(appId);
            //info.id = appId;
            
            if (e.CommandName == "pass")
            {
                info.status = AcctChangeEnum.审核成功;
            }
            else if (e.CommandName == "fail")
            {
                info.status = AcctChangeEnum.审核失败;
            }

            info.SureTime = DateTime.Now;
            info.SureUser = currentManage.id;
            

            if (BLL.User.SettlementAccountApply.Check(info))
            {
                
                AlertAndRedirect("操作成功","UserPayAccts.aspx");
            }
            else
            {
                AlertAndRedirect("操作失败", "UserPayAccts.aspx");
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string chkItem = Request.Form["chkItem"];
            if (!string.IsNullOrEmpty(chkItem))
            {
                string[] ids = chkItem.Split(',');
                foreach (string _id in ids)
                {
                    int _appid = 0;
                    if (int.TryParse(_id, out _appid))
                    {
                        BLL.User.SettlementAccountApply.Delete(_appid);
                    }
                }
                AlertAndRedirect("操作成功", "UserPayAccts.aspx");
            }
        }
}
}