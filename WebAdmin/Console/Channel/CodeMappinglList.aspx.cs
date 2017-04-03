using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.Model.Channel;

namespace viviapi.web.Manage
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CodeMappinglList : viviapi.WebComponents.Web.ManagePageBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.ManageFactory.CheckSecondPwd();
            setPower();
            if (!this.IsPostBack)
            {
                this.ddlSupp.Items.Add(new ListItem("---选择接口商---", ""));
                DataTable supps = Factory.GetList("isbank=1 and code<>100 and code <> 101").Tables[0];
                foreach (DataRow dr in supps.Rows)
                {
                    this.ddlSupp.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }

                LoadData();
            }
        }

        void LoadData()
        {
            string where = "1=1";
            this.gvData.DataSource = BLL.Channel.CodeMappingFactory.GetList(where);
            this.gvData.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = BLL.ManageFactory.CheckCurrentPermission(false
    , ManageRole.Interfaces);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("CodeMappingEdit.aspx");
        }
    }
}