using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.WebUI7uka.usermodule.behalf
{
    public partial class setting2 : viviapi.WebComponents.Web.UserPageBase
    {
        protected viviapi.BLL.User.UserSetting setbll = new viviapi.BLL.User.UserSetting();

        private viviapi.Model.User.UserSettingInfo _setting = null;
        public viviapi.Model.User.UserSettingInfo setting
        {
            get
            {
                if (_setting == null)
                {
                    _setting = setbll.GetModel(CurrentUser.ID);
                }
                if (_setting == null)
                {
                    _setting = new viviapi.Model.User.UserSettingInfo();
                }

                return _setting;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        void InitForm()
        {
            if (setting != null)
            {
                this.rbl_set.SelectedValue = setting.isRequireAgentDistAudit.ToString();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (setting != null)
            {
                setting.isRequireAgentDistAudit = byte.Parse(this.rbl_set.SelectedValue);
                setbll.Insert(setting);

                AlertAndRedirect("操作成功");
            }

        }
    }
}
