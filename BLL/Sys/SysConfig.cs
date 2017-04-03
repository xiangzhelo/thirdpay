using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Sys
{
    /// <summary>
    /// sysconfig
    /// </summary>
    public partial class SysConfig
    {
        private readonly DAL.Sys.SysConfig dal = new DAL.Sys.SysConfig();

        public static string SysconfigCachekey = Sys.Constant.CacheMark + "SYSCONFIG";

        internal static string SqlTable = "sysconfig";
        internal static string SqlTableField = @"[key],[value]";

        public static SysConfig Instance
        {
            get
            {
                var instance = new SysConfig();
                return instance;
            }
        }


        public SysConfig()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Sys.SysConfig model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Sys.SysConfig model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Update(string key, string value)
        {
            try
            {
                return dal.Update(key, value);
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
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Sys.SysConfig GetModel(int id)
        {

            return dal.GetModel(id);
        }

        public string GetValue(string key)
        {
            try
            {
                DataSet data = GetCacheList();

                if (data == null)
                    return string.Empty;

                DataRow[] result = data.Tables[0].Select("[key]='" + key+"'");

                if (result.Length < 1)
                    return string.Empty;

                return result[0]["value"].ToString();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public DataSet GetCacheList()
        {
            try
            {
                string cacheKey = SysconfigCachekey;

                var data = new DataSet();
                data = (DataSet)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

                if (data == null)
                {
                    SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SqlTable, SqlTableField, string.Empty, null);

                    var strSql = new StringBuilder();
                    strSql.Append(" select [key],[value] ");
                    strSql.Append(" FROM [sysconfig] ");

                    data = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());

                    viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, data);
                }
                return data;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Sys.SysConfig> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Sys.SysConfig> DataTableToList(DataTable dt)
        {
            List<Model.Sys.SysConfig> modelList = new List<Model.Sys.SysConfig>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Sys.SysConfig model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

