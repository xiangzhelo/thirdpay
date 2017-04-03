using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents;
using viviapi.Model;
using viviLib;
using viviapi.BLL;
using System.Data;

namespace viviAPI.WebUI2015.longbao
{
    

    public partial class site : System.Web.UI.MasterPage
    {public string WebSiteTitleSuffix = "";
        public string KeyWords = "";
        public string Description = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            DataTable data = viviapi.BLL.Sys.SiteSettings.GetKeyValues();

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    string key = row["key"].ToString();
                    string value = row["value"].ToString();

                    if (key == "WebSiteTitleSuffix")
                        WebSiteTitleSuffix = value;

                    else if (key == "KeyWords")
                        KeyWords = value;

                    else if (key == "Description")
                        Description = value;
                }
            }
        }

        //private WebInfo _objectInfo = null;
        //public WebInfo ObjectInfo
        //{
        //    get
        //    {
        //        if (_objectInfo == null)
        //        {
        //            _objectInfo = WebInfoFactory.GetWebInfoByDomain(XRequest.GetHost());
        //        }
        //        return _objectInfo;
        //    }
        //}


    }
}