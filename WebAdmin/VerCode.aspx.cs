using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.WebAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public partial class VerCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int num = 68;
            int height = 21;
            int num2 = 1;
            base.Response.Cache.SetNoStore();
            Bitmap bitmap = new Bitmap(num, height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Color[] array = new Color[]
			{
				Color.AliceBlue,
				Color.Aqua,
				Color.Black,
				Color.Brown,
				Color.DarkRed,
				Color.SkyBlue,
				Color.Silver,
				Color.Tan,
				Color.Violet,
				Color.SpringGreen
			};
            try
            {
                Random random = new Random();
                graphics.Clear(Color.White);
                for (int i = 0; i < 2; i++)
                {
                    int x = random.Next(bitmap.Width) - num2;
                    int x2 = random.Next(bitmap.Width) - num2;
                    int y = random.Next(bitmap.Height);
                    int y2 = random.Next(bitmap.Height);
                    graphics.DrawLine(new Pen(Color.Silver), x, y, x2, y2);
                }
                string text = "3,4,5,6,7,9,0,1,2,8";
                string[] array2 = text.Split(new char[]
				{
					','
				});
                string text2 = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    text2 += array2[random.Next(array2.Length)];
                }
                Font font = new Font("Verdana", 12f, FontStyle.Bold);
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, bitmap.Width - num2, bitmap.Height), Color.DarkRed, Color.DarkRed, 1.2f, true);
                graphics.DrawString(text2, font, brush, (float)random.Next(bitmap.Width - num), (float)random.Next(bitmap.Height - 20));
                for (int i = 0; i < 5; i++)
                {
                }
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Gif";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                this.Session["CCode"] = text2.ToLower();
            }
            finally
            {
                graphics.Dispose();
                bitmap.Dispose();
            }
        }
        public static string CheckValiDateNo(string str)
        {
            object obj = HttpContext.Current.Session["CCode"];
            string result;
            if (obj == null)
            {
                result = "验证码失效";
            }
            else
            {
                if (obj.ToString().Equals(str.ToUpper()))
                {
                    result = "";
                }
                else
                {
                    result = "验证码不正确，请重新输入！";
                }
            }
            return result;
        }
        public static void CreateCheckCodeImage(int count)
        {
            string text = VerCode.GenerateCheckCode(count);
            if (text != null && text.Trim() != string.Empty)
            {
                Bitmap bitmap = new Bitmap((int)Math.Ceiling((double)text.Length * 12.5), 22);
                Graphics graphics = Graphics.FromImage(bitmap);
                try
                {
                    Random random = new Random();
                    graphics.Clear(Color.White);
                    for (int i = 0; i < 25; i++)
                    {
                        int x = random.Next(bitmap.Width);
                        int x2 = random.Next(bitmap.Width);
                        int y = random.Next(bitmap.Height);
                        int y2 = random.Next(bitmap.Height);
                        graphics.DrawLine(new Pen(Color.Silver), x, y, x2, y2);
                    }
                    Font font = new Font("Arial", 12f, FontStyle.Bold | FontStyle.Italic);
                    System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, bitmap.Width, bitmap.Height), Color.Green, Color.OrangeRed, 1.2f, true);
                    graphics.DrawString(text, font, brush, 2f, 2f);
                    for (int j = 0; j < 100; j++)
                    {
                        int x3 = random.Next(bitmap.Width);
                        int y3 = random.Next(bitmap.Height);
                        bitmap.SetPixel(x3, y3, Color.FromArgb(random.Next()));
                    }
                    graphics.DrawRectangle(new Pen(Color.Green), 0, 0, bitmap.Width - 1, bitmap.Height - 1);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.ContentType = "image/Gif";
                    HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                }
                finally
                {
                    graphics.Dispose();
                    bitmap.Dispose();
                }
            }
        }
        private static string GenerateCheckCode(int count)
        {
            string text = new Random().Next(10000, 99999).ToString();
            HttpContext.Current.Session["CCode"] = text;
            return text;
        }
    }
}