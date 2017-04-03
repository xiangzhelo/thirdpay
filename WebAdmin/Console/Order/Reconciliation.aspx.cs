using System;
using System.Data;
using viviapi.Model;

namespace viviAPI.WebAdmin.Console.order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Reconciliation : viviapi.WebComponents.Web.ManagePageBase
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
              
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.System);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion


        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtorders.Text.Trim()))
            {
                AlertAndRedirect("请输入订单号");
            }
            else
            {
                DataTable _data = new DataTable();
                _data.Columns.Add("orderid", typeof(string));
                _data.Columns.Add("supporder", typeof(string));
                _data.Columns.Add("realamt", typeof(string));
                _data.Columns.Add("result", typeof(string));
                _data.Columns.Add("status", typeof(string));
                _data.Columns.Add("coin", typeof(string));
                _data.Columns.Add("cardtype", typeof(string));


                string callback = string.Empty;

                string suppid = this.ddlsupp.SelectedValue;
                string[] orders = this.txtorders.Text.Split('\n');

                string temp = string.Empty;
                foreach (string item in orders)
                {
                   
                }

                rptOrders.DataSource = _data;
                rptOrders.DataBind();

            }
        }        
    }
}