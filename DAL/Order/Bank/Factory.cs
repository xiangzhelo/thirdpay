using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model.Common;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviLib.Data;

namespace viviapi.DAL.Order.Bank
{
    public class Factory
    {
        internal const string SqlTable = "v_orderbank";
        internal const string FIELDS = @"[id]
      ,[orderid]
      ,[ordertype]
      ,[userid]
      ,[typeId]
      ,[paymodeId]
      ,[userorder]
      ,[refervalue]
      ,[version]
      ,[realvalue]
      ,[notifyurl]
      ,[againNotifyUrl]
      ,[notifycount]
      ,[notifystat]
      ,[notifycontext]
      ,[notifytime]
      ,[returnurl]
      ,[attach]
      ,[payerip]
      ,[clientip]
      ,[referUrl]
      ,[addtime]
      ,[supplierID]
      ,[supplierOrder]
      ,[status]
      ,[completetime]
      ,[payRate]
      ,[supplierRate]
      ,[promRate]
      ,[payAmt]
      ,[promAmt]
      ,[supplierAmt]
      ,[profits]
      ,[server]
      ,[modetypename]
      ,[modeName]
      ,[manageId]
      ,[commission]
      ,[cus_subject]
      ,[cus_price]
      ,[cus_quantity]
      ,[cus_description]
      ,[cus_field1]
      ,[cus_field2]
      ,[cus_field3]
      ,[cus_field4]
      ,[cus_field5]
      ,[processingtime]
      ,[agentid]
      ,[reconciled]
      ,[reconcileTime]
      ,[reconciledAmt],[deduct],[processingtime]";

        #region GetModel

        /// <summary>
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderBankInfo GetModelByOrderId(string orderId)
        {
            SqlParameter[] parameters = { new SqlParameter("@orderid", SqlDbType.VarChar, 30) };

            parameters[0].Value = orderId;
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_orderbank_getbankdirectinfo",
                parameters);

