using System;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace viviapi.SysConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class WebSetting
    {
        #region ScheduledTaskSettings
        /// <summary>
        /// Web.config文件中的officeKStar/sheduledTaskSettings节信息。
        /// </summary>
        public static List<viviLib.ScheduledTask.ScheduledTask> ScheduledTaskSettings
        {
            get
            {
                if (System.Configuration.ConfigurationManager.GetSection("officeKStar/scheduledTaskSettings") != null)
                {
                    return System.Configuration.ConfigurationManager.GetSection("officeKStar/scheduledTaskSettings") as List<viviLib.ScheduledTask.ScheduledTask>;

                }
                return new List<viviLib.ScheduledTask.ScheduledTask>();
            }
        }
        #endregion
    }
}
