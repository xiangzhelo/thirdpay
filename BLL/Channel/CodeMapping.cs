using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using DBAccess;
using viviapi.Model.Channel;
using viviLib.ExceptionHandling;

////
namespace viviapi.BLL.Channel
{
    /// <summary>
    /// 产品银行与对应接品商类
    /// </summary>
    public class CodeMappingFactory
    {
        #region  Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static int Add(CodeMappingInfo model)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@pmodeCode", SqlDbType.VarChar,20),
					new SqlParameter("@suppId", SqlDbType.Int,4),
					new SqlParameter("@suppCode", SqlDbType.VarChar,20)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.pmodeCode;
                parameters[2].Value = model.suppId;
                parameters[3].Value = model.suppCode;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_codemapping_ADD", parameters);
                return (int)parameters[0].Value;

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public static bool Update(CodeMappingInfo model)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@pmodeCode", SqlDbType.VarChar,20),
					new SqlParameter("@suppId", SqlDbType.Int,4),
					new SqlParameter("@suppCode", SqlDbType.VarChar,20)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.pmodeCode;
                parameters[2].Value = model.suppId;
                parameters[3].Value = model.suppCode;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_codemapping_Update", parameters);
                if (rowsAffected > 0)
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
        public static bool Delete(int id)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
                parameters[0].Value = id;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_codemapping_Delete", parameters);
                if (rowsAffected > 0)
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
        /// 得到一个对象实体
        /// </summary>
        public static CodeMappingInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
                parameters[0].Value = id;

                CodeMappingInfo model = new CodeMappingInfo();
                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_codemapping_GetModel", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                    }
                    model.pmodeCode = ds.Tables[0].Rows[0]["pmodeCode"].ToString();
                    if (ds.Tables[0].Rows[0]["suppId"].ToString() != "")
                    {
                        model.suppId = int.Parse(ds.Tables[0].Rows[0]["suppId"].ToString());
                    }
                    model.suppCode = ds.Tables[0].Rows[0]["suppCode"].ToString();
                    return model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select [id]
      ,[pmodeCode]
      ,[suppId]
      ,[suppCode]
      ,[SuppName]
      ,[modeName] ");
            strSql.Append(" FROM V_Codemapping ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), null);
        }
    }
}
