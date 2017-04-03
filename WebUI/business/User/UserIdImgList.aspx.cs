using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.Model.User;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;
using viviLib.Data;

namespace viviapi.web.business.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserIdImgLists : viviapi.WebComponents.Web.BusinessPageBase
    {

        public string orderBy
        {
            get
            {
                return WebBase.GetQueryStringString("orderby", "balance");
            }
        }

        public string orderByType
        {
            get
            {
                return WebBase.GetQueryStringString("type", "asc");
            }
        }

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

        public int UserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("userid", 0);
            }
        }

        public int proid
        {
            get
            {
                return WebBase.GetQueryStringInt32("proid", 0);
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
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

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
            if (!string.IsNullOrEmpty(this.cmd) && this.ItemID > 0 && this.UserId > 0)
            {
                usersIdImageInfo update = new usersIdImageInfo();
                update.id = this.ItemID;
                if (cmd == "ok")
                {
                    update.status = IdImageStatus.审核成功;
                }
                if (cmd == "fail")
                {
                    update.status = IdImageStatus.审核失败;
                }
                update.why = string.Empty;
                update.checktime = DateTime.Now;
                update.admin = this.ManageId;
                update.userId = this.UserId;
                BLL.User.usersIdImage bll = new viviapi.BLL.User.usersIdImage();
                if (bll.Check(update))
                {
                    AlertAndRedirect("操作成功", "UserIdImgList.aspx?s=1");
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
            bool result = BLL.ManageFactory.CheckCurrentPermission(false
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

            string orderby = string.Empty;

            DataSet pageData = BLL.User.usersIdImage.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptIamges.DataSource = pageData.Tables[1];
            this.rptIamges.DataBind();

        }
        #endregion

        protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.LoadData();
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
                    cmd = string.Format("<a onclick=\"return confirm('审核成功?')\" href=\"?cmd=ok&ID={0}&userid={1}\" style=\"color:Green;\">通过</a> <a onclick=\"return confirm('审核失败？')\" href=\"?cmd=fail&ID={0}&userid={1}\" style=\"color:red;\">失败</a>", itemid, _userid);
                }
                Label labcmd = (Label)e.Item.FindControl("labagcmd");
                labcmd.Text = cmd;
                #endregion
            }
        }

        protected string getpassview(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return string.Empty;
            if (Convert.ToInt32(obj) > 0)
                return "√";
            else
                return "×";
        }
        protected void btnCashTo_Click(object sender, EventArgs e)
        {
            //string ids = this.
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["chkItem"];
            foreach (string id in ids.Split(','))
            {
                BLL.User.usersIdImage bll = new viviapi.BLL.User.usersIdImage();
                bll.Delete(int.Parse(id));
            }
            this.LoadData();
        }
    }
}