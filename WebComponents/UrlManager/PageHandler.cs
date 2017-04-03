using System;
using System.Web;
using System.Web.UI;
using System.IO;

namespace viviLib.WebComponents.UrlManager
{
	/// <summary>
	/// PageHandler ��ժҪ˵����
	/// </summary>
	public class PageHandler : HandlerBase
	{
		/// <summary>
		/// ���캯����
		/// </summary>
		public PageHandler()
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
		/// ͨ��ʵ�� IHttpHandler �ӿڵ��Զ��� HttpHandler ���� HTTP Web ����Ĵ���
		/// </summary>
		/// <param name="context">HttpContext �������ṩ������Ϊ HTTP �����ṩ������ڲ������������� Request��Response��Session �� Server�������á�</param>
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
						//һ�㾲̬�ļ�����.
					case "cache_static_news":
						//���Ų鿴��̬�ļ�����
					case "cache_static_pub":
						//�ѵ�鿴��̬�ļ�����
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
									#region ���Ų鿴��̬�ļ�����
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
									#region һ�㾲̬�ļ�����
									PageParser.GetCompiledPageInstance(this.RealPath	, path , context).ProcessRequest(context);
									this.SetStaticFilter(context , filePath);
									return;
									#endregion
							}
						}
						else
						{
							//ֱ�ӵ��þ�̬�ļ�
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
