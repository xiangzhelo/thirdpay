using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Supplier
{
    public class Supplier
    {
        internal const string SQL_TABLE = "supplier";
        //money,pay,nopay,Integral
        internal const string SQL_TABLE_FIELDS = @"[id]
      ,[code]
      ,[name]
      ,[name1]
      ,[logourl]
      ,[isbank]
      ,[iscard]
      ,[issms]
      ,[issx]
      ,[puserid]
      ,[puserkey]
      ,[pusername]
      ,[puserid1]
      ,[puserkey1]
      ,[puserid2]
      ,[puserkey2]
      ,[puserid3]
      ,[puserkey3]
      ,[puserid4]
      ,[puserkey4]
      ,[puserid5]
      ,[puserkey5]
      ,[purl]
      ,[pbakurl]
      ,[pcardbakurl]
      ,[postBankUrl]
      ,[postCardUrl]
      ,[postSMSUrl]
      ,[desc]
      ,[sort]
      ,[release]
      ,[issys]
      ,[jumpUrl]
      ,[distributionUrl]
      ,[isdistribution]
      ,[queryCardUrl]
      ,[timeout]
      ,[synsRetCode]
      ,[asynsRetCode]
      ,[synsSummitLog]
      ,[asynsRetLog]
      ,[limitAmount]";


        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(SupplierInfo model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@code", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@logourl", SqlDbType.VarChar,50),
					new SqlParameter("@isbank", SqlDbType.Bit,1),
					new SqlParameter("@iscard", SqlDbType.Bit,1),
					new SqlParameter("@issms", SqlDbType.Bit,1),
					new SqlParameter("@issx", SqlDbType.Bit,1),
					new SqlParameter("@puserid", SqlDbType.VarChar,100),
					new SqlParameter("@puserkey", SqlDbType.VarChar,200),
					new SqlParameter("@pusername", SqlDbType.VarChar,50),
					new SqlParameter("@puserid1", SqlDbType.VarChar,100),
					new SqlParameter("@puserkey1", SqlDbType.VarChar,200),
					new SqlParameter("@puserid2", SqlDbType.VarChar,100),
					new SqlParameter("@puserkey2", SqlDbType.VarChar,200),
					new SqlParameter("@puserid3", SqlDbType.VarChar,100),
					new SqlParameter("@puserkey3", SqlDbType.VarChar,200),
					new SqlParameter("@puserid4", SqlDbType.VarChar,100),
					new SqlParameter("@puserkey4", SqlDbType.VarChar,200),
					new SqlParameter("@puserid5", SqlDbType.VarChar,100),
					new SqlParameter("@puserkey5", SqlDbType.VarChar,200),
					new SqlParameter("@purl", SqlDbType.VarChar,50),
					new SqlParameter("@pbakurl", SqlDbType.VarChar,50),
					new SqlParameter("@postBankUrl", SqlDbType.VarChar,200),
					new SqlParameter("@postCardUrl", SqlDbType.VarChar,200),
					new SqlParameter("@postSMSUrl", SqlDbType.VarChar,200),
					new SqlParameter("@desc", SqlDbType.NVarChar,2000),
					new SqlParameter("@sort", SqlDbType.Int,4),
					new SqlParameter("@release", SqlDbType.Bit,1),
					new SqlParameter("@issys", SqlDbType.Bit,1),
                    new SqlParameter("@pcardbakurl", SqlDbType.VarChar,50)
                    ,new SqlParameter("@name1", SqlDbType.VarChar,100)
                    ,new SqlParameter("@jumpUrl", SqlDbType.NVarChar,255)
                    ,new SqlParameter("@isdistribution", SqlDbType.Bit,1)
                    ,new SqlParameter("@distributionUrl", SqlDbType.VarChar,255)
                    ,new SqlParameter("@queryCardUrl", SqlDbType.VarChar,255)
                    ,new SqlParameter("@timeout", SqlDbType.Int,4)
                    ,new SqlParameter("@synsRetCode", SqlDbType.VarChar,2000)
                    ,new SqlParameter("@asynsRetCode", SqlDbType.VarChar,2000)
                    ,new SqlParameter("@synsSummitLog", SqlDbType.Bit,1)
                    ,new SqlParameter("@asynsRetLog", SqlDbType.Bit,1)
                     ,new SqlParameter("@multiacct", SqlDbType.Bit,1)
                     ,new SqlParameter("@usejump", SqlDbType.Bit,1) 
                     ,new SqlParameter("@limitAmount", SqlDbType.Int,4)
                                            };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.code;
            parameters[2].Value = model.name;
            parameters[3].Value = model.logourl;
            parameters[4].Value = model.isbank;
            parameters[5].Value = model.iscard;
            parameters[6].Value = model.issms;
            parameters[7].Value = model.issx;
            parameters[8].Value = model.puserid;
            parameters[9].Value = model.puserkey;
            parameters[10].Value = model.pusername;
            parameters[11].Value = model.puserid1;
            parameters[12].Value = model.puserkey1;
            parameters[13].Value = model.puserid2;
            parameters[14].Value = model.puserkey2;
            parameters[15].Value = model.puserid3;
            parameters[16].Value = model.puserkey3;
            parameters[17].Value = model.puserid4;
            parameters[18].Value = model.puserkey4;
            parameters[19].Value = model.puserid5;
            parameters[20].Value = model.puserkey5;
            parameters[21].Value = model.purl;
            parameters[22].Value = model.pbakurl;
            parameters[23].Value = model.postBankUrl;
            parameters[24].Value = model.postCardUrl;
            parameters[25].Value = model.postSMSUrl;
            parameters[26].Value = model.desc;
            parameters[27].Value = model.sort;
            parameters[28].Value = model.release;
            parameters[29].Value = model.issys;
            parameters[30].Value = model.pcardbakurl;
            parameters[31].Value = model.name1;
            parameters[32].Value = model.jumpUrl;

            parameters[33].Value = model.isdistribution;
            parameters[34].Value = model.distributionUrl;
            parameters[35].Value = model.queryCardUrl;

            parameters[36].Value = model.timeout;

            parameters[37].Value = model.SynsRetCode;
            parameters[38].Value = model.AsynsRetCode;

            parameters[39].Value = model.SynsSummitLog;
            parameters[40].Value = model.AsynsRetLog;
            parameters[41].Value = model.multiacct;
            parameters[42].Value = model.useJump;
            parameters[43].Value = model.limitAmount;
            rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_supplier_add", parameters);

            return (int)parameters[0].Value;
        }
        #endregion

        #region Update
        /// <summary>
        ///  更新一条数据
        /// </summary>
        public bool Update(SupplierInfo model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@code", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@logourl", SqlDbType.VarChar,50),
					new SqlParameter("@isbank", SqlDbType.Bit,1),
					new SqlParameter("@iscard", SqlDbType.Bit,1),
					new SqlParameter("@issms", SqlDbType.Bit,1),
					new SqlParameter("@issx", SqlDbType.Bit,1),
					new SqlParameter("@puserid", SqlDbType.VarChar,100),
					new SqlParameter("@puserkey", SqlDbType.VarChar,200),
					new SqlParameter("@pusername", SqlDbType.VarChar,50),
					new SqlParameter("@puserid1", SqlDbType.VarChar,100),
					new SqlParameter("@puserkey1", SqlDbType.VarChar,200),
					new SqlParameter("@puserid2", SqlDbType.VarChar,100),
					new SqlParameter("@puserkey2", SqlDbType.VarChar,200),
					new SqlParameter("@puserid3", SqlDbType.VarChar,100),
					new SqlParameter("@puserkey3", SqlDbType.VarChar,200),
					new SqlParameter("@puserid4", SqlDbType.VarChar,100),
					new SqlParameter("@puserkey4", SqlDbType.VarChar,200),
					new SqlParameter("@puserid5", SqlDbType.VarChar,100),
					new SqlParameter("@puserkey5", SqlDbType.VarChar,200),
					new SqlParameter("@purl", SqlDbType.VarChar,50),
					new SqlParameter("@pbakurl", SqlDbType.VarChar,50),
					new SqlParameter("@postBankUrl", SqlDbType.VarChar,200),
					new SqlParameter("@postCardUrl", SqlDbType.VarChar,200),
					new SqlParameter("@postSMSUrl", SqlDbType.VarChar,200),
					new SqlParameter("@desc", SqlDbType.NVarChar,2000),
					new SqlParameter("@sort", SqlDbType.Int,4),
					new SqlParameter("@release", SqlDbType.Bit,1),
					new SqlParameter("@issys", SqlDbType.Bit,1),
                    new SqlParameter("@pcardbakurl", SqlDbType.VarChar,50),
                    new SqlParameter("@name1", SqlDbType.VarChar,100),
                    new SqlParameter("@jumpUrl", SqlDbType.NVarChar,255)
                    ,new SqlParameter("@isdistribution", SqlDbType.Bit,1)
                    ,new SqlParameter("@distributionUrl", SqlDbType.VarChar,255)
                    ,new SqlParameter("@queryCardUrl", SqlDbType.VarChar,255)
                    ,new SqlParameter("@timeout", SqlDbType.Int,4)
                    ,new SqlParameter("@synsRetCode", SqlDbType.VarChar,2000)
                    ,new SqlParameter("@asynsRetCode", SqlDbType.VarChar,2000)
                    ,new SqlParameter("@synsSummitLog", SqlDbType.Bit,1)
                    ,new SqlParameter("@asynsRetLog", SqlDbType.Bit,1)
                    ,new SqlParameter("@multiacct", SqlDbType.Bit,1)
                    ,new SqlParameter("@usejump", SqlDbType.Bit,1) 
                    ,new SqlParameter("@limitAmount", SqlDbType.Int,4)
                                            };
            parameters[0].Value = model.id;
            parameters[1].Value = model.code;
            parameters[2].Value = model.name;
            parameters[3].Value = model.logourl;
            parameters[4].Value = model.isbank;
            parameters[5].Value = model.iscard;
            parameters[6].Value = model.issms;
            parameters[7].Value = model.issx;
            parameters[8].Value = model.puserid;
            parameters[9].Value = model.puserkey;
            parameters[10].Value = model.pusername;
            parameters[11].Value = model.puserid1;
            parameters[12].Value = model.puserkey1;
            parameters[13].Value = model.puserid2;
            parameters[14].Value = model.puserkey2;
            parameters[15].Value = model.puserid3;
            parameters[16].Value = model.puserkey3;
            parameters[17].Value = model.puserid4;
            parameters[18].Value = model.puserkey4;
            parameters[19].Value = model.puserid5;
            parameters[20].Value = model.puserkey5;
            parameters[21].Value = model.purl;
            parameters[22].Value = model.pbakurl;
            parameters[23].Value = model.postBankUrl;
            parameters[24].Value = model.postCardUrl;
            parameters[25].Value = model.postSMSUrl;
            parameters[26].Value = model.desc;
            parameters[27].Value = model.sort;
            parameters[28].Value = model.release;
            parameters[29].Value = model.issys;
            parameters[30].Value = model.pcardbakurl;
            parameters[31].Value = model.name1;
            parameters[32].Value = model.jumpUrl;

            parameters[33].Value = model.isdistribution;
            parameters[34].Value = model.distributionUrl;
            parameters[35].Value = model.queryCardUrl;
            parameters[36].Value = model.timeout;
            parameters[37].Value = model.SynsRetCode;
            parameters[38].Value = model.AsynsRetCode;

            parameters[39].Value = model.SynsSummitLog;
            parameters[40].Value = model.AsynsRetLog;
            parameters[41].Value = model.multiacct;
            parameters[42].Value = model.useJump;
            parameters[43].Value = model.limitAmount;
            rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_supplier_Update", parameters);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region GetModel
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SupplierInfo GetModel(int id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_supplier_GetModel", parameters);
            return GetModelFromDs(ds);
        }

        public SupplierInfo GetModelByCode(int code)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@code", SqlDbType.Int, 4)
            };
            parameters[0].Value = code;
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_supplier_GetModelBycode", parameters);
            return GetModelFromDs(ds);
        }


        public SupplierInfo GetModelFromDs(DataSet ds)
        {
            var model = new SupplierInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["code"].ToString() != "")
                {
                    model.code = int.Parse(ds.Tables[0].Rows[0]["code"].ToString());
                }
                try
                {
                    if (ds.Tables[0].Rows[0]["limitAmount"].ToString() != "")
                    {
                        model.limitAmount = int.Parse(ds.Tables[0].Rows[0]["limitAmount"].ToString());
                    }
                }
                catch (Exception ex)
                {

                }
                model.name = ds.Tables[0].Rows[0]["name"].ToString();
                model.name1 = ds.Tables[0].Rows[0]["name1"].ToString();
                model.logourl = ds.Tables[0].Rows[0]["logourl"].ToString();
                model.pcardbakurl = ds.Tables[0].Rows[0]["pcardbakurl"].ToString();
                if (ds.Tables[0].Rows[0]["isbank"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isbank"].ToString() == "1") ||
                        (ds.Tables[0].Rows[0]["isbank"].ToString().ToLower() == "true"))
                    {
                        model.isbank = true;
                    }
                    else
                    {
                        model.isbank = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["iscard"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["iscard"].ToString() == "1") ||
                        (ds.Tables[0].Rows[0]["iscard"].ToString().ToLower() == "true"))
                    {
                        model.iscard = true;
                    }
                    else
                    {
                        model.iscard = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["issms"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["issms"].ToString() == "1") ||
                        (ds.Tables[0].Rows[0]["issms"].ToString().ToLower() == "true"))
                    {
                        model.issms = true;
                    }
                    else
                    {
                        model.issms = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["issx"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["issx"].ToString() == "1") ||
                        (ds.Tables[0].Rows[0]["issx"].ToString().ToLower() == "true"))
                    {
                        model.issx = true;
                    }
                    else
                    {
                        model.issx = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["isdistribution"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isdistribution"].ToString() == "1") ||
                        (ds.Tables[0].Rows[0]["isdistribution"].ToString().ToLower() == "true"))
                    {
                        model.isdistribution = true;
                    }
                    else
                    {
                        model.isdistribution = false;
                    }
                }
                model.puserid = ds.Tables[0].Rows[0]["puserid"].ToString();
                model.puserkey = ds.Tables[0].Rows[0]["puserkey"].ToString();
                model.pusername = ds.Tables[0].Rows[0]["pusername"].ToString();
                model.puserid1 = ds.Tables[0].Rows[0]["puserid1"].ToString();
                model.puserkey1 = ds.Tables[0].Rows[0]["puserkey1"].ToString();
                model.puserid2 = ds.Tables[0].Rows[0]["puserid2"].ToString();
                model.puserkey2 = ds.Tables[0].Rows[0]["puserkey2"].ToString();
                model.puserid3 = ds.Tables[0].Rows[0]["puserid3"].ToString();
                model.puserkey3 = ds.Tables[0].Rows[0]["puserkey3"].ToString();
                model.puserid4 = ds.Tables[0].Rows[0]["puserid4"].ToString();
                model.puserkey4 = ds.Tables[0].Rows[0]["puserkey4"].ToString();
                model.puserid5 = ds.Tables[0].Rows[0]["puserid5"].ToString();
                model.puserkey5 = ds.Tables[0].Rows[0]["puserkey5"].ToString();
                model.purl = ds.Tables[0].Rows[0]["purl"].ToString();
                model.pbakurl = ds.Tables[0].Rows[0]["pbakurl"].ToString();
                model.postBankUrl = ds.Tables[0].Rows[0]["postBankUrl"].ToString();
                model.jumpUrl = ds.Tables[0].Rows[0]["jumpUrl"].ToString();
                model.postCardUrl = ds.Tables[0].Rows[0]["postCardUrl"].ToString();
                model.postSMSUrl = ds.Tables[0].Rows[0]["postSMSUrl"].ToString();
                model.distributionUrl = ds.Tables[0].Rows[0]["distributionUrl"].ToString();
                model.queryCardUrl = ds.Tables[0].Rows[0]["queryCardUrl"].ToString();

                model.desc = ds.Tables[0].Rows[0]["desc"].ToString();

                model.SynsRetCode = ds.Tables[0].Rows[0]["synsRetCode"].ToString();
                model.AsynsRetCode = ds.Tables[0].Rows[0]["asynsRetCode"].ToString();

                if (ds.Tables[0].Rows[0]["sort"].ToString() != "")
                {
                    model.sort = int.Parse(ds.Tables[0].Rows[0]["sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["release"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["release"].ToString() == "1") ||
                        (ds.Tables[0].Rows[0]["release"].ToString().ToLower() == "true"))
                    {
                        model.release = true;
                    }
                    else
                    {
                        model.release = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["issys"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["issys"].ToString() == "1") ||
                        (ds.Tables[0].Rows[0]["issys"].ToString().ToLower() == "true"))
                    {
                        model.issys = true;
                    }
                    else
                    {
                        model.issys = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["timeout"].ToString() != "")
                {
                    model.timeout = int.Parse(ds.Tables[0].Rows[0]["timeout"].ToString());
                }

                if (ds.Tables[0].Rows[0]["synsSummitLog"].ToString() != "")
                {
                    model.SynsSummitLog = bool.Parse(ds.Tables[0].Rows[0]["synsSummitLog"].ToString());
                }
                if (ds.Tables[0].Rows[0]["asynsRetLog"].ToString() != "")
                {
                    model.AsynsRetLog = bool.Parse(ds.Tables[0].Rows[0]["asynsRetLog"].ToString());
                }
                if (ds.Tables[0].Rows[0]["multiacct"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["multiacct"].ToString() == "1") ||
                        (ds.Tables[0].Rows[0]["multiacct"].ToString().ToLower() == "true"))
                    {
                        model.multiacct = true;
                    }
                    else
                    {
                        model.multiacct = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["usejump"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["usejump"].ToString() == "1") ||
                        (ds.Tables[0].Rows[0]["usejump"].ToString().ToLower() == "true"))
                    {
                        model.useJump = true;
                    }
                    else
                    {
                        model.useJump = false;
                    }
                }
                return model;
            }
            return null;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append("select id,code,name,name1,logourl,isbank,iscard,issms,issx,puserid,puserkey,pusername,puserid1,puserkey1,puserid2,puserkey2,puserid3,puserkey3,puserid4,puserkey4,puserid5,puserkey5,purl,pbakurl,postBankUrl,postCardUrl,postSMSUrl,[desc],sort,release,issys,pcardbakurl,limitAmount ");
            strSql.Append(" FROM supplier ");
            strSql.Append(" Order by sort ");

            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataSet GetList(string where)
        {
            var strSql = new StringBuilder();
            strSql.Append("select id,code,name,name1,logourl,isbank,iscard,issms,issx,puserid,puserkey,pusername,puserid1,puserkey1,puserid2,puserkey2,puserid3,puserkey3,puserid4,puserkey4,puserid5,puserkey5,purl,pbakurl,postBankUrl,postCardUrl,postSMSUrl,[desc],sort,release,issys,pcardbakurl,limitAmount ");
            strSql.Append(" FROM supplier ");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.AppendFormat(" where {0}", where);
            }
            strSql.Append(" Order by sort ");


            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        ///     根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            var ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }

                var paramList = new List<SqlParameter>();
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELDS, tables, where, orderby, key, pageSize, page,
                                 false);

                ds = DataBase.ExecuteDataset(CommandType.Text, sql, paramList.ToArray());
                return ds;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return ds;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            var builder = new StringBuilder(" 1 = 1");

            if ((param != null) && (param.Count > 0))
            {
                for (int i = 0; i < param.Count; i++)
                {
                    SqlParameter parameter;
                    SearchParam iparam = param[i];
                    switch (iparam.ParamKey.Trim().ToLower())
                    {
                        case "code":
                            builder.Append(" AND [code] = @code");
                            parameter = new SqlParameter("@code", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "used":
                            builder.Append(" AND [issys] = @issys");
                            parameter = new SqlParameter("@issys", SqlDbType.Bit);
                            parameter.Value = (bool)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "name":
                            builder.Append(" AND [name] like @name");
                            parameter = new SqlParameter("@name", SqlDbType.VarChar, 20);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 20) + "%";
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }
    }
}