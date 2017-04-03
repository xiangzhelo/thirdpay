using System;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.Model.Finance;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Withdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TocashScheme : viviapi.WebComponents.Web.ManagePageBase
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

        public TocashSchemeInfo _ItemInfo = null;
        public TocashSchemeInfo ItemInfo
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
                        _ItemInfo = new TocashSchemeInfo();
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
                    AlertAndRedirect("删除成功!", "TocashSchemes.aspx");
                }
                else
                {
                    AlertAndRedirect("删除失败!", "TocashSchemes.aspx");
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
            DataTable table = viviapi.BLL.Finance.TocashScheme.GetList("type=1").Tables[0];
            
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
            Response.Redirect("TocashSchemeModi.aspx");
        }
    }
}
