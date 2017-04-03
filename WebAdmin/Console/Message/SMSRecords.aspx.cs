using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model;

namespace viviAPI.WebAdmin.Console.Message
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SMSRecords : viviapi.WebComponents.Web.ManagePageBase
    {
        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
                , ManageRole.Merchant);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        public DataTable detailData
        {
            get
            {
                return ViewState["detailData"] as DataTable;
            }
            set
            {
                ViewState["detailData"] = value;
            }
        }

        public int logid
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public string cmd
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("cmd", string.Empty);
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
            Docmd();
            if (!this.IsPostBack)
            {
                this.LoadData();
            }
        }

        void Docmd()
        {
            if (logid > 0 && !string.IsNullOrEmpty(cmd))
            { 
                if(cmd == "open" || cmd == "close")
                {
                    bool state = cmd == "open";

                    viviapi.BLL.PhoneValidFactory.PhoneSetting(logid, state);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();

            if (!string.IsNullOrEmpty(txtMobile.Text))
            {
                listParam.Add(new viviLib.Data.SearchParam("Mobile", txtMobile.Text.Trim()));
            }
            
            DataSet pageData = viviapi.BLL.PhoneValidFactory.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            detailData = pageData.Tables[2];

            this.repSms.DataSource = pageData.Tables[1];
            this.repSms.DataBind();
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        protected  void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repSms_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string phone = DataBinder.Eval(e.Item.DataItem, "phone").ToString();
                bool enable = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "enable"));
                string id = DataBinder.Eval(e.Item.DataItem, "ID").ToString();

                Repeater rptDetail = (Repeater)e.Item.FindControl("rptDetail");
                Literal litcmd =(Literal)e.Item.FindControl("litcmd");
                if (rptDetail != null)
                {
                    DataRow[] list = this.detailData.Select("phone="+phone);
                    rptDetail.DataSource = list;
                    rptDetail.DataBind();
                }
                if (enable)
                {
                    litcmd.Text = "<a onclick=\"return confirm('你确定要关闭发送短信功能吗?')\" href=\"?cmd=close&ID=" + id + "\" style=\"color:Green;\">关闭</a>"; ;
                }
                else
                {
                    litcmd.Text = "<a onclick=\"return confirm('你确定要开启发送短信功能吗?')\" href=\"?cmd=open&ID=" + id + "\" style=\"color:Green;\">开启</a>"; ;
                }
            }
        }
    }
}