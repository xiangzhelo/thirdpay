using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

namespace viviAPI.WebUI7uka.usermodule.recharg
{
    public partial class rechargelist : viviapi.WebComponents.Web.UserPageBase
    {
        protected viviapi.BLL.APP.Recharge rechargeBLL = new viviapi.BLL.APP.Recharge();

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
            //if (!string.IsNullOrEmpty(this.ddlstatus.SelectedValue))
            //{
            //    listParam.Add(new SearchParam("status", int.Parse(this.ddlstatus.SelectedValue)));
            //}


            DataSet pageData = rechargeBLL.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, true);
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

        public string GetPayTypeName(object paytype)
        {
            if (paytype == DBNull.Value)
                return string.Empty;

            int _type = Convert.ToInt32(paytype);
            if (_type == 102)
                return "网上银行";
            else if (_type == 101)
                return "支付宝";
            else if (_type == 1004)
                return "微信";
            else if (_type == 1005)
                return "QQ支付";
            else if (_type == 967)
                return "工商银行";
            else if (_type == 970)
                return "招商银行";
            else if (_type == 965)
                return "建设银行";
            else if (_type == 965)
                return "建设银行";
            else if (_type == 964)
                return "农业银行";
            else if (_type == 980)
                return "民生银行";
            else if (_type == 978)
                return "平安银行";
            else if (_type == 981)
                return "交通银行";
            else if (_type == 986)
                return "光大银行";
            else if (_type == 971)
                return "邮政储蓄";
            else if (_type == 972)
                return "兴业银行";
            else if (_type == 963)
                return "中国银行";
            else if (_type == 989)
                return "北京银行";
            else
            {
            return "财付通"; }

        }
    }
}
