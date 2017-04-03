using System;
using System.Web;
using System.Collections.Generic;

namespace viviLib.WebComponents.UrlManager
{
	/// <summary>
	/// HandlerBase 的摘要说明。
	/// </summary>
	public class HandlerBase : System.Web.IHttpHandler , System.Web.SessionState.IRequiresSessionState
	{
		/// <summary>
		/// 构造函数。
		/// </summary>
		public HandlerBase()
		{
		}

		/// <summary>
		/// 构造函数。
		/// </summary>
		/// <param name="context">HttpContext 对象，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session 和 Server）的引用。</param>
		/// <param name="config">符合的配置项。</param>
		/// <param name="simplePath">相对于应用程序根目录的路径。</param>
		/// <param name="realPath">相对于应用程序根目录的实际需调用文件路径。</param>
		/// <param name="filePath">相对于应用程序根目录的静态文件路径。</param>
		/// <param name="pathInfo">源的附加路径信息。 </param>
		/// <param name="queryString">请求查询字符串。</param>
		/// <param name="url">重定向URL。</param>
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

		#region IHttpHandler 成员
		/// <summary>
		/// 通过实现 IHttpHandler 接口的自定义 HttpHandler 启用 HTTP Web 请求的处理。
		/// </summary>
		/// <param name="context">HttpContext 对象，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session 和 Server）的引用。</param>
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
		/// 获取一个值，该值指示其他请求是否可以使用 IHttpHandler 实例。
		/// </summary>
		public virtual bool IsReusable 
		{
			get { return true; }
		}
		#endregion


		#region Error404Url
		/// <summary>
		/// 404错误的请求页面。
		/// </summary>
		protected string Error404Url
		{
            get { return "/Error404Url.aspx"; } //SystemConfiguration.SystemPath.Error404Url;}
		}
		#endregion

		#region SetStaticFilter
		/// <summary>
		/// 设置过滤器。
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
		/// 正在写入的静态文件路径。
		/// </summary>
		public static List<String> WritingStaticFilePathes
		{
			get{return _writingStaticFilePathes;}
		}
        private static List<String> _writingStaticFilePathes = new List<String>();
		#endregion
	}
}
