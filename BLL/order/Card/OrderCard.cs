using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviapi.SysConfig;
using viviapi.IDAL;
using viviapi.Model.Order;
using viviLib.Security;
using DBAccess;
using System.Transactions;
using viviLib.Data;
using viviapi.Model.User;

namespace viviapi.BLL
{
    /// <summary>
    /// 点卡订单处理类
    /// </summary>
    public class OrderCard
    {
        public OrderCard()
        {

        }

        private static readonly IBLLStrategy.IOrderCardStrategy orderInsertStrategy = LoadInsertStrategy();
        private static readonly IMessaging.IOrderCard orderQueue = MessagingFactory.QueueAccess.CreateCardOrder();


        // Get an instance of the Order DAL using the DALFactory
        // Making this static will cache the DAL instance after the initial load
        private static readonly IOrderCard dal = DALFactory.DataAccess.CreateOrderCard();
       

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GenerateUniqueOrderId(string prefix, int typeId)
        {
            string typefactor = "00";
            if (typeId.ToString(CultureInfo.InvariantCulture).Length == 3)
                typefactor = typeId.ToString(CultureInfo.InvariantCulture).Substring(1);

            Random random = new Random(Guid.NewGuid().GetHashCode());
            string orderid = prefix + DateTime.Now.ToString("yyMMddHHmmssff") + typefactor + random.Next(1000).ToString("0000");
            if (Cache.WebCache.GetCacheService().RetrieveObject(orderid) != null)
            {
                return GenerateUniqueOrderId(prefix, typeId);
            }
            Cache.WebCache.GetCacheService().AddObject(orderid, orderid);
            return orderid;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public void Insert(OrderCardInfo order)
        {
            // Insert the order (a)synchrounously based on configuration
            orderInsertStrategy.Insert(order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public void InsertItem(CardItemInfo order)
        {
            // Insert the order (a)synchrounously based on configuration
            orderInsertStrategy.InsertItem(order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public void Complete(OrderCardInfo order)
        {
            // Insert the order (a)synchrounously based on configuration
            orderInsertStrategy.Complete(order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public void ItemComplete(CardItemInfo order, out bool allCompleted, out string opstate, out string ovalue, out decimal ototalvalue)
        {
            // Insert the order (a)synchrounously based on configuration
            orderInsertStrategy.ItemComplete(order, out allCompleted, out opstate, out ovalue, out ototalvalue);
        }

        /// <summary>
        /// A method to read an order from the system
        /// </summary>
        /// <param name="orderId">Unique identifier for an order</param>
        /// <returns>All the information about the order</returns>
        public OrderCardInfo GetModelByOrderId(string orderId)
        {
            // Validate input
            if (string.IsNullOrEmpty(orderId))
                return null;

            // Return the order from the DAL
            return dal.GetModelByOrderId(orderId);
        }

        /// <summary>
        /// A method to read an order from the system
        /// </summary>
        /// <param name="orderId">Unique identifier for an order</param>
        /// <returns>All the information about the order</returns>
        public CardItemInfo GetItemModel(string orderId, int serial)
        {
            // Validate input
            if (string.IsNullOrEmpty(orderId))
                return null;

            // Return the order from the DAL
            return dal.GetItemModel(orderId, serial);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId">Unique identifier for an order</param>
        /// <returns>All the information about the order</returns>
        public DataTable DataItemsByOrderId(string orderId)
        {
            // Validate input
            if (string.IsNullOrEmpty(orderId))
                return null;

            // Return the order from the DAL
            return dal.DataItemsByOrderId(orderId);
        }


        /// <summary>
        /// A method to read an order from the system
        /// </summary>
        /// <param name="orderId">Unique identifier for an order</param>
        /// <returns>All the information about the order</returns>
        public OrderCardInfo GetModel(long id)
        {
            // Validate input
            if (id <= 0L)
                return null;
            try
            {
                // Return the order from the DAL
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// A method to read an order from the system
        /// </summary>
        /// <param name="orderId">Unique identifier for an order</param>
        /// <returns>All the information about the order</returns>
        public OrderCardInfo GetModel(long id, int userid)
        {
            // Validate input
            if (id <= 0L)
                return null;
            try
            {
                // Return the order from the DAL
                return dal.GetModel(id, userid);
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
                return null;
            }
        }


        /// <summary>
        /// A method to read an order from the system
        /// </summary>
        /// <param name="orderId">Unique identifier for an order</param>
        /// <returns>All the information about the order</returns>
        public bool UpdateNotifyInfo(OrderCardInfo order)
        {
            if (order == null)
                return false;

            // Return the order from the DAL
            return dal.Notify(order);
        }

        /// <summary>
        /// This method determines which Order Insert Strategy to use based on user's configuration.
        /// </summary>
        /// <returns>An instance of PetShop.IBLLStrategy.IOrderStrategy</returns>
        private static IBLLStrategy.IOrderCardStrategy LoadInsertStrategy()
        {
            // Look up which strategy to use from config file
            string path = RuntimeSetting.OrderCardStrategyAssembly;
            //ConfigurationManager.AppSettings["OrderStrategyAssembly"];
            string className = RuntimeSetting.OrderCardStrategyClass;
            // ConfigurationManager.AppSettings["OrderStrategyClass"];

            // Using the evidence given in the config file load the appropriate assembly and class
            return (IBLLStrategy.IOrderCardStrategy)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// Method to process asynchronous order from the queue
        /// </summary>
        public Object ReceiveFromQueue(int timeout)
        {
            return orderQueue.Receive(timeout);
        }

       

        //public string GetCallBackUrl(OrderCardInfo orderinfo)
        //{
        //    //return SystemApiHelper.GetCardBackUrl(orderinfo);//orderinfo.notifyurl + "?" + BuilderParms(orderinfo, orderinfo.notifyurl);
        //}

        


    }
}
