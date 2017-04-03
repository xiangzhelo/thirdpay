using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Transactions;
using viviapi.Model.Order;
using viviapi.Model.Order.Card;
using viviLib.Data;
using viviLib.ExceptionHandling;
using viviLib.Utils;

namespace viviapi.BLL.Order.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class Factory
    {
        viviapi.DAL.Order.Card.Factory dal = new DAL.Order.Card.Factory();

        public static Factory Instance
        {
            get
            {
                var bank = new Factory();
                return bank;
            }
        }


        public string GenerateOrderId(string prefix)
        {
            return "C" + Common.GuidToLongID().ToString(CultureInfo.InvariantCulture);
        }


        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, bool isstat)
        {
            try
            {
                return dal.PageSearch(searchParams, pageSize, page, orderby, isstat);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public OrderCardInfo GetModelByOrderId(string orderId)
        {
            try
            {
                return dal.GetModelByOrderId(orderId);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public OrderCardInfo GetModel(long id, int userid)
        {
            try
            {
                return dal.GetModel(id, userid);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public OrderCardInfo GetModel(long id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        #region ResetState
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public bool ResetState(string orderid)
        {
            try
            {
                return dal.ResetState(orderid);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="suppid"></param>
        /// <param name="refervalue"></param>
        /// <returns></returns>
        public bool ResetState(string orderid, int suppid, decimal refervalue)
        {
            try
            {
                return dal.ResetState(orderid, suppid, refervalue);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 扣单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool Deduct(string orderId)
        {
            bool result = false;
            // Validate input
            if (string.IsNullOrEmpty(orderId))
                return result;

            try
            {
                result = dal.Deduct(orderId);
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
                result = false;
            }
            return result;
        }

        /// <summary>
        ///还单
        /// </summary>
        /// <param name="orderId">Unique identifier for an order</param>
        /// <returns>All the information about the order</returns>
        public bool ReDeduct(string orderId)
        {
            bool result = false;

            if (string.IsNullOrEmpty(orderId))
                return result;

            try
            {
                result = dal.ReDeduct(orderId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                result = false;
            }
            return result;
        }

        public CheckAPIParameter CheckCardInfo(int userid
            , string userorderid
            , int cardtype
            , string cardno
            , string cardpwd
            , int orderAmt)
        {
            try
            {
                return dal.CheckCardInfo(userid, userorderid, cardtype, cardno, cardpwd, orderAmt);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public bool ChkIsCanNotify(string orderid)
        {
            try
            {
                return dal.ChkIsCanNotify(orderid);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataRow CallbackInsert(string orderid, int supplierId, int status, string errCode, string errMsg,
            byte continueSubmit)
        {
            try
            {
                return dal.CallbackInsert(orderid, supplierId, status, errCode, errMsg, continueSubmit);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetTimeoutRetrunOrders(DateTime sdt, DateTime edt)
        {
            try
            {
                return dal.GetTimeoutRetrunOrders(sdt, edt);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetTimeoutRetrunOrders2(DateTime sdt, DateTime edt)
        {
            try
            {
                return dal.GetTimeoutRetrunOrders2(sdt, edt);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetToggleInterfaceList(DateTime sdt, DateTime edt)
        {
            try
            {
                return dal.GetToggleInterfaceList(sdt, edt);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetViewStatusName(object status)
        {
            if (status == DBNull.Value)
                return string.Empty;
            //viviapi.Model.Order.OrderStatusEnum _stat = (viviapi.Model.Order.OrderStatusEnum)Convert.ToInt32(status);
            if (Convert.ToInt32(status) == 8)
                return "失败";
            else
                return Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum), status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetViewSuccessAmt(object status, object amt)
        {
            if (status == DBNull.Value || amt == DBNull.Value)
                return "0";

            //viviapi.Model.Order.OrderStatusEnum _stat = (viviapi.Model.Order.OrderStatusEnum)Convert.ToInt32(status);
            if (Convert.ToInt32(status) == 2)
                return decimal.Round(Convert.ToDecimal(amt), 2).ToString();
            else
                return "0";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="reconciledResult"></param>
        /// <param name="reconciledAmt"></param>
        /// <returns></returns>
        public bool Reconcilie(string orderid, byte reconciledResult, string reconciledAmt)
        {
            try
            {
                return dal.Reconcilie(orderid, reconciledResult, reconciledAmt);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OrderCardInfo SystemHandleOrder(OrderCardInfo model)
        {
            try
            {
                OrderCardInfo result = null;

                string cacheKey = "SystemHandles" + model.orderid;

                object objModel = Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

                if (objModel == null)
                {
                    using (var ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        dal.SystemHandleOrder(model);
                        ts.Complete();
                    }

                    result = GetModelByOrderId(model.orderid);

                    Cache.WebCache.GetCacheService().AddObject(cacheKey, result, 5);
                }
                else
                {
                    result = objModel as OrderCardInfo;
                }
                return result;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public int search_check(int o_userid, string userorderid, out DataRow row)
        {
            row = null;
            try
            {
                return dal.search_check(o_userid, userorderid,out row);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public DataSet GetlistBybatno(string batno)
        {
            try
            {
                return dal.GetlistBybatno(batno);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

    }
}
