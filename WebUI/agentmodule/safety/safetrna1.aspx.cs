using System;
using viviapi.BLL.Sys;
using viviapi.BLL.Tools;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.agentmodule.safety
{
    public partial class Safetrna1 : AgentPageBase
    {

        public string SessionKey
        {
            get
            {
                return string.Format(Constant.RealNameAuthenticationSessionKey, UserId);
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
            txtfullname.Attributes["readonly"] = "true";
            txtmale.Attributes["readonly"] = "true";
            txtbirthday.Attributes["readonly"] = "true";
            txtlocation.Attributes["readonly"] = "true";
            txtIdcard.Attributes["readonly"] = "true";

            try
            {
                var idcard = Session[SessionKey] as IdcardInfo;
                if (idcard != null)
                {
                    txtfullname.Value = idcard.fullname;
                    txtIdcard.Value = idcard.code;
                    txtmale.Value = idcard.gender;
                    if (idcard.birthday.Length == 8)
                        txtbirthday.Value = idcard.birthday.Substring(0, 4) + "年" + idcard.birthday.Substring(4, 2) + "月" + idcard.birthday.Substring(6, 2) + "日";
                    else
                        txtbirthday.Value = idcard.birthday;
                    txtlocation.Value = idcard.location;
                }
            }
            catch
            { }
        }

        protected void btnSure_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            var idcard = Session[SessionKey] as IdcardInfo;
            if (idcard != null)
            {
                CurrentUser.full_name = idcard.fullname;
                CurrentUser.IdCard = idcard.code;
                CurrentUser.addtress = idcard.location;
                CurrentUser.male = idcard.gender;
                CurrentUser.IsRealNamePass = 1;
                CurrentUser.PayeeBank = idcard.fullname;

                if (viviapi.BLL.User.Factory.Update(CurrentUser, null))
                {
                    msg = "true";
                }
                else
                {
                    msg = "认证失败";
                }
            }

            if (msg.Equals("true"))
            {
                Response.Redirect("safetrna.aspx", true);
            }
            else
            {
                AlertAndRedirect(msg);
            }
        }
    }
}
