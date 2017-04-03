using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Web.Caching;

namespace viviapi.SysConfig.Menu
{
	/// <summary>
	/// ConfigurationHelper ��ժҪ˵����
	/// </summary>
	public sealed class ConfigHelper
	{
		private ConfigHelper()
		{
		}		

		#region SystemInstances
        protected static volatile System.Web.Caching.Cache webCache = System.Web.HttpRuntime.Cache;
        /// <summary>
        /// ��ȡHTTP�������
        /// </summary>
        public static System.Web.Caching.Cache cacheSystemInstances
        {
            get { return webCache; }
        }

		#region SystemInstances
		/// <summary>
		/// ϵͳʵ����
		/// </summary>
		public static List<SystemInstance>  SystemInstances
		{
			get
			{
				if (cacheSystemInstances["SystemInstances"] == null)
				{
					LoadSystemInstances();
				}
				return cacheSystemInstances["SystemInstances"] as List<SystemInstance> ;
			}
		}
		#endregion

		#region GetSystemInstance
		/// <summary>
		/// ���ݱ�ŷ���ָ����ϵͳʵ����
		/// </summary>
		/// <param name="systemId">ϵͳ��š�</param>
		/// <returns>ϵͳ��š�</returns>
		public static SystemInstance GetSystemInstance(int systemId)
		{
			List<SystemInstance>  list	= SystemInstances;
			for (int i = 0  ; i < list.Count ; i ++)
			{
				SystemInstance item	= getSystemInstance(list[i] , systemId);
				if (item != null)
				{
					return item;
				}
			}
			return null;
		}

		private static SystemInstance getSystemInstance(SystemInstance systemInstance , int systemId)
		{
			if (systemInstance.SystemId == systemId)
			{
				return systemInstance;
			}
			if (systemInstance.Items.Count > 0)
			{
				for (int i = 0 ; i < systemInstance.Items.Count ; i ++)
				{
					SystemInstance item	= getSystemInstance(systemInstance.Items[i] , systemId);
					if (item != null)
					{
						return item;
					}
				}
			}
			return null;
		}
		#endregion

		#region LoadSystemInstances
		/// <summary>
		/// ����ϵͳ������Ϣ��
		/// </summary>
		public static void LoadSystemInstances()
		{
			string path	= SystemPath.SystemConfigPath;
			
			if (System.IO.File.Exists(path))
			{
				try
				{
					List<SystemInstance>  list	= (List<SystemInstance> )Kenfor.KStar.Utility.Serialize.XmlSerializer.GetDeserializeObjectFromFile(path , typeof(List<SystemInstance> ));

					cacheSystemInstances.Add("SystemInstances" , list , CacheItemPriority.Normal , null , new Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.FileDependency(path));
				}
				catch (Exception ex)
				{
					Kenfor.KStar.Utility.ExceptionHandling.ExceptionHandler.HandleException(ex);
				}
			}
		}
		#endregion

		#region WriteSystemInstances
		/// <summary>
		/// д��ϵͳ���á�
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static bool WriteSystemInstances(List<SystemInstance>  list)
		{
			if (list != null)
			{
				try
				{
					string path	= SystemPath.SystemConfigPath;

					Kenfor.KStar.Utility.Serialize.XmlSerializer.SerializeToFile(list	, path);

					return true;
				}
				catch (Exception ex)
				{
					Kenfor.KStar.Utility.ExceptionHandling.ExceptionHandler.HandleException(ex);
				}
				return false;
			}
			return false;
		}
		#endregion

		#region GetMenuSetting
		/// <summary>
		/// ����ϵͳʵ�����ز˵��б�
		/// </summary>
		/// <param name="systemInstance">ϵͳʵ����</param>
		/// <returns>�˵��б�</returns>
		public static MenuItemCollection GetMenuSetting(SystemInstance systemInstance)
		{
			string key		= "Menu_";
			if (systemInstance.SystemType == SystemType.Custom)
			{
				key			+= systemInstance.SystemId.ToString("d"	, System.Globalization.NumberFormatInfo.InvariantInfo);
			}
			else
			{
				key			+= systemInstance.SystemType.ToString();
			}

			if (cacheSystemInstances[key] == null)
			{
				string path			= SystemPath.GetMenuConfigPath(systemInstance);

				if (System.IO.File.Exists(path))
				{
					MenuItemCollection list		= Kenfor.KStar.Utility.Serialize.XmlSerializer.GetDeserializeObjectFromFile(path , typeof(MenuItemCollection)) as MenuItemCollection;

					cacheSystemInstances.Add(key , list , CacheItemPriority.Normal , null , new Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.FileDependency(path));
				}
			}
			return cacheSystemInstances[key] as MenuItemCollection;
		}
		#endregion

		#region GetPowerSetting
		/// <summary>
		/// ����ϵͳʵ�����ز˵��б�
		/// </summary>
		/// <param name="systemInstance">ϵͳʵ����</param>
		/// <returns>�˵��б�</returns>
		public static PowerItemCollection GetPowerSetting(SystemInstance systemInstance)
		{
			string key		= "Power_";
			if (systemInstance.SystemType == SystemType.Custom)
			{
				key			+= systemInstance.SystemId.ToString("d"	, System.Globalization.NumberFormatInfo.InvariantInfo);
			}
			else
			{
				key			+= systemInstance.SystemType.ToString();
			}

			if (cacheSystemInstances[key] == null)
			{
				string path			= SystemPath.GetPowerConfigPath(systemInstance);

				if (System.IO.File.Exists(path))
				{
					PowerItemCollection list	= Kenfor.KStar.Utility.Serialize.XmlSerializer.GetDeserializeObjectFromFile(path , typeof(PowerItemCollection)) as PowerItemCollection;

					cacheSystemInstances.Add(key , list , CacheItemPriority.Normal , null , new Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.FileDependency(path));
				}
			}
			return cacheSystemInstances[key] as PowerItemCollection;
		}
		#endregion
		#endregion
	}
}

