using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using DBAccess;
using viviLib;
using viviLib.ExceptionHandling;
using viviapi.Model;
using viviapi.Model.Channel;

namespace viviapi.BLL.order
{
    /// <summary>
    /// 
    /// </summary>
    public class reconciliation_temp
    {
        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public  int Add(Model.order.reconciliation_temp model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into reconciliation_temp(");
                strSql.Append("serverid,orderid,count)");
                strSql.Append(" values (");
                strSql.Append("@serverid,@orderid,@count)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@serverid", SqlDbType.VarChar,50),
					new SqlParameter("@orderid", SqlDbType.VarChar,30),
					new SqlParameter("@count", SqlDbType.Int,4)};
                parameters[0].Value = model.serverid;
                parameters[1].Value = model.orderid;
                parameters[2].Value = model.count;
                object obj = DataBase.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(string orderid,string callback)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update reconciliation_temp set ");
                strSql.Append("count=count+1,callback=@callback");
                strSql.Append(" where orderid=@orderid");
                SqlParameter[] parameters = {
					new SqlParameter("@orderid", SqlDbType.VarChar,30),
                    new SqlParameter("@callback", SqlDbType.VarChar,2000)                                            
                                            };
                parameters[0].Value = orderid;
                parameters[1].Value = callback;

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
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string orderid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from reconciliation_temp ");
            strSql.Append(" where orderid=@orderid ");
            SqlParameter[] parameters = {
					new SqlParameter("@orderid", SqlDbType.VarChar,30)			};
            parameters[0].Value = orderid;

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
        public Model.order.reconciliation_temp GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,serverid,orderid,count from reconciliation_temp ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Model.order.reconciliation_temp model = new Model.order.reconciliation_temp();
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
        public Model.order.reconciliation_temp DataRowToModel(DataRow row)
        {
            Model.order.reconciliation_temp model = new Model.order.reconciliation_temp();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["serverid"] != null)
                {
                    model.serverid = row["serverid"].ToString();
                }
                if (row["orderid"] != null)
                {
                    model.orderid = row["orderid"].ToString();
                }
                if (row["count"] != null && row["count"].ToString() != "")
                {
                    model.count = int.Parse(row["count"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id,serverid,orderid,count ");
                strSql.Append(" FROM reconciliation_temp ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
    }
}
