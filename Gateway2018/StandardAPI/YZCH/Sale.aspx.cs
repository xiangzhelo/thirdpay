using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Channel;
using viviapi.BLL.Order.Card;
using viviapi.BLL.Sys;
using viviapi.BLL.User;
using viviapi.ETAPI.Common;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviapi.Model.Order.Card;
using viviapi.Model.supplier;
using viviapi.Model.User;
using viviapi.SysConfig;
using viviapi.SysInterface.Card;
using viviapi.SysInterface.Card.Card70;
using viviapi.WebComponents.GateWay;
using viviLib.ExceptionHandling;
using viviLib.Web;
using CardInfo = viviapi.SysInterface.Card.Card70.CardInfo;
using Factory = viviapi.BLL.Order.Card.Factory;
using Utility = viviapi.SysInterface.Card.Card70.Utility;

namespace viviAPI.Gateway2018.StandardAPI.YZCH
{
    public partial class Sale : CardTransBase
    {
        protected OrderCard OrderBll = new OrderCard();

        #region 页面处理
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Process(HttpContext.Current);
        }
        #endregion

        #region Process
        /// <summary>
        /// 
        /// </summary>
        private void Process(HttpContext context)
        {
            var apiCardInfo = new CardInfo(context);

            var result = new SyncResult { returnorderid = apiCardInfo.orderid };

            string retCode = Utility.CheckParameter(apiCardInfo);

            if (retCode == "1")
            {
                string sysOrderId = Factory.Instance.GenerateOrderId(OrderPrefix);

                var orderInfo = InitOrder(sysOrderId, apiCardInfo);
                if (orderInfo == null)
                {
                    apiCardInfo.Msg = "初始化订单失败,系统繁忙";
                    retCode = "12";
                }
                else
                {
                    var suppResponse = new CardSynchCallBack();

                    if (apiCardInfo.ProcessMode == 1)
                    {
                        #region 通过接口
                        var supp = (SupplierCode)apiCardInfo.SupplierId;

                        suppResponse = OrderCardUtils.SynchSubmit(supp
                               , sysOrderId
                               , apiCardInfo.TypeId
                               , apiCardInfo.CardNo
                               , apiCardInfo.CardPwd
                               , apiCardInfo.OrderAmt
                               , string.Empty
                               , 1);

                        if (suppResponse.SummitStatus == 0)
                        {
                            retCode = Utility.ConvertSynchronousErrorCode(supp, suppResponse.SuppErrorCode);

                            string viewMsg = Utility.GetMessageByCode(retCode);

                            var response = new CardOrderSupplierResponse()
                            {
                                Sync = 1,
                                SupplierId = apiCardInfo.SupplierId,
                                SuppTransNo = suppResponse.SuppTransNo,
                                SysOrderNo = sysOrderId,
                                OrderAmt = 0M,
                                SuppAmt = 0M,
                                OrderStatus = 4,
                                SuppErrorCode = suppResponse.SuppErrorCode,
                                SuppErrorMsg = suppResponse.SuppErrorMsg,
                                Opstate = retCode,
                                ViewMsg = viewMsg,
                                Method = 1
                            };

                            OrderCardUtils.FinishForSync(orderInfo, response);

                            apiCardInfo.Msg = "提交失败，errCode:" + suppResponse.SuppErrorCode + " errMsg:" + suppResponse.SuppErrorMsg;
                        }
                        else
                        {
                            apiCardInfo.Msg = "提卡成功，等待处理结果";
                        }
                        #endregion
                    }
                    else
                    {
                        #region 系统自已处理
                        apiCardInfo.SupplierId = 0;
                        suppResponse.SuppTransNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        suppResponse.OrderStatus = 2;
                        suppResponse.SuppErrorMsg = "支付成功";
                        suppResponse.SuppErrorCode = "1";
                        suppResponse.SuccAmt = apiCardInfo.OrderAmt;
                        #endregion
                    }

                    if (apiCardInfo.ProcessMode == 2 || suppResponse.OrderStatus == 2)
                    {
                        #region 系统自处理
                        var resInfo = new CardProcessResultInfo
                        {
                            supplierId = 0,
                            orderid = sysOrderId,
                            supplierOrder = suppResponse.SuppTransNo,
                            status = 2,
                            opstate = "1",
                            msg = suppResponse.SuppErrorMsg,
                            userViewMsg = suppResponse.SuppErrorMsg,
                            tranAMT = suppResponse.SuccAmt,
                            suppAmt = 0M,
                            errtype = suppResponse.SuppErrorCode,
                            method = apiCardInfo.ProcessMode,
                            count = 0
                        };
                        apiCardInfo.Msg = "提卡成功，等待处理结果";
                        var process = new SystemProcessCard();

                        var tmr = new System.Threading.Timer(process.Process, resInfo, 1000, 0);
                        resInfo.tmr = tmr;
                        #endregion
                    }
                }
            }

            if (retCode != "1")
            {
                #region 记录日志
                if (this.DebuglogOpen)
                {
                    var debugInfo = new viviapi.Model.Sys.debuginfo
                    {
                        userid = apiCardInfo.UserId,
                        addtime = DateTime.Now,
                        bugtype = viviapi.Model.Sys.debugtypeenum.卡类订单,
                        errorcode = retCode,
                        errorinfo = apiCardInfo.Msg,
                        userorder = apiCardInfo.orderid,
                        url = context.Request.RawUrl,
                        detail = ""

                    };
                    Debuglog.Insert(debugInfo);
                }
                #endregion
            }

            result.returncode = retCode;


            string responseText = Utility.GetResponseText(result, apiCardInfo.APIkey);
            context.Response.Write(responseText);
        }
        #endregion

