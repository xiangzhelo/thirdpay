using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using viviapi.BLL;
using viviapi.Model;
using viviapi.Model.User;

namespace viviapi.WebComponents.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class UserHandlerBase : IHttpHandler, IRequiresSessionState
    {
        private UserInfo _currentUser = null;
        /// <summary>
        /// 当前登录的商户
        /// </summary>
        public UserInfo CurrentUser
        {
            get
            {
                if (_currentUser == null)
                    _currentUser = BLL.User.Login.CurrentMember;
                return _currentUser;
            }
        }
        public bool IsLogin
        {
            get
            {
                return CurrentUser != null;
            }
        }

        public int UserId
        {
            get
            {
                return CurrentUser == null ? 0 : CurrentUser.ID;
            }
        }

        private WebInfo _webinfo = null;
        public WebInfo WebSiteInfo
        {
            get
            {
                if (this._webinfo == null)
                {
                    this._webinfo = WebInfoFactory.CurrentWebInfo;
                }
                return _webinfo;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session == null)
            {
                context.Response.StatusCode = 405;
                context.Response.End();
            }
            else if (IsLogin == false)
            {
                context.Response.StatusCode = 405;
                context.Response.End();
            }
            OnLoad(context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public virtual void OnLoad(HttpContext context)
        {
        }

        public string GetValue(string param)
        {
            string val = string.Empty;

            try
            {
                if (HttpContext.Current.Request.Form[param] != null)
                {
                    val = HttpContext.Current.Request.Form[param];
                }
                else if (HttpContext.Current.Request.QueryString[param] != null)
                {
                    val = HttpContext.Current.Request.QueryString[param];
                }
                else
                {
                    val = "";
                }
            }
            catch (Exception exception)
            {
            }
            return val;
        }

    }
}
