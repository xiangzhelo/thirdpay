using System;
using System.Collections.Generic;
using System.Text;
using viviapi.Model.Order;

namespace viviapi.IMessaging
{
    public interface IOrderSms
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderMessage"></param>
        void Complete(OrderSmsInfo orderMessage);
    }
}
