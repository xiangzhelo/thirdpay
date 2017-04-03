using System;
using System.IO;
using viviLib.Configuration;

namespace viviapi.SysConfig
{
	/// <summary>
	/// RuntimeSetting 的摘要说明。
	/// </summary>
	public sealed class MongoDBSetting
	{
        private MongoDBSetting()
		{
		}
        private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configurations\memcached.config");
		private static readonly string _group			= "MongoDB";
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
        public static string GetConfig(string group, string key)
        {
            return ConfigHelper.GetConfig(_filePath, group, key);

        }
		/// <summary>
		/// 
		/// </summary>
		public static string Connstring
		{
			get{	return ConfigHelper.GetConfig(SettingGroup , "connStr");}
		}

		/// <summary>
		/// 
		/// </summary>
        public static string DefaultDB
		{
			get{	return ConfigHelper.GetConfig(SettingGroup , "defaultdb");}
		}
        	

		/// <summary>
		///
		/// </summary>
        public static string CollectionName
		{
            get { return ConfigHelper.GetConfig(SettingGroup, "collectionName"); }
		}

		/// <summary>
		/// 网站描述。
		/// </summary>
		public static string WebSiteDescription
		{
			get{return ConfigHelper.GetConfig(SettingGroup , "WebSiteDescription");}
		}

        /// <summary>
        /// 
        /// </summary>
        public static string SiteDomain
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "Sitedomain");                
            }

        }
	}
}
