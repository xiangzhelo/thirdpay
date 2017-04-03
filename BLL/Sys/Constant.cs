using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.BLL.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Constant
    {
        /// <summary>
        /// 用于参数认证
        /// </summary>
        public readonly static string ParameterEncryptionKey = "{142C339F-01C5-4df6-A6B1-AEF2C9C77CDA}-{325BB0C1-1A4B-4072-BCEF-4784B5E089BC}-{DE4EB920-7850-4b2d-AA32-6DC76364B12A}";

        public readonly static string MailParameterEncryptionKey = "{0660AFB4-7D42-412c-99F6-79BE06A26760}";

        public readonly static string ManageGOTOUserAdminKey = "{CCA9C379-F668-4667-813A-47046FB56009}";

        public static readonly string RealNameAuthenticationSessionKey = "{{2BF23DC9-A16A-4fd0-B639-10F48474968D}}_{0}";

        public static readonly string PhoneVerificationCacheKey = "{{F359C1C3-8EB9-48d8-9ADD-0165076A0B3A}}_{0}";

        public static readonly string EmailRegCodeCacheKey = "{{F359C1C3-8EB9-48d8-9ADD-0165076A0B5C}}_{0}";

        //成功标志
        //public readonly static string OrderNotifySuccessFlag = "opstate=0";

        /// <summary>
        /// 服务器缓存标志
        /// </summary>
        public static string CacheMark
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CacheMark"];
            }
        }

        /// <summary>
        /// 常用对象缓存时间
        /// </summary>
        public static int ModelCache
        {
            get
            {
                int modelCache = 10;

                string config = System.Configuration.ConfigurationManager.AppSettings["ModelCache"];

                if (!string.IsNullOrEmpty(config))
                {
                    int.TryParse(config, out modelCache);
                }

                return modelCache;
            }
        }
    }
}
