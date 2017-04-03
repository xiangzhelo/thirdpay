using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using viviLib.ExceptionHandling;

namespace viviapi.WebComponents.QqConnetSDK
{
    /// <summary>
    /// QqConnet 基础类
    /// </summary>
    #region QqConnet

    public class QQConnet
    {
        private string QQ_OAUTH_CONSUMER_KEY;
        private string QQ_OAUTH_CONSUMER_SECRET;
        private string QQ_CALLBACK_URL;
        private string QQ_SCOPE;
        public string State = "";

        public QQConnet()
        {
            this.VerifyQzoneSection();
            this.QQ_SCOPE = "get_user_info";//授权项
        }

        #region VerifyQzoneSection
        /// <summary>
        /// 检测QzoneSection节点
        /// </summary>
        /// <param name="table"></param>
        private void VerifyQzoneSection()
        {
            QQ_OAUTH_CONSUMER_KEY = BLL.Sys.OtherSettings.AppKey;
            if (string.IsNullOrEmpty(QQ_OAUTH_CONSUMER_KEY))
            {
                this.ShowErrMsg("AppKey 未配置 ", 0);
            }

            QQ_OAUTH_CONSUMER_SECRET = BLL.Sys.OtherSettings.AppSecret;
            if (string.IsNullOrEmpty(QQ_OAUTH_CONSUMER_SECRET))
            {
                this.ShowErrMsg("AppSecret 未配置 ", 0);
            }

            QQ_CALLBACK_URL = WebUtility.GetSiteDomain() + "/merchant/receiveresult/qqcallback.aspx";
            if (string.IsNullOrEmpty(QQ_CALLBACK_URL))
            {
                this.ShowErrMsg("CALLBACK_URL 未配置 ", 0);
            }
        }
        #endregion

        public string APP_ID
        {
            get { return this.QQ_OAUTH_CONSUMER_KEY; }
        }

