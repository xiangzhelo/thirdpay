using viviLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model;
using viviLib.ExceptionHandling;
using DBAccess;
using viviapi.Model.Sys;
using viviLib.Data;

namespace viviapi.BLL.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Debuglog
    {
        static viviapi.DAL.Sys.Debuglog dal = new viviapi.DAL.Sys.Debuglog();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Insert(viviapi.Model.Sys.debuginfo model)
        {
            try
            {
                return dal.Insert(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
           
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static debuginfo GetModel(int id)
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


        /// <summary>
        /// 根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            try
            {
                return dal.PageSearch(searchParams, pageSize, page, orderby);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

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

        public static bool DeleteList(string idlist)
        {
            try
            {
                return dal.DeleteList(idlist);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
    }
}
