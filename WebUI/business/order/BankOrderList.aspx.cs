using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using viviapi.BLL.Supplier;
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
    /// <summary>
    /// 
    /// </summary>
    public partial class BankOrderList : viviapi.WebComponents.Web.BusinessPageBase
    {
        /// <summary>
        /// 交易金额
        /// </summary>
        protected string TotalTranATM = "0.00";

        /// <summary>
        /// 商户金额
        /// </summary>
        protected string TotalUserATM = "0.00";

        /// <summary>
        /// 代理总提成
        /// </summary>
        protected string TotalPromATM = "0.00";

        /// <summary>
        /// 平台利润
        /// </summary>
        protected string TotalProfit = "0.00";

        //业务员提成
        protected string TotalCommission = "0.00";

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
            

            if (this.ctype > -1)
            {
                ddlChannelType.SelectedValue = ctype.ToString();
            }
            if (this.NotifyStatus > -1)
            {
                this.ddlNotifyStatus.SelectedValue = this.NotifyStatus.ToString();
            }
            //if (!string.IsNullOrEmpty(this.kano))
            //{
            //    this.txtCardNo.Text = this.kano;
            //}
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

            if (this.MID > -1)
                this.ddlmange.SelectedValue = this.MID.ToString();
        }
        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();

            int tempId = 0;

            string sysorderid = txtOrderId.Text.Trim();
            string userorderid = txtUserOrder.Text.Trim();

            if (isSuperAdmin == false)
            {
                if (string.IsNullOrEmpty(sysorderid) &&
                    string.IsNullOrEmpty(userorderid))
                {
                    listParam.Add(new viviLib.Data.SearchParam("manageId", ManageId));
                    listParam.Add(new viviLib.Data.SearchParam("status", 2));
                }
            }
            if (!string.IsNullOrEmpty(sysorderid))
            {
                listParam.Add(new viviLib.Data.SearchParam("orderid", sysorderid));
            }
            if (!string.IsNullOrEmpty(userorderid))
            {
                listParam.Add(new viviLib.Data.SearchParam("userorder", userorderid));
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

            

            if (!string.IsNullOrEmpty(txtSuppOrder.Text.Trim()))
            {
                listParam.Add(new viviLib.Data.SearchParam("supplierOrder", txtSuppOrder.Text.Trim()));
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

           

            DataSet pageData = viviapi.BLL.Order.Bank.Factory.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby,true);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = pageData.Tables[1];
            this.rptOrders.DataBind();

            if (this.currPage > -1)
                this.Pager1.CurrentPageIndex = this.currPage;

            DataTable stat = pageData.Tables[2];
            if (stat.Rows.Count >= 0)
            {
                if (stat.Rows[0]["realvalue"] != DBNull.Value)
                {
                    TotalTranATM = Convert.ToDecimal(stat.Rows[0]["realvalue"]).ToString("f2");
                }
                if (stat.Rows[0]["payAmt"] != DBNull.Value)
                {
                    TotalUserATM = Convert.ToDecimal(stat.Rows[0]["payAmt"]).ToString("f2");
                }
                if (stat.Rows[0]["commission"] != DBNull.Value)
                {
                    TotalCommission = Convert.ToDecimal(stat.Rows[0]["commission"]).ToString("f2");
                }
                if (stat.Rows[0]["profits"] != DBNull.Value)
                {
                    //TotalPromATM = Convert.ToDecimal(stat.Rows[0]["promAmt"]).ToString("f2");
                    TotalProfit = Convert.ToDecimal(stat.Rows[0]["profits"]).ToString("f2");
                }
            }
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

            return Factory.GetModelByCode(int.Parse(obj.ToString())).name;
        }

        #region rptOrders_ItemDataBound
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (this.isSuperAdmin == false)
                {
                    HtmlTableCell _th_profits = e.Item.FindControl("th_profits") as HtmlTableCell;
                    if (_th_profits != null)
                        _th_profits.Visible = false;

                    HtmlTableCell _th_supplier = e.Item.FindControl("th_supplier") as HtmlTableCell;
                    if (_th_supplier != null)
                        _th_supplier.Visible = false;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (this.isSuperAdmin == false)
                {
                    HtmlTableCell _th_profits = e.Item.FindControl("tr_profits") as HtmlTableCell;
                    if (_th_profits != null)
                        _th_profits.Visible = false;

                    HtmlTableCell _th_supplier = e.Item.FindControl("tr_supplier") as HtmlTableCell;
                    if (_th_supplier != null)
                        _th_supplier.Visible = false;
                }

                Button btnReissue = e.Item.FindControl("btnReissue") as Button;
                Button btnRest    = e.Item.FindControl("btnRest") as Button;
                Button btnDeduct  = e.Item.FindControl("btnDeduct") as Button;
                Button btnReDeduct = e.Item.FindControl("btnReDeduct") as Button;

                int userid = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "userid").ToString());
                string status = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                
                switch (status)
                {
                    case "1":
                        btnReissue.Visible = false;
                        btnRest.Visible = false;
                        btnDeduct.Visible =false;
                        btnReDeduct.Visible = false;
                        break;
                    case "2":
                        btnReissue.Visible = true;
                        btnRest.Visible = false;
                        btnReDeduct.Visible = false;
                        btnDeduct.Visible = false;

                        /*btnDeduct.OnClientClick = "return confirm('是否确定扣量？')";
                        object completeTime = DataBinder.Eval(e.Item.DataItem, "CompleteTime");
                        double difftime = GetDifftime(userid,completeTime);
                        if (difftime > viviapi.SysConfig.RuntimeSetting.DeductSafetyTime)
                        {
                            btnDeduct.Text = "扣";
                        }
                        else if (difftime > 0.0 && difftime <= viviapi.SysConfig.RuntimeSetting.DeductSafetyTime)
                        {
                            btnDeduct.Text = "危险";
                        }
                        else
                        {
                            btnDeduct.Text = "不能";
                        }  */                      
                        break;
                    case "4":                    
                        btnReissue.Visible = true;
                        btnRest.Visible = false;
                        btnDeduct.Visible = false;
                        btnReDeduct.Visible = false;
                        break;
                    case "8":
                        btnReissue.Visible = true;
                        btnRest.Visible = false;
                        btnDeduct.Visible = false;
                        btnReDeduct.Visible = false;
                        break;
                }
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="completeTime"></param>
        /// <returns></returns>
        public double GetDifftime(int userId,object completeTime)
        {
            DateTime _comptime = DateTime.MinValue;

            UserAccessTimeInfo acctInfo = BLL.User.UserAccessTime.GetModel(userId);
            if (acctInfo == null)
                return 1000.0;

            DateTime? userAcceTime = BLL.User.UserAccessTime.GetModel(userId).lastAccesstime;
            if (userAcceTime.HasValue)
                _comptime = userAcceTime.Value;

            DateTime _comptime2 = Convert.ToDateTime(completeTime);

            return _comptime2.Subtract(_comptime).TotalMinutes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            string Url = "BankOrderList.aspx?status=2";
            Url += "&ctype=" + ddlChannelType.SelectedValue;
            Url += "&userid=" + txtuserid.Text;
            Url += "&ns=" + ddlNotifyStatus.SelectedValue;
            //Url += "&ka=" + txtCardNo.Text;
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
                    string orderId = e.CommandArgument.ToString();
                    if (string.IsNullOrEmpty(orderId))
                        return;

                    //OrderBankNotify notity = new OrderBankNotify();
                    string callback = "";// notity.SynchronousNotify(orderId);

                    AlertAndRedirect("返回：" + callback, Url);
                }
                else if (e.CommandName == "ResetOrder")
                {
                    string Argument = e.CommandArgument.ToString();
                    if (string.IsNullOrEmpty(Argument))
                        return;
                    string[] arr = Argument.Split('$');

                    Response.Redirect(string.Format("ResetOrder.aspx?orderid={0}&oclass=1&supp={1}&amt={2}", arr[0], arr[1], arr[2]));
                }
                else if (e.CommandName == "Deduct")//扣量
                {
                    string orderId = e.CommandArgument.ToString();
                    //BLL.OrderBank bll = new OrderBank();
                    //if (bll.Deduct(orderId))
                    //{
                    //    AlertAndRedirect("扣量成功", Url);                        
                    //}
                    //else
                    //{
                    //    AlertAndRedirect("扣量失败，可能是余额不足", Url);
                    //}
                    LoadData();
                }
                else if (e.CommandName == "ReDeduct")//归还订单
                {
                    string orderId = e.CommandArgument.ToString();
                    //BLL.OrderBank bll = new OrderBank();
                    //if (bll.ReDeduct(orderId))
                    //{
                    //    AlertAndRedirect("还单成功", Url);
                    //}
                    //else
                    //{
                    //    AlertAndRedirect("还单失败", Url);
                    //}
                    LoadData();
                }
            }
            catch(Exception ex)
            {
                AlertAndRedirect(ex.Message, Url);                
            }
        }

        protected string GetParm(object orderid, object supp,object amt)
        {
            try
            {
                return string.Format("{0}${1}${2}", orderid, supp, amt);
            }
            catch
            {
                return string.Format("{0}${1}${2}", "", "", "0.00");
            }
        }
    }
}