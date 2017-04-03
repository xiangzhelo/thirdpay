using System;
using viviapi.BLL;
using viviapi.BLL.News;
using viviapi.Model;
using viviapi.Model.News;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebUI7uka
{
    public partial class View : PageBase
    {
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("newsid", 0);
            }
        }

        public int inajax
        {
            get
            {
                return WebBase.GetQueryStringInt32("inajax", 0);
            }
        }

        public string gettp = "";
        private NewsInfo _itemInfo = null;
        public string tid_1_str = "";
        public string tid_2_str = "";
        public NewsInfo ItemInfo
        {
            get
            {
                if (_itemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        _itemInfo = NewsFactory.GetCacheModel(this.ItemInfoId);
                    }
                }
                return _itemInfo;
            }
        }

        protected string PageTitle = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["tid"].ToString().Equals("2"))
                {
                    gettp = "站内公告";
                }
                else
                {
                    gettp = "新闻动态";
                }
            }
            catch
            {
                gettp = "新闻动态";
            }
            if (!this.IsPostBack)
            {
                if (ItemInfo != null)
                {
                    PageTitle = ItemInfo.newstitle;
                    if (ItemInfo != null)
                    {
                        if (ItemInfo.newstype.ToString() == "新闻")
                        {
                            tid_1_str = "class='about-menu-current'";
                            gettp = "新闻动态";
                        }
                        else {
                            tid_2_str = "class='about-menu-current'";
                            gettp = "站内公告";
                        }
                        if (inajax == 1)
                        {
                            Response.Write(ItemInfo.newscontent);
                            Response.End();
                        }
                        this.newstime.Text = ItemInfo.addTime.ToString();
                        this.newstitle.Text = ItemInfo.newstitle;
                        //newsaddtime.InnerText = ItemInfo.addTime.ToString("yyyy年MM月dd日 ");
                        this.newscontenct.Text = ItemInfo.newscontent;
                        //this.LitSiteName.Text = this.SiteName;
                        //this.litAddTime.Text = viviLib.TimeControl.FormatConvertor.DateTimeToDateString(ItemInfo.addTime);
                    }
                }
            }
        }
    }
}
