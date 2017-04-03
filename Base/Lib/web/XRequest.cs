using System;
using System.Web;

namespace viviLib
{
    /// <summary>
    /// 
    /// </summary>
    public class XRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listlength"></param>
        /// <param name="listname"></param>
        /// <returns></returns>
        public static string GetCheckListValue(int listlength, string listname)
        {
            string str = "";
            for (int i = 1; i < (listlength + 1); i++)
            {
                if (GetString(listname + i.ToString()) != "")
                {
                    str = str + GetString(listname + i.ToString()) + ",";
                }
            }
            if (str.Contains(","))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
            {
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            return request.Url.Host;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static float GetFloat(string strName, float defValue)
        {
            if (GetQueryFloat(strName, defValue) == defValue)
            {
                return GetFormFloat(strName, defValue);
            }
            return GetQueryFloat(strName, defValue);
        }

        public static float GetFormFloat(string strName, float defValue)
        {
            return Utility.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
        }

        public static int GetFormInt(string strName, int defValue)
        {
            return Utility.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
        }

        public static string GetFormString(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.Form[strName];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        public static int GetInt(string strName, int defValue)
        {
            if (GetQueryInt(strName, defValue) == defValue)
            {
                return GetFormInt(strName, defValue);
            }
            return GetQueryInt(strName, defValue);
        }

        public static string GetIP()
        {
            string ip = string.Empty;
            ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            switch (ip)
            {
                case null:
                case "":
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    break;
            }
            if ((ip == null) || (ip == string.Empty))
            {
                ip = HttpContext.Current.Request.UserHostAddress;
            }
            if (!(((ip != null) && (ip != string.Empty)) && Utility.IsIP(ip)))
            {
                return "0.0.0.0";
            }
            return ip;
        }

        public static string GetPageName()
        {
            string[] strArray = HttpContext.Current.Request.Url.AbsolutePath.Split(new char[] { '/' });
            return strArray[strArray.Length - 1].ToLower();
        }

        public static int GetParamCount()
        {
            return (HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count);
        }

        public static float GetQueryFloat(string strName, float defValue)
        {
            return Utility.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
        }

        public static int GetQueryInt(string strName, int defValue)
        {
            return Utility.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
        }

        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.QueryString[strName];
        }

        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        public static string GetServerString(string strName)
        {
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }

        public static string GetString(string strName)
        {
            if ("".Equals(GetQueryString(strName)))
            {
                return GetFormString(strName);
            }
            return GetQueryString(strName);
        }

        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        public static string GetUrlReferrer()
        {
            string str = null;
            try
            {
                str = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch
            {
            }
            if (str == null)
            {
                return "";
            }
            return str;
        }

        public static bool IsBrowserGet()
        {
            string[] strArray = new string[] { "ie", "opera", "netscape", "mozilla" };
            string str = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < strArray.Length; i++)
            {
                if (str.IndexOf(strArray[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsSearchEnginesGet()
        {
            string[] strArray = new string[] { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom" };
            string str = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            for (int i = 0; i < strArray.Length; i++)
            {
                if (str.IndexOf(strArray[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static void SaveRequestFile(string path)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpContext.Current.Request.Files[0].SaveAs(path);
            }
        }
    }
}

