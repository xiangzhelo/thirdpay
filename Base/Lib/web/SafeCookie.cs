using System;
using System.Web;

namespace viviLib.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class SafeCookie
    {
        string keystr = "{F8CD3296-2524-43c9-A83B-F4E47B0A6B7D}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CookieName"></param>
        public static void DelCookie(string CookieName)
        {
            SetCookie(CookieName, "", DateTime.Now.AddYears(-1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CookieName"></param>
        /// <returns></returns>
        public static string GetCookie(string CookieName)
        {
            string str = null;
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                str = viviLib.Security.Cryptography.RijndaelDecrypt(HttpContext.Current.Request.Cookies[CookieName].Value);
            }
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CookieName"></param>
        /// <param name="CookieValue"></param>
        public static void SetCookie(string CookieName, string CookieValue)
        {
            CookieValue = viviLib.Security.Cryptography.RijndaelEncrypt(CookieValue);
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                current.Response.Cookies[CookieName].Value = CookieValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CookieName"></param>
        /// <param name="CookieValue"></param>
        /// <param name="ExpireTime"></param>
        public static void SetCookie(string CookieName, string CookieValue, DateTime ExpireTime)
        {
            HttpContext.Current.Response.Cookies[CookieName].Expires = ExpireTime;
            SetCookie(CookieName, CookieValue);
        }
    }
}

