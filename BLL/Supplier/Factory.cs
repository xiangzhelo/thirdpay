using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using DBAccess;
using viviapi.Model;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Supplier
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Factory
    {

        public const string SqlTable = "supplier";
        public const string SqlTableFields = @"[id]
      ,[code]
      ,[name]
      ,[name1]
      ,[logourl]
      ,[isbank]
      ,[iscard]
      ,[issms]
      ,[issx],[isdistribution]
      ,[puserid]
      ,[puserkey]
      ,[pusername]
      ,[puserid1]
      ,[puserkey1]
      ,[puserid2]
      ,[puserkey2]
      ,[puserid3]
      ,[puserkey3]
      ,[puserid4]
      ,[puserkey4]
      ,[puserid5]
      ,[puserkey5]
      ,[purl]
      ,[pbakurl]
      ,[jumpUrl]
      ,[pcardbakurl]
      ,[postBankUrl]
      ,[postCardUrl]
      ,[postSMSUrl],[distributionUrl]
      ,[desc]
      ,[sort]
      ,[release]
      ,[issys],[timeout],[synsRetCode],[asynsRetCode] ,[limitAmount]";

        private static DAL.Supplier.Supplier Dal = new DAL.Supplier.Supplier();

        public static string CacheKey = Sys.Constant.CacheMark + "SUPPLIER_{0}";

        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static int Add(SupplierInfo model)
        {
            try
            {
                int id = Dal.Add(model);
                return id;
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
        public static bool Update(SupplierInfo model)
        {
            try
            {
                bool success = Dal.Update(model);

                if (success)
                {
                    ClearCache(model.code.Value);
                    return true;
                }
                else
                {
                    return false;
                }
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
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static SupplierInfo GetCacheModel(int code)
        {
            var model = new SupplierInfo();

            string cacheKey = string.Format(CacheKey, code);
            model = (SupplierInfo)Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

            if (model == null)
            {
                IDictionary<string, object> sqldepparms = new Dictionary<string, object>();
                sqldepparms.Add("code", code);

                SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SqlTable, SqlTableFields, "[code]=@code", sqldepparms);

                model = GetModelByCode(code);

                viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, model);
            }

            return model;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SupplierInfo GetModel(int id)
        {
            try
            {
                return Dal.GetModel(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static SupplierInfo GetModelByCode(int code)
        {
            try
            {
                return Dal.GetModelByCode(code);
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataSet GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append("select id,code,name,name1,logourl,isbank,iscard,issms,issx,puserid,puserkey,pusername,puserid1,puserkey1,puserid2,puserkey2,puserid3,puserkey3,puserid4,puserkey4,puserid5,puserkey5,purl,pbakurl,postBankUrl,postCardUrl,postSMSUrl,[desc],sort,release,issys,pcardbakurl ");
            strSql.Append(" FROM supplier ");
            strSql.Append(" Order by sort ");

            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataSet GetList(string where)
        {
            var strSql = new StringBuilder();
            strSql.Append("select id,code,name,name1,logourl,isbank,iscard,issms,issx,puserid,puserkey,pusername,puserid1,puserkey1,puserid2,puserkey2,puserid3,puserkey3,puserid4,puserkey4,puserid5,puserkey5,purl,pbakurl,postBankUrl,postCardUrl,postSMSUrl,[desc],sort,release,issys,pcardbakurl ");
            strSql.Append(" FROM supplier ");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.AppendFormat(" where {0}", where);
            }
            strSql.Append(" Order by sort ");


            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
        }
        #endregion

        #region PageSearch
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            try
            {
                return Dal.PageSearch(searchParams, pageSize, page, orderby);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region GetSupplierName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetSupplierName(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return string.Empty;

            int id = Convert.ToInt32(obj);

            if (id <= 0)
                return "0";

            string suppName = id.ToString(CultureInfo.InvariantCulture);

            switch (id)
            {
                case 51:
                    suppName = "51支付卡";
                    break;
                case 70:
                    suppName = "70卡";
                    break;
                case 80:
                    suppName = "欧飞";
                    break;
                case 85:
                    suppName = "汇元";
                    break;
                case 851:
                    suppName = "汇速";
                    break;
                case 100:
                    suppName = "财付通";
                    break;
                case 101:
                    suppName = "支付宝";
                    break;
                case 102:
                    suppName = "易宝";
                    break;
                case 600:
                    suppName = "环迅";
                    break;
                case 1001:
                    suppName = "国付宝";
                    break;
                case 1003:
                    suppName = "宝付";
                    break;
                case 1005:
                    suppName = "贝付";
                    break;
                default:
                    var suppInfo = GetModelByCode(id);
                    if (suppInfo != null)
                    {
                        suppName = suppInfo.name;
                    }
                    break;
            }

            return suppName;
        }
        #endregion

        //清理缓存 
        static void ClearCache(int code)
        {
            string cacheKey = string.Format(CacheKey, code);
            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cacheKey);
        }
    }
}
