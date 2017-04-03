using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL.Finance.Agent;
using viviapi.Model;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.AgentWithdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SettledAgentSummarys : viviapi.WebComponents.Web.ManagePageBase
    {
        protected WithdrawAgentSummary _bll = new WithdrawAgentSummary();
       protected WithdrawAgent stlAgtBLL = new WithdrawAgent();

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public int ItemInfoStatus
        {
            get
            {
                return WebBase.GetQueryStringInt32("status", 0);
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
            setPower();
            if (!this.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                this.BindData();
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Financial);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        #region BindData
        /// <summary>
        /// 
        /// </summary>
        private void BindData()
        {
            var listParam = new List<SearchParam>();

            int tempId = 0;
            if (!String.IsNullOrEmpty(txtUserId.Text.Trim()))
            {
                if (int.TryParse(this.txtUserId.Text.Trim(), out tempId))
                {
                    listParam.Add(new SearchParam("userid", tempId));
                }
            }

            if (!String.IsNullOrEmpty(txtLotno.Text.Trim()))
            {
                listParam.Add(new SearchParam("lotno", txtLotno.Text.Trim()));
            }


            if (!String.IsNullOrEmpty(ddlstatus.Text.Trim()))
            {
                if (int.TryParse(this.ddlstatus.Text.Trim(), out tempId))
                {
                    listParam.Add(new SearchParam("status", tempId));
                }
            }

            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(StimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(StimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("saddtime", StimeBox.Text.Trim()));
                    }
                }
            }

            if (!string.IsNullOrEmpty(EtimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(EtimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("eaddtime", tempdt.AddDays(1)));
                    }
                }
            }

            DataSet pageData = _bll.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty,false);


            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            DataTable data = pageData.Tables[1];

            this.rptApply.DataSource = data;
            this.rptApply.DataBind();
        }
        #endregion

        #region 查找
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region 批量审核
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPass_Click(object sender, EventArgs e)
        {
            //string ids = Request.Form["ischecked"];
            //DataTable withdrawListByApi = null;
            //string batchNo = Guid.NewGuid().ToString("N");

            //if (!string.IsNullOrEmpty(ids))
            //{
            //    if (viviapi.BLL.SettledFactory.BatchPass(ids, batchNo, out withdrawListByApi))
            //    {
            //        if (withdrawListByApi != null && withdrawListByApi.Rows.Count > 0)
            //        {
            //            List<SettledInfo> modelList = viviapi.BLL.SettledFactory.DataTableToList(withdrawListByApi);
            //            foreach (viviapi.Model.SettledInfo _info in modelList)
            //            {
            //                //ETAPI.Common.Withdraw.InitDistribution(_info);
            //            }
            //        }

            //        AlertAndRedirect("审核成功!");
            //        BindData();
            //    }
            //    else
            //    {
            //        AlertAndRedirect("审核失败!");
            //    }
            //}
            //else
            //{
            //    AlertAndRedirect("请选择要审核的申请!");
            //}
        }
        #endregion

        #region 分页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            BindData();
        }
        #endregion

        #region btnAllPass_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAllPass_Click(object sender, EventArgs e)
        {            
            //string batchNo = Guid.NewGuid().ToString("N");
            //if (viviapi.BLL.SettledFactory.AllPass(batchNo))
            //{
            //    DataTable withdrawListByApi = viviapi.BLL.SettledFactory.GetListWithdrawByApi(batchNo);
            //    if (withdrawListByApi != null && withdrawListByApi.Rows.Count > 0)
            //    {
            //        List<SettledInfo> modelList = viviapi.BLL.SettledFactory.DataTableToList(withdrawListByApi);
            //        foreach(viviapi.Model.SettledInfo _info in modelList)
            //        {
            //            //ETAPI.Common.Withdraw.InitDistribution(_info);
            //        }
            //    }
            //    AlertAndRedirect("审核成功!");
            //    BindData();
            //}
            //else
            //{
            //    AlertAndRedirect("审核失败!");
            //}
        }
        #endregion

        #region btnallfail_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnallfail_Click(object sender, EventArgs e)
        {
            //if (viviapi.BLL.SettledFactory.Allfails())
            //{
            //    AlertAndRedirect("操作成功!");
            //    BindData();
            //}
            //else
            //{
            //    AlertAndRedirect("操作失败!");
            //}
        }
        #endregion

        #region GetTranApiName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetTranApiName(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "不走接口";

            int id = Convert.ToInt32(obj);
            if (id == 100)
                return "财付通";

            return "";
        }
        #endregion

        #region Pager1_PageChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region rptApply_ItemDataBound
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptApply_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow tr_detail = e.Item.FindControl("tr_detail") as HtmlTableRow;
                Literal litimg = e.Item.FindControl("litimg") as Literal;
                litimg.Text = string.Format("<img src=\"../style/images/folder_close.gif\" style=\"cursor: hand\" onclick=\"collapse(this, '{0}')\" alt=\"\" />"
                       , tr_detail.ClientID);

                string lotno = DataBinder.Eval(e.Item.DataItem, "lotno").ToString();
                string status = DataBinder.Eval(e.Item.DataItem, "status").ToString();

                Repeater rptList = (Repeater)e.Item.FindControl("rptList");
                DataSet details = stlAgtBLL.GetList(string.Format("lotno='{0}'",lotno));
                rptList.DataSource = details;
                rptList.DataBind();   
            }
        }
        #endregion

        #region rptApply_ItemCommand
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptApply_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string _id = e.CommandArgument.ToString();
                if (!string.IsNullOrEmpty(_id))
                {
                    int _s = -1;
                    if (e.CommandName == "Pass")
                    {
                        _s = 2;
                    }
                    else if (e.CommandName == "Refuse")
                    {
                        _s = 4;
                    }
                    if (_s != -1)
                    {
                        //bool result = viviapi.BLL.SettledFactory.Audit(int.Parse(_id), _s);
                        //if (result == true)
                        //{
                        //    if (_s == 2)
                        //    {
                        //        viviapi.Model.SettledInfo info = viviapi.BLL.SettledFactory.GetModel(int.Parse(_id));
                        //        if (info.status == SettledStatus.付款接口支付中)
                        //        {
                        //            //走接口
                        //            //ETAPI.Common.Withdraw.InitDistribution(info);
                        //        }
                        //    }
                        //}
                    }
                }
            }
            BindData();
        }
        #endregion        

        #region rptList_ItemCommand
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }
        #endregion

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
               
            }
        }
    }
}