using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviapi.Model.Order;

namespace viviapi.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderBankAsynchronous : IBLLStrategy.IOrderBankStrategy
    {
        // Get an instance of the MessagingFactory
        // Making this static will cache the Messaging instance after the initial load
        private static readonly viviapi.IMessaging.IOrderBank asynchOrder = viviapi.MessagingFactory.QueueAccess.CreateBankOrder();

        /// <summary>
        /// This method serializes the order object and send it to the queue for asynchronous processing
        /// </summary>
        /// <param name="order">All information about the order</param>
        public void Insert(OrderBankInfo order)
        {
            asynchOrder.Send(order);
        }


        /// <summary>
        /// This method serializes the order object and send it to the queue for asynchronous processing
        /// </summary>
        /// <param name="order">All information about the order</param>
        public void Complete(OrderBankInfo order)
        {
            asynchOrder.Complete(order);
        }
    }
}
