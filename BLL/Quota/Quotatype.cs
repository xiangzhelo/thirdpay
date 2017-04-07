using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Quota
{
    public class Quotatype
    {
        static viviapi.DAL.Quota.QuotaType dal = new viviapi.DAL.Quota.QuotaType();
        public static DataSet getByuserid(int userid)
        {
            try
            {
                return dal.getByuserid(userid);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
    }
}
