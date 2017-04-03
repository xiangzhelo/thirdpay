using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using DBAccess;
using viviLib;
using viviLib.Data;
using viviLib.ExceptionHandling;
using viviapi.Model.APP;

namespace viviapi.BLL.APP
{
	/// <summary>
	/// 数据访问类:apprecharge
	/// </summary>
	public partial class Recharge
	{
        private readonly DAL.APP.Recharge dal = new DAL.APP.Recharge();

        public static Recharge Instance
        {
            get
            {
                var instance = new Recharge();
                return instance;
            }
        }


        public Recharge()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		    try
		    {
		        return dal.GetMaxId();
		    }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
            try
            {
                return dal.Exists(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
		}

		/// <summary>
		///  增加一条数据
		/// </summary>
		public int Add(Model.APP.Recharge model)
		{
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(Model.APP.Recharge model)
		{
            try
            {
                return dal.Update(model);
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
            try
            {
                return dal.Delete(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
            try
            {
                return dal.DeleteList(idlist);
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
        public Model.APP.Recharge GetModel(string orderid)
		{
            try
            {
                return dal.GetModel(orderid);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
            try
            {
                return dal.GetList(strWhere);
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
            try
            {
                return dal.GetList(Top, strWhere, filedOrder);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
            try
            {
                return dal.GetRecordCount(strWhere);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
            try
            {
                return dal.GetListByPage(strWhere,orderby,startIndex,endIndex);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "apprecharge";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
		#region  MethodEx

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public  DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby,bool isstat)
        {
            DataSet ds = new DataSet();
            try
            {
                return dal.PageSearch(searchParams, pageSize, page, orderby, isstat);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return ds;
            }
        }

        public string GetStatusName(object s)
        {
            int status = Convert.ToInt32(s);
            string sname = "等待付款";
            if (status == 2)
            {
                sname = "付款成功";
            }
            else if (status == 4)
            {
                sname = "付款失败";
            }

            return sname;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionNo">订单号</param>
        /// <param name="suppTranNo">接口商订单号</param>
        /// <param name="payMoney">交易金额</param>
        /// <param name="status">交易状态</param>
        /// <returns>0 处理失败 1 处理成功 2 已经处理</returns>
        public int Complete(string transactionNo,string suppTranNo, decimal payMoney, int status)
        {
            try
            {
                return dal.Complete(transactionNo, suppTranNo, payMoney, status);
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        
		#endregion  MethodEx
	}
}

