using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Order.Card
{
    /// <summary>
    ///     数据访问类:cardwithhold
    /// </summary>
    public class cardwithhold
    {
        internal string FIELDS = @"[id]
      ,[userid]
      ,[cardtype]
      ,[cardno]
      ,[cardpwd]
      ,[source]
      ,[status]
      ,[facevalue]
      ,[settle]
      ,[withhold]
      ,[backamt]
      ,[supplierid]
      ,[supprate]
      ,[profit]
      ,[addtime]
      ,[updatetime]
      ,[UserName]
      ,[suppName]
      ,[cardTypeName]
      ,[isclose],[lockAmt]";
        internal string SQL_TABLE = "v_cardwithhold";

        #region  Method

        /// <summary>
        ///     得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("userid", "cardwithhold");
        }

        /// <summary>
        ///     是否存在该记录
        /// </summary>
        public bool Exists(int userid, int cardtype, string cardno)
        {
            int rowsAffected;
            SqlParameter[] parameters =
            {
                new SqlParameter("@userid", SqlDbType.Int, 4),
                new SqlParameter("@cardtype", SqlDbType.Int, 4),
                new SqlParameter("@cardno", SqlDbType.VarChar, 40)
            };
            parameters[0].Value = userid;
            parameters[1].Value = cardtype;
            parameters[2].Value = cardno;

            int result = DbHelperSQL.RunProcedure("proc_cardwithhold_Exists", parameters, out rowsAffected);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     增加一条数据
        /// </summary>
        public int Add(Model.Order.Cardwithhold model)
        {
            int rowsAffected;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4),
                new SqlParameter("@userid", SqlDbType.Int, 4),
                new SqlParameter("@cardtype", SqlDbType.Int, 4),
                new SqlParameter("@cardno", SqlDbType.VarChar, 40),
                new SqlParameter("@cardpwd", SqlDbType.VarChar, 40),
                new SqlParameter("@source", SqlDbType.TinyInt, 1),
                new SqlParameter("@status", SqlDbType.TinyInt, 1),
                new SqlParameter("@facevalue", SqlDbType.Decimal, 9),
                new SqlParameter("@settle", SqlDbType.Decimal, 9),
                new SqlParameter("@withhold", SqlDbType.Decimal, 9),
                new SqlParameter("@backamt", SqlDbType.Decimal, 9),
                new SqlParameter("@supplierid", SqlDbType.Int, 4),
                new SqlParameter("@supprate", SqlDbType.Decimal, 9),
                new SqlParameter("@addtime", SqlDbType.DateTime),
                new SqlParameter("@updatetime", SqlDbType.DateTime)
            };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.cardtype;
            parameters[3].Value = model.cardno;
            parameters[4].Value = model.cardpwd;
            parameters[5].Value = model.source;
            parameters[6].Value = model.status;
            parameters[7].Value = model.facevalue;
            parameters[8].Value = model.settle;
            parameters[9].Value = model.withhold;
            parameters[10].Value = model.backamt;
            parameters[11].Value = model.supplierid;
            parameters[12].Value = model.supprate;
            parameters[13].Value = model.addtime;
            parameters[14].Value = model.updatetime;

            DbHelperSQL.RunProcedure("proc_cardwithhold_ADD", parameters, out rowsAffected);
            return (int) parameters[0].Value;
        }


        /// <summary>
        ///     更新一条数据
        /// </summary>
        public bool Update(Model.Order.Cardwithhold model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4),
                new SqlParameter("@userid", SqlDbType.Int, 4),
                new SqlParameter("@cardtype", SqlDbType.Int, 4),
                new SqlParameter("@cardno", SqlDbType.VarChar, 40),
                new SqlParameter("@cardpwd", SqlDbType.VarChar, 40),
                new SqlParameter("@source", SqlDbType.TinyInt, 1),
                new SqlParameter("@status", SqlDbType.TinyInt, 1),
                new SqlParameter("@facevalue", SqlDbType.Decimal, 9),
                new SqlParameter("@settle", SqlDbType.Decimal, 9),
                new SqlParameter("@withhold", SqlDbType.Decimal, 9),
                new SqlParameter("@backamt", SqlDbType.Decimal, 9),
                new SqlParameter("@supplierid", SqlDbType.Int, 4),
                new SqlParameter("@supprate", SqlDbType.Decimal, 9),
                new SqlParameter("@addtime", SqlDbType.DateTime),
                new SqlParameter("@updatetime", SqlDbType.DateTime)
            };
            parameters[0].Value = model.id;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.cardtype;
            parameters[3].Value = model.cardno;
            parameters[4].Value = model.cardpwd;
            parameters[5].Value = model.source;
            parameters[6].Value = model.status;
            parameters[7].Value = model.facevalue;
            parameters[8].Value = model.settle;
            parameters[9].Value = model.withhold;
            parameters[10].Value = model.backamt;
            parameters[11].Value = model.supplierid;
            parameters[12].Value = model.supprate;
            parameters[13].Value = model.addtime;
            parameters[14].Value = model.updatetime;

            DbHelperSQL.RunProcedure("proc_cardwithhold_Update", parameters, out rowsAffected);
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            DbHelperSQL.RunProcedure("proc_cardwithhold_Delete", parameters, out rowsAffected);
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     删除一条数据
        /// </summary>
        public bool Delete(int userid, int cardtype, string cardno)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from cardwithhold ");
            strSql.Append(" where userid=@userid and cardtype=@cardtype and cardno=@cardno ");
            SqlParameter[] parameters =
            {
                new SqlParameter("@userid", SqlDbType.Int, 4),
                new SqlParameter("@cardtype", SqlDbType.Int, 4),
                new SqlParameter("@cardno", SqlDbType.VarChar, 40)
            };
            parameters[0].Value = userid;
            parameters[1].Value = cardtype;
            parameters[2].Value = cardno;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from cardwithhold ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     批量删除数据
        /// </summary>
        public bool BatchColse(string idlist, byte iscolse)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ids", SqlDbType.VarChar, 1000)
                , new SqlParameter("@iscolse", SqlDbType.TinyInt, 1)
            };
            parameters[0].Value = idlist;
            parameters[1].Value = iscolse;

            int rows = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_cardwithhold_batchcolse", parameters);

            return rows > 0;
        }


        /// <summary>
        ///     批量删除数据
        /// </summary>
        public bool BatchUnlock(string idlist)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ids", SqlDbType.VarChar, 1000)
            };
            parameters[0].Value = idlist;

            int rows = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_cardwithhold_unlock", parameters);

            return rows > 0;
        }

        /// <summary>
        ///     批量删除数据
        /// </summary>
        public bool ALLColse(List<SearchParam> searchParams)
        {
            var paramList = new List<SqlParameter>();
            string where = BuilderWhere(searchParams, paramList);

            string sqlText = "update [cardwithhold] set [isclose]=@upstatus where " + where;

            int rows = DataBase.ExecuteNonQuery(CommandType.Text, sqlText, paramList.ToArray());

            return rows > 0;
        }

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.Order.Cardwithhold GetModel(int id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            var model = new Model.Order.Cardwithhold();
            DataSet ds = DbHelperSQL.RunProcedure("proc_cardwithhold_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }


        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.Order.Cardwithhold DataRowToModel(DataRow row)
        {
            var model = new Model.Order.Cardwithhold();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["userid"] != null && row["userid"].ToString() != "")
                {
                    model.userid = int.Parse(row["userid"].ToString());
                }
                if (row["cardtype"] != null && row["cardtype"].ToString() != "")
                {
                    model.cardtype = int.Parse(row["cardtype"].ToString());
                }
                if (row["cardno"] != null)
                {
                    model.cardno = row["cardno"].ToString();
                }
                if (row["cardpwd"] != null)
                {
                    model.cardpwd = row["cardpwd"].ToString();
                }
                if (row["source"] != null && row["source"].ToString() != "")
                {
                    model.source = int.Parse(row["source"].ToString());
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.status = int.Parse(row["status"].ToString());
                }
                if (row["facevalue"] != null && row["facevalue"].ToString() != "")
                {
                    model.facevalue = decimal.Parse(row["facevalue"].ToString());
                }
                if (row["settle"] != null && row["settle"].ToString() != "")
                {
                    model.settle = decimal.Parse(row["settle"].ToString());
                }
                if (row["withhold"] != null && row["withhold"].ToString() != "")
                {
                    model.withhold = decimal.Parse(row["withhold"].ToString());
                }
                if (row["backamt"] != null && row["backamt"].ToString() != "")
                {
                    model.backamt = decimal.Parse(row["backamt"].ToString());
                }
                if (row["supplierid"] != null && row["supplierid"].ToString() != "")
                {
                    model.supplierid = int.Parse(row["supplierid"].ToString());
                }
                if (row["supprate"] != null && row["supprate"].ToString() != "")
                {
                    model.supprate = decimal.Parse(row["supprate"].ToString());
                }
                if (row["addtime"] != null && row["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(row["addtime"].ToString());
                }
                if (row["updatetime"] != null && row["updatetime"].ToString() != "")
                {
                    model.updatetime = DateTime.Parse(row["updatetime"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        ///     获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(
                "select id,userid,cardtype,cardno,cardpwd,source,status,facevalue,settle,withhold,backamt,supplierid,supprate,addtime,updatetime ");
            strSql.Append(" FROM cardwithhold ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        ///     获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top);
            }
            strSql.Append(
                " id,userid,cardtype,cardno,cardpwd,source,status,facevalue,settle,withhold,backamt,supplierid,supprate,addtime,updatetime ");
            strSql.Append(" FROM cardwithhold ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        ///     获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append("select count(1) FROM cardwithhold ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }

        /// <summary>
        ///     分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from cardwithhold T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #region 分页

        /// <summary>
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, byte isstat)
        {
            var ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "addTime desc";
                }

                var paramList = new List<SqlParameter>();
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(FIELDS, tables, where, orderby, key, pageSize, page, false);

                //+ "\r\n" + "select ISNULL(sum(amount),0) from V_Settled where " + where;

                if (isstat == 1)
                {
                    sql += "\r\n" + "select sum(profit) as profit from v_cardwithhold where " + where;
                }


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
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            var builder = new StringBuilder(" 1 = 1");

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
                            parameter.Value = (int) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "isclose":
                            builder.Append(" AND [isclose] = @isclose");
                            parameter = new SqlParameter("@isclose", SqlDbType.TinyInt);
                            parameter.Value = (byte) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "cardno": //系统交易号
                            builder.Append(" AND [cardno] like @cardno");
                            parameter = new SqlParameter("@cardno", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "supplierid":
                            builder.Append(" AND [supplierid] = @supplierid");
                            parameter = new SqlParameter("@supplierid", SqlDbType.Int);
                            parameter.Value = (int) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "begindate":
                            builder.Append(" AND [addtime] >= @begindate");
                            parameter = new SqlParameter("@begindate", SqlDbType.DateTime);
                            parameter.Value = (DateTime) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "enddate":
                            builder.Append(" AND [addtime] <= @enddate");
                            parameter = new SqlParameter("@enddate", SqlDbType.DateTime);
                            parameter.Value = (DateTime) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }

        #endregion

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "cardwithhold";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  Method

        #region  MethodEx

        #endregion  MethodEx
    }
}