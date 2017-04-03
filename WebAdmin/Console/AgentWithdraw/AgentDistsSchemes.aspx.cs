using System;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.AgentWithdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AgentDistsSchemes : viviapi.WebComponents.Web.ManagePageBase
    {
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public string Action
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }
        public bool isUpdate
        {
            get
            {
                return ItemInfoId > 0 && Action == "edit";
            }
        }

        public bool isDel
        {
            get
            {
                return ItemInfoId > 0 && Action == "del";
            }
        }

        public viviapi.Model.Finance.TocashSchemeInfo _ItemInfo = null;
        public viviapi.Model.Finance.TocashSchemeInfo ItemInfo
        {
            get
            {
                if (_ItemInfo == null)
                {
                    if (isUpdate)
                    {
                        _ItemInfo = viviapi.BLL.Finance.TocashScheme.GetModel(ItemInfoId);
                    }
                    else
                    {
                        _ItemInfo = new viviapi.Model.Finance.TocashSchemeInfo();
                    }
                }
                return _ItemInfo;
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
            DoCmd();
            if (!this.IsPostBack)
            {
                this.BindView();
            }
        }

        void DoCmd()
        {
            if (isDel)
            {
                if (viviapi.BLL.Finance.TocashScheme.Delete(this.ItemInfoId))
                {
                    AlertAndRedirect("删除成功!", "AgentDistsSchemeModi.aspx");
                }
                else
                {
                    AlertAndRedirect("删除失败!", "AgentDistsSchemeModi.aspx");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Financial);

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
            DataTable table = viviapi.BLL.Finance.TocashScheme.GetList("type=2").Tables[0];
            
            this.GridView1.DataSource = table.DefaultView;
            this.GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            this.BindView();
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgentDistsSchemeModi.aspx");
        }
    }
}
