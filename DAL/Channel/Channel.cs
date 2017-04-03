using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model.Channel;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Channel
{
    /// <summary>
    ///     支付通道
    ///     2012-02-17
    /// </summary>
    public class Channel
    {
        internal static string SqlTable = "v_channel";
        internal static string SqlTableField = @"[id]
      ,[code]
      ,[typeId]
      ,[supplier]
      ,[supprate]
      ,[modeName]
      ,[modeEnName]
      ,[faceValue]
      ,[isOpen]
      ,[sort]
      ,[name]
      ,[modetypename]
      ,[usingSupplierName]
      ,[typesupprate]
      ,[typesupp]";

        #region Add

        /// <summary>
        ///     增加一条数据
        /// </summary>
        public int Add(ChannelInfo model)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4),
                new SqlParameter("@code", SqlDbType.VarChar, 10),
                new SqlParameter("@typeId", SqlDbType.Int, 4),
                new SqlParameter("@supplier", SqlDbType.Int, 4),
                new SqlParameter("@modeName", SqlDbType.VarChar, 50),
                new SqlParameter("@modeEnName", SqlDbType.VarChar, 50),
                new SqlParameter("@faceValue", SqlDbType.Int, 4),
                new SqlParameter("@isOpen", SqlDbType.TinyInt, 1),
                new SqlParameter("@addtime", SqlDbType.DateTime),
                new SqlParameter("@sort", SqlDbType.Int, 4)
            };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.code;
            parameters[2].Value = model.typeId;
            parameters[3].Value = model.supplier;
            parameters[4].Value = model.modeName;
            parameters[5].Value = model.modeEnName;
            parameters[6].Value = model.faceValue;
            parameters[7].Value = model.isOpen;
            parameters[8].Value = model.addtime;
            parameters[9].Value = model.sort;

            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channel_ADD", parameters);
            var id = (int) parameters[0].Value;

            return id;
        }

        #endregion

        #region Update

        /// <summary>
        ///     更新一条数据
        /// </summary>
        public bool Update(ChannelInfo model)
        {
            int rowsAffected;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4),
                new SqlParameter("@code", SqlDbType.VarChar, 10),
                new SqlParameter("@typeId", SqlDbType.Int, 4),
                new SqlParameter("@supplier", SqlDbType.Int, 4),
                new SqlParameter("@modeName", SqlDbType.VarChar, 50),
                new SqlParameter("@modeEnName", SqlDbType.VarChar, 50),
                new SqlParameter("@faceValue", SqlDbType.Int, 4),
                new SqlParameter("@isOpen", SqlDbType.TinyInt, 1),
                new SqlParameter("@addtime", SqlDbType.DateTime),
                new SqlParameter("@sort", SqlDbType.Int, 4)
            };
            parameters[0].Value = model.id;
            parameters[1].Value = model.code;
            parameters[2].Value = model.typeId;
            parameters[3].Value = model.supplier;
            parameters[4].Value = model.modeName;
            parameters[5].Value = model.modeEnName;
            parameters[6].Value = model.faceValue;
            parameters[7].Value = model.isOpen;
            parameters[8].Value = DateTime.Now;
            parameters[9].Value = model.sort;


            rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channel_Update", parameters);
            bool success = rowsAffected > 0;

            return success;
        }

        #endregion

        #region Delete

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

            rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channel_Delete", parameters);
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region GetModel

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public ChannelInfo GetModel(int id)
        {
            SqlParameter[] parameters = {new SqlParameter("@id", SqlDbType.Int, 4)};
            parameters[0].Value = id;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channel_GetModel", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return GetModelFromRow(ds.Tables[0].Rows[0]);
            }
            return null;
        }

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public ChannelInfo GetModelByCode(string code)
        {
            SqlParameter[] parameters = {new SqlParameter("@code", SqlDbType.VarChar, 10)};
            parameters[0].Value = code;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channel_GetBycode", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return GetModelFromRow(ds.Tables[0].Rows[0]);
            }
            return null;
        }

        #region GetModelFromDs

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static ChannelInfo GetModelFromRow(DataRow dr)
        {
            var model = new ChannelInfo();

            if (dr["id"].ToString() != "")
            {
                model.id = int.Parse(dr["id"].ToString());
            }
            model.code = dr["code"].ToString();
            if (dr["typeId"].ToString() != "")
            {
                model.typeId = int.Parse(dr["typeId"].ToString());
            }
            if (dr["supplier"].ToString() != "")
            {
                model.supplier = int.Parse(dr["supplier"].ToString());
            }
            if (dr["suppRate"].ToString() != "")
            {
                model.supprate = Convert.ToDecimal(dr["suppRate"]);
            }
            model.modeName = dr["modeName"].ToString();
            model.modeEnName = dr["modeEnName"].ToString();
            if (dr["faceValue"].ToString() != "")
            {
                model.faceValue = int.Parse(dr["faceValue"].ToString());
            }
            if (dr["isOpen"].ToString() != "")
            {
                model.isOpen = int.Parse(dr["isOpen"].ToString());
            }
            //if (dr["addtime"].ToString() != "")
            //{
            //    model.addtime = DateTime.Parse(dr["addtime"].ToString());
            //}
            if (dr["sort"].ToString() != "")
            {
                model.sort = int.Parse(dr["sort"].ToString());
            }
            return model;
        }

        #endregion

        #endregion

        #region GetList

        /// <summary>
        ///     获得数据列表
        /// </summary>
        public DataSet GetList(int typeId)
        {
            SqlParameter[] parameters = {new SqlParameter("@typeId", SqlDbType.Int)};
            parameters[0].Value = typeId;

            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channel_GetList", parameters);
        }

        #endregion

        #region 分页

        /// <summary>
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="userid"></param>
        /// <param name="typeid"></param>
        /// <param name="facevalue"></param>
        /// <param name="chanelstatus"></param>
        /// <returns></returns>
        public DataSet GetBankChanels(int pageindex, int pagesize, int userid, int typeid, int facevalue,
            int chanelstatus)
        {
            #region

            string sql = @"select count(*) as total
from (
select
	dbo.f_getuserChanelStatus(a.isOpen,b.isOpen,c.sysIsOpen,c.userIsOpen) as chanelstatus
from
	channeltype a left join channel b on a.typeId = b.typeId
				  left join channeltypeusers c on a.typeId = c.typeId and c.userId = @userId
where
	a.release = 1 and a.typeid <= 102
	and (a.typeid = @typeid or @typeid is null)
	and (b.faceValue = @faceValue or @faceValue is null)
	) dd
where (dd.chanelstatus = @chanelstatus or @chanelstatus is null)

select typeid,code,faceValue,modetypename,chanelstatus,modeName
from (
select
	a.typeid,
	b.code,
	b.faceValue,
	a.modetypename,b.modeName,
	dbo.f_getuserChanelStatus(a.isOpen,b.isOpen,c.sysIsOpen,c.userIsOpen) as chanelstatus
	,ROW_NUMBER() OVER(ORDER BY a.typeid,b.facevalue) AS P_ROW 
from
	channeltype a left join channel b on a.typeId = b.typeId
				  left join channeltypeusers c on a.typeId = c.typeId and c.userId = @userId
where
	a.release = 1 and a.typeid <= 102
	and (a.typeid = @typeid or @typeid is null)
	and (b.faceValue = @faceValue or @faceValue is null)
	) dd
where (dd.chanelstatus = @chanelstatus or @chanelstatus is null) 
and dd.P_ROW BETWEEN @page*@pagesize+1 AND @page*@pagesize+@pagesize ";

            #endregion

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@page", pageindex - 1));
            parameters.Add(new SqlParameter("@pagesize", pagesize));
            parameters.Add(new SqlParameter("@userId", userid));
            if (typeid > 0)
                parameters.Add(new SqlParameter("@typeid", typeid));
            else
                parameters.Add(new SqlParameter("@typeid", DBNull.Value));

            if (facevalue > 0)
                parameters.Add(new SqlParameter("@facevalue", facevalue));
            else
                parameters.Add(new SqlParameter("@facevalue", DBNull.Value));

            if (chanelstatus > -1)
                parameters.Add(new SqlParameter("@chanelstatus", chanelstatus));
            else
                parameters.Add(new SqlParameter("@chanelstatus", DBNull.Value));

            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, sql, parameters.ToArray());
            return ds;
        }

        public DataTable GetCardChanels(int userid, int typeid, int facevalue, int chanelstatus)
        {
            #region

            string sql = @"
select typeid,code,faceValue,modetypename,chanelstatus
from (
select
	a.typeid,
	b.code,
	b.faceValue,
	a.modetypename,
	dbo.f_getuserChanelStatus(a.isOpen,b.isOpen,c.sysIsOpen,c.userIsOpen) as chanelstatus	
from
	channeltype a left join channel b on a.typeId = b.typeId
				  left join channeltypeusers c on a.typeId = c.typeId and c.userId = @userId
where
	a.release = 1 and a.typeid > 102
	and (a.typeid = @typeid or @typeid is null)
	and (b.faceValue = @faceValue or @faceValue is null)
	) dd
where (dd.chanelstatus = @chanelstatus or @chanelstatus is null) 
";

            #endregion

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userId", userid));
            if (typeid > 0)
                parameters.Add(new SqlParameter("@typeid", typeid));
            else
                parameters.Add(new SqlParameter("@typeid", DBNull.Value));

            if (facevalue > 0)
                parameters.Add(new SqlParameter("@facevalue", facevalue));
            else
                parameters.Add(new SqlParameter("@facevalue", DBNull.Value));

            if (chanelstatus > -1)
                parameters.Add(new SqlParameter("@chanelstatus", chanelstatus));
            else
                parameters.Add(new SqlParameter("@chanelstatus", DBNull.Value));

            DataTable ds = DataBase.ExecuteDataset(CommandType.Text, sql, parameters.ToArray()).Tables[0];
            return ds;
        }

        public DataSet GetCardChanels(int pageindex, int pagesize, int userid, int typeid, int facevalue,
            int chanelstatus)
        {
            #region

            string sql = @"select count(*) as total
from (
select
	dbo.f_getuserChanelStatus(a.isOpen,b.isOpen,c.sysIsOpen,c.userIsOpen) as chanelstatus
from
	channeltype a left join channel b on a.typeId = b.typeId
				  left join channeltypeusers c on a.typeId = c.typeId and c.userId = @userId
where
	a.release = 1 and a.typeid > 102
	and (a.typeid = @typeid or @typeid is null)
	and (b.faceValue = @faceValue or @faceValue is null)
	) dd
where (dd.chanelstatus = @chanelstatus or @chanelstatus is null)

select typeid,code,faceValue,modetypename,chanelstatus
from (
select
	a.typeid,
	b.code,
	b.faceValue,
	a.modetypename,
	dbo.f_getuserChanelStatus(a.isOpen,b.isOpen,c.sysIsOpen,c.userIsOpen) as chanelstatus
	,ROW_NUMBER() OVER(ORDER BY a.typeid,b.facevalue) AS P_ROW 
from
	channeltype a left join channel b on a.typeId = b.typeId
				  left join channeltypeusers c on a.typeId = c.typeId and c.userId = @userId
where
	a.release = 1 and a.typeid > 102
	and (a.typeid = @typeid or @typeid is null)
	and (b.faceValue = @faceValue or @faceValue is null)
	) dd
where (dd.chanelstatus = @chanelstatus or @chanelstatus is null) 
and dd.P_ROW BETWEEN @page*@pagesize+1 AND @page*@pagesize+@pagesize ";

            #endregion

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@page", pageindex - 1));
            parameters.Add(new SqlParameter("@pagesize", pagesize));
            parameters.Add(new SqlParameter("@userId", userid));
            if (typeid > 0)
                parameters.Add(new SqlParameter("@typeid", typeid));
            else
                parameters.Add(new SqlParameter("@typeid", DBNull.Value));

            if (facevalue > 0)
                parameters.Add(new SqlParameter("@facevalue", facevalue));
            else
                parameters.Add(new SqlParameter("@facevalue", DBNull.Value));

            if (chanelstatus > -1)
                parameters.Add(new SqlParameter("@chanelstatus", chanelstatus));
            else
                parameters.Add(new SqlParameter("@chanelstatus", DBNull.Value));

            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, sql, parameters.ToArray());
            return ds;
        }

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
                string tables = SqlTable;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }

                var paramList = new List<SqlParameter>();
                string userSearchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, userSearchWhere, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(SqlTableField, tables, userSearchWhere, orderby, key, pageSize,
                                 page, false);

                ds = DataBase.ExecuteDataset(CommandType.Text, sql, paramList.ToArray());
                return ds;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return ds;
            }
        }

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
                        case "id":
                            builder.Append(" AND [id] = @id");
                            parameter = new SqlParameter("@id", SqlDbType.Int);
                            parameter.Value = (int) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "typeid":
                            builder.Append(" AND [typeId] = @typeId");
                            parameter = new SqlParameter("@typeId", SqlDbType.Int, 4);
                            parameter.Value = (int) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "cardtype":
                            builder.Append(" AND [typeId] > 102 ");
                            break;
                    }
                }
            }
            return builder.ToString();
        }

        #endregion
    }
}