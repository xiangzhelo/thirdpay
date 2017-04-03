using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using viviLib.ExceptionHandling;
using viviLib.TimeControl;

namespace viviLib.Web
{
    /// <summary>
    /// WebBase 的摘要说明。
    /// </summary>
    public sealed class WebBase
    {
        private static System.Web.HttpApplication _httpApplication;

        /// <summary>
        /// 构造函数。
        /// </summary>
        private WebBase()
        {
        }

        /// <summary>
        /// 返回由指定的参数替换后的QueryString。
        /// </summary>
        /// <param name="querystring">原来的QueryString集合。</param>
        /// <param name="list">需要替换参数的QueryString集合。</param>
        /// <returns>完成后的QueryString集合。</returns>
        public static NameValueCollection BuildQueryString(NameValueCollection querystring, NameValueCollection list)
        {
            if (((querystring == null) || (querystring.Count == 0)) && ((list == null) || (list.Count == 0)))
            {
                return new NameValueCollection(0);
            }
            if ((querystring == null) || (querystring.Count == 0))
            {
                return new NameValueCollection(list);
            }
            if ((list == null) || (list.Count == 0))
            {
                return new NameValueCollection(querystring);
            }
            NameValueCollection values = new NameValueCollection(querystring);
            for (int i = 0; i < list.AllKeys.Length; i++)
            {
                values[list.AllKeys[i]] = list[list.AllKeys[i]];
            }
            return values;
        }

        /// <summary>
        /// 返回由指定的参数替换后的QueryString。
        /// </summary>
        /// <param name="querystring">原来的QueryString集合。</param>
        /// <param name="list">需要替换参数的QueryString集合。</param>
        /// <returns>完成后的QueryString。</returns>
        public static string BuildQueryStringString(NameValueCollection querystring, NameValueCollection list)
        {
            NameValueCollection values = BuildQueryString(querystring, list);
            if (values.Count == 0)
            {
                return string.Empty;
            }
            string str = string.Empty;
            string[] allKeys = values.AllKeys;
            if (allKeys != null)
            {
                for (int i = 0; i < allKeys.Length; i++)
                {
                    string[] strArray2 = values.GetValues(allKeys[i]);
                    for (int j = 0; j < strArray2.Length; j++)
                    {
                        if (str.Length == 0)
                        {
                            str = str + string.Format("?{0}={1}", allKeys[i], HttpUtility.UrlEncode(strArray2[j]));
                        }
                        else
                        {
                            str = str + string.Format("&{0}={1}", allKeys[i], HttpUtility.UrlEncode(strArray2[j]));
                        }
                    }
                }
            }
            return str;
        }

        /// <summary>
        /// 返回分页链接。
        /// </summary>
        /// <param name="pagerParam"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetPageUrl(string pagerParam, int page)
        {
            NameValueCollection list = new NameValueCollection(1);
            list.Add(pagerParam, string.Format("{0:d}", page));
            return (Request.Path + BuildQueryStringString(Request.QueryString, list));
        }

