using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using viviLib.Data;

using viviapi.IDAL;
using viviapi.Model.Order;
using viviLib.ExceptionHandling;
using DBAccess;

//
namespace viviapi.SQLServerDAL
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderBank:IOrderBank
    {
        #region Insert
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public long Insert(OrderBankInfo model)
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
					new SqlParameter("@referUrl", SqlDbType.VarChar,200),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@supplierId", SqlDbType.Int,4),                    
					new SqlParameter("@status", SqlDbType.TinyInt,1),
                    new SqlParameter("@server", SqlDbType.Int),
                    new SqlParameter("@manageId", SqlDbType.Int),//业务员
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
                    new SqlParameter("@agentId", SqlDbType.Int,4),
                                            };
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
                parameters[17].Value = model.server;
                parameters[18].Value = model.manageId;
                parameters[19].Value = model.version;

                parameters[20].Value = model.cus_subject;
                parameters[21].Value = model.cus_price;
                parameters[22].Value = model.cus_quantity;
                parameters[23].Value = model.cus_description;
                parameters[24].Value = model.cus_field1;
                parameters[25].Value = model.cus_field2;
                parameters[26].Value = model.cus_field3;
                parameters[27].Value = model.cus_field4;
                parameters[28].Value = model.cus_field5;
                parameters[29].Value = model.agentId;

                DataBase.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "proc_orderbank_add", parameters);
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
        public bool Complete(OrderBankInfo model)
        {
            SqlParameter[] parameters = {	
				            new SqlParameter("@suppId", SqlDbType.Int),
			                new SqlParameter("@orderid", SqlDbType.VarChar,30),
                            new SqlParameter("@userId", SqlDbType.Int),
                            new SqlParameter("@typeId", SqlDbType.Int),
                            new SqlParameter("@status", SqlDbType.TinyInt),
                            new SqlParameter("@deduct", SqlDbType.TinyInt),
                            new SqlParameter("@supplierOrder", SqlDbType.VarChar,50),                           
                            new SqlParameter("@realvalue", SqlDbType.Decimal,9),
                            new SqlParameter("@payRate", SqlDbType.Decimal,9),
                            new SqlParameter("@supplierRate", SqlDbType.Decimal,9),                            
                            new SqlParameter("@payAmt", SqlDbType.Decimal,9),
                            new SqlParameter("@supplierAmt", SqlDbType.Decimal,9),
                            new SqlParameter("@profits", SqlDbType.Decimal,9),
                            new SqlParameter("@addtime", SqlDbType.DateTime),
                            new SqlParameter("@completetime", SqlDbType.DateTime),
                            new SqlParameter("@manageId", SqlDbType.Int),
                            new SqlParameter("@promRate", SqlDbType.Decimal,9),
                            new SqlParameter("@promAmt", SqlDbType.Decimal,9),
                            new SqlParameter("@promId", SqlDbType.Int)
                                        };
            parameters[0].Value = model.supplierId;
            parameters[1].Value = model.orderid;
            parameters[2].Value = model.userid;
            parameters[3].Value = model.typeId;
            parameters[4].Value = model.status;
            parameters[5].Value = model.deduct;
            parameters[6].Value = model.supplierOrder;
            parameters[7].Value = model.realvalue;
            parameters[8].Value = model.payRate;
            parameters[9].Value = model.supplierRate;
            parameters[10].Value = model.payAmt;
            parameters[11].Value = model.supplierAmt;
            parameters[12].Value = model.profits;
            parameters[13].Value = DateTime.Now;
            parameters[14].Value = model.completetime;
            parameters[15].Value = model.manageId;
            parameters[16].Value = model.promRate;
            parameters[17].Value = model.promAmt;
            parameters[18].Value = model.agentId;

            /*
                1 不存在此订单
             * 2 订单已处理
             * 3
			0 处理成功
             */
            object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_orderbank_settled", parameters);

            if (result == DBNull.Value)
                return false;

            byte bresult = Convert.ToByte(result);

            bool sucess = (bresult == 0 || bresult == 2);
           
            return sucess;
        }
        #endregion

        #region GetModel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal OrderBankInfo GetModelFromDs(DataSet ds)
        {
            OrderBankInfo model = new OrderBankInfo();

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
                if (ds.Tables[0].Rows[0]["agentid"] != null && ds.Tables[0].Rows[0]["agentid"].ToString() != "")
                {
                    model.agentId = Convert.ToInt32(ds.Tables[0].Rows[0]["agentid"].ToString());
                }
               
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
        public OrderBankInfo GetModelByOrderId(string orderId)
        {
            SqlParameter[] parameters = { new SqlParameter("@orderid", SqlDbType.VarChar, 30) };

            parameters[0].Value = orderId;
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_orderbank_getbankdirectinfo", parameters);
            
            return GetModelFromDs(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public  OrderBankInfo GetModel(long id, int userid)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.BigInt), new SqlParameter("@userid", SqlDbType.Int) };
            parameters[0].Value = id;
            parameters[1].Value = userid;
            
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_orderbank_GetModel", parameters);
            return GetModelFromDs(ds);
        }

        public OrderBankInfo GetModel(long id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.BigInt), new SqlParameter("@userid", SqlDbType.Int) };
            parameters[0].Value = id;
            parameters[1].Value = DBNull.Value;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_orderbank_GetModel", parameters);
            return GetModelFromDs(ds);
        }
        #endregion        

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Notify(OrderBankInfo model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					    new SqlParameter("@orderid", SqlDbType.VarChar,30),		
                        new SqlParameter("@againNotifyUrl", SqlDbType.VarChar,2000),
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
            parameters[5].Value = model.notifytime;

            rowsAffected = DataBase.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "proc_orderbank_notify", parameters);
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

        
    }
}
