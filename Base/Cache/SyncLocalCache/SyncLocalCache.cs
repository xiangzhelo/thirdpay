using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web;

namespace viviapi.Cache
{
    /// <summary>
    /// 同步本地缓存
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SyncLocalCache : IHttpHandler
    {
        /// <summary>
        /// 同步本地缓存处理方法
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        { 
            context.Response.ContentType = "text/plain";
            string cacheKey = context.Request.QueryString["cacheKey"];
            string passKey = context.Request.QueryString["passKey"];
            
            //键值是否有效
            if (string.IsNullOrEmpty(cacheKey))
            {
                context.Response.Write("CacheKey is not null!");
                return;
            }
          
            //对传递的参数进行有效性检查
            if (passKey != viviLib.Security.Cryptography.MD5(cacheKey + viviapi.SysConfig.MemCachedConfig.AuthCode))
            {
                context.Response.Write("AuthCode is not valid!");
                return;
            }

            //更新本地缓存（注：此处不可使用MemCachedStrategy的RemoveObject方法，因为该方法中有SyncRemoteCache的调用，会造成循环调用）
            WebCache.LoadCacheStrategy(new DefaultCacheStrategy());
            WebCache.GetCacheService().RemoveObject(cacheKey);
            WebCache.LoadDefaultCacheStrategy();    

            context.Response.Write("OK");
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
