using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using viviapi.Model.Finance;
using viviLib.Data;
using viviLib.ExceptionHandling;
using viviLib.Utils;

namespace viviapi.BLL.Finance
{
    /// <summary>
    /// Model.Finance.WithdrawSuppTranLog
    /// </summary>
    public partial class WithdrawSuppTranLog
    {
        private readonly viviapi.DAL.Finance.WithdrawSuppTranLog dal = new viviapi.DAL.Finance.WithdrawSuppTranLog();
        public WithdrawSuppTranLog()
        { }

        public static WithdrawSuppTranLog Instance
        {
            get
            {
                var instance = new WithdrawSuppTranLog();
                return instance;
            }
        }

        public string GenerateOrderId()
        {
            return "Y" + Common.GuidToLongID().ToString(CultureInfo.InvariantCulture);
        }

        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string trade_no)
        {
            return dal.Exists(trade_no);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Finance.WithdrawSuppTranLog model)
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
        public bool Update(Model.Finance.WithdrawSuppTranLog model)
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
        public bool Delete(string trade_no)
        {

            return dal.Delete(trade_no);
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
        public Model.Finance.WithdrawSuppTranLog GetModel(int id)
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Finance.WithdrawSuppTranLog> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Finance.WithdrawSuppTranLog> DataTableToList(DataTable dt)
        {
            List<Model.Finance.WithdrawSuppTranLog> modelList = new List<Model.Finance.WithdrawSuppTranLog>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Finance.WithdrawSuppTranLog model;
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

        #region GetStatusText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetStatusText(object status)
        {
            if (status == null || status == DBNull.Value)
                return string.Empty;

            int temp = Convert.ToInt32(status);
            switch (temp)
            {
                case 1:
                    return "处理中";
                case 2:
                    return "代发成功";
                case 4:
                    return "代发失败";
            }
            return temp.ToString();
        }
        #endregion

        #region Process
        /// <summary>
        /// 
        /// </summary>
        /// <param name="suppId"></param>
        /// <param name="tradeNo"></param>
        /// <param name="isCancel"></param>
        /// <param name="status"></param>
        /// <param name="amount"></param>
        /// <param name="suppTradeNo"></param>
        /// <param name="message"></param>
        /// <param name="billTradeNo"></param>
        /// <returns></returns>
        public int Process(int suppId
            , string tradeNo
            , bool isCancel
            , int status
            , string amount
            , string suppTradeNo
            , string message
            , out string billTradeNo)
        {
            billTradeNo = string.Empty;
            try
            {
                return dal.Process(suppId, tradeNo, isCancel, status, amount, suppTradeNo, message, out billTradeNo);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }
        #endregion

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

