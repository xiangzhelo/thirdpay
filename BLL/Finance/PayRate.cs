using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Finance
{
    /// <summary>
    /// PayRate
    /// </summary>
    public partial class PayRate
    {
        private readonly viviapi.DAL.Finance.PayRate dal = new viviapi.DAL.Finance.PayRate();
        public PayRate()
        { }

        public static PayRate Instance
        {
            get
            {
                var instance = new PayRate();
                return instance;
            }
        }

        #region  BasicMethod

        public int Insert(viviapi.Model.Finance.PayRate model)
        {
            try
            {
                return dal.Insert(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
           
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(viviapi.Model.Finance.PayRate model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(viviapi.Model.Finance.PayRate model)
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
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public viviapi.Model.Finance.PayRate GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
           
        }
        public viviapi.Model.Finance.PayRate GetModel(byte rateType, int billId)
        {
            try
            {
                return dal.GetModel(rateType, billId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        public viviapi.Model.Finance.PayRate GetModelByUser(int userId)
        {
            try
            {
                return dal.GetModelByUser(userId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

       

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public viviapi.Model.Finance.PayRate GetModelByCache(int id)
        {
            string cacheKey = "PayRateModel-" + id;
            object objModel = Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(id);
                    if (objModel != null)
                    {
                        int modelCache = Sys.Constant.ModelCache;
                        //Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                        Cache.WebCache.GetCacheService().AddObject(cacheKey, objModel, modelCache);
                    }
                }
                catch { }
            }
            return (viviapi.Model.Finance.PayRate)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<viviapi.Model.Finance.PayRate> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<viviapi.Model.Finance.PayRate> DataTableToList(DataTable dt)
        {
            var modelList = new List<viviapi.Model.Finance.PayRate>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                viviapi.Model.Finance.PayRate model;
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

        #region GetRateTypeName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetRateTypeName(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return string.Empty;



            int id = Convert.ToInt32(obj);
            string typeName = id.ToString(CultureInfo.InvariantCulture);

            switch (id)
            {
                case 1:
                    typeName = "商户等级";
                    break;
                case 2:
                    typeName = "独立商户";
                    break;
                case 3:
                    typeName = "接口商";
                    break;
            }

            return typeName;
        }
        #endregion

        #region GetUserPayRate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="payType"></param>
        /// <returns></returns>
        public decimal GetUserPayRate(int userId, int payType)
        {
            try
            {
                return dal.GetUserPayRate(userId, payType);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }
        #endregion

        #region GetSupplierPayRate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="payType"></param>
        /// <returns></returns>
        public decimal GetSupplierPayRate(int supplierId, int payType)
        {
            try
            {
                return dal.GetSupplierPayRate(supplierId, payType);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }
        #endregion

        #endregion  ExtensionMethod

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            try
            {
                return dal.PageSearch(searchParams, pageSize, page, orderby);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
    }
}

