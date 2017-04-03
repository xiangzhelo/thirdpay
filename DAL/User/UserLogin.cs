using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBAccess;

namespace viviapi.DAL.User
{
	/// <summary>
	/// 数据访问类:userlogin
	/// </summary>
	public partial class UserLogin
	{
        public UserLogin()
		{}
		#region  BasicMethod
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.User.UserLogin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into userlogin(");
			strSql.Append("userId,lastLoginIp,lastLoginTime,currentLoginIp,currentLoginTime,sessionId,loginType)");
			strSql.Append(" values (");
			strSql.Append("@userId,@lastLoginIp,@lastLoginTime,@currentLoginIp,@currentLoginTime,@sessionId,@loginType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@lastLoginIp", SqlDbType.VarChar,50),
					new SqlParameter("@lastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@currentLoginIp", SqlDbType.VarChar,50),
					new SqlParameter("@currentLoginTime", SqlDbType.DateTime),
					new SqlParameter("@sessionId", SqlDbType.VarChar,60),
					new SqlParameter("@loginType", SqlDbType.TinyInt,1)};
			parameters[0].Value = model.userId;
			parameters[1].Value = model.lastLoginIp;
			parameters[2].Value = model.lastLoginTime;
			parameters[3].Value = model.currentLoginIp;
			parameters[4].Value = model.currentLoginTime;
			parameters[5].Value = model.sessionId;
			parameters[6].Value = model.loginType;

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
		public bool Update(Model.User.UserLogin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update userlogin set ");
			strSql.Append("userId=@userId,");
			strSql.Append("lastLoginIp=@lastLoginIp,");
			strSql.Append("lastLoginTime=@lastLoginTime,");
			strSql.Append("currentLoginIp=@currentLoginIp,");
			strSql.Append("currentLoginTime=@currentLoginTime,");
			strSql.Append("sessionId=@sessionId,");
			strSql.Append("loginType=@loginType");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@lastLoginIp", SqlDbType.VarChar,50),
					new SqlParameter("@lastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@currentLoginIp", SqlDbType.VarChar,50),
					new SqlParameter("@currentLoginTime", SqlDbType.DateTime),
					new SqlParameter("@sessionId", SqlDbType.VarChar,60),
					new SqlParameter("@loginType", SqlDbType.TinyInt,1),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.userId;
			parameters[1].Value = model.lastLoginIp;
			parameters[2].Value = model.lastLoginTime;
			parameters[3].Value = model.currentLoginIp;
			parameters[4].Value = model.currentLoginTime;
			parameters[5].Value = model.sessionId;
			parameters[6].Value = model.loginType;
			parameters[7].Value = model.id;

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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from userlogin ");
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
			strSql.Append("delete from userlogin ");
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
        public Model.User.UserLogin GetModel(int userId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,userId,lastLoginIp,lastLoginTime,currentLoginIp,currentLoginTime,sessionId,loginType from userlogin ");
            strSql.Append(" where userId=@userId");
			SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4)
			};
            parameters[0].Value = userId;

			Model.User.UserLogin model=new Model.User.UserLogin();
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
		public Model.User.UserLogin DataRowToModel(DataRow row)
		{
			Model.User.UserLogin model=new Model.User.UserLogin();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["userId"]!=null && row["userId"].ToString()!="")
				{
					model.userId=int.Parse(row["userId"].ToString());
				}
				if(row["lastLoginIp"]!=null)
				{
					model.lastLoginIp=row["lastLoginIp"].ToString();
				}
				if(row["lastLoginTime"]!=null && row["lastLoginTime"].ToString()!="")
				{
					model.lastLoginTime=DateTime.Parse(row["lastLoginTime"].ToString());
				}
				if(row["currentLoginIp"]!=null)
				{
					model.currentLoginIp=row["currentLoginIp"].ToString();
				}
				if(row["currentLoginTime"]!=null && row["currentLoginTime"].ToString()!="")
				{
					model.currentLoginTime=DateTime.Parse(row["currentLoginTime"].ToString());
				}
				if(row["sessionId"]!=null)
				{
					model.sessionId=row["sessionId"].ToString();
				}
				if(row["loginType"]!=null && row["loginType"].ToString()!="")
				{
					model.loginType=int.Parse(row["loginType"].ToString());
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
			strSql.Append("select id,userId,lastLoginIp,lastLoginTime,currentLoginIp,currentLoginTime,sessionId,loginType ");
			strSql.Append(" FROM userlogin ");
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
			strSql.Append(" id,userId,lastLoginIp,lastLoginTime,currentLoginIp,currentLoginTime,sessionId,loginType ");
			strSql.Append(" FROM userlogin ");
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
			strSql.Append("select count(1) FROM userlogin ");
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
			strSql.Append(")AS Row, T.*  from userlogin T ");
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
			parameters[0].Value = "userlogin";
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

