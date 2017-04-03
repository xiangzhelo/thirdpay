using System.Web;
using System.Web.SessionState;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviLib.Security;
using viviLib.Web;

namespace viviAPI.WebUI7uka.usermodule.WS.Client
{
    public class SignIn : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string msg = string.Empty;

            try
            {
                string userName = WebBase.GetFormString("txtusername", string.Empty);
                string userPwd  = WebBase.GetFormString("txtpassword", string.Empty);
                string ip       =   WebBase.GetFormString("ip", ServerVariables.TrueIP);

                if (string.IsNullOrEmpty(userName))
                {
                    msg = ("请输入商户名!");
                }
                else if (string.IsNullOrEmpty(userPwd))
                {
                    msg = ("请输入商户密码!");
                }
                else
                {
                    msg = viviapi.BLL.User.Login.SignIn(1, 0, userName, Cryptography.MD5(userPwd), ip, ip);

                    if (msg == "success")
                    {
                        msg = "success," + HttpContext.Current.Session[viviapi.BLL.User.Login.UserLoginSessionid].ToString();
                    }
                }
            }
            catch
            {
                msg = "login fail";
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
