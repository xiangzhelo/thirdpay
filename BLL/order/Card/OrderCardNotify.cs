using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviapi.Model.Order;
using System.Net;
using viviLib;

namespace viviapi.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderCardNotify
    {
        //public static string SuccessFlag //= BLL.Sys.Constant.OrderNotifySuccessFlag;
        private static readonly viviapi.IMessaging.IOrderCardNotify notifyQueue = MessagingFactory.QueueAccess.OrderCardNotify();

        public void DoNotify(string orderId)
        {
            OrderCard ordercard = new OrderCard();

            OrderCardInfo orderinfo = ordercard.GetModelByOrderId(orderId);
            DoNotify(orderinfo);
        }

        public void DoNotify(OrderCardInfo order)
        {
            BLL.OrderNotifyBase.AsynchronousNotify(2, order);
        }

        /// <summary>
        /// 
        /// </summary>
        public OrderCardInfo ReceiveFromQueue(int timeout)
        {
            return notifyQueue.Receive(timeout);
        }

        

        #region NotifyCheckStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateInfo"></param>
        public void NotifyCheckStatus(Object stateInfo)
        {
            try
            {
                OrderCard bll = new OrderCard();
                viviapi.Model.Order.OrderCardNotify _notify = (viviapi.Model.Order.OrderCardNotify)stateInfo;
                string notifyUrl = "";// bll.GetCallBackUrl(_notify.orderInfo);
                string SuccessFlag = Utility.Successflag(_notify.orderInfo.version);

                if (string.IsNullOrEmpty(notifyUrl))
                {
                    _notify.tmr.Dispose();
                    _notify.tmr = null;
                }
                else
                {
                    bool timerflag = false;
                    string callback = string.Empty;

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
                    try { callback = viviLib.Web.WebClientHelper.GetString(notifyUrl, string.Empty, "GET", System.Text.Encoding.GetEncoding("GB2312"), 100000); }
                    catch { }
                 
                    if (_notify.orderInfo.notifycount <= 10)//.notifycount == 1 || callback.ToLower() == SuccessFlag)
                    {
                        bool isOk = (callback.StartsWith(SuccessFlag) || callback.ToLower().StartsWith(SuccessFlag));
                        _notify.orderInfo.notifystat = isOk ? 2 : 4;
                        _notify.orderInfo.againNotifyUrl = notifyUrl;
                        _notify.orderInfo.notifycontext = callback;
                        _notify.orderInfo.notifytime = DateTime.Now;
                        bll.UpdateNotifyInfo(_notify.orderInfo);
                    }
                    if (callback.ToLower() == SuccessFlag || _notify.orderInfo.notifycount >= 10)
                    {
                        if (_notify != null)
                        {
                            _notify.tmr.Dispose();
                            _notify.tmr = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }
        }
        #endregion

    }
}
