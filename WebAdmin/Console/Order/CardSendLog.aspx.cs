using System;
using System.Collections.Generic;
using System.Data;
using viviapi.BLL.Order.Card;
using viviapi.Model;

namespace viviAPI.WebAdmin.Console.order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CardSendLog : viviapi.WebComponents.Web.ManagePageBase
    {
        OrderCardSend bll = new OrderCardSend();
        
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
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                LoadData();
            }
        }
        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            var listParam = new List<viviLib.Data.SearchParam>();

            string val = txtorderid.Text.Trim();
            if (!string.IsNullOrEmpty(val))
            {
                listParam.Add(new viviLib.Data.SearchParam("orderid", val));
            }

            int tempId = 0;
            val = ddlChannelType.SelectedValue;
            if (!string.IsNullOrEmpty(val))
            {
                if (int.TryParse(val, out tempId))
                {
                    listParam.Add(new viviLib.Data.SearchParam("typeid", tempId));
                }
            }

            val = ddlsuccess.SelectedValue;
            if (!string.IsNullOrEmpty(val))
            {
                if (int.TryParse(val, out tempId))
                {
                    listParam.Add(new viviLib.Data.SearchParam("success", tempId));
                }
            }
            val = txtCardNo.Text.Trim();
            if (!string.IsNullOrEmpty(val))
            {
                listParam.Add(new viviLib.Data.SearchParam("cardno", val));
            }
           
            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(StimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(StimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("stime", StimeBox.Text.Trim()));
                    }
                }
            }

            if (!string.IsNullOrEmpty(EtimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(EtimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("etime", tempdt.AddDays(1)));
                    }
                }
            }
            

            string orderby = string.Empty;// orderBy + " " + orderByType;


            DataSet pageData = bll.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptSendLogs.DataSource = pageData.Tables[1];
            this.rptSendLogs.DataBind();

        }
        #endregion

        #region setPower
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
        #region 分页
        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion
    }
}
