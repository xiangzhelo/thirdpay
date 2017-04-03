using System;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Security;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.manage
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Manage : ManagePageBase
    {
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public string Action
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }
        public bool IsUpdate
        {
            get
            {
                return ItemInfoId > 0 && Action == "edit";
            }
        }

        public bool IsDel
        {
            get
            {
                return ItemInfoId > 0 && Action == "del";
            }
        }

        private viviapi.Model.Manage _itemInfo = null;
        public viviapi.Model.Manage ItemInfo
        {
            get
            {
                if (_itemInfo == null)
                {
                    if (IsUpdate)
                    {
                        _itemInfo = viviapi.BLL.ManageFactory.GetModel(ItemInfoId);
                    }
                    else
                    {
                        _itemInfo = new viviapi.Model.Manage();
                    }
                }
                return _itemInfo;
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
                this.BindView();
            }
        }

        void DoCmd()
        {
            if (IsDel)
            {
                if (viviapi.BLL.ManageFactory.Delete(this.ItemInfoId))
                {
                    AlertAndRedirect("删除成功!", "List.aspx");
                }
                else
                {
                    AlertAndRedirect("删除失败!", "List.aspx");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(true
, ManageRole.None);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindView()
        {
            foreach (int num in Enum.GetValues(typeof(ManageRole)))
            {
                this.LevelList.Items.Add(new ListItem(viviapi.BLL.ManageFactory.GetManageRoleView((ManageRole)num), num.ToString()));
            }
            if (IsUpdate)
            {
                this.UserNameBox.Text = ItemInfo.username;
                this.RelNameBox.Text = ItemInfo.relname;
                this.LevelList.SelectedValue = ((int)ItemInfo.role).ToString();
                this.ddlCommissionType.SelectedValue = ItemInfo.commissiontype.ToString();
                this.ddlStus.SelectedValue = ItemInfo.status.ToString();
                if (this.ItemInfo.commission.HasValue)
                    this.txtCommission.Text = ItemInfo.commission.Value.ToString("f4");
                if (this.ItemInfo.cardcommission.HasValue)
                    this.txtCardCommission.Text = ItemInfo.cardcommission.Value.ToString("f4");
            }


            DataTable manageList = viviapi.BLL.ManageFactory.GetList(string.Empty).Tables[0];
            manageList.Columns.Add("LevelText");
            manageList.Columns.Add("Commissiontypeview");
            manageList.Columns.Add("statusName");
            foreach (DataRow row in manageList.Rows)
            {
                int _s = (int)row["status"];
                if (_s == 1)
                {
                    row["statusName"] = "正常";
                }
                else if (_s == 0)
                {
                    row["statusName"] = "锁定";
                }
                row["LevelText"] = viviapi.BLL.ManageFactory.GetManageRoleView((ManageRole)(int)row["role"]);
                if (row["Commissiontype"] != DBNull.Value)
                    row["Commissiontypeview"] = row["Commissiontype"].ToString() == "2" ? "按支付金额%" : "按条固定提成";
            }
            this.GridView1.DataSource = manageList;
            this.GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            this.BindView();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string username = this.UserNameBox.Text.Trim();
            string password = this.PassWordBox.Text.Trim();
            string secPwd = this.SecPassWordBox.Text.Trim();
            string realName = this.RelNameBox.Text.Trim();

            int role = int.Parse(this.LevelList.SelectedValue);

            ItemInfo.username = username;
            if (!string.IsNullOrEmpty(password))
                ItemInfo.password = Cryptography.MD5(password);
            if (!string.IsNullOrEmpty(secPwd))
                ItemInfo.secondpwd = Cryptography.MD5(secPwd);

            ItemInfo.relname = realName;
            ItemInfo.role = (ManageRole)role;
            ItemInfo.commissiontype = int.Parse(this.ddlCommissionType.SelectedValue);
            decimal commission = 0M;
            if (!decimal.TryParse(this.txtCommission.Text.Trim(), out commission))
            {
                AlertAndRedirect("请输入数字");
                return;
            }
            ItemInfo.commission = commission;

            decimal _cardcommission = 0M;
            if (!decimal.TryParse(this.txtCardCommission.Text.Trim(), out _cardcommission))
            {
                AlertAndRedirect("请输入数字");
                return;
            }
            ItemInfo.cardcommission = _cardcommission;

            ItemInfo.status = int.Parse(this.ddlStus.SelectedValue);

            bool success = false;
            if (this.IsUpdate)
            {
                if (viviapi.BLL.ManageFactory.Update(ItemInfo))
                {
                    success = true;
                }
            }
            else
            {
                if (viviapi.BLL.ManageFactory.Add(ItemInfo) > 0)
                {
                    success = true;
                }
            }
            if (success)
            {
                AlertAndRedirect("操作成功", "List.aspx");
            }
            else
            {
                AlertAndRedirect("操作失败");
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Edit.aspx");
        }
    }
}
