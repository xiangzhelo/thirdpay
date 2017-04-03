using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using DBAccess;
using viviLib;
using viviLib.ExceptionHandling;
using viviapi.Model;


namespace viviapi.BLL.Payment
{
    /// <summary>
    /// 支付通道类别
    /// 2012-02-17
    /// </summary>
    public class PaymodeType
    {
        internal static string PAYMODETYPE_CACHEKEY = Sys.Constant.CacheMark + "{{E493762A-6010-4c74-818D-657F8EE0BD13}}";
        internal static string SQL_TABLE = "paymodetype";
        internal static string SQL_TABLE_FIELD = "id,type,modetypename,payrateid,isOpen,addtime,sort,release";


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataTable GetList(bool iscache)
        {
            try
            {
                string cacheKey = PAYMODETYPE_CACHEKEY;

                DataSet ds = new DataSet();
             
                if(iscache)
                ds = (DataSet)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

                if (ds == null || !iscache)
                {
                    SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_TABLE, SQL_TABLE_FIELD, "", null);

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select id,type,modetypename,payrateid,isOpen,addtime,sort,release ");
                    strSql.Append(" FROM paymodetype Where release = 1 order by sort");

                    ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());

                    if(iscache)
                        viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, ds);
                }

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

    }
}
