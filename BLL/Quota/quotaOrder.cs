using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