            return GetModelFromDs(ds);
        }

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public OrderBankInfo GetModel(long id, int userid)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.BigInt),
                new SqlParameter("@userid", SqlDbType.Int)
            };
            parameters[0].Value = id;
            parameters[1].Value = userid;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_orderbank_GetModel", parameters);
            return GetModelFromDs(ds);
        }


        public OrderBankInfo GetModel(long id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.BigInt),
                new SqlParameter("@userid", SqlDbType.Int)
            };
            parameters[0].Value = id;
            parameters[1].Value = DBNull.Value;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_orderbank_GetModel", parameters);
            return GetModelFromDs(ds);
        }


        internal OrderBankInfo GetModelFromDs(DataSet ds)
        {
            var model = new OrderBankInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = long.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.orderid = ds.Tables[0].Rows[0]["orderid"].ToString();
                if (ds.Tables[0].Rows[0]["ordertype"].ToString() != "")
                {
                    model.ordertype = int.Parse(ds.Tables[0].Rows[0]["ordertype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["typeId"].ToString() != "")
                {
                    model.typeId = int.Parse(ds.Tables[0].Rows[0]["typeId"].ToString());
                }
                model.paymodeId = ds.Tables[0].Rows[0]["paymodeId"].ToString();
                model.userorder = ds.Tables[0].Rows[0]["userorder"].ToString();
                if (ds.Tables[0].Rows[0]["refervalue"].ToString() != "")
                {
                    model.refervalue = decimal.Parse(ds.Tables[0].Rows[0]["refervalue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["realvalue"].ToString() != "")
                {
                    model.realvalue = decimal.Parse(ds.Tables[0].Rows[0]["realvalue"].ToString());
                }
                model.notifyurl = ds.Tables[0].Rows[0]["notifyurl"].ToString();
                model.againNotifyUrl = ds.Tables[0].Rows[0]["againNotifyUrl"].ToString();
                if (ds.Tables[0].Rows[0]["notifycount"].ToString() != "")
                {
                    model.notifycount = int.Parse(ds.Tables[0].Rows[0]["notifycount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["notifystat"].ToString() != "")
                {
                    model.notifystat = int.Parse(ds.Tables[0].Rows[0]["notifystat"].ToString());
                }
                model.notifycontext = ds.Tables[0].Rows[0]["notifycontext"].ToString();
                model.returnurl = ds.Tables[0].Rows[0]["returnurl"].ToString();
                model.attach = ds.Tables[0].Rows[0]["attach"].ToString();
                model.payerip = ds.Tables[0].Rows[0]["payerip"].ToString();
                model.clientip = ds.Tables[0].Rows[0]["clientip"].ToString();
                model.referUrl = ds.Tables[0].Rows[0]["referUrl"].ToString();
                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["supplierID"].ToString() != "")
                {
                    model.supplierId = int.Parse(ds.Tables[0].Rows[0]["supplierID"].ToString());
                }
                model.supplierOrder = ds.Tables[0].Rows[0]["supplierOrder"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["completetime"].ToString() != "")
                {
                    model.completetime = DateTime.Parse(ds.Tables[0].Rows[0]["completetime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payRate"].ToString() != "")
                {
                    model.payRate = decimal.Parse(ds.Tables[0].Rows[0]["payRate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["supplierRate"].ToString() != "")
                {
                    model.supplierRate = decimal.Parse(ds.Tables[0].Rows[0]["supplierRate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["promRate"].ToString() != "")
                {
                    model.promRate = decimal.Parse(ds.Tables[0].Rows[0]["promRate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payAmt"].ToString() != "")
                {
                    model.payAmt = decimal.Parse(ds.Tables[0].Rows[0]["payAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["promAmt"].ToString() != "")
                {
                    model.promAmt = decimal.Parse(ds.Tables[0].Rows[0]["promAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["supplierAmt"].ToString() != "")
                {
                    model.supplierAmt = decimal.Parse(ds.Tables[0].Rows[0]["supplierAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["profits"].ToString() != "")
                {
                    model.profits = decimal.Parse(ds.Tables[0].Rows[0]["profits"].ToString());
                }
                if (ds.Tables[0].Rows[0]["server"].ToString() != "")
                {
                    model.server = int.Parse(ds.Tables[0].Rows[0]["server"].ToString());
                }
                if (ds.Tables[0].Rows[0]["manageId"].ToString() != "")
                {
                    model.manageId = int.Parse(ds.Tables[0].Rows[0]["manageId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["commission"].ToString() != "")
                {
                    model.commission = decimal.Parse(ds.Tables[0].Rows[0]["commission"].ToString());
                }
                model.version = ds.Tables[0].Rows[0]["version"].ToString();
                if (ds.Tables[0].Rows[0]["cus_subject"] != null && ds.Tables[0].Rows[0]["cus_subject"].ToString() != "")
                {
                    model.cus_subject = ds.Tables[0].Rows[0]["cus_subject"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cus_price"] != null && ds.Tables[0].Rows[0]["cus_price"].ToString() != "")
                {
                    model.cus_price = ds.Tables[0].Rows[0]["cus_price"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cus_quantity"] != null &&
                    ds.Tables[0].Rows[0]["cus_quantity"].ToString() != "")
                {
                    model.cus_quantity = ds.Tables[0].Rows[0]["cus_quantity"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cus_description"] != null &&
                    ds.Tables[0].Rows[0]["cus_description"].ToString() != "")
                {
                    model.cus_description = ds.Tables[0].Rows[0]["cus_description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cus_field1"] != null && ds.Tables[0].Rows[0]["cus_field1"].ToString() != "")
                {
                    model.cus_field1 = ds.Tables[0].Rows[0]["cus_field1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cus_field2"] != null && ds.Tables[0].Rows[0]["cus_field2"].ToString() != "")
                {
                    model.cus_field2 = ds.Tables[0].Rows[0]["cus_field2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cus_field3"] != null && ds.Tables[0].Rows[0]["cus_field3"].ToString() != "")
                {
                    model.cus_field3 = ds.Tables[0].Rows[0]["cus_field3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cus_field4"] != null && ds.Tables[0].Rows[0]["cus_field4"].ToString() != "")
                {
                    model.cus_field4 = ds.Tables[0].Rows[0]["cus_field4"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cus_field5"] != null && ds.Tables[0].Rows[0]["cus_field5"].ToString() != "")
                {
                    model.cus_field5 = ds.Tables[0].Rows[0]["cus_field5"].ToString();
                }
                if (ds.Tables[0].Rows[0]["agentid"] != null && ds.Tables[0].Rows[0]["agentid"].ToString() != "")
                {
                    model.agentId = Convert.ToInt32(ds.Tables[0].Rows[0]["agentid"].ToString());
                }

                return model;
            }
            return null;
        }

        #endregion

        #region 查询有关

        /// <summary>
        ///     根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <param name="isstat"></param>
        /// <returns>分页数据。</returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, bool isstat)
        {
            string tables = SqlTable;
            string key = "[id]";
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = "id desc";
            }

            var paramList = new List<SqlParameter>();
            string searchWhere = BuilderWhere(searchParams, paramList);

            string sql = SqlHelper.GetCountSQL(tables, searchWhere, string.Empty) + "\r\n" +
                         SqlHelper.GetPageSelectSQL(FIELDS, tables, searchWhere, orderby, key, pageSize, page, false);
            if (isstat)
            {
                sql += "\r\n" +
                       @"select sum(1) ordtotal
,sum(case when [status]=2 then 1 else 0 end) succordtotal
,sum(refervalue) refervalue
,sum(case when [status]=2 then realvalue else 0 end) realvalue,sum(isnull(promAmt,0)) promAmt
,sum(case when [status]=2 and [deduct]=0 then payAmt else 0 end) payAmt
,sum(case when [status]=2 and [deduct]=0 then supplierAmt-payAmt-promAmt
 when [status]=2 and [deduct]=1 then supplierAmt-promAmt
 else 0 end) profits
,sum(promAmt) promAmt,sum(commission) commission from v_orderbank where " + searchWhere;
            }

            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, sql, paramList.ToArray());
            return ds;
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
                    if (iparam.CmpOperator == "=")
                    {
                        switch (iparam.ParamKey.Trim().ToLower())
                        {
                            case "userid":
                                builder.Append(" AND [userid] = @userid");
                                parameter = new SqlParameter("@userid", SqlDbType.Int);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "agentid": //代理
                                builder.Append(" AND [agentid] = @agentid");
                                parameter = new SqlParameter("@agentid", SqlDbType.Int);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "manageid":
                                builder.Append(" AND [manageId] = @manageId");
                                parameter = new SqlParameter("@manageId", SqlDbType.Int);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "typeid":
                                builder.Append(" AND [typeId] = @typeId");
                                parameter = new SqlParameter("@typeId", SqlDbType.Int);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "supplierid":
                                builder.Append(" AND [supplierID] = @supplierID");
                                parameter = new SqlParameter("@supplierID", SqlDbType.Int);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "deduct":
                                builder.Append(" AND [deduct] = @deduct");
                                parameter = new SqlParameter("@deduct", SqlDbType.Int);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "userorder":
                                builder.Append(" AND [userorder] like @userorder");
                                parameter = new SqlParameter("@userorder", SqlDbType.VarChar, 30);
                                parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 30) + "%";
                                paramList.Add(parameter);
                                break;
                            case "orderid":
                                builder.Append(" AND [orderid] like @orderid");
                                parameter = new SqlParameter("@orderid", SqlDbType.VarChar, 30);
                                parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 30) + "%";
                                paramList.Add(parameter);
                                break;
                            case "supplierorder":
                                builder.Append(" AND [supplierOrder] like @supplierOrder");
                                parameter = new SqlParameter("@supplierOrder", SqlDbType.VarChar, 30);
                                parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 30) + "%";
                                paramList.Add(parameter);
                                break;
                            case "orderid_like":
                                builder.Append(" AND [orderid] like @orderid");
                                parameter = new SqlParameter("@orderid", SqlDbType.VarChar, 30);
                                parameter.Value = SqlHelper.CleanString((string)iparam.ParamValue, 30) + "%";
                                paramList.Add(parameter);
                                break;
                            case "status":
                                builder.Append(" AND [status] = @status");
                                parameter = new SqlParameter("@status", SqlDbType.TinyInt);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "statusallfail":
                                builder.Append(" AND ([status] = 4 or  [status] = 8)");
                                //parameter = new SqlParameter("@status", SqlDbType.TinyInt);
                                //parameter.Value = (int)iparam.ParamValue;
                                //paramList.Add(parameter);
                                break;
                            case "notifystat":
                                builder.Append(" AND [notifystat] = @notifystat");
                                parameter = new SqlParameter("@notifystat", SqlDbType.TinyInt);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "promid":
                                builder.Append(
                                    " AND exists(select 0 from PromotionUser where PromotionUser.PID = @promid and PromotionUser.RegId=userid)");
                                parameter = new SqlParameter("@promid", SqlDbType.Int);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "stime":
                                builder.Append(" AND [processingtime] >= @stime");
                                parameter = new SqlParameter("@stime", SqlDbType.DateTime);
                                parameter.Value = iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "etime":
                                builder.Append(" AND [processingtime] <= @etime");
                                parameter = new SqlParameter("@etime", SqlDbType.DateTime);
                                parameter.Value = iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                        }
                    }
                    else
                    {
                        switch (iparam.ParamKey.Trim().ToLower())
                        {
                            case "status":
                                builder.AppendFormat(" AND [status] {0} @status1", iparam.CmpOperator);
                                parameter = new SqlParameter("@status1", SqlDbType.TinyInt);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                        }
                    }
                }
            }
            return builder.ToString();
        }

        #endregion

        #region Deduct

        /// <summary>
        /// </summary>
        public bool Deduct(string orderid)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                new SqlParameter("@result", SqlDbType.Bit)
            };
            parameters[0].Value = orderid;
            parameters[1].Direction = ParameterDirection.Output;
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_orderbank_deduct", parameters);

            return (bool)parameters[1].Value;
        }

        #endregion

        #region ReDeduct

        /// <summary>
        /// </summary>
        public bool ReDeduct(string orderid)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                new SqlParameter("@result", SqlDbType.Bit)
            };
            parameters[0].Value = orderid;
            parameters[1].Direction = ParameterDirection.Output;
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_orderbank_rededuct", parameters);

            return (bool)parameters[1].Value;
        }

        #endregion

        #region Reconcilie
        /// <summary>
        /// 订单对账
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="reconciledResult">
        /// 0 未对账
        /// 1 对账成功
        /// 2 金额实际金额不符合
        /// 4 对账失败 
        /// </param>
        /// <param name="reconciledAmt"></param>
        /// <returns></returns>
        public bool Reconcilie(string orderid, byte reconciledResult, decimal reconciledAmt)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                new SqlParameter("@reconciled", SqlDbType.TinyInt),
                new SqlParameter("@reconcileTime", SqlDbType.TinyInt),
                new SqlParameter("@reconciledAmt", SqlDbType.Decimal,9)
            };
            parameters[0].Value = orderid;
            parameters[1].Value = reconciledResult;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = reconciledAmt;

            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_orderbank_reconcilie", parameters);

            return (bool)parameters[1].Value;
        }
        #endregion

        #region CheckAPIParms

        /// <summary>
        /// </summary>
        /// <param name="intputUserid"></param>
        /// <param name="ischeckuserorder"></param>
        /// <param name="intputUserorder"></param>
        /// <returns></returns>
        public FunExecResult CheckAPIParms(int intputUserid
            , int typeid
            , bool ischeckuserorder
            , string intputUserorder)
        {

            var chkResult = new FunExecResult();

            int result = 0;

            SqlParameter[] parameters =
            {
                  new SqlParameter("@intput_userid", SqlDbType.Int, 4)
                , new SqlParameter("@typeid", SqlDbType.Int, 4)
                , new SqlParameter("@ischeckuserorder", SqlDbType.Bit)
                , new SqlParameter("@intput_userorder", SqlDbType.VarChar, 30)
                , new SqlParameter("@result", SqlDbType.TinyInt)
            };
            parameters[0].Value = intputUserid;
            parameters[1].Value = typeid;
            parameters[2].Value = ischeckuserorder;
            parameters[3].Value = intputUserorder;
            parameters[4].Direction = ParameterDirection.Output;

            DataSet data = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_orderbank_checkdata", parameters);

            
            result = Convert.ToInt32(parameters[4].Value);
            if (result == 0 && data != null && data.Tables.Count > 0)
            {
                var userInfo = new UserInfo();

                DataRow row = data.Tables[0].Rows[0];
                userInfo.ID = intputUserid;
                userInfo.APIKey = row["apikey"].ToString();
                if (row["isdebug"] != DBNull.Value)
                    userInfo.isdebug = Convert.ToInt32(row["isdebug"]);
                if (row["manageId"] != DBNull.Value)
                    userInfo.manageId = Convert.ToInt32(row["manageId"]);
                if (row["RiskWarning"] != DBNull.Value)
                    userInfo.RiskWarning = Convert.ToByte(row["RiskWarning"]);

                chkResult.Obj = userInfo;
            }
            chkResult.ErrCode = result;

            return chkResult;
        }

        #endregion

        #region search_check
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o_userid"></param>
        /// <param name="userorderid"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public int search_check(int o_userid, string userorderid, out DataRow row)
        {
            row = null;
            int check_result = 99;


            SqlParameter[] parameters = {    
                                            new SqlParameter("@o_userid", SqlDbType.Int, 4)
                                           ,new SqlParameter("@userorderid_str", SqlDbType.VarChar,30) 
                                           ,new SqlParameter("@result", SqlDbType.TinyInt) 
                                        };
            parameters[0].Value = o_userid;
            parameters[1].Value = userorderid;
            parameters[2].Direction = ParameterDirection.Output;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_orderbank_search", parameters);
            check_result = Convert.ToInt32(parameters[2].Value);

            if (check_result == 0)
                row = ds.Tables[0].Rows[0];



            return check_result;
        }
        #endregion
    }
}