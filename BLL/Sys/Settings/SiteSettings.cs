using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Sys
{
    /// <summary>
    /// 网站类
    /// </summary>
    public class SiteSettings
    {
        #region GetKeyValues
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetKeyValues()
        {
            try
            {
                return SysConfig.Instance.GetList("type=0").Tables[0];
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
        #endregion

        #region WebSiteTitleSuffix
        /// <summary>
        /// 网站标题后缀
        /// </summary>
        public static string WebSiteTitleSuffix
        {
            get
            {
                return SysConfig.Instance.GetValue("WebSiteTitleSuffix");
            }
        }
        #endregion

        #region 网站关键字
        /// <summary>
        /// 网站关键字
        /// </summary>
        public static string KeyWords
        {
            get
            {
                return SysConfig.Instance.GetValue("KeyWords");
            }
        }
        #endregion

        #region 网站描述
        /// <summary>
        /// 网站描述
        /// </summary>
        public static string Description
        {
            get
            {
                return SysConfig.Instance.GetValue("Description");
            }
        }
        #endregion
    }
}
