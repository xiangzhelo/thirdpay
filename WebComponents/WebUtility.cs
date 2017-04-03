using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using viviapi.BLL.Supplier;
using viviapi.Model.User;
using viviapi.SysConfig;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.WebComponents
{
    /// <summary>
    /// WebUtility 的摘要说明
    /// </summary>
    public class WebUtility
    {
        #region 信息提醒并转发到其他页面
        /// <summary>
        /// 信息提醒并转发到当前页面。
        /// </summary>
        /// <param name="msg">提示信息。</param>
        public static void AlertAndRedirect(Page P, string msg)
        {
            AlertAndRedirect(P, msg, null);
        }

        /// <summary>
        /// 信息提醒并转发到其他页面。
        /// </summary>
        /// <param name="msg">提示信息。</param>
        /// <param name="url">需要转发的URL。</param>
        public static void AlertAndRedirect(Page P, string msg, string url)
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
        #endregion

        #region 信息提醒关闭当前页面
        /// <summary>
        /// 信息提醒关闭当前页面。
        /// </summary>
        /// <param name="msg">提示信息。</param>
        public static void AlertAndClose(Page P, string msg)
        {
            string script = string.Empty;

            if (msg == null || msg.Length == 0)
            {
                script = @"
<SCRIPT LANGUAGE='javascript'><!--
window.close();
//--></SCRIPT>
";
            }
            else
            {
                script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
alert({0});
window.close();
//--></SCRIPT>
", viviLib.Security.AntiXss.JavaScriptEncode(msg));
            }
            P.ClientScript.RegisterClientScriptBlock(P.GetType(), "AlertAndClose", script);
        }
        #endregion

        #region ShowErrorMsg
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public static void ShowErrorMsg(string error)
        {
            HttpContext.Current.Response.Write(error);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetIPAddress(string ip)
        {
            try
            {
                IpList list = new IpList();
                list.IP = ip;
                return list.IPLocation().Replace("本机地址", "局域网IP");
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetIPAddressInfo(string ip)
        {
            try
            {
                IpList list = new IpList();
                list.IP = ip;
                return list.IPAddInfo().Replace("CZ88.NET", "");
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }
        #endregion

        #region GetIpAddr
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static String GetIpAddr(String ip)
        {
            String ipAddr = "";
            try
            {
                if (ip.StartsWith("http://"))
                    ip = ip.Replace("http://", "");
                String IP = "http://www.ip138.com/ips1388.asp?ip=";
                IP += ip;
                IP += "&action=2";


                //获取网页源码   
                System.Net.WebClient webClient = new System.Net.WebClient();
                String strSource = "";
                try
                {
                    strSource = webClient.DownloadString(IP);
                    //this.txbAddr.Text = strSource;   
                }
                catch (System.Net.WebException e)
                {
                    return ipAddr = e.ToString();
                }

                //提取地址   
                String regex = @"<li>.+<li>";
                ipAddr = System.Text.RegularExpressions.Regex.Match(strSource, regex).ToString();
                ipAddr = ipAddr.Replace("<li>本站主数据：", "");
                ipAddr = ipAddr.Replace("</li><li>", "");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return ipAddr;
        }
        #endregion

        public static string GetSupplierName(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return string.Empty;

            try
            {
                return Factory.GetSupplierName(obj);
            }
            catch
            {
                return string.Empty;
            }
        }

        #region GetAreaInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <param name="province"></param>
        /// <param name="city"></param>
        public static void GetAreaInfo(out string area, out string province, out string city)
        {
            area = "";
            province = "";
            city = "";

            try
            {
                string clientIP = ServerVariables.GetIPAddress();
                if (!string.IsNullOrEmpty(clientIP))
                {
                    area = GetIpAddr(clientIP);

                    Match m = Regex.Match(area, @"(?<pro>.*?)省(?<city>.*?)市");
                    if (m.Success)
                    {
                        province = m.Groups["pro"].Value;
                        city = m.Groups["city"].Value;
                    }
                    else
                    {
                        m = Regex.Match(area, @"(?<pro>.*?)市(?<city>.*?)区");
                        if (m.Success)
                        {
                            province = m.Groups["pro"].Value;
                            city = m.Groups["city"].Value;
                        }
                        else
                        {
                            m = Regex.Match(area, @"(?<pro>.*?)市");

                            if (m.Success)
                            {
                                province = m.Groups["pro"].Value;
                                city = m.Groups["pro"].Value;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }

        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl"></param>
        public static void BindBankSupplierDDL(DropDownList ddl)
        {
            ddl.Items.Clear();

            string where = "isbank =1";
            DataTable data = Factory.GetList(where).Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (data != null)
            {
                foreach (DataRow dr in data.Rows)
                {
                    ddl.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl"></param>
        public static void BindBquestionDDL(DropDownList ddl)
        {
            ddl.Items.Clear();

            BLL.User.Question bll = new viviapi.BLL.User.Question();

            DataTable data = bll.GetCacheList();
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (data != null)
            {
                foreach (DataRow dr in data.Rows)
                {
                    ddl.Items.Add(new ListItem(dr["question"].ToString(), dr["id"].ToString()));
                }
            }
        }

        public static string GetPayModeViewName(int pmode)
        {
            string _name = string.Empty;
            switch (pmode)
            {
                case 1:
                    _name = "银行帐户";
                    break;
                case 2:
                    _name = "支付宝";
                    break;
                case 3:
                    _name = "财付通";
                    break;
            }
            return _name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl"></param>
        public static void BindCardSupplierDLL(DropDownList ddl)
        {
            ddl.Items.Clear();

            string where = "iscard =1";
            DataTable data = Factory.GetList(where).Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (data != null)
            {
                foreach (DataRow dr in data.Rows)
                {
                    ddl.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl"></param>
        public static void BindSMSSupplierDLL(DropDownList ddl)
        {
            ddl.Items.Clear();

            string where = "issms =1";
            DataTable data = Factory.GetList(where).Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (data != null)
            {
                foreach (DataRow dr in data.Rows)
                {
                    ddl.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl"></param>
        public static void BindSXSupplierDLL(DropDownList ddl)
        {
            ddl.Items.Clear();

            string where = "issx =1";
            DataTable data = Factory.GetList(where).Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            if (data != null)
            {
                foreach (DataRow dr in data.Rows)
                {
                    ddl.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }
            }
        }

        public static string GetCurrentHost()
        {
            string host = "http://" + HttpContext.Current.Request.ServerVariables["http_host"];

            return host;
        }


        public static string GetSiteDomain()
        {
            string domain = RuntimeSetting.Sitedomain;

            if (string.IsNullOrEmpty(domain))
            {
                var webinfo = BLL.WebInfoFactory.CurrentWebInfo;

                if (webinfo != null)
                {
                    domain = webinfo.Domain;
                }
            }

            return domain;
        }


        public static string GetGatewayUrl()
        {
            string gatewayUrl = RuntimeSetting.GatewayUrl;

            if (string.IsNullOrEmpty(gatewayUrl))
            {
                var webinfo = BLL.WebInfoFactory.CurrentWebInfo;

                if (webinfo != null)
                {
                    gatewayUrl = webinfo.PayUrl;
                }
            }

            return gatewayUrl;
        }

        public static string GetMinDate()
        {
            return DateTime.Today.AddMonths(-3).ToString("yyyy-MM-dd 23:59:59");
        }

        public static string GetMaxDate()
        {
            return DateTime.Today.ToString("yyyy-MM-dd 23:59:59");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CheckValiDateCode(string input)
        {
            object validateCode = HttpContext.Current.Session["CCode"];
            if (validateCode == null)
            {
                return "验证码失效";
            }
            if (validateCode.ToString().ToUpper().Equals(input.ToUpper()))
            {
                return "";
            }
            return "验证码不正确，请重新输入！";
        }

        public static double GetDifftime(int userId, object completeTime)
        {
            try
            {
                DateTime comptime;

                var acctInfo = BLL.User.UserAccessTime.GetModel(userId);
                if (acctInfo == null)
                    return 1000.0;

                DateTime? userAcceTime = viviapi.BLL.User.UserAccessTime.GetModel(userId).lastAccesstime;
                comptime = userAcceTime.Value;

                DateTime comptime2 = Convert.ToDateTime(completeTime);

                return comptime2.Subtract(comptime).TotalMinutes;
            }
            catch (Exception exception)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(exception);
                return 0;
            }

        }

        //public static string GenerateAPIKey()
        //{
        //    return viviLib.Text.Strings.GetRnd(128, true, true, true, false, "");
        //}

        public static void ClearCache(string cacheKey)
        {
            try
            {
                var list = new List<string>();

                list.Add(GetSiteDomain());
                list.Add(GetGatewayUrl());

                foreach (var website in list)
                {
                    if (!string.IsNullOrEmpty(website))
                    {
                        cacheKey = BLL.Sys.Constant.CacheMark + cacheKey;

                        string apiKey = MemCachedConfig.AuthCode;
                        string sign = viviLib.Security.Cryptography.MD5(cacheKey + apiKey);

                        string apiUrl = website +
                                        string.Format("/tools/SyncLocalCache.ashx?cacheKey={0}&passKey={1}", cacheKey, sign);

                        string callback = string.Empty;
                        try
                        {
                            callback = viviLib.Web.WebClientHelper.GetString(apiUrl, null, "get", System.Text.Encoding.GetEncoding("gbk"));
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            catch (Exception)
            {

            }

        }
    }
}
