using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.Finance;
using viviapi.BLL.User;
using viviapi.Model;
using viviapi.Model.Promotion;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.Text;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserDomain : ManagePageBase
    {
        readonly viviapi.BLL.User.UserHost hostBll = new viviapi.BLL.User.UserHost();

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
        /// userid
        /// </summary>
        public int UserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("userid", 0);
            }
        }

        private viviapi.Model.User.UserHostInfo _model = null;
        public viviapi.Model.User.UserHostInfo MUserHostInfo
        {
            get
            {
                if (_model == null )
                {
                    if (ItemInfoId > 0)
                    {
                        _model = hostBll.GetModel(ItemInfoId);
                    }
                    else
                    {
                        _model = new UserHostInfo();
                    }
                    
                }
                return _model;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsUpdate
        {
            get
            {
                return ItemInfoId > 0;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        #region InitForm
        void InitForm()
        {
            if (IsUpdate && MUserHostInfo != null)
            {
                this.txtuserid.Text = MUserHostInfo.userid.ToString();
                this.txthostName.Text = MUserHostInfo.hostName;
                this.txthostUrl.Text = MUserHostInfo.hostUrl;
                this.rblstatus.Text = MUserHostInfo.status.ToString();
                this.txtdesc.Text = MUserHostInfo.desc;
            }
            else
            {
                if (UserId > 0)
                {
                    this.txtuserid.Text = UserId.ToString();
                }
            }
        }
        #endregion



        #region Save
        /// <summary>
        /// 
        /// </summary>
        void Save()
        {
            string strErr = "";
            if (!PageValidate.IsNumber(txtuserid.Text))
            {
                strErr += "userid格式错误！\\n";
            }
            if (this.txthostName.Text.Trim().Length == 0)
            {
                strErr += "hostName不能为空！\\n";
            }
            if (this.txthostUrl.Text.Trim().Length == 0)
            {
                strErr += "hostUrl不能为空！\\n";
            }
            if (strErr != "")
            {
                ShowMessageBox(strErr);
                return;
            }
            int userid = int.Parse(this.txtuserid.Text);
            string siteip = this.txtsiteip.Text;
            int sitetype = 1;
            string hostName = this.txthostName.Text;
            string hostUrl = this.txthostUrl.Text;
            int status = int.Parse(this.rblstatus.SelectedValue);
            string desc = this.txtdesc.Text;

            MUserHostInfo.userid = userid;
            MUserHostInfo.siteip = siteip;
            MUserHostInfo.sitetype = sitetype;
            MUserHostInfo.hostName = hostName;
            MUserHostInfo.hostUrl = hostUrl;
            MUserHostInfo.status = (UserHostStatus)status;
            MUserHostInfo.desc = desc;

            bool success = false;

            if (IsUpdate == false)
            {
                success = hostBll.Add(MUserHostInfo) > 0;
            }
            else
            {
                success = hostBll.Update(MUserHostInfo);
            }

            if (success)
            {
                AlertAndRedirect("操作成功", "DomainList.aspx");
            }
            else
            {
                ShowMessageBox("操作失败");
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

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Save();
        }




    }
}
