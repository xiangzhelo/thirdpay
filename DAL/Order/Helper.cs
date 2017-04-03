using System;
using System.Data;
using System.Data.SqlClient;
using DBAccess;

namespace viviapi.DAL.Order
{
    public class Helper
    {
        /// <summary>
        ///     8 非法用户
        ///     13 系统繁忙
        ///     14 不存在该笔订单
        ///     0 支付成功
        ///     1 数据接收成功
        ///     11 支付成功,实际面值 {0}元
        ///     10 充值卡无效
        /// </summary>
        /// <param name="o_userid"></param>
        /// <param name="userorderid"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public int search_check(int o_userid, string userorderid, out DataRow row)
        {
            row = null;
            int check_result = 99;


            SqlParameter[] parameters =
            {
                new SqlParameter("@o_userid", SqlDbType.Int, 4)
                , new SqlParameter("@userorderid_str", SqlDbType.VarChar, 30)
                , new SqlParameter("@result", SqlDbType.TinyInt)
            };
            parameters[0].Value = o_userid;
            parameters[1].Value = userorderid;
            parameters[2].Direction = ParameterDirection.Output;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_order_search_chk", parameters);
            check_result = Convert.ToInt32(parameters[2].Value);

            if (check_result == 0)
                row = ds.Tables[0].Rows[0];


            return check_result;
        }
    }
}