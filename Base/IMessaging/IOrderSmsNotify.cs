using System;
using viviapi.Model.Order;
using System.Collections.Generic;
using System.Text;

namespace viviapi.IMessaging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderSmsNotify
    {
        /// <summary>
        /// Method to retrieve order information from a messaging queue
        /// </summary>
        /// <returns>All information about an order</returns>
        OrderSmsInfo Receive();

        /// <summary>
        /// Method to retrieve order information from a messaging queue
        /// </summary>
        /// <returns>All information about an order</returns>
        OrderSmsInfo Receive(int timeout);

        /// <summary>
        /// Method to send an order to a message queue for later processing
        /// </summary>
        /// <param name="body">All information about an order</param>
        void Send(OrderSmsInfo orderMessage);
    }
}
