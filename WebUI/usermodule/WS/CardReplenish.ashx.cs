using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebUI7uka.usermodule.WS
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CardReplenish : UserHandlerBase
    {
        public string Orderid
        {
            get
            {
                return WebBase.GetQueryStringString("order", "");
            }
        }

        public override void OnLoad(HttpContext context)
        {
            string msg = "";
            try
            {
                if (!string.IsNullOrEmpty(Orderid))
                {
                    msg = viviapi.SysInterface.Card.APINotification.SynchronousNotify(Orderid);
                }
            }
            catch (Exception exception)
            {
                msg = exception.Message;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
        }
    }
}
