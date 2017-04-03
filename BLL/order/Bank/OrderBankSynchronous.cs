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
    public class OrderBankSynchronous : IBLLStrategy.IOrderBankStrategy
    {
        private static readonly IDAL.IOrderBank dal = DALFactory.DataAccess.CreateOrderBank();

        /// <summary>
        /// Inserts the order and updates the inventory stock within a transaction.
        /// </summary>
        /// <param name="order">All information about the order</param>
        public void Insert(Model.Order.OrderBankInfo order)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                long id = dal.Insert(order);
                if (id <= 0L)
                {
                    new ApplicationException("Add orders fails");
                }

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
        public void Complete(Model.Order.OrderBankInfo order)
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
        }
    }
}
