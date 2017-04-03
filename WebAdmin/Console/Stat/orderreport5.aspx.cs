using System;
using System.Data;
using viviapi.Model;

namespace viviAPI.WebAdmin.Console.Stat
{
    public partial class Orderreport5 : viviapi.WebComponents.Web.ManagePageBase
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

                LoadData();
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

        void LoadData()
        {
            string userId = txtuserid.Text.Trim();
            int uid = 0;
            if (int.TryParse(userId, out uid))
            {

            }
            int typeid = 0;
            if (!string.IsNullOrEmpty(this.ddlChannelType.SelectedValue))
            {
                typeid = int.Parse(ddlChannelType.SelectedValue);
            }

            string sdate = StimeBox.Text;
            string edate = EtimeBox.Text;

            DataSet data = viviapi.BLL.Order.Statistics.AgentStat4(uid, typeid, sdate, edate, this.Pager1.PageSize, this.Pager1.CurrentPageIndex - 1, string.Empty);

            this.Pager1.RecordCount = Convert.ToInt32(data.Tables[0].Rows[0][0]);
            this.gv_data.DataSource = data.Tables[1];
            this.gv_data.DataBind();
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}