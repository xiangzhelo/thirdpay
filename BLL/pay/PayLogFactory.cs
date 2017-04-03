using System;
using System.Data;
using System.Data.SqlClient;
using viviLib;
using DBAccess;

namespace viviapi.BLL
{
    public class PayLogFactory
    {
        public static int Add(int uid, decimal prices, decimal money)
        {
            SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, uid), DataBase.MakeInParam("@AdsType", SqlDbType.TinyInt, 1, 2), DataBase.MakeInParam("@AdsPrices", SqlDbType.Decimal, 8, prices), DataBase.MakeInParam("@PayMoney", SqlDbType.Decimal, 8, money), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, DateTime.Now) };
            if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "PayLog_ADD", prams) != 1)
            {
                return 0;
            }
            return 1;
        }
    }
}

