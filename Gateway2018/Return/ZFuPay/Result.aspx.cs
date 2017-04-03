using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.Gateway2018.Return.ZFuPay
{
    public partial class Result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pageWrite(Request.Form.ToString().Trim());
            viviapi.ETAPI.ZFuPay.PostPay.Default.Return(HttpContext.Current);
        }
        protected void pageWrite(string str)
        {
            str = str + "%%%%%%%%%%%%";
            FileStream fs = File.Open("D:\\Result.txt", FileMode.Append, FileAccess.Write);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }
    }
}