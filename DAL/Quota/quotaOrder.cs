using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace viviapi.DAL.Quota
{
    public class quotaOrder
    {
        public int placeorder(Model.Quota.quotaOrder model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@userid", SqlDbType.Int,4),
                    new SqlParameter("@quota_type", SqlDbType.Int,4),
                    new SqlParameter("@quotaValue", SqlDbType.Decimal,9),
                    new SqlParameter("@orderid", SqlDbType.VarChar,30),
                    new SqlParameter("@addtime", SqlDbType.DateTime),
                    new SqlParameter("@updatetime", SqlDbType.DateTime),
                    new SqlParameter("@year", SqlDbType.Int,4),
                    new SqlParameter("@month", SqlDbType.Int,4),
                    new SqlParameter("@clientip", SqlDbType.VarChar,20)
            };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.quota_type;
            parameters[3].Value = model.quotaValue;
            parameters[4].Value = model.orderid;
            parameters[5].Value = model.addtime;
            parameters[6].Value = model.updatetime;
            parameters[7].Value = model.year;
            parameters[8].Value = model.month;
            parameters[9].Value = model.clientip;
            DbHelperSQL.RunProcedure("proc_quotaOrder_add", parameters, out rowsAffected);
            return (int) parameters[0].Value;
        }
    }
}
