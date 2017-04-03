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
    public class OrderCardAsynchronous : IBLLStrategy.IOrderCardStrategy
    {
        // Get an instance of the MessagingFactory
        // Making this static will cache the Messaging instance after the initial load
        private static readonly viviapi.IMessaging.IOrderCard asynchOrder = viviapi.MessagingFactory.QueueAccess.CreateCardOrder();

        /// <summary>
        /// This method serializes the order object and send it to the queue for asynchronous processing
        /// </summary>
        /// <param name="order">All information about the order</param>
        public void Insert(OrderCardInfo order)
        {
            asynchOrder.Send(order);
        }

        /// <summary>
        /// This method serializes the order object and send it to the queue for asynchronous processing
        /// </summary>
        /// <param name="order">All information about the order</param>
        public void InsertItem(CardItemInfo order)
        {
            asynchOrder.SendItem(order);
        }


        /// <summary>
        /// This method serializes the order object and send it to the queue for asynchronous processing
        /// </summary>
        /// <param name="order">All information about the order</param>
        public void Complete(OrderCardInfo order)
        {
            asynchOrder.Complete(order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="allCompleted"></param>
        /// <param name="opstate"></param>
        /// <param name="ovalue"></param>
        /// <param name="ototalvalue"></param>
        public bool ItemComplete(CardItemInfo order, out bool allCompleted, out string opstate, out string ovalue, out decimal ototalvalue)
        {
            allCompleted = false;
            opstate = string.Empty;
            ovalue = string.Empty;
            ototalvalue = 0M;

            asynchOrder.ItemComplete(order);

            return true;

        }
    }
}
