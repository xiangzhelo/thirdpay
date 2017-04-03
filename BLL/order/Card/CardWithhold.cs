using System;
using System.Data;
using System.Collections.Generic;
using System.Threading;

using viviLib.ExceptionHandling;
using viviapi.Model;
using viviLib.Data;

namespace viviapi.BLL.Order
{
	/// <summary>
	/// cardwithhold
	/// </summary>
	public partial class Cardwithhold
	{
        private readonly viviapi.DAL.Order.Card.cardwithhold dal = new viviapi.DAL.Order.Card.cardwithhold();
		public Cardwithhold()
		{}
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
		public bool Exists(int userid,int cardtype,string cardno)
		{
			return dal.Exists(userid,cardtype,cardno);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(viviapi.Model.Order.Cardwithhold model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(viviapi.Model.Order.Cardwithhold model)
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
		public bool Delete(int userid,int cardtype,string cardno)
		{
			
			return dal.Delete(userid,cardtype,cardno);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idlist"></param>
        /// <param name="iscolse"></param>
        /// <returns></returns>
        public bool BatchColse(string idlist, byte iscolse)
        {
            try
            {
                return dal.BatchColse(idlist, iscolse);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

	    public bool BatchUnlock(string idlist)
	    {
            try
            {
                return dal.BatchUnlock(idlist);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
	    }

	    public bool ALLColse(List<viviLib.Data.SearchParam> searchParams)
        {
            try
            {
                return dal.ALLColse(searchParams);
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
		public viviapi.Model.Order.Cardwithhold GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

        ///// <summary>
        ///// 得到一个对象实体，从缓存中
        ///// </summary>
        //public viviapi.Model.Order.cardwithhold GetModelByCache(int id)
        //{
			
        //    string CacheKey = "cardwithholdModel-" + id;
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
        //    return (viviapi.Model.Order.cardwithhold)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<viviapi.Model.Order.Cardwithhold> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<viviapi.Model.Order.Cardwithhold> DataTableToList(DataTable dt)
		{
			List<viviapi.Model.Order.Cardwithhold> modelList = new List<viviapi.Model.Order.Cardwithhold>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				viviapi.Model.Order.Cardwithhold model;
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


        #region PageSearch
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, byte stat)
        {
            try
            {
                return dal.PageSearch(searchParams, pageSize, page, orderby, stat);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion
		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

