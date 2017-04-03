using System;
using System.Collections.Generic;
using System.Text;
using viviapi.Model.Order;

namespace viviapi.IMessaging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderCard
    {
        /// <summary>
        /// Method to retrieve order information from a messaging queue
        /// </summary>
        /// <returns>All information about an order</returns>
        Object Receive();

        /// <summary>
        /// Method to retrieve order information from a messaging queue
        /// </summary>
        /// <returns>All information about an order</returns>
        Object Receive(int timeout);

        /// <summary>
        /// Method to send an order to a message queue for later processing
        /// </summary>
        /// <param name="body">All information about an order</param>
        void Send(OrderCardInfo orderMessage);

        /// <summary>
        /// Method to send an order to a message queue for later processing
        /// </summary>
        /// <param name="body">All information about an order</param>
        void SendItem(CardItemInfo orderMessage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderMessage"></param>
        void Complete(OrderCardInfo orderMessage);

        void ItemComplete(CardItemInfo orderMessage);
    }
}
