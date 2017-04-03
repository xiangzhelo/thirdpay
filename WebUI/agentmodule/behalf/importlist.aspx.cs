using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using viviapi.BLL.Finance;
using viviapi.Model.Finance;
using viviapi.Model.Finance.Agent;
using viviapi.WebComponents.Web;
using viviLib.Data;


namespace viviAPI.WebUI7uka.agentmodule.behalf
{
    public partial class Importlist : AgentPageBase
    {
        protected viviapi.BLL.Finance.Agent.WithdrawAgentSummary _bll = new viviapi.BLL.Finance.Agent.WithdrawAgentSummary();
        protected viviapi.BLL.Finance.Agent.WithdrawAgent stlAgtBLL = new viviapi.BLL.Finance.Agent.WithdrawAgent();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
                LoadData();
            }
        }

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

        void InitForm()
        {
            this.sdate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.edate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }


        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {

            List<SearchParam> listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("userid", this.UserId));
            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(sdate.Value.Trim()))
            {
                if (DateTime.TryParse(sdate.Value.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("starttime", tempdt));
                    }
                }
            }

            if (!string.IsNullOrEmpty(edate.Value.Trim()))
            {
                if (DateTime.TryParse(edate.Value.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("endtime", tempdt.AddDays(1)));
                    }
                }
            }

            DataSet pageData = _bll.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, false);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptrecharges.DataSource = pageData.Tables[1];
            this.rptrecharges.DataBind();


        }
        public string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", date, hour, m, s);
        }
        #endregion



        protected void b_search_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string audit_status = DataBinder.Eval(e.Item.DataItem, "audit_status").ToString();
                if (audit_status == "1")
                {
                    Button btnSure = e.Item.FindControl("btnSure") as Button;
                    Button btnCancel = e.Item.FindControl("btnCancel") as Button;

                    btnSure.Visible = true;
                    btnCancel.Visible = true;
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (rptrecharges.Items.Count == 0)
                {
                    Literal lit = (Literal)e.Item.FindControl("litfoot");
                    lit.Text = @" <tfoot>
                        <tr>
                            <td colspan=""10"" class=""nomsg"">
                                －_－^..暂无记录
                            </td>
                        </tr>
                     </tfoot>     ";
                }
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void rptrecharges_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                AlertAndRedirect(message, "importlist.aspx");
            }
        }

        string Affirm(string lotno, int status)
        {
            string message = string.Empty;

            int result = _bll.Affirm(lotno, status, UserId, CurrentUser.UserName, viviLib.Web.ServerVariables.TrueIP);
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
                message = "输入状态不正确";
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
                    List<WithdrawAgent> list = stlAgtBLL.GetModelList(string.Format("lotno='{0}'",lotno));
                    foreach (var item in list)
                    {
                        if (item.tranApi > 0 && item.is_cancel == false)
                        {
                            //viviapi.ETAPI.Withdraw.InitDistribution2(item);
                        }
                    }
                }
            }
            #endregion

            return message;
        }
    }
}
