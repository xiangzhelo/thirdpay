using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace viviapi.Cache
{
    /// <summary>
    /// 基础配置项,包括站点所在相对路径
    /// </summary>
    public class BaseConfigs
    {
        private static string webPath = "";

        static BaseConfigs()
        {
            webPath = System.Configuration.ConfigurationManager.AppSettings["WebPath"];
        }

        /// <summary>
        /// 获取站点根目录信息
        /// </summary>
        public static string GetPath
        {
            get { return webPath; }
        }
    }
}
