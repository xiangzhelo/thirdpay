using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserHosts : viviapi.WebComponents.Web.ManagePageBase
    {
        viviapi.BLL.User.UserHost bll = new viviapi.BLL.User.UserHost();

        public string ItemStatus
        {
            get
            {
                return WebBase.GetQueryStringString("s", "");
            }
        }
        public string cmd
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }

        public int ItemID
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
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
            DoCmd();
            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(ItemStatus))
                {
                    this.StatusList.SelectedValue = ItemStatus;
                }
                this.LoadData();
            }
        }
        #region DoCmd
        /// <summary>
        /// 
        /// </summary>
        void DoCmd()
        {
            if (!string.IsNullOrEmpty(this.cmd) && this.ItemID > 0)
            {
                int status = 1;
                if (cmd == "close")
                {
                    status = 2;
                }
               
                if (bll.ChangeStatus(ItemID,status))
                {
                    AlertAndRedirect("操作成功", "UserHosts.aspx");
                }
                else
                {
                    AlertAndRedirect("操作失败");
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
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Merchant);

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
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();

            //if (!isSuperAdmin)
            //{
            //    listParam.Add(new viviLib.Data.SearchParam("manageId", ManageId));
            //}
            if (!string.IsNullOrEmpty(this.StatusList.SelectedValue))
                listParam.Add(new viviLib.Data.SearchParam("status", int.Parse(this.StatusList.SelectedValue)));
            int userId = 0;
            if (int.TryParse(this.txtUserId.Text, out userId))
            {
                if (userId > 0)
                    listParam.Add(new viviLib.Data.SearchParam("userid", userId));
            }
            if (!string.IsNullOrEmpty(this.txtUserName.Text))
            {
                listParam.Add(new viviLib.Data.SearchParam("userName", this.txtUserName.Text));
            }
            
            string orderby = string.Empty;

            DataSet pageData = viviapi.BLL.User.UserHost.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);

            this.rptIamges.DataSource = pageData.Tables[1];
            this.rptIamges.DataBind();

        }
        #endregion

        protected string GetPaymentUrl(object id)
        {
            if (id == null || id == DBNull.Value)
                return string.Empty;

            return string.Format(viviapi.BLL.WebInfoFactory.CurrentWebInfo.PayUrl + "links/CheckPay.aspx?h={0}&k={1}", id
                ,viviLib.Security.Cryptography.MD5(id.ToString() + viviapi.BLL.Sys.Constant.ParameterEncryptionKey));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void rptUsersItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string itemid = DataBinder.Eval(e.Item.DataItem, "id").ToString();
                string _userid = DataBinder.Eval(e.Item.DataItem, "userid").ToString();
                string status = DataBinder.Eval(e.Item.DataItem, "status").ToString();

                string cmd = string.Empty;
                #region
                if (status == "1")
                {
                    cmd = string.Format("<a onclick=\"return confirm('关闭？')\" href=\"?cmd=close&ID={0}&userid={1}\" style=\"color:red;\">关闭</a>", itemid, _userid);
                }
                else if (status == "2")
                {
                    cmd = string.Format("<a onclick=\"return confirm('开启?')\" href=\"?cmd=open&ID={0}&userid={1}\" style=\"color:Green;\">开启</a>", itemid, _userid);
                }
                Label labcmd = (Label)e.Item.FindControl("labagcmd");
                labcmd.Text = cmd;
                #endregion
            }
        }
                
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string ids = Request.Form["chkItem"];
                foreach (string id in ids.Split(','))
                {
                    viviapi.BLL.User.UserHost bll = new viviapi.BLL.User.UserHost();
                    bll.Delete(int.Parse(id));
                }
            }
            catch { }
            this.LoadData();
        }
        protected void btnOpen_Click(object sender, EventArgs e)
        {

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }
}
}