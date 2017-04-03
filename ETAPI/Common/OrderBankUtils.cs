using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using viviapi.BLL;
using viviapi.Model.Common;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.SysInterface.Bank;

namespace viviapi.ETAPI.Common
{
    /// <summary>
    /// 网银
    /// </summary>
    public class OrderBankUtils
    {
        static readonly BLL.OrderBank BLLBank = new OrderBank();

        #region SuppPageReturn
        /// <summary>
        /// 处理网银支付同步返回订单信息，执行扣量等操作，并跳转到支付完成页面
        /// （与SuppNotify相比没有给供应商接口返回的成功或失败信息输出,而是跳转到支付完成页面）
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="orderId">系统内部订单号</param>
        /// <param name="supplierOrderId">接口订单号</param>
        /// <param name="status">状态</param>
        /// <param name="opstate">返回码</param>
        /// <param name="msg">返回信息</param>
        /// <param name="tranAmt">消费金额</param>
        /// <param name="suppSettleAmt">供应商给平台的费率</param>
        public static void SuppPageReturn(int supplierId, string orderId, string supplierOrderId, int status,
            string opstate, string msg, decimal tranAmt, decimal suppSettleAmt)
        {
            bool insertToDb = false;

            var orderInfo = Cache.WebCache.GetCacheService().RetrieveObject(orderId) as OrderBankInfo;
            if (orderInfo == null)
            {
                orderInfo = BLL.Order.Bank.Factory.Instance.GetModelByOrderId(orderId);
            }

            if (orderInfo != null)
            {
                insertToDb = orderInfo.status == 1;

                var execResult = new FunExecResult();
                if (insertToDb)
                {
                    #region InsertToDb
                    orderInfo.status = status;
                    orderInfo.realvalue = tranAmt;
                    orderInfo.msg = msg;
                    execResult = InsertToDb(supplierId
                        , orderInfo
                        , supplierOrderId
                        , status
                        , opstate
                        , msg, tranAmt, suppSettleAmt);
                    #endregion
                }

                if (execResult.ErrCode == 0)
                {
                    Utility.ReturnToMerchant(orderInfo);
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Write("system error");
                }
            }
        }
        #endregion

        #region SuppNotify
        /// <summary>
        /// 处理网银支付异步通知返回订单信息，执行扣量等操作,处理完毕会自动输出succflag或failflag给接口供应商平台
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="orderId"></param>
        /// <param name="supplierOrderId"></param>
        /// <param name="status"></param>
        /// <param name="opstate"></param>
        /// <param name="msg"></param>
        /// <param name="tranAmt">结算金额</param>
        ///  <param name="suppSettleAmt">供应商给平台的费率</param>
        /// <param name="succflag"></param>
        /// <param name="failflag"></param>
        public static void SuppNotify(int supplierId, string orderId, string supplierOrderId, int status,
            string opstate, string msg, decimal tranAmt, decimal suppSettleAmt,
            string succflag, string failflag)
        {
            var execResult = new FunExecResult()
            {
                ErrCode = 0,
                ErrMsg = ""
            };

            var orderInfo = Cache.WebCache.GetCacheService().RetrieveObject(orderId) as OrderBankInfo;
            if (orderInfo == null)
            {
                orderInfo = BLL.Order.Bank.Factory.Instance.GetModelByOrderId(orderId);
            }
            if (orderInfo != null)
            {
                bool insertToDb = orderInfo.status == 1;
                if (insertToDb)
                {
                    #region InsertToDb
                    orderInfo.status = status;
                    orderInfo.realvalue = tranAmt;
                    orderInfo.msg = msg;

                    execResult = InsertToDb(supplierId
                        , orderInfo
                        , supplierOrderId
                        , status
                        , opstate
                        , msg, tranAmt, suppSettleAmt);

                    //Cache.WebCache.GetCacheService().RemoveObject(orderId);
                    #endregion
                }
                if (orderInfo.status != 1)
                {
                    APINotification.SynchronousNotifyX(orderInfo);
                }
            }

            if (execResult.ErrCode == 0)
            {
                if (!string.IsNullOrEmpty(succflag))
                {
                    System.Web.HttpContext.Current.Response.Write(succflag);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(failflag))
                {
                    System.Web.HttpContext.Current.Response.Write(failflag);
                }
            }
        }
        #endregion

