using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using viviapi.BLL.Withdraw;
using viviLib.Data;
using viviLib.ExceptionHandling;
using viviLib.Utils;

namespace viviapi.BLL.Finance.Agent
{
	/// <summary>
	/// withdrawAgent
	/// </summary>
	public partial class WithdrawAgent
	{
		private readonly viviapi.DAL.Finance.Agent.WithdrawAgent dal=new viviapi.DAL.Finance.Agent.WithdrawAgent();
		public WithdrawAgent()
		{}

        public static WithdrawAgent Instance
        {
            get
            {
                var instance = new WithdrawAgent();
                return instance;
            }
        }


        public string GenerateOrderId()
        {
            return "F" + Common.GuidToLongID().ToString(CultureInfo.InvariantCulture);
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
		public int  Add(Model.Finance.Agent.WithdrawAgent model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.Finance.Agent.WithdrawAgent model)
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
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.Finance.Agent.WithdrawAgent GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

        public Model.Finance.Agent.WithdrawAgent GetModel(string tranno)
        {

            return dal.GetModel(tranno);
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
		public List<Model.Finance.Agent.WithdrawAgent> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.Finance.Agent.WithdrawAgent> DataTableToList(DataTable dt)
		{
			List<Model.Finance.Agent.WithdrawAgent> modelList = new List<Model.Finance.Agent.WithdrawAgent>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.Finance.Agent.WithdrawAgent model;
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

        #region 取消
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trade_no"></param>
        /// <returns>
        /// 99 未知错误
        ///1 不存在此单
        ///2 此单已取消
        ///3 已审核，不可取消
        ///4 系统出错

        ///0 审核成功
        /// </returns>
        public int Cancel(string trade_no)
        {
            try
            {
                int result = dal.Cancel(trade_no);
                if (result == 0)
                {
                    DoNotify(trade_no);
                }

                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public string doCancel(string trade_no)
        {
            string message = string.Empty;

            int result = Cancel(trade_no);
            if (result == 0)
            {
                message = "取消成功";
            }
            else if (result == 1)
            {
                message = "不存在此单";
            }
            else if (result == 2)
            {
                message = "此单已取消，不可重复操作";
            }
            else if (result == 3)
            {
                message = "已审核，不可取消";
            }
            else if (result == 4)
            {
                message = "系统故障，请查看日志";
            }
            else if (result == 5)
            {
                message = "用户未确认";
            }
            else
            {
                message = "未知错误";
            }
            return message;
        }
        #endregion

        #region 审核
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trade_no">交易号</param>
        /// <param name="suppid">交易号</param>
        /// <param name="auditstatus">状态 2 审核成功 3 审核失败 </param>
        /// <param name="auditUser">审核人</param>
        /// <param name="auditUserName">审核人姓名</param>
        /// <returns>
        /// 99 未知错误
        ///1 不存在此单
        ///2 此单已取消
        ///3 已审核过，不可重复操作
        ///4 输入状态不正确
        ///5 系统出错
        ///0 审核成功
        /// </returns>
        public int Audit(string trade_no, int auditstatus, int auditUser, string auditUserName)
        {
            try
            {
                int result = dal.Audit(trade_no, auditstatus, auditUser, auditUserName);
                if (result == 0)
                {
                    DoNotify(trade_no);
                }
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public string doAudit(string trade_no, int auditUser, string auditUserName)
        {
            string message = string.Empty;
            int result = Audit(trade_no, 2, auditUser, auditUserName);
            if (result == 0)
            {
                message = "审核成功";
            }
            else if (result == 1)
            {
                message = "不存在此单";
            }
            else if (result == 2)
            {
                message = "此单已取消";
            }
            else if (result == 3)
            {
                message = "已审核过，不可重复操作";
            }
            else if (result == 4)
            {
                message = "输入状态不正确";
            }
            else if (result == 5)
            {
                message = "系统故障，请查看日志";
            }
            else if (result == 6)
            {
                message = "用户未确认,不可操作";
            }
            else
            {
                message = "未知错误";
            }
            return message;
        }

        public string doRefuse(string trade_no, int auditUser, string auditUserName)
        {
            string message = string.Empty;
            int result = Audit(trade_no, 3, auditUser, auditUserName);
            if (result == 0)
            {
                message = "操作成功";
            }
            else if (result == 1)
            {
                message = "不存在此单";
            }
            else if (result == 2)
            {
                message = "此单已取消";
            }
            else if (result == 3)
            {
                message = "已审核过，不可重复操作";
            }
            else if (result == 4)
            {
                message = "输入状态不正确";
            }
            else if (result == 5)
            {
                message = "系统故障，请查看日志";
            }
            else
            {
                message = "未知错误";
            }
            return message;
        }
        #endregion

        #region 结案
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trade_no"></param>
        /// <param name="pstatus"></param>
        /// <returns>
        /// 99 未知错误
        /// 1 不存在此单
        /// 2 此单已取消
        /// 3 此单未审核，不可完成此操作
        /// 4 此单已结案	
        /// 5 系统出错

        /// 0 操作成功
        /// </returns>
        public int Complete(string trade_no, int pstatus)
        {
            try
            {
                int result = dal.Complete(trade_no, pstatus);

                if (result == 0)
                {
                    DoNotify(trade_no);
                }
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public string PaySuccess(string trade_no)
        {
            string message = string.Empty;

            int result = Complete(trade_no, 2);
            if (result == 0)
            {
                message = "付款成功";
            }
            else if (result == 1)
            {
                message = "不存在此单";
            }
            else if (result == 2)
            {
                message = "此单已取消";
            }
            else if (result == 3)
            {
                message = "此单未审核，不可完成此操作";
            }
            else if (result == 4)
            {
                message = "此单已结案";
            }
            else if (result == 5)
            {
                message = "系统故障，请查看日志";
            }
            else
            {
                message = "未知错误";
            }

            return message;
        }

        public string PayFail(string trade_no)
        {
            string message = string.Empty;

            int result = Complete(trade_no, 3);
            if (result == 0)
            {
                message = "付款成功";
            }
            else if (result == 1)
            {
                message = "不存在此单";
            }
            else if (result == 2)
            {
                message = "此单已取消";
            }
            else if (result == 3)
            {
                message = "此单未审核，不可完成此操作";
            }
            else if (result == 4)
            {
                message = "此单已结案";
            }
            else if (result == 5)
            {
                message = "系统故障，请查看日志";
            }
            else
            {
                message = "未知错误";
            }

            return message;
        }
        #endregion

        #region ChkParms
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="amount"></param>
        /// <param name="row"></param>
        /// <returns>
        /// 0 检测正常
        /// 1 不存在此用户
        /// 2 用户状态不正常
        /// 3 商户未签约当前产品
        /// 4 未设置提现方案
        /// 5 不能低于最小允许金额
        /// 6 不能超过最大允许金额
        /// 7 提现金额超过余额
        /// 
        /// 99异常错误
        /// </returns>
        public int ChkParms(int userid, string bankCode, decimal amount, out DataRow row)
        {
            row = null;
            try
            {
                return dal.ChkParms(userid, bankCode, amount, out row);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }
        #endregion

        #region PageSearch
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, byte stat)
        {
            try
            {
                return dal.PageSearch(searchParams, pageSize, page, orderby, stat);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trade_no"></param>
        public void DoNotify(string trade_no)
        {
            //Model.Finance.Agent.WithdrawAgent _info = GetModel(trade_no);
            //if (_info != null)
            //{
            //    notifyHelper _api = new notifyHelper();
            //    _api.model = _info;

            //    Thread t1 = new Thread(new ThreadStart(_api.DoNotify));
            //    t1.Start();
            //}
        }
        #endregion

        #region Affirm

        public int Affirm(string trade_no, byte sure, string clientip)
        {
            try
            {
                int result = dal.Affirm(trade_no, sure, clientip);
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }
        #endregion

        #region GetShowText

        #region GetIsSureText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="audit_status"></param>
        /// <returns></returns>
        public string GetIsSureText(object issure)
        {
            string _text = string.Empty;
            if (issure == DBNull.Value)
                return _text;

            int _s = Convert.ToInt32(issure);
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
                _text = "未确认";
            }

            return _text;
        }
        #endregion

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
                _text = "等待审核";
            }
            else if (_s == 2)
            {
                _text = "审核通过";
            }
            else if (_s == 3)
            {
                _text = "审核拒绝";
            }

            return _text;
        }
        #endregion

        #region GetPaymentStatusText
        /// <summary>
        /// 取付款状态
        /// </summary>
        /// <param name="audit_status"></param>
        /// <returns></returns>
        public string GetPaymentStatusText(object payment_status)
        {
            string _text = string.Empty;
            if (payment_status == DBNull.Value)
                return _text;

            int _s = Convert.ToInt32(payment_status);
            if (_s == 1)
            {
                _text = "未知";
            }
            else if (_s == 2)
            {
                _text = "成功";
            }
            else if (_s == 3)
            {
                _text = "失败";
            }
            else if (_s == 4)
            {
                _text = "付款中";
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

        #region GetSureText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="is_cancel"></param>
        /// <returns></returns>
        public string GetSureText(object is_sure)
        {
            string _text = string.Empty;
            if (is_sure == DBNull.Value)
                return _text;

            int _s = Convert.ToByte(is_sure);
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
                _text = "API提交";
            }
            else if (_s == 2)
            {
                _text = "上传文件";
            }
            return _text;
        }
        #endregion

        public string GetStatusText(object is_cancel, object audit_status, object payment_status)
        {
            string _text = "未知";
            if (is_cancel == DBNull.Value)
                return _text;

            byte _is_cancel = Convert.ToByte(is_cancel);
            if (_is_cancel == 1)
            {
                return "已取消";
            }

            byte _audit_status = Convert.ToByte(audit_status);
            if (_audit_status == 1)
            {
                _text = "等待审核";
            }
            else if (_audit_status == 2)
            {
                _text = "已审核(处理中)";
            }
            else if (_audit_status == 3)
            {
                _text = "审核拒绝";
            }

            byte _payment_status = Convert.ToByte(payment_status);
            if (_payment_status == 2)
            {
                _text = "支付成功";
            }
            else if (_payment_status == 3)
            {
                _text = "付款失败";
            }
            else if (_payment_status == 4)
            {
                _text = "已审核(处理中)";
            }
            return _text;
        }
        #endregion

		#endregion  ExtensionMethod
	}
}

