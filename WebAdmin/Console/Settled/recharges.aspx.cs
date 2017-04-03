using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.settled
{
    public partial class Recharges : viviapi.WebComponents.Web.ManagePageBase
    {
        protected string wzfmoney = "0.00";
        protected viviapi.BLL.APP.Recharge rechargeBLL = new viviapi.BLL.APP.Recharge();

        protected string yzfmoney = string.Empty;

        public string orderBy
        {
            get
            {
                return WebBase.GetQueryStringString("orderby", "balance");
            }
        }

        public string orderByType
        {
            get
            {
                return WebBase.GetQueryStringString("type", "asc");
            }
        }

        public string UserStatus
        {
            get
            {
                return WebBase.GetQueryStringString("UserStatus", "");
            }
        }
        public string cmd
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }

        public int UserID
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public int proid
        {
            get
            {
                return WebBase.GetQueryStringInt32("proid", 0);
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
            if (!this.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                this.LoadData();
            }
        }

        #region setPower
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
        #endregion

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            string orderby = "id desc";
            if (ViewState["__Sort"] != null)
                orderby = ViewState["__Sort"].ToString();
            
            List<SearchParam> listParam = new List<SearchParam>();


            string userId = txtuserId.Text.Trim();
            int tempId = 0;
            if (int.TryParse(userId, out tempId))
            {
                listParam.Add(new SearchParam("userid", tempId));
            }

            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(StimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(StimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("starttime", StimeBox.Text.Trim()));
                    }
                }
            }

            if (!string.IsNullOrEmpty(EtimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(EtimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("endtime", tempdt.AddDays(1)));
                    }
                }
            }
            if (!string.IsNullOrEmpty(this.ddlstatus.SelectedValue))
            {
                listParam.Add(new SearchParam("status", int.Parse(this.ddlstatus.SelectedValue)));
            }
           

            DataSet pageData = rechargeBLL.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby,true);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.recharges.DataSource = pageData.Tables[1];
            this.recharges.DataBind();

            try
            {
                DataRow row = pageData.Tables[2].Rows[0];

                wzfmoney = string.Format("{0:f2}", row["realPayAmt"]);
            }
            catch { }

        }
        #endregion


        protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }


        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

       
        protected string GetStatusName(object status)
        {
            if (status == DBNull.Value)
                return string.Empty;
            return this.rechargeBLL.GetStatusName(Convert.ToInt32(status));
        }

       
        protected void recharges_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            #region Header
            if (e.Item.ItemType == ListItemType.Header)
            {
                LinkButton lkbtnSort = (LinkButton)e.Item.FindControl("iBtn" + e.CommandName.Trim());
                if (ViewState[e.CommandName.Trim()] == null)
                {
                    ViewState[e.CommandName.Trim()] = "DESC";
                    lkbtnSort.Text = lkbtnSort.Text + "▼";
                }
                else
                {
                    if (ViewState[e.CommandName.Trim()].ToString().Trim() == "DESC")
                    {
                        ViewState[e.CommandName.Trim()] = "ASC";
                        if (lkbtnSort.Text.IndexOf("▼") != -1)
                            lkbtnSort.Text = lkbtnSort.Text.Trim().Replace("▼", "▲");
                        else
                            lkbtnSort.Text = lkbtnSort.Text + "▲";
                    }
                    else
                    {
                        ViewState[e.CommandName.Trim()] = "DESC";
                        if (lkbtnSort.Text.IndexOf("▲") != -1)
                            lkbtnSort.Text = lkbtnSort.Text.Replace("▲", "▼");
                        else
                            lkbtnSort.Text = lkbtnSort.Text + "▼";

                    }
                }
                ViewState["__text"] = lkbtnSort.Text;
                ViewState["__id"] = e.CommandName.Trim();
                ViewState["__Sort"] = e.CommandName.ToString().Trim() + " " + ViewState[e.CommandName.Trim()].ToString().Trim();

                LoadData();
                //DataView dv = GetData;
                //dv.Sort = e.CommandName.ToString().Trim() + " " + ViewState[e.CommandName.Trim()].ToString().Trim();
                //rpOrder.DataSource = dv;
                //rpOrder.DataBind();
            }
            #endregion

            try
            {
                if (e.CommandName == "Freeze")
                {
                    string[] arr = e.CommandArgument.ToString().Split(',');

                    Response.Redirect(string.Format("Freeze.aspx?ID={0}&amt={1}&reason={2}", arr[0], arr[1],arr[2]), false);
                }
                if (e.CommandName == "btnReplenish")
                {
                    Response.Redirect(string.Format("resetrecharges.aspx?orderid={0}", e.CommandArgument), false);
                }
            }
            catch (Exception ex)
            {
                AlertAndRedirect(ex.Message);
            }
        }

        protected void recharges_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (ViewState["__id"] != null)
                {
                    LinkButton lkbtnSort = (LinkButton)e.Item.FindControl("iBtn" + ViewState["__id"].ToString().Trim());
                    lkbtnSort.Text = ViewState["__text"].ToString();
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string typeName = string.Empty;
                int status = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "status"));
                Button btnReplenish = (Button)e.Item.FindControl("btnReplenish");
                //Button btnFreeze = (Button)e.Item.FindControl("btnFreeze");
                btnReplenish.Visible = (status == 1);
                //btnFreeze.Visible = (status == 2);
            }
        }

    }
}
