using System;

namespace viviLib.WebComponents.UrlManager
{
	/// <summary>
	/// UrlManagerFactory ��ժҪ˵����
	/// </summary>
	public class UrlManagerFactory : System.Web.IHttpHandlerFactory
	{
		#region IHttpHandlerFactory ��Ա
		/// <summary>
		/// ʹ���������������еĴ������ʵ����
		/// </summary>
		/// <param name="handler">Ҫ���õ� IHttpHandler ����</param>
		public void ReleaseHandler(System.Web.IHttpHandler handler)
		{
		}

		/// <summary>
		/// ����ʵ�� IHttpHandler �ӿڵ����ʵ����
		/// </summary>
		/// <param name="context">HttpContext ���ʵ�������ṩ������Ϊ HTTP �����ṩ������ڲ������������� Request��Response��Session �� Server�������á�</param>
		/// <param name="requestType">�ͻ���ʹ�õ� HTTP ���ݴ��䷽����GET �� POST���� </param>
		/// <param name="url">��������Դ�� RawUrl��</param>
		/// <param name="pathTranslated">��������Դ�� PhysicalApplicationPath��</param>
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
