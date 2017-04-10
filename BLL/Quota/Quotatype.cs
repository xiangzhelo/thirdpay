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
        public static DataSet getType()
        {
            try
            {
                return dal.getType();
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
        public static bool settingIsopen(int quota_type, int isopen)
        {
            try
            {
                return dal.settingIsopen(quota_type,isopen);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static bool settingDefaultPayrate(int quota_type, decimal payrate)
        {
            try
            {
                return dal.settingDefaultPayrate(quota_type, payrate);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

    }
}
