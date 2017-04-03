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
using viviapi.BLL;

namespace viviapi.WebComponents.Web
{
    /// <summary>
    /// PageBase 的摘要说明
    /// </summary>
    public class PageBaseControl : System.Web.UI.UserControl
    {
        private WebInfo _webinfo = null;
        public WebInfo webInfo
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

        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName
        {
            get
            {
                if (this.webInfo == null)
                    return string.Empty;
                return webInfo.Name;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        public void AlertAndRedirect(string msg,string url)
        {
            AlertAndRedirect(this.Page, msg, url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void AlertAndRedirect(string msg)
        {
            AlertAndRedirect(this.Page, msg, null);
        }
        /// <summary>
        /// 信息提醒并转发到其他页面。
        /// </summary>
        /// <param name="msg">提示信息。</param>
        /// <param name="url">需要转发的URL。</param>
        public void AlertAndRedirect(Page P, string msg, string url)
        {
            string script = string.Empty;
            if ((msg == null || msg.Length == 0) && (url == null || url.Length == 0))
            {
                script = @"
<SCRIPT LANGUAGE='javascript'><!--
location.href=location.href;
//--></SCRIPT>
";
            }
            else if (msg == null || msg.Length == 0)
            {
                script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
location.href=""{0}"";
//--></SCRIPT>
", url);
            }
            else if (url == null || url.Length == 0)
            {
                script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
alert({0});
location.href=location.href;
//--></SCRIPT>
",
                    viviLib.Security.AntiXss.JavaScriptEncode(msg));
            }
            else
            {
                script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
alert({0});
location.href=""{1}"";
//--></SCRIPT>
",
                    viviLib.Security.AntiXss.JavaScriptEncode(msg),
                    url);
            }

            P.ClientScript.RegisterClientScriptBlock(P.GetType(), "AlertAndRedirect", script);
        }


    }
}
