using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using DBAccess;
using viviapi.Model.Order;
using viviapi.Model.Order.Card;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Order.Card
{
    public class Factory
    {
        internal const string SQL_TABLE = "v_ordercard";
        internal const string FIELDS = @"[id]
      ,[orderid]
      ,[ordertype]
      ,[userid]
      ,[typeId]
      ,[paymodeId]
      ,[userorder]
      ,[refervalue]
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
      ,[cardNo]
      ,[cardPwd]
      ,[desc]
      ,[manageId]
      ,[msg]
      ,[commission]
      ,[cardnum]
      ,[resultcode]
      ,[ismulticard]
      ,[version],cus_subject,cus_price,cus_quantity,cus_description,cus_field1,cus_field2,cus_field3,cus_field4,cus_field5,errtype,agentid,faceValue,deduct,[processingtime]";

        #region GetModel

        /// <summary>
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal OrderCardInfo GetModelFromDs(DataSet ds)
        {
            var model = new OrderCardInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = long.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.orderid = ds.Tables[0].Rows[0]["orderid"].ToString();
                model.cardNo = ds.Tables[0].Rows[0]["cardNo"].ToString();
                model.cardPwd = ds.Tables[0].Rows[0]["cardPwd"].ToString();
                model.Desc = ds.Tables[0].Rows[0]["desc"].ToString();
                if (ds.Tables[0].Rows[0]["ordertype"].ToString() != "")
                {
                    model.ordertype = int.Parse(ds.Tables[0].Rows[0]["ordertype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["manageId"].ToString() != "")
                {
                    model.manageId = int.Parse(ds.Tables[0].Rows[0]["manageId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["cardtype"].ToString() != "")
                {
                    model.cardType = int.Parse(ds.Tables[0].Rows[0]["cardtype"].ToString());
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
                model.faceValue = 0M;
                if (ds.Tables[0].Rows[0]["faceValue"].ToString() != "")
                {
                    model.faceValue = decimal.Parse(ds.Tables[0].Rows[0]["faceValue"].ToString());
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
                model.msg = ds.Tables[0].Rows[0]["msg"].ToString();
                model.userViewMsg = ds.Tables[0].Rows[0]["userViewMsg"].ToString();
                model.opstate = ds.Tables[0].Rows[0]["opstate"].ToString();

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
                if (ds.Tables[0].Rows[0]["processingtime"].ToString() != "")
                {
                    model.processingtime = DateTime.Parse(ds.Tables[0].Rows[0]["processingtime"].ToString());
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
                if (ds.Tables[0].Rows[0]["cardnum"].ToString() != "")
                {
                    model.cardnum = int.Parse(ds.Tables[0].Rows[0]["cardnum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ismulticard"].ToString() != "")
                {
                    model.ismulticard = int.Parse(ds.Tables[0].Rows[0]["ismulticard"].ToString());
                }
                model.resultcode = ds.Tables[0].Rows[0]["resultcode"].ToString();
                model.ovalue = ds.Tables[0].Rows[0]["ovalue"].ToString();
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
                model.errtype = ds.Tables[0].Rows[0]["errtype"].ToString();
                if (ds.Tables[0].Rows[0]["agentid"] != null && ds.Tables[0].Rows[0]["agentid"].ToString() != "")
                {
                    model.agentId = int.Parse(ds.Tables[0].Rows[0]["agentid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["withhold"] != null && ds.Tables[0].Rows[0]["withhold"].ToString() != "")
                {
                    model.withhold_type = byte.Parse(ds.Tables[0].Rows[0]["withhold"].ToString());
                }
                if (ds.Tables[0].Rows[0]["makeup"] != null && ds.Tables[0].Rows[0]["makeup"].ToString() != "")
                {
                    model.makeup = byte.Parse(ds.Tables[0].Rows[0]["makeup"].ToString());
                }

                model.method = 1;
                if (model.supplierId == 0)
                    model.method = 2;

                return model;
            }
            return null;
        }

        /// <summary>
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderCardInfo GetModelByOrderId(string orderId)
        {
            SqlParameter[] parameters = { new SqlParameter("@orderid", SqlDbType.VarChar, 30) };

            parameters[0].Value = orderId;
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_get", parameters);

            return GetModelFromDs(ds);
        }

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public OrderCardInfo GetModel(long id, int userid)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.BigInt),
                new SqlParameter("@userid", SqlDbType.Int)
            };
            parameters[0].Value = id;
            parameters[1].Value = userid;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_GetModel", parameters);
            return GetModelFromDs(ds);
        }


        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public OrderCardInfo GetModel(long id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.BigInt),
                new SqlParameter("@userid", SqlDbType.Int)
            };
            parameters[0].Value = id;
            parameters[1].Value = DBNull.Value;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_GetModel", parameters);
            return GetModelFromDs(ds);
        }

        #endregion

        #region ResetState

        /// <summary>
        ///     重置订单状态
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public bool ResetState(string orderid)
        {
            const string sqlText = @"update ordercardamt set [status] = 1 where  [status] = 4 and orderid=@orderid ";
            SqlParameter[] parameters = { new SqlParameter("@orderid", SqlDbType.VarChar, 30) };
            parameters[0].Value = orderid;
            return DataBase.ExecuteNonQuery(CommandType.Text, sqlText, parameters) > 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="suppid"></param>
        /// <param name="refervalue"></param>
        /// <returns></returns>
        public bool ResetState(string orderid, int suppid, decimal refervalue)
        {
            const string sqlText = @"declare @status tinyint
set @status = 1
select @status = [status] from ordercardamt with(nolock) where orderid=@orderid
set @status = isnull(@status,1)

if (@status = 1 or @status = 4)
begin
	update ordercard set supplierID = @supplierID,refervalue=@refervalue where orderid=@orderid
	update ordercardamt set [status] = 1 where orderid=@orderid
end";
            SqlParameter[] parameters =
            {
                new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                new SqlParameter("@supplierID", SqlDbType.Int, 4),
                new SqlParameter("@refervalue", SqlDbType.Decimal, 9)
            };
            parameters[0].Value = orderid;
            parameters[1].Value = suppid;
            parameters[2].Value = refervalue;

            return DataBase.ExecuteNonQuery(CommandType.Text, sqlText, parameters) > 0;
        }

        #endregion

        #region Deduct

        /// <summary>
        ///     扣单
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
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordercard_deduct", parameters);

            return (bool)parameters[1].Value;
        }

        #endregion

        #region ReDeduct

        /// <summary>
        ///     还单
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
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordercard_rededuct", parameters);

            return (bool)parameters[1].Value;
        }

        #endregion

        #region ChkIsCanNotify

        /// <summary>
        /// </summary>
        public bool ChkIsCanNotify(string orderid)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@orderid", SqlDbType.VarChar, 30)
            };
            parameters[0].Value = orderid;

            object r = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_ordercard_chkIsCanNotify", parameters);
            if (r == DBNull.Value)
            {
                return false;
            }

            byte result = Convert.ToByte(r);

            return result == 1;
        }

        #endregion

        #region CallBackInsert

        /// <summary>
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="supplierId"></param>
        /// <param name="status"></param>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <param name="continueSubmit"></param>
        /// <returns></returns>
        public DataRow CallbackInsert(string orderid, int supplierId, int status, string errCode, string errMsg,
            byte continueSubmit)
        {
            string cacheKey = "CallbackInsert" + orderid + supplierId;
            try
            {
                object flag = HttpRuntime.Cache[cacheKey];
                if (flag != null)
                    return flag as DataRow;

                SqlParameter[] parameters =
                {
                    new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                    new SqlParameter("@suppId", SqlDbType.Int, 4),
                    new SqlParameter("@status", SqlDbType.TinyInt, 1),
                    new SqlParameter("@errCode", SqlDbType.NVarChar, 50),
                    new SqlParameter("@errMsg", SqlDbType.NVarChar, 200),
                    new SqlParameter("@addTime", SqlDbType.DateTime),
                    new SqlParameter("@continueSubmit", SqlDbType.TinyInt)
                };

                parameters[0].Value = orderid;
                parameters[1].Value = supplierId;
                parameters[2].Value = status;
                parameters[3].Value = errCode;
                parameters[4].Value = errMsg;
                parameters[5].Value = DateTime.Now;
                parameters[6].Value = continueSubmit;


                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercardcallback_Insert",
                    parameters);

                DataRow row = ds.Tables[0].Rows[0];

                HttpRuntime.Cache.Insert(cacheKey
                    , row
                    , null
                    , DateTime.Now.AddSeconds(5.0)
                    , TimeSpan.Zero);

                return row;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
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
        /// <returns>分页数据。</returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, bool isstat)
        {
            var ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "[id] desc";
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
,sum(promAmt) promAmt,sum(commission) commission from v_ordercard where " + searchWhere;
                }
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
                            case "orderid_like":
                                builder.Append(" AND [orderid] like @orderid");
                                parameter = new SqlParameter("@orderid", SqlDbType.VarChar, 30);
                                parameter.Value = SqlHelper.CleanString((string)iparam.ParamValue, 30) + "%";
                                paramList.Add(parameter);
                                break;
                            case "cardno":
                                builder.Append(" AND [cardNo] like @cardno");
                                parameter = new SqlParameter("@cardno", SqlDbType.NVarChar, 50);
                                parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 50) + "%";
                                paramList.Add(parameter);
                                break;
                            case "supplierorder":
                                builder.Append(" AND [supplierOrder] like @supplierOrder");
                                parameter = new SqlParameter("@supplierOrder", SqlDbType.VarChar, 30);
                                parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 30) + "%";
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
                                builder.Append(" AND ([notifystat] = @notifystat AND ordertype <> 8)");
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

        #region CheckCardInfo

        /// <summary>
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="userorderid"></param>
        /// <param name="cardtype"></param>
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <param name="orderAmt"></param>
        /// <returns></returns>
        public CheckAPIParameter CheckCardInfo(int userid
            , string userorderid
            , int cardtype
            , string cardno
            , string cardpwd
            , int orderAmt)
        {
            var result = new CheckAPIParameter();

            try
            {
                SqlParameter[] parameters =
                {
                      new SqlParameter("@userid", SqlDbType.Int, 4)
                    , new SqlParameter("@cardtype", SqlDbType.Int, 4)
                    , new SqlParameter("@cardNo", SqlDbType.NVarChar, 100)
                    , new SqlParameter("@cardPwd", SqlDbType.NVarChar, 100)
                    , new SqlParameter("@orderAmt", SqlDbType.Int, 4)
                    , new SqlParameter("@userorder", SqlDbType.NVarChar, 30)
                    , new SqlParameter("@checkTime", SqlDbType.DateTime, 8)
                };

                parameters[0].Value = userid;
                parameters[1].Value = cardtype;
                parameters[2].Value = cardno;
                parameters[3].Value = cardpwd;
                parameters[4].Value = orderAmt;
                parameters[5].Value = userorderid;
                parameters[6].Value = DateTime.Now.AddHours(-1);

                DataTable dt =
                    DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_checkcard_repeat", parameters).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    result.IsRepeat = Convert.ToByte(row["repeat"]);
                    result.Makeup = Convert.ToByte(row["makeup"]);
                    result.CardBalance = Convert.ToDecimal(row["withhold"]);
                    result.Cardpwd = Convert.ToString(row["cardpwd"]);
                    result.Supprate = Convert.ToDecimal(row["supprate"]);
                    result.Supplierid = Convert.ToInt32(row["supplierid"]);
                    result.Isclose = Convert.ToByte(row["isclose"]);
                }
                else
                {
                    result = null;
                }

                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        #endregion

        #region GetTimeoutRetrunOrders

        /// <summary>
        ///     取超时未返回的订单
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <returns></returns>
        public DataTable GetTimeoutRetrunOrders(DateTime sdt, DateTime edt)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@sdt", SqlDbType.DateTime, 8),
                new SqlParameter("@edt", SqlDbType.DateTime, 8)
            };
            parameters[0].Value = sdt;
            parameters[1].Value = edt;

            return
                DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_gettimeoutRetrun", parameters)
                    .Tables[0];
        }

        #endregion

        #region GetTimeoutRetrunOrders2

        /// <summary>
        ///     取通知失败的订单。
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <returns></returns>
        public DataTable GetTimeoutRetrunOrders2(DateTime sdt, DateTime edt)
        {
            string sqlText =
                @"select userid,ordercard.typeId,channeltype.modetypename,supplierID,orderid,cardNo,refervalue,processingtime from ordercard with(nolock), channeltype 
where 
	ordercard.typeId=channeltype.typeId
and
	processingtime >= @sdt and processingtime <= @edt
and
    makeup=0
and not exists(select 0 from ordercardamt with(nolock) where ordercardamt.orderid = ordercard.orderid)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@sdt", SqlDbType.DateTime, 8),
                new SqlParameter("@edt", SqlDbType.DateTime, 8)
            };
            parameters[0].Value = sdt;
            parameters[1].Value = edt;

            return DataBase.ExecuteDataset(CommandType.Text, sqlText, parameters).Tables[0];
        }

        #endregion

        #region GetToggleInterface

        /// <summary>
        ///     切换接口
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <returns></returns>
        public DataTable GetToggleInterfaceList(DateTime sdt, DateTime edt)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@sdt", SqlDbType.DateTime, 8),
                new SqlParameter("@edt", SqlDbType.DateTime, 8),
                new SqlParameter("@newtime", SqlDbType.DateTime, 8)
            };
            parameters[0].Value = sdt;
            parameters[1].Value = edt;
            parameters[2].Value = DateTime.Now;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_change_supplier",
                parameters);
            if (ds == null)
                return null;

            if (ds.Tables.Count > 0)
                return ds.Tables[0];

            return null;
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
        public bool Reconcilie(string orderid, byte reconciledResult, string reconciledAmt)
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

            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordercard_reconcilie", parameters);

            return (bool)parameters[1].Value;
        }
        #endregion

        #region SystemHandleOrder
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void SystemHandleOrder(OrderCardInfo model)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@orderid", SqlDbType.VarChar, 30), //订单号
                new SqlParameter("@typeid", SqlDbType.Int), //

                new SqlParameter("@userId", SqlDbType.Int), //商户编号
                new SqlParameter("@promId", SqlDbType.Int), //代理
                new SqlParameter("@manageId", SqlDbType.Int), //业务
                new SqlParameter("@status", SqlDbType.TinyInt), //订单状态
                            
                new SqlParameter("@refervalue", SqlDbType.Decimal, 9), //提交金额                            
                //new SqlParameter("@faceValue", SqlDbType.Decimal, 9), //真实面值
                //new SqlParameter("@realvalue", SqlDbType.Decimal, 9), //结算金额


                new SqlParameter("@completetime", SqlDbType.DateTime), //完成时间
                           
                new SqlParameter("@opstate", SqlDbType.NVarChar, 200), //opstate
                new SqlParameter("@suppcode", SqlDbType.NVarChar, 50), //
                new SqlParameter("@suppmsg", SqlDbType.NVarChar, 200), //信息
                new SqlParameter("@userMsg", SqlDbType.NVarChar, 200), //显示给用户的信息 
                           
                           
                new SqlParameter("@cardno", SqlDbType.VarChar, 40), //卡号                            
                new SqlParameter("@cardpwd", SqlDbType.VarChar, 40), //卡密   
                
                new SqlParameter("@ismulticard", SqlDbType.TinyInt,1),
                new SqlParameter("@batno", SqlDbType.VarChar,30)
   
            };
            parameters[0].Value = model.orderid;
            parameters[1].Value = model.typeId;

            parameters[2].Value = model.userid;
            parameters[3].Value = model.agentId;
            parameters[4].Value = model.manageId;
            parameters[5].Value = model.status;

            parameters[6].Value = model.refervalue;
            //parameters[7].Value = model.faceValue;
            //parameters[8].Value = model.realvalue;
            parameters[7].Value = DateTime.Now;

            parameters[8].Value = model.opstate;

            parameters[9].Value = model.errtype;
            parameters[10].Value = model.msg;
            parameters[11].Value = model.userViewMsg;

            parameters[12].Value = model.cardNo;
            parameters[13].Value = model.cardPwd;

            parameters[14].Value = model.ismulticard;
            parameters[15].Value = model.Batno;

            DataBase.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "proc_ordercard_sys_settled", parameters);
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

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_search", parameters);
            check_result = Convert.ToInt32(parameters[2].Value);

            if (check_result == 0)
                row = ds.Tables[0].Rows[0];



            return check_result;
        }
        #endregion

        #region search_check
        /// <summary>
        /// 
        /// </summary>
        /// <param name="batno"></param>
        /// <returns></returns>
        public DataSet GetlistBybatno(string batno)
        {
            SqlParameter[] parameters = {    
                                           new SqlParameter("@batno", SqlDbType.VarChar,30) 
                                        };
            parameters[0].Value = batno;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_getlist_bybatno", parameters);

            return ds;
        }
        #endregion
    }
}