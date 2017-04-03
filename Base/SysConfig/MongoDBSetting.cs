using System;
using System.IO;
using viviLib.Configuration;

namespace viviapi.SysConfig
{
	/// <summary>
	/// RuntimeSetting ��ժҪ˵����
	/// </summary>
	public sealed class MongoDBSetting
	{
        private MongoDBSetting()
		{
		}
        private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configurations\memcached.config");
		private static readonly string _group			= "MongoDB";
		/// <summary>
		/// �����顣
		/// </summary>
		public static string SettingGroup
		{
			get{return _group;}
		}

        /// <summary>
        /// ��������ֵ��
        /// </summary>
        /// <param name="group">�顣</param>
        /// <param name="key">��ֵ��</param>
        /// <returns>����ֵ��</returns>
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
		/// ��վ������
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