        #region InsertToDb
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierId">接口供应商ID</param>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="supplierOrderId"></param>
        /// <param name="status"></param>
        /// <param name="opstate">状态码</param>
        /// <param name="msg">返回信息</param>
        /// <param name="tranAmt">结算金额</param>
        /// <param name="suppSettleAmt">供应商给平台的结算价</param>
        /// <returns></returns>
        public static FunExecResult InsertToDb(int supplierId
            , OrderBankInfo orderInfo
            , string supplierOrderId
            , int status
            , string opstate
            , string msg
            , decimal tranAmt
            , decimal suppSettleAmt)
        {
            var result = new FunExecResult()
            {
                ErrCode = 0,
                ErrMsg = ""
            };

            string cacheKey = "OrderBankComplete" + orderInfo.orderid + orderInfo.supplierId.ToString(CultureInfo.InvariantCulture);

            object flag = Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);
            if (flag != null)
                return result;

            try
            {
                orderInfo.supplierId = supplierId;
                orderInfo.status = status;
                orderInfo.deduct = 0;

                #region 扣量
                if (BLL.Sys.TransactionSettings.OpenDeduct && status == 2)
                {
                    Model.User.UserInfo userInfo = BLL.User.Factory.GetCacheUserBaseInfo(orderInfo.userid);
                    if (userInfo != null)
                    {
                        //0x3e8 1000 扣率 扣率 = 被扣单的机率是多少
                        if (new Random(Guid.NewGuid().GetHashCode()).Next(1, 1000) <= userInfo.CPSDrate)
                        {
                            orderInfo.deduct = 1;
                        }
                    }
                }
                #endregion

                orderInfo.realvalue = tranAmt;
                orderInfo.supplierId = supplierId;
                orderInfo.completetime = DateTime.Now;

                orderInfo.payRate = 0M;
                orderInfo.payAmt = 0M;
                orderInfo.promRate = 0M;
                orderInfo.promAmt = 0M;
                orderInfo.supplierRate = 0M;
                orderInfo.supplierAmt = 0M;
                orderInfo.profits = 0M;
                
                if (status == 2)
                {
                    if (orderInfo.deduct == 0)
                    {
                        #region 计算费率
                        if (orderInfo.payRate <= 0M)
                        {
                            orderInfo.payRate = BLL.Finance.PayRate.Instance.GetUserPayRate(orderInfo.userid,
                                orderInfo.typeId);
                        }
                        orderInfo.payAmt = orderInfo.payRate * tranAmt;


                        if (orderInfo.agentId > 0)
                        {
                            //代理费率
                            orderInfo.promRate = BLL.Finance.PayRate.Instance.GetUserPayRate(orderInfo.agentId,
                                orderInfo.typeId);
                            //代理金额
                            orderInfo.promAmt = (orderInfo.promRate - orderInfo.payRate) * tranAmt;

                            if (orderInfo.promAmt < 0)
                                orderInfo.promAmt = 0;
                        }
                        #endregion
                    }
                    if (orderInfo.supplierRate <= 0M)
                    {
                        #region 计算平台费率
                        var chanelInfo = BLL.Channel.Channel.GetModel(orderInfo.paymodeId, orderInfo.userid, false);
                        if (chanelInfo != null)
                        {
                            orderInfo.supplierRate = chanelInfo.supprate;
                        }
                        #endregion
                    }

                    orderInfo.supplierAmt = suppSettleAmt;
                    if (suppSettleAmt > 0 && tranAmt > 0)
                        orderInfo.supplierRate = suppSettleAmt / tranAmt;

                    //利润
                    orderInfo.profits = orderInfo.supplierAmt - orderInfo.payAmt - orderInfo.promAmt;
                }

                orderInfo.opstate = opstate;
                orderInfo.msg = msg;

                orderInfo.supplierOrder = "";
                if (!string.IsNullOrEmpty(supplierOrderId))
                    orderInfo.supplierOrder = supplierOrderId;

                BLLBank.Complete(orderInfo);

                result.ErrCode = 0;
                result.ErrMsg = "success";

                Cache.WebCache.GetCacheService().AddObject(cacheKey, status, 5);

            }
            catch (System.Threading.ThreadAbortException ex)
            {
                result.ErrCode = 98;
                result.ErrMsg = ex.Message;
                //
            }
            catch (Exception ex)
            {
                result.ErrCode = 99;
                result.ErrMsg = ex.Message;
            }

            return result;
        }
        #endregion

        #region Reconcilie
        /// <summary>
        /// 
        /// </summary>
        /// <param name="suppId"></param>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        public void Reconcilie(int suppId, string orderid, decimal orderAmt)
        {
            switch (suppId)
            {
                case (int)SupplierCode.Baofoo:
                    Baofoo.Bank.Instance.Reconcilie(orderid, orderAmt);
                    break;
            }
        }
        #endregion
    }
}
