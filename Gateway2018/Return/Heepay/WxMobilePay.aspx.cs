﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.Gateway2018.Return.Heepay
{
    public partial class WxMobilePay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.ETAPI.Heepay.WxMobilePay pay = new viviapi.ETAPI.Heepay.WxMobilePay();
            pay.Notify(HttpContext.Current, isNotify: false);
        }
    }
}