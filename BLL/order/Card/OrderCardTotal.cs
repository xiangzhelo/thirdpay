using System;
using System.Collections.Generic;
using System.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Order.Card
{
	/// <summary>
	/// ordercardtotal
	/// </summary>
	public partial class OrderCardTotal
	{
		private readonly viviapi.DAL.Order.Card.OrderCardTotal dal=new viviapi.DAL.Order.Card.OrderCardTotal();
        public OrderCardTotal()
		{}

        public static OrderCardTotal Instance
        {
            get
            {
                var instance = new OrderCardTotal();
                return instance;
            }
        }

		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(viviapi.Model.Order.Card.OrderCardTotal model)
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

	    public bool Notify(string orderid, string notifyUrl, int notifystatus)
	    {
            try
            {
                return dal.Notify(orderid, notifyUrl, notifystatus);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
	    }

	    /// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(viviapi.Model.Order.Card.OrderCardTotal model)
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
		public viviapi.Model.Order.Card.OrderCardTotal GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
	    public viviapi.Model.Order.Card.OrderCardTotal GetModelByOrderId(string orderId)
	    {
            try
            {
                return dal.GetModelByOrderId(orderId);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
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
		public List<viviapi.Model.Order.Card.OrderCardTotal> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<viviapi.Model.Order.Card.OrderCardTotal> DataTableToList(DataTable dt)
		{
			List<viviapi.Model.Order.Card.OrderCardTotal> modelList = new List<viviapi.Model.Order.Card.OrderCardTotal>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				viviapi.Model.Order.Card.OrderCardTotal model;
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

