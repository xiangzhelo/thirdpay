using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Order
{
    public class Statistics
    {
        static  DAL.Order.Statistics dal = new DAL.Order.Statistics();

        #region Stat
        /// <summary>
        /// 
        /// </summary>
        /// <param name="suppid"></param>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <returns></returns>
        public static DataTable Stat(int suppid, DateTime sdt, DateTime edt)
        {
            try
            {
                return dal.Stat(suppid, sdt, edt);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region StatForAgent
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentid"></param>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <returns></returns>
        public static DataTable StatForAgent(int agentid, DateTime sdt, DateTime edt)
        {
            try
            {
                return dal.StatForAgent(agentid, sdt, edt);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region StatForBusiness
        /// <summary>
        /// 
        /// </summary>
        /// <param name="manageId"></param>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <returns></returns>
        public static DataTable StatForBusiness(int manageId, DateTime sdt, DateTime edt)
        {
            try
            {
                return dal.StatForBusiness(manageId, sdt, edt);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region AgentStat2
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static DataSet AgentStat2(DateTime sdt, DateTime edt, int page, int pagesize, string orderby)
        {
            try
            {
                return dal.AgentStat2(sdt, edt, page, pagesize, orderby);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region AgentStat3
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentid"></param>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static DataSet AgentStat3(int agentid, DateTime sdt, DateTime edt, int page, int pagesize, string orderby)
        {
            try
            {
                return dal.AgentStat3(agentid, sdt, edt, page, pagesize, orderby);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region AgentStat4
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="typeid"></param>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static DataSet AgentStat4(int userid, int typeid, string sdt, string edt, int pagesize, int page, string orderby)
        {
            try
            {
                return dal.AgentStat4(userid, typeid, sdt, edt, pagesize, page, orderby);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region GetAgentTotalAmt
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentid"></param>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <returns></returns>
        public static decimal GetAgentTotalAmt(int agentid, DateTime sdt, DateTime edt)
        {
            try
            {
                return dal.GetAgentTotalAmt(agentid,  sdt, edt);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }
        #endregion

        #region GetAgentIncome
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentid"></param>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <returns></returns>
        public static decimal GetAgentIncome(int agentid, DateTime sdt, DateTime edt)
        {
            try
            {
                return dal.GetAgentIncome(agentid, sdt, edt);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }
        #endregion

        #region BusinessStat4
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static DataSet BusinessStat4(DateTime sdt, DateTime edt, int page, int pagesize, string orderby)
        {
            try
            {
                return dal.BusinessStat4(sdt, edt, page, pagesize, orderby);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region BusinessStat7
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <returns></returns>
        public static DataSet BusinessStat7(DateTime sdt, DateTime edt)
        {
            try
            {
                return dal.BusinessStat7(sdt, edt);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion
    }
}
