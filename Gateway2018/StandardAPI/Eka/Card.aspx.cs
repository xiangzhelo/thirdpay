using System;
using System.Web;
using viviapi.BLL;
using viviapi.BLL.Order.Card;
using viviapi.BLL.Sys;
using viviapi.ETAPI.Common;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviapi.Model.Order.Card;
using viviapi.Model.supplier;
using viviapi.Model.User;
using viviapi.SysConfig;
using viviapi.SysInterface.Card.Eka;
using viviapi.WebComponents.GateWay;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviAPI.Gateway2018.StandardAPI.Eka
{
    public partial class Card : CardTransBase
    {
        protected OrderCard OrderBll = new OrderCard();

        protected void Page_Load(object sender, EventArgs e)
        {
            Process(HttpContext.Current);
        }

        #region Process
        /// <summary>
        /// 
        /// </summary>
        private void Process(HttpContext context)
        {
            var apiCardInfo = new CardInfo(context);

            string opstate = Utility.CheckParameter(apiCardInfo);

            if (opstate == "0")
            {
                string sysOrderId = Factory.Instance.GenerateOrderId(OrderPrefix);

                var orderInfo = InitOrder(sysOrderId, apiCardInfo);
                if (orderInfo == null)
                {
                    apiCardInfo.Msg = "初始化订单失败,系统繁忙";
                    opstate = "-999";
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
                            opstate = Utility.ConvertSynchronousErrorCode(supp, suppResponse.SuppErrorCode);

                            string viewMsg = Utility.GetMessageByCode(opstate);

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
                                Opstate = opstate,
                                ViewMsg = viewMsg,
                                Method = 1
                            };

                            OrderCardUtils.FinishForSync(orderInfo, response);
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
                        suppResponse.SuppErrorCode = "0";
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
                            opstate = "0",
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

            if (opstate != "0")
            {
                #region 记录日志
                if (this.DebuglogOpen)
                {
                    var debugInfo = new viviapi.Model.Sys.debuginfo
                    {
                        userid = apiCardInfo.UserId,
                        addtime = DateTime.Now,
                        bugtype = viviapi.Model.Sys.debugtypeenum.卡类订单,
                        errorcode = opstate,
                        errorinfo = apiCardInfo.Msg,
                        userorder = apiCardInfo.orderid,
                        url = context.Request.RawUrl,
                        detail = ""

                    };
                    Debuglog.Insert(debugInfo);
                }
                #endregion
            }

            string retcode = string.Format("opstate={0}", opstate);
            context.Response.ContentType = "text/plain";
            context.Response.Write(retcode);
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
                    attach = info.attach,
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
                    notifyurl = info.callbackurl,
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
