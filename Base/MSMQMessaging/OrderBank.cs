using System;
using System.Configuration;
using System.Messaging;
using viviapi.Model.Order;


namespace viviapi.MSMQMessaging {

    /// <summary>
    /// This class is an implementation for sending and receiving orders to and from MSMQ
    /// </summary>
    public class OrderBank : BaseQueue, viviapi.IMessaging.IOrderBank
    {
        // Path example - FormatName:DIRECT=OS:MyMachineName\Private$\OrderQueueName
        private static readonly string queuePath = viviapi.SysConfig.MSMQSetting.BankOrderPath;
        private static int queueTimeout = 20;

        public OrderBank()
            : base(queuePath, queueTimeout)
        {
            // Set the queue to use Binary formatter for smaller foot print and performance
            queue.Formatter = new BinaryMessageFormatter();
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns>All information for an order</returns>
        public new OrderBankInfo Receive()
        {
            // This method involves in distributed transaction and need Automatic Transaction type
            base.transactionType = MessageQueueTransactionType.Automatic;
            return (OrderBankInfo)((Message)base.Receive()).Body;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public OrderBankInfo Receive(int timeout)
        {
            base.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeout));
            return Receive();
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderMessage">All information for an order</param>
        public void Send(OrderBankInfo orderMessage)
        {
            // This method does not involve in distributed transaction and optimizes performance using Single type
            base.transactionType = MessageQueueTransactionType.Single;
            base.Send(orderMessage);
        }

        /// <summary>
        /// 发送完成的订单到队列        
        /// </summary>
        /// <param name="orderMessage">All information for an order</param>
        public void Complete(OrderBankInfo orderMessage)
        {
            // This method does not involve in distributed transaction and optimizes performance using Single type
            base.transactionType = MessageQueueTransactionType.Single;
            base.Send(orderMessage);
        }
    }
}
