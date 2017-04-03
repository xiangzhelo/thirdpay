using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using viviapi.WebComponents.ScheduledTask;
using viviLib.ExceptionHandling;
using viviLib.Web;
using System.IO;

namespace viviAPI.Gateway2018
{
    public class Global : System.Web.HttpApplication
    {
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