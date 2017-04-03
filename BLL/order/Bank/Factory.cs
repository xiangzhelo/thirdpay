using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Transactions;
using viviapi.Model.Common;
using viviapi.Model.Order;
using viviLib.Data;
using viviLib.ExceptionHandling;
using viviLib.Utils;

namespace viviapi.BLL.Order.Bank
{
    public class Factory
    {
        readonly DAL.Order.Bank.Factory _dal = new DAL.Order.Bank.Factory();

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
            return "B" + Common.GuidToLongID().ToString(CultureInfo.InvariantCulture);
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, bool isstat)
        {
            try
            {
                return _dal.PageSearch(searchParams, pageSize, page, orderby, isstat);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public OrderBankInfo GetModelByOrderId(string orderId)
        {
            try
            {
                return _dal.GetModelByOrderId(orderId);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public OrderBankInfo GetModel(long id, int userid)
        {
            try
            {
                return _dal.GetModel(id, userid);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public OrderBankInfo GetModel(long id)
        {
            try
            {
                return _dal.GetModel(id);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        /// <summary>
        /// 扣单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool Deduct(string orderId)
        {
            bool result = false;

            if (string.IsNullOrEmpty(orderId))
                return false;

            try
            {
                result = _dal.Deduct(orderId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 还单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool ReDeduct(string orderId)
        {
            bool result = false;
            if (string.IsNullOrEmpty(orderId))
                return false;

            try
            {
                result = _dal.ReDeduct(orderId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                result = false;
            }
            return result;
        }



        public FunExecResult CheckApiParms(int intputUserid
            , int typeid
            , bool ischeckuserorder
            , string intputUserorder)
        {
            try
            {
                return _dal.CheckAPIParms(intputUserid,typeid, ischeckuserorder, intputUserorder);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
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

        public string GetStatusView(int status)
        {
            switch (status)
            {
                case 1:
                    return "处理中";
                    break;
                case 2:
                    return "已完成";
                    break;
                case 4:
                    return "失败";
                    break;
            }
            return "";
        }

        //对账
        public bool Reconcilie(string orderid, byte reconciledResult, decimal reconciledAmt)
        {
            try
            {
                return _dal.Reconcilie(orderid, reconciledResult, reconciledAmt);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public int search_check(int o_userid, string userorderid, out DataRow row)
        {
            row = null;
            try
            {
                return _dal.search_check(o_userid, userorderid, out row);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

    }
}