        #region 初始化订单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        OrderCardInfo InitOrder(string orderid, CardInfo info)
        {
            try
            {
                var order = new OrderCardInfo
                {
                    ordertype = 1,
                    orderid = orderid,
                    userid = info.UserId,
                    userorder = info.orderid,
                    typeId = info.TypeId,
                    cardType = info.CardType,
                    cardNo = info.CardNo,
                    cardPwd = info.CardPwd,
                    paymodeId = info.ChanelNo,
                    refervalue = info.OrderAmt,
                    faceValue = 0M,
                    attach = info.ext,
                    referUrl =
                        HttpContext.Current.Request.UrlReferrer != null
                            ? HttpContext.Current.Request.UrlReferrer.ToString()
                            : string.Empty,
                    clientip = ServerVariables.TrueIP,
                    addtime = DateTime.Now,
                    completetime = DateTime.Now,
                    notifycontext = string.Empty,
                    notifycount = 0,
                    notifystat = 0,
                    notifyurl = info.url,
                    payRate = 0M,
                    supplierId = info.SupplierId,
                    supplierOrder = string.Empty,
                    server = RuntimeSetting.ServerId,
                    manageId = info.ManageId,
                    cardnum = 1,
                    resultcode = "",
                    ismulticard = 0,
                    status = 1,
                    ovalue = string.Empty,
                    opstate = "",
                    msg = info.Msg,
                    userViewMsg = info.Msg,
                    errtype = "",
                    Desc = info.Msg,
                    version = info.Version,
                    withhold_type = 0,
                    makeup = (byte)(info.ProcessMode == 2 ? 1 : 0)
                };
                order.cus_field1 = info.typeid;
                order.cus_field2 = info.productid;

                if (order.manageId <= 0)
                {
                    order.agentId = viviapi.BLL.User.Factory.GetPromID(info.UserId);
                }

                if (info.ProcessMode == 1)
                {
                    viviapi.Cache.WebCache.GetCacheService().AddObject(order.orderid, order, ExpiresTime);
                }

                OrderBll.Insert(order);

                return order;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion
    }
}
