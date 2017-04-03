using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SysMailList : ManagePageBase
    {

        viviapi.BLL.Sys.SysMailConfig bllConfig = new viviapi.BLL.Sys.SysMailConfig();
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
                this.LoadData();
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

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            
            DataSet ds = bllConfig.GetAllList();
            this.rptdata.DataSource = ds;
            this.rptdata.DataBind();

        }
        #endregion

        protected void rptdata_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                bllConfig.Delete(Convert.ToInt32(e.CommandArgument));
            }
            this.LoadData();
        }



    }
}