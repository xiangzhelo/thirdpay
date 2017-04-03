using System;
using System.Text;
using System.Web;
using System.Web.UI;

namespace viviLib.Utils
{
    public class MessageObject
    {
        public static void Location()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\"> \n");
            builder.Append("window.location.href=window.location.href;");
            builder.Append("</script>");
            HttpContext.Current.Response.Write(builder.ToString());
        }

        public static void RedirectPage(string url)
        {
            string str = "http://" + HttpContext.Current.Request.Url.Host + url;
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\"> \n");
            builder.Append(string.Format("window.location.href='{0}';", str));
            builder.Append("</script>");
            HttpContext.Current.Response.Write(builder.ToString());
        }

        public static void Show(string str)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\"> \n");
            builder.Append("alert(\"" + str.Trim() + "\"); history.back(-1);\n");
            builder.Append("</script>");
            HttpContext.Current.Response.Write(builder.ToString());
        }

        public static void Show(Page page,string str)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\"> \n");
            builder.Append("alert(\"" + str.Trim() + "\");\n");
            builder.Append("</script>");
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "show", builder.ToString());
        }

        public static void ShowClose(string str)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\">\n");
            builder.Append("alert(\"" + str.Trim() + "\"); \n");
            builder.Append("window.close();\n");
            builder.Append("</script>\n");
            HttpContext.Current.Response.Write(builder.ToString());
        }

        public static void ShowJS(Page MyPage, string strCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\"> \n");
            builder.Append(strCode.Trim());
            builder.Append("</script>");
            MyPage.Response.Write(builder.ToString());
        }

        public static void ShowLocation(string str)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\"> \n");
            builder.Append("alert(\"" + str.Trim() + "\"); \n");
            builder.Append("window.location.href=window.location.href;\n");
            builder.Append("</script>");
            HttpContext.Current.Response.Write(builder.ToString());
        }

        public static void ShowPre(string str)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\"> \n");
            builder.Append("alert(\"" + str.Trim() + "\"); \n");
            builder.Append("var p=document.referrer; \n");
            builder.Append("window.location.href=p;\n");
            builder.Append("</script>");
            HttpContext.Current.Response.Write(builder.ToString());
        }

        public static void ShowRedirect(string str, string url)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\"> \n");
            builder.Append("alert(\"" + str.Trim() + "\"); \n");
            builder.Append("window.location.href=\"" + url.Trim() + "\";\n");
            builder.Append("</script>");
            HttpContext.Current.Response.Write(builder.ToString());
        }

        public static void Write(string str)
        {
            HttpContext.Current.Response.Write(str);
        }
    }
}

