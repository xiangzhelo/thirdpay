using System;
using viviapi.WebComponents.ScheduledTask;
using System.Configuration;
using viviLib.ExceptionHandling;
using viviLib.Web;
using System.IO;
using System.Web;
using System.Collections.Generic;

namespace viviAPI.WebUI7uka
{
    public class Global : System.Web.HttpApplication
    {

        /// <summary>
        /// 执行计划任务的类。
        /// </summary>
        private ScheduledTasks _scheduledTasks;

        protected void Application_Start(object sender, EventArgs e)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configurations\log4net.config");
            log4net.Config.XmlConfigurator.Configure(new FileInfo(path));

            WebBase.HttpApplication = this;
            this._scheduledTasks = new ScheduledTasks();
            this._scheduledTasks.Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
           /* if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["uidir"]))
            {
                if (Request.Url.PathAndQuery.IndexOf(ConfigurationManager.AppSettings["uidir"]) < 0)
                {
                    try
                    {
                        var url = "~/" + ConfigurationManager.AppSettings["uidir"] + "/" + Request.Url.PathAndQuery.TrimStart('/');
                        if (File.Exists(Server.MapPath(url)))
                        {
                            HttpContext.Current.Server.Transfer(url, true);
                        }
                    }
                    catch { }
                }
            }*/
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ExceptionHandler.HandleException(Server.GetLastError());
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}