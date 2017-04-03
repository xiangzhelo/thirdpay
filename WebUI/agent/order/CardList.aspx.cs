using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Order.Card;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.agent.order
{
    public partial class CardOrderList : AgentPageBase
    {
        /// <summary>
        /// 交易金额
        /// </summary>
        protected string TotalTranATM = string.Empty;

        /// <summary>
        /// 商户金额
        /// </summary>
        protected string TotalUserATM = string.Empty;

        /// <summary>
        /// 代理总提成
        /// </summary>
        protected string TotalPromATM = string.Empty;

        /// <summary>
        /// 平台利润
        /// </summary>
        protected string TotalProfit = string.Empty;

        //业务员提成
        protected string TotalCommission = "0.00";


        #region
       

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
            
            if (this.ctype > -1)
            {
                ddlChannelType.SelectedValue = ctype.ToString();
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

            
        }
        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            var listParam = new List<viviLib.Data.SearchParam>();
            int tempId = 0;

            listParam.Add(new viviLib.Data.SearchParam("agentid", this.UserId));
            

            if (!string.IsNullOrEmpty(this.txtOrderId.Text.Trim()))
            {
                listParam.Add(new viviLib.Data.SearchParam("orderId_like", txtOrderId.Text.Trim()));
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
            if (!string.IsNullOrEmpty(txtCardNo.Text.Trim()))
            {
                listParam.Add(new viviLib.Data.SearchParam("cardNo", txtCardNo.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(txtSuppOrder.Text.Trim()))
            {
                listParam.Add(new viviLib.Data.SearchParam("supplierOrder", txtSuppOrder.Text.Trim()));
            }

            listParam.Add(new viviLib.Data.SearchParam("status", 2));

           

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



            DataSet pageData = Factory.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby,true);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = pageData.Tables[1];
            this.rptOrders.DataBind();

            if (this.currPage > -1)
                this.Pager1.CurrentPageIndex = currPage;

            DataTable stat = pageData.Tables[2];
            TotalTranATM = stat.Rows[0]["realvalue"].ToString();
            TotalUserATM = stat.Rows[0]["payAmt"].ToString();
            TotalPromATM = stat.Rows[0]["promAmt"].ToString();
            TotalProfit = stat.Rows[0]["profits"].ToString();
            if (stat.Rows[0]["promAmt"] != DBNull.Value)
            {
                TotalCommission = Convert.ToDecimal(stat.Rows[0]["promAmt"]).ToString("f2");
            }

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

        #region rptOrders_ItemDataBound
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {               
                HtmlTableRow carddetail = e.Item.FindControl("tr_carddetail") as HtmlTableRow;
                Literal litimg = e.Item.FindControl("litimg") as Literal;
                int userid = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "userid").ToString());
                int cardnum = 0;
                object cardnumobj = DataBinder.Eval(e.Item.DataItem, "cardnum");
                if (cardnumobj  != DBNull.Value)
                {
                    cardnum = Convert.ToInt32(cardnumobj);
                }
                if (cardnum > 1)
                {
                                      
                }
                else
                {
                    carddetail.Style.Add("display", "none");
                }
                
                string status = DataBinder.Eval(e.Item.DataItem, "status").ToString();

               
            }
        }
        #endregion

        #region rptOrders_ItemCommand
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

                    string callbackText = viviapi.SysInterface.Card.APINotification.SynchronousNotify(orderId);

                    ShowMessageBox("返回：" + callbackText);
                }
                if (e.CommandName == "Change")
                {
                    string orderId = e.CommandArgument.ToString();
                    if (string.IsNullOrEmpty(orderId))
                        return;

                    Response.Redirect(string.Format("ModifyOrder.aspx?orderid={0}", orderId));

                    //string callback = "调整失败";
                    //if (BLL.Order.Dal.UpdateCardOrderStatus(orderId))
                    //{
                    //    callback = "调整成功";
                    //}

                    //AlertAndRedirect(callback, Url);
                }
                else if (e.CommandName == "ResetOrder")
                {
                    Response.Redirect(string.Format("CardResetOrder.aspx?orderid={0}", e.CommandArgument));
                }
                else if (e.CommandName == "Deduct")//扣量
                {
                    string orderId = e.CommandArgument.ToString();

                    if (Factory.Instance.Deduct(orderId))
                    {
                        ShowMessageBox("扣量成功");
                    }
                    else
                    {
                        ShowMessageBox("扣量失败，可能是余额不足");
                    }
                    LoadData();
                }
                else if (e.CommandName == "ReDeduct")//归还订单
                {
                    string orderId = e.CommandArgument.ToString();
                    if (Factory.Instance.ReDeduct(orderId))
                    {
                        ShowMessageBox("还单成功");
                    }
                    else
                    {
                        ShowMessageBox("还单失败");
                    }
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
        #endregion

        #region rptcardDetail_ItemCommand
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptcardDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ResetOrder")
                {
                    string Argument = e.CommandArgument.ToString();
                    if (string.IsNullOrEmpty(Argument))
                        return;
                    string[] arr = Argument.Split('$');

                    Response.Redirect(string.Format("ResetOrder.aspx?orderid={0}&oclass=2&supp={1}&amt={2}", arr[0], arr[1], arr[2]));
                }

                #region 
                /*if (e.CommandName == "Reissue")
                {
                    string orderId = e.CommandArgument.ToString();
                    if (string.IsNullOrEmpty(orderId))
                        return;

                    OrderCardNotify notity = new OrderCardNotify();
                    string callback = notity.SynchronousNotify(orderId);

                    AlertAndRedirect("返回：" + callback);
                }
                else if (e.CommandName == "ResetOrder")
                {
                    string Argument = e.CommandArgument.ToString();
                    if (string.IsNullOrEmpty(Argument))
                        return;
                    string[] arr = Argument.Split('$');

                    Response.Redirect(string.Format("ResetOrder.aspx?orderid={0}&oclass=2&supp={1}&amt={2}", arr[0], arr[1], arr[2]));
                }
                else if (e.CommandName == "Deduct")//扣量
                {
                    string orderId = e.CommandArgument.ToString();

                    if (bll.Deduct(orderId))
                    {
                        AlertAndRedirect("扣量成功");
                    }
                    else
                    {
                        AlertAndRedirect("扣量失败，可能是余额不足");
                    }
                    LoadData();
                }
                else if (e.CommandName == "ReDeduct")//归还订单
                {
                    string orderId = e.CommandArgument.ToString();
                    BLL.OrderCard bll = new OrderCard();
                    if (bll.ReDeduct(orderId))
                    {
                        AlertAndRedirect("还单成功");
                    }
                    else
                    {
                        AlertAndRedirect("还单失败");
                    }
                    LoadData();
                }*/
                #endregion
            }
            catch (Exception ex)
            {
                AlertAndRedirect(ex.Message);
            }
        }
        #endregion

        protected void rptcardDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnitemRest = e.Item.FindControl("btnitemRest") as Button;
                string status = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                switch (status)
                {
                    case "1":
                        btnitemRest.Visible = true;
                        break;
                    case "2":
                        btnitemRest.Visible = false;
                        break;
                    case "4":
                        btnitemRest.Visible = false;
                        break;
                    case "8":
                        btnitemRest.Visible = false;
                        break;
                }
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
