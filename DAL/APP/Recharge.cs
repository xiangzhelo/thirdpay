using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.APP
{
    public class Recharge
    {
        internal const string SqlTable = "apprecharge";
        internal const string FieldNews = @"[id]
      ,[paytype]
      ,[rechtype]
      ,[orderid]
      ,[account]
      ,[userid]
      ,[rechargeAmt]
      ,[realPayAmt]
      ,[addtime]
      ,[status]
      ,[processstatus]
      ,[processtime]
      ,[smsnotification]
      ,[field1]
      ,[field2]
      ,[remark],Balance";

        #region  Method

        /// <summary>
        ///     得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "apprecharge");
        }

        /// <summary>
        ///     是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            int rowsAffected;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            int result = DbHelperSQL.RunProcedure("proc_apprecharge_Exists", parameters, out rowsAffected);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     增加一条数据
        /// </summary>
        public int Add(Model.APP.Recharge model)
        {
            int rowsAffected;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4),
                new SqlParameter("@paytype", SqlDbType.Int, 4),
                new SqlParameter("@rechtype", SqlDbType.TinyInt, 1),
                new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                new SqlParameter("@account", SqlDbType.NVarChar, 50),
                new SqlParameter("@userid", SqlDbType.Int, 4),
                new SqlParameter("@rechargeAmt", SqlDbType.Decimal, 9),
                new SqlParameter("@realPayAmt", SqlDbType.Decimal, 9),
                new SqlParameter("@addtime", SqlDbType.DateTime),
                new SqlParameter("@status", SqlDbType.TinyInt, 1),
                new SqlParameter("@processstatus", SqlDbType.TinyInt, 1),
                new SqlParameter("@processtime", SqlDbType.DateTime),
                new SqlParameter("@smsnotification", SqlDbType.Bit, 1),
                new SqlParameter("@field1", SqlDbType.NVarChar, 50),
                new SqlParameter("@field2", SqlDbType.NVarChar, 50),
                new SqlParameter("@remark", SqlDbType.NVarChar, 200),
                new SqlParameter("@suppid", SqlDbType.Int, 4)
            };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.paytype;
            parameters[2].Value = model.rechtype;
            parameters[3].Value = model.orderid;
            parameters[4].Value = model.account;
            parameters[5].Value = model.userid;
            parameters[6].Value = model.rechargeAmt;
            parameters[7].Value = model.realPayAmt;
            parameters[8].Value = model.addtime;
            parameters[9].Value = model.status;
            parameters[10].Value = model.processstatus;
            parameters[11].Value = model.processtime;
            parameters[12].Value = model.smsnotification;
            parameters[13].Value = model.field1;
            parameters[14].Value = model.field2;
            parameters[15].Value = model.remark;
            parameters[16].Value = model.suppid;

            DbHelperSQL.RunProcedure("proc_apprecharge_ADD", parameters, out rowsAffected);
            return (int) parameters[0].Value;
        }

        /// <summary>
        ///     更新一条数据
        /// </summary>
        public bool Update(Model.APP.Recharge model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4),
                new SqlParameter("@paytype", SqlDbType.Int, 4),
                new SqlParameter("@rechtype", SqlDbType.TinyInt, 1),
                new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                new SqlParameter("@account", SqlDbType.NVarChar, 50),
                new SqlParameter("@userid", SqlDbType.Int, 4),
                new SqlParameter("@rechargeAmt", SqlDbType.Decimal, 9),
                new SqlParameter("@realPayAmt", SqlDbType.Decimal, 9),
                new SqlParameter("@addtime", SqlDbType.DateTime),
                new SqlParameter("@status", SqlDbType.TinyInt, 1),
                new SqlParameter("@processstatus", SqlDbType.TinyInt, 1),
                new SqlParameter("@processtime", SqlDbType.DateTime),
                new SqlParameter("@smsnotification", SqlDbType.Bit, 1),
                new SqlParameter("@field1", SqlDbType.NVarChar, 50),
                new SqlParameter("@field2", SqlDbType.NVarChar, 50),
                new SqlParameter("@remark", SqlDbType.NVarChar, 200)
            };
            parameters[0].Value = model.id;
            parameters[1].Value = model.paytype;
            parameters[2].Value = model.rechtype;
            parameters[3].Value = model.orderid;
            parameters[4].Value = model.account;
            parameters[5].Value = model.userid;
            parameters[6].Value = model.rechargeAmt;
            parameters[7].Value = model.realPayAmt;
            parameters[8].Value = model.addtime;
            parameters[9].Value = model.status;
            parameters[10].Value = model.processstatus;
            parameters[11].Value = model.processtime;
            parameters[12].Value = model.smsnotification;
            parameters[13].Value = model.field1;
            parameters[14].Value = model.field2;
            parameters[15].Value = model.remark;

            DbHelperSQL.RunProcedure("proc_apprecharge_Update", parameters, out rowsAffected);
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

            DbHelperSQL.RunProcedure("proc_apprecharge_Delete", parameters, out rowsAffected);
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
            strSql.Append("delete from apprecharge ");
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
        public Model.APP.Recharge GetModel(string orderid)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@orderid", SqlDbType.VarChar, 30)
            };
            parameters[0].Value = orderid;

            var model = new Model.APP.Recharge();
            DataSet ds = DbHelperSQL.RunProcedure("proc_apprecharge_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }


        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.APP.Recharge DataRowToModel(DataRow row)
        {
            var model = new Model.APP.Recharge();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["paytype"] != null && row["paytype"].ToString() != "")
                {
                    model.paytype = int.Parse(row["paytype"].ToString());
                }
                if (row["suppid"] != null && row["suppid"].ToString() != "")
                {
                    model.suppid = int.Parse(row["suppid"].ToString());
                }
                if (row["rechtype"] != null && row["rechtype"].ToString() != "")
                {
                    model.rechtype = int.Parse(row["rechtype"].ToString());
                }
                if (row["orderid"] != null)
                {
                    model.orderid = row["orderid"].ToString();
                }
                if (row["account"] != null)
                {
                    model.account = row["account"].ToString();
                }
                if (row["userid"] != null && row["userid"].ToString() != "")
                {
                    model.userid = int.Parse(row["userid"].ToString());
                }
                if (row["rechargeAmt"] != null && row["rechargeAmt"].ToString() != "")
                {
                    model.rechargeAmt = decimal.Parse(row["rechargeAmt"].ToString());
                }
                if (row["realPayAmt"] != null && row["realPayAmt"].ToString() != "")
                {
                    model.realPayAmt = decimal.Parse(row["realPayAmt"].ToString());
                }
                if (row["addtime"] != null && row["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(row["addtime"].ToString());
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.status = int.Parse(row["status"].ToString());
                }
                if (row["processstatus"] != null && row["processstatus"].ToString() != "")
                {
                    model.processstatus = int.Parse(row["processstatus"].ToString());
                }
                if (row["processtime"] != null && row["processtime"].ToString() != "")
                {
                    model.processtime = DateTime.Parse(row["processtime"].ToString());
                }
                if (row["smsnotification"] != null && row["smsnotification"].ToString() != "")
                {
                    if ((row["smsnotification"].ToString() == "1") ||
                        (row["smsnotification"].ToString().ToLower() == "true"))
                    {
                        model.smsnotification = true;
                    }
                    else
                    {
                        model.smsnotification = false;
                    }
                }
                if (row["field1"] != null)
                {
                    model.field1 = row["field1"].ToString();
                }
                if (row["field2"] != null)
                {
                    model.field2 = row["field2"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
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
                "select id,paytype,rechtype,orderid,account,userid,rechargeAmt,realPayAmt,addtime,status,processstatus,processtime,smsnotification,field1,field2,remark ");
            strSql.Append(" FROM apprecharge ");
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
                " id,paytype,rechtype,orderid,account,userid,rechargeAmt,realPayAmt,addtime,status,processstatus,processtime,smsnotification,field1,field2,remark ");
            strSql.Append(" FROM apprecharge ");
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
            strSql.Append("select count(1) FROM apprecharge ");
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
            strSql.Append(")AS Row, T.*  from apprecharge T ");
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
            parameters[0].Value = "apprecharge";
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

        /// <summary>
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, bool isstat)
        {
            var ds = new DataSet();
            try
            {
                string tables = SqlTable;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }

                var paramList = new List<SqlParameter>();
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(FieldNews, tables, where, orderby, key, pageSize, page, false);
                if (isstat)
                {
                    sql += "\r\n" + " select sum(realPayAmt) as realPayAmt from apprecharge where " + where;
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
        private string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
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
                        case "status":
                            builder.Append(" AND [status] = @status");
                            parameter = new SqlParameter("@status", SqlDbType.Int);
                            parameter.Value = (int) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "username":
                            builder.Append(" AND [account] like @userName");
                            parameter = new SqlParameter("@userName", SqlDbType.VarChar, 20);
                            parameter.Value = "%" + SqlHelper.CleanString((string) iparam.ParamValue, 100) + "%";
                            paramList.Add(parameter);
                            break;
                        case "starttime":
                            builder.Append(" AND [addtime] > @starttime");
                            parameter = new SqlParameter("@starttime", SqlDbType.DateTime);
                            parameter.Value = Convert.ToDateTime(iparam.ParamValue);
                            paramList.Add(parameter);
                            break;
                        case "endtime":
                            builder.Append(" AND [addtime] < @endtime");
                            parameter = new SqlParameter("@endtime", SqlDbType.DateTime);
                            parameter.Value = Convert.ToDateTime(iparam.ParamValue);
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }

        public string GetStatusName(int status)
        {
            string sname = "未付款";
            if (status == 2)
            {
                sname = "付款成功";
            }
            else if (status == 4)
            {
                sname = "付款失败";
            }

            return sname;
        }


        /// <summary>
        /// </summary>
        /// <param name="transactionNo"></param>
        /// <param name="suppTranNo"></param>
        /// <param name="payMoney"></param>
        /// <param name="status"></param>
        /// <returns>0 处理失败 1 处理成功 2 已经处理</returns>
        public int Complete(string transactionNo, string suppTranNo, decimal payMoney, int status)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                    new SqlParameter("@realPayAmt", SqlDbType.Decimal, 9),
                    new SqlParameter("@status", SqlDbType.TinyInt, 1),
                    new SqlParameter("@processtime", SqlDbType.DateTime),
                    new SqlParameter("@supptranno", SqlDbType.VarChar, 30)
                };
                parameters[0].Value = transactionNo;
                parameters[1].Value = payMoney;
                parameters[2].Value = status;
                parameters[3].Value = DateTime.Now;
                parameters[4].Value = suppTranNo;

                object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_apprecharge_complete",
                    parameters);

                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        #endregion  MethodEx
    }
}