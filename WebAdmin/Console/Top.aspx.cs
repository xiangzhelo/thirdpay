using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using viviapi.Model;


namespace viviapi.web.Manage
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Top : viviapi.WebComponents.Web.ManagePageBase
    {
        protected string username;

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();            
            if (!this.IsPostBack)
            {
                this.username = this.currentManage.username;
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            //ManageRole role = ManageRole.Administrator | ManageRole.Financial | ManageRole.UnionAdmin | ManageRole.SuperAdmin | ManageRole.CustomerService;

            //if ((currentManage.role & role) == currentManage.role)
            //{
            //    return;
            //}
            //else
            //{
            //    Response.End();
            //}
        }
        #endregion
    }
}

