using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using viviapi.Model;
using viviapi.BLL;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.WebComponents.Web
{
    public class PageBase : System.Web.UI.Page
    {
        public string getTitle(string subPageTitle)
        {
            //if (string.IsNullOrEmpty(subPageTitle))
            //{
            //    subPageTitle = SiteName;
            //}
            return string.Format("{1} {0}-{2}", SiteName, subPageTitle,BLL.Sys.SiteSettings.WebSiteTitleSuffix);
        }

        public string WebSiteTitleSuffix
        {
            get
            {
                return BLL.Sys.SiteSettings.WebSiteTitleSuffix;
            }
        }

        public string KeyWords
        {
            get
            {
                //return "\"" + BLL.SysConfig.WebSiteKey + "\"";
                return BLL.Sys.SiteSettings.KeyWords;

            }
        }


        public string Description
        {
            get
            {
                //return "\"" + BLL.SysConfig.WebSitedescription + "\""; 
                return BLL.Sys.SiteSettings.Description; 

            }
        }

        public string firstPage
        {
            get
            {
                string _page = viviapi.SysConfig.RuntimeSetting.firstpage;
                if (string.IsNullOrEmpty(_page))
                    _page = "Login.aspx";
                return _page;

            }
        }
        public string statJs
        {
            get
            {
                if (webInfo != null)
                    return System.Web.HttpUtility.HtmlDecode(webInfo.Code);
                return string.Empty;
            }
        }
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
        public void AlertAndRedirect(string msg, string url)
        {
            AlertAndRedirect(this, msg, url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void AlertAndRedirect(string msg)
        {
            AlertAndRedirect(this, msg, null);
        }

        public void AlertAndRedirect(Page P, string msg)
        {
            AlertAndRedirect(P, msg, null);
        }

        /// <summary>
        /// 信息提醒并转发到其他页面。
        /// </summary>
        /// <param name="P"></param>
        /// <param name="msg">提示信息。</param>
        /// <param name="url">需要转发的URL。</param>
        public void AlertAndRedirect(Page P, string msg, string url)
        {
            string script;
            if (string.IsNullOrEmpty(msg) && string.IsNullOrEmpty(url))
            {
                script = @"
<SCRIPT LANGUAGE='javascript'><!--
location.href=location.href;
//--></SCRIPT>
";
            }
            else if (string.IsNullOrEmpty(msg))
            {
                script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
location.href=""{0}"";
//--></SCRIPT>
", url);
            }
            else if (string.IsNullOrEmpty(url))
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

            //P.ClientScript.RegisterClientScriptBlock(P.GetType(), "AlertAndRedirect", script);
            P.ClientScript.RegisterStartupScript(P.GetType(), "AlertAndRedirect", script);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="P"></param>
        /// <param name="msg"></param>
        public void ShowMessageBox(string msg)
        {
            string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
alert({0});
//--></SCRIPT>
", viviLib.Security.AntiXss.JavaScriptEncode(msg));

            this.ClientScript.RegisterStartupScript(this.GetType(), "ShowMessageBox", script);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="P"></param>
        /// <param name="msg"></param>
        public void ShowMessageBoxAndBack(string msg)
        {
            string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
alert({0});
history.go(-1);
//--></SCRIPT>
", viviLib.Security.AntiXss.JavaScriptEncode(msg));

            this.ClientScript.RegisterStartupScript(this.GetType(), "ShowMessageBox", script);
        }


        public void AlertAndRedirect2(string msg,string url)
        {
            string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
alert({0});
top.location.href=""{1}"";
//--></SCRIPT>
", AntiXss.JavaScriptEncode(msg), url);

            this.ClientScript.RegisterStartupScript(this.GetType(), "AlertAndRedirect2", script);
        }

        /// <summary>
        /// 本月第一天
        /// </summary>
        /// <returns></returns>
        public DateTime FirstDayOfMonth
        {
            get
            {
                return Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-01 00:00:00"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime ToDayFirstTime
        {
            get
            {
                return Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            }
        }
        public string CutWord(object _str)
        {
            if (_str == null)
                return string.Empty;
            return CutWord(System.Web.HttpUtility.HtmlEncode(_str.ToString()), 30);
        }

        public string CutWord(string _str)
        {
            return CutWord(_str, 30);
        }

        public string CutWord(string _str, int len)
        {
            if (!string.IsNullOrEmpty(_str))
            {
                if (_str.Length > len)
                    return _str.Substring(0, len) + "...";
                return _str;
            }
            return string.Empty;
        }

        public string GetPostValue(string key)
        {
            return WebBase.GetFormString(key, "");
        }
    }
}
