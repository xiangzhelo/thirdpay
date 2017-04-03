using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model;
using viviapi.Model.supplier;
using viviapi.SysConfig;

namespace viviapi.ETAPI.Weixin
{
    /// <summary>
    /// TenpayUtil 的摘要说明。
    /// 配置文件
    /// </summary>
    public class TenpayUtil
    {
        public static string tenpay = "1";
        
        ///// <summary>
        ///// partner=>
        ///// </summary>
        //public static string partner
        //{
        //    get
        //    {
        //        if (suppInfo != null)
        //        {
        //            return suppInfo.SuppAccount;

        //        }
        //        return string.Empty;
        //    }
        //}

        /// <summary>
        /// partner=>
        /// </summary>
        public static string partner
        {
            get
            {
                if (suppInfo != null)
                {
                    return suppInfo.SuppAccount;

                }
                return string.Empty;
            }
        }
        public static string key
        {
            get
            {
                if (suppInfo != null) return suppInfo.SuppKey;
                return string.Empty;
            }
        }

        
        //public static string appSecret
        //{
        //    get
        //    {
        //        if (suppInfo != null) return suppInfo.pusername;
        //        return string.Empty;
        //    }
        //}

        /// <summary>
        /// username
        /// </summary>
        public static string appid
        {
            get
            {
                if (suppInfo != null) return suppInfo.SuppUserName;
                return string.Empty;
            }
        }


        //public static string appkey
        //{
        //    get
        //    {
        //        if (suppInfo != null) return suppInfo.puserkey1;
        //        return string.Empty;
        //    }
        //}


        public static string tenpay_notify
        {
            get
            {
                ETAPIBase common = new ETAPIBase((int)SupplierCode.Weixin);
                return common.SiteDomain + "/Receive/Weixin/paynotifyurl.aspx";
                    //支付完成后的回调处理页面,*替换成notify_url.asp所在路径
            }

        }

        public static ETAPIBase suppInfo
        {
            get
            {
                ETAPIBase common = new ETAPIBase((int)SupplierCode.Weixin);
                return common;
            }
        }

        public TenpayUtil()
        {
        }
        public static string getNoncestr()
        {
            Random random = new Random();
            return MD5Util.GetMD5(random.Next(1000).ToString(), "GBK");
        }


        public static string getTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }


        /** 对字符串进行URL编码 */
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
                }


                return res;
            }
        }

        /** 对字符串进行URL解码 */
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
                }


                return res;

            }
        }


        /** 取时间戳生成随即数,替换交易单号中的后10位流水号 */
        public static UInt32 UnixStamp()
        {
            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToUInt32(ts.TotalSeconds);
        }
        /** 取随机数 */
        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }

        public static string FormatRequestData(NameValueCollection list, Encoding encoding)
        {
            if ((list == null) || (list.Count == 0))
            {
                return string.Empty;
            }
            string[] strArray = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                strArray[i] = string.Format("{0}={1}", list.Keys[i], HttpUtility.UrlEncode(list[i], encoding));
            }
            return string.Join("&", strArray);
        }

        public static string GetString(string url, string postData, string method, Encoding encoding, int timeout)
        {
            string str = string.Empty;
            using (WebResponse response = GetWebResponse(url, postData, method, encoding, timeout))
            {
                using (Stream stream = response.GetResponseStream())
                {
                    str = viviLib.IO.File.ReadContent(stream, encoding);
                    stream.Close();
                }
                response.Close();
            }
            return str;
        }

        public static WebResponse GetWebResponse(string url, NameValueCollection list, string method, Encoding encoding, int timeout)
        {
            return GetWebResponse(url, FormatRequestData(list, encoding), method, encoding, timeout);
        }

        public static WebResponse GetWebResponse(string url, string postData, string method, Encoding encoding, int timeout)
        {
            if (string.Compare(method, "get", true) == 0)
            {
                if (url.IndexOf("?") == -1)
                {
                    url = url + "?" + postData;
                }
                else
                {
                    url = url + "&" + postData;
                }
                postData = string.Empty;
                method = "GET";
            }
            else
            {
                method = "POST";
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null)
            {
                return null;
            }
            request.Method = method;
            request.ContentType = "text/xml";
            request.Timeout = timeout;
            if (string.Compare(method, "post", true) == 0)
            {
                byte[] bytes = encoding.GetBytes(postData);
                request.ContentLength = bytes.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
            }
            return request.GetResponse();
        }



    }
}