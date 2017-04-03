using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace viviapi.MessagingFactory
{
    public sealed class QueueAccess
    {
        // Look up the Messaging implementation we should be using
        private static readonly string path = viviapi.SysConfig.MSMQSetting.OrderMessaging;// ConfigurationManager.AppSettings["OrderMessaging"];

        private QueueAccess() { }

        /// <summary>
        /// 在线支付
        /// </summary>
        /// <returns></returns>
        public static viviapi.IMessaging.IOrderBank CreateBankOrder()
        {
            string className = path + ".OrderBank";
            return (viviapi.IMessaging.IOrderBank)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static viviapi.IMessaging.IOrderCard CreateCardOrder()
        {
            string className = path + ".OrderCard";
            return (viviapi.IMessaging.IOrderCard)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static viviapi.IMessaging.IOrderSms CreateSmsOrder()
        {
            string className = path + ".OrderSms";
            return (viviapi.IMessaging.IOrderSms)Assembly.Load(path).CreateInstance(className);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static viviapi.IMessaging.IOrderBankNotify OrderBankNotify()
        {
            string className = path + ".OrderBankNotify";
            return (viviapi.IMessaging.IOrderBankNotify)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static viviapi.IMessaging.IOrderCardNotify OrderCardNotify()
        {
            string className = path + ".OrderCardNotify";
            return (viviapi.IMessaging.IOrderCardNotify)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public static viviapi.IMessaging.IOrderCardNotifyX OrderCardNotifyX()
        {
            
            string className = path + ".OrderCardNotifyX";
            return (viviapi.IMessaging.IOrderCardNotifyX)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static viviapi.IMessaging.IOrderSmsNotify OrderSmsNotify()
        {
            string className = path + ".OrderSmsNotify";
            return (viviapi.IMessaging.IOrderSmsNotify)Assembly.Load(path).CreateInstance(className);
        }
    }
}
