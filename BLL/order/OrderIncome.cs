using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Order
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderIncome
    {
        DAL.Order.OrderIncome dal = new DAL.Order.OrderIncome();

        public static OrderIncome Instance
        {
            get
            {
                var instance = new OrderIncome();
                return instance;
            }
        }
        /// <summary>
        /// 当天收益统计
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataSet TodayIncomeStat(int userid)
        {
            string date = DateTime.Today.ToString("yyyy-MM-dd");
            try
            {
                if (userid == -1)
                {
                    return dal.IncomeStat(date, date);
                }
                else
                    return dal.IncomeStat(userid, date, date);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public DataSet TodayIncomeStatGroupByPayType(int userid)
        {
            string date = DateTime.Today.ToString("yyyy-MM-dd");
            try
            {
                return dal.IncomeStatGroupByPayType(userid, date, date);
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
    }
}
