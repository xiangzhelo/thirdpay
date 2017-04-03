using System;
using System.Data;
using System.Collections.Generic;

using viviapi.Model;

namespace viviapi.BLL.Sys
{
	/// <summary>
	/// 
	/// </summary>
	public partial class SysMailConfig
	{
		private readonly viviapi.DAL.Sys.SysMailConfig dal = new viviapi.DAL.Sys.SysMailConfig();
		public SysMailConfig()
		{}
		#region  BasicMethod

	    public int Insert(Model.Sys.SysMailConfig model)
	    {
            return dal.Insert(model);
	    }

	    /// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(viviapi.Model.Sys.SysMailConfig model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(viviapi.Model.Sys.SysMailConfig model)
		{
			return dal.Update(model);
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
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.Sys.SysMailConfig GetModel(int id)
		{
			return dal.GetModel(id);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
	    public Model.Sys.SysMailConfig GetDefaultModel()
	    {
            return dal.GetDefaultModel();
	    }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Model.Sys.SysMailConfig GetDefaultByCache()
        {
            string cacheKey = "SysMailConfigModel-" + "GetDefaultModel";
            object objModel = Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetDefaultModel();
                    if (objModel != null)
                    {
                        int modelCache = viviapi.SysConfig.RuntimeSetting.ModelCache;

                        Cache.WebCache.GetCacheService().AddObject(cacheKey, objModel, modelCache);
                    }
                }
                catch { }
            }
            return (viviapi.Model.Sys.SysMailConfig)objModel;
        }

	    /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public viviapi.Model.Sys.SysMailConfig GetModelByCache(int id)
        {
            string cacheKey = "SysMailConfigModel-" + id;
            object objModel = Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(id);
                    if (objModel != null)
                    {
                        int modelCache = viviapi.SysConfig.RuntimeSetting.ModelCache;
                        //viviapi.Common.DataCache.SetCache(cacheKey, objModel, DateTime.Now.AddMinutes(modelCache), TimeSpan.Zero);

                        Cache.WebCache.GetCacheService().AddObject(cacheKey, objModel, modelCache);
                    }
                }
                catch { }
            }
            return (viviapi.Model.Sys.SysMailConfig)objModel;
        }

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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<viviapi.Model.Sys.SysMailConfig> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<viviapi.Model.Sys.SysMailConfig> DataTableToList(DataTable dt)
		{
			List<viviapi.Model.Sys.SysMailConfig> modelList = new List<viviapi.Model.Sys.SysMailConfig>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				viviapi.Model.Sys.SysMailConfig model;
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
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

