using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using viviapi.WebComponents.Web;
using viviLib.ExceptionHandling;

namespace viviAPI.WebUI7uka.usermodule.WS
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetUserInfo : UserHandlerBase
    {
        public String Mark(string num)
        {
            if (string.IsNullOrEmpty(num))
                return string.Empty;

            return "*" + num.Substring(1);
        }

        public override void OnLoad(HttpContext context)
        {
            string msg = "";

            try
            {
                var result = new UserInfoJson {result = 0, username = string.Empty, name = string.Empty, msg = "未知错误"};

                string username = GetValue("username");

                if (!string.IsNullOrEmpty(username))
                {
                    var userinfo = viviapi.BLL.User.Factory.GetModelByName(username);

                    if (userinfo != null)
                    {
                        result.result = userinfo.ID;
                        result.username = username;
                        result.name = Mark(userinfo.full_name);
                    }
                    else
                    {
                        result.msg = "此账户不存在";
                    }
                }
                msg = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                msg = "error";
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
        }
    }
}
