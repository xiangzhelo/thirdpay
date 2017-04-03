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
    public class RegisterSettings
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
                return SysConfig.Instance.GetList("type=1").Tables[0];
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
        #endregion

        #region RegisterOpen
        /// <summary>
        /// 是否开启注册
        /// </summary>
        public static bool RegisterOpen
        {
            get
            {
                return SysConfig.Instance.GetValue("RegisterOpen") == "1";
            }
        }
        #endregion

        #region RequiredAudit
        /// <summary>
        /// 商户注册是否审核
        /// </summary>
        public static bool RequiredAudit
        {
            get
            {
                return SysConfig.Instance.GetValue("RequiredAudit") == "1";
            }
        }
        #endregion

        #region AllowUserloginByEmail
        /// <summary>
        /// 是否允许通过邮件登录
        /// </summary>
        public static bool AllowUserloginByEmail
        {
            get
            {
                return SysConfig.Instance.GetValue("AllowUserloginByEmail") == "1";
            }
        }
        #endregion

        #region AllowUserloginByPhone
        /// <summary>
        /// 是否允许通过手机登录
        /// </summary>
        public static bool AllowUserloginByPhone
        {
            get
            {
                return SysConfig.Instance.GetValue("AllowUserloginByPhone") == "1";
            }
        }
        #endregion

        #region ActivationByEmail
        /// <summary>
        /// 注册是否需要发送激活邮件
        /// </summary>
        public static bool ActivationByEmail
        {
            get
            {
                return SysConfig.Instance.GetValue("ActivationByEmail") == "1";
            }
        }
        #endregion

        #region LoginMsgForlock
        /// <summary>
        /// 被锁定账户的登录提示信息
        /// </summary>
        public static string LoginMsgForlock
        {
            get
            {
                return SysConfig.Instance.GetValue("LoginMsgForlock");
            }
        }
        #endregion

        #region LoginMsgForUnCheck
        /// <summary>
        /// 未审核账户的登录提示信息
        /// </summary>
        public static string LoginMsgForUnCheck
        {
            get
            {
                return SysConfig.Instance.GetValue("LoginMsgForUnCheck");
            }
        }
        #endregion

        #region LoginMsgForCheckfail
        /// <summary>
        /// 账户审核失败的登录提示信息
        /// </summary>
        public static string LoginMsgForCheckfail
        {
            get
            {
                return SysConfig.Instance.GetValue("LoginMsgForCheckfail");
            }
        }
        #endregion

        #region PhoneAuthenticate
        /// <summary>
        /// 需要手机验证
        /// </summary>
        public static bool PhoneAuthenticate
        {
            get
            {
                return SysConfig.Instance.GetValue("PhoneAuthenticate") == "1";
            }
        }
        #endregion 

        #region SmsMaxSendTimes
        /// <summary>
        /// 手机最多最大发送信息次数
        /// </summary>
        public static int SmsMaxSendTimes
        {
            get
            {
                int count = 1;
                string value = SysConfig.Instance.GetValue("SmsMaxSendTimes");
                if (!string.IsNullOrEmpty(value))
                {
                    if (!int.TryParse(value, out count))
                    {
                        count = 1;
                    }
                }
                return count;
            }
        }
        #endregion

        #region DefaultUserLevel
        /// <summary>
        /// 
        /// </summary>
        public static int DefaultUserLevel
        {
            get
            {
                int level = 1;
                string value = SysConfig.Instance.GetValue("DefaultUserLevel");
                if (!string.IsNullOrEmpty(value))
                {
                    if (!int.TryParse(value, out level))
                    {
                        level = 1;
                    }
                }
                return level;
            }
        }
        #endregion

        #region DefaultCPSDrate
        /// <summary>
        /// 
        /// </summary>
        public static int DefaultCPSDrate
        {
            get
            {
                int count = 1;
                string value = SysConfig.Instance.GetValue("DefaultCPSDrate");
                if (!string.IsNullOrEmpty(value))
                {
                    if (!int.TryParse(value, out count))
                    {
                        count = 1;
                    }
                }
                return count;
            }
        }
        #endregion

        #region DefaultCardVersion
        /// <summary>
        /// 点卡版本
        /// </summary>
        public static byte DefaultCardVersion
        {
            get
            {
                byte cardVersion = 1;
                string val = SysConfig.Instance.GetValue("DefaultCardVersion");

                if (!string.IsNullOrEmpty(val))
                {
                    byte.TryParse(val, out cardVersion);
                }

                return cardVersion;

            }
        }
        #endregion
    }
}
