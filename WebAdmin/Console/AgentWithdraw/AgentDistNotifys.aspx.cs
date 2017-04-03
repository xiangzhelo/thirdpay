using System;
using System.Collections.Generic;
using System.Data;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebAdmin.Console.AgentWithdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AgentDistNotifys : ManagePageBase
    {
        protected string totalMoney = "0.00元";

        protected viviapi.BLL.Finance.Agent.WithdrawAgentNotify notifyBLL 
            = new viviapi.BLL.Finance.Agent.WithdrawAgentNotify();
       
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
            var listParam = new List<SearchParam>();
            int tempId = 0;
            if (!String.IsNullOrEmpty(txtUserId.Text.Trim()))
            {
                if (int.TryParse(this.txtUserId.Text.Trim(), out tempId))
                {
                    listParam.Add(new SearchParam("userid", tempId));
                }
            }

            //系统交易号
            if (!String.IsNullOrEmpty(txttrade_no.Text.Trim()))
            {
                listParam.Add(new SearchParam("trade_no", this.txttrade_no.Text.Trim()));
            }

            //商户订单号
            if (!String.IsNullOrEmpty(txtout_trade_no.Text.Trim()))
            {
                listParam.Add(new SearchParam("out_trade_no", this.txtout_trade_no.Text.Trim()));
            }
            //付款接口
            if (!string.IsNullOrEmpty(ddlnotifystatus.SelectedValue))
            {
                listParam.Add(new SearchParam("tranapi", int.Parse(ddlnotifystatus.SelectedValue)));
            }

            //通知状态
            if (!string.IsNullOrEmpty(ddlnotifystatus.SelectedValue))
            {
                listParam.Add(new SearchParam("notifystatus", int.Parse(ddlnotifystatus.SelectedValue)));
            }



            #endregion

            DataSet pageData = notifyBLL.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);


            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            DataTable data = pageData.Tables[1];


            this.rptList.DataSource = data;
            this.rptList.DataBind();

            //TotalMoney = Convert.ToString(pageData.Tables[2].Rows[0][0]);
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        
    }
}