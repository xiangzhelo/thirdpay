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


namespace viviapi.BLL.Order.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskInterval : viviLib.ScheduledTask.IScheduledTaskExecute
    {
        private static int notifyTransactionTimeout = MSMQSetting.NotifyTransactionTimeout_Card;
        private static int notifyqueueTimeout = MSMQSetting.NotifyQueueTimeout_Card;
        private static int notifybatchSize = MSMQSetting.NotifyBatchSize_Card; 
        private static int notifythreadCount = MSMQSetting.NotifyThreadCount_Card; 
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
        /// 
        /// </summary>
        private static void ProcessNotify()
        {
            TimeSpan tsTimeout = TimeSpan.FromSeconds(Convert.ToDouble(notifyTransactionTimeout * notifybatchSize));
            BLL.OrderCardNotify notifyBLL = new viviapi.BLL.OrderCardNotify();

            ArrayList queueOrders = new ArrayList();
            for (int j = 0; j < notifybatchSize; j++)
            {
                try
                {
                    OrderCardInfo _order = notifyBLL.ReceiveFromQueue(notifyqueueTimeout);
                    _order.notifycount = 0;

                    viviapi.Model.Order.OrderCardNotify notifyInfo = new viviapi.Model.Order.OrderCardNotify();
                    
                    notifyInfo.orderInfo = _order;
                    Timer Tmr = new Timer(notifyBLL.NotifyCheckStatus, notifyInfo, 0, 1000);
                    notifyInfo.tmr = Tmr;
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
        #endregion

        #region ProcessNotify
        /// <summary>
        /// 
        /// </summary>
        /*private static void ProcessNotify()
        {
            TimeSpan tsTimeout = TimeSpan.FromSeconds(Convert.ToDouble(notifyTransactionTimeout * notifybatchSize));
            BLL.OrderCardNotify notifyBLL = new viviapi.BLL.OrderCardNotify();

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

            //process the queued orders
            for (int k = 0; k < queueOrders.Count; k++)
            {
                OrderCardInfo _order = (OrderCardInfo)queueOrders[k];

                viviapi.Model.Order.OrderCardNotify notifyInfo = new viviapi.Model.Order.OrderCardNotify();
                notifyInfo.orderInfo = _order;

                Timer Tmr = new Timer(notifyBLL.NotifyCheckStatus, notifyInfo, 0, 1000);
                notifyInfo.tmr = Tmr;
                processedItems++;
            }
        }*/
        #endregion
        
    }
}
