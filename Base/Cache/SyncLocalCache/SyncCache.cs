using System;
using System.Text;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Threading;
using System.Net;

namespace viviapi.Cache
{
    /// <summary>
    /// 同步缓存类
    /// </summary>
    public class SyncCache
    {
        /// <summary>
        /// 除本站之外的负载均衡站点列表
        /// </summary>
        static List<string> syncCacheUrlList = null;
    
        static SyncCache()
        {
            syncCacheUrlList = new List<string>();
            syncCacheUrlList.AddRange(viviapi.SysConfig.MemCachedConfig.SyncCacheUrl.
                Replace("tools/", "tools/SyncLocalCache.ashx").Split(','));
         
            int port = HttpContext.Current.Request.Url.Port;
            string localUrl = string.Format("{0}://{1}{2}{3}",
                                             HttpContext.Current.Request.Url.Scheme,
                                             HttpContext.Current.Request.Url.Host,
                                             (port == 80 || port == 0) ? "" : ":" + port,
                                             BaseConfigs.GetPath);

            Predicate<string> matchUrl = new Predicate<string>
            (
                delegate(string webUrl)
                {
                    return webUrl.IndexOf(localUrl) >= 0; //移除本地站点链接，因为当前站点缓存已被移除。
                }
            );

            syncCacheUrlList.RemoveAll(matchUrl);
        }

       
        /// <summary>
        /// 同步远程缓存信息
        /// </summary>
        /// <param name="cacheKey">要同步的缓存键值</param>
        public static void SyncRemoteCache(string cacheKey)
        {
            foreach (string webSite in syncCacheUrlList)
            {
                string url = string.Format("{0}?cacheKey={1}&passKey={2}", 
                                           webSite, 
                                           cacheKey,
                                          HttpUtility.UrlEncode(viviLib.Security.Cryptography.MD5(cacheKey+viviapi.SysConfig.MemCachedConfig.AuthCode)));
                ThreadSyncRemoteCache src = new ThreadSyncRemoteCache(url);
                new Thread(new ThreadStart(src.Send)).Start();
            }
        }

        /// <summary>
        /// 多线程更新远程缓存
        /// </summary>
        public class ThreadSyncRemoteCache
        {
            public string _url;

            public ThreadSyncRemoteCache(string url)
            {
                _url = url;
            }

            public void Send()
            {
                try
                {
                    //设置循环三次，如果某一次更新成功("OK")，则跳出循环
                    for (int count = 0; count < 3; count++)
                    {
                        if (this.SendWebRequest(_url) == "OK")
                            break;
                        else
                            Thread.Sleep(5000);//如果更新不成功，则暂停5秒后再次更新
                    }
                }
                catch { }
                finally
                {
                    if (Thread.CurrentThread.IsAlive)
                        Thread.CurrentThread.Abort();                  
                }
           }

            /// <summary>
            /// 发送web请求
            /// </summary>
            /// <param name="url"></param>
            /// <returns></returns>
            public string SendWebRequest(string url)
            {
                #region 注释代码
                //StringBuilder builder = new StringBuilder();
                //try
                //{
                //    WebClient client = new WebClient();
                //    client.Encoding = Encoding.UTF8;
                //    //request.Timeout = 15000;
                //    //request.ContentType = "Text/XML";
                //    client.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322)");
                //    using (StreamReader reader = new StreamReader(client.OpenRead(url), Encoding.UTF8))
                //    {
                //        builder.Append(reader.ReadToEnd().Trim());
                //        reader.Close();
                //    }
                //}
                //catch
                //{
                //    builder.Append("Process Failed!");
                //} 
                //return builder.ToString(); 
                #endregion

                StringBuilder builder = new StringBuilder();
                try
                {
                    WebRequest request = WebRequest.Create(new Uri(url));
                    request.Method = "GET";
                    request.Timeout = 15000;
                    request.ContentType = "Text/XML";
                    using (WebResponse response = request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            builder.Append(reader.ReadToEnd());
                        }
                    }
                }
                catch(Exception ex)
                {
                    builder.Append("Process Failed!");
                }
                return builder.ToString();
            }
        }
    }
}
