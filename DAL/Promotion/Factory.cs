using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model.Promotion;

namespace viviapi.DAL.Promotion
{
    /// <summary>
    /// </summary>
    public class Factory
    {
        #region Insert

        /// <summary>
        /// </summary>
        /// <param name="promoter"></param>
        /// <returns></returns>
        public int Insert(Promoter promoter)
        {
            var prams = new[]
            {
                DataBase.MakeInParam("@RegId", SqlDbType.Int, 4, promoter.RegId)
                , DataBase.MakeInParam("@PID", SqlDbType.Int, 4, promoter.PID)
                , DataBase.MakeInParam("@Prices", SqlDbType.Money, 8, promoter.Prices)
                , DataBase.MakeInParam("@PTime", SqlDbType.DateTime, 8, promoter.PromTime)
                , DataBase.MakeInParam("@PStatus", SqlDbType.Int, 4, promoter.PromStatus)
            };

            int id =
                Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_promotionUser_insert", prams));
            return 0;
        }

        #endregion

        #region Delete

        /// <summary>
        /// </summary>
        /// <param name="regId"></param>
        /// <returns></returns>
        public bool Delete(int regId)
        {
            var prams = new[]
            {
                DataBase.MakeInParam("@RegId", SqlDbType.Int, 4, regId)
            };

            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_promotionUser_delete", prams) > 0;
        }

        #endregion

        #region  GetUserNum

        /// <summary>
        ///     应该有代理数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetUserNum(int userId)
        {
            string sql = "select count(0) from PromotionUser(nolock) where PID=@PID";

            var parameters = new[] {new SqlParameter("@PID", SqlDbType.Int, 4)};
            parameters[0].Value = userId;

            return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.Text, sql, parameters));
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="regId"></param>
        /// <returns></returns>
        public Promoter GetModel(int regId)
        {
            var strSql = new StringBuilder();
            strSql.Append("select  top 1 PromId,RegId,PID,Prices from PromotionUser ");
            strSql.Append(" where RegId=@RegId ");
            SqlParameter[] parameters = {new SqlParameter("@RegId", SqlDbType.Int, 4)};
            parameters[0].Value = regId;
            var model = new Promoter();
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PromId"].ToString() != "")
                {
                    model.PromId = int.Parse(ds.Tables[0].Rows[0]["PromId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RegId"].ToString() != "")
                {
                    model.RegId = int.Parse(ds.Tables[0].Rows[0]["RegId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PID"].ToString() != "")
                {
                    model.PID = int.Parse(ds.Tables[0].Rows[0]["PID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Prices"].ToString() != "")
                {
                    model.Prices = decimal.Parse(ds.Tables[0].Rows[0]["Prices"].ToString());
                }
                return model;
            }
            return model;
        }
    }
}