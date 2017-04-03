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

namespace viviAPI.WebAdmin
{
    public class Global : System.Web.HttpApplication
    {
        private ScheduledTasks scheduledTasks;
        protected void Application_Start(object sender, EventArgs e)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configurations\log4net.config");
            log4net.Config.XmlConfigurator.Configure(new FileInfo(path));

            WebBase.HttpApplication = this;
            this.scheduledTasks = new ScheduledTasks();
            this.scheduledTasks.Start();
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