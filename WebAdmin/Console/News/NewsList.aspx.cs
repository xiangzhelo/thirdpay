using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.News;
using viviapi.Model;
using viviapi.Model.News;
using viviLib.Data;

namespace viviAPI.WebAdmin.Console.News
{
    /// <summary>
    /// 
    /// </summary>
    public partial class NewsList : viviapi.WebComponents.Web.ManagePageBase
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
                foreach (int num in Enum.GetValues(typeof(NewsType)))
                {
                    this.ddl_type.Items.Add(new ListItem(Enum.GetName(typeof(NewsType), num), num.ToString()));
                }
                this.BindData();
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
           , ManageRole.News);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        protected void BindData()
        {
            List<SearchParam> listParam = new List<SearchParam>();
            if (!string.IsNullOrEmpty(this.ddl_type.SelectedValue))
                listParam.Add(new SearchParam("newstype", int.Parse(this.ddl_type.SelectedValue)));
            string keyword = txtTitle.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                listParam.Add(new SearchParam("newstitle", keyword));
            }
            DataSet pageData = NewsFactory.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);


            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.GridView1.DataSource = pageData.Tables[1];
            this.GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView data = (DataRowView)e.Row.DataItem;

                Label lblNewsType = e.Row.FindControl("lblNewsType") as Label;
                if (lblNewsType != null)
                {
                    lblNewsType.Text = Enum.GetName(typeof(NewsType), data["newstype"]);
                }
            }
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            string newstitle = string.Format("{0}月{1}日提现已出款请商户查收", DateTime.Now.Month, DateTime.Now.Day);
            var itemInfo = new NewsInfo
            {
                newstype = NewsType.佣金公告,
                newstitle = newstitle,
                addTime = DateTime.Now,
                newscontent = newstitle,
                IsRed = 0,
                IsTop = 0,
                IsPop = 0,
                Isbold = 0,
                Color = "",
                release = true
            };

            if (NewsFactory.Add(itemInfo) > 0)
            {
                AlertAndRedirect("发送成功!", "NewsList.aspx");
            }
            else
            {
                AlertAndRedirect("发送失败!", "NewsList.aspx");
            }
        }
        protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            BindData();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int newsId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "update")
            {
                Response.Redirect("NewsEdit.aspx?id=" + newsId.ToString(), true);
            }
            else if (e.CommandName == "del")
            {
                if (NewsFactory.Delete(newsId))
                {
                    AlertAndRedirect("删除成功");
                    BindData();
                }
                else
                {
                    AlertAndRedirect("删除失败");
                }
            }
        }

        protected string GetPublishText(object release)
        {
            if (release == DBNull.Value)
                return "";

            bool isPublish = Convert.ToBoolean(release);

            if (isPublish)
                return "已发布";
            else
            {
                return "未发布";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string ids = Request.Form["NewsIds"];
                foreach (string id in ids.Split(','))
                {
                    NewsFactory.Delete(int.Parse(id));
                }

                AlertAndRedirect("删除成功!", "NewsList.aspx");
            }
            catch
            {
                AlertAndRedirect("删除失败!", "NewsList.aspx");

            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
