using System;
using System.Collections.Generic;
using System.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Quota
{
    public partial class Quotapayrate
    {
        
        static viviapi.DAL.Quota.Quotapayrate dal = new viviapi.DAL.Quota.Quotapayrate();

        public static string Getpayratelist(viviapi.Model.Quota.quotapayrate model)
        {
            try
            {
                return dal.Getpayratelist(model);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return "null";
            }
        }
        public static int update_selfisopen(viviapi.Model.Quota.quotapayrate model)
        {
            try
            {
                return dal.update_selfisopen(model);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public static DataSet getpayrate(int searchuserID)
        {
            try
            {
                return dal.getpayrate(searchuserID);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static int settingPayrate(Model.Quota.quotapayrate model)
        {
            try
            {
                return dal.settingPayrate(model);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }
        public static int settingSysisopen(Model.Quota.quotapayrate model)
        {
            try
            {
                return dal.settingSysisopen(model);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }
    }
}