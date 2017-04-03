using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.agentmodule.account
{
    public partial class Info : AgentPageBase
    {
        public string getidcard = "";
        public string gettel = "";
        public string getemail = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        void InitForm()
        {
            txtuserid.Attributes["readonly"] = "true";
            txtusername.Attributes["readonly"] = "true";
            txtuserlev.Attributes["readonly"] = "true";
            txtsitename.Attributes["readonly"] = "true";
            txtsiteUrl.Attributes["readonly"] = "true";
            txtLinkMan.Attributes["readonly"] = "true";
            txtStatus.Attributes["readonly"] = "true";
            txtqq.Attributes["readonly"] = "true";

            txtuserid.Value = CurrentUser.ID.ToString();
            txtusername.Value = CurrentUser.UserName;
            txtuserlev.Value = viviapi.BLL.User.UserLevel.Instance.GetLevelName(CurrentUser.UserLevel);

            txtsitename.Value = CurrentUser.SiteName;
            txtsiteUrl.Value = CurrentUser.SiteUrl;

            txtLinkMan.Value = CurrentUser.full_name;
            txtStatus.Value = Enum.GetName(typeof(viviapi.Model.User.UserStatusEnum), CurrentUser.Status);
            txtqq.Value = CurrentUser.QQ;

            if (CurrentUser.Email.Length > 2)
            {
                getemail = UserViewEmail;
            }
            else
            {
                getemail = "<span style='color:red;'>未通过邮箱验证</span>，<a href='/usermodule/safety/modiemail.aspx' targert='mainframe' style='color:#6694ae;'>马上去验证</a>";
            }

            if (CurrentUser.Tel.Length > 4)
            {
                string strtel = CurrentUser.Tel;
                gettel = strtel.Substring(0, 3) + "********";
            }
            else
            {
                gettel = "<span style='color:red;'>未通过手机验证</span>，<a href='/usermodule/safety/modiphone.aspx' targert='mainframe' style='color:#6694ae;'>马上去验证</a>";
            }

            if (CurrentUser.IdCard.Length > 4)
            {
                string stridcard = CurrentUser.IdCard;
                getidcard = "**************" + stridcard.Substring((stridcard.Length - 4), 4) + "(<span style='color:#448b45;'>已通过实名认证</span>)";
            }
            else
            {
                getidcard = "(<span style='color:red;'>未通过实名认证</span>，<a href='/usermodule/safety/safetrna.aspx' targert='mainframe' style='color:#6694ae;'>马上去认证</a>)";
            }
        }
    }
}
