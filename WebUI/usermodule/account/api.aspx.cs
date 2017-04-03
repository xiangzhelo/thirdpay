using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.usermodule.account
{
    public partial class API : UserPageBase
    {
        public string getuserid = "";
        public string getapikey = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void InitForm()
        {
            //txtuserid.Attributes["readonly"] = "true";
            //txtapikey.Attributes["readonly"] = "true";

            //txtuserid.Value = currentUser.ID.ToString();
            //txtapikey.Value = currentUser.APIKey;

            //txtReturnUrl.Value = currentUser.smsNotifyUrl;

            getuserid = ThisUser.ID.ToString(CultureInfo.InvariantCulture);
            getapikey = ThisUser.APIKey;
        }

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    // currentUser.QQ = txtqq.Value.Trim();
        //    currentUser.smsNotifyUrl = txtReturnUrl.Value.Trim();

        //    if (viviapi.BLL.User.Factory.Update(currentUser, null))
        //    {
        //        AlertAndRedirect("更新成功");
        //    }
        //    else
        //    {
        //        AlertAndRedirect("更新失败");
        //    }
        //}


        private UserInfo _thisUser = null;
        /// <summary>
        /// 
        /// </summary>
        public UserInfo ThisUser
        {
            get { return _thisUser ?? (_thisUser = Factory.GetModel(UserId)); }
        }

        protected void btnModiKey_Click1(object sender, EventArgs e)
        {
            ThisUser.APIKey = viviapi.BLL.User.Factory.GenerateAPIKey();  //
            Factory.Update(ThisUser, null);
            AlertAndRedirect("操作成功", "api.aspx");
        }
    }
}
