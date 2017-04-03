using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Order.Bank;
using viviapi.Model;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;
using System.Data;

namespace viviAPI.WebUI7uka.usermodule.order
{
    public partial class bankreport : viviapi.WebComponents.Web.UserPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadData();
            }
        }

        protected void b_search_Click(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadData();
            }
        }
        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal litdo = (Literal)e.Item.FindControl("litdo");
                string stats = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                string orderid = DataBinder.Eval(e.Item.DataItem, "orderid").ToString();
                if (stats == "2" || stats == "4")
                {
                    litdo.Text = string.Format("<a href=\"javascript:switchstate('{0}');\">&laquo; 查看</a>", DataBinder.Eval(e.Item.DataItem, "againnotifyurl"));
                }
                //if (stats == "8")
                //{
                //    viviapi.BLL.OrderCard bll = new viviapi.BLL.OrderCard();
                //    OrderCardInfo orderinfo = bll.GetModel(orderid);
                //    orderinfo.opstate = "-1";
                //    orderinfo.realvalue = 0M;

                //    string againnotifyurl = bll.GetCallBackUrl(orderinfo);

                //    litdo.Text = string.Format("<a href=\"javascript:switchstate('{0}');\">&laquo; 查看</a>", againnotifyurl);
                //}
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
        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            #region build where
            var listParam = new List<viviLib.Data.SearchParam>();
            listParam.Add(new viviLib.Data.SearchParam("userid", UserId));
            int tempId = 0;

            string s_type = select_field.Value;
            if (int.TryParse(s_type, out tempId))
            {
                string searchkey = okey.Value.Trim();
                if (!string.IsNullOrEmpty(searchkey))
                {
                    if (tempId == 1)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("userorder", searchkey));
                    }
                    else if (tempId == 2)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("cardNo", searchkey));
                    }
                }
            }
            if (!string.IsNullOrEmpty(Success.Value))
            {
                if (int.TryParse(Success.Value, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("status", tempId));
                    }
                }
            }


            #endregion

            string orderby = string.Empty;// orderBy + " " + orderByType;

            DataSet pageData = BankNotify.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);

            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = pageData.Tables[1];
            this.rptOrders.DataBind();


        }
        public string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", date, hour, m, s);
        }
        #endregion
    }
}
