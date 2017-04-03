using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DBAccess;
using viviLib;
using viviLib.ExceptionHandling;
using viviapi.Model;
using viviapi.Model.Channel;

namespace viviapi.BLL.Channel
{
    /// <summary>
    /// 数据访问类:channelsupplier
    /// </summary>
    public partial class Channelsupplier
    {
        public Channelsupplier()
        { }
        #region  BasicMethod

        ///// <summary>
        ///// 得到最大ID
        ///// </summary>
        //public int GetMaxId()
        //{
        //    return DbHelperSQL.GetMaxID("typeid", "channelsupplier");
        //}

        ///// <summary>
        ///// 是否存在该记录
        ///// </summary>
        //public bool Exists(int typeid, int suppid)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) from channelsupplier");
        //    strSql.Append(" where typeid=@typeid and suppid=@suppid ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@typeid", SqlDbType.Int,4),
        //            new SqlParameter("@suppid", SqlDbType.Int,4)			};
        //    parameters[0].Value = typeid;
        //    parameters[1].Value = suppid;

        //    return DbHelperSQL.Exists(strSql.ToString(), parameters);
        //}

        public static bool Insert(Model.Channel.ChannelSupplier model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@typeid", SqlDbType.Int,4),
					new SqlParameter("@suppid", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@isopen", SqlDbType.Bit,1),
					new SqlParameter("@payrate", SqlDbType.Decimal,9),new SqlParameter("@isdefault", SqlDbType.Bit,1)};
            parameters[0].Value = model.typeid;
            parameters[1].Value = model.suppid;
            parameters[2].Value = model.userid;
            parameters[3].Value = model.isopen;
            parameters[4].Value = model.payrate;
            parameters[5].Value = model.isdefault;

            int rows = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channelsupplier_Insert", parameters);
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
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.Channel.ChannelSupplier model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into channelsupplier(");
            strSql.Append("typeid,suppid,userid,isopen,payrate)");
            strSql.Append(" values (");
            strSql.Append("@typeid,@suppid,@userid,@isopen,@payrate)");
            SqlParameter[] parameters = {
					new SqlParameter("@typeid", SqlDbType.Int,4),
					new SqlParameter("@suppid", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@isopen", SqlDbType.Bit,1),
					new SqlParameter("@payrate", SqlDbType.Decimal,9)};
            parameters[0].Value = model.typeid;
            parameters[1].Value = model.suppid;
            parameters[2].Value = model.userid;
            parameters[3].Value = model.isopen;
            parameters[4].Value = model.payrate;

            int rows = DataBase.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Channel.ChannelSupplier model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update channelsupplier set ");
            strSql.Append("userid=@userid,");
            strSql.Append("isopen=@isopen,");
            strSql.Append("payrate=@payrate");
            strSql.Append(" where typeid=@typeid and suppid=@suppid ");
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@isopen", SqlDbType.Bit,1),
					new SqlParameter("@payrate", SqlDbType.Decimal,9),
					new SqlParameter("@typeid", SqlDbType.Int,4),
					new SqlParameter("@suppid", SqlDbType.Int,4)};
            parameters[0].Value = model.userid;
            parameters[1].Value = model.isopen;
            parameters[2].Value = model.payrate;
            parameters[3].Value = model.typeid;
            parameters[4].Value = model.suppid;

            int rows = DataBase.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
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
        public bool Delete(int typeid, int suppid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from channelsupplier ");
            strSql.Append(" where typeid=@typeid and suppid=@suppid ");
            SqlParameter[] parameters = {
					new SqlParameter("@typeid", SqlDbType.Int,4),
					new SqlParameter("@suppid", SqlDbType.Int,4)			};
            parameters[0].Value = typeid;
            parameters[1].Value = suppid;

            int rows = DataBase.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
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
        public Model.Channel.ChannelSupplier GetModel(int typeid, int suppid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 typeid,suppid,userid,isopen,payrate from channelsupplier ");
            strSql.Append(" where typeid=@typeid and suppid=@suppid ");
            SqlParameter[] parameters = {
					new SqlParameter("@typeid", SqlDbType.Int,4),
					new SqlParameter("@suppid", SqlDbType.Int,4)			};
            parameters[0].Value = typeid;
            parameters[1].Value = suppid;

            Model.Channel.ChannelSupplier model = new Model.Channel.ChannelSupplier();
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
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
        public Model.Channel.ChannelSupplier DataRowToModel(DataRow row)
        {
            Model.Channel.ChannelSupplier model = new Model.Channel.ChannelSupplier();
            if (row != null)
            {
                if (row["typeid"] != null && row["typeid"].ToString() != "")
                {
                    model.typeid = int.Parse(row["typeid"].ToString());
                }
                if (row["suppid"] != null && row["suppid"].ToString() != "")
                {
                    model.suppid = int.Parse(row["suppid"].ToString());
                }
                if (row["userid"] != null && row["userid"].ToString() != "")
                {
                    model.userid = int.Parse(row["userid"].ToString());
                }
                if (row["isopen"] != null && row["isopen"].ToString() != "")
                {
                    if ((row["isopen"].ToString() == "1") || (row["isopen"].ToString().ToLower() == "true"))
                    {
                        model.isopen = true;
                    }
                    else
                    {
                        model.isopen = false;
                    }
                }
                if (row["payrate"] != null && row["payrate"].ToString() != "")
                {
                    model.payrate = decimal.Parse(row["payrate"].ToString());
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
            strSql.Append("select typeid,suppid,userid,isopen,payrate ");
            strSql.Append(" FROM channelsupplier ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(int typeid,int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT A.[typeid]
      ,B.[id]     
	  ,B.[name]
	  ,B.[name1],B.code,[userid]
      ,isnull([isopen],0) as isopen
      ,isnull([payrate],0)*100 as payrate
      ,isnull(isdefault,0) as isdefault
  FROM supplier B  LEFT JOIN  [channelsupplier] A
		ON A.suppid = B.code and A.typeid = @typeid and A.userid=@userid
  where B.iscard = 1 ");

            SqlParameter[] parameters = {
					new SqlParameter("@typeid", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4)			};
            parameters[0].Value = typeid;
            parameters[1].Value = 0;
            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList1(int typeid, int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT suppid,name1,payrate,isnull(isdefault,0) as isdefault  from V_channelsupplier where typeid=@typeid and userid=@userid and isopen =1");

            SqlParameter[] parameters = {
					new SqlParameter("@typeid", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4)			};
            parameters[0].Value = typeid;
            parameters[1].Value = 0;
            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        ///
        /// </summary>
        public static decimal GetPayRate(int typeid, int suppid)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT payrate from channelsupplier where typeid=@typeid and userid=0 and suppid =@suppid");

                SqlParameter[] parameters = {
					new SqlParameter("@typeid", SqlDbType.Int,4),
					new SqlParameter("@suppid", SqlDbType.Int,4)			};
                parameters[0].Value = typeid;
                parameters[1].Value = suppid;

                object result = DataBase.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
                if (result == DBNull.Value)
                    return 0M;
                else
                    return Convert.ToDecimal(result);
            }
            catch (Exception ex)
            {
                return 0M;
            }
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
            strSql.Append(" typeid,suppid,userid,isopen,payrate ");
            strSql.Append(" FROM channelsupplier ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM channelsupplier ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DataBase.ExecuteScalar(CommandType.Text, strSql.ToString());
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
                strSql.Append("order by T.suppid desc");
            }
            strSql.Append(")AS Row, T.*  from channelsupplier T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DataBase.ExecuteDataset(CommandType.Text,strSql.ToString());
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
            parameters[0].Value = "channelsupplier";
            parameters[1].Value = "suppid";
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
    }
}

