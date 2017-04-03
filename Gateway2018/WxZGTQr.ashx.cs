using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace viviAPI.Gateway2018
{
    /// <summary>
    /// WxZGTQr 的摘要说明
    /// </summary>
    public class WxZGTQr : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string url = context.Request.QueryString["url"];
            byte[] buffer = Hex2byte(url);
            context.Response.ClearContent();
            context.Response.ContentType = "image/Gif";
            context.Response.BinaryWrite(buffer);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public static byte[] Hex2byte(string s)
        {
            byte[] src = Encoding.Default.GetBytes(s.ToLower());
            byte[] ret = new byte[src.Count() / 2];
            for (int i = 0; i < src.Count(); i += 2)
            {
                byte hi = src[i];
                byte low = src[i + 1];
                hi = (byte)((hi >= 'a' && hi <= 'f') ? 0x0a + (hi - 'a')
                : hi - '0');
                low = (byte)((low >= 'a' && low <= 'f') ? 0x0a + (low - 'a')
                : low - '0');
                ret[i / 2] = (byte)(hi << 4 | low);
            }
            return ret;
        }
    }
}