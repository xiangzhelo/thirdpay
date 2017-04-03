using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace viviAPI.WebUI7uka.usermodule.Ajax
{
    [Serializable]
    public class GetUserInfoResult
    {
        public int result { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string msg { get; set; }
    }
    public class GetUserInfo_new : IHttpHandler, IReadOnlySessionState
    {

        public String Mark(string num)
        {
            if (string.IsNullOrEmpty(num))
                return string.Empty;

            return "*" + num.Substring(1);
        }

        public void ProcessRequest(HttpContext context)
        {
            string msg = "";

            GetUserInfoResult result = new GetUserInfoResult();
            result.result = 0;
            result.username = string.Empty;
            result.name = string.Empty;
            result.msg = "未知错误";

            try
            {
                if (viviapi.BLL.User.Login.CurrentMember == null)
                {
                    result.msg = "登录信息失效，请重新登录";
                }
                else
                {
                    result.msg = "此账户不存在";

                    string username = viviLib.XRequest.GetString("username");

                    if (!string.IsNullOrEmpty(username))
                    {
                        var userinfo = viviapi.BLL.User.Factory.GetModelByName(username);

                        if (userinfo != null)
                        {
                            result.result = userinfo.ID;
                            result.username = username;
                            result.name = Mark(userinfo.full_name);
                        }
                    }
                }
                msg = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            }
            catch (Exception ex)
            {
                result.msg = ex.Message;
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
