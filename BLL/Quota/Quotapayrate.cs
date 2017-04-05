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
    }
}