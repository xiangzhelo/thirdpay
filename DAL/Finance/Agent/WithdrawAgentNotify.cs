using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
//Please add references
using DBAccess;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Finance.Agent
{
	/// <summary>
	/// 数据访问类:withdrawAgentNotify
	/// </summary>
	public partial class WithdrawAgentNotify
	{
        internal string FIELDS = @"[id]
      ,[notify_id]
      ,[userid]
      ,[trade_no]
      ,[out_trade_no]
      ,[notifystatus]
      ,[notifyurl]
      ,[resText]
      ,[addTime]
      ,[resTime]
      ,[ext1]
      ,[ext2]
      ,[ext3]
      ,[remark]";
        internal string SQL_TABLE = "withdrawAgentNotify";

		public WithdrawAgentNotify()
		{}

		#region  Method
		/// <summary>
		///  增加一条数据
		/// </summary>
		public int Add(viviapi.Model.Finance.Agent.WithdrawAgentNotify model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@notify_id", SqlDbType.VarChar,20),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@trade_no", SqlDbType.VarChar,40),
					new SqlParameter("@out_trade_no", SqlDbType.VarChar,64),
					new SqlParameter("@notifystatus", SqlDbType.TinyInt,1),
					new SqlParameter("@notifyurl", SqlDbType.VarChar,1000),
					new SqlParameter("@resText", SqlDbType.NVarChar,200),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@resTime", SqlDbType.DateTime),
					new SqlParameter("@ext1", SqlDbType.VarChar,50),
					new SqlParameter("@ext2", SqlDbType.VarChar,50),
					new SqlParameter("@ext3", SqlDbType.VarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,500)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.notify_id;
			parameters[2].Value = model.userid;
			parameters[3].Value = model.trade_no;
			parameters[4].Value = model.out_trade_no;
			parameters[5].Value = model.notifystatus;
			parameters[6].Value = model.notifyurl;
			parameters[7].Value = model.resText;
			parameters[8].Value = model.addTime;
			parameters[9].Value = model.resTime;
			parameters[10].Value = model.ext1;
			parameters[11].Value = model.ext2;
			parameters[12].Value = model.ext3;
			parameters[13].Value = model.remark;

			DbHelperSQL.RunProcedure("proc_withdrawAgentNotify_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(viviapi.Model.Finance.Agent.WithdrawAgentNotify model)
		{
			int rowsAffected=0;
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@notify_id", SqlDbType.VarChar,20),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@trade_no", SqlDbType.VarChar,40),
					new SqlParameter("@out_trade_no", SqlDbType.VarChar,64),
					new SqlParameter("@notifystatus", SqlDbType.TinyInt,1),
					new SqlParameter("@notifyurl", SqlDbType.VarChar,1000),
					new SqlParameter("@resText", SqlDbType.NVarChar,200),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@resTime", SqlDbType.DateTime),
					new SqlParameter("@ext1", SqlDbType.VarChar,50),
					new SqlParameter("@ext2", SqlDbType.VarChar,50),
					new SqlParameter("@ext3", SqlDbType.VarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.notify_id;
			parameters[2].Value = model.userid;
			parameters[3].Value = model.trade_no;
			parameters[4].Value = model.out_trade_no;
			parameters[5].Value = model.notifystatus;
			parameters[6].Value = model.notifyurl;
			parameters[7].Value = model.resText;
			parameters[8].Value = model.addTime;
			parameters[9].Value = model.resTime;
			parameters[10].Value = model.ext1;
			parameters[11].Value = model.ext2;
			parameters[12].Value = model.ext3;
			parameters[13].Value = model.remark;

			DbHelperSQL.RunProcedure("proc_withdrawAgentNotify_Update",parameters,out rowsAffected);
			if (rowsAffected > 0)
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
			int rowsAffected=0;
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			DbHelperSQL.RunProcedure("proc_withdrawAgentNotify_Delete",parameters,out rowsAffected);
			if (rowsAffected > 0)
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from withdrawAgentNotify ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public viviapi.Model.Finance.Agent.WithdrawAgentNotify GetModel(int id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			viviapi.Model.Finance.Agent.WithdrawAgentNotify model=new viviapi.Model.Finance.Agent.WithdrawAgentNotify();
			DataSet ds= DbHelperSQL.RunProcedure("proc_withdrawAgentNotify_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
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
		public viviapi.Model.Finance.Agent.WithdrawAgentNotify DataRowToModel(DataRow row)
		{
			viviapi.Model.Finance.Agent.WithdrawAgentNotify model=new viviapi.Model.Finance.Agent.WithdrawAgentNotify();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["notify_id"]!=null)
				{
					model.notify_id=row["notify_id"].ToString();
				}
				if(row["userid"]!=null && row["userid"].ToString()!="")
				{
					model.userid=int.Parse(row["userid"].ToString());
				}
				if(row["trade_no"]!=null)
				{
					model.trade_no=row["trade_no"].ToString();
				}
				if(row["out_trade_no"]!=null)
				{
					model.out_trade_no=row["out_trade_no"].ToString();
				}
				if(row["notifystatus"]!=null && row["notifystatus"].ToString()!="")
				{
					model.notifystatus=int.Parse(row["notifystatus"].ToString());
				}
				if(row["notifyurl"]!=null)
				{
					model.notifyurl=row["notifyurl"].ToString();
				}
				if(row["resText"]!=null)
				{
					model.resText=row["resText"].ToString();
				}
				if(row["addTime"]!=null && row["addTime"].ToString()!="")
				{
					model.addTime=DateTime.Parse(row["addTime"].ToString());
				}
				if(row["resTime"]!=null && row["resTime"].ToString()!="")
				{
					model.resTime=DateTime.Parse(row["resTime"].ToString());
				}
				if(row["ext1"]!=null)
				{
					model.ext1=row["ext1"].ToString();
				}
				if(row["ext2"]!=null)
				{
					model.ext2=row["ext2"].ToString();
				}
				if(row["ext3"]!=null)
				{
					model.ext3=row["ext3"].ToString();
				}
				if(row["remark"]!=null)
				{
					model.remark=row["remark"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,notify_id,userid,trade_no,out_trade_no,notifystatus,notifyurl,resText,addTime,resTime,ext1,ext2,ext3,remark ");
			strSql.Append(" FROM withdrawAgentNotify ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,notify_id,userid,trade_no,out_trade_no,notifystatus,notifyurl,resText,addTime,resTime,ext1,ext2,ext3,remark ");
			strSql.Append(" FROM withdrawAgentNotify ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM withdrawAgentNotify ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from withdrawAgentNotify T ");
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
			parameters[0].Value = "withdrawAgentNotify";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

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
                    orderby = "addTime desc";
                }

                var paramList = new List<SqlParameter>();
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(FIELDS, tables, where, orderby, key, pageSize, page, false);

                //+ "\r\n" + "select ISNULL(sum(amount),0) from V_Settled where " + where;


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
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "notifystatus":
                            builder.Append(" AND [notifystatus] = @notifystatus");
                            parameter = new SqlParameter("@notifystatus", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "trade_no": //系统交易号
                            builder.Append(" AND [trade_no] like @trade_no");
                            parameter = new SqlParameter("@trade_no", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "out_trade_no": //系统交易号
                            builder.Append(" AND [out_trade_no] like @out_trade_no");
                            parameter = new SqlParameter("@out_trade_no", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "begindate":
                            builder.Append(" AND [addTime] >= @addTime");
                            parameter = new SqlParameter("@beginpaytime", SqlDbType.DateTime);
                            parameter.Value = (DateTime)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "enddate":
                            builder.Append(" AND [addTime] <= @addTime");
                            parameter = new SqlParameter("@endpaytime", SqlDbType.DateTime);
                            parameter.Value = (DateTime)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }

        #endregion

		#region  MethodEx

		#endregion  MethodEx
	}
}

