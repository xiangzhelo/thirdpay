using System;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviapi.Model.Order;

namespace viviapi.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderBankNotify
    {
        //public static string SuccessFlag = BLL.Sys.Constant.OrderNotifySuccessFlag;
        private static readonly viviapi.IMessaging.IOrderBankNotify notifyQueue = MessagingFactory.QueueAccess.OrderBankNotify();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public void DoNotify(OrderBankInfo order)
        {
            if (order == null)
                return;

            BLL.OrderNotifyBase.AsynchronousNotify(1, order);
        }       

        /// <summary>
        /// 
        /// </summary>
        public OrderBankInfo ReceiveFromQueue(int timeout)
        {
            return notifyQueue.Receive(timeout);
        }

        //#region SynchronousNotify
        ///// <summary>
        ///// 同步补发
        ///// </summary>
        ///// <param name="orderId"></param>
        ///// <returns></returns>
        //public string SynchronousNotify(string orderId)
        //{
        //    if (string.IsNullOrEmpty(orderId))
        //        return string.Empty;

        //    //OrderBank _orderBank = new OrderBank();
        //    OrderBankInfo orderInfo = BLL.Order.Bank.Factory.Instance.GetModelByOrderId(orderId);
        //    if (orderInfo == null)
        //        return string.Empty;

        //    string SuccessFlag = SystemApiHelper.Successflag(orderInfo.version);
        //    string notifyUrl = _orderBank.GetCallBackUrl(orderInfo);
        //    string callback = string.Empty;
        //    try
        //    {
        //        if (viviLib.Text.PageValidate.IsUrl(notifyUrl))
        //        {
        //            callback = viviLib.Web.WebClientHelper.GetString(notifyUrl
        //                , string.Empty
        //                , "GET"
        //                , System.Text.Encoding.GetEncoding("GB2312")
        //                , 100000);

        //            bool isOk = callback.StartsWith(SuccessFlag) || callback.ToLower().StartsWith(SuccessFlag);

        //            if (isOk && orderInfo.notifystat != 2)
        //            {
        //                orderInfo.notifystat = 2;
        //                orderInfo.againNotifyUrl = notifyUrl;
        //                orderInfo.notifycontext = callback;
        //                _orderBank.UpdateNotifyInfo(orderInfo);
        //            }
        //        }
        //        else
        //        {
        //            callback = "返回地址不正确！";
        //        }
        //    }
        //    catch { }

        //    return callback;
        //}
        //#endregion

        #region NotifyCheckStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateInfo"></param>
        public void NotifyCheckStatus(Object stateInfo)
        {
            try
            {
                OrderBank _orderBank = new OrderBank();

                viviapi.Model.Order.OrderNotify _notify = (viviapi.Model.Order.OrderNotify)stateInfo;
                string notifyUrl = ""; //_orderBank.GetCallBackUrl(_notify.orderInfo);

                string SuccessFlag = SystemApiHelper.Successflag(_notify.orderInfo.version);

                if (string.IsNullOrEmpty(notifyUrl))
                {
                    _notify.tmr.Dispose();
                    _notify.tmr = null;
                }
                else
                {
                    bool timerflag = false;
                    _notify.orderInfo.notifycount++;

                    if (_notify.tmr != null)
                    {
                        switch (_notify.orderInfo.notifycount)
                        {
                            case 1:
                                timerflag = (_notify.tmr).Change(TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(200));
                                break;
                            case 2:
                                timerflag = (_notify.tmr).Change(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(200));//1分钟
                                break;
                            case 3:
                                timerflag = (_notify.tmr).Change(TimeSpan.FromMinutes(2), TimeSpan.FromSeconds(200));//2分钟
                                break;
                            case 4:
                                timerflag = (_notify.tmr).Change(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(200));//5分钟
                                break;
                            case 5:
                                timerflag = (_notify.tmr).Change(TimeSpan.FromMinutes(10), TimeSpan.FromSeconds(200));//10分钟
                                break;
                            case 6:
                                timerflag = (_notify.tmr).Change(TimeSpan.FromMinutes(20), TimeSpan.FromSeconds(200));//20分钟
                                break;
                            case 7:
                                timerflag = (_notify.tmr).Change(TimeSpan.FromMinutes(30), TimeSpan.FromSeconds(200));//30分钟
                                break;
                            case 8:
                                timerflag = (_notify.tmr).Change(TimeSpan.FromMinutes(60), TimeSpan.FromSeconds(200));//1小时
                                break;
                            case 9:
                                timerflag = (_notify.tmr).Change(TimeSpan.FromMinutes(120), TimeSpan.FromSeconds(200));//2小时
                                break;
                            case 10:
                                timerflag = (_notify.tmr).Change(TimeSpan.FromMinutes(240), TimeSpan.FromSeconds(200));//4小时
                                break;
                        }
                    }
                   
                    string callback = string.Empty;

                    try { callback = viviLib.Web.WebClientHelper.GetString(notifyUrl, string.Empty, "GET", System.Text.Encoding.GetEncoding("GB2312"), 100000); }
                    catch (WebException e) { callback = e.Status.ToString() + e.Message; }

                    if (_notify.orderInfo.notifycount <= 10)// || callback.ToLower() == SuccessFlag)//
                    {
                        bool isOk = callback.StartsWith(SuccessFlag) || callback.ToLower().StartsWith(SuccessFlag);
                        _notify.orderInfo.notifystat = isOk ? 2 : 4;
                        _notify.orderInfo.againNotifyUrl = notifyUrl;
                        _notify.orderInfo.notifycontext = callback;
                        _notify.orderInfo.notifytime = DateTime.Now;
                        _orderBank.UpdateNotifyInfo(_notify.orderInfo);
                    }

                    if (callback.ToLower() == SuccessFlag || _notify.orderInfo.notifycount >= 10)
                    {
                        if (_notify.tmr != null)
                        {
                            _notify.tmr.Dispose();
                            _notify.tmr = null;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }
        }
        #endregion
    }
}
