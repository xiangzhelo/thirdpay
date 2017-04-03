using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBAccess;
using viviapi.BLL.Finance;
using viviapi.Model.Channel;
using viviapi.Model.Finance;
using viviapi.Model.Finance.Agent;
using viviapi.Model.Settled;

using viviapi.WebComponents.Web;
using viviLib.Data;
using System.Data;

namespace viviAPI.WebUI7uka.usermodule.behalf
{
    public partial class Agentdists : UserPageBase
    {
        protected viviapi.BLL.Finance.Agent.WithdrawAgent stlAgtBLL = new viviapi.BLL.Finance.Agent.WithdrawAgent();
        TocashSchemeInfo _scheme = null;
        /// <summary>
        /// 
        /// </summary>
        protected TocashSchemeInfo scheme
        {
            get
            {
                if (_scheme == null)
                    _scheme = TocashScheme.GetModelByUser(2, this.UserId);

                return _scheme;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
                LoadData();
            }
        }

        void InitForm()
        {
            //this.sdate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            //this.edate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        void LoadData()
        {

            #region Param List
            var listParam = new List<viviLib.Data.SearchParam>();
            listParam.Add(new viviLib.Data.SearchParam("userid", UserId));
            listParam.Add(new viviLib.Data.SearchParam("mode", 1));

            //系统交易号
            if (!String.IsNullOrEmpty(txttrade_no.Value.Trim()))
            {
                listParam.Add(new SearchParam("trade_no", this.txttrade_no.Value.Trim()));
            }

            //商户订单号
            if (!String.IsNullOrEmpty(txtout_trade_no.Value.Trim()))
            {
                listParam.Add(new SearchParam("out_trade_no", this.txtout_trade_no.Value.Trim()));
            }

            //收款银行
            if (!String.IsNullOrEmpty(ddlbankCode.SelectedValue))
            {
                listParam.Add(new SearchParam("bankCode", this.ddlbankCode.SelectedValue));
            }

            //收款账户
            if (!String.IsNullOrEmpty(this.txtbankAccountName.Text))
            {
                listParam.Add(new SearchParam("bankAccountName", this.txtbankAccountName.Text));
            }



            //审核状态
            if (!string.IsNullOrEmpty(ddlaudit_status.SelectedValue))
            {
                listParam.Add(new SearchParam("audit_status", int.Parse(ddlaudit_status.SelectedValue)));
            }
            //付款状态
            if (!string.IsNullOrEmpty(ddlpayment_status.SelectedValue))
            {
                listParam.Add(new SearchParam("payment_status", int.Parse(ddlpayment_status.SelectedValue)));
            }


            #endregion

            DataSet pageData = stlAgtBLL.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, 0);


            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            DataTable data = pageData.Tables[1];


            this.rptDetails.DataSource = data;
            this.rptDetails.DataBind();

        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string issure = DataBinder.Eval(e.Item.DataItem, "issure").ToString();
                if (issure == "1")
                {
                    Button btnSure = e.Item.FindControl("btnSure") as Button;
                    Button btnCancel = e.Item.FindControl("btnCancel") as Button;

                    btnSure.Visible = true;
                    btnCancel.Visible = true;
                }
            }
        }

        protected void b_search_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void rptDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string message = string.Empty;
                string lotno = e.CommandArgument.ToString();

                if (e.CommandName == "sure")
                {
                    message = Affirm(lotno, 2);
                }
                else if (e.CommandName == "cancel")
                {
                    message = Affirm(lotno, 3);
                }
                AlertAndRedirect(message, "agentdists.aspx");
            }
        }

        string Affirm(string trade_no, byte status)
        {
            string message = string.Empty;

            int result = stlAgtBLL.Affirm(trade_no, status, viviLib.Web.ServerVariables.TrueIP);
            if (result == 0)
            {
                message = "操作成功";
            }
            else if (result == 1)
            {
                message = "不存在此单";
            }
            else if (result == 2)
            {
                message = "此单已处理，不可重复操作";
            }
            else if (result == 3)
            {
                message = "输入不正确";
            }
            else if (result == 4)
            {
                message = "系统出错";
            }
            else
            {
                message = "未知错误";
            }

            #region 直接走接口
            ///
            if (status == 2 && result == 0)
            {
                if (scheme.tranRequiredAudit == 0 && scheme.vaiInterface == 1)
                {
                    var item = stlAgtBLL.GetModel(trade_no);

                    if (item.tranApi > 0 && item.is_cancel == false)
                    {
                      //  viviapi.ETAPI.Withdraw.InitDistribution2(item);
                    }
                }
            }
            #endregion

            return message;
        }
    }
}
