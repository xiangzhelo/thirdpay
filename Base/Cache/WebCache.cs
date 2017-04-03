using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Xml;
using System.IO;
using viviapi.SysConfig;
using XmlElement = System.Xml.XmlElement;

namespace viviapi.Cache
{
    /// <summary>
    /// 缓存类，对缓存进行全局控制管理
    /// </summary>
    public class WebCache
    {
        /// <summary>
        /// 缓存策略接口类
        /// </summary>
        private static volatile ICacheStrategy cs;
        /// <summary>
        /// 用于线程锁对象 
        /// </summary>
        private static object lockHelper = new object();
        /// <summary>
        /// 是否使用memcached
        /// </summary>
        private static bool applyMemCached = false;
        /// <summary>
        /// 缓存策略接口实例（在WebCache静态方法中初始化该实例信息）
        /// </summary>
        private static ICacheStrategy memcachedStrategy;

        /// <summary>
        /// 构造函数
        /// </summary>
        static WebCache()
        {
            InitialCacheStrategy();
        }

        /// <summary>
        /// 实始化缓存测试
        /// </summary>
        static void InitialCacheStrategy()
        {
            if (MemCachedConfig.ApplyMemCached)
                applyMemCached = true;

            if (applyMemCached)
            {
                try
                {
                    cs = memcachedStrategy = new MemCachedStrategy();
                }
                catch
                {
                    throw new Exception("请检查Discuz.EntLib.dll文件是否被放置在bin目录下并配置正确");
                }
            }
            else
                cs = new DefaultCacheStrategy();
        }

        /// <summary>
        /// 单体模式返回当前类的实例
        /// </summary>
        /// <returns></returns>
        public static ICacheStrategy GetCacheService()
        {
            if (cs == null)
            {
                lock (lockHelper)
                {
                    if (cs == null)
                        InitialCacheStrategy();
                }
            }
            return cs;
        }      
      
    
        /// <summary>
        /// 加载指定的缓存策略，实现策略的动态更新
        /// </summary>
        /// <param name="ics">要更换的缓存策略</param>
        public static void LoadCacheStrategy(ICacheStrategy ics)
        {
            lock (lockHelper)
            {
                //当不使用MemCached时
                if (!applyMemCached)
                    cs = ics;
            }
        }

        /// <summary>
        /// 加载默认的缓存策略，实现策略的默认还原
        /// </summary>
        public static void LoadDefaultCacheStrategy()
        {
            lock (lockHelper)
            {
                //当不使用MemCached时
                if (applyMemCached)
                    cs = memcachedStrategy;
                else
                    cs = new DefaultCacheStrategy();
            }
        }
    }    
}
