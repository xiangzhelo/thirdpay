using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using viviapi.BLL.Sys;
using viviLib.Web;

namespace viviapi.WebComponents.GateWay
{
    /// <summary>
    /// 
    /// </summary>
    public class BankTransBase:Page
    {
        #region 系统配置
        //最大交易额
        /// <summary>
        /// 
        /// </summary>
        public decimal MaxChargeAMT = TransactionSettings.MaxTranATM;
        //最小交易额
        /// <summary>
        /// 
        /// </summary>
        public decimal MinTranAMT = TransactionSettings.MinTranATM;

        /// <summary>
        /// 是否需要验证来路-域名绑定
        /// </summary>
        protected bool RequiredCheckUrlReferrer
        {
            get
            {
                return TransactionSettings.CheckUrlReferrer;
            }
        }

        /// <summary>
        /// 是否需要验证订单号是否重复
        /// </summary>
        protected bool RequiredCheckUserOrderNo
        {
            get
            {
                return TransactionSettings.CheckUserOrderNo;
            }
        }

        /// <summary>
        /// 订单号前缀
        /// </summary>
        public string OrderPrefix
        {
            get
            {
                return TransactionSettings.OrderPrefix;
            }
        }

        /// <summary>
        /// 缓存超时时间
        /// </summary>
        public int ExpiresTime
        {
            get
            {
                return TransactionSettings.ExpiresTime;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        protected bool DebuglogOpen
        {
            get
            {
                return TransactionSettings.Debuglog;
            }
        }
        #endregion

        public string GetParmValue(string parmName)
        {
            string parmValue = WebBase.GetQueryStringString(parmName, "");
            if (string.IsNullOrEmpty(parmValue))
            {
                parmValue = WebBase.GetFormString(parmName, "");
            }
            return parmValue;
        }
    }
}
