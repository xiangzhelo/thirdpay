using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace viviapi.BLL
{
    /// <summary>
    /// This is a synchronous implementation of IOrderStrategy 
    /// By implementing IOrderStrategy interface, the developer can add a new order insert strategy without re-compiling the whole BLL 
    /// </summary>
    public class OrderCardSynchronous : IBLLStrategy.IOrderCardStrategy
    {
        private static readonly IDAL.IOrderCard dal = DALFactory.DataAccess.CreateOrderCard();

        /// <summary>
        /// Inserts the order and updates the inventory stock within a transaction.
        /// </summary>
        /// <param name="order">All information about the order</param>
        public void Insert(Model.Order.OrderCardInfo order)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                dal.Insert(order);

                // Update the inventory to reflect the current inventory after the order submission
                //Inventory inventory = new Inventory();
                //inventory.TakeStock(order.LineItems);

                // Calling Complete commits the transaction.
                // Excluding this call by the end of TransactionScope's scope will rollback the transaction
                ts.Complete();
            }
        }


        /// <summary>
        /// Inserts the order and updates the inventory stock within a transaction.
        /// </summary>
        /// <param name="order">All information about the order</param>
        public void InsertItem(Model.Order.CardItemInfo order)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                dal.InsertItem(order);

                // Update the inventory to reflect the current inventory after the order submission
                //Inventory inventory = new Inventory();
                //inventory.TakeStock(order.LineItems);

                // Calling Complete commits the transaction.
                // Excluding this call by the end of TransactionScope's scope will rollback the transaction
                ts.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public void Complete(Model.Order.OrderCardInfo order)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                dal.Complete(order);

                // Update the inventory to reflect the current inventory after the order submission
                //Inventory inventory = new Inventory();
                //inventory.TakeStock(order.LineItems);

                // Calling Complete commits the transaction.
                // Excluding this call by the end of TransactionScope's scope will rollback the transaction
                ts.Complete();
            }
            //OrderNotify notify = new OrderNotify();
            //notify.DoNotify(order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool ItemComplete(Model.Order.CardItemInfo order, out bool allCompleted, out string opstate, out string ovalue, out decimal ototalvalue)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                allCompleted = false;
                opstate = string.Empty;
                ovalue = string.Empty;
                ototalvalue = 0M;

                dal.ItemComplete(order, out allCompleted, out opstate, out ovalue, out ototalvalue);

                // Update the inventory to reflect the current inventory after the order submission
                //Inventory inventory = new Inventory();
                //inventory.TakeStock(order.LineItems);

                // Calling Complete commits the transaction.
                // Excluding this call by the end of TransactionScope's scope will rollback the transaction
                ts.Complete();
                return true;
            }
            //OrderNotify notify = new OrderNotify();
            //notify.DoNotify(order);
        }
    }
}
