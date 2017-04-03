using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

namespace viviAPI.WebAdmin
{
    public partial class Log4Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Random random = new Random();


            ILog log = log4net.LogManager.GetLogger("MyLogger");

            for (int i = 0; i < 1; i++)
            {
                //记录错误日志   
                log.Error("error", new Exception("在这里发生了一个异常,Error Number:" + random.Next()));
                //记录严重错误   
                log.Fatal("fatal", new Exception("在发生了一个致命错误，Exception Id：" + random.Next()));
                //记录一般信息   
                log.Info("提示：系统正在运行");
                //记录调试信息   
                log.Debug("调试信息：debug");
                //记录警告信息   
                log.Warn("警告：warn");
            }

            try
            {
                int i = 0;
                int j = 1;

                int a = j/i;
            }
            catch (Exception exception)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(exception);
                
            }
        }
    }
}
