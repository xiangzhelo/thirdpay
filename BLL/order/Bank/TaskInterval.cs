using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Transactions;
using viviapi.Model.Order;
using viviapi.SysConfig;


namespace viviapi.BLL.Order.Bank
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskInterval : viviLib.ScheduledTask.IScheduledTaskExecute
    {
        private static int notifyTransactionTimeout = MSMQSetting.NotifyTransactionTimeout;
        private static int notifyqueueTimeout = MSMQSetting.NotifyQueueTimeout;
        private static int notifybatchSize = MSMQSetting.NotifyBatchSize; 
        private static int notifythreadCount = MSMQSetting.NotifyThreadCount; 

        /// <summary>
        /// 执行
        /// </summary>
        public void Execute()
        {
            //处理通知事件
            ProcessNotify();
        }

        #region ProcessNotify
        /// <summary>
        /// 记录在队列中失败列表 重新补发
        /// </summary>
        private static void ProcessNotify()
        {
            BLL.OrderBankNotify notifyBLL = new viviapi.BLL.OrderBankNotify();
         
            for (int j = 0; j < notifybatchSize; j++)
            {
                try
                {
                    OrderBankInfo _order = notifyBLL.ReceiveFromQueue(notifyqueueTimeout);
                    _order.notifycount = 0;

                    OrderNotify notifyInfo = new OrderNotify();
                    notifyInfo.orderInfo = _order;

                    Timer Tmr = new Timer(notifyBLL.NotifyCheckStatus, notifyInfo, 0, 1000);
                    notifyInfo.tmr = Tmr;
                }
                catch 
                {
                    
                }
            }
        }
        #endregion

        #region ProcessNotify
        /// <summary>
        /// 记录在队列中失败列表 重新补发
        /// </summary>
        /*private static void ProcessNotify()
        {
            BLL.OrderBankNotify notifyBLL = new viviapi.BLL.OrderBankNotify();

            TimeSpan tsTimeout = TimeSpan.FromSeconds(Convert.ToDouble(notifyTransactionTimeout * notifybatchSize));
            TimeSpan datetimeStarting = new TimeSpan(DateTime.Now.Ticks);
            double elapsedTime = 0;
            int processedItems = 0;

            ArrayList queueOrders = new ArrayList();
            for (int j = 0; j < notifybatchSize; j++)
            {
                try
                {
                    if ((elapsedTime + notifyqueueTimeout + notifyTransactionTimeout) < tsTimeout.TotalSeconds)
                    {
                        queueOrders.Add(notifyBLL.ReceiveFromQueue(notifyqueueTimeout));
                    }
                    else
                    {
                        j = notifybatchSize;
                    }
                    elapsedTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds - datetimeStarting.TotalSeconds;
                }
                catch (System.TimeoutException)
                {
                    j = notifybatchSize;
                }
            }
            for (int k = 0; k < queueOrders.Count; k++)
            {
                OrderBankInfo _order = (OrderBankInfo)queueOrders[k];
                _order.notifycount = 0;

                OrderNotify notifyInfo = new OrderNotify();
                notifyInfo.orderInfo = _order;

                Timer Tmr = new Timer(notifyBLL.NotifyCheckStatus, notifyInfo, 0, 1000);
                notifyInfo.tmr = Tmr;
                processedItems++;
            }
        }*/
        #endregion
        
    }
}
