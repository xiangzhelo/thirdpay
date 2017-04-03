using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using viviapi.Model;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviLib;
using viviLib.Data;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;
using System.Data;

namespace viviAPI.WebUI7uka.usermodule.behalf
{
    public partial class importitems : viviapi.WebComponents.Web.UserPageBase
    {
        protected string qty_str = "0";
        protected string cancel_qty_str = "0";
        protected string qty1_str = "0";
        protected string qty1_amt_str = "0.00";

        protected string qty2_str = "0";
        protected string qty2_amt_str = "0.00";

        protected string qty3_str = "0";
        protected string qty3_amt_str = "0.00";

        protected string qty4_str = "0";
        protected string qty4_amt_str = "0.00";


        protected viviapi.BLL.Finance.Agent.WithdrawAgent stlAgtBLL = new viviapi.BLL.Finance.Agent.WithdrawAgent();
        public string lotno
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("lotno", "");
            }
        }
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
            if (!string.IsNullOrEmpty(lotno))
            {
                this.txtLotNo.Text = lotno;
            }
            //this.sdate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            //this.edate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }


        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {

            List<SearchParam> listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("userid", this.UserId));
            listParam.Add(new SearchParam("mode", 2));

            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(this.txtLotNo.Text.Trim()))
            {
                listParam.Add(new SearchParam("lotno", this.txtLotNo.Text.Trim()));
            }

            decimal num = decimal.Zero;
            if (!string.IsNullOrEmpty(this.txtamtfrom.Text.Trim()))
            {
                if (decimal.TryParse(this.txtamtfrom.Text.Trim(), out num))
                {
                    listParam.Add(new SearchParam("amount_from", num));
                }
            }
            if (!string.IsNullOrEmpty(this.txtamtto.Text.Trim()))
            {
                if (decimal.TryParse(this.txtamtto.Text.Trim(), out num))
                {
                    listParam.Add(new SearchParam("txtamtto", num));
                }
            }
            //审核状态
            if (!string.IsNullOrEmpty(ddlaudit_status.SelectedValue))
            {
                listParam.Add(new SearchParam("audit_status", int.Parse(ddlaudit_status.SelectedValue)));
            }
            //付款状态
            if (!string.IsNullOrEmpty(ddlpayment_status.SelectedValue))
            {
                listParam.Add(new SearchParam("payment_status", int.Parse(ddlpayment_status.SelectedValue)));
            }
            //if (!string.IsNullOrEmpty(sdate.Value.Trim()))
            //{
            //    if (DateTime.TryParse(sdate.Value.Trim(), out tempdt))
            //    {
            //        if (tempdt > DateTime.MinValue)
            //        {
            //            listParam.Add(new SearchParam("starttime", tempdt));
            //        }
            //    }
            //}

            //if (!string.IsNullOrEmpty(edate.Value.Trim()))
            //{
            //    if (DateTime.TryParse(edate.Value.Trim(), out tempdt))
            //    {
            //        if (tempdt > DateTime.MinValue)
            //        {
            //            listParam.Add(new SearchParam("endtime", tempdt.AddDays(1)));
            //        }
            //    }
            //}

            DataSet pageData = stlAgtBLL.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, 2);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptrecharges.DataSource = pageData.Tables[1];
            this.rptrecharges.DataBind();

            try
            {
                DataTable data2 = pageData.Tables[2];
                DataRow row = data2.Rows[0];

                qty_str = string.Format("{0}", row["qty"]);
                cancel_qty_str = string.Format("{0}", row["cancel_qty"]);

                qty1_str = string.Format("{0}", row["qty1"]);
                qty1_amt_str = string.Format("{0:f2}", row["amt1"]);


                qty2_str = string.Format("{0}", row["qty2"]);
                qty2_amt_str = string.Format("{0:f2}", row["amt2"]);


                qty3_str = string.Format("{0}", row["qty3"]);
                qty3_amt_str = string.Format("{0:f2}", row["amt3"]);

                qty4_str = string.Format("{0}", row["qty4"]);
                qty4_amt_str = string.Format("{0:f2}", row["amt4"]);
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
