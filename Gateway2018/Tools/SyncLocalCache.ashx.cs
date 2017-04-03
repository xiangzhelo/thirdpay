using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using viviapi.Cache;
using viviLib.Web;

namespace viviAPI.Gateway2018.Tools
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SyncLocalCache : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string callback = "";
            try
            {
                string cacheKey = WebBase.GetQueryStringString("cacheKey", "");
                string passKey = WebBase.GetQueryStringString("passKey", "");

                //键值是否有效
                if (string.IsNullOrEmpty(cacheKey))
                {
                    callback = ("CacheKey is not null!");
                }

                //对传递的参数进行有效性检查
                if (passKey != viviLib.Security.Cryptography.MD5(cacheKey + viviapi.SysConfig.MemCachedConfig.AuthCode))
                {
                    callback = ("AuthCode is not valid!");
                }


                if (string.IsNullOrEmpty(callback))
                {
                    WebCache.LoadCacheStrategy(new DefaultCacheStrategy());
                    WebCache.GetCacheService().RemoveObject(cacheKey);
                    WebCache.LoadDefaultCacheStrategy();
                }
               

                callback = "OK";
            }
            catch (Exception)
            {

                callback = "Error";
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(callback);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
