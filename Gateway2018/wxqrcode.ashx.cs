using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec;
using viviapi.ETAPI.Weixin;
using viviLib.Web;

namespace viviAPI.Gateway2018
{
    /// <summary>
    /// wxqrcode 的摘要说明
    /// </summary>
    public class wxqrcode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string url = HttpUtility.UrlDecode(WebBase.GetQueryStringString("url", string.Empty));

            if (!string.IsNullOrEmpty(url))
            {
                url = viviLib.Security.Cryptography.DecryptConnString(url).Replace("\0", "");

                ShowImage(url);
            }

            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="codeURL"></param>
        public static void ShowImage(string codeURL)
        {
            int widhtHeight = 300;
            string EC_level = "L";
            int margin = 0;

            var qrCodeEncoder = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                QRCodeScale = 4,
                QRCodeVersion = 0,
                QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L
            };


            //String data = UrlEncode1(chl);
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeURL);

            var mStream = new System.IO.MemoryStream();
            image.Save(mStream, System.Drawing.Imaging.ImageFormat.Gif);

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Gif";
            HttpContext.Current.Response.BinaryWrite(mStream.ToArray());
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}