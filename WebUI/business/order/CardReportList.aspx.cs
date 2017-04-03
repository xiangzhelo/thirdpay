using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Channel;

using viviapi.Model;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;

namespace viviapi.web.business.Order
{
    public partial class CardReportList : viviapi.WebComponents.Web.BusinessPageBase
    {

        #region
        protected int UserId
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("userid", -1);
            }
        }

        protected int Status
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("status", -1);
            }
        }

        protected int ctype
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("ctype", -1);
            }
        }

        protected int currPage
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("currpage", -1);
            }
        }

        protected int MID
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("mid", -1);
            }
        }

        protected int NotifyStatus
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("ns", -1);
            }
        }

        protected string kano
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("ka", string.Empty);
            }
        }


        protected string stime
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("stime", string.Empty);
            }
        }

        protected string etime
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("etime", string.Empty);
            }
        }

        protected string sysorderid
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("orderid", string.Empty);
            }
        }

        protected string userorderid
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("userorder", string.Empty);
            }
        }

        protected string supporderid
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("supporder", string.Empty);
            }
        }
        #endregion
        //public UserAccessTimeInfo _userAcceTime = null;
        //public UserAccessTimeInfo userAcceTime 
        //{
        //    get
        //    {
        //        if (_userAcceTime == null && UserId > 0)
        //        {
        //            _userAcceTime =BLL.User.UserAccessTime.GetModel(UserId);
        //        }
        //        return _userAcceTime;
        //    }
        //}




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

                InitForm();
                LoadData();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitForm()
        {
            if (this.UserId > -1)
            {
                this.txtuserid.Text = UserId.ToString();
            }
            if (this.Status > -1)
            {
                this.ddlOrderStatus.SelectedValue = Status.ToString();
            }
            if (this.ctype > -1)
            {
                ddlChannelType.SelectedValue = ctype.ToString();
            }
            if (this.NotifyStatus > -1)
            {
                this.ddlNotifyStatus.SelectedValue = this.NotifyStatus.ToString();
            }
            if (!string.IsNullOrEmpty(this.kano))
            {
                this.txtCardNo.Text = this.kano;
            }
            if (!string.IsNullOrEmpty(this.stime))
            {
                this.StimeBox.Text = this.stime;
            }
            if (!string.IsNullOrEmpty(this.etime))
            {
                this.EtimeBox.Text = this.etime;
            }
            if (!string.IsNullOrEmpty(this.sysorderid))
            {
                this.txtOrderId.Text = this.sysorderid;
            }
            if (!string.IsNullOrEmpty(this.userorderid))
            {
                this.txtUserOrder.Text = this.userorderid;
            }
            if (!string.IsNullOrEmpty(this.supporderid))
            {
                this.txtSuppOrder.Text = this.supporderid;
            }

            ddlmange.Items.Add("--请选择业务员--");
            DataTable data = BLL.ManageFactory.GetList(" status =1").Tables[0];
            foreach (DataRow dr in data.Rows)
            {
                ddlmange.Items.Add(new ListItem(dr["username"].ToString(), dr["id"].ToString()));
            }
            if (MID > -1)
                this.ddlmange.SelectedValue = MID.ToString();
        }
        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();
            listParam.Add(new viviLib.Data.SearchParam("ordertype", 1));
            listParam.Add(new viviLib.Data.SearchParam("status","<>",1));

            int tempId = 0;
            //if (currentManage.role != ManageRole.SuperAdmin)
            //{
            //    listParam.Add(new viviLib.Data.SearchParam("manageId", ManageId));
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(ddlmange.SelectedValue))
            //    {
            //        if (int.TryParse(ddlmange.SelectedValue, out tempId))
            //        {
            //            listParam.Add(new viviLib.Data.SearchParam("manageId", tempId));
            //        }
            //    }
            //}
            if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
            {
                listParam.Add(new viviLib.Data.SearchParam("cardno", txtCardNo.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtOrderId.Text.Trim()))
            {
                listParam.Add(new viviLib.Data.SearchParam("orderId_like", txtOrderId.Text));
            }
            if (!string.IsNullOrEmpty(txtuserid.Text.Trim()))
            {
                if (int.TryParse(txtuserid.Text.Trim(), out tempId))
                {
                    listParam.Add(new viviLib.Data.SearchParam("userid", tempId));
                }
            }

            if (!string.IsNullOrEmpty(ddlChannelType.SelectedValue))
            {
                if (int.TryParse(ddlChannelType.SelectedValue, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("typeId", tempId));
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtUserOrder.Text.Trim()))
            {
                listParam.Add(new viviLib.Data.SearchParam("userorder", txtUserOrder.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(txtSuppOrder.Text.Trim()))
            {
                listParam.Add(new viviLib.Data.SearchParam("supplierOrder", txtSuppOrder.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(ddlOrderStatus.SelectedValue))
            {
                if (int.TryParse(ddlOrderStatus.SelectedValue, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("status", tempId));
                    }
                }
            }

            if (!string.IsNullOrEmpty(ddlNotifyStatus.SelectedValue))
            {
                if (int.TryParse(ddlNotifyStatus.SelectedValue, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("notifystat", tempId));
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

           

            DataSet pageData =viviapi.BLL.Order.Card.Factory.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby,false);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = pageData.Tables[1];
            this.rptOrders.DataBind();

            if (this.currPage > 0)
                this.Pager1.CurrentPageIndex = currPage;
            
        }
        #endregion

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = BLL.ManageFactory.CheckCurrentPermission(false
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

        protected string getsupplierName(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return string.Empty;

            return viviapi.BLL.Supplier.Factory.GetModelByCode(int.Parse(obj.ToString())).name;
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
            string Url = "CardReportList.aspx?status=" + this.ddlOrderStatus.SelectedValue;
            Url += "&ctype=" + ddlChannelType.SelectedValue;
            Url += "&userid=" + txtuserid.Text;
            Url += "&ns=" + ddlNotifyStatus.SelectedValue;
            Url += "&ka=" + txtCardNo.Text;
            Url += "&stime=" + StimeBox.Text;
            Url += "&etime=" + EtimeBox.Text;
            Url += "&mid=" + this.ddlmange.SelectedValue;
            Url += "&orderid=" + this.txtOrderId.Text;
            Url += "&userorder=" + this.txtUserOrder.Text;
            Url += "&supporder=" + this.txtSuppOrder.Text;
            Url += "&currpage=" + this.Pager1.CurrentPageIndex;
            try
            {
                if (e.CommandName == "Reissue")
                {
                    //string orderId = e.CommandArgument.ToString();
                    //if (string.IsNullOrEmpty(orderId))
                    //    return;

                    //viviapi.BLL.OrderCardNotify notity = new viviapi.BLL.OrderCardNotify();
                    //string callback = notity.SynchronousNotify(orderId);

                    //AlertAndRedirect("返回：" + callback, Url);                    
                }
            }
            catch (Exception ex)
            {
                AlertAndRedirect(ex.Message, Url);
            }
        }

        protected string getversionName(object version)
        {
            if (version == DBNull.Value)
                return string.Empty;

            if (version.ToString() == "1")
                return "多";

            return "单";
        }

    }
}