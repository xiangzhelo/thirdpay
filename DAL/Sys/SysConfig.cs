using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
//Please add references
using DBAccess;

namespace viviapi.DAL.Sys
{
	/// <summary>
	/// 数据访问类:sysconfig
	/// </summary>
	public partial class SysConfig
	{
		public SysConfig()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.Sys.SysConfig model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into sysconfig(");
			strSql.Append("type,key,value,desc)");
			strSql.Append(" values (");
			strSql.Append("@type,@key,@value,@desc)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@type", SqlDbType.Int,4),
					new SqlParameter("@key", SqlDbType.VarChar,50),
					new SqlParameter("@value", SqlDbType.VarChar,100),
					new SqlParameter("@desc", SqlDbType.VarChar,100)};
			parameters[0].Value = model.type;
			parameters[1].Value = model.key;
			parameters[2].Value = model.value;
			parameters[3].Value = model.desc;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.Sys.SysConfig model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update sysconfig set ");
			strSql.Append("type=@type,");
			strSql.Append("key=@key,");
			strSql.Append("value=@value,");
			strSql.Append("desc=@desc");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@type", SqlDbType.Int,4),
					new SqlParameter("@key", SqlDbType.VarChar,50),
					new SqlParameter("@value", SqlDbType.VarChar,100),
					new SqlParameter("@desc", SqlDbType.VarChar,100),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.type;
			parameters[1].Value = model.key;
			parameters[2].Value = model.value;
			parameters[3].Value = model.desc;
			parameters[4].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
        public bool Update(string key,string value)
        {
            var strSql = new StringBuilder();
            strSql.Append("update sysconfig set ");
            strSql.Append("[value]=@value");
            strSql.Append(" where [key]=@key");
            SqlParameter[] parameters = {
					new SqlParameter("@key", SqlDbType.VarChar,50),
					new SqlParameter("@value", SqlDbType.VarChar,100)};
            parameters[0].Value = key;
            parameters[1].Value = value;

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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from sysconfig ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from sysconfig ");
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
		public Model.Sys.SysConfig GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,type,key,value,desc from sysconfig ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Model.Sys.SysConfig model=new Model.Sys.SysConfig();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
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
		public Model.Sys.SysConfig DataRowToModel(DataRow row)
		{
			Model.Sys.SysConfig model=new Model.Sys.SysConfig();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["type"]!=null && row["type"].ToString()!="")
				{
					model.type=int.Parse(row["type"].ToString());
				}
				if(row["key"]!=null)
				{
					model.key=row["key"].ToString();
				}
				if(row["value"]!=null)
				{
					model.value=row["value"].ToString();
				}
				if(row["desc"]!=null)
				{
					model.desc=row["desc"].ToString();
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
			strSql.Append("select id,type,[key],[value],[desc] ");
			strSql.Append(" FROM sysconfig ");
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
			strSql.Append(" id,type,key,value,desc ");
			strSql.Append(" FROM sysconfig ");
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
			strSql.Append("select count(1) FROM sysconfig ");
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
			strSql.Append(")AS Row, T.*  from sysconfig T ");
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
			parameters[0].Value = "sysconfig";
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
	}
}

