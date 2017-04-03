using System;
using System.Web;
using System.Collections;
using System.Web.Caching;

namespace viviapi.Cache
{
   	/// <summary>
	/// 默认缓存管理类
	/// </summary>
	public class  DefaultCacheStrategy : ICacheStrategy
	{
        /// <summary>
        /// 缓存策略
        /// </summary>
		private static readonly DefaultCacheStrategy instance = new DefaultCacheStrategy();

        /// <summary>
        /// 获取HTTP缓存对象
        /// </summary>
        protected static volatile System.Web.Caching.Cache webCache = System.Web.HttpRuntime.Cache;

        /// <summary>
        /// 默认缓存存活期为3600秒(1小时)
        /// </summary>
        protected int _timeOut = 3600;
        /// <summary>
        /// 线程同步锁
        /// </summary>
		private static object syncObj = new object();

		/// <summary>
		/// 构造函数
		/// </summary>
		static DefaultCacheStrategy()
		{   
            //lock (syncObj)
            //{
            //    //System.Web.HttpContext context = System.Web.HttpContext.Current;
            //    //if(context != null)
            //    //    webCache = context.Cache;
            //    //else
            //    webCache = System.Web.HttpRuntime.Cache;
            //}	
		}
		
		
		/// <summary>
        /// 设置到期相对时间[单位: 秒] 
		/// </summary>
        public virtual int TimeOut
		{
            set { _timeOut = value > 0 ? value : 3600; }
            get { return _timeOut > 0 ? _timeOut : 3600; }
		}

        /// <summary>
        /// 获取HTTP缓存对象
        /// </summary>
        public static System.Web.Caching.Cache GetWebCacheObj
		{
			get { return webCache; }
		}
		
		/// <summary>
		/// 加入当前对象到缓存中
		/// </summary>
		/// <param name="objId">对象的键值</param>
		/// <param name="o">缓存的对象</param>
        public virtual void AddObject(string objId, object o)
        {
            if (objId == null || objId.Length == 0 || o == null)
            {
                return;
            }

            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);

            if (TimeOut == 7200 || TimeOut == 0)
            {
                webCache.Insert(objId, o, null, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, callBack);
            }
            else
            {
                webCache.Insert(objId, o, null, DateTime.Now.AddSeconds(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
            }
        }

        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算,但在本类中该值无效</param>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        public virtual void AddObject(int hashCode, string objId, object o)
        {
            AddObject(objId, o);
        }

        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="expires">过期时间,单位:秒</param>
        public virtual void AddObject(string objId, object o, int expires)
        {
            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);
            webCache.Insert(objId, o, null, DateTime.Now.AddSeconds(expires), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
        }

        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="saved">是否持久化保存</param>
        public virtual void AddObject(string objId, object o, bool saved)
        {
            AddObject(objId, o);
        }

        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="expires">过期时间,单位:秒</param>
        /// <param name="saved">是否持久化保存</param>
        public virtual void AddObject(string objId, object o, int expires, bool saved)
        {
            AddObject(objId, o, expires);
        }

        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算,但在本类中该值无效</param>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="saved">是否持久化保存</param>
        public virtual void AddObject(int hashCode, string objId, object o, bool saved)
        {
            AddObject(objId, o);
        }

        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="saved">是否被持久化，如为true，则在缓存中找不到后，从数据库中检索相应信息并返回</param>
        public virtual void RemoveObject(string objId, bool saved)
        {
            RemoveObject(objId);           
        }

        /// <summary>
        /// 建立回调委托的一个实例
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="val">缓存的对象</param>
        /// <param name="reason">缓存失效原因</param>
        public void onRemove(string key, object val, CacheItemRemovedReason reason)
        {
            switch (reason)
            {
                case CacheItemRemovedReason.DependencyChanged:
                    break;
                case CacheItemRemovedReason.Expired:
                    {
                        //CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(this.onRemove);

                        //webCache.Insert(key, val, null, System.DateTime.Now.AddMinutes(TimeOut),
                        //    System.Web.Caching.Cache.NoSlidingExpiration,
                        //    System.Web.Caching.CacheItemPriority.High,
                        //    callBack);
                        break;
                    }
                case CacheItemRemovedReason.Removed:
                    {
                        break;
                    }
                case CacheItemRemovedReason.Underused:
                    {
                        break;
                    }
                default: break;
            }
            //viviLib.Logging.LogHelper.Write("onRemove: " + key + " reason:" + reason.ToString() + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //如需要使用缓存日志,则需要使用下面代码
            //myLogVisitor.WriteLog(this,key,val,reason);			
        }

		/// <summary>
		/// 删除缓存对象
		/// </summary>
		/// <param name="objId">对象的关键字</param>
        public virtual void RemoveObject(string objId)
		{
            if (objId == null || objId.Length == 0)
            {
                return;
            }
			webCache.Remove(objId);

            //SyncCache.SyncRemoteCache(objId);
		}


        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算,但在本类中该值无效</param>
        /// <param name="objId">对象的键值</param>
        public virtual void RemoveObject(int hashCode, string objId)
        {
            RemoveObject(objId);
        }

		/// <summary>
		/// 返回一个指定的对象
		/// </summary>
		/// <param name="objId">对象的关键字</param>
		/// <returns>获取缓存的对象</returns>
        public virtual object RetrieveObject(string objId)
		{
            if (objId == null || objId.Length == 0)
            {
                return null;
            }			
			return webCache.Get(objId);
		}

        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算,但在本类中该值无效</param>
        /// <param name="objId">对象的键值</param>
        /// <returns>获取缓存的对象</returns>
        public virtual object RetrieveObject(int hashCode, string objId)
		{
            if (objId == null || objId.Length == 0)
            {
                return null;
            }			
			return webCache.Get(objId);
		}
        

        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="objId">对象的关键字</param>
        /// <param name="type">返回的结果类型</param>
        /// <param name="saved">是否被持久化，如为true，则在缓存中找不到后，从数据库中检索相应信息并返回</param>
        /// <returns>获取缓存的对象</returns>
        public virtual object RetrieveObject(string objId, Type type, bool saved)
        {
            return RetrieveObject(objId);
        }

        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算, 但在本类中该值无效</param>
        /// <param name="objId">对象的键值</param>
        /// <param name="type">返回的结果类型</param>
        /// <param name="saved">是否被持久化，如为true，则在缓存中找不到后，从数据库中检索相应信息并返回</param>
        /// <returns>获取缓存的对象</returns>
        public virtual object RetrieveObject(int hashCode, string objId, Type type, bool saved)
        {
            return RetrieveObject(objId);
        }

    }
}
