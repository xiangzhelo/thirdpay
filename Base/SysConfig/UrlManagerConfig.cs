using System;
using System.Xml;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;


namespace viviapi.SysConfig
{
	/// <summary>
	/// UrlManagerConfig ��ժҪ˵����
	/// </summary>
	[Serializable]
	public class UrlManagerConfig
	{
		private string _host			= string.Empty;
		private string _action			= string.Empty;
		private string _pattern			= string.Empty;
		private string _url				= string.Empty;
		private string _realPath		= string.Empty;
		private string _filePath		= string.Empty;
		private string _queryString		= string.Empty;
		private string _pathInfo		= string.Empty;
		private TimeSpan _timeSpan		= new TimeSpan(0);

		/// <summary>
		/// ���캯����
		/// </summary>
		public UrlManagerConfig()
		{
		}
        protected static volatile System.Web.Caching.Cache webCache = System.Web.HttpRuntime.Cache;

		#region ����
		/// <summary>
		/// ������
		/// </summary>
		public string Host
		{
			get{return this._host;}
			set{this._host	= value;}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string Action
		{
			get{return this._action;}
			set{this._action	= value;}
		}
		
		/// <summary>
		/// ����·������
		/// </summary>
		public string Pattern
		{
			get{return this._pattern;}
			set{this._pattern	= value;}
		}

		/// <summary>
		/// ��ַ��
		/// </summary>
		public string Url
		{
			get{return this._url;}
			set{this._url		= value;}
		}

		/// <summary>
		/// ʵ�ʷ���·����
		/// </summary>
		public string RealPath
		{
			get{return this._realPath;}
			set{this._realPath	= value;}
		}

		/// <summary>
		/// ��̬�ļ����·����
		/// </summary>
		public string FilePath
		{
			get{return this._filePath;}
			set{this._filePath	= value;}
		}

		/// <summary>
		/// QueryString��Ϣ��
		/// </summary>
		public string QueryString
		{
			get{return this._queryString;}
			set{this._queryString	= value;}
		}

		/// <summary>
		/// ·����Ϣ��
		/// </summary>
		public string PathInfo
		{
			get{return this._pathInfo;}
			set{this._pathInfo		= value;}
		}

		/// <summary>
		/// ��̬�ļ�����ʱ�䡣
		/// </summary>
		public TimeSpan TimeSpan
		{
			get{return this._timeSpan;}
			set{this._timeSpan	= value;}
		}
		#endregion

		#region List<UrlManagerConfig>
		/// <summary>
		/// ��XML�ĵ������з������ö������顣
		/// </summary>
		/// <param name="doc">XML�ĵ�����</param>
		/// <param name="host">��������</param>
		/// <returns>���ö������顣</returns>
		public static List<UrlManagerConfig> GetListFromXmlDocument(XmlDocument doc , string host)
		{
			XmlNodeList nodes					= doc.SelectNodes("configs/" + host + "/location");
            List<UrlManagerConfig> list = new List<UrlManagerConfig>(nodes.Count);
			foreach (XmlNode node in nodes)
			{
				list.Add(GetFromXmlNode(node , host));
			}

			return list;
		}
		#endregion

		#region GetFromXmlNode
		/// <summary>
		/// ��XML����ڵ㷵�ض������
		/// </summary>
		/// <param name="node">XML�ڵ㡣</param>
		/// <param name="host">��������</param>
		/// <returns>����</returns>
		public static UrlManagerConfig GetFromXmlNode(XmlNode node , string host)
		{
			UrlManagerConfig item	= new UrlManagerConfig();

			item.Host				= host;

			if (node.Attributes["action"] != null &&
				node.Attributes["action"].Value != null && 
				node.Attributes["action"].Value.Length > 0)
			{
				item.Action		= node.Attributes["action"].Value;
			}

			if (node.Attributes["pattern"] != null &&
				node.Attributes["pattern"].Value != null && 
				node.Attributes["pattern"].Value.Length > 0)
			{
				item.Pattern		= node.Attributes["pattern"].Value;
			}

			if (node.Attributes["url"] != null &&
				node.Attributes["url"].Value != null && 
				node.Attributes["url"].Value.Length > 0)
			{
				item.Url			= node.Attributes["url"].Value;
			}


			if (node.Attributes["realpath"] != null &&
				node.Attributes["realpath"].Value != null && 
				node.Attributes["realpath"].Value.Length > 0)
			{
				item.RealPath		= node.Attributes["realpath"].Value;
			}

			if (node.Attributes["filepath"] != null &&
				node.Attributes["filepath"].Value != null && 
				node.Attributes["filepath"].Value.Length > 0)
			{
				item.FilePath		= node.Attributes["filepath"].Value;
			}

			if (node.Attributes["pathinfo"] != null &&
				node.Attributes["pathinfo"].Value != null && 
				node.Attributes["pathinfo"].Value.Length > 0)
			{
				item.PathInfo		= node.Attributes["pathinfo"].Value;
			}
			if (node.Attributes["querystring"] != null &&
				node.Attributes["querystring"].Value != null && 
				node.Attributes["querystring"].Value.Length > 0)
			{
				item.QueryString	= node.Attributes["querystring"].Value;
			}
			if (node.Attributes["timespan"] != null &&
				node.Attributes["timespan"].Value != null && 
				node.Attributes["timespan"].Value.Length > 0)
			{
				item.TimeSpan		= TimeSpan.Parse(node.Attributes["timespan"].Value);
			}

			return item;
		}
		#endregion

		#region GetConfigs
		/// <summary>
		/// ��������������Ӧ�������
		/// </summary>
		/// <param name="host"></param>
		/// <returns></returns>
		public static List<UrlManagerConfig> GetConfigs(string host)
		{
			if (webCache.Get(host) != null)
			{
                return webCache.Get(host) as List<UrlManagerConfig>;
			}			
			string path	= viviapi.SysConfig.RuntimeSetting.UrlManagerConfigPath;
			
			if (System.IO.File.Exists(path))
			{
				try
				{
					XmlTextReader reader		= new XmlTextReader(path);
					reader.MoveToContent();
					XmlDocument doc				= new XmlDocument();
					doc.LoadXml(reader.ReadOuterXml());

					List<UrlManagerConfig> list	= GetListFromXmlDocument(doc , host);
					//cache.Add(host , list , CacheItemPriority.Normal , null , new Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.FileDependency(path));
                    webCache.Insert(host, list, new System.Web.Caching.CacheDependency(path));
					reader.Close();
					return list;
				}
				catch (Exception ex)
				{
					viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
				}
			}
			return new List<UrlManagerConfig>(0);
		}
		#endregion

		#region IsMatch
		/// <summary>
		/// �ж��Ƿ���Ϲ���
		/// </summary>
		/// <param name="context">HttpContext �������ṩ������Ϊ HTTP �����ṩ������ڲ������������� Request��Response��Session �� Server�������á�</param>
		/// <param name="config">���ϵ������</param>
		/// <param name="simplePath">�����Ӧ�ó����Ŀ¼��·����</param>
		/// <param name="realPath">�����Ӧ�ó����Ŀ¼��ʵ��������ļ�·����</param>
		/// <param name="filePath">�����Ӧ�ó����Ŀ¼�ľ�̬�ļ�·����</param>
		/// <param name="pathInfo">Դ�ĸ���·����Ϣ�� </param>
		/// <param name="queryString">�����ѯ�ַ�����</param>
		/// <param name="url">�ض���URL��</param>
		/// <returns></returns>
		public static bool IsMatch(HttpContext context , 
			ref UrlManagerConfig config ,
			ref string simplePath ,
			ref string realPath ,
			ref string filePath ,
			ref string pathInfo ,
			ref string queryString ,
			ref string url)
		{
			string simpleUrl	= context.Request.RawUrl.Substring((context.Request.ApplicationPath == "/") ? (context.Request.ApplicationPath.Length - 1) : context.Request.ApplicationPath.Length);

			simplePath			= context.Request.Path.Substring((context.Request.ApplicationPath == "/") ? (context.Request.ApplicationPath.Length - 1) : context.Request.ApplicationPath.Length);

			List<UrlManagerConfig> defaultList	= GetConfigs("none");

			if (defaultList != null)
			{
				foreach (UrlManagerConfig item in defaultList)
				{
					Regex reg = new Regex(item.Pattern , RegexOptions.IgnoreCase | RegexOptions.Singleline);
					Match m	= reg.Match(simpleUrl);

					if (m.Success)
					{
						config			= item;

						string[] strs	= new string[m.Groups.Count];
						for (int i = 0; i < m.Groups.Count ; i++) 
						{
							strs[i]		= m.Groups[i].Value;
						}
						
						realPath		= string.Format(item.RealPath		, strs);
						filePath		= FormatFilePath(context , string.Format(item.FilePath		, strs));
						pathInfo		= string.Format(item.PathInfo		, strs);
						queryString		= string.Format(item.QueryString	, strs);
						url				= string.Format(item.Url			, strs);
//						realPath		= context.Request.Path;
//						filePath		= context.Request.Path;
//						pathInfo		= context.Request.PathInfo;
//						queryString		= context.Request.QueryString.ToString();

						return true;
					}
				}
			}

			List<UrlManagerConfig> list	= GetConfigs(context.Request.Url.Host);


			if (list != null)
			{
				foreach (UrlManagerConfig item in list)
				{
					Regex reg = new Regex(item.Pattern , RegexOptions.IgnoreCase | RegexOptions.Singleline);

					Match m	= reg.Match(simpleUrl);

					if (m.Success)
					{
						config			= item;
						
						string[] strs	= new string[m.Groups.Count];
						for (int i = 0; i < m.Groups.Count ; i++) 
						{
							strs[i]		= m.Groups[i].Value;
						}
						
						realPath		= string.Format(item.RealPath		, strs);
						filePath		= FormatFilePath(context , string.Format(item.FilePath		, strs));
						pathInfo		= string.Format(item.PathInfo		, strs);
						queryString		= string.Format(item.QueryString	, strs);
						url				= string.Format(item.Url			, strs);

						return true;
					}
				}
			}

			return false;
		}
		#endregion

		#region FormatFilePath
		/// <summary>
		/// ��ʽ������·����
		/// </summary>
		/// <param name="context"></param>
		/// <param name="filePath"></param>
		/// <returns></returns>
		internal static string FormatFilePath(HttpContext context , string filePath)
		{
			int index	= filePath.LastIndexOf(".");
			if (index > 0)
			{
				return filePath.Substring(0 , index) + "." + context.Request.Url.Host + filePath.Substring(index);
			}
			return filePath;
		}
		#endregion		
	}
}
