using System;
using System.Web;
using System.Web.UI;
using System.IO;

namespace viviLib.WebComponents.UrlManager
{
	/// <summary>
	/// PageHandler 的摘要说明。
	/// </summary>
	public class PageHandler : HandlerBase
	{
		/// <summary>
		/// 构造函数。
		/// </summary>
		public PageHandler()
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
		public PageHandler(
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


		/// <summary>
		/// 通过实现 IHttpHandler 接口的自定义 HttpHandler 启用 HTTP Web 请求的处理。
		/// </summary>
		/// <param name="context">HttpContext 对象，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session 和 Server）的引用。</param>
		public override void ProcessRequest(System.Web.HttpContext context)
		{
			base.ProcessRequest(context);

			string path	= context.Server.MapPath(this.RealPath);
			if (System.IO.File.Exists(path))
			{
				switch (this.Config.Action)
				{
					case "none":
						PageParser.GetCompiledPageInstance(this.RealPath , path , context).ProcessRequest( context );
						break;
					case "rewrite":
						context.RewritePath(context.Request.Path , this.PathInfo , this.QueryString);
						PageParser.GetCompiledPageInstance(this.RealPath , path , context).ProcessRequest( context );
						break;
					case "cache_static":
						//一般静态文件缓存.
					case "cache_static_news":
						//新闻查看静态文件缓存
					case "cache_static_pub":
						//蒲点查看静态文件缓存
						string filePath		= context.Server.MapPath(this.FilePath);
						FileInfo fileInfo		= new FileInfo(filePath);
						FileInfo fileInfoSource	= new FileInfo(path);
						bool toRebuild		= false;
						if (!fileInfo.Exists ||
							(this.Config.TimeSpan.Ticks > 0 &&
							fileInfo.LastWriteTime.Add(this.Config.TimeSpan) < DateTime.Now) ||
							fileInfoSource.LastWriteTime > fileInfo.LastWriteTime)
						{
							toRebuild		= true;
						}

						if (toRebuild)
						{
							context.RewritePath(context.Request.Path , this.PathInfo , this.QueryString);

							switch (this.Config.Action)
							{
								case "cache_static_news":
									#region 新闻查看静态文件缓存
									/*long newsId			= 0;
									if (context.Request.QueryString["newsId"] != null && 
										context.Request.QueryString["newsId"].Length != 0)
									{
										newsId			= Convert.ToInt64(context.Request.QueryString["newsId"]	, 10);

									}
									DataModel.News.News newsItem	= null;
						
									if (context.Items[News.PageBaseNewsView.CONTEXT_KEY_NEWS] == null &&
										newsId > 0)
									{
										context.Items[News.PageBaseNewsView.CONTEXT_KEY_NEWS]	= DAL.News.News.Get(newsId);
									}

									newsItem	= context.Items[News.PageBaseNewsView.CONTEXT_KEY_NEWS] as DataModel.News.News;

									if (newsItem == null)
									{
										context.Response.Redirect(this.Error404Url	, true);
									}

									if (newsItem != null &&
										newsItem.CheckState == DataModel.News.CheckState.Passed &&
										newsItem.IsView)
									{
										PageParser.GetCompiledPageInstance(this.RealPath	, path , context).ProcessRequest(context);

										this.SetStaticFilter(context , filePath);
										return;
									}
									else
									{
										context.Response.Redirect(this.Error404Url	, true);
										return;
									}*/
									#endregion
								case "cache_static":
								default:
									#region 一般静态文件缓存
									PageParser.GetCompiledPageInstance(this.RealPath	, path , context).ProcessRequest(context);
									this.SetStaticFilter(context , filePath);
									return;
									#endregion
							}
						}
						else
						{
							//直接调用静态文件
							context.Response.WriteFile(filePath);
							context.Response.End();
						}
						break;
				}
			}
			else 
			{
				context.Response.Redirect(this.Error404Url	, true);
			}
		}
	}
}
