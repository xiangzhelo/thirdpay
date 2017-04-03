using System;

namespace viviLib.Web
{

    /// <summary>
    /// ServerVariables ��ժҪ˵����
    /// </summary>
    public sealed class ServerVariables
    {
        /// <summary>
        /// ���캯����
        /// </summary>
        private ServerVariables()
        {
        }

        /// <summary>
        /// ��ǰ HTTP �������ʵIP��ַ��
        /// </summary>
        public static string TrueIP
        {
            get
            {
                if (WebBase.Request != null)
                {
                    if (WebBase.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    {
                        if (WebBase.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                        {
                            return WebBase.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        }
                        return string.Empty;
                    }
                    if (WebBase.Request.ServerVariables["REMOTE_ADDR"] != null)
                    {
                        return WebBase.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                return string.Empty;
            }
        }

        public static string GetIPAddress()
        {
            string userIP;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    userIP =
                        System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    userIP = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
            }
            else
            {
                userIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return userIP;
        }
    }
}

