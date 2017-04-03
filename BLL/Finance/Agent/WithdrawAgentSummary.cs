using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Transactions;
using viviapi.Model.Finance.Agent;
using viviLib.Data;
using viviLib.ExceptionHandling;
using viviLib.Utils;

namespace viviapi.BLL.Finance.Agent
{
    /// <summary>
    /// withdrawAgentSummary
    /// </summary>
    public partial class WithdrawAgentSummary
    {
        private readonly viviapi.DAL.Finance.Agent.WithdrawAgentSummary dal = new DAL.Finance.Agent.WithdrawAgentSummary();

        public WithdrawAgentSummary()
        { }

        #region Generatelotno
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Generatelotno()
        {
            return "S" + Common.GuidToLongID().ToString(CultureInfo.InvariantCulture);
        }
        #endregion

        public bool Insert(Model.Finance.Agent.WithdrawAgentSummary summarymodel
            , List<Model.Finance.Agent.WithdrawAgent> itemlist)
        {
            try
            {
                dal.Insert(summarymodel, itemlist);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }


        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string lotno)
        {
            return dal.Exists(lotno);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Finance.Agent.WithdrawAgentSummary model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Finance.Agent.WithdrawAgentSummary model)
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
        public bool Delete(string lotno)
        {

            return dal.Delete(lotno);
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
        public Model.Finance.Agent.WithdrawAgentSummary GetModel(int id)
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
        public List<Model.Finance.Agent.WithdrawAgentSummary> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Finance.Agent.WithdrawAgentSummary> DataTableToList(DataTable dt)
        {
            List<Model.Finance.Agent.WithdrawAgentSummary> modelList = new List<Model.Finance.Agent.WithdrawAgentSummary>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Finance.Agent.WithdrawAgentSummary model;
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

        #region ChkParms
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="tamount"></param>
        /// <returns>
        /// 0 检测正常
        /// 1 用户状态不正常
        /// 2 商户未签约当前产品
        /// 3 未设置提现方案
        /// 4 不能低于最小允许金额
        /// 5 不能超过最大允许金额
        /// 6 提现金额超过余额
        /// </returns>
        public int ChkParms(int userid, decimal tamount)
        {
            try
            {
                return dal.ChkParms(userid, tamount);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }
        #endregion

        public int Affirm(string lot_no, int auditstatus, int auditUser, string auditUserName, string clientip)
        {
            try
            {
                return dal.Affirm(lot_no, auditstatus, auditUser, auditUserName, clientip);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, bool isstat)
        {
            try
            {
                return dal.PageSearch(searchParams, pageSize, page, orderby, isstat);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        #region GetShowText
        #region GetAuditStatusText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="audit_status"></param>
        /// <returns></returns>
        public string GetAuditStatusText(object audit_status)
        {
            string _text = string.Empty;
            if (audit_status == DBNull.Value)
                return _text;

            int _s = Convert.ToInt32(audit_status);
            if (_s == 1)
            {
                _text = "等待处理";
            }
            else if (_s == 2)
            {
                _text = "已确认";
            }
            else if (_s == 3)
            {
                _text = "已取消";
            }

            return _text;
        }
        #endregion

        #region GetStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="audit_status"></param>
        /// <returns></returns>
        public string GetStatus(object status)
        {
            string _text = string.Empty;
            if (status == DBNull.Value)
                return _text;

            int _s = Convert.ToInt32(status);
            if (_s == 1)
            {
                _text = "等待处理";
            }
            else if (_s == 2)
            {
                _text = "部分完成";
            }
            else if (_s == 3)
            {
                _text = "全部完成";
            }


            return _text;
        }
        #endregion

        #region GetCancelText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="is_cancel"></param>
        /// <returns></returns>
        public string GetCancelText(object is_cancel)
        {
            string _text = string.Empty;
            if (is_cancel == DBNull.Value)
                return _text;

            int _s = Convert.ToInt32(is_cancel);
            if (_s == 0)
            {
                _text = "未取消";
            }
            else if (_s == 1)
            {
                _text = "已取消";
            }
            return _text;
        }
        #endregion

        #region GetNotifyStatusText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="is_cancel"></param>
        /// <returns></returns>
        public string GetNotifyStatusText(object notifystatus)
        {
            string _text = string.Empty;
            if (notifystatus == DBNull.Value)
                return _text;

            int _s = Convert.ToInt32(notifystatus);
            if (_s == 0)
            {
                _text = "发送失败";
            }
            else if (_s == 1)
            {
                _text = "处理中";
            }
            else if (_s == 2)
            {
                _text = "已成功";
            }
            return _text;
        }
        #endregion

        #region GetModeText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="is_cancel"></param>
        /// <returns></returns>
        public string GetModeText(object mode)
        {
            string _text = string.Empty;
            if (mode == DBNull.Value)
                return _text;

            int _s = Convert.ToInt32(mode);
            if (_s == 1)
            {
                _text = "API接口提交";
            }
            else if (_s == 2)
            {
                _text = "手动增加";
            }
            return _text;
        }
        #endregion
        #endregion

        #endregion  ExtensionMethod
    }
}

