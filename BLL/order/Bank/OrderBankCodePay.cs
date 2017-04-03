using System;
using System.Data;
using System.Collections.Generic;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Order.Bank
{
    /// <summary>
    /// orderbankcodepay
    /// </summary>
    public partial class OrderBankCodePay
    {
        private readonly viviapi.DAL.Order.Bank.OrderBankCodePay dal = new DAL.Order.Bank.OrderBankCodePay();
        public OrderBankCodePay()
        { }

        public static OrderBankCodePay Instance
        {
            get
            {
                var instance = new OrderBankCodePay();
                return instance;
            }
        }

        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Order.Bank.OrderBankCodePay model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception exception)
            {

                ExceptionHandler.HandleException(exception);
                return 0;
            }
            
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Order.Bank.OrderBankCodePay model)
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
        public Model.Order.Bank.OrderBankCodePay GetModel(string orderno)
        {
            try
            {
                return dal.GetModel(orderno);
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
        //public viviapi.Model.Order.OrderBankCodePay GetModelByCache(string orderno)
        //{

        //    //string CacheKey = "orderbankcodepayModel-" + id;
        //    //object objModel = DataCache.GetCache(CacheKey);
        //    //if (objModel == null)
        //    //{
        //    //    try
        //    //    {
        //    //        objModel = dal.GetModel(id);
        //    //        if (objModel != null)
        //    //        {
        //    //            int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //    //            Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //    //        }
        //    //    }
        //    //    catch { }
        //    //}
        //    //return (viviapi.Model.Order.OrderBankCodePay)objModel;
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
        public List<Model.Order.Bank.OrderBankCodePay> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Order.Bank.OrderBankCodePay> DataTableToList(DataTable dt)
        {
            List<Model.Order.Bank.OrderBankCodePay> modelList = new List<Model.Order.Bank.OrderBankCodePay>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Order.Bank.OrderBankCodePay model;
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

