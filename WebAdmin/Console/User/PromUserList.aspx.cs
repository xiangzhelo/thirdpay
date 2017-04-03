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
using viviLib.Data;
using DBAccess;

namespace viviapi.web.Manage.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PromUserList : viviapi.WebComponents.Web.ManagePageBase
    {
        protected string wzfmoney = string.Empty;
        protected string yzfmoney = string.Empty;

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

        public string UserStatus
        {
            get
            {
                return WebBase.GetQueryStringString("UserStatus", "");
            }
        }

        public string cmd
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }

        public int UserID
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
                if (!string.IsNullOrEmpty(UserStatus))
                {
                    this.StatusList.SelectedValue = UserStatus;
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
            if (!string.IsNullOrEmpty(this.cmd) && this.UserID > 0)
            {
                List<UsersUpdateLog> updateList = new List<UsersUpdateLog>();            

                UserInfo userinfo = BLL.User.Factory.GetModel(UserID);
                if (cmd == "ok")
                {
                    updateList.Add(newUpdateLog("Status", ((int)userinfo.Status).ToString(), "2","审核"));
                    userinfo.Status = 2;
                }
                else if (cmd == "del")
                {
                    updateList.Add(newUpdateLog("Status", ((int)userinfo.Status).ToString(), "4","锁定"));
                    userinfo.Status = 4;
                }
                else if (cmd == "pok")
                {
                    updateList.Add(newUpdateLog("UserType", ((int)userinfo.UserType).ToString(), "2", "设为代理"));
                    updateList.Add(newUpdateLog("UserLevel", ((int)userinfo.UserLevel).ToString(), "1", "设为代理"));

                    userinfo.UserType =  UserTypeEnum.代理;
                    userinfo.UserLevel = 1;
                }
                else if (cmd == "pdel")
                {
                    updateList.Add(newUpdateLog("UserType", ((int)userinfo.UserType).ToString(), "1", "取消代理"));
                    updateList.Add(newUpdateLog("UserLevel", ((int)userinfo.UserLevel).ToString(), "100", "取消代理"));

                    userinfo.UserType   =  UserTypeEnum.会员;
                    userinfo.UserLevel  =  1;
                }
                if (BLL.User.Factory.Update(userinfo, updateList))
                {
                    AlertAndRedirect("操作成功", "PromUserList.aspx");
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
            listParam.Add(new viviLib.Data.SearchParam("userType", 2));
            if (!string.IsNullOrEmpty(this.StatusList.SelectedValue))
                listParam.Add(new viviLib.Data.SearchParam("status", int.Parse(this.StatusList.SelectedValue)));
            string keyword = KeyWordBox.Text.Trim();

            if (!string.IsNullOrEmpty(this.SeachType.SelectedValue) && !string.IsNullOrEmpty(keyword))
            {
                if (this.SeachType.SelectedValue.ToLower() == "userid")
                {
                    int userId = 0;
                    int.TryParse(keyword, out userId);
                    listParam.Add(new viviLib.Data.SearchParam("id", userId));
                }
                else if (this.SeachType.SelectedValue == "UserName")
                {
                    listParam.Add(new viviLib.Data.SearchParam("userName", keyword));
                }
            }

            string orderby = orderBy + " " + orderByType;

            DataSet pageData = BLL.User.Factory.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);

            DataTable proms = pageData.Tables[1];
            DataTable webinfos = WebInfoFactory.GetList(string.Empty);
            
            proms.Columns.Add("Mystr");
            proms.Columns.Add("agstr");                      
            proms.Columns.Add("prourl");
            foreach (DataRow row in proms.Rows)
            {
                foreach (DataRow website in webinfos.Rows)
                {
                    if (int.Parse(row["ID"].ToString()) == int.Parse(website["ID"].ToString()))
                    {
                        row["prourl"] = website["Domain"].ToString();
                    }
                }
            }
            this.rptUsers.DataSource = proms;
            this.rptUsers.DataBind();

        }
        #endregion      

        protected void Pager1_PageChanged(object sender, EventArgs e)
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
            if (e.Item.ItemType == ListItemType.Header)
            {
                //switch (orderBy)
                //{
                //    case "balance":
                //        HyperLink hlinkOrderby = (HyperLink)e.Item.FindControl("hlinkOrderby");
                //        if (this.orderByType == "asc")
                //        {
                //            hlinkOrderby.Text = "余额↓";
                //            hlinkOrderby.NavigateUrl = "?orderby=balance&type=desc";
                //        }
                //        else
                //        {
                            
                //            hlinkOrderby.Text = "余额↑";
                //            hlinkOrderby.NavigateUrl = "?orderby=balance&type=asc";
                //        }
                //        break;
                //}
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string userType = DataBinder.Eval(e.Item.DataItem, "userType").ToString();
                string userStatus = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                string userLev = DataBinder.Eval(e.Item.DataItem, "userLevel").ToString();
                string userId = DataBinder.Eval(e.Item.DataItem, "id").ToString();

                Label lblUserType = (Label)e.Item.FindControl("lblUserType");
                lblUserType.Text = Enum.GetName(typeof(UserTypeEnum), int.Parse(userType));
                Label lblUserStat = (Label)e.Item.FindControl("lblUserStat");
                lblUserStat.Text = Enum.GetName(typeof(UserStatusEnum), int.Parse(userStatus));
                Label lblUserLevel = (Label)e.Item.FindControl("lblUserLevel");
                lblUserLevel.Text = Enum.GetName(typeof(UserLevelEnum), int.Parse(userLev));

                string cmd = string.Empty;
                #region 
                if (userStatus == "1")
                {
                    cmd = string.Format("<a onclick=\"return confirm('你确定要通过该用户吗？')\" href=\"?cmd=ok&ID={0}\" style=\"color:Green;\">通过</a> <a onclick=\"return confirm('你确定要锁定该用户吗？')\" href=\"?cmd=del&ID={0}\" style=\"color:red;\">锁定</a>", userId);
                }
                else if (userStatus == "2")
                {
                    cmd = string.Format("<a onclick=\"return confirm('你确定要锁定该用户吗？')\" href=\"?cmd=del&ID={0}\" style=\"color:red;\">锁定</a>",userId);
                }
                else if (userStatus == "4")
                {
                    cmd = string.Format("<a onclick=\"return confirm('你确定要恢复该用户吗？')\" href=\"?cmd=ok&ID={0}\">恢复</a>", userId);
                }
                Label labcmd = (Label)e.Item.FindControl("labcmd");
                labcmd.Text = cmd;
                #endregion

                #region 
                cmd = string.Empty;
                if (userType == "1")
                {
                    cmd = string.Format(" <a onclick=\"return confirm('你确定要将该用户设为代理吗？')\" href=\"?cmd=pok&ID={0}\" style=\"color:red;\">设为代理</a>",userId);
                }
                else if (userType == "2")
                {
                    cmd = string.Format("<a onclick=\"return confirm('你确定要取消该用户的代理权限吗？')\" href=\"?cmd=pdel&ID={0}\" style=\"color:red;\">取消代理权限</a>", userId);
                }

                Label labagcmd = (Label)e.Item.FindControl("labagcmd");
                labagcmd.Text = cmd;
                #endregion

            }            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        private UsersUpdateLog newUpdateLog(string f, string n, string o,string desc)
        {
            UsersUpdateLog item = new UsersUpdateLog();
            item.userid = UserID;
            item.Addtime = DateTime.Now;
            item.field = f;
            item.newvalue = n;
            item.oldValue = o;
            item.Editor = BLL.ManageFactory.CurrentManage.username;
            item.OIp = viviLib.Web.ServerVariables.TrueIP;
            item.Desc = desc;
            return item;
        }
    }
}