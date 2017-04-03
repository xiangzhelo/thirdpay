using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using viviapi.Model;

namespace viviapi.WebComponents.Web
{
    /// <summary>
    /// MangePageBase 的摘要说明
    /// </summary>
    public class ManagePageBase : PageBase
    {
        public DateTime sDate = Convert.ToDateTime("2014-01-01");
        public DateTime eDate = Convert.ToDateTime("2888-01-10");

        public bool isSuperAdmin
        {
            get
            {
                return currentManage.isSuperAdmin > 0;
            }
        }

        private Model.Manage _currentManage = null;
        /// <summary>
        /// 当前登录的商户
        /// </summary>
        public Model.Manage currentManage
        {
            get
            {
                if (_currentManage == null)
                {
                    _currentManage 
                        = BLL.ManageFactory.CurrentManage;
                }
                return _currentManage;
            }
        }

        public int ManageId
        {
            get
            {
                return currentManage.id;
            }
        }

        /// <summary>
        /// 商户ID
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return currentManage != null;
            }
        }
        public string[] AllowHosts = new string[] { "long-bao.com","localhost","127.0.0.1" };
        public bool CheckHost
        {
            get {
                string currHost = HttpContext.Current.Request.Url.Host;
                foreach (string h in AllowHosts)
                {
                    if (currHost.ToLower().Contains(h))
                    {
                        return true; 
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void checkLogin()
        {
//            if (HttpContext.Current.Request.Url.Host.IndexOf("leuyo.com") < 0)
//            {
//                string script = string.Format(@"
//<SCRIPT LANGUAGE='javascript'><!--
//alert('您还没有被授权使用');top.location.href=""{0}"";
////--></SCRIPT>", "/Console/Login.aspx");

//                HttpContext.Current.Response.Write(script);
//                HttpContext.Current.Response.End();
//            }
            //if (!CheckHost)
            //{
            //    HttpContext.Current.Response.Write("非法请求，请联系管理员！");
            //    HttpContext.Current.Response.End();
            //}
            string loginUrl = string.Format("/{0}/Login.aspx",viviapi.SysConfig.RuntimeSetting.ManagePagePath);
            if (DateTime.Now < sDate || DateTime.Now > eDate)
            {
                string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
alert('试用过期请联系管理员');top.location.href=""{0}"";
//--></SCRIPT>", loginUrl);

                HttpContext.Current.Response.Write(script);
                HttpContext.Current.Response.End();
            }
            if (!this.IsLogin)
            {
                string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
top.location.href=""{0}"";
//--></SCRIPT>", loginUrl);

                HttpContext.Current.Response.Write(script);
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            checkLogin();
        }
    }
}
