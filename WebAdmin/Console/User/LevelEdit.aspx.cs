using System;
using System.Globalization;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LevelEdit : ManagePageBase
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

        public bool isUpdate
        {
            get
            {
                return ItemInfoId > 0;
            }
        }

        public viviapi.Model.User.UserLevel _model = null;
        public viviapi.Model.User.UserLevel model
        {
            get
            {
                if (_model == null)
                {
                    if (isUpdate)
                    {
                        _model = viviapi.BLL.User.UserLevel.Instance.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        _model = new viviapi.Model.User.UserLevel();
                    }
                }

                return _model;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                ShowInfo();
            }
        }

        void ShowInfo()
        {
            txtLevel.Attributes["readonly"] = "true";
            if (isUpdate && model != null)
            {
                txtLevel.Text = model.level.ToString(CultureInfo.InvariantCulture);
                this.txtlevName.Text = model.levName;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";

            if (this.txtlevName.Text.Trim().Length == 0)
            {
                strErr += "levName不能为空！\\n";
            }

            if (strErr != "")
            {
                ShowMessageBox(strErr);
                return;
            }

            string levName = this.txtlevName.Text;


            model.levName = levName;


            if (viviapi.BLL.User.UserLevel.Instance.Insert(model) > 0)
            {
                AlertAndRedirect("操作成功", "UserLevels.aspx");
            }
            else
            {
                ShowMessageBox("修改失败");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Interfaces);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
    }
}