        /// <summary>
        /// State 数据.
        /// </summary>
        /// <returns></returns>
        private void GenerationState()
        {
            string value = Guid.NewGuid().ToString();
            this.State = value.Replace("-", "");
        }
        /// <summary>
        /// Get方法请求url,获取请求内容
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public string RequestUrl(string Url)
        {
            try
            {
                //ServerXMLHTTP xmlhttp = new ServerXMLHTTP();
                //xmlhttp.setTimeouts(10000, 10000, 10000, 50000);
                //xmlhttp.open("GET", Url, false, null, null);
                //xmlhttp.send("");
                //if (xmlhttp.readyState == 4)
                //{
                //    return xmlhttp.responseText;
                //}
                //return "";

                return viviLib.Web.WebClientHelper.GetString(Url, null, "GET", System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        ///// <summary>
        ///// Post方法请求url,获取请求内容
        ///// </summary>
        ///// <param name="Url"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public string RequestUrl_post(string Url, string data)
        //{
        //    ServerXMLHTTP xmlhttp = new ServerXMLHTTP();
        //    xmlhttp.setTimeouts(10000, 10000, 10000, 50000);
        //    xmlhttp.open("POST", Url, false, null, null);
        //    xmlhttp.setRequestHeader("Host", " graph.qq.com");
        //    xmlhttp.setRequestHeader("content-length ", data.Length.ToString());
        //    xmlhttp.setRequestHeader("content-type ", "application/x-www-form-urlencoded");
        //    xmlhttp.setRequestHeader("Connection", " Keep-Alive");
        //    xmlhttp.setRequestHeader("Cache-Control", " no-cache");
        //    xmlhttp.send(data);
        //    if (xmlhttp.readyState == 4)
        //    {
        //        return xmlhttp.responseText;
        //    }
        //    return "";
        //}
        /// <summary>
        /// 生成登录地址
        /// </summary>
        /// <returns></returns>
        public string GetAuthorization_Code()
        {
            this.GenerationState();

            string url, str;
            url = "https://graph.qq.com/oauth2.0/authorize";
            str = "client_id=" + this.QQ_OAUTH_CONSUMER_KEY;
            str += "&redirect_uri=" + this.QQ_CALLBACK_URL;
            str += "&response_type=code";
            str += "&scope=" + this.QQ_SCOPE;
            str += "&state=" + this.State;
            url = url + "?" + str;
            return url;
        }

        /// <summary>
        /// 检测是否合法
        /// </summary>
        /// <returns></returns>
        public bool VerifyCallback()
        {
            string Code = this.Request("code");
            string State = this.Request("state");
            if (Code != "")
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取 access_token
        /// 示例.返回结果:正确 access_token=ABCDEFGABCDEFGABCDEFG&expires_in=7776000; 
        /// </summary>
        /// <returns></returns>
        public string GetAccess_Token()
        {
            string Access_Token = string.Empty;
            try
            {
                string url, str, result;
                url = "https://graph.qq.com/oauth2.0/token";
                str = "client_id=" + this.QQ_OAUTH_CONSUMER_KEY;
                str += "&client_secret=" + this.QQ_OAUTH_CONSUMER_SECRET;
                str += "&redirect_uri=" + this.QQ_CALLBACK_URL;
                str += "&grant_type=authorization_code";
                str += "&code=" + this.Request("code");
                str += "&state=" + this.Request("state");
                url = url + "?" + str;
                result = RequestUrl(url);
                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Split('&')[0];
                    Access_Token = result.Replace("access_token=", "");
                }
            }
            catch (Exception ex)
            {
               ExceptionHandler.HandleException(ex);

            }
            return Access_Token;
        }
        /// <summary>
        /// 获取Openid
        /// 示例.返回结果:正确 callback( {"client_id":"100000001","openid":"ABCDEFGABCDEFGABCDEFG"} ); 
        /// </summary>
        /// <returns></returns>
        public string GetOpenid(string Access_Token)
        {
            string Openid = string.Empty;
            try
            {
                string url, str, result;
                url = "https://graph.qq.com/oauth2.0/me";
                str = "access_token=" + Access_Token;
                url = url + "?" + str;
                result = RequestUrl(url);               
                if (!string.IsNullOrEmpty(result))
                {
                    JSON j = new JSON(result);
                    Openid = j.GetValue("openid").ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);

            }
            return Openid;
        }
        ///// <summary>
        ///// 发送记录到微博
        ///// </summary>
        ///// <param name="content">发送内容</param>
        ///// <returns></returns>
        //public string Post_Webo(string Access_Token, string Openid, string content)
        //{
        //    string url, str;
        //    url = "https://graph.qq.com/t/add_t";
        //    str = "oauth_consumer_key=" + this.QQ_OAUTH_CONSUMER_KEY;
        //    str += "&access_token=" + Access_Token;
        //    str += "&openid=" + Openid;
        //    str += "&content=" + content;
        //    return RequestUrl_post(url, str);
        //}

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public QQUserInfo GetUserInfo(string accessToken, string openid)
        {
            QQUserInfo user = null;

            try
            {
                string url, str, result;
                url = "https://graph.qq.com/user/get_user_info";
                str = "oauth_consumer_key=" + this.QQ_OAUTH_CONSUMER_KEY;
                str += "&access_token=" + accessToken;
                str += "&openid=" + openid;
                url += "?" + str;
                result = RequestUrl(url);

                //viviLib.Logging.LogHelper.Debug(result);

                user = (QQUserInfo)Newtonsoft.Json.JsonConvert.DeserializeObject(result, typeof(QQUserInfo));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return user;
        }
        /// <summary>
        /// 显示错误提示信息
        /// </summary>
        /// <param name="error"></param>
        public void ShowErrMsg(string ErrMsg, params object[] ErrParams)
        {
            string error = "";
            if (ErrParams.Length > 0)
            {
                error = ErrMsg;
            }
            else
            {
                int num = ErrMsg.IndexOf("\"error\"");
                if (num != -1)
                {
                    JSON j = new JSON(ErrMsg);
                    string description = j.GetValue("error_description").ToString();
                    switch (description)
                    {
                        case "code is reused error": //错误提示：code代码已被使用，无法重复使用该代码。其他错误不在一一列出。
                            break;
                    }
                    error = ErrMsg;
                }
            }
            if (error != "")
            {
                HttpContext.Current.Response.Write(ErrMsg);
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 获取与设置服务端会话
        /// </summary>
        /// <returns></returns>
        public object Session(string name, params object[] value)
        {
            object result = "";
            if (value.Length > 0)
                HttpContext.Current.Session[name] = value[0];
            else
                result = HttpContext.Current.Session[name];
            return result == null ? "" : result;
        }
        /// <summary>
        /// 获取接收地址栏参数值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string Request(string name)
        {
            string value = HttpContext.Current.Request.QueryString[name];
            return value == null ? "" : value.Trim();
        }
    }
    #endregion

    /// <summary>
    /// ConfigurationHandler 配置节点句柄类
    /// </summary>
    #region ConfigurationHandler
    public class ConfigurationHandler : IConfigurationSectionHandler
    {
        public virtual object Create(Object parent, Object context, XmlNode node)
        {
            Hashtable m_Config = new Hashtable();
            foreach (XmlNode child in node.ChildNodes)
            {
                m_Config.Add(child.Attributes["key"].Value, child.Attributes["value"].Value);
            }
            return m_Config;
        }
    }
    #endregion

}


