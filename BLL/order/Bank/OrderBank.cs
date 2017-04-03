using System;
using System.Data;
using System.Web;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviLib.Security;
using viviapi.SysConfig;
using viviapi.IDAL;
using viviapi.Model.Order;
using DBAccess;
using System.Transactions;
using viviLib.Data;

namespace viviapi.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderBank
    {
        private static readonly IBLLStrategy.IOrderBankStrategy  OrderInsertStrategy = LoadInsertStrategy();
        private static readonly IMessaging.IOrderBank OrderQueue = MessagingFactory.QueueAccess.CreateBankOrder();
       

        // Get an instance of the Order DAL using the DALFactory
        // Making this static will cache the DAL instance after the initial load
        private static readonly IOrderBank Dal = DALFactory.DataAccess.CreateOrderBank();

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public void Insert(OrderBankInfo order)
        {
            // Insert the order (a)synchrounously based on configuration
            OrderInsertStrategy.Insert(order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public void Complete(OrderBankInfo order)
        {
            // Insert the order (a)synchrounously based on configuration
            OrderInsertStrategy.Complete(order);
        }

        /// <summary>
        /// This method determines which Order Insert Strategy to use based on user's configuration.
        /// </summary>
        /// <returns>An instance of PetShop.IBLLStrategy.IOrderStrategy</returns>
        private static IBLLStrategy.IOrderBankStrategy LoadInsertStrategy()
        {
            // Look up which strategy to use from config file
            string path = RuntimeSetting.OrderStrategyAssembly;
            //ConfigurationManager.AppSettings["OrderStrategyAssembly"];
            string className = RuntimeSetting.OrderStrategyClass;
            // ConfigurationManager.AppSettings["OrderStrategyClass"];

            // Using the evidence given in the config file load the appropriate assembly and class
            return (IBLLStrategy.IOrderBankStrategy)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// Method to process asynchronous order from the queue
        /// </summary>
        public OrderBankInfo ReceiveFromQueue(int timeout)
        {
            return OrderQueue.Receive(timeout);
        }
    }
}
