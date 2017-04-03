using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBAccess;

using viviLib.Web;
using viviapi.Model;
using viviapi.BLL;

namespace viviapi.web
{
    public partial class Contact : viviapi.WebComponents.Web.PageBase
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ////新闻
                //List<NewsInfo> list = BLL.NewsFactory.GetCacheList(6, 1, 10);
                //if (list != null)
                //{
                //    this.rptnews.DataSource = list;
                //    this.rptnews.DataBind();
                //}
            }
        }
    }
}
