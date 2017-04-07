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
    }
}
