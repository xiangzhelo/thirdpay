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
    public class SettleSettings
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
                return SysConfig.Instance.GetList("type=4").Tables[0];
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
        #endregion

        #region OpenWithdraw
        /// <summary>
        /// 
        /// </summary>
        public static bool OpenWithdraw
        {
            get
            {
                return SysConfig.Instance.GetValue("OpenWithdraw") == "1";
            }
        }
        #endregion

        #region ColseWithdrawReason
        /// <summary>
        /// 
        /// </summary>
        public static string ColseWithdrawReason
        {
            get
            {
                return SysConfig.Instance.GetValue("ColseWithdrawReason");
            }
        }
        #endregion

        #region DefaultScheme
        /// <summary>
        /// 
        /// </summary>
        public static int DefaultScheme
        {
            get
            {
                int scheme = 0;

                string value = SysConfig.Instance.GetValue("DefaultScheme");

                if (int.TryParse(value, out scheme))
                {

                }
                return scheme;
            }
        }
        #endregion
    }
}
