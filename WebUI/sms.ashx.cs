﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace viviAPI.WebUI2015
{
    /// <summary>
    /// sms 的摘要说明
    /// </summary>
    public class sms : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write("1");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}