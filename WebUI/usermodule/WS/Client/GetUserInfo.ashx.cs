using System.Web;

namespace viviAPI.WebUI7uka.usermodule.WS.Client
{
    public class GetUserInfo : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string msg = "fail";

            try
            {
                string token = viviLib.Web.WebBase.GetFormString("token", string.Empty);
                if (!string.IsNullOrEmpty(token))
                {
                    int userid = viviapi.BLL.User.Login.GetUserIdBySession(token);
                    if (userid > 0)
                    {
                        var userinfo = viviapi.BLL.User.Factory.GetModel(userid);
                        if (userinfo != null)
                        {
                            msg = "success" + Newtonsoft.Json.JsonConvert.SerializeObject(userinfo, Newtonsoft.Json.Formatting.Indented);
                        }
                    }
                }
            }
            catch
            { 
                
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
