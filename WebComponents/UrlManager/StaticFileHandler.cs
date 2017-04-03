using System;
using System.Web;

namespace viviLib.WebComponents.UrlManager
{
	/// <summary>
	/// StaticFileHandler 的摘要说明。
	/// </summary>
	public class StaticFileHandler : HandlerBase
	{
		/// <summary>
		/// 构造函数。
		/// </summary>
		public StaticFileHandler()
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
		public StaticFileHandler(
			HttpContext context ,
			viviapi.SysConfig.UrlManagerConfig config ,
			string simplePath ,
			string realPath ,
			string filePath ,
			string pathInfo ,
			string queryString ,
			string url) : base(context , config , simplePath , realPath , filePath , pathInfo , queryString , url)
		{
		}

		public override void ProcessRequest(System.Web.HttpContext context)
		{
			base.ProcessRequest (context);

//			context.Response.Write(this.RealPath);


			context.Response.Clear();
			string path	= context.Server.MapPath(this.RealPath);
			if (System.IO.File.Exists(path))
			{
				context.Response.WriteFile(path);
				context.Response.End();
			}
			else 
			{
				context.Response.Redirect(this.Error404Url	, true);
			}
		}

	}
}
