using System;
using System.Reflection;

namespace viviapi.DALFactory
{
    /// <summary>
    /// 
    /// </summary>
    public class DataAccess
    {
        private static readonly string path      = viviapi.SysConfig.RuntimeSetting.WebDAL;  //ConfigurationManager.AppSettings["WebDAL"];
        private static readonly string orderPath = viviapi.SysConfig.RuntimeSetting.OrdersDAL; //ConfigurationManager.AppSettings["OrdersDAL"];

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static viviapi.IDAL.IOrderBank CreateOrderBank()
        {
            string className = path + ".OrderBank";
            return (viviapi.IDAL.IOrderBank)Assembly.Load(orderPath).CreateInstance(className);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static viviapi.IDAL.IOrderCard CreateOrderCard()
        {
            string className = path + ".OrderCard";
            return (viviapi.IDAL.IOrderCard)Assembly.Load(orderPath).CreateInstance(className);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static viviapi.IDAL.IOrderSms CreateOrderSms()
        {
            string className = path + ".OrderSms";
            return (viviapi.IDAL.IOrderSms)Assembly.Load(orderPath).CreateInstance(className);
        }
    }
}
