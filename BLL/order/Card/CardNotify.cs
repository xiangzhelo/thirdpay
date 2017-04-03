using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Order.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class CardNotify
    {
        viviapi.DAL.Order.Card.CardNotify dal = new DAL.Order.Card.CardNotify();

        public static CardNotify Instance
        {
            get
            {
                var notify = new CardNotify();
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

        public bool Insert(Model.Order.Card.CardNotify model)
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
