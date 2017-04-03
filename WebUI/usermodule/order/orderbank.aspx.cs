using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using viviapi.WebComponents.Web;
using viviLib.Data;


namespace viviAPI.WebUI7uka.usermodule.order
{
    public partial class orderbank : UserPageBase
    {
        protected string pagerefervalue = "0";
        protected string pagerealvalue = "0";
        protected string totalrefervalue = "0";
        protected string totalrealvalue = "0";

        protected string pageordertotal = "0";
        protected string pageordersucctotal = "0";
        protected string totalordertotal = "0";
        protected string totalsuccordertotal = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
                LoadData();
            }
        }

        #region InitForm
        void InitForm()
        {
            this.sdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            this.edate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            //this.Success.Value = "0";
            // this.channelId.Value = "0";
            this.okey.Value = string.Empty;
        }
        #endregion

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            #region build where
            var listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("userid", UserId));
            listParam.Add(new SearchParam("deduct", 0));

            int tempId = 0;
            //if (!string.IsNullOrEmpty(channelId.Value))
            //{
            //    if (int.TryParse(channelId.Value, out tempId))
            //    {
            //        if (tempId > 0)
            //        {
            //            listParam.Add(new SearchParam("typeId", 102));
            //        }
            //    }
            //}
            string s_type = select_field.Value;
            if (int.TryParse(s_type, out tempId))
            {
                string searchkey = okey.Value.Trim();
                if (!string.IsNullOrEmpty(searchkey))
                {
                    if (tempId == 1)
                    {
                        listParam.Add(new SearchParam("userorder", searchkey));
                    }
                    else if (tempId == 3)
                    {
                        listParam.Add(new SearchParam("orderid", searchkey));
                    }
                    else if (tempId == 2)
                    {
                        listParam.Add(new SearchParam("cardno", searchkey));
                    }
                }
            }

            if (!string.IsNullOrEmpty(Success.Value))
            {
                if (int.TryParse(Success.Value, out tempId))
                {
                    if (tempId > 0)
                    {
                        if (tempId != 4)
                            listParam.Add(new SearchParam("status", tempId));
                        else
                            listParam.Add(new SearchParam("statusallfail", tempId));
                    }
                }
            }

            DateTime tempdt = DateTime.MinValue;
            if (DateTime.TryParse(builderdate(this.sdate.Value, "00", "00", "00"), out tempdt))
            {
                if (tempdt > DateTime.MinValue)
                {
                    listParam.Add(new SearchParam("stime", tempdt.ToString()));
                }
            }

            if (DateTime.TryParse(builderdate(this.edate.Value, "23", "59", "59"), out tempdt))
            {
                if (tempdt > DateTime.MinValue)
                {
                    listParam.Add(new SearchParam("etime", tempdt.ToString()));
                }
            }
            if (!string.IsNullOrEmpty(ddlNotifyStatus.SelectedValue))
            {
                if (int.TryParse(ddlNotifyStatus.SelectedValue, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new SearchParam("notifystat", tempId));
                    }
                }
            }
            #endregion

            string orderby = string.Empty;// orderBy + " " + orderByType;



            DataSet pageData = viviapi.BLL.Order.Bank.Factory.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby, true);

            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = pageData.Tables[1];
            this.rptOrders.DataBind();

            DataTable data2 = pageData.Tables[2];
            if (data2 != null && data2.Rows.Count > 0)
            {
                try
                {
                    totalrefervalue = Convert.ToDecimal(data2.Rows[0]["refervalue"]).ToString("f0");
                    totalrealvalue = Convert.ToDecimal(data2.Rows[0]["realvalue"]).ToString("f0");
                    totalordertotal = Convert.ToDecimal(data2.Rows[0]["ordtotal"]).ToString("f0");
                    totalsuccordertotal = Convert.ToDecimal(data2.Rows[0]["succordtotal"]).ToString("f0");
                }
                catch
                { }
            }

            data2 = pageData.Tables[1];
            try
            {
                pagerefervalue = Convert.ToDecimal(data2.Compute("sum(refervalue)", "")).ToString("f0");
                pagerealvalue = Convert.ToDecimal(data2.Compute("sum(realvalue)", "")).ToString("f0");
                pageordertotal = data2.Rows.Count.ToString();
                DataRow[] drs = data2.Select("status=2");
                pageordersucctotal = drs.Length.ToString();
            }
            catch
            { }


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

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal litdo = (Literal)e.Item.FindControl("litdo");
                string stats = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                string orderid = DataBinder.Eval(e.Item.DataItem, "orderid").ToString();
                if (stats == "2" || stats == "4" || stats == "8")
                {
                    litdo.Text = string.Format("<a href=\"javascript:replenish('{0}');\">&laquo; 补单</a>", orderid);
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (rptOrders.Items.Count == 0)
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
    }
}
