using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace viviapi.DAL.Quota
{
    public class QuotaType
    {
        public DataSet getByuserid(int userid)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@userid", SqlDbType.Int,4)
            };
            parameters[0].Value = userid;
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_quotatype_getbyuserid", parameters);
            return ds;
        }
        public DataSet getType()
        {
            SqlParameter[] parameters = {};
            string sql = "select * from dbo.quotatype";
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, sql, parameters);
            return ds;
        }
        public bool settingIsopen(int quota_type,int isopen)
        {
            string sqlText = "update [quotatype] set isopen=@isopen where quota_type=@quota_type";

            SqlParameter[] prams =
            {
                DataBase.MakeInParam("@isopen", SqlDbType.Int, 4, isopen)
                , DataBase.MakeInParam("@quota_type", SqlDbType.Int, 4, quota_type)
            };

            return DataBase.ExecuteNonQuery(CommandType.Text, sqlText, prams) > 0;
        }
        public bool settingDefaultPayrate(int quota_type, decimal payrate)
        {
            string sqlText = "update [quotatype] set default_payrate=@payrate where quota_type=@quota_type";

            SqlParameter[] prams =
            {
                DataBase.MakeInParam("@payrate", SqlDbType.Decimal, 9, payrate)
                , DataBase.MakeInParam("@quota_type", SqlDbType.Int, 4, quota_type)
            };

            return DataBase.ExecuteNonQuery(CommandType.Text, sqlText, prams) > 0;
        }
    }
}
