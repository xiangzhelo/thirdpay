using DBAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace viviapi.DAL.Quota
{
    public class Quotapayrate
    {
        public string Getpayratelist(viviapi.Model.Quota.quotapayrate model)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@userid", SqlDbType.Int,4)};
            parameters[0].Value = model.Userid;
            DataSet obj = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_quotapayrate_Getpayratelist", parameters);
            //return JsonConvert.SerializeObject(obj);
            if (obj == null)
            {
                return "null";
            }
            else
            {
                return JsonConvert.SerializeObject(obj);
                //return obj.Tables[0].Rows[0].Table;
            }
        }
        public int update_selfisopen(Model.Quota.quotapayrate model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@out", SqlDbType.Int,4),
                    new SqlParameter("@quota_type", SqlDbType.Int,4),
                    new SqlParameter("@userid", SqlDbType.Int,4),
                    new SqlParameter("@isopen", SqlDbType.Int,4)
            };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.Quota_type;
            parameters[2].Value = model.Userid;
            parameters[3].Value = model.Selfisopen;
            DbHelperSQL.RunProcedure("proc_quotapayrate_update", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }

        public DataSet getpayrate(int searchuserID)
        {
            string sql = "select a.quota_type,a.name,b.sysisopen as sysisopen,b.payrate as payrate from dbo.quotatype a left join dbo.quotapayrate b on a.quota_type=b.quota_type where b.userid=" + searchuserID + " union select c.quota_type as quota_type,c.name as name,1 as sysisopen,c.default_payrate as payrate from dbo.quotatype c where c.quota_type not in(select quota_type from quotapayrate where userid=" + searchuserID + " )";
        
            DataSet ds=DataBase.ExecuteDataset(CommandType.Text, sql);
            return ds;
        }

        public int settingPayrate(Model.Quota.quotapayrate model)
        {
            int rowsAffected;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
                ,new SqlParameter("@quota_type", SqlDbType.Int, 4)
                ,new SqlParameter("@userid", SqlDbType.Int, 4)
                ,new SqlParameter("@payrate", SqlDbType.Decimal)
            };

            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.Quota_type;
            parameters[2].Value = model.Userid;
            parameters[3].Value = model.Payrate;

            DbHelperSQL.RunProcedure("proc_quotapayrate_changepayrate", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }
        public int settingSysisopen(Model.Quota.quotapayrate model)
        {
            int rowsAffected;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
                ,new SqlParameter("@quota_type", SqlDbType.Int, 4)
                ,new SqlParameter("@userid", SqlDbType.Int, 4)
                ,new SqlParameter("@isopen", SqlDbType.Int, 4)
            };

            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.Quota_type;
            parameters[2].Value = model.Userid;
            parameters[3].Value = model.Sysisopen;

            DbHelperSQL.RunProcedure("proc_quotapayrate_changesysisopen", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }
    }
}
