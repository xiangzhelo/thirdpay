using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBAccess;

namespace viviapi.BLL.Stat
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderReport
    {
        /// <summary>
        /// 按用户统计
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static DataTable ReportByUser(DateTime beginTime, DateTime endTime, int uid)
        {
            List<SqlParameter> sqlprams = new List<SqlParameter>();
            if (uid > 0)
            {
                sqlprams.Add(DataBase.MakeInParam("@userid", SqlDbType.Int, 4, uid));
            }
            sqlprams.Add(DataBase.MakeInParam("@beginTime", SqlDbType.DateTime, 8, beginTime));
            sqlprams.Add(DataBase.MakeInParam("@endTime", SqlDbType.DateTime, 8, endTime));            

            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "Proc_Stat_ReportByUser", sqlprams.ToArray()).Tables[0];
        }

    }
}
