using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebUI7uka.usermodule.settlement
{
    public partial class Transferlist : UserPageBase
    {
        protected viviapi.BLL.Finance.Transfer tranBLL = new viviapi.BLL.Finance.Transfer();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
                LoadData();
            }
        }

        void InitForm()
        {
            this.sdate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.edate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }


        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {

            List<SearchParam> listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("userid", this.UserId));
            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(sdate.Value.Trim()))
            {
                if (DateTime.TryParse(sdate.Value.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("starttime", tempdt));
                    }
                }
            }

            if (!string.IsNullOrEmpty(edate.Value.Trim()))
            {
                if (DateTime.TryParse(edate.Value.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("endtime", tempdt.AddDays(1)));
                    }
                }
            }

            DataSet pageData = tranBLL.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptrecharges.DataSource = pageData.Tables[1];
            this.rptrecharges.DataBind();


        }
        public string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", date, hour, m, s);
        }
        #endregion



        protected void b_search_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (rptrecharges.Items.Count == 0)
                {
                    Literal lit = (Literal)e.Item.FindControl("litfoot");
                    lit.Text = @" <tfoot>
                        <tr>
                            <td colspan=""10"" class=""nomsg"">
                                －_－^..暂无记录
                            </td>
                        </tr>
                     </tfoot>     ";
                }
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
