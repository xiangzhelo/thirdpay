using System;
using System.Configuration;
using System.Messaging;
using viviapi.Model.Order;

namespace viviapi.MSMQMessaging
{
    /// <summary>
    /// 异步通知队列
    /// </summary>
    public class OrderSmsNotify : BaseQueue, viviapi.IMessaging.IOrderSmsNotify
    {
        private static readonly string queuePath = viviapi.SysConfig.MSMQSetting.SmsNotifyPath;
        private static int queueTimeout = 20;

        public OrderSmsNotify()
            : base(queuePath, queueTimeout)
        {
            // Set the queue to use Binary formatter for smaller foot print and performance
            queue.Formatter = new BinaryMessageFormatter();
        }

        /// <summary>
        /// Method to retrieve order messages from Pet Shop Message Queue        
        /// </summary>
        /// <returns>All information for an order</returns>
        public new OrderSmsInfo Receive()
        {
            // This method involves in distributed transaction and need Automatic Transaction type
            base.transactionType = MessageQueueTransactionType.Automatic;
            return (OrderSmsInfo)((Message)base.Receive()).Body;
        }

        public OrderSmsInfo Receive(int timeout)
        {
            base.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeout));
            return Receive();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderMessage">All information for an order</param>
        public void Send(OrderSmsInfo orderMessage)
        {
            // This method does not involve in distributed transaction and optimizes performance using Single type
            base.transactionType = MessageQueueTransactionType.Single;
            base.Send(orderMessage);
        }
    }
}
