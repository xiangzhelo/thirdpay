using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviLib.Configuration;
using System.IO;

namespace viviapi.SysConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class MemCachedConfig
    {
        private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configurations\memcached.config");
        private static readonly string _group = "MemCachedConfig";
		/// <summary>
		/// 设置组。
		/// </summary>
		public static string SettingGroup
		{
			get{return _group;}
		}

        /// <summary>
        /// 返回配置值。
        /// </summary>
        /// <param name="group">组。</param>
        /// <param name="key">键值。</param>
        /// <returns>配置值。</returns>
        public static string GetConfig(string key)
        {
            return ConfigHelper.GetConfig(_filePath, _group, key);

        }
        /// <summary>
        /// 是否应用MemCached
        /// </summary>
        public static bool ApplyMemCached
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(GetConfig("ApplyMemCached"));
                }
                catch
                {
                    return false;
                }
            }
        }
            
        

        /// <summary>
        /// 链接地址
        /// </summary>
        public static string ServerList
        {
            get { return GetConfig("ServerList"); }
        }

        /// <summary>
        /// 权重，表示当前memcached服务器被访问的权重（数据越大，则访问使用的机率越高）
        /// </summary>
        public static string Weights
        {
            get
            {
                return GetConfig("Weights");
            }
        }
        /// <summary>
        /// 链接池名称
        /// </summary>
        public static string PoolName
        {
            get
            {
                return GetConfig("PoolName");
            }
        }

        /// <summary>
        /// 初始化链接数
        /// </summary>
        public static int IntConnections
        {
            get
            {
                int _intConnections = Convert.ToInt32(GetConfig("IntConnections"));
                return _intConnections > 0 ? _intConnections : 3;
            }
        }

        /// <summary>
        /// 最少链接数
        /// </summary>
        public static int MinConnections
        {
            get
            {
                int _minConnections = Convert.ToInt32(GetConfig("MinConnections"));
                return _minConnections > 0 ? _minConnections : 3;
            }
        }

        /// <summary>
        /// 最大连接数
        /// </summary>
        public static int MaxConnections
        {
            get
            {
                int _maxConnections = Convert.ToInt32(GetConfig("MaxConnections"));
                return _maxConnections > 0 ? _maxConnections : 5;
            }
        }

        /// <summary>
        /// Socket链接超时时间
        /// </summary>
        public static int SocketConnectTimeout
        {
            get
            {
                int _socketConnectTimeout = Convert.ToInt32(GetConfig("SocketConnectTimeout"));
                return _socketConnectTimeout > 1000 ? _socketConnectTimeout : 1000;
            }
        }

        /// <summary>
        /// socket超时时间
        /// </summary>
        public static int SocketTimeout
        {
            get
            {
                int _socketTimeout = Convert.ToInt32(GetConfig("SocketTimeout"));
                return _socketTimeout > 1000 ? MaintenanceSleep : 3000;
            }
        }

        /// <summary>
        /// 维护线程休息时间
        /// </summary>
        public static int MaintenanceSleep
        {
            get
            {
                int _maintenanceSleep = Convert.ToInt32(GetConfig("MaintenanceSleep"));
                return _maintenanceSleep > 0 ? _maintenanceSleep : 30;
            }
        }

        /// <summary>
        /// 链接失败后是否重启,详情参见http://baike.baidu.com/view/1084309.htm
        /// </summary>
        public static bool FailOver
        {
            get
            {
                bool _failOver = Convert.ToBoolean(GetConfig("FailOver"));
                return _failOver;
            }
        }

        /// <summary>
        /// 是否用nagle算法启动socket
        /// </summary>
        public static bool Nagle
        {
            get
            {
                bool _nagle = Convert.ToBoolean(GetConfig("Nagle"));
                return _nagle;
            }
        }

        /// <summary>
        /// 是否用nagle算法启动socket
        /// </summary>
        public string HashingAlgorithm
        {
            get
            {
                string _hashingAlgorithm = GetConfig("HashingAlgorithm");
                return _hashingAlgorithm;
            }
        }


        /// <summary>
        /// 本地缓存到期时间，该设置会与memcached搭配使用
        /// </summary>
        public static int LocalCacheTime
        {
            get
            {
                int _localCacheTime = Convert.ToInt32(GetConfig("LocalCacheTime"));
                return _localCacheTime;
            }
        }

        /// <summary>
        /// Memcached缓存到期时间，0为不受时间限制
        /// </summary>
        public static int MemCacheTime
        {
            get
            {
                int _memCacheTime = Convert.ToInt32(GetConfig("MemCacheTime"));
                return _memCacheTime;
            }
        }

        /// <summary>
        /// 是否用nagle算法启动socket
        /// </summary>
        public static bool RecordeLog
        {
            get
            {
                bool _recordeLog = Convert.ToBoolean(GetConfig("RecordeLog"));
                return _recordeLog;
            }
        }

        /// <summary>
        /// 负载均衡下同步缓存的url信息(注:站点之间用","分割)
        /// </summary>
        public static string SyncCacheUrl
        {
            get
            {
                string _syncCacheUrl = GetConfig("SyncCacheUrl");
                return _syncCacheUrl;
            }
        }

        /// <summary>
        /// 负载均衡下同步缓存的认证码信息
        /// </summary>
        public static string AuthCode
        {
            get
            {
                string _authCode = GetConfig("AuthCode");
                return _authCode;
            }
        }

        /// <summary>
        /// 是否应用BASE64编码byte[]二进制数据
        /// </summary>
        public static bool ApplyBase64
        {
            get
            {
                bool _applyBase64 = Convert.ToBoolean(GetConfig("ApplyBase64"));
                return _applyBase64;
            }
        }
    }
}
