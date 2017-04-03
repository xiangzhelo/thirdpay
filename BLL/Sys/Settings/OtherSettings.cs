using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public class OtherSettings
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
                return SysConfig.Instance.GetList("type=5").Tables[0];
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
        #endregion

        #region AppKey
        /// <summary>
        /// AppKey
        /// </summary>
        public static string AppKey
        {
            get
            {
                return SysConfig.Instance.GetValue("AppKey");
            }
        }
        #endregion

        #region AppSecret
        /// <summary>
        /// AppKey
        /// </summary>
        public static string AppSecret
        {
            get
            {
                return SysConfig.Instance.GetValue("AppSecret");
            }
        }
        #endregion

        #region AliDCode_logo_name
        /// <summary>
        /// AliDCode_logo_name
        /// </summary>
        public static string AliDCode_logo_name
        {
            get
            {
                return SysConfig.Instance.GetValue("AliDCode_logo_name");
            }
        }
        #endregion

        #region AliDCode_goods_info_name
        /// <summary>
        /// AliDCode_goods_info_name
        /// </summary>
        public static string AliDCode_goods_info_name
        {
            get
            {
                return SysConfig.Instance.GetValue("AliDCode_goods_info_name");
            }
        }
        #endregion

        #region AliDCode_goods_info_desc
        /// <summary>
        /// AliDCode_goods_info_desc
        /// </summary>
        public static string AliDCode_goods_info_desc
        {
            get
            {
                return SysConfig.Instance.GetValue("AliDCode_goods_info_desc");
            }
        }
        #endregion
    }
}
