using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.ETAPI.Swiftpass;

namespace viviAPI.Gateway2018.Receive.swiftpass
{
    public partial class result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var gw = new Gateway();
            gw.callback();
        }
    }
}