        /// <summary>
        /// 从QueryString中返回参数。
        /// </summary>
        /// <param name="param">参数。</param>
        /// <param name="defaultValue">默认值。</param>
        /// <returns></returns>
        public static bool GetQueryStringBoolean(string param, bool defaultValue)
        {
            if (Request.QueryString[param] != null)
            {
                bool flag = !defaultValue;
                if ((string.Compare(Request.QueryString[param], flag.ToString(), true) == 0) || (string.Compare(Request.QueryString[param], defaultValue ? "0" : "1", true) == 0))
                {
                    return !defaultValue;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 从QueryString中返回参数。
        /// </summary>
        /// <param name="param">参数。</param>
        /// <param name="defaultValue">默认值。</param>
        /// <returns></returns>
        public static DateTime GetQueryStringDateTime(string param, DateTime defaultValue)
        {
            if ((Request.QueryString[param] != null) && (Request.QueryString[param].Length != 0))
            {
                return FormatConvertor.StringToDateTime(Request.QueryString[param]);
            }
            return defaultValue;
        }

        /// <summary>
        /// 从QueryString中返回参数。
        /// </summary>
        /// <param name="param">参数。</param>
        /// <param name="defaultValue">默认值。</param>
        /// <returns></returns>
        public static decimal GetQueryStringDecimal(string param, decimal defaultValue)
        {
            if ((Request.QueryString[param] != null) && (Request.QueryString[param].Length != 0))
            {
                try
                {
                    return Convert.ToDecimal(Request.QueryString[param]);
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 从QueryString中返回参数。
        /// </summary>
        /// <param name="param">参数。</param>
        /// <param name="defaultValue">默认值。</param>
        /// <returns></returns>
        public static double GetQueryStringDouble(string param, double defaultValue)
        {
            if ((Request.QueryString[param] != null) && (Request.QueryString[param].Length != 0))
            {
                try
                {
                    return Convert.ToDouble(Request.QueryString[param]);
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 从QueryString中返回参数。
        /// </summary>
        /// <param name="param">参数。</param>
        /// <param name="defaultValue">默认值。</param>
        /// <returns></returns>
        public static int GetQueryStringInt32(string param, int defaultValue)
        {
            if ((Request.QueryString[param] != null) && (Request.QueryString[param].Length != 0))
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString[param], 10);
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 从QueryString中返回参数。
        /// </summary>
        /// <param name="param">参数。</param>
        /// <param name="defaultValue">默认值。</param>
        /// <returns></returns>
        public static long GetQueryStringInt64(string param, long defaultValue)
        {
            if ((Request.QueryString[param] != null) && (Request.QueryString[param].Length != 0))
            {
                try
                {
                    return Convert.ToInt64(Request.QueryString[param], 10);
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 从QueryString中返回参数。
        /// </summary>
        /// <param name="param">参数。</param>
        /// <param name="defaultValue">默认值。</param>
        /// <returns></returns>
        public static string GetQueryStringString(string param, string defaultValue)
        {
            if (Request.QueryString[param] == null)
            {
                return defaultValue;
            }
            return Request.QueryString[param];
        }

        /// <summary>
        /// 从QueryString中返回参数。
        /// </summary>
        /// <param name="param">参数。</param>
        /// <param name="defaultValue">默认值。</param>
        /// <returns></returns>
        public static string GetFormString(string param, string defaultValue)
        {
            if (Request.Form[param] == null)
            {
                return defaultValue;
            }
            return Request.Form[param];
        }

        /// <summary>
        /// 当前应用程序对象
        /// </summary>
        public static HttpApplicationState Application
        {
            get
            {
                if (Context != null)
                {
                    return Context.Application;
                }
                if (_httpApplication == null)
                {
                    return null;
                }
                return _httpApplication.Application;
            }
        }

        /// <summary>
        /// 当前 HTTP 请求获取 <see cref="T:System.Web.HttpContext" /> 对象。
        /// </summary>
        public static HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }

        /// <summary>
        /// 当前的应用程序。
        /// </summary>
        public static System.Web.HttpApplication HttpApplication
        {
            set
            {
                _httpApplication = value;
            }
        }

        /// <summary>
        /// 当前HTTP请求的Request对象。
        /// </summary>
        public static HttpRequest Request
        {
            get
            {
                if (Context == null)
                {
                    return null;
                }
                return Context.Request;
            }
        }

        /// <summary>
        /// 当前HTTP请求的Response对象。
        /// </summary>
        public static HttpResponse Response
        {
            get
            {
                if (Context == null)
                {
                    return null;
                }
                return Context.Response;
            }
        }

        /// <summary>
        /// 当前HTTP请求的HttpServerUtility对象。
        /// </summary>
        public static HttpServerUtility Server
        {
            get
            {
                if (Context == null)
                {
                    return null;
                }
                return Context.Server;
            }
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
        /// 为发前 HTTP 请示获取 <see cref="T:System.Web.SessionState.HttpSessionState" /> 的实例。
        /// </summary>
        public static HttpSessionState Session
        {
            get
            {
                if (Context == null)
                {
                    return null;
                }
                return Context.Session;
            }
        }
    }
}

