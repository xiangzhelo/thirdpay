using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.Channel;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.channel
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ChannelList : ManagePageBase
    {
        public string PtypeId
        {
            get
            {
                return WebBase.GetQueryStringString("typeId", "");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.BLL.ManageFactory.CheckSecondPwd();
            setPower();
            if (!this.IsPostBack)
            {
                this.ddlType.Items.Add(new ListItem("---全部类别---", ""));
                DataTable types = viviapi.BLL.Channel.ChannelType.GetList(null).Tables[0];
                foreach (DataRow dr in types.Rows)
                {
                    this.ddlType.Items.Add(new ListItem(dr["modetypename"].ToString(), dr["typeId"].ToString()));
                }

                if (!string.IsNullOrEmpty(PtypeId))
                {
                    this.ddlType.SelectedValue = PtypeId;
                }
                LoadData();
            }
        }

        void LoadData()
        {
            var listParam = new List<viviLib.Data.SearchParam>();

            if (!string.IsNullOrEmpty(ddlType.SelectedValue))
            {
                int typeId = 0;
                if (int.TryParse(this.ddlType.SelectedValue, out typeId))
                {
                    listParam.Add(new SearchParam("typeId", typeId));
                }
            }

            int pageSize = this.Pager1.PageSize;
            int pageIndex = this.Pager1.CurrentPageIndex;

            DataSet pageData = Channel.PageSearch(listParam, pageSize, pageIndex, "");
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptchannels.DataSource = pageData.Tables[1];
            this.rptchannels.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Interfaces);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Edit.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void rptchannels_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    if (e.CommandName == "del")
                    {
                        int cid = Convert.ToInt32(e.CommandArgument);
                        Channel.Delete(cid);
                        ShowMessageBox("删除成功");
                    }
                }
                catch (Exception ex)
                {
                    ShowMessageBox("Error:" + ex.Message);
                }
            }
        }

        protected void rptchannels_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var drv = e.Item.DataItem as DataRowView;
                var litopen = e.Item.FindControl("litopen") as Literal;
                string img = "unknown";

                if (drv != null && drv["isOpen"] != DBNull.Value)
                {
                    img = Convert.ToInt32(drv["isOpen"]) == 0 ? "wrong" : "right";
                }
                if (litopen != null)
                {
                    litopen.Text = string.Format("<img src='../style/images/{0}.png' />", img);
                }
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();

            Session["selecttype"] = ddlType.SelectedValue;
        }
    }
}