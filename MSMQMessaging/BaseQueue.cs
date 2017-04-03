using System;
using System.Messaging;

namespace viviapi.MSMQMessaging {
    /// <summary>
    /// This could be a base class for all PetShop MSMQ messaging implementation as 
    /// it provides a basic implementation for sending and receving messages to and from transactional queue
    /// 这可能是所有宠物店MSMQ消息实现的基类，因为它提供了一个基本实现发送和接收消息，从事务性队列
    /// </summary>
    public class BaseQueue : IDisposable {

        protected MessageQueueTransactionType transactionType = MessageQueueTransactionType.Automatic;
        protected MessageQueue queue;
        protected TimeSpan timeout;

        public BaseQueue(string queuePath, int timeoutSeconds)
        {
            queue = new MessageQueue(queuePath);
            timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeoutSeconds));

            // Performance optimization since we don't need these features
            //该值指示发送方 ID 是否应附在消息中
            queue.DefaultPropertiesToSend.AttachSenderId = false;
            //发送前是否必须验证消息。
            queue.DefaultPropertiesToSend.UseAuthentication = false;
            //消息成为私有的
            queue.DefaultPropertiesToSend.UseEncryption = false;
            //应用程序的确认消息的类型 确定系统在管理队列中传递的确认消息的类型以及向发送应用程序返回确认消息的时间
            queue.DefaultPropertiesToSend.AcknowledgeType = AcknowledgeTypes.None;//用于请求不发送任何确认消息（无论是肯定的还是否定的）。 
            //是否在始发计算机的计算机日记中保留消息的副本
            //要求在消息成功地从发件计算机传送到下一步后，将消息的副本保存在发件计算机的计算机日记中时为 true，否则为 false。 默认值为 false。 
            queue.DefaultPropertiesToSend.UseJournalQueue = false;
        }

        /// <summary>
        /// Derived classes call this from their own Receive methods but cast
        /// the return value to something meaningful.
        /// 
        /// 派生类调用这个从自己的接收方法，但返回值转换为有意义的。
        /// </summary>
        public virtual object Receive() {
            try {
                using (Message message = queue.Receive(timeout, transactionType))
                    return message;
            }
            catch (MessageQueueException mqex) {
                if (mqex.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                {
                    return null;
                }
                //throw new TimeoutException();

                throw;
            }
        }

        /// <summary>
        /// Derived classes may call this from their own Send methods that
        /// accept meaningful objects.
        /// </summary>
        public virtual void Send(object msg) {
            queue.Send(msg, transactionType);
        }

        #region IDisposable Members
        public void Dispose() {
            queue.Dispose();
        }
        #endregion
    }
}
