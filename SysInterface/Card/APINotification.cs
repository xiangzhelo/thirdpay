
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Order.Card;
using viviapi.Model.Order;
using viviLib.ExceptionHandling;

namespace viviapi.SysInterface.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class APINotification
    {
        private static readonly IMessaging.IOrderCardNotify NotifyQueue = MessagingFactory.QueueAccess.OrderCardNotify();
        private static readonly IMessaging.IOrderCardNotifyX NotifyQueueX = MessagingFactory.QueueAccess.OrderCardNotifyX();

        #region Process
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="orderid"></param>
        /// <param name="notifyUrl"></param>
        /// <returns></returns>
        public static bool Process(string version, string orderid, string notifyUrl)
        {
            try
            {
                string callback = string.Empty;
                string status = string.Empty;
                string message = string.Empty;
                string statusCode = string.Empty;
                string statusDesc = string.Empty;
                bool isOk = false;
                int notifystat = 4;

                if (viviLib.Text.PageValidate.IsUrl(notifyUrl))
                {
                    try
                    {
                        callback = viviLib.Web.WebClientHelper.GetString(notifyUrl
                        , string.Empty
                        , "GET"
                        , Encoding.GetEncoding("GB2312")
                        , 60 * 1000);

                        isOk = Utility.CheckCallBackIsSuccess(version, callback);
                        if (isOk) notifystat = 2;
                    }
                    catch (WebException e)
                    {
                        message = e.Message;
                        status = e.Status.ToString();

                        if (e.Status == WebExceptionStatus.ProtocolError)
                        {
                            statusCode = ((HttpWebResponse)e.Response).StatusCode.ToString();
                            statusDesc = ((HttpWebResponse)e.Response).StatusDescription;
                        }
                        ExceptionHandler.HandleException(e);
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.HandleException(ex);
                    }
                    var notify = new Model.Order.Card.CardNotify()
                    {
                        orderid = orderid,
                        status = status,
                        message = message,
                        httpStatusCode = statusCode,
                        StatusDescription = statusDesc,
                        againNotifyUrl = notifyUrl,
                        notifystat = notifystat,
                        notifycontext = callback,
                        notifytime = DateTime.Now
                    };

                    CardNotify.Instance.Insert(notify);

                    return isOk;
                }
                else
                {
                    callback = "返回地址不正确！";
                }
                return false;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }

        }
        #endregion

        public static void SynchronousNotifyX(OrderCardInfo orderInfo)
        {
            if (orderInfo == null
                || orderInfo.status == 1)
                return;

            if (orderInfo.ismulticard == 0)
            {
                byte exists = CardNotify.Instance.Exists(orderInfo.orderid);
                if (exists == 0 || exists == 1)
                {
                    string notifyUrl = Utility.GetCardNotifyUrl(orderInfo);
                    if (!string.IsNullOrEmpty(notifyUrl))
                    {
                        bool isOK = Process(orderInfo.version, orderInfo.orderid, notifyUrl);
                        if (isOK == false)
                        {
                            NotifyQueue.Send(orderInfo);
                        }
                    }
                }
                else if (exists == 3)
                {
                    NotifyQueue.Send(orderInfo);
                }
            }
        }

        #region SynchronousNotify
        /// <summary>
        /// 补发
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static string SynchronousNotify(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return string.Empty;

            OrderCardInfo orderInfo = Factory.Instance.GetModelByOrderId(orderId);
            if (orderInfo == null)
                return string.Empty;

            string successFlag = Utility.Successflag(orderInfo.version);

            string notifyUrl = Utility.GetCardNotifyUrl(orderInfo);

            string callback = string.Empty;
            string status = string.Empty;
            string message = string.Empty;
            string statusCode = string.Empty;
            string statusDesc = string.Empty;
            bool isOk = false;

            int notifystat = 4;
            try
            {
                if (!string.IsNullOrEmpty(notifyUrl))
                {
                    callback = viviLib.Web.WebClientHelper.GetString(notifyUrl
                        , string.Empty
                        , "GET"
                        , System.Text.Encoding.GetEncoding("GB2312")
                        , 5 * 1000);//5s

                    isOk = Utility.CheckCallBackIsSuccess(orderInfo.version, callback);

                    if (isOk) notifystat = 2;
                }
            }
            catch (WebException e)
            {
                message = e.Message;
                status = e.Status.ToString();

                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    statusCode = ((HttpWebResponse)e.Response).StatusCode.ToString();
                    statusDesc = ((HttpWebResponse)e.Response).StatusDescription;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }

            if (orderInfo.notifystat != 2)
            {
                var notify = new Model.Order.Card.CardNotify()
                {
                    orderid = orderInfo.orderid,
                    status = status,
                    message = message,
                    httpStatusCode = statusCode,
                    StatusDescription = statusDesc,
                    againNotifyUrl = notifyUrl,
                    notifystat = notifystat,
                    notifycontext = callback,
                    notifytime = DateTime.Now
                };

                BLL.Order.Card.CardNotify.Instance.Insert(notify);
            }

            return callback;
        }
        #endregion

        #region DoAsynchronousNotify
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        public static void DoAsynchronousNotify(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return;

            OrderCardInfo orderInfo = Factory.Instance.GetModelByOrderId(orderId);

            DoAsynchronousNotify(orderInfo);
        }
        #endregion

        #region DoAsynchronousNotify
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderInfo"></param>
        public static void DoAsynchronousNotify(OrderCardInfo orderInfo)
        {
            if (orderInfo == null || orderInfo.status == 1)
                return;

            if (orderInfo.ismulticard == 0)
            {
                string notifyUrl = Utility.GetCardNotifyUrl(orderInfo);

                if (!string.IsNullOrEmpty(notifyUrl))
                    Common.AsynchronousNotify(2, orderInfo, notifyUrl);
            }
            else
            {
                viviapi.Model.Order.Card.OrderCardTotal orderTotal =
                    OrderCardTotal.Instance.GetModelByOrderId(orderInfo.Batno);

                if (orderTotal != null && orderTotal.status > 1)
                {
                    string notifyUrl = Utility.GetMultiCardNotifyUrl(orderTotal);

                    if (!string.IsNullOrEmpty(notifyUrl))
                        Common.AsynchronousNotify(4, orderTotal, notifyUrl);
                }
            }
        }
        #endregion

        #region ReceiveFromQueue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public OrderCardInfo ReceiveFromQueue(int timeout)
        {
            return NotifyQueue.Receive(timeout);
        }
        #endregion

        #region ReceiveFromQueue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public viviapi.Model.Order.Card.CardNotify ReceiveFromQueueX(int timeout)
        {
            return NotifyQueueX.Receive(timeout);
        }
        #endregion

        #region NotifyCheckStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateInfo"></param>
        public void NotifyCheckStatus(Object stateInfo)
        {
            try
            {
                var notify = (Model.Order.OrderCardNotify)stateInfo;
                string notifyUrl = notify.NotifyUrl;

                if (string.IsNullOrEmpty(notifyUrl))
                {
                    notifyUrl = Utility.GetCardNotifyUrl(notify.orderInfo);
                    notify.NotifyUrl = notifyUrl;
                }

                if (string.IsNullOrEmpty(notifyUrl))
                {
                    return;
                }

                if (string.IsNullOrEmpty(notifyUrl))
                {
                    notify.tmr.Dispose();
                    notify.tmr = null;
                }
                else
                {
                    notify.orderInfo.notifycount++;

                    if (notify.tmr != null)
                    {
                        switch (notify.orderInfo.notifycount)
                        {
                            #region
                            case 1:
                                (notify.tmr).Change(TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(200));
                                break;
                            case 2:
                                (notify.tmr).Change(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(200));//1分钟
                                break;
                            case 3:
                                (notify.tmr).Change(TimeSpan.FromMinutes(2), TimeSpan.FromSeconds(200));//2分钟
                                break;
                            case 4:
                                (notify.tmr).Change(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(200));//5分钟
                                break;
                            case 5:
                                (notify.tmr).Change(TimeSpan.FromMinutes(10), TimeSpan.FromSeconds(200));//10分钟
                                break;
                            case 6:
                                (notify.tmr).Change(TimeSpan.FromMinutes(20), TimeSpan.FromSeconds(200));//20分钟
                                break;
                            case 7:
                                (notify.tmr).Change(TimeSpan.FromMinutes(30), TimeSpan.FromSeconds(200));//30分钟
                                break;
                            case 8:
                                (notify.tmr).Change(TimeSpan.FromMinutes(60), TimeSpan.FromSeconds(200));//1小时
                                break;
                            case 9:
                                (notify.tmr).Change(TimeSpan.FromMinutes(120), TimeSpan.FromSeconds(200));//2小时
                                break;
                            default:
                                (notify.tmr).Change(TimeSpan.FromMinutes(240), TimeSpan.FromSeconds(200));//4小时
                                break;
                            #endregion
                        }
                    }

                    if (notify.orderInfo.notifycount >= 10)
                    {
                        if (notify.tmr != null)
                        {
                            notify.tmr.Dispose();
                            notify.tmr = null;
                        }
                        return;
                    }

                    StartTask(notify);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
        #endregion

        public void StartTask(OrderCardNotify info)
        {
            Thread threadHand1 = new Thread(() => Process(info));
            threadHand1.IsBackground = true;
            threadHand1.SetApartmentState(ApartmentState.STA);

            threadHand1.Start();
        }

        #region Process
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        private void Process(OrderCardNotify info)
        {
            try
            {
                string notifyUrl = info.NotifyUrl;

                if (string.IsNullOrEmpty(notifyUrl))
                    return;

                string callback = string.Empty;
                string status = string.Empty;
                string message = string.Empty;
                string statusCode = string.Empty;
                string statusDesc = string.Empty;

                bool isOk = false;

                try
                {
                    callback = viviLib.Web.WebClientHelper.GetString(notifyUrl
                        , string.Empty
                        , "GET"
                        , Encoding.GetEncoding("GB2312")
                        , 1 * 60 * 1000);

                    isOk = Utility.CheckCallBackIsSuccess(info.orderInfo.version, callback);

                }
                catch (WebException e)
                {
                    message = e.Message;
                    status = e.Status.ToString();

                    if (e.Status == WebExceptionStatus.ProtocolError)
                    {
                        statusCode = ((HttpWebResponse)e.Response).StatusCode.ToString();
                        statusDesc = ((HttpWebResponse)e.Response).StatusDescription;
                    }
                }
                catch (Exception e)
                {
                    message = e.Message;
                }

                int notifystat = isOk ? 2 : 4;

                var notifyInfo = new Model.Order.Card.CardNotify()
                {
                    orderid = info.orderInfo.orderid,
                    status = status,
                    message = message,
                    httpStatusCode = statusCode,
                    StatusDescription = statusDesc,
                    againNotifyUrl = notifyUrl,
                    notifystat = notifystat,
                    notifycontext = callback,
                    notifytime = DateTime.Now
                };

                CardNotify.Instance.Insert(notifyInfo);

                if (isOk)
                {
                    if (info.tmr != null)
                    {
                        info.tmr.Dispose();
                        info.tmr = null;
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }

        }
        #endregion
    }
}
