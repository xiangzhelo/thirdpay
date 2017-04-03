using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public partial class validata_code : System.Web.UI.Page
{   
    
    protected void Page_Load(object sender, EventArgs e)
    {

        string sCode = "";
        //清除該頁輸出緩存，設置該頁無緩存
        Response.Buffer = true;
        Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.AppendHeader("Pragma", "No-Cache");

        // '將驗證碼圖片寫入記憶體流，並將其以 "image/Png" 格式輸出
        MemoryStream oStream = new MemoryStream();
        try
        {
            CreateValidateCodeImage(oStream, ref sCode, 4, 100, 40, 18);
            Session["_ValidateCode"] = sCode;
            Response.ClearContent();
            Response.ContentType = "image/Png";
            Response.BinaryWrite(oStream.ToArray());
        }
        finally
        {
            // '釋放資源
            oStream.Dispose();
        }
    }
    /// <summary>
    /// 產生圖形驗證碼。
    /// </summary>
    /// <param name="MemoryStream">記憶體資料流。</param>
    /// <param name="Code">傳出驗證碼。</param>
    /// <param name="CodeLength">驗證碼字元數。</param>
    /// <param name="Width"></param>
    /// <param name="Height"></param>
    /// <param name="FontSize"></param>
    public void CreateValidateCodeImage(MemoryStream MemoryStream, ref string Code, int CodeLength, int Width, int Height, int FontSize)
    {
        Bitmap oBmp;
        oBmp = CreateValidateCodeImage(ref Code, CodeLength, Width, Height, FontSize);
        try
        {
            oBmp.Save(MemoryStream, ImageFormat.Png);
        }
        finally { oBmp.Dispose(); }
    }
    /// <summary>
    /// 產生圖形驗證碼。
    /// </summary>
    /// <param name="Code">傳出驗證碼。</param>
    /// <param name="CodeLength">驗證碼字元數。</param>
    /// <param name="Width"></param>
    /// <param name="Height"></param>
    /// <param name="FontSize"></param>
    /// <returns></returns>
    public Bitmap CreateValidateCodeImage(ref string Code, int CodeLength, int Width, int Height, int FontSize)
    {
        String sCode = String.Empty;
        //顏色列表，用於驗證碼、噪線、噪點
        Color[] oColors ={
             System.Drawing.Color.Black,
             System.Drawing.Color.Red,
             System.Drawing.Color.Blue,
             System.Drawing.Color.Green,
             System.Drawing.Color.Orange,
             System.Drawing.Color.Brown,
             System.Drawing.Color.Brown,
             System.Drawing.Color.DarkBlue
            };
        //字體列表，用於驗證碼
        string[] oFontNames = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
        //驗證碼的字元集，去掉了一些容易混淆的字元
        char[] oCharacter = {
   '2',
   '3',
   '4',
   '5',
   '6',
   '8',
   '9',
   'A',
   'B',
   'C',
   'D',
   'E',
   'F',
   'G',
   'H',
   'J',
   'K',
   'L',
   'M',
   'N',
   'P',
   'R',
   'S',
   'T',
   'W',
   'X',
   'Y'
  };
        Random oRnd = new Random();
        Bitmap oBmp = null;
        Graphics oGraphics = null;
        int N1 = 0;
        System.Drawing.Point oPoint1 = default(System.Drawing.Point);
        System.Drawing.Point oPoint2 = default(System.Drawing.Point);
        string sFontName = null;
        Font oFont = null;
        Color oColor = default(Color);

        //生成驗證碼字串
        for (N1 = 0; N1 <= CodeLength - 1; N1++)
        {
            sCode += oCharacter[oRnd.Next(oCharacter.Length)];
        }

        oBmp = new Bitmap(Width, Height);
        oGraphics = Graphics.FromImage(oBmp);
        oGraphics.Clear(System.Drawing.Color.White);
        try
        {
            for (N1 = 0; N1 <= 4; N1++)
            {
                //畫噪線
                oPoint1.X = oRnd.Next(Width);
                oPoint1.Y = oRnd.Next(Height);
                oPoint2.X = oRnd.Next(Width);
                oPoint2.Y = oRnd.Next(Height);
                oColor = oColors[oRnd.Next(oColors.Length)];
                oGraphics.DrawLine(new Pen(oColor), oPoint1, oPoint2);
            }

            for (N1 = 0; N1 <= sCode.Length - 1; N1++)
            {
                //畫驗證碼字串
                sFontName = oFontNames[oRnd.Next(oFontNames.Length)];
                oFont = new Font(sFontName, FontSize, FontStyle.Italic);
                oColor = oColors[oRnd.Next(oColors.Length)];
                oGraphics.DrawString(sCode[N1].ToString(), oFont, new SolidBrush(oColor), Convert.ToSingle(N1) * FontSize + 10, Convert.ToSingle(8));
            }

            for (int i = 0; i <= 30; i++)
            {
                //畫噪點
                int x = oRnd.Next(oBmp.Width);
                int y = oRnd.Next(oBmp.Height);
                Color clr = oColors[oRnd.Next(oColors.Length)];
                oBmp.SetPixel(x, y, clr);
            }

            Code = sCode;
            return oBmp;
        }
        finally
        {
            oGraphics.Dispose();
        }


    }
}
