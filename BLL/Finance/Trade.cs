using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Web;
using viviLib.ExceptionHandling;
using viviapi.Model;
using viviapi.Model.User;
using viviLib;
using DBAccess;
using viviLib.Data;

namespace viviapi.BLL.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public class Trade
    {
        private static DAL.Finance.Trade Dal = new DAL.Finance.Trade();

        #region GetUserIncome
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sdate"></param>
        /// <param name="edate"></param>
        /// <returns></returns>
        public static decimal GetUserIncome(int userId, DateTime sdate, DateTime edate)
        {
            try
            {
                return Dal.GetUserIncome(userId, sdate, edate);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classid">1网银收入 2点卡收入 3其它收入</param>
        /// <param name="userId"></param>
        /// <param name="sdate"></param>
        /// <param name="edate"></param>
        /// <returns></returns>
        public static decimal GetUserIncome(int classid, int userId, DateTime sdate, DateTime edate)
        {
            try
            {
                return Dal.GetUserIncome(classid, userId, sdate, edate);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }
        #endregion

        #region GetUserIncome2
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sdate"></param>
        /// <param name="edate"></param>
        /// <returns></returns>
        public static decimal GetUserIncome2(int userId, DateTime sdate, DateTime edate)
        {
            try
            {
                return Dal.GetUserIncome2(userId, sdate, edate);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }
        #endregion

        #region GetUserOrderAmt
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sdate"></param>
        /// <param name="edate"></param>
        /// <returns></returns>
        public static decimal GetUserOrderAmt(int userId, DateTime sdate, DateTime edate)
        {
            try
            {
                return Dal.GetUserOrderAmt(userId, sdate, edate);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
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
        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            try
            {
                return Dal.PageSearch(searchParams, pageSize, page, orderby);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region GetNdaysIncome
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="userid"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static decimal GetNdaysIncome(int classid, int userid, int days)
        {
            try
            {
                return Dal.GetNdaysIncome(classid, userid, days);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }
        #endregion

        #region GetUserDetentionAmt
        /// <summary>
        /// 取用户扣压金额
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataTable GetUserDetentionAmt(int userId)
        {
            try
            {
                return Dal.GetUserDetentionAmt(userId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 取总的扣压金额
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static decimal GetUserTotalDetentionAmt(int userId)
        {
            try
            {
                decimal total = 0M;
                DataTable dataTable = GetUserDetentionAmt(userId);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    if (row != null)
                    {
                        total= Convert.ToDecimal(row["totaldamt"]);
                    }
                }
                return total;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }

        #endregion

        #region GetBillTypeText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="billType"></param>
        /// <returns></returns>
        public static string GetBillTypeText(int billType)
        {
            string typeName = billType.ToString();

            switch (billType)
            {
                case 1:
                    typeName = "网银收益";
                    break;
                case 2:
                    typeName = "点卡收益";
                    break;
                case 3:
                    typeName = "代理提成";
                    break;
                case 4:
                    typeName = "后台加款";
                    break;
                case 5:
                    typeName = "扣量返还";
                    break;
                case 6:
                    typeName = "转账收入";
                    break;
                case 7:
                    typeName = "账户充值";
                    break;

                case 100:
                    typeName = "提现";
                    break;
                case 101:
                    typeName = "后台减款";
                    break;
                case 105:
                    typeName = "扣单";
                    break;
                case 106:
                    typeName = "转账扣除";
                    break;
                case 107:
                    typeName = "结冻";
                    break;
            }
            return typeName;
        }
        #endregion



    }
}
