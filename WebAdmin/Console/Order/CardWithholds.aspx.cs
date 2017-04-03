using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviLib.Data;

namespace viviAPI.WebAdmin.Console.order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CardWithholds : viviapi.WebComponents.Web.ManagePageBase
    {
        protected string total_amount = "0.00元";
        protected viviapi.BLL.Order.Cardwithhold bll = new viviapi.BLL.Order.Cardwithhold();
        protected viviapi.BLL.Order.Cardwithholds dbll = new viviapi.BLL.Order.Cardwithholds();


        #region Page_Load
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
                DataTable list = Factory.GetList("").Tables[0];
                ddlSupplier.Items.Add(new ListItem("--原处理接口--", ""));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupplier.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }

                this.txtStimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.txtEtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                this.txtStimeBox.Text = DateTime.Now.ToString("yyyy-MM-01");
                this.txtEtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

                this.BindData();
            }
        }
        #endregion

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

        #region BindData
        /// <summary>
        /// 
        /// </summary>
        private void BindData()
        {
            #region Param List
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();
            int tempId = 0;
            if (!String.IsNullOrEmpty(txtUserId.Text.Trim()))
            {
                if (int.TryParse(this.txtUserId.Text.Trim(), out tempId))
                {
                    listParam.Add(new viviLib.Data.SearchParam("userid", tempId));
                }
            }
            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(txtStimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(txtStimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("begindate", tempdt));
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtEtimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(txtEtimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("enddate", tempdt.AddDays(1)));
                    }
                }
            }


            //付款接口
            if (!string.IsNullOrEmpty(ddlSupplier.SelectedValue))
            {
                listParam.Add(new SearchParam("supplierid", int.Parse(ddlSupplier.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(ddlisclose.SelectedValue))
            {
                listParam.Add(new SearchParam("isclose", byte.Parse(ddlisclose.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(txtCardno.Text))
            {
                listParam.Add(new SearchParam("cardno", txtCardno.Text.Trim()));
            }



            #endregion

            DataSet pageData = bll.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, 1);


            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            DataTable data = pageData.Tables[1];


            this.rptcards.DataSource = data;
            this.rptcards.DataBind();

            try
            {
                DataRow row = pageData.Tables[2].Rows[0];
                total_amount = string.Format("{0:f2}", row["profit"]);
            }
            catch
            {

            }
        }
        #endregion

        #region Pager1_PageChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region btnSearch_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region rptList

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

            }
        }


        #endregion

        #region GetData

        public List<viviLib.Data.SearchParam>  GetSearchParams()
        {
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();
            int tempId = 0;
            if (!String.IsNullOrEmpty(txtUserId.Text.Trim()))
            {
                if (int.TryParse(this.txtUserId.Text.Trim(), out tempId))
                {
                    listParam.Add(new viviLib.Data.SearchParam("userid", tempId));
                }
            }
            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(txtStimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(txtStimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("begindate", tempdt));
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtEtimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(txtEtimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("enddate", tempdt.AddDays(1)));
                    }
                }
            }


            //付款接口
            if (!string.IsNullOrEmpty(ddlSupplier.SelectedValue))
            {
                listParam.Add(new SearchParam("supplierid", int.Parse(ddlSupplier.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(ddlisclose.SelectedValue))
            {
                listParam.Add(new SearchParam("isclose", byte.Parse(ddlisclose.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(txtCardno.Text))
            {
                listParam.Add(new SearchParam("cardno", txtCardno.Text.Trim()));
            }
            return listParam;
        }

        /// <summary>
        /// 
        /// </summary>
        private DataSet GetData()
        {

            #region Param List
            List<viviLib.Data.SearchParam> listParam = GetSearchParams();
            #endregion

            DataSet pageData = bll.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, 1);




            return pageData;
        }
        #endregion

        #region btnExport_Click
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DataSet ds = GetData();
            //    if (ds != null)
            //    {
            //        DataTable data = ds.Tables[1];
            //        data.Columns.Add("sName", typeof(string));
            //        foreach (DataRow dr in data.Rows)
            //        {
            //            dr["sName"] = stlAgtBLL.GetPaymentStatusText(dr["payment_status"]);
            //        }
            //        data.AcceptChanges();

            //        data.TableName = "Rpt";
            //        string path = Server.MapPath("~/common/template/xls/SettledAgent.xls");

            //        Aspose.Cells.WorkbookDesigner designer = new Aspose.Cells.WorkbookDesigner();
            //        designer.Workbook = new Workbook(path);


            //        //数据源 
            //        designer.SetDataSource(data);
            //        designer.Process();

            //        designer.Workbook.Save(this.Response
            //            , DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls"
            //            , ContentDisposition.Attachment
            //            , designer.Workbook.SaveOptions);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    AlertAndRedirect(ex.Message);
            //}
        }
        #endregion

        protected void rptcards_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int id = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"));

                Repeater rptDetail = (Repeater)e.Item.FindControl("rptDetail");

                if (rptDetail != null)
                {
                    DataSet _detail = dbll.GetList("withholdid=" + id);
                    rptDetail.DataSource = _detail;
                    rptDetail.DataBind();
                }

            }
        }

        public string GetMethodViewText(object method)
        {
            if (method == DBNull.Value)
                return "";

            byte _m = byte.Parse(method.ToString());
            if (_m == 1)
                return "接口处理";

            return "系统处理";
        }

        public string GetIsCloseViewText(object isclose)
        {
            if (isclose == DBNull.Value)
                return "";

            byte _m = byte.Parse(isclose.ToString());
            if (_m == 1)
                return "已关闭";

            return "未关闭";
        }

        protected void btnColse_Click(object sender, EventArgs e)
        {
            string message = "请选择要操作的记录";
            string ids = Request.Form["ischecked"];
            if (!string.IsNullOrEmpty(ids))
            {
                bool success = bll.BatchColse(ids, byte.Parse(this.rblstatus.SelectedValue));
                if (success)
                {
                    message = "操作成功";
                }
                else
                {
                    message = "操作失败";
                }
                BindData();
            }

            AlertAndRedirect(message);
        }

        protected void btnAllColse_Click(object sender, EventArgs e)
        {
            string message = "";
            List<viviLib.Data.SearchParam> listParam = GetSearchParams();
            listParam.Add(new SearchParam("upstatus", byte.Parse(this.rblstatus.SelectedValue)));

            bool success = bll.ALLColse(listParam);
            if (success)
            {
                message = "操作成功";
            }
            else
            {
                message = "操作失败";
            }
            BindData();
            AlertAndRedirect(message);
        }

        protected void btnUnLock_Click(object sender, EventArgs e)
        {
            string message = "请选择要操作的记录";
            string ids = Request.Form["ischecked"];
            if (!string.IsNullOrEmpty(ids))
            {
                bool success = bll.BatchUnlock(ids);
                if (success)
                {
                    message = "操作成功";
                }
                else
                {
                    message = "操作失败";
                }
                BindData();
            }

            AlertAndRedirect(message);
        }

    }
}