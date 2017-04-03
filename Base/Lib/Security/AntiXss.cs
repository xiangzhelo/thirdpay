using System;
using Microsoft.Security.Application;

namespace viviLib.Security
{
   

    /// <summary>
    /// AntiXss 的摘要说明。
    /// </summary>
    public sealed class AntiXss
    {
        private AntiXss()
        {
        }

        /// <summary>
        /// Encodes input strings for use in HTML attributes.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HtmlAttributeEncode(string s)
        {
            return Microsoft.Security.Application.AntiXss.HtmlAttributeEncode(s);
        }

        /// <summary>
        /// Encodes input strings for use in HTML.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HtmlEncode(string s)
        {
            return Microsoft.Security.Application.AntiXss.HtmlEncode(s);
        }

        /// <summary>
        /// Encodes input strings for use in JavaScript.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string JavaScriptEncode(string s)
        {
            if ((s != null) && (s.Length != 0))
            {
                return Microsoft.Security.Application.AntiXss.JavaScriptEncode(s);
            }
            return "''";
        }

        /// <summary>
        /// Encodes input strings for use in Universal Resource Locators (URLs).
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UrlEncode(string s)
        {
            return Microsoft.Security.Application.AntiXss.UrlEncode(s);
        }

        /// <summary>
        /// Encodes input strings for use in Visual Basic Script.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string VisualBasicScriptEncode(string s)
        {
            return Microsoft.Security.Application.AntiXss.VisualBasicScriptEncode(s);
        }

        /// <summary>
        /// Encodes input strings for use in XML attributes.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string XmlAttributeEncode(string s)
        {
            return Microsoft.Security.Application.AntiXss.XmlAttributeEncode(s);
        }

        /// <summary>
        /// Encodes input strings for use in XML.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string XmlEncode(string s)
        {
            return Microsoft.Security.Application.AntiXss.XmlEncode(s);
        }
    }
}

