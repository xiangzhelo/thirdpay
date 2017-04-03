using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using viviLib;
using DBAccess;
using viviLib.ExceptionHandling;

namespace viviapi.BLL
{
    public class SysConfig
    {
        internal static string SYSCONFIG_CACHEKEY = "SysConfig_Getlist";
        internal static string SQL_TABLE = "SysConfig";
        internal static string SQL_TABLE_FIELD = "id,value";

        #region IsAudit
        /// <summary>
        /// 商户注册是否审核
        /// </summary>
        public static bool IsAudit
        {
            get
            {
                return getData(1) == "1";
            }
        }
         #endregion

        #region IsOpenRegistration
        /// <summary>
        /// 是否开启注册
        /// </summary>
        public static bool IsOpenRegistration
        {
            get
            {
                return getData(2) == "1";
            }
        }
         #endregion

        #region IsPhoneVerification
        /// <summary>
        /// 需要手机验证
        /// </summary>
        public static bool IsPhoneVerification
        {
            get
            {
                return getData(3) == "1";
            }
        }
         #endregion

        #region MaxInformationNumber
        /// <summary>
        /// 每个手机最多可以发送信息条数
        /// </summary>
        public static int MaxInformationNumber
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(4)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(4));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion        

        #region 支付通道商设置
        #region 网银支付
        /// <summary>
        /// 当前网银支付的通道商
        /// </summary>
        public static int BankPaySupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(5)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(5));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region 默认点卡
        /// <summary>
        /// 当前默认点卡的通道商
        /// </summary>
        public static int DefaultCardPaySupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(6)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(6));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region ShenZhouXing
        /// <summary>
        /// 当前默认点卡的通道商
        /// </summary>
        public static int PayShenZhouXingSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(7)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(7));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region ShengDa
        /// <summary>
        /// 当前默认点卡的通道商
        /// </summary>
        public static int PayShengDaSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(8)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(8));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region ZhengTu
        /// <summary>
        /// 当前默认点卡的通道商
        /// </summary>
        public static int PayZhengTuSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(9)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(9));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region JuWang
        /// <summary>
        /// 当前默认点卡的通道商
        /// </summary>
        public static int PayJuWangSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(10)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(10));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region QQ
        /// <summary>
        /// 当前默认点卡的通道商
        /// </summary>
        public static int PayQQSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(11)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(11));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region LianTong
        /// <summary>
        /// 当前默认点卡的通道商
        /// </summary>
        public static int PayLianTongSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(12)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(12));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region JiuYou
        /// <summary>
        /// 当前默认点卡的通道商
        /// </summary>
        public static int PayJiuYouSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(13)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(13));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region WangYi
        /// <summary>
        /// 当前默认点卡的通道商
        /// </summary>
        public static int PayWangYiSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(14)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(14));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region WanMei
        /// <summary>
        /// 当前默认点卡的通道商
        /// </summary>
        public static int PayWanMeiSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(15)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(15));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region ShuHu
        /// <summary>
        /// 当前默认点卡的通道商
        /// </summary>
        public static int PayShuHuSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(16)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(16));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region DianXin
        /// <summary>
        /// 当前默认点卡的通道商
        /// </summary>
        public static int PayDianXinSupId
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(17)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(17));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion
        #endregion

        #region 提现设置

        #region T+0
        #region MinGetMoney
        /// <summary>
        /// 最小提现金额
        /// </summary>
        public static decimal MinGetMoney
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(25)))
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(getData(25));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region MaxGetMoney
        /// <summary>
        /// 最大提现金额
        /// </summary>
        public static decimal MaxGetMoney
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(26)))
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(getData(26));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion         

        #region Charges
        /// <summary>
        /// 最大提现金额
        /// </summary>
        public static decimal Charges
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(28)))
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(getData(28));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion        

        #region CashTimesEveryDay
        /// <summary>
        /// 最大提现金额
        /// </summary>
        public static int CashTimesEveryDay
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(27)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(27));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion        
        #endregion

        #region T+1
        #region MinGetMoney
        /// <summary>
        /// 最小提现金额
        /// </summary>
        public static decimal MinGetMoney1
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(29)))
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(getData(29));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region MaxGetMoney1
        /// <summary>
        /// 最大提现金额
        /// </summary>
        public static decimal MaxGetMoney1
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(30)))
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(getData(30));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region Charges
        /// <summary>
        /// 最大提现金额
        /// </summary>
        public static decimal Charges1
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(32)))
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(getData(32));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region CashTimesEveryDay
        /// <summary>
        /// 最大提现金额
        /// </summary>
        public static int CashTimesEveryDay1
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(31)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(31));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion
        #endregion

        #region T+7
        #region MinGetMoney
        /// <summary>
        /// 最小提现金额
        /// </summary>
        public static decimal MinGetMoney2
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(33)))
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(getData(33));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region MaxGetMoney
        /// <summary>
        /// 最大提现金额
        /// </summary>
        public static decimal MaxGetMoney2
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(34)))
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(getData(34));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region Charges
        /// <summary>
        /// 最大提现金额
        /// </summary>
        public static decimal Charges2
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(36)))
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(getData(36));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion

        #region CashTimesEveryDay
        /// <summary>
        /// 最大提现金额
        /// </summary>
        public static int CashTimesEveryDay2
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(35)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(35));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }
        #endregion
        #endregion
        #endregion

        /// <summary>
        /// 有来路站点个数
        /// </summary>
        public static int LaiLuCount
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(37)))
                    {
                        return 0;
                    }

                    return Convert.ToInt32(getData(37));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return 0;
                }
            }
        }

        /// <summary>
        /// 无来路开启状态 1 开启 0 关闭
        /// </summary>
        public static bool IsOpenNoLaiLu
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(getData(38)))
                    {
                        return false;
                    }

                    return Convert.ToInt32(getData(38)) == 1;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool Update(int id, string value)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update SysConfig set ");
                strSql.Append("value=@value");
                strSql.Append(" where id=@id");
                SqlParameter[] parameters = {					
					new SqlParameter("@value", SqlDbType.VarChar,100),
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = value;
                parameters[1].Value = id;

                ClearCache();
                return DataBase.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }

        }

        private static string getData(int id)
        {
            try
            {
                DataSet data = GetCacheList();
                if (data == null)
                    return string.Empty;

                DataRow[] result = data.Tables[0].Select("id=" + id.ToString());
                if (result == null || result.Length < 1)
                    return string.Empty;

                return result[0]["value"].ToString();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static DataSet GetCacheList()
        {
            try
            {
                string cacheKey = SYSCONFIG_CACHEKEY;

                DataSet data = new DataSet();
                data = (DataSet)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

                if (data == null)
                {
                    SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_TABLE, SQL_TABLE_FIELD, string.Empty, null);

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(" select id,type,value ");
                    strSql.Append(" FROM SysConfig ");

                    data = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());

                    viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, data);
                }
                return data;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }            
        }

        internal static void ClearCache()
        {
            string cahcekey = SYSCONFIG_CACHEKEY;
            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cahcekey);
        }
    }
}

