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
    ///     数据访问类:ordercardsend
    /// </summary>
    public class OrderCardSend
    {
        internal string FIELDS = @"[id]
      ,[orderid]
      ,[source]
      ,[suppId]
      ,[success]
      ,[summitStatus]
      ,[orderStatus]
      ,[typeid]
      ,[cardno]
      ,[cardpass]
      ,[facevalue]
      ,[suppTransNo]
      ,[responseText]
      ,[errCode]
      ,[errMsg]
      ,[initTime]
      ,[message]
      ,[completeTime]";
        internal string SQL_TABLE = "ordercardsend";

        #region  Method

        /// <summary>
        ///     增加一条数据
        /// </summary>
        public int Add(Model.Order.OrderCardSend model)
        {
            int rowsAffected;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4),
                new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                new SqlParameter("@source", SqlDbType.TinyInt, 1),
                new SqlParameter("@suppId", SqlDbType.Int, 4),
                new SqlParameter("@success", SqlDbType.TinyInt, 1),
                new SqlParameter("@summitStatus", SqlDbType.TinyInt, 1),
                new SqlParameter("@orderStatus", SqlDbType.TinyInt, 1),
                new SqlParameter("@cardno", SqlDbType.VarChar, 50),
                new SqlParameter("@cardpass", SqlDbType.VarChar, 50),
                new SqlParameter("@suppTransNo", SqlDbType.VarChar, 50),
                new SqlParameter("@responseText", SqlDbType.VarChar, 2000),
                new SqlParameter("@errCode", SqlDbType.NVarChar, 50),
                new SqlParameter("@errMsg", SqlDbType.NVarChar, 200),
                new SqlParameter("@initTime", SqlDbType.DateTime),
                new SqlParameter("@message", SqlDbType.NVarChar, 200),
                new SqlParameter("@completeTime", SqlDbType.DateTime),
                new SqlParameter("@typeid", SqlDbType.Int, 4),
                new SqlParameter("@facevalue", SqlDbType.Int, 4)
            };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.orderid;
            parameters[2].Value = model.source;
            parameters[3].Value = model.suppId;
            parameters[4].Value = model.success;
            parameters[5].Value = model.summitStatus;
            parameters[6].Value = model.orderStatus;
            parameters[7].Value = model.cardno;
            parameters[8].Value = model.cardpass;
            parameters[9].Value = model.suppTransNo;
            parameters[10].Value = model.responseText;
            parameters[11].Value = model.errCode;
            parameters[12].Value = model.errMsg;
            parameters[13].Value = model.initTime;
            parameters[14].Value = model.message;
            parameters[15].Value = model.completeTime;

            parameters[16].Value = model.typeid;
            parameters[17].Value = model.facevalue;

            DbHelperSQL.RunProcedure("proc_ordercardsend_ADD", parameters, out rowsAffected);
            return (int) parameters[0].Value;
        }

        /// <summary>
        ///     更新一条数据
        /// </summary>
        public bool Update(Model.Order.OrderCardSend model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4),
                new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                new SqlParameter("@source", SqlDbType.TinyInt, 1),
                new SqlParameter("@suppId", SqlDbType.Int, 4),
                new SqlParameter("@success", SqlDbType.TinyInt, 1),
                new SqlParameter("@summitStatus", SqlDbType.TinyInt, 1),
                new SqlParameter("@orderStatus", SqlDbType.TinyInt, 1),
                new SqlParameter("@cardno", SqlDbType.VarChar, 50),
                new SqlParameter("@cardpass", SqlDbType.VarChar, 50),
                new SqlParameter("@suppTransNo", SqlDbType.VarChar, 50),
                new SqlParameter("@responseText", SqlDbType.VarChar, 2000),
                new SqlParameter("@errCode", SqlDbType.NVarChar, 50),
                new SqlParameter("@errMsg", SqlDbType.NVarChar, 200),
                new SqlParameter("@initTime", SqlDbType.DateTime),
                new SqlParameter("@message", SqlDbType.NVarChar, 200),
                new SqlParameter("@completeTime", SqlDbType.DateTime)
            };
            parameters[0].Value = model.id;
            parameters[1].Value = model.orderid;
            parameters[2].Value = model.source;
            parameters[3].Value = model.suppId;
            parameters[4].Value = model.success;
            parameters[5].Value = model.summitStatus;
            parameters[6].Value = model.orderStatus;
            parameters[7].Value = model.cardno;
            parameters[8].Value = model.cardpass;
            parameters[9].Value = model.suppTransNo;
            parameters[10].Value = model.responseText;
            parameters[11].Value = model.errCode;
            parameters[12].Value = model.errMsg;
            parameters[13].Value = model.initTime;
            parameters[14].Value = model.message;
            parameters[15].Value = model.completeTime;

            DbHelperSQL.RunProcedure("proc_ordercardsend_Update", parameters, out rowsAffected);
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

            DbHelperSQL.RunProcedure("proc_ordercardsend_Delete", parameters, out rowsAffected);
            if (rowsAffected > 0)
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
            strSql.Append("delete from ordercardsend ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.Order.OrderCardSend GetModel(int id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            var model = new Model.Order.OrderCardSend();
            DataSet ds = DbHelperSQL.RunProcedure("proc_ordercardsend_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }


        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.Order.OrderCardSend DataRowToModel(DataRow row)
        {
            var model = new Model.Order.OrderCardSend();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["orderid"] != null)
                {
                    model.orderid = row["orderid"].ToString();
                }
                if (row["source"] != null && row["source"].ToString() != "")
                {
                    model.source = byte.Parse(row["source"].ToString());
                }
                if (row["suppId"] != null && row["suppId"].ToString() != "")
                {
                    model.suppId = int.Parse(row["suppId"].ToString());
                }
                if (row["success"] != null && row["success"].ToString() != "")
                {
                    model.success = int.Parse(row["success"].ToString());
                }
                if (row["summitStatus"] != null && row["summitStatus"].ToString() != "")
                {
                    model.summitStatus = int.Parse(row["summitStatus"].ToString());
                }
                if (row["orderStatus"] != null && row["orderStatus"].ToString() != "")
                {
                    model.orderStatus = int.Parse(row["orderStatus"].ToString());
                }
                if (row["cardno"] != null)
                {
                    model.cardno = row["cardno"].ToString();
                }
                if (row["cardpass"] != null)
                {
                    model.cardpass = row["cardpass"].ToString();
                }
                if (row["suppTransNo"] != null)
                {
                    model.suppTransNo = row["suppTransNo"].ToString();
                }
                if (row["responseText"] != null)
                {
                    model.responseText = row["responseText"].ToString();
                }
                if (row["errCode"] != null)
                {
                    model.errCode = row["errCode"].ToString();
                }
                if (row["errMsg"] != null)
                {
                    model.errMsg = row["errMsg"].ToString();
                }
                if (row["initTime"] != null && row["initTime"].ToString() != "")
                {
                    model.initTime = DateTime.Parse(row["initTime"].ToString());
                }
                if (row["message"] != null)
                {
                    model.message = row["message"].ToString();
                }
                if (row["completeTime"] != null && row["completeTime"].ToString() != "")
                {
                    model.completeTime = DateTime.Parse(row["completeTime"].ToString());
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
                "select id,orderid,source,suppId,success,summitStatus,orderStatus,cardno,cardpass,suppTransNo,responseText,errCode,errMsg,initTime,message,completeTime ");
            strSql.Append(" FROM ordercardsend ");
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
                " id,orderid,source,suppId,success,summitStatus,orderStatus,cardno,cardpass,suppTransNo,responseText,errCode,errMsg,initTime,message,completeTime ");
            strSql.Append(" FROM ordercardsend ");
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
            strSql.Append("select count(1) FROM ordercardsend ");
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
            strSql.Append(")AS Row, T.*  from ordercardsend T ");
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
            parameters[0].Value = "ordercardsend";
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

        #region 分页

        /// <summary>
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            var ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "initTime desc";
                }

                var paramList = new List<SqlParameter>();
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(FIELDS, tables, where, orderby, key, pageSize, page, false);

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
                        case "orderid": //系统交易号
                            builder.Append(" AND [orderid] like @orderid");
                            parameter = new SqlParameter("@orderid", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "cardno":
                            builder.Append(" AND [cardno] like @cardno");
                            parameter = new SqlParameter("@cardno", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "typeid":
                            builder.Append(" AND [typeid] = @typeid");
                            parameter = new SqlParameter("@typeid", SqlDbType.Int);
                            parameter.Value = (int) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "success":
                            builder.Append(" AND [success] = @success");
                            parameter = new SqlParameter("@success", SqlDbType.TinyInt);
                            parameter.Value = (byte) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "supplierid":
                            builder.Append(" AND [suppId] = @supplierid");
                            parameter = new SqlParameter("@supplierid", SqlDbType.Int);
                            parameter.Value = (int) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "begindate":
                            builder.Append(" AND [initTime] >= @begindate");
                            parameter = new SqlParameter("@begindate", SqlDbType.DateTime);
                            parameter.Value = (DateTime) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "enddate":
                            builder.Append(" AND [initTime] <= @enddate");
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

        #endregion  MethodEx
    }
}