using System;
using System.Data;
using viviapi.Model;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.Stat
{
    public partial class OrderReport : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
               , ManageRole.Report);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            DateTime stime = DateTime.Parse(this.StimeBox.Text.Trim());
            DateTime etime = DateTime.Parse(this.EtimeBox.Text.Trim());

            DataTable data = viviapi.BLL.Stat.OrderReport.ReportByUser(stime, etime, 0);
            double num = 0.0;
            double num2 = 0.0;

            foreach (DataRow row in data.Rows)
            {
                num += Convert.ToDouble(row["totalAmt"].ToString());
                num2 += Convert.ToDouble(row["payAmt"].ToString());
            }
            this.lbmoney.Text = num.ToString("f2");
            this.lbchumoney.Text = num2.ToString("f2");

            this.gv_data.DataSource = data;
            this.gv_data.DataBind();
        }
    }
}