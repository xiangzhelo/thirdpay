/**  版本信息模板在安装目录下，可自行修改。
* supplierAccount.cs
*
* 功 能： N/A
* 类 名： supplierAccount
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-2-4 10:22:18   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
//Please add references
using DBAccess;

namespace viviapi.DAL.Supplier
{
    /// <summary>
    /// 数据访问类:supplierAccount
    /// </summary>
    public partial class SupplierAccount
    {
        public SupplierAccount()
        { }
        #region  BasicMethod

        public int Insert(viviapi.Model.Supplier.SupplierAccount model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@code", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@apiAccount", SqlDbType.VarChar,500),
					new SqlParameter("@apiKey", SqlDbType.VarChar,500),
					new SqlParameter("@userName", SqlDbType.VarChar,500),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@domain", SqlDbType.VarChar,500),
					new SqlParameter("@jumpdomain", SqlDbType.VarChar,500),
					new SqlParameter("@available", SqlDbType.Bit,1),
					new SqlParameter("@isdefault", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.code;
            parameters[2].Value = model.name;
            parameters[3].Value = model.apiAccount;
            parameters[4].Value = model.apiKey;
            parameters[5].Value = model.userName;
            parameters[6].Value = model.email;
            parameters[7].Value = model.domain;
            parameters[8].Value = model.jumpdomain;
            parameters[9].Value = model.available;
            parameters[10].Value = model.isdefault;

            object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_supplierAccount_insert", parameters);
            
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
        /// 增加一条数据
        /// </summary>
        public int Add(viviapi.Model.Supplier.SupplierAccount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into supplierAccount(");
            strSql.Append("code,name,apiAccount,apiKey,userName,email,domain,jumpdomain,available,isdefault)");
            strSql.Append(" values (");
            strSql.Append("@code,@name,@apiAccount,@apiKey,@userName,@email,@domain,@jumpdomain,@available,@isdefault)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@code", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@apiAccount", SqlDbType.VarChar,100),
					new SqlParameter("@apiKey", SqlDbType.VarChar,50),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@domain", SqlDbType.VarChar,500),
					new SqlParameter("@jumpdomain", SqlDbType.VarChar,500),
					new SqlParameter("@available", SqlDbType.Bit,1),
					new SqlParameter("@isdefault", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.code;
            parameters[1].Value = model.name;
            parameters[2].Value = model.apiAccount;
            parameters[3].Value = model.apiKey;
            parameters[4].Value = model.userName;
            parameters[5].Value = model.email;
            parameters[6].Value = model.domain;
            parameters[7].Value = model.jumpdomain;
            parameters[8].Value = model.available;
            parameters[9].Value = model.isdefault;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(viviapi.Model.Supplier.SupplierAccount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update supplierAccount set ");
            strSql.Append("code=@code,");
            strSql.Append("name=@name,");
            strSql.Append("apiAccount=@apiAccount,");
            strSql.Append("apiKey=@apiKey,");
            strSql.Append("userName=@userName,");
            strSql.Append("email=@email,");
            strSql.Append("domain=@domain,");
            strSql.Append("jumpdomain=@jumpdomain,");
            strSql.Append("available=@available,");
            strSql.Append("isdefault=@isdefault");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@code", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@apiAccount", SqlDbType.VarChar,100),
					new SqlParameter("@apiKey", SqlDbType.VarChar,50),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@domain", SqlDbType.VarChar,500),
					new SqlParameter("@jumpdomain", SqlDbType.VarChar,500),
					new SqlParameter("@available", SqlDbType.Bit,1),
					new SqlParameter("@isdefault", SqlDbType.TinyInt,1),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.code;
            parameters[1].Value = model.name;
            parameters[2].Value = model.apiAccount;
            parameters[3].Value = model.apiKey;
            parameters[4].Value = model.userName;
            parameters[5].Value = model.email;
            parameters[6].Value = model.domain;
            parameters[7].Value = model.jumpdomain;
            parameters[8].Value = model.available;
            parameters[9].Value = model.isdefault;
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
            strSql.Append("delete from supplierAccount ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
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
            strSql.Append("delete from supplierAccount ");
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
        public viviapi.Model.Supplier.SupplierAccount GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,code,name,apiAccount,apiKey,userName,email,domain,jumpdomain,available,isdefault from supplierAccount ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            viviapi.Model.Supplier.SupplierAccount model = new viviapi.Model.Supplier.SupplierAccount();
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

        public viviapi.Model.Supplier.SupplierAccount GetModelByDomain(int code, string domain)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@code", SqlDbType.Int,4)
                    ,new SqlParameter("@domain", SqlDbType.VarChar,500)
			};
            parameters[0].Value = code;
            parameters[1].Value = domain;

            viviapi.Model.Supplier.SupplierAccount model = new viviapi.Model.Supplier.SupplierAccount();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_supplierAccount_GetModelBydomain",
                parameters);
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
        public viviapi.Model.Supplier.SupplierAccount DataRowToModel(DataRow row)
        {
            viviapi.Model.Supplier.SupplierAccount model = new viviapi.Model.Supplier.SupplierAccount();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["code"] != null && row["code"].ToString() != "")
                {
                    model.code = int.Parse(row["code"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["apiAccount"] != null)
                {
                    model.apiAccount = row["apiAccount"].ToString();
                }
                if (row["apiKey"] != null)
                {
                    model.apiKey = row["apiKey"].ToString();
                }
                if (row["userName"] != null)
                {
                    model.userName = row["userName"].ToString();
                }
                if (row["email"] != null)
                {
                    model.email = row["email"].ToString();
                }
                if (row["domain"] != null)
                {
                    model.domain = row["domain"].ToString();
                }
                if (row["jumpdomain"] != null)
                {
                    model.jumpdomain = row["jumpdomain"].ToString();
                }
                if (row["available"] != null && row["available"].ToString() != "")
                {
                    if ((row["available"].ToString() == "1") || (row["available"].ToString().ToLower() == "true"))
                    {
                        model.available = true;
                    }
                    else
                    {
                        model.available = false;
                    }
                }
                if (row["isdefault"] != null && row["isdefault"].ToString() != "")
                {
                    model.isdefault = int.Parse(row["isdefault"].ToString());
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
            strSql.Append("select id,code,name,apiAccount,apiKey,userName,email,domain,jumpdomain,available,isdefault ");
            strSql.Append(" FROM supplierAccount ");
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
            strSql.Append(" id,code,name,apiAccount,apiKey,userName,email,domain,jumpdomain,available,isdefault ");
            strSql.Append(" FROM supplierAccount ");
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
            strSql.Append("select count(1) FROM supplierAccount ");
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
            strSql.Append(")AS Row, T.*  from supplierAccount T ");
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
            parameters[0].Value = "supplierAccount";
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

