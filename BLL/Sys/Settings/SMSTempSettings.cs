using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Sys
{
    /// <summary>
    /// 短信模板设置
    /// </summary>
    public class SMSTempSettings
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
                return SysConfig.Instance.GetList("type=3").Tables[0];
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
        #endregion

        #region SMS_Temp_Register
        /// <summary>
        /// 
        /// </summary>
        public static string SMSTempRegister
        {
            get
            {
                return SysConfig.Instance.GetValue("SMS_Temp_Register");
            }
        }
        #endregion

        #region SMS_Temp_Authenticate
        /// <summary>
        /// 
        /// </summary>
        public static string SMS_Temp_Authenticate 
        {
            get
            {
                return SysConfig.Instance.GetValue("SMS_Temp_Authenticate");
            }
        }
        #endregion

        #region SMS_Temp_Modify
        /// <summary>
        /// 
        /// </summary>
        public static string SMS_Temp_Modify
        {
            get
            {
                return SysConfig.Instance.GetValue("SMS_Temp_Modify");
            }
        }
        #endregion

        #region SMS_Temp_FindPwd
        /// <summary>
        /// 
        /// </summary>
        public static string SMS_Temp_FindPwd
        {
            get
            {
                return SysConfig.Instance.GetValue("SMS_Temp_FindPwd");
            }
        }
        #endregion

        #region SMS_Temp_Withdraw
        /// <summary>
        /// 
        /// </summary>
        public static string SMS_Temp_Withdraw
        {
            get
            {
                return SysConfig.Instance.GetValue("SMS_Temp_Withdraw");
            }
        }
        #endregion

        #region SMS_KEY
        /// <summary>
        /// 
        /// </summary>
        public static string SMS_KEY
        {
            get
            {
                return SysConfig.Instance.GetValue("SMS_Temp_1");
            }
        }
        #endregion

        #region SMS_SN
        /// <summary>
        /// 
        /// </summary>
        public static string SMS_SN
        {
            get
            {
                return SysConfig.Instance.GetValue("SMS_Temp_2");
            }
        }
        #endregion

        #region SMS_WebSite
        /// <summary>
        /// 
        /// </summary>
        public static string SMS_WebSite
        {
            get
            {
                return SysConfig.Instance.GetValue("SMS_Temp_3");
            }
        }
        #endregion

        #region SMS_Success
        /// <summary>
        /// 
        /// </summary>
        public static string SMS_SendSuccessCode
        {
            get
            {
                return SysConfig.Instance.GetValue("SMS_SendSuccessCode");
            }
        }
        #endregion
    }
}
