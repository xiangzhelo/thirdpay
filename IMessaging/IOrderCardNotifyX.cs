using System;
using viviapi.Model.Order;
using System.Collections.Generic;
using System.Text;
using viviapi.Model.Order.Card;

namespace viviapi.IMessaging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderCardNotifyX
    {
        /// <summary>
        /// Method to retrieve order information from a messaging queue
        /// </summary>
        /// <returns>All information about an order</returns>
        CardNotify Receive();

        /// <summary>
        /// Method to retrieve order information from a messaging queue
        /// </summary>
        /// <returns>All information about an order</returns>
        CardNotify Receive(int timeout);

        /// <summary>
        /// Method to send an order to a message queue for later processing
        /// </summary>
        /// <param name="body">All information about an order</param>
        void Send(CardNotify orderMessage);
    }
}
