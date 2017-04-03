using System;
using System.Web;

namespace viviLib.WebComponents.UrlManager
{
	/// <summary>
	/// StaticFileHandler ��ժҪ˵����
	/// </summary>
	public class StaticFileHandler : HandlerBase
	{
		/// <summary>
		/// ���캯����
		/// </summary>
		public StaticFileHandler()
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
