using System;
using System.Messaging;

namespace viviapi.MSMQMessaging {
    /// <summary>
    /// This could be a base class for all PetShop MSMQ messaging implementation as 
    /// it provides a basic implementation for sending and receving messages to and from transactional queue
    /// ����������г����MSMQ��Ϣʵ�ֵĻ��࣬��Ϊ���ṩ��һ������ʵ�ַ��ͺͽ�����Ϣ���������Զ���
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
            //��ֵָʾ���ͷ� ID �Ƿ�Ӧ������Ϣ��
            queue.DefaultPropertiesToSend.AttachSenderId = false;
            //����ǰ�Ƿ������֤��Ϣ��
            queue.DefaultPropertiesToSend.UseAuthentication = false;
            //��Ϣ��Ϊ˽�е�
            queue.DefaultPropertiesToSend.UseEncryption = false;
            //Ӧ�ó����ȷ����Ϣ������ ȷ��ϵͳ�ڹ�������д��ݵ�ȷ����Ϣ�������Լ�����Ӧ�ó��򷵻�ȷ����Ϣ��ʱ��
            queue.DefaultPropertiesToSend.AcknowledgeType = AcknowledgeTypes.None;//�������󲻷����κ�ȷ����Ϣ�������ǿ϶��Ļ��Ƿ񶨵ģ��� 
            //�Ƿ���ʼ��������ļ�����ռ��б�����Ϣ�ĸ���
            //Ҫ������Ϣ�ɹ��شӷ�����������͵���һ���󣬽���Ϣ�ĸ��������ڷ���������ļ�����ռ���ʱΪ true������Ϊ false�� Ĭ��ֵΪ false�� 
            queue.DefaultPropertiesToSend.UseJournalQueue = false;
        }

        /// <summary>
        /// Derived classes call this from their own Receive methods but cast
        /// the return value to something meaningful.
        /// 
        /// ���������������Լ��Ľ��շ�����������ֵת��Ϊ������ġ�
        /// </summary>
        public virtual object Receive() {
            try {
                using (Message message = queue.Receive(timeout, transactionType))
                    return message;
            }
            catch (MessageQueueException mqex) {
                if (mqex.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                    throw new TimeoutException();

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
