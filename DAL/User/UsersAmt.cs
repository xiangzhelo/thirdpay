using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model.User;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.User
{
    public class UsersAmt
    {
        internal const string SQL_TABLE = "usersAmt";
        //money,pay,nopay,Integral
        internal const string SQL_TABLE_FIELD = @"[id]
      ,[userId]
      ,[Integral]
      ,[Freeze]
      ,[balance]
      ,[payment]
      ,[unpayment],[Freeze],(ISNULL([balance],0)-ISNULL([unpayment],0)-ISNULL([Freeze],0)) as enableAmt";

        #region GetModelFromDs

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UsersAmtInfo GetModel(int userId)
        {
            SqlParameter[] parameters = { new SqlParameter("@userId", SqlDbType.Int, 4) };
            parameters[0].Value = userId;

            var model = new UsersAmtInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usersAmt_GetModel", parameters);
            model = GetModelFromDs(ds);

            return model;
        }

        /// <summary>
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal UsersAmtInfo GetModelFromDs(DataSet ds)
        {
            var model = new UsersAmtInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userId"].ToString() != "")
                {
                    model.UserId = int.Parse(ds.Tables[0].Rows[0]["userId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Integral"].ToString() != "")
                {
                    model.Integral = int.Parse(ds.Tables[0].Rows[0]["Integral"].ToString());
                }
                if (ds.Tables[0].Rows[0]["balance"].ToString() != "")
                {
                    model.Balance = decimal.Parse(ds.Tables[0].Rows[0]["balance"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment"].ToString() != "")
                {
                    model.Payment = decimal.Parse(ds.Tables[0].Rows[0]["payment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["unpayment"].ToString() != "")
                {
                    model.Unpayment = decimal.Parse(ds.Tables[0].Rows[0]["unpayment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Freeze"].ToString() != "")
                {
                    model.Freeze = decimal.Parse(ds.Tables[0].Rows[0]["Freeze"].ToString());
                }
                if (ds.Tables[0].Rows[0]["enableAmt"].ToString() != "")
                {
                    model.enableAmt = decimal.Parse(ds.Tables[0].Rows[0]["enableAmt"].ToString());
                }
                //if (ds.Tables[0].Rows[0]["special"].ToString() != "")
                //{
                //    model.special = byte.Parse(ds.Tables[0].Rows[0]["special"].ToString());
                //}
                //if (ds.Tables[0].Rows[0]["payrate"].ToString() != "")
                //{
                //    model.payrate = int.Parse(ds.Tables[0].Rows[0]["payrate"].ToString());
                //}
                return model;
            }
            return null;
        }

        #endregion


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append("select id,userId,Integral,Freeze,balance,payment,unpayment,unpayment2,balance-Freeze-unpayment as enableAmt ");
            strSql.Append(" FROM usersAmt ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        ///     根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            var ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }

                var paramList = new List<SqlParameter>();
                string userSearchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, userSearchWhere, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELD, tables, userSearchWhere, orderby, key, pageSize,
                                 page, false);
                // PageData data = new PageData();

                ds = DataBase.ExecuteDataset(CommandType.Text, sql, paramList.ToArray());
                //using (IDataReader reader = DataBase.ExecuteReader(CommandType.Text, sql, paramList.ToArray()))
                //{
                //    if (!reader.Read())
                //    {
                //        return data;
                //    }
                //    data.RecordCount = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                //    if (!reader.NextResult())
                //    {
                //        return data;
                //    }
                //    while (reader.Read())
                //    {
                //        data.Items.Add(ReaderBind(reader));
                //    }
                //}
                return ds;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return ds;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            var builder = new StringBuilder(" 1 = 1");

            if ((param != null) && (param.Count > 0))
            {
                for (int i = 0; i < param.Count; i++)
                {
                    SqlParameter parameter;
                    SearchParam iparam = param[i];
                    if (iparam.CmpOperator == "=")
                    {
                        switch (iparam.ParamKey.Trim().ToLower())
                        {
                            case "userid":
                                builder.Append(" AND [userid] = @userid");
                                parameter = new SqlParameter("@userid", SqlDbType.Int);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            //case "manageid":
                            //    builder.Append(" AND [manageId] = @manageId");
                            //    parameter = new SqlParameter("@manageId", SqlDbType.Int);
                            //    parameter.Value = (int)iparam.ParamValue;
                            //    paramList.Add(parameter);
                            //    break;
                        }
                    }
                    else
                    {
                        switch (iparam.ParamKey.Trim().ToLower())
                        {
                            case "balance":
                                builder.AppendFormat(" AND [balance] {0} @balance", iparam.CmpOperator);
                                parameter = new SqlParameter("@balance", SqlDbType.Decimal);
                                parameter.Value = iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                        }
                    }
                }
            }
            return builder.ToString();
        }
    }
}