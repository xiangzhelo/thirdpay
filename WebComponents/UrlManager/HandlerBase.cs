using System;
using System.Web;
using System.Collections.Generic;

namespace viviLib.WebComponents.UrlManager
{
	/// <summary>
	/// HandlerBase ��ժҪ˵����
	/// </summary>
	public class HandlerBase : System.Web.IHttpHandler , System.Web.SessionState.IRequiresSessionState
	{
		/// <summary>
		/// ���캯����
		/// </summary>
		public HandlerBase()
		{
		}

		/// <summary>
		/// ���캯����
		/// </summary>
		/// <param name="context">HttpContext �������ṩ������Ϊ HTTP �����ṩ������ڲ������������� Request��Response��Session �� Server�������á�</param>
		/// <param name="config">���ϵ������</param>
		/// <param name="simplePath">�����Ӧ�ó����Ŀ¼��·����</param>
		/// <param name="realPath">�����Ӧ�ó����Ŀ¼��ʵ��������ļ�·����</param>
		/// <param name="filePath">�����Ӧ�ó����Ŀ¼�ľ�̬�ļ�·����</param>
		/// <param name="pathInfo">Դ�ĸ���·����Ϣ�� </param>
		/// <param name="queryString">�����ѯ�ַ�����</param>
		/// <param name="url">�ض���URL��</param>
		public HandlerBase(
			HttpContext context ,
		 	viviapi.SysConfig.UrlManagerConfig config ,
			string simplePath ,
			string realPath ,
			string filePath ,
			string pathInfo ,
			string queryString , 
			string url) : this()
		{
			this.Config			= config;
			this.SimplePath		= context.Request.ApplicationPath == "/" ? simplePath : context.Request.ApplicationPath + simplePath;
			this.RealPath		= context.Request.ApplicationPath == "/" ? realPath : context.Request.ApplicationPath + realPath;
			this.FilePath		= context.Request.ApplicationPath == "/" ? filePath : context.Request.ApplicationPath + filePath;
			this.PathInfo		= pathInfo;
			this.QueryString	= queryString;
			this.Url			= url;
		}

		protected viviapi.SysConfig.UrlManagerConfig Config;
		protected string SimplePath;
		protected string RealPath;
		protected string FilePath;
		protected string PathInfo;
		protected string QueryString;
		protected string Url;

		#region IHttpHandler ��Ա
		/// <summary>
		/// ͨ��ʵ�� IHttpHandler �ӿڵ��Զ��� HttpHandler ���� HTTP Web ����Ĵ���
		/// </summary>
		/// <param name="context">HttpContext �������ṩ������Ϊ HTTP �����ṩ������ڲ������������� Request��Response��Session �� Server�������á�</param>
		public virtual void ProcessRequest(HttpContext context)
		{
			if (this.Config != null)
			{
				switch (this.Config.Action)
				{
					case "none":
						this.SimplePath		= context.Request.Path;
						this.RealPath		= context.Request.Path;
						this.FilePath		= context.Request.Path;
						this.PathInfo		= context.Request.PathInfo;
						this.QueryString	= context.Request.QueryString.ToString();
						this.Url			= context.Request.Url.ToString();
						break;
					case "redirect":
						context.Response.Redirect(this.Url , true);
						break;
					case "rewrite":
						break;
				}
			}
			else
			{
				this.Config			= new viviapi.SysConfig.UrlManagerConfig();
				this.Config.Action	= "none";
				this.SimplePath		= context.Request.Path;
				this.RealPath		= context.Request.Path;
				this.FilePath		= context.Request.Path;
				this.PathInfo		= context.Request.PathInfo;
				this.QueryString	= context.Request.QueryString.ToString();
				this.Url			= context.Request.Url.ToString();
			}
		}

		
		/// <summary>
		/// ��ȡһ��ֵ����ֵָʾ���������Ƿ����ʹ�� IHttpHandler ʵ����
		/// </summary>
		public virtual bool IsReusable 
		{
			get { return true; }
		}
		#endregion


		#region Error404Url
		/// <summary>
		/// 404���������ҳ�档
		/// </summary>
		protected string Error404Url
		{
            get { return "/Error404Url.aspx"; } //SystemConfiguration.SystemPath.Error404Url;}
		}
		#endregion

		#region SetStaticFilter
		/// <summary>
		/// ���ù�������
		/// </summary>
		/// <param name="context"></param>
		/// <param name="path"></param>
		protected void SetStaticFilter(HttpContext context , string physicalPath)
		{
			string dir				= System.IO.Path.GetDirectoryName(physicalPath);

			if (!System.IO.Directory.Exists(dir))
			{
				System.IO.Directory.CreateDirectory(dir);
			}

			
//			if (!HandlerBase.WritingStaticFilePathes.Contains(physicalPath.ToLower()))
//			{
//				lock (HandlerBase.WritingStaticFilePathes)
//				{
//					HandlerBase.WritingStaticFilePathes.Add(physicalPath.ToLower());
//				}
//				context.Response.Filter	= new Filter(context.Response.Filter		, physicalPath);
//			}
			context.Response.Filter	= new Filter(context.Response.Filter		, physicalPath);
		}
		#endregion

		#region WritingStaticFilePathes
		/// <summary>
		/// ����д��ľ�̬�ļ�·����
		/// </summary>
		public static List<String> WritingStaticFilePathes
		{
			get{return _writingStaticFilePathes;}
		}
        private static List<String> _writingStaticFilePathes = new List<String>();
		#endregion
	}
}
