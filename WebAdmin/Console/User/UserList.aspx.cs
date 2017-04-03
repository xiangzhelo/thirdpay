using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserList : ManagePageBase
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

        public int manageid
        {
            get
            {
                return WebBase.GetQueryStringInt32("manageid", 0);
            }
        }

        public int proid
        {
            get
            {
                return WebBase.GetQueryStringInt32("agentid", 0);
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

                DataTable data = viviapi.BLL.User.UserLevel.Instance.GetAllList().Tables[0];
                ddluserlevel.Items.Add(new ListItem("商户等级",""));
                foreach (DataRow row in data.Rows)
                {
                    ddluserlevel.Items.Add(new ListItem(row["levName"].ToString(), row["level"].ToString()));
                }
                if (this.proid > 0)
                    this.txtagent.Text = proid.ToString();
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
                var updateList = new List<UsersUpdateLog>();

                UserInfo userinfo = viviapi.BLL.User.Factory.GetModel(UserID);
                if (cmd == "ok")
                {
                    updateList.Add(newUpdateLog("Status", ((int)userinfo.Status).ToString(), "2", "审核"));
                    userinfo.Status = 2;
                }
                else if (cmd == "del")
                {
                    updateList.Add(newUpdateLog("Status", ((int)userinfo.Status).ToString(), "4", "锁定"));
                    userinfo.Status = 4;
                }
                else if (cmd == "pok")
                {
                    updateList.Add(newUpdateLog("UserType", ((int)userinfo.UserType).ToString(), "2", "设为代理"));
                    updateList.Add(newUpdateLog("UserLevel", ((int)userinfo.UserLevel).ToString(), "1", "设为代理"));

                    userinfo.UserType = UserTypeEnum.代理;
                    userinfo.UserLevel = 0;
                }
                else if (cmd == "pdel")
                {
                    updateList.Add(newUpdateLog("UserType", ((int)userinfo.UserType).ToString(), "1", "取消代理"));
                    updateList.Add(newUpdateLog("UserLevel", ((int)userinfo.UserLevel).ToString(), "100", "取消代理"));

                    userinfo.UserType = UserTypeEnum.会员;
                    userinfo.UserLevel =0;
                }
                if (viviapi.BLL.User.Factory.Update(userinfo, updateList))
                {
                    AlertAndRedirect("操作成功", "UserList.aspx");
                }
                else
                {
                    AlertAndRedirect("操作失败", "UserList.aspx?UserStatus=1");
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
            var listParam = new List<SearchParam>();

            if (isSuperAdmin == false)
            {
                listParam.Add(new SearchParam("manageId", ManageId));
            }
            else
            {
                if (manageid > 0)
                {
                    listParam.Add(new SearchParam("manageId", manageid));
                }
            }

            if (proid > 0)
                listParam.Add(new SearchParam("proid", proid));
            if (!string.IsNullOrEmpty(this.StatusList.SelectedValue))
                listParam.Add(new SearchParam("status", int.Parse(this.StatusList.SelectedValue)));
            if (!string.IsNullOrEmpty(this.ddlisSpecialPayRate.SelectedValue))
                listParam.Add(new SearchParam("special", int.Parse(this.ddlisSpecialPayRate.SelectedValue)));

            //string keyword = KeyWordBox.Text.Trim();

            //if (!string.IsNullOrEmpty(this.SeachType.SelectedValue) && !string.IsNullOrEmpty(keyword))
            //{
            //    if (this.SeachType.SelectedValue.ToLower() == "userid")
            //    {
            //        int userId = 0;
            //        int.TryParse(keyword, out userId);
            //        listParam.Add(new SearchParam("id", userId));
            //    }
            //    else if (this.SeachType.SelectedValue == "UserName")
            //    {
            //        listParam.Add(new SearchParam("userName", keyword));
            //    }
            //}
            if (!string.IsNullOrEmpty(this.txtuserName.Text.Trim()))
            {
                listParam.Add(new SearchParam("userName", txtuserName.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtuserId.Text.Trim()))
            {
                int userid = 0;
                if (int.TryParse(this.txtuserId.Text.Trim(), out userid))
                {
                    listParam.Add(new SearchParam("id", userid));
                }
            }
            if (!string.IsNullOrEmpty(this.ddluserlevel.SelectedValue))
            {
                listParam.Add(new SearchParam("userlevel", int.Parse(this.ddluserlevel.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.txtQQ.Text.Trim()))
            {
                listParam.Add(new SearchParam("qq", txtQQ.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtMail.Text.Trim()))
            {
                listParam.Add(new SearchParam("email", txtMail.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtTel.Text.Trim()))
            {
                listParam.Add(new SearchParam("tel", txtTel.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtfullname.Text.Trim()))
            {
                listParam.Add(new SearchParam("full_name", txtfullname.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(ddlSpecial.SelectedValue))
            {
                listParam.Add(new SearchParam("specialchannel", ddlSpecial.SelectedValue));
            }
            string orderby = orderBy + " " + orderByType;

            DataSet pageData = viviapi.BLL.User.Factory.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptUsers.DataSource = pageData.Tables[1];
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

        protected void RptUsersItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                switch (orderBy)
                {
                    case "balance":
                        HyperLink hlinkOrderby = (HyperLink)e.Item.FindControl("hlinkOrderby");
                        if (this.orderByType == "asc")
                        {
                            hlinkOrderby.Text = "余额↓";
                            hlinkOrderby.NavigateUrl = "?orderby=balance&type=desc";
                        }
                        else
                        {

                            hlinkOrderby.Text = "余额↑";
                            hlinkOrderby.NavigateUrl = "?orderby=balance&type=asc";
                        }
                        break;
                }
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string userType = DataBinder.Eval(e.Item.DataItem, "userType").ToString();
                string userStatus = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                string levName = DataBinder.Eval(e.Item.DataItem, "levName").ToString();
                string settles = DataBinder.Eval(e.Item.DataItem, "settles").ToString();
                string manageId = DataBinder.Eval(e.Item.DataItem, "manageId").ToString();

                Label lblUserType = (Label)e.Item.FindControl("lblUserType");
                lblUserType.Text = Enum.GetName(typeof(UserTypeEnum), int.Parse(userType));
                Label lblUserStat = (Label)e.Item.FindControl("lblUserStat");
                lblUserStat.Text = Enum.GetName(typeof(UserStatusEnum), int.Parse(userStatus));
                Label lblUserLevel = (Label)e.Item.FindControl("lblUserLevel");
                lblUserLevel.Text = levName;

                
                string userId = DataBinder.Eval(e.Item.DataItem, "id").ToString();

                string cmd = string.Empty;
                #region
                if (userStatus == "1")
                {
                    cmd = string.Format("<a onclick=\"return confirm('你确定要通过该用户吗？')\" href=\"?cmd=ok&ID={0}\" style=\"color:Green;\">通过</a> <a onclick=\"return confirm('你确定要锁定该用户吗？')\" href=\"?cmd=del&ID={0}\" style=\"color:red;\">锁定</a>", userId);
                }
                else if (userStatus == "2")
                {
                    cmd = string.Format("<a onclick=\"return confirm('你确定要锁定该用户吗？')\" href=\"?cmd=del&ID={0}\" style=\"color:red;\">锁定</a>", userId);
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
                //if (userType == "1")
                //{
                //    cmd = string.Format(" <a onclick=\"return confirm('你确定要将该用户设为代理吗？')\" href=\"?cmd=pok&ID={0}\" style=\"color:red;\">设为代理</a>", userId);
                //}
                //else if (userType == "2")
                //{
                //    cmd = string.Format("<a onclick=\"return confirm('你确定要取消该用户的代理权限吗？')\" href=\"?cmd=pdel&ID={0}\" style=\"color:red;\">取消代理权限</a>", userId);
                //}
                if (!String.IsNullOrEmpty(manageId))
                {
                    viviapi.Model.Manage _mangeInfo = viviapi.BLL.ManageFactory.GetModel(int.Parse(manageId));
                    if (_mangeInfo != null)
                    {
                        cmd = _mangeInfo.relname;
                    }
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
        private UsersUpdateLog newUpdateLog(string f, string n, string o, string desc)
        {
            UsersUpdateLog item = new UsersUpdateLog();
            item.userid = UserID;
            item.Addtime = DateTime.Now;
            item.field = f;
            item.newvalue = n;
            item.oldValue = o;
            item.Editor = viviapi.BLL.ManageFactory.CurrentManage.username;
            item.OIp = viviLib.Web.ServerVariables.TrueIP;
            item.Desc = desc;
            return item;
        }

        protected string Getpassview(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return string.Empty;
            if (Convert.ToInt32(obj) > 0)
                return "√";
            else
                return "×";
        }
        protected void btn_Msg_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["chkItem"];
            if (string.IsNullOrEmpty(ids))
            {
                this.AlertAndRedirect("选择商户");
            }
            else
            {
                Response.Redirect("../news/SendMsg.aspx?uid=" + ids);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string ids = Request.Form["chkItem"];
                foreach (string id in ids.Split(','))
                {
                    viviapi.BLL.User.Factory.Del(int.Parse(id));
                }
            }
            catch { }

            LoadData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserAdd.aspx", true);
        }

        public string IsSpecialChannel(object userid)
        {
            int special = 0;

            special = viviapi.BLL.Channel.ChannelTypeUsers.Exists(Convert.ToInt32(userid));

            if (special == 1)
                return "(独)";
            else
                return string.Empty;
        }

        protected void btn_ClearCache_Click(object sender, EventArgs e)
        {
            try
            {
                string ids = Request.Form["chkItem"];
                foreach (string id in ids.Split(','))
                {
                    string cacheKey = string.Format(viviapi.BLL.User.Factory.USER_CACHE_KEY, id);

                    viviapi.WebComponents.WebUtility.ClearCache(cacheKey);

                    cacheKey = string.Format(viviapi.BLL.Channel.ChannelTypeUsers.ChannelTypeUsers_CACHEKEY, id);

                    viviapi.WebComponents.WebUtility.ClearCache(cacheKey);
                }
            }
            catch { }
        }
    }
}