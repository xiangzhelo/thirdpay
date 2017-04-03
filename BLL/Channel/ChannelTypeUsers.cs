using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using DBAccess;
using viviLib;
using viviLib.ExceptionHandling;
using viviapi.Model.Channel;

namespace viviapi.BLL.Channel
{
    /// <summary>
    /// 用户通道
    /// 
    /// </summary>
    public class ChannelTypeUsers
    {
        public static string ChannelTypeUsers_CACHEKEY = Sys.Constant.CacheMark + "CHANNEL_TYPE_USER_{0}";
        internal static string SQL_TABLE = "channeltypeusers";
        internal static string SQL_TABLE_FIELD = "[id],[typeId],[userId],[userIsOpen],[suppid],[sysIsOpen],[updateTime]";

        #region Add
        /// <summary>
        /// 增加一条数据,系统会自动检查是否存在相同用户的相同通道记录，如果存在则自动转为修改
        /// </summary>
        public static int Add(ChannelTypeUserInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@typeId", SqlDbType.Int,4),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@userIsOpen", SqlDbType.Bit,1),
					new SqlParameter("@sysIsOpen", SqlDbType.Bit,1),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@updateTime", SqlDbType.DateTime)
                                            };
                parameters[0].Value = model.typeId;
                parameters[1].Value = model.userId;
                parameters[2].Value = model.userIsOpen;
                parameters[3].Value = model.sysIsOpen;
                parameters[4].Value = model.addTime;
                parameters[5].Value = model.updateTime;

                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_channeltypeusers_add", parameters);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    ClearCache(model.userId);
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

        #region AddSupp
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int AddSupp(ChannelTypeUserInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@typeId", SqlDbType.Int,4),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@suppid", SqlDbType.Int,4),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@updateTime", SqlDbType.DateTime)
                                            };
                parameters[0].Value = model.typeId;
                parameters[1].Value = model.userId;
                parameters[2].Value = model.suppid;
                parameters[3].Value = model.addTime;
                parameters[4].Value = model.updateTime;

                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_channeltypeusers_addsuppid", parameters);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    ClearCache(model.userId);
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

        #region Exists
        /// <summary>
        /// 
        /// </summary>
        public static int Exists(int userid)
        {
            try
            {
                SqlParameter[] parameters = {					
					new SqlParameter("@userId", SqlDbType.Int,4)
                                            };
                parameters[0].Value = userid;

                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_channeltypeusers_exists", parameters);
                if (obj == null || obj == DBNull.Value)
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

        #region Setting
        /// <summary>
        /// 设置开启状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isOpen">
        /// 0 关闭
        /// 1 开启
        /// 3 恢复默认
        /// </param>
        /// <returns></returns>        
        public static bool Setting(int userId, int isOpen)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
                    new SqlParameter("@isOpen", SqlDbType.TinyInt),
                    new SqlParameter("@addtime", SqlDbType.DateTime)};
                parameters[0].Value = userId;
                parameters[1].Value = isOpen;
                parameters[2].Value = DateTime.Now;

                bool success = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channeltypeusers_Setting", parameters) > 0;

                if (success)
                {
                    ClearCache(userId);
                }
                return success;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion        

        #region GetModel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ChannelTypeUserInfo GetModel(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.Int, 4) };

            parameters[0].Value = id;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channeltypeusers_GetModel", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return GetModelFromRow(ds.Tables[0].Rows[0]);
            }
            return null;    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static ChannelTypeUserInfo GetModel(int userId, int typeId)
        {
            SqlParameter[] parameters = {new SqlParameter("@userId", SqlDbType.Int,4),
                    new SqlParameter("@typeId", SqlDbType.Int,4)};
            parameters[0].Value = userId;
            parameters[1].Value = typeId;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channeltypeusers_GetbyKey", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return GetModelFromRow(ds.Tables[0].Rows[0]);
            }
            return null;    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static ChannelTypeUserInfo GetCacheModel(int userId, int typeId)
        {
            DataTable data = ChannelTypeUsers.GetList(userId, true);
            if (data == null || data.Rows.Count <= 0)
                return null;

            DataRow[] chanelUser = data.Select("typeId=" + typeId.ToString());
            if (chanelUser != null && chanelUser.Length > 0)
            {
                return GetModelFromRow(chanelUser[0]);
            }
            return null;
        }

        #region GetModelFromRow
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static ChannelTypeUserInfo GetModelFromRow(DataRow dr)
        {
            ChannelTypeUserInfo model = new ChannelTypeUserInfo();
            if (dr["id"].ToString() != "")
            {
                model.id = int.Parse(dr["id"].ToString());
            }
            if (dr["typeId"].ToString() != "")
            {
                model.typeId = int.Parse(dr["typeId"].ToString());
            }

            if (dr["userId"].ToString() != "")
            {
                model.userId = int.Parse(dr["userId"].ToString());
            }
            if (dr["userIsOpen"].ToString() != "")
            {
                if ((dr["userIsOpen"].ToString() == "1") || (dr["userIsOpen"].ToString().ToLower() == "true"))
                {
                    model.userIsOpen = true;
                }
                else
                {
                    model.userIsOpen = false;
                }
            }
            else
            {
                model.userIsOpen = null;
            }
            if (dr["sysIsOpen"].ToString() != "")
            {
                if ((dr["sysIsOpen"].ToString() == "1") || (dr["sysIsOpen"].ToString().ToLower() == "true"))
                {
                    model.sysIsOpen = true;
                }
                else
                {
                    model.sysIsOpen = false;
                }
            }
            else
            {
                model.sysIsOpen = null;
            }
            if (dr.Table.Columns.Contains("addTime") && dr["addTime"].ToString() != "")
            {
                model.addTime = DateTime.Parse(dr["addTime"].ToString());
            }
            if (dr.Table.Columns.Contains("updateTime") && dr["updateTime"].ToString() != "")
            {
                model.updateTime = DateTime.Parse(dr["updateTime"].ToString());
            }
            model.suppid = null;
            if (dr["suppid"].ToString() != "")
            {
                model.suppid = int.Parse(dr["suppid"].ToString());
            }
            return model;
        }
        #endregion
        #endregion  成员方法

        #region GetList
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataTable GetList(int userId, bool iscache)
        {
            try
            {
                string cacheKey = string.Format(ChannelTypeUsers_CACHEKEY, userId);

                DataSet ds = new DataSet();

                if (iscache)
                    ds = (DataSet)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

                if (ds == null || !iscache)
                {
                    IDictionary<string, object> sqldepparms = new Dictionary<string, object>();
                    sqldepparms.Add("userId", userId);
                    SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_TABLE, SQL_TABLE_FIELD, "[userId]=@userId", sqldepparms);

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select [id],[suppid],[typeId],[userId],[userIsOpen],[sysIsOpen],addTime,updateTime ");
                    strSql.Append(" FROM [ChannelTypeUsers] ");

                    ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString() + " where userId=" + userId.ToString());

                    if (iscache)
                        viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, ds);
                }

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        static void ClearCache(int userId)
        {
            string cacheKey = String.Format(ChannelTypeUsers_CACHEKEY, userId);
            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cacheKey);
        }
    }
}
