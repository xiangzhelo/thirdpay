using System;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.Channel;
using viviapi.Model;
using viviapi.Model.Channel;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserChannel : ManagePageBase
    {

        public int UserID
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public int typeId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public string cmd
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", string.Empty);
            }
        }

        public int ajaxUserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("userId", 0);
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
                puser.Value = UserID.ToString();
                lblInfo.Text = "当前用户ID：" + UserID.ToString();
                this.LoadData();
            }
        }

        void DoCmd()
        {
            if (typeId > 0 && !string.IsNullOrEmpty(cmd) && this.ajaxUserId > 0)
            {
                var obj = new ChannelTypeUserInfo
                {
                    userId = ajaxUserId,
                    typeId = typeId,
                    sysIsOpen = cmd != "close",
                    addTime = DateTime.Now,
                    userIsOpen = null
                };

                string result = "error";
                if (viviapi.BLL.Channel.ChannelTypeUsers.Add(obj) > 0)
                {
                    result = "success";
                }
                Response.Write(result);
                Response.End();
            }
        }
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
            DataTable data = viviapi.BLL.Channel.ChannelType.GetList(true).Tables[0];

            //通道类别状态
            if (!data.Columns.Contains("type_status"))
                data.Columns.Add("type_status", typeof(string));

            //系统设置
            if (!data.Columns.Contains("sys_setting"))
                data.Columns.Add("sys_setting", typeof(string));

            //用户前台设置
            if (!data.Columns.Contains("user_setting"))
                data.Columns.Add("user_setting", typeof(string));

            if (!data.Columns.Contains("payrate"))
                data.Columns.Add("payrate", typeof(double));

            if (!data.Columns.Contains("suppid"))
                data.Columns.Add("suppid", typeof(int));



            foreach (DataRow dr in data.Rows)
            {
                int typeId = int.Parse(dr["typeId"].ToString());

                bool type_stutas = false;
                bool? sys_setting = null;
                bool? user_setting = null;

                ChannelTypeUserInfo setting = ChannelTypeUsers.GetModel(UserID, typeId);
                ChannelTypeInfo typeInfo = ChannelType.GetModelByTypeId(typeId);
                switch (typeInfo.isOpen)
                {
                    case OpenEnum.Close:
                    case OpenEnum.AllClose:
                        type_stutas = false;
                        break;
                    case OpenEnum.Open:
                    case OpenEnum.AllOpen:
                        type_stutas = true;
                        break;
                }

                dr["type_status"] = type_stutas ? "right" : "wrong";
                dr["sys_setting"] = "Unknown";
                dr["user_setting"] = "Unknown";

                dr["suppid"] = 0;
                if (setting != null)
                {
                    if (setting.sysIsOpen.HasValue)
                        dr["sys_setting"] = setting.sysIsOpen.Value ? "right" : "wrong";
                    if (setting.userIsOpen.HasValue)
                        dr["user_setting"] = setting.userIsOpen.Value ? "right" : "wrong";
                    if (setting.suppid.HasValue)
                        dr["suppid"] = setting.suppid.Value;
                }
                dr["payrate"] = 100 * viviapi.BLL.Finance.PayRate.Instance.GetUserPayRate(this.UserID, Convert.ToInt32(dr["typeId"]));

            }
            rpt_paymode.DataSource = data;
            rpt_paymode.DataBind();
        }
        #endregion

        protected void btnAllOpen_Click(object sender, EventArgs e)
        {
            viviapi.BLL.Channel.ChannelTypeUsers.Setting(this.UserID, 1);
            LoadData();
        }
        protected void btnAllColse_Click(object sender, EventArgs e)
        {
            viviapi.BLL.Channel.ChannelTypeUsers.Setting(this.UserID, 0);
            LoadData();
        }

        protected void btnReSet_Click(object sender, EventArgs e)
        {
            viviapi.BLL.Channel.ChannelTypeUsers.Setting(this.UserID, 3);
            LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_paymode_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = e.Item.DataItem as DataRowView;
                int typeId = int.Parse(dr["typeId"].ToString());

                ChannelTypeUserInfo setting = ChannelTypeUsers.GetModel(UserID, typeId);
                if (setting != null)
                {
                    if (setting.sysIsOpen.HasValue)
                    {
                        Button btnOpen = e.Item.FindControl("btn_open") as Button;
                        Button btnClose = e.Item.FindControl("btn_close") as Button;

                        btnOpen.Enabled = !setting.sysIsOpen.Value;
                        btnClose.Enabled = setting.sysIsOpen.Value;
                    }
                }

                DropDownList ddlsupp = e.Item.FindControl("ddlsupp") as DropDownList;
                if (ddlsupp != null)
                {
                    ////ddlsupp.Visible = (typeId == 102);
                    //if (typeId == 102)
                    //{

                    //}

                    int suppid = int.Parse(dr["suppid"].ToString());
                    bind(ddlsupp, suppid);
                }
            }
        }

        void bind(DropDownList ddlctrl, int suppId)
        {
            string where = "";// "isbank=1";
            DataTable list = viviapi.BLL.Supplier.Factory.GetList(where).Tables[0];
            ddlctrl.Items.Add(new ListItem("--默认--", "0"));
            foreach (DataRow dr in list.Rows)
            {
                ddlctrl.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
            }
            ddlctrl.SelectedValue = suppId.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rpt_paymode_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var setting = new ChannelTypeUserInfo
            {
                updateTime = DateTime.Now,
                typeId = int.Parse(e.CommandArgument.ToString())
            };

            setting.updateTime = DateTime.Now;
            setting.userId = UserID;
            setting.userIsOpen = null;

            if (e.CommandName == "open")
            {
                setting.sysIsOpen = true;
            }
            else if (e.CommandName == "close")
            {
                setting.sysIsOpen = false;
            }
            viviapi.BLL.Channel.ChannelTypeUsers.Add(setting);
            LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rpt_paymode.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    HiddenField hfTypeId = item.FindControl("hftypeId") as HiddenField;
                    if (hfTypeId != null)
                    {
                        int typeId = Convert.ToInt32(hfTypeId.Value);
                        DropDownList ddlsupp = item.FindControl("ddlsupp") as DropDownList;
                        if (ddlsupp != null)
                        {
                            var setting = new ChannelTypeUserInfo {updateTime = DateTime.Now, typeId = typeId};

                            setting.updateTime = DateTime.Now;
                            setting.userId = UserID;
                            setting.suppid = int.Parse(ddlsupp.SelectedValue);

                            ChannelTypeUsers.AddSupp(setting);
                        }

                        //if (typeId == 102)
                        //{

                        //}
                    }
                }

                string cacheKey = string.Format(ChannelTypeUsers.ChannelTypeUsers_CACHEKEY, UserID);

                viviapi.WebComponents.WebUtility.ClearCache(cacheKey);
            }

            LoadData();
        }
    }
}