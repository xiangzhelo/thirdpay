using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using viviapi.IDAL;
using viviapi.Model.Order;
using viviLib.ExceptionHandling;
using DBAccess;
using viviLib.Data;

//
namespace viviapi.SQLServerDAL
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderSms : IOrderSms
    {
        internal const string SQL_TABLE = "ordersms";
        internal const string FIELDS = @"[id],[status],[orderid],[userorder],[supplierId],[userid],[mobile],[fee],[message],[servicenum],[linkid],[gwid],[payRate],[supplierRate],[promRate],[payAmt],[promAmt],[supplierAmt],[profits],[server],[addtime],[completetime]";

        #region Insert
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public bool Insert(OrderSmsInfo model)
        {
            //try
            //{
                SqlParameter[] parameters = {
					new SqlParameter("@orderid", SqlDbType.NVarChar,30),
					new SqlParameter("@userorder", SqlDbType.NVarChar,30),
					new SqlParameter("@supplierId", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@mobile", SqlDbType.VarChar,20),
					new SqlParameter("@fee", SqlDbType.Decimal,9),
					new SqlParameter("@message", SqlDbType.NVarChar,50),
					new SqlParameter("@servicenum", SqlDbType.VarChar,50),
					new SqlParameter("@linkid", SqlDbType.VarChar,50),
					new SqlParameter("@gwid", SqlDbType.VarChar,2),
					new SqlParameter("@payRate", SqlDbType.Decimal,9),
					new SqlParameter("@supplierRate", SqlDbType.Decimal,9),
					new SqlParameter("@promRate", SqlDbType.Decimal,9),
					new SqlParameter("@payAmt", SqlDbType.Decimal,9),
					new SqlParameter("@promAmt", SqlDbType.Decimal,9),
					new SqlParameter("@supplierAmt", SqlDbType.Decimal,9),
					new SqlParameter("@profits", SqlDbType.Decimal,9),
					new SqlParameter("@server", SqlDbType.Int,4),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@completetime", SqlDbType.DateTime),
                    new SqlParameter("@status", SqlDbType.TinyInt),
                    new SqlParameter("@manageId", SqlDbType.Int),
                    new SqlParameter("@Cmd", SqlDbType.NVarChar,10),
                    new SqlParameter("@userMsgContenct", SqlDbType.NVarChar,50)};
                parameters[0].Value = model.orderid;
                parameters[1].Value = model.userorder;
                parameters[2].Value = model.supplierId;
                parameters[3].Value = model.userid;
                parameters[4].Value = model.mobile;
                parameters[5].Value = model.fee;
                parameters[6].Value = model.message;
                parameters[7].Value = model.servicenum;
                parameters[8].Value = model.linkid;
                parameters[9].Value = model.gwid;
                parameters[10].Value = model.payRate;
                parameters[11].Value = model.supplierRate;
                parameters[12].Value = model.promRate;
                parameters[13].Value = model.payAmt;
                parameters[14].Value = model.promAmt;
                parameters[15].Value = model.supplierAmt;
                parameters[16].Value = model.profits;
                parameters[17].Value = model.server;
                parameters[18].Value = model.addtime;
                parameters[19].Value = model.completetime;
                parameters[20].Value = model.status;
                parameters[21].Value = model.manageId;
                parameters[22].Value = model.Cmd;
                parameters[23].Value = model.userMsgContenct;

                DataBase.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "proc_ordersms_Insert", parameters);
                return true;
            //}
            //catch (Exception ex)
            //{
            //    viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            //    return false;
            //}
        }
        #endregion

        #region GetModel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal OrderSmsInfo GetModelFromDs(DataSet ds)
        {
            OrderSmsInfo model = new OrderSmsInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.orderid = ds.Tables[0].Rows[0]["orderid"].ToString();
                model.userorder = ds.Tables[0].Rows[0]["userorder"].ToString();
                if (ds.Tables[0].Rows[0]["supplierId"].ToString() != "")
                {
                    model.supplierId = int.Parse(ds.Tables[0].Rows[0]["supplierId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                model.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                if (ds.Tables[0].Rows[0]["fee"].ToString() != "")
                {
                    model.fee = decimal.Parse(ds.Tables[0].Rows[0]["fee"].ToString());
                }
                model.message = ds.Tables[0].Rows[0]["message"].ToString();
                model.servicenum = ds.Tables[0].Rows[0]["servicenum"].ToString();
                model.linkid = ds.Tables[0].Rows[0]["linkid"].ToString();
                model.gwid = ds.Tables[0].Rows[0]["gwid"].ToString();
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
                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["completetime"].ToString() != "")
                {
                    model.completetime = DateTime.Parse(ds.Tables[0].Rows[0]["completetime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
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
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderSmsInfo GetModel(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userid", SqlDbType.Int) };

            parameters[0].Value = id;
            parameters[1].Value = DBNull.Value;
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordersms_GetById", parameters);

            return GetModelFromDs(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OrderSmsInfo GetModel(int id, int userid)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userid", SqlDbType.Int) };
            parameters[0].Value = id;
            parameters[1].Value = userid;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordersms_GetById", parameters);
            return GetModelFromDs(ds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderSmsInfo GetModel(string orderId)
        {
            SqlParameter[] parameters = { new SqlParameter("@orderId", SqlDbType.VarChar, 30) };

            parameters[0].Value = orderId;
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordersms_Get", parameters);

            return GetModelFromDs(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OrderSmsInfo GetModel(int suppId, string linkId)
        {
            SqlParameter[] parameters = { 
                                            new SqlParameter("@supplierId", SqlDbType.Int)
                                            ,new SqlParameter("@linkid", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = suppId;
            parameters[1].Value = linkId;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordersms_GetModel", parameters);
            return GetModelFromDs(ds);
        }
        #endregion

        #region Deduct
        /// <summary>
        ///  结算
        /// </summary>
        public bool Deduct(string orderid)
        {
            SqlParameter[] parameters = {					
			                new SqlParameter("@orderid", SqlDbType.VarChar,30),
                            new SqlParameter("@result", SqlDbType.Bit)
                                        };
            parameters[0].Value = orderid;
            parameters[1].Direction = ParameterDirection.Output;
            DataBase.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "proc_ordersms_deduct", parameters);

            return (bool)parameters[1].Value;
        }
        #endregion

        #region ReDeduct
        /// <summary>
        ///  结算
        /// </summary>
        public bool ReDeduct(string orderid)
        {
            SqlParameter[] parameters = {					
			                new SqlParameter("@orderid", SqlDbType.VarChar,30),
                            new SqlParameter("@result", SqlDbType.Bit)
                                        };
            parameters[0].Value = orderid;
            parameters[1].Direction = ParameterDirection.Output;
            DataBase.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "proc_ordersms_rededuct", parameters);

            return (bool)parameters[1].Value;
        }
        #endregion

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Notify(OrderSmsInfo model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {                        
					    new SqlParameter("@linkId", SqlDbType.VarChar,50),		
                        new SqlParameter("@againNotifyUrl", SqlDbType.VarChar,300),
					    new SqlParameter("@notifycount", SqlDbType.Int,4),
					    new SqlParameter("@notifystat", SqlDbType.TinyInt,1),
					    new SqlParameter("@notifycontext", SqlDbType.VarChar,200),
                        new SqlParameter("@notifytime", SqlDbType.DateTime),
                        new SqlParameter("@userOrder", SqlDbType.VarChar,30),
                        new SqlParameter("@suppId", SqlDbType.VarChar,50),
                        new SqlParameter("@issucc", SqlDbType.Bit,1),//是否成功
                        new SqlParameter("@errcode", SqlDbType.VarChar,50) //错误代码
					};
            parameters[0].Value = model.linkid;
            //parameters[1].Value = model.notifyurl;
            parameters[1].Value = model.againNotifyUrl;
            parameters[2].Value = model.notifycount;
            parameters[3].Value = model.notifystat;
            parameters[4].Value = model.notifycontext;
            parameters[5].Value = DateTime.Now;
            if (model.issucc)
            {
                parameters[6].Value = model.notifycontext;
            }
            else
            {
                parameters[6].Value = string.Empty;
            }
            parameters[7].Value = model.supplierId;
            parameters[8].Value = model.issucc;
            parameters[9].Value = model.errcode;

            rowsAffected = DataBase.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "proc_ordersms_notify", parameters);
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

        #region 查询有关
        /// <summary>
        /// 根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = "V_ordersms";
                string fields = @"[id],[orderid],[userorder],[supplierId],[userid],[mobile],[fee],[message],[servicenum],[linkid],[gwid],[payRate],[supplierRate],[promRate],[payAmt],[promAmt],[supplierAmt],[profits],[server],[addtime],[completetime],
[againNotifyUrl],[notifycount],[notifystat],[notifycontext],[notifytime],[status],[commission]";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string searchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, searchWhere, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(fields, tables, searchWhere, orderby, key, pageSize, page, false)
                    + "\r\n" + "select sum(fee) realvalue,sum(case when [status]=2 then payAmt else 0 end) payAmt,sum(supplierAmt-(case when [status]=2 then payAmt else 0 end)) profits,sum(promAmt) promAmt,sum(commission) commission from V_ordersms where " + searchWhere;

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
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            StringBuilder builder = new StringBuilder(" 1 = 1");

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
                            case "typeid":
                                builder.Append(" AND [typeId] = @typeId");
                                parameter = new SqlParameter("@typeId", SqlDbType.Int);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "userorder":
                                builder.Append(" AND [userorder] like @userorder");
                                parameter = new SqlParameter("@userorder", SqlDbType.VarChar, 30);
                                parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 30) + "%";
                                paramList.Add(parameter);
                                break;
                            case "supplierorder":
                                builder.Append(" AND [linkid] like @supplierOrder");
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
                            case "notifystat":
                                builder.Append(" AND [notifystat] = @notifystat");
                                parameter = new SqlParameter("@notifystat", SqlDbType.TinyInt);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "promid":
                                builder.Append(" AND exists(select 0 from PromotionUser where PromotionUser.PID = @promid and PromotionUser.RegId=userid)");
                                parameter = new SqlParameter("@promid", SqlDbType.Int);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "stime":
                                builder.Append(" AND [addtime] >= @stime");
                                parameter = new SqlParameter("@stime", SqlDbType.DateTime);
                                parameter.Value = iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "etime":
                                builder.Append(" AND [addtime] <= @etime");
                                parameter = new SqlParameter("@etime", SqlDbType.DateTime);
                                parameter.Value = iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "mobile":
                                builder.Append(" AND [mobile] like @mobile");
                                parameter = new SqlParameter("@mobile", SqlDbType.VarChar,20);
                                parameter.Value = "%" + (string)iparam.ParamValue + "%";
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
    }
}
