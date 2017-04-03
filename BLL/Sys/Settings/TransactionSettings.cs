using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Sys
{
    /// <summary>
    /// 交易类配置
    /// </summary>
    public class TransactionSettings
    {
        #region GetKeyValues
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetKeyValues()
        {
            try
            {
                return SysConfig.Instance.GetList("type=2").Tables[0];
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
        #endregion

        #region MinTranATM
        /// <summary>
        /// 最小交易金额
        /// </summary>
        public static decimal MinTranATM
        {
            get
            {
                decimal minTranATM = 0.02M;
                string value = SysConfig.Instance.GetValue("MinTranATM");
                if (!string.IsNullOrEmpty(value))
                {
                    if (!decimal.TryParse(value, out minTranATM))
                    {
                        minTranATM = 0.02M;
                    }
                }
                return minTranATM;
            }
        }
        #endregion

        #region MaxTranATM
        /// <summary>
        /// 最大交易金额
        /// </summary>
        public static decimal MaxTranATM
        {
            get
            {
                decimal maxTranATM = 5000M;
                string value = SysConfig.Instance.GetValue("MaxTranATM");
                if (!string.IsNullOrEmpty(value))
                {
                    if (!decimal.TryParse(value, out maxTranATM))
                    {
                        maxTranATM = 5000M;
                    }
                }
                return maxTranATM;
            }
        }
        #endregion

        #region ExpiresTime
        /// <summary>
        /// 订单信息缓存失效时间 单位分钟
        /// </summary>
        public static int ExpiresTime
        {
            get
            {
                int count = 5;
                string value = SysConfig.Instance.GetValue("ExpiresTime");
                if (!string.IsNullOrEmpty(value))
                {
                    if (!int.TryParse(value, out count))
                    {
                        count = 5;
                    }
                }
                return count;
            }
        }
        #endregion

        #region CheckUrlReferrer
        /// <summary>
        /// 是否验证来路 1 判定 0 不判定
        /// </summary>
        public static bool CheckUrlReferrer
        {
            get
            {
                return SysConfig.Instance.GetValue("CheckUrlReferrer") == "1";
            }
        }
        #endregion

        #region OrderPrefix
        /// <summary>
        /// 订单号前缀
        /// </summary>
        public static string OrderPrefix
        {
            get
            {
                return SysConfig.Instance.GetValue("OrderPrefix");
            }
        }
        #endregion

        #region CheckUserOrderNo
        /// <summary>
        /// 订单号重复检查
        /// </summary>
        public static bool CheckUserOrderNo
        {
            get
            {
                return SysConfig.Instance.GetValue("CheckUserOrderNo") == "1";
            }
        }
        #endregion

        #region RiskWarning
        /// <summary>
        /// 显示交易风险提示
        /// </summary>
        public static bool RiskWarning
        {
            get
            {
                return SysConfig.Instance.GetValue("RiskWarning") == "1";
            }
        }
        #endregion

        #region RiskWarning_Bank
        /// <summary>
        /// 显示交易风险提示
        /// </summary>
        public static bool RiskWarning_Bank
        {
            get
            {
                return SysConfig.Instance.GetValue("RiskWarning_Bank") == "1";
            }
        }
        #endregion

        #region RiskWarning_Alipay
        /// <summary>
        /// 显示交易风险提示
        /// </summary>
        public static bool RiskWarning_Alipay
        {
            get
            {
                return SysConfig.Instance.GetValue("RiskWarning_Alipay") == "1";
            }
        }
        #endregion

        #region RiskWarning_AliCode
        /// <summary>
        /// 显示交易风险提示
        /// </summary>
        public static bool RiskWarning_AliCode
        {
            get
            {
                return SysConfig.Instance.GetValue("RiskWarning_AliCode") == "1";
            }
        }
        #endregion

        #region RiskWarning
        /// <summary>
        /// 显示交易风险提示
        /// </summary>
        public static bool RiskWarning_WXpay
        {
            get
            {
                return SysConfig.Instance.GetValue("RiskWarning_WXpay") == "1";
            }
        }
        #endregion

        #region Debuglog
        /// <summary>
        /// 记录交易错误日志（方便查错）
        /// </summary>
        public static bool Debuglog
        {
            get
            {
                return SysConfig.Instance.GetValue("Debuglog") == "1";
            }
        }
        #endregion

        #region RefCount
        /// <summary>
        /// 有来路站点个数
        /// </summary>
        public static int RefCount
        {
            get
            {
                int count = 1;
                string value = SysConfig.Instance.GetValue("RefCount");
                if (!string.IsNullOrEmpty(value))
                {
                    if (!int.TryParse(value, out count))
                    {
                        count = 1;
                    }
                }
                return count;
            }
        }
        #endregion

        #region WithoutRef
        /// <summary>
        /// 无来路开启状态
        /// </summary>
        public static bool WithoutRef
        {
            get
            {
                return SysConfig.Instance.GetValue("WithoutRef") == "1";
            }
        }
        #endregion

        #region OpenDeduct
        /// <summary>
        /// 启用扣量
        /// </summary>
        public static bool OpenDeduct
        {
            get
            {
                return SysConfig.Instance.GetValue("OpenDeduct") == "1";
            }
        }
        #endregion
    }
}
