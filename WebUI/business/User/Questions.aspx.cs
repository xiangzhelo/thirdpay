using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using viviLib.Web;
using viviLib.Security;
using viviapi.Model;

namespace viviapi.web.business
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Questions : viviapi.WebComponents.Web.BusinessPageBase
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
                this.BindView();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.System);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindView()
        {
            BLL.User.Question bll = new viviapi.BLL.User.Question();

            DataSet ds = bll.GetList(string.Empty);
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuestionEdit.aspx");
        }
    }
}