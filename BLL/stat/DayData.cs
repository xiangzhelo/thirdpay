using viviLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DBAccess;

namespace viviapi.BLL
{   
    public class DayData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static DataTable GetDayData(DateTime beginTime, DateTime endTime, int uid)
        {
            List<SqlParameter> sqlprams = new List<SqlParameter>();
            if (uid > 0)
            {
                sqlprams.Add(DataBase.MakeInParam("@uid", SqlDbType.Int, 4, uid));
            }
            sqlprams.Add(DataBase.MakeInParam("@beginTime", SqlDbType.DateTime, 8, beginTime));
            sqlprams.Add(DataBase.MakeInParam("@endTime", SqlDbType.DateTime, 8, endTime));
            //md by vivisoft 加了订单类型控制
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "Proc_SelectDayData", sqlprams.ToArray()).Tables[0];
        }

        public static DataTable GetDayHHReginfo(DateTime begintime, DateTime endtime, int adid)
        {
            SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, begintime), DataBase.MakeInParam("@EndTime", SqlDbType.DateTime, 8, endtime), DataBase.MakeInParam("@UserId", SqlDbType.Int, 4, adid) };
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "Proc_SelectDayDataHH", prams).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static DataTable GetGMDayData(DateTime beginTime, DateTime endTime, int uid)
        {
            List<SqlParameter> sqlprams = new List<SqlParameter>();
            if (uid > 0)
            {
                sqlprams.Add(DataBase.MakeInParam("@uid", SqlDbType.Int, 4, uid));
            }
            sqlprams.Add(DataBase.MakeInParam("@beginTime", SqlDbType.DateTime, 8, beginTime));
            sqlprams.Add(DataBase.MakeInParam("@endTime", SqlDbType.DateTime, 8, endTime));
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "GM_SelectDayData", sqlprams.ToArray()).Tables[0];
        }
    }
}

