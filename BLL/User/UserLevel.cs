using System;
using System.Collections.Generic;
using System.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.User
{
    /// <summary>
    /// userLevel
    /// </summary>
    public partial class UserLevel
    {
        private readonly viviapi.DAL.User.UserLevel dal = new viviapi.DAL.User.UserLevel();
        public UserLevel()
        { }

        public static UserLevel Instance
        {
            get
            {
                var instance = new UserLevel();
                return instance;
            }
        }

        #region  BasicMethod

        public int Insert(viviapi.Model.User.UserLevel model)
        {
            try
            {
                return dal.Insert(model);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(viviapi.Model.User.UserLevel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(viviapi.Model.User.UserLevel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            try
            {
                return dal.Delete(id);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }

        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        public string GetLevelName(int level)
        {
            try
            {
                if (level <= 0)
                    return "";

                return dal.GetLevelName(level);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return "";
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public viviapi.Model.User.UserLevel GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
           
        }

        ///// <summary>
        ///// 得到一个对象实体，从缓存中
        ///// </summary>
        //public viviapi.Model.User.userLevel GetModelByCache(int id)
        //{

        //    string CacheKey = "userLevelModel-" + id;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(id);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch{}
        //    }
        //    return (viviapi.Model.User.userLevel)objModel;
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
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
        public List<viviapi.Model.User.UserLevel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<viviapi.Model.User.UserLevel> DataTableToList(DataTable dt)
        {
            List<viviapi.Model.User.UserLevel> modelList = new List<viviapi.Model.User.UserLevel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                viviapi.Model.User.UserLevel model;
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

