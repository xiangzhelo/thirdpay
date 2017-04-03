using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.User
{
	/// <summary>
	/// userreservewords
	/// </summary>
	public partial class UserReservewords
	{
		private readonly DAL.User.UserReservewords dal = new DAL.User.UserReservewords();  
        public UserReservewords()
		{}
		#region  BasicMethod

	    public bool Exists(int userID)
	    {
            try
            {
                return dal.Exists(userID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
               return false;
                
            }
	    }

	    /// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Model.User.UserReservewords model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(Model.User.UserReservewords model)
		{
			return dal.Update(model);
		}

	    public bool Insert(Model.User.UserReservewords model)
	    {
	        try
	        {
                return dal.Insert(model);
	        }
	        catch (Exception ex)
	        {
	            ExceptionHandler.HandleException(ex);
	            throw;
	        }
	    }

	    /// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.Delete();
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Model.User.UserReservewords GetModel(int userId)
		{
			//该表无主键信息，请自定义主键/条件字段
            return dal.GetModel(userId);
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
        public List<Model.User.UserReservewords> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<Model.User.UserReservewords> DataTableToList(DataTable dt)
		{
            var modelList = new List<Model.User.UserReservewords>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                Model.User.UserReservewords model;
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

