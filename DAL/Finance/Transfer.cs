using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Finance
{
    public partial class Transfer
    {
        internal string SQL_TABLE = "v_transfer";
        internal  string SQL_TABLE_FIELD = @"[id]
      ,[year]
      ,[month]
      ,[userid]
      ,[touserid]
      ,[amt]
      ,[charge]
      ,[remark]
      ,[status]
      ,[addtime]
      ,[updatetime]
      ,[username]
      ,[full_name]
      ,[username1]
      ,[full_name1]";


        public Transfer()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "transfer");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from transfer");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(Model.Finance.Transfer model)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@year", SqlDbType.Int,4),
					new SqlParameter("@month", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@touserid", SqlDbType.Int,4),
					new SqlParameter("@amt", SqlDbType.Decimal,9),
					new SqlParameter("@charge", SqlDbType.Decimal,9),
					new SqlParameter("@remark", SqlDbType.VarChar,200),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@updatetime", SqlDbType.DateTime)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.year;
                parameters[2].Value = model.month;
                parameters[3].Value = model.userid;
                parameters[4].Value = model.touserid;
                parameters[5].Value = model.amt;
                parameters[6].Value = model.charge;
                parameters[7].Value = model.remark;
                parameters[8].Value = model.status;
                parameters[9].Value = model.addtime;
                parameters[10].Value = model.updatetime;

                DbHelperSQL.RunProcedure("proc_transfer_add", parameters, out rowsAffected);
                return (int)parameters[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Finance.Transfer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update transfer set ");
            strSql.Append("year=@year,");
            strSql.Append("month=@month,");
            strSql.Append("userid=@userid,");
            strSql.Append("touserid=@touserid,");
            strSql.Append("amt=@amt,");
            strSql.Append("charge=@charge,");
            strSql.Append("remark=@remark,");
            strSql.Append("status=@status,");
            strSql.Append("addtime=@addtime,");
            strSql.Append("updatetime=@updatetime");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@year", SqlDbType.Int,4),
					new SqlParameter("@month", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@touserid", SqlDbType.Int,4),
					new SqlParameter("@amt", SqlDbType.Decimal,9),
					new SqlParameter("@charge", SqlDbType.Decimal,9),
					new SqlParameter("@remark", SqlDbType.VarChar,200),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@updatetime", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.year;
            parameters[1].Value = model.month;
            parameters[2].Value = model.userid;
            parameters[3].Value = model.touserid;
            parameters[4].Value = model.amt;
            parameters[5].Value = model.charge;
            parameters[6].Value = model.remark;
            parameters[7].Value = model.status;
            parameters[8].Value = model.addtime;
            parameters[9].Value = model.updatetime;
            parameters[10].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from transfer ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)			};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from transfer ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Finance.Transfer GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,year,month,userid,touserid,amt,charge,remark,status,addtime,updatetime from transfer ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)			};
            parameters[0].Value = id;

            Model.Finance.Transfer model = new Model.Finance.Transfer();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Finance.Transfer DataRowToModel(DataRow row)
        {
            Model.Finance.Transfer model = new Model.Finance.Transfer();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["year"] != null && row["year"].ToString() != "")
                {
                    model.year = int.Parse(row["year"].ToString());
                }
                if (row["month"] != null && row["month"].ToString() != "")
                {
                    model.month = int.Parse(row["month"].ToString());
                }
                if (row["userid"] != null && row["userid"].ToString() != "")
                {
                    model.userid = int.Parse(row["userid"].ToString());
                }
                if (row["touserid"] != null && row["touserid"].ToString() != "")
                {
                    model.touserid = int.Parse(row["touserid"].ToString());
                }
                if (row["amt"] != null && row["amt"].ToString() != "")
                {
                    model.amt = decimal.Parse(row["amt"].ToString());
                }
                if (row["charge"] != null && row["charge"].ToString() != "")
                {
                    model.charge = decimal.Parse(row["charge"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.status = int.Parse(row["status"].ToString());
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,year,month,userid,touserid,amt,charge,remark,status,addtime,updatetime ");
            strSql.Append(" FROM transfer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,year,month,userid,touserid,amt,charge,remark,status,addtime,updatetime ");
            strSql.Append(" FROM transfer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM transfer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
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
            strSql.Append(")AS Row, T.*  from transfer T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

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
            parameters[0].Value = "transfer";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod

        /// <summary>
        /// 根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public  DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string userSearchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, userSearchWhere, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELD, tables, userSearchWhere, orderby, key, pageSize, page, false);
             

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
        private  string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
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
                        case "touserid":
                            builder.Append(" AND [touserid] = @touserid");
                            parameter = new SqlParameter("@touserid", SqlDbType.Int);
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
