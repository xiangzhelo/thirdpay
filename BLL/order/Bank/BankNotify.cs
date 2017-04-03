using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Order.Bank
{
    /// <summary>
    /// 
    /// </summary>
    public class BankNotify
    {
        viviapi.DAL.Order.Bank.BankNotify dal = new DAL.Order.Bank.BankNotify();

        public static BankNotify Instance
        {
            get
            {
                var notify = new BankNotify();
                return notify;
            }
        }

        public byte Exists(string orderid)
        {
            try
            {
                return dal.Exists(orderid);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 3;
            }
        }

        public bool Insert(Model.Order.Bank.BankNotify model)
        {
            try
            {
                return dal.Insert(model);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public void UpdateInQueueStatus(string orderid)
        {
            try
            {
                dal.UpdateInQueueStatus(orderid);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        public DataTable GetNotifyFailList()
        {
            try
            {
                return dal.GetNotifyFailList();
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            try
            {
                return dal.PageSearch(searchParams, pageSize, page, orderby);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public string GetNotifyStatViewText(object notifystat)
        {
            if (notifystat == DBNull.Value)
                return "";

            byte status = Convert.ToByte(notifystat);
            switch (status)
            {
                case 1:
                    return "处理中";
                case 2:
                    return "下发成功";
                case 4:
                    return "下发失败";
            }

            return status.ToString(CultureInfo.InvariantCulture);

        }

        
    }
}
