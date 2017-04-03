using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using viviapi.BLL.News;
using viviapi.Model;
using viviapi.Model.News;
using viviapi.WebComponents.Web;
using viviapi.WebComponents;
using viviapi.Model.User;
using viviapi.BLL.User;
using viviLib.Security;
using viviapi.WebComponents.Template;

namespace viviAPI.WebUI7uka
{
    public partial class Index : PageBase
    {
        public int imcount = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //新闻
                List<NewsInfo> list = NewsFactory.GetCacheList(1, 1, 6);
                if (list != null)
                {
                    Repeater1.DataSource = list;
                    Repeater1.DataBind();
                    //this.rpnews.DataSource = list;
                    //this.rpnews.DataBind();
                }

                //公告
                List<NewsInfo> glist = NewsFactory.GetCacheList(2, 1, 2);
                if (list != null)
                {
                    //this.rpgg.DataSource = glist;
                    //this.rpgg.DataBind();
                }
            }
        }
        
        public static string NoHTML(string Htmlstring)  //替换HTML标记   
        { //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<img[^>]*>;", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            if (Htmlstring.Length > 100)
            {
                Htmlstring = Htmlstring.Substring(0, 100);
            }

            return Htmlstring;

        }

        public int Getim()
        {
            return imcount++;
        }
    }
}
