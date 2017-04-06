using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Quota
{
    public class quotaOrder
    {
        static viviapi.DAL.Quota.quotaOrder dal = new viviapi.DAL.Quota.quotaOrder();

        public static int Placeorder(viviapi.Model.Quota.quotaOrder model)
        {
            try
            {
                return dal.placeorder(model);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }
        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby) {
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

        public static DataSet getOrder(List<SearchParam> searchParams)
        {
            try
            {
                return dal.getOrder(searchParams);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
    }
}
