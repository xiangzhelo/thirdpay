using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model.Finance;
using viviapi.Model.Settled;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Finance
{
    /// <summary>
    /// 提现方案操作类
    /// </summary>
    public class TocashScheme
    {
        private static DAL.Finance.TocashScheme dal = new DAL.Finance.TocashScheme();

        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static int Add(TocashSchemeInfo model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #region Update
        /// <summary>
        ///  更新一条数据
        /// </summary>
        public static bool Update(TocashSchemeInfo model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int id)
        {
            try
            {
                return dal.Delete(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region GetModel
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static TocashSchemeInfo GetModelByUser(int type,int userId)
        {
            try
            {
                return dal.GetModelByUser(type, userId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static TocashSchemeInfo GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region GetList
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            try
            {
                return dal.GetList(strWhere);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion
    }
}
