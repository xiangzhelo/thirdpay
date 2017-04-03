using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using viviLib.Data;
using viviLib.ExceptionHandling;
using viviLib.Utils;

namespace viviapi.BLL.Finance
{
    /// <summary>
    /// withdraw
    /// </summary>
    public partial class Withdraw
    {
        private readonly viviapi.DAL.Finance.Withdraw dal = new viviapi.DAL.Finance.Withdraw();
        public Withdraw()
        { }

        public static Withdraw Instance
        {
            get
            {
                var instance = new Withdraw();
                return instance;
            }
        }

        public string GenerateOrderId()
        {
            return "W" + Common.GuidToLongID().ToString(CultureInfo.InvariantCulture);
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
        public int Add(viviapi.Model.Finance.Withdraw model)
        {
            return dal.Add(model);
        }

        #region Apply
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Apply(viviapi.Model.Finance.Withdraw model)
        {
            try
            {
                return dal.Apply(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #region Audit
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tranno"></param>
        /// <param name="batchNo"></param>
        /// <param name="auditResult">
        /// 0 审核退回
        /// 1 审核通过
        /// </param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool Audit(string tranno, string batchNo, byte auditResult, string remark)
        {
            try
            {
                bool result = dal.Audit(tranno, batchNo, auditResult, remark);

                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region Cancel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tranno"></param>
        /// <returns></returns>
        public bool Cancel(string tranno)
        {
            try
            {
                bool result = dal.Cancel(tranno);

                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(viviapi.Model.Finance.Withdraw model)
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
        #endregion

        #region Complete
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Complete(viviapi.Model.Finance.Withdraw model)
        {
            try
            {
                return dal.Complete(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

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
        public viviapi.Model.Finance.Withdraw GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }

        }

        public viviapi.Model.Finance.Withdraw GetModel(string tranno)
        {
            try
            {
                return dal.GetModel(tranno);
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
        public List<viviapi.Model.Finance.Withdraw> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<viviapi.Model.Finance.Withdraw> DataTableToList(DataTable dt)
        {
            List<viviapi.Model.Finance.Withdraw> modelList = new List<viviapi.Model.Finance.Withdraw>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                viviapi.Model.Finance.Withdraw model;
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

        #region GetUserDaySettledTimes

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public int GetUserDaySettledTimes(int userid, string day)
        {
            try
            {
                return dal.GetUserDaySettledTimes(userid, day);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #region GetUserDaySettledAmt

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public decimal GetUserDaySettledAmt(int userid, string day)
        {
            try
            {
                return dal.GetUserDaySettledAmt(userid, day);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #region GetBankName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetBankName(string code)
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

        #region GetStatusName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetStatusName(int status)
        {
            string name = "";
            switch (status)
            {
                case 0:
                    name = "未知";
                    break;
                case 1:
                    name = "已取消";
                    break;
                case 2:
                    name = "审核中";
                    break;
                case 4:
                    name = "支付中";
                    break;
                case 8:
                    name = "处理完成";
                    break;

            }
            return name;
        }
        #endregion

        #region GetPayType
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payType"></param>
        /// <returns></returns>
        public string GetPayType(int payType)
        {
            string name = "";
            switch (payType)
            {
                case 1:
                    name = "银行卡";
                    break;
                case 2:
                    name = "支付宝";
                    break;
                case 3:
                    name = "财付通";
                    break;

            }
            return name;
        }
        #endregion

        #region GetModeName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetModeName(int status)
        {
            string name = "";
            switch (status)
            {
                case 0:
                    name = "未知";
                    break;
                case 1:
                    name = "手动提现";
                    break;
                case 2:
                    name = "系统结算";
                    break;
                case 4:
                    name = "通过接口";
                    break;
                case 8:
                    name = "上传文件";
                    break;

            }
            return name;
        }
        #endregion

        #endregion  ExtensionMethod

        #region PageSearch
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby,bool stat)
        {
            try
            {
                return dal.PageSearch(searchParams, pageSize, page, orderby, stat);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
        #endregion

        
    }
}

