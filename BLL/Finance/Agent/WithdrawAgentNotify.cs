using System;
using System.Collections.Generic;
using System.Data;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Finance.Agent
{
	/// <summary>
	/// withdrawAgentNotify
	/// </summary>
	public partial class WithdrawAgentNotify
	{
		private readonly viviapi.DAL.Finance.Agent.WithdrawAgentNotify dal=new viviapi.DAL.Finance.Agent.WithdrawAgentNotify();
		public WithdrawAgentNotify()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(viviapi.Model.Finance.Agent.WithdrawAgentNotify model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(viviapi.Model.Finance.Agent.WithdrawAgentNotify model)
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
		public viviapi.Model.Finance.Agent.WithdrawAgentNotify GetModel(int id)
		{
			
			return dal.GetModel(id);
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
		public List<viviapi.Model.Finance.Agent.WithdrawAgentNotify> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<viviapi.Model.Finance.Agent.WithdrawAgentNotify> DataTableToList(DataTable dt)
		{
			var modelList = new List<viviapi.Model.Finance.Agent.WithdrawAgentNotify>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				viviapi.Model.Finance.Agent.WithdrawAgentNotify model;
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

        #region PageSearch
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
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
        #endregion

		#region  ExtensionMethod

		#endregion  ExtensionMethod

        #region GetShowText

        #region GetNotifyStatusText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifystatus"></param>
        /// <returns></returns>
        public string GetNotifyStatusText(object notifystatus)
        {
            string text = string.Empty;
            if (notifystatus == DBNull.Value)
                return text;

            int _s = Convert.ToInt32(notifystatus);
            if (_s == 1)
            {
                text = "处理中";
            }
            else if (_s == 0)
            {
                text = "失败";
            }
            else if (_s == 2)
            {
                text = "成功";
            }

            return text;
        }
        #endregion

        #endregion
	}
}

