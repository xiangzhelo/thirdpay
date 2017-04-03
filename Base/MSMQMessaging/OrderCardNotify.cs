using System;
using System.Configuration;
using System.Messaging;
using viviapi.Model.Order;

namespace viviapi.MSMQMessaging
{
    /// <summary>
    /// 异步通知队列
    /// </summary>
    public class OrderCardNotify : BaseQueue, viviapi.IMessaging.IOrderCardNotify
    {
        private static readonly string queuePath = viviapi.SysConfig.MSMQSetting.CardNotifyPath;
        private static int queueTimeout = 20;

        public OrderCardNotify()
            : base(queuePath, queueTimeout)
        {
            // Set the queue to use Binary formatter for smaller foot print and performance
            queue.Formatter = new BinaryMessageFormatter();
        }

        /// <summary>
        /// Method to retrieve order messages from Pet Shop Message Queue        
        /// </summary>
        /// <returns>All information for an order</returns>
        public new OrderCardInfo Receive()
        {
            // This method involves in distributed transaction and need Automatic Transaction type
            base.transactionType = MessageQueueTransactionType.Automatic;
            return (OrderCardInfo)((Message)base.Receive()).Body;
        }

        public OrderCardInfo Receive(int timeout)
        {
            base.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeout));
            return Receive();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderMessage">All information for an order</param>
        public void Send(OrderCardInfo orderMessage)
        {
            // This method does not involve in distributed transaction and optimizes performance using Single type
            base.transactionType = MessageQueueTransactionType.Single;
            base.Send(orderMessage);
        }
    }
}
