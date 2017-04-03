using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model.Promotion;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Promotion
{
    /// <summary>
    /// 
    /// </summary>
    public class Factory
    {
        static viviapi.DAL.Promotion.Factory dal = new viviapi.DAL.Promotion.Factory();

        #region Insert
        /// <summary>
        /// 
        /// </summary>
        /// <param name="promoter"></param>
        /// <returns></returns>
        public static int Insert(Promoter promoter)
        {
            try
            {
                return dal.Insert(promoter);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="regId"></param>
        /// <returns></returns>
        public static bool Delete(int regId)
        {
            try
            {
                return dal.Delete(regId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region  GetUserNum
        /// <summary>
        /// 应该有代理数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetUserNum(int userId)
        {
            try
            {
                return dal.GetUserNum(userId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regId"></param>
        /// <returns></returns>
        public static Promoter GetModel(int regId)
        {
            try
            {
                return dal.GetModel(regId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
    }
}

