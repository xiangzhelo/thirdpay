using System;
using System.Configuration;
using System.Messaging;
using viviapi.Model.Order;
using viviapi.Model.Order.Card;


namespace viviapi.MSMQMessaging {

    /// <summary>
    /// This class is an implementation for sending and receiving orders to and from MSMQ
    /// </summary>
    public class OrderCard : BaseQueue, viviapi.IMessaging.IOrderCard
    {
        // Path example - FormatName:DIRECT=OS:MyMachineName\Private$\OrderQueueName
        private static readonly string queuePath = viviapi.SysConfig.MSMQSetting.CardOrderPath;
        private static int queueTimeout = 20;

        public OrderCard()
            : base(queuePath, queueTimeout)
        {
            // Set the queue to use Binary formatter for smaller foot print and performance
            queue.Formatter = new BinaryMessageFormatter();
        }

        /// <summary>
        /// Method to retrieve order messages from Pet Shop Message Queue
        /// 为了从宠物店消息队列中检索消息的方法
        /// </summary>
        /// <returns>All information for an order</returns>
        public new Object Receive()
        {
            // This method involves in distributed transaction and need Automatic Transaction type
            base.transactionType = MessageQueueTransactionType.Automatic;
            return (Object)((Message)base.Receive()).Body;
        }

        public Object Receive(int timeout)
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

        public void SendNotify(CardNotify orderMessage)
        {
            // This method does not involve in distributed transaction and optimizes performance using Single type
            base.transactionType = MessageQueueTransactionType.Single;
            base.Send(orderMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderMessage">All information for an order</param>
        public void SendItem(CardItemInfo orderMessage)
        {
            // This method does not involve in distributed transaction and optimizes performance using Single type
            base.transactionType = MessageQueueTransactionType.Single;
            base.Send(orderMessage);
        }

        /// <summary>
        /// 发送完成的订单到队列        
        /// </summary>
        /// <param name="orderMessage">All information for an order</param>
        public void Complete(OrderCardInfo orderMessage)
        {
            // This method does not involve in distributed transaction and optimizes performance using Single type
            base.transactionType = MessageQueueTransactionType.Single;
            base.Send(orderMessage);
        }

        public void ItemComplete(CardItemInfo orderMessage)
        {
            base.transactionType = MessageQueueTransactionType.Single;
            base.Send(orderMessage);
        }
    
    }
}
