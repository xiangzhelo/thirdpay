using System;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.Model.Settled;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public partial class IncreaseAmtEdit : ManagePageBase
    {
        /// <summary>
        /// 
        /// </summary>
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool isUpdate
        {
            get
            {
                return ItemInfoId > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int userId
        {
            get
            {
                return WebBase.GetQueryStringInt32("user", 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IncreaseAmtInfo _ItemInfo = null;
        public IncreaseAmtInfo model
        {
            get
            {
                if (_ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        _ItemInfo = viviapi.BLL.Settled.IncreaseAmt.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        _ItemInfo = new IncreaseAmtInfo();
                    }
                }
                return _ItemInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (userId > 0)
            {
                string msg = string.Empty;
                viviapi.Model.User.UsersAmtInfo amtInfo = viviapi.BLL.User.UsersAmt.GetModel(userId);
                if (amtInfo == null)
                {
                    msg = "0.00";
                }
                else
                {
                    msg = (amtInfo.Balance - amtInfo.Freeze - amtInfo.Unpayment).ToString("f2");
                }
                Response.Write(msg);
                Response.End();
            }
            viviapi.BLL.ManageFactory.CheckSecondPwd();
            setPower();
            if (!this.IsPostBack)
            {
                ShowInfo();
            }
        }

        #region ShowInfo
        /// <summary>
        /// 
        /// </summary>
        void ShowInfo()
        {
            if (isUpdate && model != null)
            {
                this.txtuserId.Text = model.userId.ToString();
                this.txtincreaseAmt.Text = model.increaseAmt.ToString();
                //this.txtaddtime.Text = model.addtime.ToString();
                //this.txtmangeId.Text = model.mangeId.ToString();
                //this.txtmangeName.Text = model.mangeName;
                //this.txtstatus.Text = model.status.ToString();
                this.txtdesc.Text = model.desc;
            }
        }
        #endregion

        #region Save
        void Save()
        {
            if (this.IsValid)
            {
                int userId = 0;
                if (!int.TryParse(this.txtuserId.Text, out userId))
                {
                    AlertAndRedirect("请输入正确的用户ID");
                    return;
                }

                decimal increaseAmt = 0M;
                if(!decimal.TryParse(this.txtincreaseAmt.Text,out increaseAmt))
                {
                    AlertAndRedirect("请输入正确的金额");
                    return;
                }
                // int status = int.Parse(this.txtstatus.Text);
                string desc = this.txtdesc.Text;

                model.userId = userId;
                model.increaseAmt = increaseAmt;
                model.addtime = DateTime.Now;
                model.mangeId = ManageId;
                model.mangeName = currentManage.username;
                model.status = 1;                
                model.optype = (optypeenum)int.Parse(this.rbl_optype.SelectedValue);
                model.desc = desc;

                if (!this.isUpdate)
                {
                    int id = viviapi.BLL.Settled.IncreaseAmt.Add(model);
                    if (id > 0)
                    {
                        AlertAndRedirect("操作成功！", "IncreaseAmts.aspx");
                    }
                    else
                    {
                        AlertAndRedirect("操作失败！");
                    }
                }
            }
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Save();
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

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int userId = 0;
            if (!int.TryParse(this.txtuserId.Text.Trim(), out userId))
            {
                args.IsValid = false;
                return;
            }
            if (!viviapi.BLL.User.Factory.Exists(int.Parse(this.txtuserId.Text)))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }

        }
    }
}
