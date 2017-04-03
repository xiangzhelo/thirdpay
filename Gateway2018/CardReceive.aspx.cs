﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
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
using viviapi.SysInterface.Card.MyAPI;
using viviLib.ExceptionHandling;
using viviLib.Web;
using Factory = viviapi.BLL.Order.Card.Factory;
using Utility = viviapi.SysInterface.Card.Utility;
using System.Web.UI;
using System.Threading;

namespace viviAPI.Gateway2018
{
    public partial class CardReceive : Page
    {
        protected viviapi.BLL.OrderCard OrderBll = new viviapi.BLL.OrderCard();

        #region 系统配置
        /// <summary>
        /// 订单号前缀
        /// </summary>
        public string OrderPrefix
        {
            get
            {
                return TransactionSettings.OrderPrefix;
            }
        }

        /// <summary>
        /// 缓存超时时间
        /// </summary>
        public int ExpiresTime
        {
            get
            {
                return TransactionSettings.ExpiresTime;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        protected bool DebuglogOpen
        {
            get
            {
                return TransactionSettings.Debuglog;
            }
        }
        protected void PostCardToSupplier(object _context)
        {

            var context = (HttpContext)_context;
            string opstate = "1";
            CardInfoList cardInfoList = new CardInfoList(context);
            foreach (CardInfo apiCardInfo in cardInfoList)
            {
                opstate = viviapi.SysInterface.Card.MyAPI.Utility.CheckParameter(apiCardInfo, context);
                //if (opstate == "1")
                //{
                    string sysOrderId = Factory.Instance.GenerateOrderId(OrderPrefix);

                    var orderInfo = InitOrder(context, sysOrderId, apiCardInfo);
                    if (orderInfo == null)
                    {
                        apiCardInfo.Msg = "初始化订单失败,系统繁忙";
                        opstate = "13";
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
                            //订单没有提交成功（提交到接口商没有成功）
                            if (suppResponse.SummitStatus == 0)
                            {
                                //错误代码
                                opstate = viviapi.SysInterface.Card.MyAPI.Utility.ConvertSynchronousErrorCode(supp, suppResponse.SuppErrorCode);
                                //错误信息
                                string viewMsg = viviapi.SysInterface.Card.MyAPI.Utility.GetMessageByCode(opstate);

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
                                    SuppErrorMsg = string.IsNullOrEmpty(suppResponse.SuppErrorMsg) ? suppResponse.Message : suppResponse.SuppErrorMsg,
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

                        if (apiCardInfo.ProcessMode == 2
                            || suppResponse.OrderStatus == 2)
                        {
                            #region 系统自处理
                            var resInfo = new CardProcessResultInfo
                            {
                                supplierId = 0,
                                orderid = sysOrderId,
                                status = 2,
                                opstate = "0",
                                supplierOrder = suppResponse.SuppTransNo,
                                msg = suppResponse.SuppErrorMsg,
                                userViewMsg = suppResponse.SuppErrorMsg,
                                tranAMT = suppResponse.SuccAmt,
                                suppAmt = 0M,
                                errtype = "0",
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
               // }
                
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            CardInfoList cardInfoList = new CardInfoList(HttpContext.Current);
            var myCardInfo = new CardInfo(HttpContext.Current);

            string opstate = "1";
            Thread thread = new Thread(new ParameterizedThreadStart(PostCardToSupplier));
            //Thread thread = new Thread(new  (PostCardToSupplier));
            HttpContext context = new HttpContext(HttpContext.Current.Request, HttpContext.Current.Response);
            thread.Start(context);
            string retcode = string.Format("opstate={0}", opstate);
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(retcode);
            //}
            //参数没接收成功
            if (opstate != "1")
            {
                #region 记录日志
                if (this.DebuglogOpen)
                {
                    foreach (CardInfo apiCardInfo in cardInfoList)
                    {
                        var debugInfo = new viviapi.Model.Sys.debuginfo
                        {
                            userid = apiCardInfo.UserId,
                            addtime = DateTime.Now,
                            bugtype = viviapi.Model.Sys.debugtypeenum.卡类订单,
                            errorcode = opstate,
                            errorinfo = apiCardInfo.Msg,
                            userorder = apiCardInfo.orderid,
                            url = HttpContext.Current.Request.RawUrl,
                            detail = ""

                        };
                        Debuglog.Insert(debugInfo);
                    }
                }
                #endregion
            }

            
        }


        #region 初始化订单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        OrderCardInfo InitOrder(HttpContext _context,string orderid, CardInfo info)
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
                        _context.Request.UrlReferrer != null
                            ? _context.Request.UrlReferrer.ToString()
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
