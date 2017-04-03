using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.News;
using viviapi.WebComponents.Web;
using viviLib.Web;


namespace viviAPI.WebUI2015.longbao
{
    public partial class News : PageBase
    {
        public int NewsTypeId
        {
            get
            {
                return WebBase.GetQueryStringInt32("type", 1);
            }

        }
        public int Newtid = 1;
        public string Gettp = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["tid"].ToString().Equals("2"))
                {
                    Newtid = 2;
                    Gettp = "站内公告";
                }
                else
                {
                    Newtid = 1;
                    Gettp = "新闻动态";
                }
            }
            catch
            {
                Gettp = "新闻动态";
                Newtid = 1;
            }
            if (!this.IsPostBack)
            {
                //this.Title = getTitle("新闻公告");
                LoadData();
            }
        }

        void InitForm()
        {
            //if (newsTypeId == 1)
            //{
            //    memu1.CssClass = "ahover";
            //    memu2.CssClass = "";
            //    memu3.CssClass = "";

            //    litTypeName.Text = "最新动态";
            //}
            //else if (newsTypeId == 4)
            //{
            //    memu1.CssClass = "";
            //    memu2.CssClass = "ahover";
            //    memu3.CssClass = "";

            //    litTypeName.Text = "系统公告";
            //}
            //else if (newsTypeId == 6)
            //{
            //    memu1.CssClass = "";
            //    memu2.CssClass = "";
            //    memu3.CssClass = "ahover";

            //    litTypeName.Text = "帮助 & 反馈";
            //}
        }

        void LoadData()
        {
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();
            listParam.Add(new viviLib.Data.SearchParam("newstype", Newtid));
            listParam.Add(new viviLib.Data.SearchParam("release", 1));
            DataSet pageData = NewsFactory.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);

            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptnewlist.DataSource = pageData.Tables[1];
            this.rptnewlist.DataBind();
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        public static string NoHTML(string Htmlstring)  //替换HTML标记   
        {
            //删除脚本   
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

            if (Htmlstring.Length > 255)
                Htmlstring = Htmlstring.Substring(0, 255) + "...";
            return Htmlstring;

        }
    }
}
