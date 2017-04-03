using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using viviLib;
using viviapi.Model.Payment;
using viviapi.Model.User;
using DBAccess;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Payment
{
    /// <summary>
    /// 费率
    /// </summary>
    public sealed class PayRateFactory
    {
        internal static string PAYRATEFACTORY_CACHEKEY = Sys.Constant.CacheMark + "{{D2F59122-0613-4968-ABD1-9A8F9FFD7B13}}_{0}_{1}";
        internal static string SQL_TABLE = "payrate";
        internal static string SQL_TABLE_FIELD = @"[id],[rateType],[userLevel],[levName],[p100],[p101],[p102],[p103],[p104],[p105],[p106],[p107],[p108],[p109],[p110],[p111],[p112],[p113],[p114],[p115],[p116],[p117],[p118],[p119],[p300],[p200]
      ,[p201]
      ,[p202]
      ,[p203]
      ,[p204]
      ,[p205]
	  ,[p206]
      ,[p207]
      ,[p208]
      ,[p209]
      ,[p210]
      ,[p206]";

        public static DataTable GetUserlevData()
        {
            string sql = @"select 
      [userLevel]
      ,[levName] from [payrate] where rateType = 3 or rateType = 4 ";

            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, sql);
            return ds.Tables[0];
        }


        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static int Add(PayRate model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@rateType", SqlDbType.TinyInt,1),
					new SqlParameter("@userLevel", SqlDbType.Int,4),
					new SqlParameter("@levName", SqlDbType.VarChar,50),
					new SqlParameter("@p100", SqlDbType.Decimal,9),
					new SqlParameter("@p101", SqlDbType.Decimal,9),
					new SqlParameter("@p102", SqlDbType.Decimal,9),
					new SqlParameter("@p103", SqlDbType.Decimal,9),
					new SqlParameter("@p104", SqlDbType.Decimal,9),
					new SqlParameter("@p105", SqlDbType.Decimal,9),
					new SqlParameter("@p106", SqlDbType.Decimal,9),
					new SqlParameter("@p107", SqlDbType.Decimal,9),
					new SqlParameter("@p108", SqlDbType.Decimal,9),
					new SqlParameter("@p109", SqlDbType.Decimal,9),
					new SqlParameter("@p110", SqlDbType.Decimal,9),
					new SqlParameter("@p111", SqlDbType.Decimal,9),
					new SqlParameter("@p112", SqlDbType.Decimal,9),
					new SqlParameter("@p113", SqlDbType.Decimal,9),
					new SqlParameter("@p114", SqlDbType.Decimal,9),
					new SqlParameter("@p115", SqlDbType.Decimal,9),
					new SqlParameter("@p116", SqlDbType.Decimal,9),
					new SqlParameter("@p117", SqlDbType.Decimal,9),
					new SqlParameter("@p118", SqlDbType.Decimal,9),
					new SqlParameter("@p119", SqlDbType.Decimal,9),
					new SqlParameter("@p300", SqlDbType.Decimal,9),
                    new SqlParameter("@p200", SqlDbType.Decimal,9),
					new SqlParameter("@p201", SqlDbType.Decimal,9),
					new SqlParameter("@p202", SqlDbType.Decimal,9),
					new SqlParameter("@p203", SqlDbType.Decimal,9),
					new SqlParameter("@p204", SqlDbType.Decimal,9),
					new SqlParameter("@p205", SqlDbType.Decimal,9),
					new SqlParameter("@p207", SqlDbType.Decimal,9),
					new SqlParameter("@p208", SqlDbType.Decimal,9),
					new SqlParameter("@p209", SqlDbType.Decimal,9),
					new SqlParameter("@p210", SqlDbType.Decimal,9),
					new SqlParameter("@p206", SqlDbType.Decimal,9)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.rateType;
                parameters[2].Value = model.userLevel;
                parameters[3].Value = model.levName;
                parameters[4].Value = model.p100;
                parameters[5].Value = model.p101;
                parameters[6].Value = model.p102;
                parameters[7].Value = model.p103;
                parameters[8].Value = model.p104;
                parameters[9].Value = model.p105;
                parameters[10].Value = model.p106;
                parameters[11].Value = model.p107;
                parameters[12].Value = model.p108;
                parameters[13].Value = model.p109;
                parameters[14].Value = model.p110;
                parameters[15].Value = model.p111;
                parameters[16].Value = model.p112;
                parameters[17].Value = model.p113;
                parameters[18].Value = model.p114;
                parameters[19].Value = model.p115;
                parameters[20].Value = model.p116;
                parameters[21].Value = model.p117;
                parameters[22].Value = model.p118;
                parameters[23].Value = model.p119;
                parameters[24].Value = model.p300;

                parameters[25].Value = model.p200;
                parameters[26].Value = model.p201;
                parameters[27].Value = model.p202;
                parameters[28].Value = model.p203;
                parameters[29].Value = model.p204;
                parameters[30].Value = model.p205;
                parameters[31].Value = model.p207;
                parameters[32].Value = model.p208;
                parameters[33].Value = model.p209;
                parameters[34].Value = model.p210;
                parameters[35].Value = model.p206;

                int rows = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_payrate_add", parameters);
                if (rows > 0)
                {
                    int modelid = (int)parameters[0].Value;
                    return modelid;               
                }
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }


        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(PayRate model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@rateType", SqlDbType.TinyInt,1),
					new SqlParameter("@userLevel", SqlDbType.Int,4),
					new SqlParameter("@levName", SqlDbType.VarChar,50),
					new SqlParameter("@p100", SqlDbType.Decimal,9),
					new SqlParameter("@p101", SqlDbType.Decimal,9),
					new SqlParameter("@p102", SqlDbType.Decimal,9),
					new SqlParameter("@p103", SqlDbType.Decimal,9),
					new SqlParameter("@p104", SqlDbType.Decimal,9),
					new SqlParameter("@p105", SqlDbType.Decimal,9),
					new SqlParameter("@p106", SqlDbType.Decimal,9),
					new SqlParameter("@p107", SqlDbType.Decimal,9),
					new SqlParameter("@p108", SqlDbType.Decimal,9),
					new SqlParameter("@p109", SqlDbType.Decimal,9),
					new SqlParameter("@p110", SqlDbType.Decimal,9),
					new SqlParameter("@p111", SqlDbType.Decimal,9),
					new SqlParameter("@p112", SqlDbType.Decimal,9),
					new SqlParameter("@p113", SqlDbType.Decimal,9),
					new SqlParameter("@p114", SqlDbType.Decimal,9),
					new SqlParameter("@p115", SqlDbType.Decimal,9),
					new SqlParameter("@p116", SqlDbType.Decimal,9),
					new SqlParameter("@p117", SqlDbType.Decimal,9),
					new SqlParameter("@p118", SqlDbType.Decimal,9),
					new SqlParameter("@p119", SqlDbType.Decimal,9),
					new SqlParameter("@p300", SqlDbType.Decimal,9),
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@p200", SqlDbType.Decimal,9),
					new SqlParameter("@p201", SqlDbType.Decimal,9),
					new SqlParameter("@p202", SqlDbType.Decimal,9),
					new SqlParameter("@p203", SqlDbType.Decimal,9),
					new SqlParameter("@p204", SqlDbType.Decimal,9),
					new SqlParameter("@p205", SqlDbType.Decimal,9),
					new SqlParameter("@p207", SqlDbType.Decimal,9),
					new SqlParameter("@p208", SqlDbType.Decimal,9),
					new SqlParameter("@p209", SqlDbType.Decimal,9),
					new SqlParameter("@p210", SqlDbType.Decimal,9),
                    new SqlParameter("@p206", SqlDbType.Decimal,9)};
                parameters[0].Value = (int)model.rateType;
                parameters[1].Value = model.userLevel;
                parameters[2].Value = model.levName;
                parameters[3].Value = model.p100;
                parameters[4].Value = model.p101;
                parameters[5].Value = model.p102;
                parameters[6].Value = model.p103;
                parameters[7].Value = model.p104;
                parameters[8].Value = model.p105;
                parameters[9].Value = model.p106;
                parameters[10].Value = model.p107;
                parameters[11].Value = model.p108;
                parameters[12].Value = model.p109;
                parameters[13].Value = model.p110;
                parameters[14].Value = model.p111;
                parameters[15].Value = model.p112;
                parameters[16].Value = model.p113;
                parameters[17].Value = model.p114;
                parameters[18].Value = model.p115;
                parameters[19].Value = model.p116;
                parameters[20].Value = model.p117;
                parameters[21].Value = model.p118;
                parameters[22].Value = model.p119;
                parameters[23].Value = model.p300;
                parameters[24].Value = model.id;

                parameters[25].Value = model.p200;
                parameters[26].Value = model.p201;
                parameters[27].Value = model.p202;
                parameters[28].Value = model.p203;
                parameters[29].Value = model.p204;
                parameters[30].Value = model.p205;
                parameters[31].Value = model.p207;
                parameters[32].Value = model.p208;
                parameters[33].Value = model.p209;
                parameters[34].Value = model.p210;
                parameters[35].Value = model.p206;

                int rows = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_payrate_Update", parameters);
                if (rows > 0)
                {
                    string cacheKey = string.Format(PAYRATEFACTORY_CACHEKEY, (int)model.rateType, model.userLevel);
                    viviapi.Cache.WebCache.GetCacheService().RemoveObject(cacheKey);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region GetPayRate
        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="rateType"></param>
        /// <param name="lev"></param>
        /// <param name="paytype"></param>
        /// <returns></returns>
        public static decimal GetPayRate(RateTypeEnum rateType,int lev, int paytype)
        {
            try
            {
                decimal rate = 0;
                DataTable data = GetList(rateType, lev);
                if (data == null && data.Rows.Count < 1)
                    return rate;
                
                DataRow[] dr = data.Select("userLevel=" + lev.ToString() + " and rateType=" + ((int)rateType).ToString());
                if (dr == null || dr.Length <= 0)
                    return 0M;

                return Convert.ToDecimal(dr[0]["p" + paytype.ToString()]);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

      
        
        /// <summary>
        /// 取平台费用
        /// </summary>
        /// <param name="paytype"></param>
        /// <returns></returns>
        public static decimal GetPlatformRate(int paytype)
        {
            return GetPayRate(RateTypeEnum.Platform, -2, paytype);
        }
        #endregion

        #region GetList
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rateType"></param>
        /// <param name="lev"></param>
        /// <returns></returns>
        public static DataTable GetList(string where)
        {
            try
            {
                DataSet ds = new DataSet();
                string sql = @"select [id]
      ,[rateType]
      ,[userLevel]
      ,[levName]
      ,isnull([p100],0) as p100
      ,isnull([p101],0) as p101
      ,isnull([p102],0) as p102
      ,isnull([p103],0) as p103
      ,isnull([p104],0) as p104
      ,isnull([p105],0) as p105
      ,isnull([p106],0) as p106
      ,isnull([p107],0) as p107
      ,isnull([p108],0) as p108
      ,isnull([p109],0) as p109
      ,isnull([p110],0) as p110
      ,isnull([p111],0) as p111
      ,isnull([p112],0) as p112
      ,isnull([p113],0) as p113
      ,isnull([p114],0) as p114
      ,isnull([p115],0) as p115
      ,isnull([p116],0) as p116
      ,isnull([p117],0) as p117
      ,isnull([p118],0) as p118
      ,isnull([p119],0) as p119
      ,isnull([p300],0) as p300
      ,isnull([p200],0) as p200
      ,isnull([p201],0) as p201
      ,isnull([p202],0) as p202
      ,isnull([p203],0) as p203
      ,isnull([p204],0) as p204
      ,isnull([p205],0) as p205
      ,isnull([p206],0) as p206
      ,isnull([p207],0) as p207
      ,isnull([p208],0) as p208
      ,isnull([p209],0) as p209
      ,isnull([p210],0) as p210 from [payrate] where ";
                sql += where;

                ds = DataBase.ExecuteDataset(CommandType.Text, sql);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rateType"></param>
        /// <param name="lev"></param>
        /// <returns></returns>
        public static DataTable GetList(RateTypeEnum rateType, int lev)
        {
            try
            {
                string cacheKey = string.Format(PAYRATEFACTORY_CACHEKEY, (int)rateType, lev);

                DataSet ds = new DataSet();
                ds = (DataSet)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

                if (ds == null)
                {
                    IDictionary<string, object> sqldepparms = new Dictionary<string, object>();
                    sqldepparms.Add("rateType", (int)rateType);
                    sqldepparms.Add("userLevel", lev);
                    SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_TABLE, SQL_TABLE_FIELD, "[rateType]=@rateType and userLevel=@userLevel", sqldepparms);
                                        
                    string sql = @"select [id],[rateType],[userLevel],[levName],[p100]
      ,[p101]
      ,[p102]
      ,[p103]
      ,[p104]
      ,[p105]
      ,[p106]
      ,[p107]
      ,[p108]
      ,[p109]
      ,[p110]
      ,[p111]
      ,[p112]
      ,[p113]
      ,[p114]
      ,[p115]
      ,[p116]
      ,[p117]
      ,[p118]
      ,[p119]
      ,[p300]
      ,[p200]
      ,[p201]
      ,[p202]
      ,[p203]
      ,[p204]
      ,[p205]
      ,[p206]
      ,[p207]
      ,[p208]
      ,[p209]
      ,[p210] from [payrate]";
                    ds = DataBase.ExecuteDataset(CommandType.Text, sql);
                    viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, ds);
                }
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        public static DataTable GetLevName(RateTypeEnum rateType)
        {
            string sql = "SELECT id,userLevel,levName FROM payrate WHERE rateType = @rateType";
            SqlParameter[] parameters = {
					new SqlParameter("@rateType", SqlDbType.TinyInt,1)};
            parameters[0].Value = (int)rateType;

            return DataBase.ExecuteDataset(CommandType.Text, sql, parameters).Tables[0];
        }
        public static string GetUserLevName(int userid)
        {
            try
            {
                string sql = @"SELECT a.levName FROM payrate a,userbase b WHERE
a.userLevel = b.userLevel and b.ID =@userid ";
                SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4)};
                parameters[0].Value = userid;

                return Convert.ToString(DataBase.ExecuteScalar(CommandType.Text, sql, parameters));
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static PayRate GetModel(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.Int,4) };
            parameters[0].Value = id;

            PayRate model = new PayRate();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure,"proc_payrate_GetModel", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["rateType"] != null && ds.Tables[0].Rows[0]["rateType"].ToString() != "")
                {
                    model.rateType = (RateTypeEnum)int.Parse(ds.Tables[0].Rows[0]["rateType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userLevel"] != null && ds.Tables[0].Rows[0]["userLevel"].ToString() != "")
                {
                    model.userLevel = int.Parse(ds.Tables[0].Rows[0]["userLevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["levName"] != null && ds.Tables[0].Rows[0]["levName"].ToString() != "")
                {
                    model.levName = ds.Tables[0].Rows[0]["levName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["p100"] != null && ds.Tables[0].Rows[0]["p100"].ToString() != "")
                {
                    model.p100 = decimal.Parse(ds.Tables[0].Rows[0]["p100"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p101"] != null && ds.Tables[0].Rows[0]["p101"].ToString() != "")
                {
                    model.p101 = decimal.Parse(ds.Tables[0].Rows[0]["p101"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p102"] != null && ds.Tables[0].Rows[0]["p102"].ToString() != "")
                {
                    model.p102 = decimal.Parse(ds.Tables[0].Rows[0]["p102"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p103"] != null && ds.Tables[0].Rows[0]["p103"].ToString() != "")
                {
                    model.p103 = decimal.Parse(ds.Tables[0].Rows[0]["p103"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p104"] != null && ds.Tables[0].Rows[0]["p104"].ToString() != "")
                {
                    model.p104 = decimal.Parse(ds.Tables[0].Rows[0]["p104"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p105"] != null && ds.Tables[0].Rows[0]["p105"].ToString() != "")
                {
                    model.p105 = decimal.Parse(ds.Tables[0].Rows[0]["p105"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p106"] != null && ds.Tables[0].Rows[0]["p106"].ToString() != "")
                {
                    model.p106 = decimal.Parse(ds.Tables[0].Rows[0]["p106"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p107"] != null && ds.Tables[0].Rows[0]["p107"].ToString() != "")
                {
                    model.p107 = decimal.Parse(ds.Tables[0].Rows[0]["p107"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p108"] != null && ds.Tables[0].Rows[0]["p108"].ToString() != "")
                {
                    model.p108 = decimal.Parse(ds.Tables[0].Rows[0]["p108"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p109"] != null && ds.Tables[0].Rows[0]["p109"].ToString() != "")
                {
                    model.p109 = decimal.Parse(ds.Tables[0].Rows[0]["p109"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p110"] != null && ds.Tables[0].Rows[0]["p110"].ToString() != "")
                {
                    model.p110 = decimal.Parse(ds.Tables[0].Rows[0]["p110"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p111"] != null && ds.Tables[0].Rows[0]["p111"].ToString() != "")
                {
                    model.p111 = decimal.Parse(ds.Tables[0].Rows[0]["p111"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p112"] != null && ds.Tables[0].Rows[0]["p112"].ToString() != "")
                {
                    model.p112 = decimal.Parse(ds.Tables[0].Rows[0]["p112"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p113"] != null && ds.Tables[0].Rows[0]["p113"].ToString() != "")
                {
                    model.p113 = decimal.Parse(ds.Tables[0].Rows[0]["p113"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p114"] != null && ds.Tables[0].Rows[0]["p114"].ToString() != "")
                {
                    model.p114 = decimal.Parse(ds.Tables[0].Rows[0]["p114"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p115"] != null && ds.Tables[0].Rows[0]["p115"].ToString() != "")
                {
                    model.p115 = decimal.Parse(ds.Tables[0].Rows[0]["p115"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p116"] != null && ds.Tables[0].Rows[0]["p116"].ToString() != "")
                {
                    model.p116 = decimal.Parse(ds.Tables[0].Rows[0]["p116"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p117"] != null && ds.Tables[0].Rows[0]["p117"].ToString() != "")
                {
                    model.p117 = decimal.Parse(ds.Tables[0].Rows[0]["p117"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p118"] != null && ds.Tables[0].Rows[0]["p118"].ToString() != "")
                {
                    model.p118 = decimal.Parse(ds.Tables[0].Rows[0]["p118"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p119"] != null && ds.Tables[0].Rows[0]["p119"].ToString() != "")
                {
                    model.p119 = decimal.Parse(ds.Tables[0].Rows[0]["p119"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p300"] != null && ds.Tables[0].Rows[0]["p300"].ToString() != "")
                {
                    model.p300 = decimal.Parse(ds.Tables[0].Rows[0]["p300"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p200"] != null && ds.Tables[0].Rows[0]["p200"].ToString() != "")
                {
                    model.p200 = decimal.Parse(ds.Tables[0].Rows[0]["p200"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p201"] != null && ds.Tables[0].Rows[0]["p201"].ToString() != "")
                {
                    model.p201 = decimal.Parse(ds.Tables[0].Rows[0]["p201"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p202"] != null && ds.Tables[0].Rows[0]["p202"].ToString() != "")
                {
                    model.p202 = decimal.Parse(ds.Tables[0].Rows[0]["p202"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p203"] != null && ds.Tables[0].Rows[0]["p203"].ToString() != "")
                {
                    model.p203 = decimal.Parse(ds.Tables[0].Rows[0]["p203"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p204"] != null && ds.Tables[0].Rows[0]["p204"].ToString() != "")
                {
                    model.p204 = decimal.Parse(ds.Tables[0].Rows[0]["p204"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p205"] != null && ds.Tables[0].Rows[0]["p205"].ToString() != "")
                {
                    model.p205 = decimal.Parse(ds.Tables[0].Rows[0]["p205"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p206"] != null && ds.Tables[0].Rows[0]["p206"].ToString() != "")
                {
                    model.p206 = decimal.Parse(ds.Tables[0].Rows[0]["p206"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p207"] != null && ds.Tables[0].Rows[0]["p207"].ToString() != "")
                {
                    model.p207 = decimal.Parse(ds.Tables[0].Rows[0]["p207"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p208"] != null && ds.Tables[0].Rows[0]["p208"].ToString() != "")
                {
                    model.p208 = decimal.Parse(ds.Tables[0].Rows[0]["p208"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p209"] != null && ds.Tables[0].Rows[0]["p209"].ToString() != "")
                {
                    model.p209 = decimal.Parse(ds.Tables[0].Rows[0]["p209"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p210"] != null && ds.Tables[0].Rows[0]["p210"].ToString() != "")
                {
                    model.p210 = decimal.Parse(ds.Tables[0].Rows[0]["p210"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static PayRate GetModelByUser(int userid)
        {
            SqlParameter[] parameters = { new SqlParameter("@userid", SqlDbType.Int, 4) };
            parameters[0].Value = userid;

            PayRate model = new PayRate();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_payrate_GetModelByUser", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["rateType"] != null && ds.Tables[0].Rows[0]["rateType"].ToString() != "")
                {
                    model.rateType = (RateTypeEnum)int.Parse(ds.Tables[0].Rows[0]["rateType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userLevel"] != null && ds.Tables[0].Rows[0]["userLevel"].ToString() != "")
                {
                    model.userLevel = int.Parse(ds.Tables[0].Rows[0]["userLevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["levName"] != null && ds.Tables[0].Rows[0]["levName"].ToString() != "")
                {
                    model.levName = ds.Tables[0].Rows[0]["levName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["p100"] != null && ds.Tables[0].Rows[0]["p100"].ToString() != "")
                {
                    model.p100 = decimal.Parse(ds.Tables[0].Rows[0]["p100"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p101"] != null && ds.Tables[0].Rows[0]["p101"].ToString() != "")
                {
                    model.p101 = decimal.Parse(ds.Tables[0].Rows[0]["p101"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p102"] != null && ds.Tables[0].Rows[0]["p102"].ToString() != "")
                {
                    model.p102 = decimal.Parse(ds.Tables[0].Rows[0]["p102"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p103"] != null && ds.Tables[0].Rows[0]["p103"].ToString() != "")
                {
                    model.p103 = decimal.Parse(ds.Tables[0].Rows[0]["p103"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p104"] != null && ds.Tables[0].Rows[0]["p104"].ToString() != "")
                {
                    model.p104 = decimal.Parse(ds.Tables[0].Rows[0]["p104"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p105"] != null && ds.Tables[0].Rows[0]["p105"].ToString() != "")
                {
                    model.p105 = decimal.Parse(ds.Tables[0].Rows[0]["p105"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p106"] != null && ds.Tables[0].Rows[0]["p106"].ToString() != "")
                {
                    model.p106 = decimal.Parse(ds.Tables[0].Rows[0]["p106"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p107"] != null && ds.Tables[0].Rows[0]["p107"].ToString() != "")
                {
                    model.p107 = decimal.Parse(ds.Tables[0].Rows[0]["p107"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p108"] != null && ds.Tables[0].Rows[0]["p108"].ToString() != "")
                {
                    model.p108 = decimal.Parse(ds.Tables[0].Rows[0]["p108"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p109"] != null && ds.Tables[0].Rows[0]["p109"].ToString() != "")
                {
                    model.p109 = decimal.Parse(ds.Tables[0].Rows[0]["p109"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p110"] != null && ds.Tables[0].Rows[0]["p110"].ToString() != "")
                {
                    model.p110 = decimal.Parse(ds.Tables[0].Rows[0]["p110"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p111"] != null && ds.Tables[0].Rows[0]["p111"].ToString() != "")
                {
                    model.p111 = decimal.Parse(ds.Tables[0].Rows[0]["p111"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p112"] != null && ds.Tables[0].Rows[0]["p112"].ToString() != "")
                {
                    model.p112 = decimal.Parse(ds.Tables[0].Rows[0]["p112"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p113"] != null && ds.Tables[0].Rows[0]["p113"].ToString() != "")
                {
                    model.p113 = decimal.Parse(ds.Tables[0].Rows[0]["p113"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p114"] != null && ds.Tables[0].Rows[0]["p114"].ToString() != "")
                {
                    model.p114 = decimal.Parse(ds.Tables[0].Rows[0]["p114"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p115"] != null && ds.Tables[0].Rows[0]["p115"].ToString() != "")
                {
                    model.p115 = decimal.Parse(ds.Tables[0].Rows[0]["p115"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p116"] != null && ds.Tables[0].Rows[0]["p116"].ToString() != "")
                {
                    model.p116 = decimal.Parse(ds.Tables[0].Rows[0]["p116"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p117"] != null && ds.Tables[0].Rows[0]["p117"].ToString() != "")
                {
                    model.p117 = decimal.Parse(ds.Tables[0].Rows[0]["p117"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p118"] != null && ds.Tables[0].Rows[0]["p118"].ToString() != "")
                {
                    model.p118 = decimal.Parse(ds.Tables[0].Rows[0]["p118"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p119"] != null && ds.Tables[0].Rows[0]["p119"].ToString() != "")
                {
                    model.p119 = decimal.Parse(ds.Tables[0].Rows[0]["p119"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p300"] != null && ds.Tables[0].Rows[0]["p300"].ToString() != "")
                {
                    model.p300 = decimal.Parse(ds.Tables[0].Rows[0]["p300"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p200"] != null && ds.Tables[0].Rows[0]["p200"].ToString() != "")
                {
                    model.p200 = decimal.Parse(ds.Tables[0].Rows[0]["p200"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p201"] != null && ds.Tables[0].Rows[0]["p201"].ToString() != "")
                {
                    model.p201 = decimal.Parse(ds.Tables[0].Rows[0]["p201"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p202"] != null && ds.Tables[0].Rows[0]["p202"].ToString() != "")
                {
                    model.p202 = decimal.Parse(ds.Tables[0].Rows[0]["p202"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p203"] != null && ds.Tables[0].Rows[0]["p203"].ToString() != "")
                {
                    model.p203 = decimal.Parse(ds.Tables[0].Rows[0]["p203"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p204"] != null && ds.Tables[0].Rows[0]["p204"].ToString() != "")
                {
                    model.p204 = decimal.Parse(ds.Tables[0].Rows[0]["p204"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p205"] != null && ds.Tables[0].Rows[0]["p205"].ToString() != "")
                {
                    model.p205 = decimal.Parse(ds.Tables[0].Rows[0]["p205"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p206"] != null && ds.Tables[0].Rows[0]["p206"].ToString() != "")
                {
                    model.p206 = decimal.Parse(ds.Tables[0].Rows[0]["p206"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p207"] != null && ds.Tables[0].Rows[0]["p207"].ToString() != "")
                {
                    model.p207 = decimal.Parse(ds.Tables[0].Rows[0]["p207"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p208"] != null && ds.Tables[0].Rows[0]["p208"].ToString() != "")
                {
                    model.p208 = decimal.Parse(ds.Tables[0].Rows[0]["p208"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p209"] != null && ds.Tables[0].Rows[0]["p209"].ToString() != "")
                {
                    model.p209 = decimal.Parse(ds.Tables[0].Rows[0]["p209"].ToString());
                }
                if (ds.Tables[0].Rows[0]["p210"] != null && ds.Tables[0].Rows[0]["p210"].ToString() != "")
                {
                    model.p210 = decimal.Parse(ds.Tables[0].Rows[0]["p210"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public static Decimal GetUserPayRate(UserInfo userInfo,int userId, int typeId)
        {
            BLL.User.usersetting _bll = new viviapi.BLL.User.usersetting();
            Model.User.usersettingInfo _setting = _bll.GetModel(userId);
            if (_setting != null)
            {
                if (_setting.special == 1)
                {
                    string payrate = _setting.payrate;
                     if (!string.IsNullOrEmpty(payrate))
                     {
                         string[] arr = payrate.Split('|');
                         foreach (string item in arr)
                         {
                             string[] arr1 = item.Split(':');
                             if (arr1[0] == typeId.ToString())
                             {
                                 return Convert.ToDecimal(arr1[1]);                                
                             }
                         }
                     }
                }
            }

            //商家费率
            RateTypeEnum rateType = RateTypeEnum.Member;
            if (userInfo.UserType == UserTypeEnum.代理)
                rateType = viviapi.Model.Payment.RateTypeEnum.Agent;

            return BLL.Payment.PayRateFactory.GetPayRate(rateType, (int)userInfo.UserLevel, typeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static Decimal GetUserPayRate(int userId, int typeId)
        {
            UserInfo userInfo = BLL.User.Factory.GetCacheUserBaseInfo(userId);
            if (userInfo == null)
                return 0M;

            BLL.User.usersetting _bll = new viviapi.BLL.User.usersetting();
            Model.User.usersettingInfo _setting = _bll.GetModel(userId);
            if (_setting != null)
            {
                if (_setting.special == 1)
                {
                    string payrate = _setting.payrate;
                    if (!string.IsNullOrEmpty(payrate))
                    {
                        string[] arr = payrate.Split('|');
                        foreach (string item in arr)
                        {
                            string[] arr1 = item.Split(':');
                            if (arr1[0] == typeId.ToString())
                            {
                                return Convert.ToDecimal(arr1[1]);
                            }
                        }
                    }
                }
            }

            //商家费率
            RateTypeEnum rateType = RateTypeEnum.Member;
            if (userInfo.UserType == UserTypeEnum.代理)
                rateType = viviapi.Model.Payment.RateTypeEnum.Agent;

            return BLL.Payment.PayRateFactory.GetPayRate(rateType, (int)userInfo.UserLevel, typeId);
        }      
        
    }
}