using System;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Security;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.manage
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ManageEdit : ManagePageBase
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

        private viviapi.Model.Manage _itemInfo = null;
        public viviapi.Model.Manage ItemInfo
        {
            get
            {
                if (_itemInfo == null)
                {
                    if (IsUpdate)
                    {
                        _itemInfo = ManageFactory.GetModel(ItemInfoId);
                    }
                    else
                    {
                        _itemInfo = new viviapi.Model.Manage();
                    }
                }
                return _itemInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();

            if (!this.IsPostBack)
            {
                this.InitForm();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitForm()
        {
            tr_1.Visible = !IsUpdate;
            tr_2.Visible = !IsUpdate;

            foreach (ManageRole item in Enum.GetValues(typeof(ManageRole)))
            {
                if (item != ManageRole.None)
                {
                    ListItem listItem = new ListItem(ManageFactory.GetManageRoleView(item), ((int)item).ToString());
                    if (((ItemInfo.role) & item) == item)
                    {
                        listItem.Selected = true;
                    }
                    this.cbl_roles.Items.Add(listItem);
                }
            }
            if (IsUpdate)
            {
                hf_isupdate.Value = "1";
                this.txtusername.Text = ItemInfo.username;
                this.txtrelname.Text = ItemInfo.relname;
                this.ddlCommissionType.SelectedValue = ItemInfo.commissiontype.ToString();
                this.ddlStus.SelectedValue = ItemInfo.status.ToString();
                this.lbllastloginip.Text = ItemInfo.lastLoginIp;

                if(ItemInfo.lastLoginTime.HasValue)
                this.lbllastlogintime.Text = ItemInfo.lastLoginTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                
                if (this.ItemInfo.commission.HasValue)
                    this.txtCommission.Text = ItemInfo.commission.Value.ToString("f4");

                if (this.ItemInfo.cardcommission.HasValue)
                    this.txtCardCommission.Text = ItemInfo.cardcommission.Value.ToString("f4");

                if (ItemInfo.balance.HasValue)
                    this.lblbalance.Text = ItemInfo.balance.Value.ToString("f2");

                ckb_SuperAdmin.Checked = ItemInfo.isSuperAdmin > 0;
                ckb_Agent.Checked = ItemInfo.isAgent > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string username = this.txtusername.Text.Trim();
            string realName = this.txtrelname.Text.Trim();

            string password = this.txtpassword.Text.Trim();
            string secPwd = this.txtpassword2.Text.Trim();

            ManageRole role = ManageRole.None;
            foreach (ListItem listItem in cbl_roles.Items)
            {
                if (listItem.Selected)
                {
                    if (role == ManageRole.None)
                    {
                        role = (ManageRole)Convert.ToInt32(listItem.Value);
                    }
                    else
                    {
                        role = role | (ManageRole)Convert.ToInt32(listItem.Value);
                    }
                }
            }
            ItemInfo.username = username;

            if (IsUpdate == false)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    ItemInfo.password = Cryptography.MD5(password);
                }
                if (!string.IsNullOrEmpty(secPwd))
                {
                    ItemInfo.secondpwd = Cryptography.MD5(secPwd);
                }
            }

            ItemInfo.relname = realName;
            ItemInfo.role = role;
            ItemInfo.commissiontype = int.Parse(this.ddlCommissionType.SelectedValue);
            decimal commission = 0M;
            if (!decimal.TryParse(this.txtCommission.Text.Trim(), out commission))
            {
                ShowMessageBox("请输入数字");
                return;
            }
            decimal _cardcommission = 0M;
            if (!decimal.TryParse(this.txtCardCommission.Text.Trim(), out _cardcommission))
            {
                ShowMessageBox("请输入数字");
                return;
            }
            ItemInfo.cardcommission = _cardcommission;
            ItemInfo.commission = commission;
            ItemInfo.status = int.Parse(this.ddlStus.SelectedValue);
            ItemInfo.isSuperAdmin = this.ckb_SuperAdmin.Checked ? 1 : 0;
            ItemInfo.isAgent = this.ckb_Agent.Checked ? 1 : 0;



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

        #region setPower
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
        #endregion

    }
}
