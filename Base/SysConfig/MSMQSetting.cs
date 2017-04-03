using System;
using viviLib.Configuration;

namespace viviapi.SysConfig
{
    /// <summary>
    /// RuntimeSetting 的摘要说明。
    /// </summary>
    public sealed class MSMQSetting
    {
        private MSMQSetting()
        {
        }

        private static readonly string _group = "MSMQ";
        /// <summary>
        /// 设置组。
        /// </summary>
        public static string SettingGroup
        {
            get { return _group; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string OrderMessaging
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "OrderMessaging");
            }

        }

        /// <summary>
        /// 网银订单队列
        /// </summary>
        public static string BankOrderPath
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "BankOrderPath"); }
        }

        /// <summary>
        /// 网银订单结果异步通知队列
        /// </summary>
        public static string BankNotifyPath
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "BankNotifyPath"); }
        }

        /// <summary>
        /// 卡类订单队列
        /// </summary>
        public static string CardOrderPath
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "CardOrderPath"); }
        }

        /// <summary>
        /// 卡类订单结果异步通知队列
        /// </summary>
        public static string CardNotifyPath
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "CardNotifyPath"); }
        }

        
        /// <summary>
        /// viviapi.MSMQMessaging.CardNotifyPathX
        /// </summary>
        public static string CardNotifyPathX
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "CardNotifyPathX"); }
        }

        /// <summary>
        /// 短信订单队列
        /// </summary>
        public static string SmsOrderPath
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "SmsOrderPath"); }
        }

        /// <summary>
        /// 短信订单结果异步通知队列
        /// </summary>
        public static string SmsNotifyPath
        {
            get { return ConfigHelper.GetConfig(SettingGroup, "SmsNotifyPath"); }
        }
       

        //事件处理花费时间
        public static int TransactionTimeout
        {
            get
            {
                int _transactionTimeout = 30;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "TransactionTimeout"), out _transactionTimeout))
                {
                    _transactionTimeout = 30;
                }
                return _transactionTimeout;
            }
        }


        //队列超时时间 队列等待多久没有接收到数据视为超时
        public static int QueueTimeout
        {
            get
            {
                int _queueTimeout = 20;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "QueueTimeout"), out _queueTimeout))
                {
                    _queueTimeout = 20;
                }
                return _queueTimeout;
            }
        }

        //批处理 每次处理多少条订单
        public static int BatchSize
        {
            get
            {
                int _BatchSize = 10;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "BatchSize"), out _BatchSize))
                {
                    _BatchSize = 10;
                }
                return _BatchSize;
            }
        }

        //线程条数 多线程多数 
        public static int ThreadCount
        {
            get
            {
                int _ThreadCount = 2;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "ThreadCount"), out _ThreadCount))
                {
                    _ThreadCount = 2;
                }
                return _ThreadCount;
            }
        }

        //事件处理花费时间
        public static int NotifyTransactionTimeout
        {
            get
            {
                int _transactionTimeout = 30;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "NotifyTransactionTimeout"), out _transactionTimeout))
                {
                    _transactionTimeout = 30;
                }
                return _transactionTimeout;
            }
        }

        //队列超时时间 队列等待多久没有接收到数据视为超时
        public static int NotifyQueueTimeout
        {
            get
            {
                int _queueTimeout = 20;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "NotifyQueueTimeout"), out _queueTimeout))
                {
                    _queueTimeout = 20;
                }
                return _queueTimeout;
            }
        }

        //批处理 每次处理多少条订单
        public static int NotifyBatchSize
        {
            get
            {
                int _BatchSize = 10;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "NotifyBatchSize"), out _BatchSize))
                {
                    _BatchSize = 10;
                }
                return _BatchSize;
            }
        }

        //线程条数 多线程多数 
        public static int NotifyThreadCount
        {
            get
            {
                int _ThreadCount = 2;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "NotifyThreadCount"), out _ThreadCount))
                {
                    _ThreadCount = 2;
                }
                return _ThreadCount;
            }
        }

        #region 
        //事件处理花费时间
        public static int TransactionTimeout_CardOrder
        {
            get
            {
                int _transactionTimeout = 30;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "TransactionTimeout_CardOrder"), out _transactionTimeout))
                {
                    _transactionTimeout = 30;
                }
                return _transactionTimeout;
            }
        }


        //队列超时时间 队列等待多久没有接收到数据视为超时
        public static int QueueTimeout_CardOrder
        {
            get
            {
                int _queueTimeout = 20;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "QueueTimeout_CardOrder"), out _queueTimeout))
                {
                    _queueTimeout = 20;
                }
                return _queueTimeout;
            }
        }

        //批处理 每次处理多少条订单
        public static int BatchSize_CardOrder
        {
            get
            {
                int _BatchSize = 10;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "BatchSize_CardOrder"), out _BatchSize))
                {
                    _BatchSize = 10;
                }
                return _BatchSize;
            }
        }

        //线程条数 多线程多数 
        public static int ThreadCount_CardOrder
        {
            get
            {
                int _ThreadCount = 2;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "ThreadCount_CardOrder"), out _ThreadCount))
                {
                    _ThreadCount = 2;
                }
                return _ThreadCount;
            }
        }
        #endregion



        //事件处理花费时间
        public static int NotifyTransactionTimeout_Card
        {
            get
            {
                int _transactionTimeout = 30;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "NotifyTransactionTimeout_Card"), out _transactionTimeout))
                {
                    _transactionTimeout = 30;
                }
                return _transactionTimeout;
            }
        }

        //队列超时时间 队列等待多久没有接收到数据视为超时
        public static int NotifyQueueTimeout_Card
        {
            get
            {
                int _queueTimeout = 20;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "NotifyQueueTimeout_Card"), out _queueTimeout))
                {
                    _queueTimeout = 20;
                }
                return _queueTimeout;
            }
        }

        //批处理 每次处理多少条订单
        public static int NotifyBatchSize_Card
        {
            get
            {
                int _BatchSize = 10;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "NotifyBatchSize_Card"), out _BatchSize))
                {
                    _BatchSize = 10;
                }
                return _BatchSize;
            }
        }

        //线程条数 多线程多数 
        public static int NotifyThreadCount_Card
        {
            get
            {
                int _ThreadCount = 2;
                if (!int.TryParse(ConfigHelper.GetConfig(SettingGroup, "NotifyThreadCount_Card"), out _ThreadCount))
                {
                    _ThreadCount = 2;
                }
                return _ThreadCount;
            }
        }

        //public static int NotifyTransactionTimeout = int.Parse(ConfigHelper.GetConfig(SettingGroup, "NotifyTransactionTimeout"));
        //public static int NotifyQueueTimeout = int.Parse(ConfigHelper.GetConfig(SettingGroup, "NotifyQueueTimeout"));
        //public static int NotifyBatchSize = int.Parse(ConfigHelper.GetConfig(SettingGroup, "NotifyBatchSize"));
        //public static int NotifyThreadCount = int.Parse(ConfigHelper.GetConfig(SettingGroup, "NotifyThreadCount"));
    }
}