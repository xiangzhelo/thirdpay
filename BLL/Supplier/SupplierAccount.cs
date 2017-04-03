/**  版本信息模板在安装目录下，可自行修改。
* supplierAccount.cs
*
* 功 能： N/A
* 类 名： supplierAccount
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-2-4 10:22:18   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DBAccess;

namespace viviapi.BLL.Supplier
{
	/// <summary>
	/// supplierAccount
	/// </summary>
	public partial class SupplierAccount
	{
        public const string SqlTable = "supplierAccount";
        public const string SqlTableFields = @"[id]
      ,[code]
      ,[name]
      ,[apiAccount]
      ,[apiKey]
      ,[userName]
      ,[email]
      ,[domain]
      ,[jumpdomain]
      ,[available]
      ,[isdefault]";

        public static string CacheKey = Sys.Constant.CacheMark + "SupplierAccount_{0}_{1}";

		private readonly viviapi.DAL.Supplier.SupplierAccount dal=new viviapi.DAL.Supplier.SupplierAccount();
		public SupplierAccount()
		{}
		#region  BasicMethod

        public int Insert(viviapi.Model.Supplier.SupplierAccount model)
        {
            try
            {
                return dal.Insert(model);
            }
            catch (Exception exception)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(exception);
                return 0;
            }
          
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(viviapi.Model.Supplier.SupplierAccount model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(viviapi.Model.Supplier.SupplierAccount model)
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
		public viviapi.Model.Supplier.SupplierAccount GetModel(int id)
		{
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception exception)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(exception);
                return null;
            }
		}

        public viviapi.Model.Supplier.SupplierAccount GetModelByDomain(int code, string domain)
		{
            try
            {
                return dal.GetModelByDomain(code, domain);
            }
            catch (Exception exception)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(exception);
                return null;
            }
		}

        

        ///// <summary>
        ///// 得到一个对象实体，从缓存中
        ///// </summary>
        public viviapi.Model.Supplier.SupplierAccount GetCacheModelByDomain(int code, string domain)
        {

            var model = new Model.Supplier.SupplierAccount();

            string cacheKey = string.Format(CacheKey, code, domain);

            model = (Model.Supplier.SupplierAccount)Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

            if (model == null)
            {
                IDictionary<string, object> sqldepparms = new Dictionary<string, object>();
                sqldepparms.Add("code", code);
                sqldepparms.Add("domain", domain);

                SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SqlTable, SqlTableFields, "[code]=@code AND domain=@domain", sqldepparms);

                model = GetModelByDomain(code, domain);

                viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, model);
            }

            return model;
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
		public List<viviapi.Model.Supplier.SupplierAccount> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<viviapi.Model.Supplier.SupplierAccount> DataTableToList(DataTable dt)
		{
			List<viviapi.Model.Supplier.SupplierAccount> modelList = new List<viviapi.Model.Supplier.SupplierAccount>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				viviapi.Model.Supplier.SupplierAccount model;
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

