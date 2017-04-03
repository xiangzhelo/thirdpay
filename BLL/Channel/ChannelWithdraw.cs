using System;
using System.Data;
using System.Collections.Generic;

using viviLib.ExceptionHandling;
using viviapi.Model;
using viviLib.Data;

namespace viviapi.BLL.Withdraw
{
	/// <summary>
	/// channelwithdraw
	/// </summary>
	public partial class ChannelWithdraw
	{
        private readonly viviapi.DAL.Withdraw.ChannelWithdraw dal = new viviapi.DAL.Withdraw.ChannelWithdraw();
		public ChannelWithdraw()
		{}

        public static ChannelWithdraw Instance
        {
            get
            {
                var bank = new ChannelWithdraw();
                return bank;
            }
        }

		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Model.Channel.ChannelWithdraw model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.Channel.ChannelWithdraw model)
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
		public Model.Channel.ChannelWithdraw GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

        public Model.Channel.ChannelWithdraw GetModelByBankName(string bankName)
        {
            try
            {
                return dal.GetModelByBankName(bankName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        ///// <summary>
        ///// 得到一个对象实体，从缓存中
        ///// </summary>
        //public viviapi.Model.Withdraw.channelwithdraw GetModelByCache(int id)
        //{
			
        //    string CacheKey = "channelwithdrawModel-" + id;
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
        //    return (viviapi.Model.Withdraw.channelwithdraw)objModel;
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
		public List<Model.Channel.ChannelWithdraw> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.Channel.ChannelWithdraw> DataTableToList(DataTable dt)
		{
			List<Model.Channel.ChannelWithdraw> modelList = new List<Model.Channel.ChannelWithdraw>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.Channel.ChannelWithdraw model;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankCode"></param>
        /// <returns></returns>
        public int GetSupplier(string bankCode)
        {
            try
            {
                return dal.GetSupplier(bankCode);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
		#endregion  ExtensionMethod

        #region GetSettleBankName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetSettleBankName(string code)
        {
            string bankname = code;
            switch (code)
            {
                case "0002":
                    bankname = "支付宝";
                    break;
                case "0003":
                    bankname = "财付通";
                    break;
                case "1002":
                    bankname = "工商银行";
                    break;
                case "1005":
                    bankname = "农业银行";
                    break;
                case "1003":
                    bankname = "建设银行";
                    break;
                case "1026":
                    bankname = "中国银行";
                    break;
                case "1001":
                    bankname = "招商银行";
                    break;
                case "1006":
                    bankname = "民生银行";
                    break;
                case "1020":
                    bankname = "交通银行";
                    break;
                case "1025":
                    bankname = "华夏银行";
                    break;
                case "1009":
                    bankname = "兴业银行";
                    break;
                case "1027":
                    bankname = "广发银行";
                    break;
                case "1004":
                    bankname = "浦发银行";
                    break;
                case "1022":
                    bankname = "光大银行";
                    break;
                case "1021":
                    bankname = "中信银行";
                    break;
                case "1010":
                    bankname = "平安银行";
                    break;
                case "1066":
                    bankname = "邮政储蓄银行";
                    break;
            }
            return bankname;
        }
        #endregion
	}
}

