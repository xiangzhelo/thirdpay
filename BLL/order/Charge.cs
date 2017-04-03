using System;
using System.Collections.Generic;
using System.Text;
using MongoDB;
using viviapi.Model.Order;
using System.Web;
using viviLib.Web;

namespace viviapi.BLL.Order
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Charge
    {
        public static readonly string connectionString = viviapi.SysConfig.MongoDBSetting.Connstring;
        public static readonly string databaseDefault = viviapi.SysConfig.MongoDBSetting.DefaultDB;
        public static readonly string collectionNameDefault = viviapi.SysConfig.MongoDBSetting.CollectionName;

        #region MongoDB
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="returnurl"></param>
        /// <returns></returns>
        public static bool Add(string collectionName, Model.Order.OrderBankInfo order)
        {
            try
            {
                using (Mongo server = new Mongo(connectionString))
                {
                    MongoDatabase db = server.GetDatabase(databaseDefault) as MongoDatabase;

                    MongoCollection<Document> coll = db.GetCollection<Document>(collectionName) as MongoCollection<Document>;
                    server.Connect();

                    Document charge = new Document();
                    charge["userid"] = order.userid.ToString();
                    charge["orderid"] = order.orderid;
                    charge["userorder"] = order.userorder;
                    charge["returnurl"] = order.returnurl;
                    charge["attach"] = order.attach;

                    coll.Insert(charge, true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="returnurl"></param>
        /// <returns></returns>
        public static Model.Order.OrderBankInfo Get(string collectionName, string orderId)
        {
            Model.Order.OrderBankInfo order = new viviapi.Model.Order.OrderBankInfo();
            try
            {
                using (Mongo server = new Mongo(connectionString))
                {
                    MongoDatabase db = server.GetDatabase(databaseDefault) as MongoDatabase;

                    MongoCollection<Document> coll = db.GetCollection<Document>(collectionName) as MongoCollection<Document>;
                    server.Connect();

                    Document selector = new Document() { { "orderId", orderId } };

                    Document findresult = coll.FindOne(selector);
                    order.userid = Convert.ToInt32(findresult["userid"]);
                    order.orderid = Convert.ToString(findresult["orderid"]);
                    order.userorder = Convert.ToString(findresult["userorder"]);
                    order.returnurl = Convert.ToString(findresult["returnurl"]);
                    order.attach = Convert.ToString(findresult["attach"]);

                    coll.Remove(selector, true);
                }
                return order;
            }
            catch
            {

                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="returnurl"></param>
        /// <returns></returns>
        public static bool Delete(string collectionName, string orderId)
        {
            try
            {
                using (Mongo server = new Mongo(connectionString))
                {
                    MongoDatabase db = server.GetDatabase(databaseDefault) as MongoDatabase;
                    MongoCollection<Document> coll = db.GetCollection<Document>(collectionName) as MongoCollection<Document>;
                    server.Connect();

                    coll.Remove(new Document() { { "orderId", orderId } }, true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateInfo"></param>
        public static void NotifyCheckStatus(Object stateInfo)
        {
            OrderNotify notify = (OrderNotify)stateInfo;
            string notifyUrl = GetCallBackUrl(notify.orderInfo);
            if (string.IsNullOrEmpty(notifyUrl))
            {
                notify.tmr.Dispose();
                notify.tmr = null;
            }
            else
            {
                string callback = WebClientHelper.GetString(notifyUrl, string.Empty, "GET", System.Text.Encoding.GetEncoding("GB2312"));
                if (callback.ToLower() == "ok" || notify.orderInfo.notifycount == 10)
                {
                    notify.tmr.Dispose();
                    notify.tmr = null;
                }
                if (notify.orderInfo.notifycount == 0 || callback.ToLower() == "ok")
                {
                    notify.orderInfo.notifycount++;
                    notify.orderInfo.notifystat = callback.ToLower() == "ok" ? 2 : 4;
                    notify.orderInfo.againNotifyUrl = notifyUrl;
                    notify.orderInfo.notifycontext = callback;

                    OrderBankFactory.NotifyResult(notify.orderInfo);                    
                }

                (notify.tmr).Change(TimeSpan.FromSeconds(10 * notify.orderInfo.notifycount), TimeSpan.FromSeconds(10 * notify.orderInfo.notifycount));
            }
        }


        public static string GetCallBackUrl(OrderBankInfo orderinfo)
        {
            string result = orderinfo.status == 2 ? "0" : "1";
            if (orderinfo == null || string.IsNullOrEmpty(orderinfo.notifyurl))
                return string.Empty;

            string userKey = BLL.User.UserFactory.GetUsersKey(orderinfo.userid);

            string md5Str = string.Format("result={0}&userid={1}&orderid={2}&money={3}key={4}"
                               , result
                               , orderinfo.userid
                               , orderinfo.userorder
                               , decimal.Round(orderinfo.realvalue.Value, 2)
                               , userKey);

            string sign = viviLib.Security.Cryptography.MD5(md5Str);

            string parms = string.Format("result={0}&userid={1}&orderid={2}&money={3}&ext={4}&sign={5}",
                  result
                , orderinfo.userid
                , orderinfo.userorder
                , decimal.Round(orderinfo.realvalue.Value, 2)
                , System.Web.HttpUtility.UrlEncode(orderinfo.attach, System.Text.Encoding.GetEncoding("GBK"))
                , sign);

            string callBackUrl = orderinfo.notifyurl + "?" + parms;

            return callBackUrl;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderinfo"></param>
        /// <param name="result"></param>
        public static void BankOrderReturn(OrderBankInfo orderinfo, string result, HttpContext context)
        {
            if (orderinfo == null || string.IsNullOrEmpty(orderinfo.returnurl))
                return;

            string userKey = BLL.User.UserFactory.GetUsersKey(orderinfo.userid);

            string md5Str = string.Format("result={0}&userid={1}&orderid={2}&money={3}key={4}"
                               , result
                               , orderinfo.userid
                               , orderinfo.userorder
                               , decimal.Round(orderinfo.realvalue.Value, 2)
                               , userKey);

            string sign = viviLib.Security.Cryptography.MD5(md5Str);

            string parms = string.Format("result={0}&userid={1}&orderid={2}&money={3}&ext={4}&sign={5}",
                  result
                , orderinfo.userid
                , orderinfo.userorder
                , decimal.Round(orderinfo.realvalue.Value, 2)
                , System.Web.HttpUtility.UrlEncode(orderinfo.attach, System.Text.Encoding.GetEncoding("GBK"))
                , sign);

            string returnurl = orderinfo.returnurl + "?"+parms;
            
            if (context != null)
            {
                context.Response.Redirect(returnurl);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderinfo"></param>
        /// <param name="result"></param>
        public static void BankOrderNotify(OrderBankInfo orderinfo, string result, HttpContext context)
        {
            
        }
    }
}
