using System;

namespace viviLib.WebComponents.UrlManager
{
	/// <summary>
	/// UrlManagerFactory 的摘要说明。
	/// </summary>
	public class UrlManagerFactory : System.Web.IHttpHandlerFactory
	{
		#region IHttpHandlerFactory 成员
		/// <summary>
		/// 使工厂可以重用现有的处理程序实例。
		/// </summary>
		/// <param name="handler">要重用的 IHttpHandler 对象。</param>
		public void ReleaseHandler(System.Web.IHttpHandler handler)
		{
		}

		/// <summary>
		/// 返回实现 IHttpHandler 接口的类的实例。
		/// </summary>
		/// <param name="context">HttpContext 类的实例，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session 和 Server）的引用。</param>
		/// <param name="requestType">客户端使用的 HTTP 数据传输方法（GET 或 POST）。 </param>
		/// <param name="url">所请求资源的 RawUrl。</param>
		/// <param name="pathTranslated">所请求资源的 PhysicalApplicationPath。</param>
		/// <returns></returns>
		public System.Web.IHttpHandler GetHandler(System.Web.HttpContext context, string requestType, string url, string pathTranslated)
		{
//			context.Response.Write(requestType + "<BR>");
//			context.Response.Write(url + "<BR>");
//			context.Response.Write(pathTranslated + "<BR>");

		    viviapi.SysConfig.UrlManagerConfig config	= null;
			string simplePath							= string.Empty;
			string realPath								= string.Empty;
			string filePath								= string.Empty;
			string pathInfo								= string.Empty;
			string queryString							= string.Empty;
			string redirectUrl							= string.Empty;

            bool isMatch = viviapi.SysConfig.UrlManagerConfig.IsMatch(context, ref config, ref simplePath, ref realPath, ref filePath, ref pathInfo, ref queryString, ref redirectUrl);


//			context.Response.Write(isMatch.ToString());
//			context.Response.Write(simplePath + "<BR>");
//			context.Response.Write(realPath + "<BR>");
//			context.Response.Write(filePath + "<BR>");
//			context.Response.Write(queryString + "<BR>");

			if (config != null)
			{
//				context.Response.Write(config.Action + "<BR>");
//				context.Response.Write(config.Pattern + "<BR>");
//				context.Response.Write(config.RealPath + "<BR>");
//				context.Response.Write(config.FilePath + "<BR>");
//				context.Response.Write(config.QueryString + "<BR>");
//				context.Response.Write(config.PathInfo + "<BR>");

				if (System.IO.Path.GetExtension(context.Request.Path) == ".aspx")
				{
					return new PageHandler(context , config , simplePath , realPath , filePath , pathInfo , queryString , redirectUrl);
				}
				else
				{
					return new StaticFileHandler(context , config , simplePath , realPath , filePath , pathInfo , queryString , redirectUrl);
				}
			}
			if (System.IO.Path.GetExtension(context.Request.Path) == ".aspx")
			{
				return new PageHandler();
			}
			else
			{
				return new StaticFileHandler();
			}
		}

		#endregion
	}
}
