using System;
using System.Collections.Generic;
using System.Data;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PayRate : ManagePageBase
    {
        public byte Type
        {
            get
            {
                byte type = 0;

                string qvalue = WebBase.GetQueryStringString("type", "");

                if (!string.IsNullOrEmpty(qvalue))
                {
                    byte.TryParse(qvalue, out type);
                }

                return type;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.BLL.ManageFactory.CheckSecondPwd();

            setPower();

            if (!this.IsPostBack)
            {
                if (Type > 0)
                    this.ddlused.SelectedValue = Type.ToString();
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
, ManageRole.Interfaces | ManageRole.Merchant);

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
            int tempValue = 0;

            var listParam = new List<SearchParam>();
            if (!string.IsNullOrEmpty(ddlused.SelectedValue))
            {
                byte used = Convert.ToByte(ddlused.SelectedValue);

                listParam.Add(new SearchParam("ratetype", used));
            }

            if (!string.IsNullOrEmpty(txtBillName.Text))
            {
                listParam.Add(new SearchParam("billname", txtBillName.Text));
            }

            if (!string.IsNullOrEmpty(txtBillId.Text))
            {
                if (int.TryParse(txtBillId.Text, out tempValue))
                {
                    listParam.Add(new SearchParam("billId", tempValue));
                }
            }

            DataSet pageData = viviapi.BLL.Finance.PayRate.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, "");
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);

            this.rptdata.DataSource = pageData.Tables[1];
            this.rptdata.DataBind();
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("payrateedit.aspx?type="+ddlused.SelectedItem.Value);
        }
        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }

}