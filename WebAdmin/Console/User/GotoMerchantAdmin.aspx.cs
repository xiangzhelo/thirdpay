using System;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.User
{
    public partial class GOTOMerchantAdmin : ManagePageBase
    {
        public int Userid
        {
            get
            {
                return WebBase.GetQueryStringInt32("userid", 0);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            object sessionId = viviapi.BLL.ManageFactory.GetSessionId();

            if (Userid > 0 && sessionId != null)
            {
                string plain = string.Format("{0}|{1}|{2}{3}", Userid, currentManage.id,
                    sessionId, viviapi.BLL.Sys.Constant.ManageGOTOUserAdminKey);

                string sign = viviLib.Security.Cryptography.MD5(plain);

                string gotoUrl = WebUtility.GetSiteDomain()
                    + string.Format("/Merchant/Entrance.aspx?u={0}&m={1}&key={2}&sign={3}", Userid, currentManage.id, sessionId, sign);
                
                Response.Redirect(gotoUrl);
            }
        }
    }
}
