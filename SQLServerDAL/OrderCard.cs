using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
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
    public class OrderCard:IOrderCard
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
      ,[version],cus_subject,cus_price,cus_quantity,cus_description,cus_field1,cus_field2,cus_field3,cus_field4,cus_field5,errtype,agentid,faceValue";

        #region Insert
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public long Insert(OrderCardInfo model)
        {
            //try
            //{
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt,8),
					new SqlParameter("@orderid", SqlDbType.VarChar,30),
					new SqlParameter("@ordertype", SqlDbType.TinyInt,1),
					new SqlParameter("@userid", SqlDbType.Int,4),
                    new SqlParameter("@typeId", SqlDbType.Int),
					new SqlParameter("@paymodeId", SqlDbType.VarChar,10),
					new SqlParameter("@userorder", SqlDbType.VarChar,30),
					new SqlParameter("@refervalue", SqlDbType.Decimal,9),
					new SqlParameter("@notifyurl", SqlDbType.VarChar,200),
					new SqlParameter("@returnurl", SqlDbType.VarChar,200),
					new SqlParameter("@attach", SqlDbType.VarChar,255),
                    new SqlParameter("@payerip", SqlDbType.VarChar,20),
					new SqlParameter("@clientip", SqlDbType.VarChar,20),
					new SqlParameter("@referUrl", SqlDbType.NVarChar,2000),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@supplierID", SqlDbType.Int,4),
                    new SqlParameter("@status", SqlDbType.TinyInt,1),
                    new SqlParameter("@cardNo", SqlDbType.VarChar,50),
                    new SqlParameter("@cardPwd", SqlDbType.VarChar,50),
                    new SqlParameter("@server", SqlDbType.Int),
                    new SqlParameter("@manageId", SqlDbType.Int),
                    new SqlParameter("@cardnum", SqlDbType.Int),
                    new SqlParameter("@resultcode", SqlDbType.NVarChar,100),
                    new SqlParameter("@ismulticard", SqlDbType.TinyInt,1),
                    new SqlParameter("@ovalue", SqlDbType.NVarChar,200),
                    new SqlParameter("@opstate", SqlDbType.NVarChar,200),
                    new SqlParameter("@msg", SqlDbType.NVarChar,200),
                    new SqlParameter("@cardtype", SqlDbType.Int,4),
                    new SqlParameter("@version", SqlDbType.VarChar,10),
                    new SqlParameter("@cus_subject", SqlDbType.NVarChar,100),
					new SqlParameter("@cus_price", SqlDbType.NVarChar,50),
					new SqlParameter("@cus_quantity", SqlDbType.NVarChar,50),
					new SqlParameter("@cus_description", SqlDbType.NVarChar,1000),
					new SqlParameter("@cus_field1", SqlDbType.NVarChar,100),
					new SqlParameter("@cus_field2", SqlDbType.NVarChar,100),
					new SqlParameter("@cus_field3", SqlDbType.NVarChar,100),
					new SqlParameter("@cus_field4", SqlDbType.NVarChar,100),
					new SqlParameter("@cus_field5", SqlDbType.NVarChar,100),
                    new SqlParameter("@agentid", SqlDbType.Int),
                    new SqlParameter("@faceValue", SqlDbType.Decimal,9),
                    new SqlParameter("@userViewMsg", SqlDbType.NVarChar,100),
                    new SqlParameter("@errtype", SqlDbType.NVarChar,50),
                    new SqlParameter("@makeup", SqlDbType.TinyInt,1),
                    new SqlParameter("@desc", SqlDbType.VarChar,100),
                    new SqlParameter("@batno", SqlDbType.VarChar,30)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.orderid;
                parameters[2].Value = model.ordertype;
                parameters[3].Value = model.userid;
                parameters[4].Value = model.typeId;
                parameters[5].Value = model.paymodeId;
                parameters[6].Value = model.userorder;
                parameters[7].Value = model.refervalue;
              
                parameters[8].Value = model.notifyurl;
                parameters[9].Value = model.returnurl;
                parameters[10].Value = model.attach;
                parameters[11].Value = model.payerip;
                parameters[12].Value = model.clientip;
                parameters[13].Value = model.referUrl;
                parameters[14].Value = model.addtime;
                parameters[15].Value = model.supplierId;
               
                parameters[16].Value = model.status;
                parameters[17].Value = model.cardNo;
                parameters[18].Value = model.cardPwd;

                parameters[19].Value = model.server;
                parameters[20].Value = model.manageId;

                parameters[21].Value = model.cardnum;
                parameters[22].Value = model.resultcode;

                parameters[23].Value = model.ismulticard;
                parameters[24].Value = model.ovalue;

                parameters[25].Value = model.opstate;
                parameters[26].Value = model.msg;

                parameters[27].Value = model.cardType;
                parameters[28].Value = model.version;

                parameters[29].Value = model.cus_subject;
                parameters[30].Value = model.cus_price;
                parameters[31].Value = model.cus_quantity;
                parameters[32].Value = model.cus_description;
                parameters[33].Value = model.cus_field1;
                parameters[34].Value = model.cus_field2;
                parameters[35].Value = model.cus_field3;
                parameters[36].Value = model.cus_field4;
                parameters[37].Value = model.cus_field5;

                parameters[38].Value = model.agentId;

                parameters[39].Value = model.faceValue;
                parameters[40].Value = model.userViewMsg;
                parameters[41].Value = model.errtype;

                parameters[42].Value = model.makeup;

                parameters[43].Value = model.Desc;

                parameters[44].Value = model.Batno;


                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordercard_add", parameters);
                return (long)parameters[0].Value;
            //}
            //catch (Exception ex)
            //{
            //    viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            //    return 0;
            //}
        }
        #endregion

        #region InsertItem
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long InsertItem(CardItemInfo model)
        {
            //try
            //{
            // int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt,8),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@serial", SqlDbType.Int,4),
					new SqlParameter("@porderid", SqlDbType.NVarChar,30),
					new SqlParameter("@suppid", SqlDbType.Int,4),
					new SqlParameter("@cardtype", SqlDbType.Int,4),
					new SqlParameter("@cardno", SqlDbType.NVarChar,30),
					new SqlParameter("@cardpwd", SqlDbType.NVarChar,50),
					new SqlParameter("@refervalue", SqlDbType.Decimal,9),
					new SqlParameter("@payrate", SqlDbType.Decimal,9),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@supplierOrder", SqlDbType.VarChar,50),
					new SqlParameter("@realvalue", SqlDbType.Decimal,9),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@opstate", SqlDbType.NVarChar,20),
					new SqlParameter("@msg", SqlDbType.NVarChar,50),
					new SqlParameter("@completetime", SqlDbType.DateTime),
					new SqlParameter("@supplierrate", SqlDbType.Decimal,9),
					new SqlParameter("@promrate", SqlDbType.Decimal,9),
					new SqlParameter("@commission", SqlDbType.Decimal,9),
                    new SqlParameter("@agentid", SqlDbType.Int,4)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.serial;
            parameters[3].Value = model.porderid;
            parameters[4].Value = model.suppid;
            parameters[5].Value = model.cardtype;
            parameters[6].Value = model.cardno;
            parameters[7].Value = model.cardpwd;
            parameters[8].Value = model.refervalue;
            parameters[9].Value = model.payrate;
            parameters[10].Value = model.addtime;
            parameters[11].Value = model.supplierOrder;
            parameters[12].Value = model.realvalue;
            parameters[13].Value = model.status;
            parameters[14].Value = model.opstate;
            parameters[15].Value = model.msg;
            parameters[16].Value = model.completetime;
            parameters[17].Value = model.supplierrate;
            parameters[18].Value = model.promrate;
            parameters[19].Value = model.commission;
            parameters[20].Value = model.agentId;


            DataBase.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "proc_ordercarditem_add", parameters);
            return (long)parameters[0].Value;
            //}
            //catch (Exception ex)
            //{
            //    viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            //    return 0;
            //}
        }
        #endregion

        #region Complete
        /// <summary>
        ///  结算
        /// </summary>
        public bool Complete(OrderCardInfo model)
        {
            string cacheKey = "Complete" + model.orderid + model.supplierId.ToString(CultureInfo.InvariantCulture);
            object flag = HttpRuntime.Cache[cacheKey];
            if (flag != null)
                return true;

            //try
            //{
                SqlParameter[] parameters = {					
			                new SqlParameter("@orderid", SqlDbType.VarChar,30),//订单号
                            new SqlParameter("@method", SqlDbType.TinyInt,1),//返回方式
                            new SqlParameter("@supplierid", SqlDbType.Int,4),//
                            new SqlParameter("@userId", SqlDbType.Int),//商户编号
                            new SqlParameter("@promId", SqlDbType.Int),//代理
                            new SqlParameter("@manageId", SqlDbType.Int),//业务

                            new SqlParameter("@status", SqlDbType.TinyInt),//订单状态
                            new SqlParameter("@supplierOrder", SqlDbType.VarChar,50), //接口商处理号
                            
                            new SqlParameter("@refervalue", SqlDbType.Decimal,9),//提交金额                            
                            new SqlParameter("@faceValue", SqlDbType.Decimal,9),//真实面值
                            new SqlParameter("@realvalue", SqlDbType.Decimal,9),//结算金额
                            new SqlParameter("@withholdAmt", SqlDbType.Decimal,9),//扣压金额
                            new SqlParameter("@profits", SqlDbType.Decimal,9),//利润

                            new SqlParameter("@payRate", SqlDbType.Decimal,9),//结算费率
                            new SqlParameter("@payAmt", SqlDbType.Decimal,9),//商家所得    
                            
                            new SqlParameter("@supplierRate", SqlDbType.Decimal,9), //平台所得费率
                            new SqlParameter("@supplierAmt", SqlDbType.Decimal,9),//平台所得

                            new SqlParameter("@promRate", SqlDbType.Decimal,9),//代理费率
                            new SqlParameter("@promAmt", SqlDbType.Decimal,9),//代理所得                           
                           
                            new SqlParameter("@addtime", SqlDbType.DateTime),//添加时间
                            new SqlParameter("@completetime", SqlDbType.DateTime),//完成时间
                           
                            new SqlParameter("@msg", SqlDbType.NVarChar,200),//信息
                            new SqlParameter("@userViewMsg", SqlDbType.NVarChar,200),//显示给用户的信息                            
                            new SqlParameter("@opstate", SqlDbType.NVarChar,200),//opstate
                            new SqlParameter("@errtype", SqlDbType.NVarChar,50),//
                           
                            new SqlParameter("@typeid", SqlDbType.Int),//
                            new SqlParameter("@cardno", SqlDbType.VarChar,40),//卡号                            
                            new SqlParameter("@cardpwd", SqlDbType.VarChar,40),//卡密                           
                            new SqlParameter("@cardversion", SqlDbType.TinyInt,1),//返回方式
                            new SqlParameter("@withhold_type", SqlDbType.TinyInt,1),//返回方式
                            new SqlParameter("@ismulticard", SqlDbType.TinyInt,1),
                            new SqlParameter("@batno", SqlDbType.VarChar,30)
                                        };
                parameters[0].Value = model.orderid;
                parameters[1].Value = model.method;
                parameters[2].Value = model.supplierId;

                parameters[3].Value = model.userid;
                parameters[4].Value = model.agentId;
                parameters[5].Value = model.manageId;
                parameters[6].Value = model.status;
                parameters[7].Value = model.supplierOrder;

                parameters[8].Value = model.refervalue;
                parameters[9].Value = model.faceValue;
                parameters[10].Value = model.realvalue;
                parameters[11].Value = model.withholdAmt;
                parameters[12].Value = model.profits;

                parameters[13].Value = model.payRate;
                parameters[14].Value = model.payAmt;

                parameters[15].Value = model.supplierRate;
                parameters[16].Value = model.supplierAmt;

                parameters[17].Value = model.promRate;
                parameters[18].Value = model.promAmt;

                parameters[19].Value = DateTime.Now;
                parameters[20].Value = model.completetime;

                parameters[21].Value = model.msg;
                parameters[22].Value = model.userViewMsg;
                parameters[23].Value = model.opstate;
                parameters[24].Value = model.errtype;

                parameters[25].Value = model.typeId;
                parameters[26].Value = model.cardNo;
                parameters[27].Value = model.cardPwd;
                parameters[28].Value = model.cardversion;
                parameters[29].Value = model.withhold_type;

                parameters[30].Value = model.ismulticard;
                parameters[31].Value = model.Batno;

                DataBase.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "proc_ordercard_settled", parameters);
                HttpRuntime.Cache.Insert(cacheKey, model.status, null, DateTime.Now.AddSeconds(5.0), TimeSpan.Zero);
                return true;
            //}
            //catch (Exception ex)
            //{
            //    viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            //    return false;
            //}
        }
        #endregion

        #region ItemComplete
        /// <summary>
        /// 加入缓存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="allCompleted"></param>
        /// <param name="totalValue"></param>
        /// <param name="resultcode"></param>
        /// <returns></returns>
        public bool ItemComplete(CardItemInfo model, out bool allCompleted, out string opstate, out string ovalue, out decimal ototalvalue)
        {
            allCompleted = false;            
            opstate = string.Empty;
            ovalue = string.Empty;
            ototalvalue = 0M;

            object flag = HttpRuntime.Cache["Item_Complete" + model.porderid + model.serial.ToString()];
            if (flag != null)
                return true;

            #region 数据库操作
            SqlParameter[] parameters = {					
			                new SqlParameter("@orderid", SqlDbType.VarChar,30),
                            new SqlParameter("@serial", SqlDbType.Int),
                            new SqlParameter("@status", SqlDbType.TinyInt),
                            new SqlParameter("@supplierOrder", SqlDbType.VarChar,50),                           
                            new SqlParameter("@realvalue", SqlDbType.Decimal,9),
                            new SqlParameter("@payrate", SqlDbType.Decimal,9),
                            //new SqlParameter("@supplierRate", SqlDbType.Decimal,9),                            
                            //new SqlParameter("@payAmt", SqlDbType.Decimal,9),
                            //new SqlParameter("@supplierAmt", SqlDbType.Decimal,9),
                            //new SqlParameter("@profits", SqlDbType.Decimal,9),
                            //new SqlParameter("@addtime", SqlDbType.DateTime),
                            new SqlParameter("@completetime", SqlDbType.DateTime),
                            new SqlParameter("@opstate", SqlDbType.NVarChar,10),
                            new SqlParameter("@msg", SqlDbType.NVarChar,50),
                            //new SqlParameter("@manageId", SqlDbType.Int),
                            new SqlParameter("@completed", SqlDbType.TinyInt),
                            new SqlParameter("@promRate", SqlDbType.Decimal,9)
                                        };
            parameters[0].Value = model.porderid;
            parameters[1].Value = model.serial;
            parameters[2].Value = model.status;
            parameters[3].Value = model.supplierOrder;
            parameters[4].Value = model.realvalue;
            parameters[5].Value = model.payrate;
            parameters[6].Value = model.completetime;
            parameters[7].Value = model.opstate;
            parameters[8].Value = model.msg;
            parameters[9].Direction = ParameterDirection.Output;
            parameters[10].Value = model.promrate;

            DataSet ds = DataBase.ExecuteDataset(System.Data.CommandType.StoredProcedure, "proc_ordercarditem_settled", parameters);
            HttpRuntime.Cache.Insert("Item_Complete" + model.porderid + model.serial.ToString(), model.status, null, DateTime.Now.AddSeconds(5.0), TimeSpan.Zero);
            #endregion

            if (parameters[9].Value != DBNull.Value)
            {
                #region 是否订单的所有子订单全部完成
                int result = Convert.ToInt32(parameters[9].Value);
                if(result == 1)
                    allCompleted = true;
                #endregion

                if (allCompleted)
                {
                    DataTable table1 = ds.Tables[0];
                    if (table1 != null && table1.Rows.Count > 0)
                    {
                        DataRow dr = table1.Rows[0];
                        if (dr["totalValue"] != DBNull.Value)
                        {
                            ototalvalue = Convert.ToDecimal(dr["totalValue"]);
                        }
                        if (dr["resultcode"] != DBNull.Value)
                        {
                            opstate = Convert.ToString(dr["resultcode"]);
                        }
                        if (dr["ovalue"] != DBNull.Value)
                        {
                            ovalue = Convert.ToString(dr["ovalue"]);
                        }
                    }
                }
            }

            return true;
        }
        #endregion

        #region GetModel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal OrderCardInfo GetModelFromDs(DataSet ds)
        {
            OrderCardInfo model = new OrderCardInfo();

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
                if (ds.Tables[0].Rows[0]["cus_quantity"] != null && ds.Tables[0].Rows[0]["cus_quantity"].ToString() != "")
                {
                    model.cus_quantity = ds.Tables[0].Rows[0]["cus_quantity"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cus_description"] != null && ds.Tables[0].Rows[0]["cus_description"].ToString() != "")
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
        public OrderCardInfo GetModelByOrderId(string orderId)
        {
            SqlParameter[] parameters = { new SqlParameter("@orderid", SqlDbType.VarChar, 30) };

            parameters[0].Value = orderId;
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_get", parameters);
            
            return GetModelFromDs(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OrderCardInfo GetModel(long id, int userid)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.BigInt), new SqlParameter("@userid", SqlDbType.Int) };
            parameters[0].Value = id;
            parameters[1].Value = userid;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_GetModel", parameters);
            return GetModelFromDs(ds);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OrderCardInfo GetModel(long id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.BigInt), new SqlParameter("@userid", SqlDbType.Int) };
            parameters[0].Value = id;
            parameters[1].Value = DBNull.Value;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_GetModel", parameters);
            return GetModelFromDs(ds);
        }
        #endregion        

        #region GetItemModel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="serial"></param>
        /// <returns></returns>
        public CardItemInfo GetItemModel(string orderId, int serial)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@orderId", SqlDbType.NVarChar,30),
                    new SqlParameter("@serial", SqlDbType.Int,4)};
            parameters[0].Value = orderId;
            parameters[1].Value = serial;

            CardItemInfo model = new CardItemInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercarditem_GetModel", parameters);            
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = long.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
              
                if (ds.Tables[0].Rows[0]["serial"].ToString() != "")
                {
                    model.serial = int.Parse(ds.Tables[0].Rows[0]["serial"].ToString());
                }
                model.porderid = ds.Tables[0].Rows[0]["porderid"].ToString();
                if (ds.Tables[0].Rows[0]["suppid"].ToString() != "")
                {
                    model.suppid = int.Parse(ds.Tables[0].Rows[0]["suppid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["cardtype"].ToString() != "")
                {
                    model.cardtype = int.Parse(ds.Tables[0].Rows[0]["cardtype"].ToString());
                }
                model.cardno = ds.Tables[0].Rows[0]["cardno"].ToString();
                model.cardpwd = ds.Tables[0].Rows[0]["cardpwd"].ToString();
                if (ds.Tables[0].Rows[0]["refervalue"].ToString() != "")
                {
                    model.refervalue = decimal.Parse(ds.Tables[0].Rows[0]["refervalue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payrate"].ToString() != "")
                {
                    model.payrate = decimal.Parse(ds.Tables[0].Rows[0]["payrate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                model.supplierOrder = ds.Tables[0].Rows[0]["supplierOrder"].ToString();
                if (ds.Tables[0].Rows[0]["realvalue"].ToString() != "")
                {
                    model.realvalue = decimal.Parse(ds.Tables[0].Rows[0]["realvalue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                model.opstate = ds.Tables[0].Rows[0]["opstate"].ToString();
                model.msg = ds.Tables[0].Rows[0]["msg"].ToString();
                if (ds.Tables[0].Rows[0]["completetime"].ToString() != "")
                {
                    model.completetime = DateTime.Parse(ds.Tables[0].Rows[0]["completetime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["supplierrate"].ToString() != "")
                {
                    model.supplierrate = decimal.Parse(ds.Tables[0].Rows[0]["supplierrate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["promrate"].ToString() != "")
                {
                    model.promrate = decimal.Parse(ds.Tables[0].Rows[0]["promrate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["commission"].ToString() != "")
                {
                    model.commission = decimal.Parse(ds.Tables[0].Rows[0]["commission"].ToString());
                }
                if (ds.Tables[0].Rows[0]["agent"].ToString() != "")
                {
                    model.agentId = int.Parse(ds.Tables[0].Rows[0]["agent"].ToString());
                }
                
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        public DataTable DataItemsByOrderId(string orderId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@orderId", SqlDbType.NVarChar,30)
                                        };
            parameters[0].Value = orderId;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercarditem_Getlistbyorderid", parameters);
            return ds.Tables[0];
        }

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Notify(OrderCardInfo model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					    new SqlParameter("@orderid", SqlDbType.VarChar,30),		
                        new SqlParameter("@againNotifyUrl", SqlDbType.NVarChar,2000),
					    new SqlParameter("@notifycount", SqlDbType.Int,4),
					    new SqlParameter("@notifystat", SqlDbType.TinyInt,1),
					    new SqlParameter("@notifycontext", SqlDbType.VarChar,200),
                        new SqlParameter("@notifytime", SqlDbType.DateTime)
					};
            parameters[0].Value = model.orderid;
            //parameters[1].Value = model.notifyurl;
            parameters[1].Value = model.againNotifyUrl;
            parameters[2].Value = model.notifycount;
            parameters[3].Value = model.notifystat;
            parameters[4].Value = model.notifycontext;
            parameters[5].Value = DateTime.Now;

            rowsAffected = DataBase.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "proc_ordercard_notify", parameters);
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
        public DataSet ItemPageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "[id] desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string searchWhere = ItemBuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, searchWhere, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(FIELDS, tables, searchWhere, orderby, key, pageSize, page, false)
                + "\r\n" + "select sum(refervalue) refervalue,sum(case when [status]=2 then realvalue else 0 end) realvalue,sum(case when [status]=2 then payAmt else 0 end) payAmt,sum(supplierAmt-(case when [status]=2 then payAmt else 0 end)) profits,sum(promAmt) promAmt from V_ordercard where " + searchWhere;

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
        private static string ItemBuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
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
                            case "manageid":
                                builder.Append(" AND [manageId] = @manageId");
                                parameter = new SqlParameter("@manageId", SqlDbType.Int);
                                parameter.Value = (int)iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "typeId":
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
