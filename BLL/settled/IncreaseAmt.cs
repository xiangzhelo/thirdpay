using viviLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model;
using viviLib.ExceptionHandling;
using DBAccess;
using viviapi.Model.Settled;
using viviLib.Data;

namespace viviapi.BLL.Settled
{
    /// <summary>
    /// 加款
    /// </summary>
    public sealed class IncreaseAmt
    {
        internal const string SQL_TABLE = "V_increaseAmt";
        //money,pay,nopay,Integral
        internal const string SQL_TABLE_FIELDS = @"[id]
      ,[userId]
      ,[increaseAmt]
      ,[addtime]
      ,[mangeId]
      ,[mangeName]
      ,[status]
      ,[desc],[username],[optype]";

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(viviapi.Model.Settled.IncreaseAmtInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@increaseAmt", SqlDbType.Decimal,9),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@mangeId", SqlDbType.Int,4),
					new SqlParameter("@mangeName", SqlDbType.NVarChar,50),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@desc", SqlDbType.NVarChar,100),
                                            new SqlParameter("@optype", SqlDbType.TinyInt)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.userId;
                parameters[2].Value = model.increaseAmt;
                parameters[3].Value = model.addtime;
                parameters[4].Value = model.mangeId;
                parameters[5].Value = model.mangeName;
                parameters[6].Value = model.status;
                parameters[7].Value = model.desc;
                parameters[8].Value = (int)model.optype;

                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_increaseAmt_Insert", parameters) > 0)
                {
                    return (int)parameters[0].Value;
                }
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
           
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static IncreaseAmtInfo GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
            parameters[0].Value = id;

            IncreaseAmtInfo model = new IncreaseAmtInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "Proc_increaseAmt_GetModel", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userId"].ToString() != "")
                {
                    model.userId = int.Parse(ds.Tables[0].Rows[0]["userId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["increaseAmt"].ToString() != "")
                {
                    model.increaseAmt = decimal.Parse(ds.Tables[0].Rows[0]["increaseAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["mangeId"].ToString() != "")
                {
                    model.mangeId = int.Parse(ds.Tables[0].Rows[0]["mangeId"].ToString());
                }
                model.mangeName = ds.Tables[0].Rows[0]["mangeName"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                model.desc = ds.Tables[0].Rows[0]["desc"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "userid asc,id desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string userSearchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, userSearchWhere, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELDS, tables, userSearchWhere, orderby, key, pageSize, page, false);
                // PageData data = new PageData();

                ds = DataBase.ExecuteDataset(CommandType.Text, sql, paramList.ToArray());
                return ds;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return ds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            StringBuilder builder = new StringBuilder(" 1 = 1");

            if ((param != null) && (param.Count > 0))
            {
                for (int i = 0; i < param.Count; i++)
                {
                    SqlParameter parameter;
                    SearchParam iparam = param[i];
                    switch (iparam.ParamKey.Trim().ToLower())
                    {
                        case "userid":
                            builder.Append(" AND [userid] = @userid");
                            parameter = new SqlParameter("@userid", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "stime":
                            builder.Append(" AND [addtime] >= @stime");
                            parameter = new SqlParameter("@stime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "etime":
                            builder.Append(" AND [addtime] <= @etime");
                            parameter = new SqlParameter("@etime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }
    }
}
