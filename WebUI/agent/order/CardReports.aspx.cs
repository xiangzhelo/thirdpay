using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.Order.Card;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebUI7uka.agent.order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CardReports : AgentPageBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                InitForm();
                LoadData();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitForm()
        {
           
        }

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            var listParam = new List<SearchParam>();
            int tempId = 0;
            listParam.Add(new SearchParam("agentid", this.CurrentUser.ID));
            if (!string.IsNullOrEmpty(this.txtOrderId.Text.Trim()))
            {
                listParam.Add(new SearchParam("orderid", txtOrderId.Text));
            }
            if (!string.IsNullOrEmpty(txtuserid.Text.Trim()))
            {
                if (int.TryParse(txtuserid.Text.Trim(), out tempId))
                {
                    listParam.Add(new SearchParam("userid", tempId));
                }
            }
            if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
            {
                listParam.Add(new SearchParam("cardno", txtCardNo.Text.Trim()));
            }
          
            if (!string.IsNullOrEmpty(txtUserOrder.Text.Trim()))
            {
                listParam.Add(new SearchParam("userorder", txtUserOrder.Text.Trim()));
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

            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(StimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(StimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("stime", StimeBox.Text.Trim()));
                    }
                }
            }

            if (!string.IsNullOrEmpty(EtimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(EtimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("etime", tempdt.AddDays(1)));
                    }
                }
            }

            string orderby = string.Empty;// orderBy + " " + orderByType;


            DataSet pageData = CardNotify.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);

            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = pageData.Tables[1];
            this.rptOrders.DataBind();
        }
        #endregion

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Orders);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Button btnReissue = e.Item.FindControl("btnReissue") as Button;
                //Button btnRest    = e.Item.FindControl("btnRest") as Button;
                //Button btnDeduct  = e.Item.FindControl("btnDeduct") as Button;
                //Button btnReDeduct = e.Item.FindControl("btnReDeduct") as Button;

                //int userid = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "userid").ToString());
                //string status = DataBinder.Eval(e.Item.DataItem, "status").ToString();

                //switch (status)
                //{
                //    case "1":
                //        btnReissue.Visible = false;
                //        btnRest.Visible = true;
                //        btnDeduct.Visible =false;
                //        btnReDeduct.Visible = false;
                //        break;
                //    case "2":
                //        btnReissue.Visible = true;
                //        btnRest.Visible = false;
                //        btnReDeduct.Visible = false;
                //        btnDeduct.OnClientClick = "return confirm('是否确定扣量？')";
                //        object completeTime = DataBinder.Eval(e.Item.DataItem, "CompleteTime");
                //        double difftime = GetDifftime(userid,completeTime);
                //        if (difftime > viviapi.SysConfig.RuntimeSetting.DeductSafetyTime)
                //        {
                //            btnDeduct.Text = "扣";
                //        }
                //        else if (difftime > 0.0 && difftime <= viviapi.SysConfig.RuntimeSetting.DeductSafetyTime)
                //        {
                //            btnDeduct.Text = "危险";
                //        }
                //        else
                //        {
                //            btnDeduct.Text = "不能";
                //        }                        
                //        break;
                //    case "4":                    
                //        btnReissue.Visible = true;
                //        btnRest.Visible = false;
                //        btnDeduct.Visible = false;
                //        btnReDeduct.Visible = false;
                //        break;
                //    case "8":
                //        btnReissue.Visible = true;
                //        btnRest.Visible = false;
                //        btnDeduct.Visible = false;
                //        btnReDeduct.Visible = true;
                //        break;
                //}
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Reissue")
                {
                    string orderId = e.CommandArgument.ToString();
                    if (string.IsNullOrEmpty(orderId))
                        return;

                    string callback = viviapi.SysInterface.Bank.Utility.SynchronousNotify(orderId);

                    ShowMessageBox("返回：" + callback);
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

    }
}