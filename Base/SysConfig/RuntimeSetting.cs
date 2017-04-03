using System;
using viviLib.Configuration;

namespace viviapi.SysConfig
{
    /// <summary>
    /// RuntimeSetting 的摘要说明。
    /// </summary>
    public sealed class RuntimeSetting
    {
        private RuntimeSetting()
        {
        }

        private static readonly string _group = "runtimeSettings";
        /// <summary>
        /// 设置组。
        /// </summary>
        public static string SettingGroup
        {
            get { return _group; }
        }
        /// <summary>
        /// 数据库对象所有者名
        /// </summary>
        public static string firstpage
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "firstpage"); }
        }
        /// <summary>
        /// css 地址
        /// </summary>
        public static string MainDomain
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "MainDomain"); }
        }

        /// <summary>
        /// 数据库对象所有者名
        /// </summary>
        public static string SqlDataUser
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "SqlDataUser"); }
        }

        /// <summary>
        /// 系统名称。
        /// </summary>
        public static string SystemName
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "SystemName"); }
        }

        /// <summary>
        /// 浏览器。
        /// </summary>
        public static string[] UserAgent
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "UserAgent").Split('|'); ;}
        }

        /// <summary>
        /// 网站关键字。
        /// </summary>
        public static string WebSiteKeywords
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "WebSiteKeywords"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ConnectString
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "ConnectString"); }
        }


        /// <summary>
        /// 
        /// </summary>
        public static string WebDAL
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "WebDAL");
            }

        }


        /// <summary>
        /// 
        /// </summary>
        public static string OrdersDAL
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "OrdersDAL");
            }

        }

        /// <summary>
        /// 订单处理策略
        /// </summary>
        public static string OrderStrategyAssembly
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "OrderStrategyAssembly");
            }

        }

        /// <summary>
        /// 订单处理策略类
        /// </summary>
        public static string OrderStrategyClass
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "OrderStrategyClass");
            }
        }


        /// <summary>
        /// 订单处理策略
        /// </summary>
        public static string OrderCardStrategyAssembly
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "OrderCardStrategyAssembly");
            }

        }

        /// <summary>
        /// 订单处理策略类
        /// </summary>
        public static string OrderCardStrategyClass
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "OrderCardStrategyClass");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static string OrderSmsStrategyAssembly
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "OrderSmsStrategyAssembly");
            }

        }

        /// <summary>
        ///
        /// </summary>
        public static string OrderSmsStrategyClass
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "OrderSmsStrategyClass");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Sitedomain
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "Sitedomain");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string GatewayUrl
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "GatewayUrl");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static int ServerId
        {
            get
            {
                try
                {
                    return int.Parse(ConfigHelper.GetConfig(SettingGroup, "ServerId"));
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int xiaoka_time_interval
        {
            get
            {
                try
                {
                    return int.Parse(ConfigHelper.GetConfig(SettingGroup, "xiaoka_time_interval"));
                }
                catch
                {
                    return 1;
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public static double DeductSafetyTime
        {
            get
            {
                try
                {
                    return double.Parse(ConfigHelper.GetConfig(SettingGroup, "DeductSafetyTime"));
                }
                catch
                {
                    return 0;
                }
            }

        }


        ///// <summary>
        ///// 每天最多可以提现次数
        ///// </summary>
        //public static int CPSDrate
        //{
        //    get
        //    {
        //        try
        //        {
        //            return int.Parse(ConfigHelper.GetConfig(SettingGroup, "CPSDrate"));
        //        }
        //        catch
        //        {
        //            return 0;
        //        }
        //    }

        //}

        /// <summary>
        /// 
        /// </summary>
        public static string SMSSN
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "SMSSN");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public static string SMSKEY
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "SMSKEY");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string ManagePagePath
        {
            get
            {
                string _path = ConfigHelper.GetConfig(SettingGroup, "ManagePagePath");
                if (_path == string.Empty)
                    return "Console";
                return _path;
            }

        }


        #region UrlManagerConfigPath
        /// <summary>
        /// URL管理配置文件的路径。
        /// </summary>
        public static string UrlManagerConfigPath
        {
            get { return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configurations\urlmanagerconfiguration.config"); }
        }
        #endregion

      

       

        public static string InternalAPIID
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "InternalAPIID"); }
        }

        public static string InternalAPIKey
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "InternalAPIKey"); }
        }

        

        public static int ModelCache
        {
            get
            {
                try
                {
                    return int.Parse(ConfigHelper.GetConfig(SettingGroup, "ModelCache"));
                }
                catch
                {
                    return 60;
                }
            }

        }
    }
}