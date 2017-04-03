using System;
using System.Collections.Generic;
using System.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.User
{
	/// <summary>
	/// userLoginByPartner
	/// </summary>
	public partial class UserLoginByPartner
	{
        public static UserLoginByPartner Instance
        {
            get
            {
                var instance = new UserLoginByPartner();
                return instance;
            }
        }

		private readonly viviapi.DAL.User.UserLoginByPartner dal=new viviapi.DAL.User.UserLoginByPartner();
        public UserLoginByPartner()
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
	    public int Exists(int plant, string openid)
	    {
            try
            {

                return dal.Exists(plant, openid);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
	    }

	    public int Insert(Model.User.UserLoginByPartner model)
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

	    public bool Unbind(int plant, int userid)
	    {
            try
            {

                return dal.Unbind(plant, userid);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
	    }

	    /// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Model.User.UserLoginByPartner model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.User.UserLoginByPartner model)
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
		public Model.User.UserLoginByPartner GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

	    public viviapi.Model.User.UserLoginByPartner GetModel(int plant, string openid)
	    {
            try
            {

                return dal.GetModel(plant, openid);
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
		public List<Model.User.UserLoginByPartner> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.User.UserLoginByPartner> DataTableToList(DataTable dt)
		{
			List<Model.User.UserLoginByPartner> modelList = new List<Model.User.UserLoginByPartner>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.User.UserLoginByPartner model;
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

