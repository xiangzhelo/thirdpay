using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviLib.Web;

namespace viviAPI.WebUI7uka.Merchant
{
    public partial class Entrance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userid = WebBase.GetQueryStringInt32("u", 0);
            int manageid = WebBase.GetQueryStringInt32("m", 0);
            string sessionId = WebBase.GetQueryStringString("key", "");
            string sign = WebBase.GetQueryStringString("sign", "");

            string plain = string.Format("{0}|{1}|{2}{3}", userid, manageid,
                   sessionId, viviapi.BLL.Sys.Constant.ManageGOTOUserAdminKey);


            string sign2 = viviLib.Security.Cryptography.MD5(plain);

            if (sign == sign2)
            {
                int manageid2 = viviapi.BLL.ManageFactory.GetIdBySession(sessionId);
                if (manageid == manageid2)
                {
                    HttpContext.Current.Session[viviapi.BLL.Sys.Constant.ManageGOTOUserAdminKey] = userid;

                    Response.Redirect("/usermodule/account/index.aspx");
                }
            }
        }
    }
}
