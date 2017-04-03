using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using viviapi.BLL.Order.Bank;
using viviapi.Model.Order;
using viviLib.ExceptionHandling;

namespace viviapi.SysInterface.Bank
{
    public class APINotification
    {
        private static readonly viviapi.IMessaging.IOrderBankNotify NotifyQueue = MessagingFactory.QueueAccess.OrderBankNotify();
        private static readonly viviapi.IMessaging.IOrderBankNotify BanknotifyQueue = MessagingFactory.QueueAccess.OrderBankNotify();

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
                        , 60*1000);

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
                    var notify = new Model.Order.Bank.BankNotify()
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

                    BankNotify.Instance.Insert(notify);

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

        public static void SynchronousNotifyX(OrderBankInfo orderInfo)
        {
            if (orderInfo == null)
                return;

            byte exists = BankNotify.Instance.Exists(orderInfo.orderid);
            if (exists == 0 || exists == 1)
            {
                string notifyUrl = Utility.GetBankBackUrl(orderInfo, true);
                if (!string.IsNullOrEmpty(notifyUrl))
                {
                    bool isOK = Process(orderInfo.version,orderInfo.orderid, notifyUrl);
                    if (isOK == false)
                    {
                        BanknotifyQueue.Send(orderInfo);
                    }
                }
            }
            else if (exists == 3)
            {
                BanknotifyQueue.Send(orderInfo);
            }
        }

        #region SynchronousNotify
        /// <summary>
        /// 同步补发
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns></returns>
        public static string SynchronousNotify(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return string.Empty;

            var orderInfo = Factory.Instance.GetModelByOrderId(orderId);
            if (orderInfo == null)
                return string.Empty;

            //   string successFlag = Utility.Successflag(orderInfo.version);

            string notifyUrl = Utility.GetBankBackUrl(orderInfo, true);

            string callback = string.Empty;
            string status = string.Empty;
            string message = string.Empty;
            string statusCode = string.Empty;
            string statusDesc = string.Empty;
            bool isOk = false;
            int notifystat = 4;

            try
            {
                if (viviLib.Text.PageValidate.IsUrl(notifyUrl))
                {
                    try
                    {
                        callback = viviLib.Web.WebClientHelper.GetString(notifyUrl
                        , string.Empty
                        , "GET"
                        , Encoding.GetEncoding("GB2312")
                        , 100000);

                        isOk = Utility.CheckCallBackIsSuccess(orderInfo.version, callback);// callback.StartsWith(successFlag) || callback.ToLower().StartsWith(successFlag);

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
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.HandleException(ex);
                    }

                    if (orderInfo.notifystat != 2)
                    {
                        var notify = new Model.Order.Bank.BankNotify()
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

                        BLL.Order.Bank.BankNotify.Instance.Insert(notify);
                    }
                }
                else
                {
                    callback = "返回地址不正确！";
                }
            }
            catch (Exception ex)
            {

            }


            return callback;
        }
        #endregion

        #region AsynchronousNotify
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        public static void AsynchronousNotify(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return;

            var orderInfo = Factory.Instance.GetModelByOrderId(orderId);
            if (orderInfo == null || orderInfo.status == 1)
                return;

            string notifyUrl = Utility.GetBankBackUrl(orderInfo, true);

            Common.AsynchronousNotify(1, orderInfo, notifyUrl);
        }

        public static void AsynchronousNotify(OrderBankInfo orderInfo)
        {
            if (orderInfo == null || orderInfo.status == 1)
                return;

            string notifyUrl = Utility.GetBankBackUrl(orderInfo, true);

            Common.AsynchronousNotify(1, orderInfo, notifyUrl);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public OrderBankInfo ReceiveFromQueue(int timeout)
        {
            return NotifyQueue.Receive(timeout);
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
                var notify = (OrderNotify)stateInfo;

                string notifyUrl = notify.NotifyUrl;

                if (string.IsNullOrEmpty(notifyUrl))
                {
                    notifyUrl = Utility.GetBankBackUrl(notify.OrderInfo, true);

                    notify.NotifyUrl = notifyUrl;
                }

                if (string.IsNullOrEmpty(notifyUrl))
                {
                    notify.Tmr.Dispose();
                    notify.Tmr = null;
                }
                else
                {
                    notify.NotifyCount++;

                    if (notify.Tmr != null)
                    {
                        #region
                        switch (notify.NotifyCount)
                        {
                            case 1:
                                (notify.Tmr).Change(TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(200));
                                break;

                            case 2:
                                (notify.Tmr).Change(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(200));//1分钟
                                break;

                            case 3:
                                (notify.Tmr).Change(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(200));//2分钟
                                break;
                            case 4:
                                (notify.Tmr).Change(TimeSpan.FromMinutes(10), TimeSpan.FromSeconds(200));//5分钟
                                break;
                            case 5:
                                (notify.Tmr).Change(TimeSpan.FromMinutes(20), TimeSpan.FromSeconds(200));//10分钟
                                break;
                            case 6:
                                (notify.Tmr).Change(TimeSpan.FromMinutes(20), TimeSpan.FromSeconds(200));//20分钟
                                break;
                            case 7:
                                (notify.Tmr).Change(TimeSpan.FromMinutes(30), TimeSpan.FromSeconds(200));//30分钟
                                break;
                            case 8:
                                (notify.Tmr).Change(TimeSpan.FromMinutes(60), TimeSpan.FromSeconds(200));//1小时
                                break;
                            case 9:
                                (notify.Tmr).Change(TimeSpan.FromMinutes(120), TimeSpan.FromSeconds(200));//2小时
                                break;
                            default:
                                (notify.Tmr).Change(TimeSpan.FromMinutes(240), TimeSpan.FromSeconds(200));//4小时
                                break;
                        }
                        #endregion
                    }

                    if (notify.NotifyCount > 10)
                    {
                        if (notify.Tmr != null)
                        {
                            notify.Tmr.Dispose();
                            notify.Tmr = null;
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

        public void StartTask(OrderNotify info)
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
        private void Process(OrderNotify info)
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

                    isOk = Utility.CheckCallBackIsSuccess(info.OrderInfo.version, callback);
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

                var notifyInfo = new Model.Order.Bank.BankNotify()
                {
                    orderid = info.OrderInfo.orderid,
                    status = status,
                    message = message,
                    httpStatusCode = statusCode,
                    StatusDescription = statusDesc,
                    againNotifyUrl = notifyUrl,
                    notifystat = notifystat,
                    notifycontext = callback,
                    notifytime = DateTime.Now
                };

                BankNotify.Instance.Insert(notifyInfo);

                if (isOk)
                {
                    if (info.Tmr != null)
                    {
                        info.Tmr.Dispose();
                        info.Tmr = null;
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }

        }
        #endregion

        //#region NotifyCheckStatus
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="stateInfo"></param>
        //public void NotifyCheckStatusX(Object stateInfo)
        //{
        //    try
        //    {
        //        var notify = (OrderNotifyX)stateInfo;

        //        string notifyUrl = notify.NotifyUrl;

        //        //if (string.IsNullOrEmpty(notifyUrl))
        //        //{
        //        //    notifyUrl = Utility.GetBankBackUrl(notify.OrderInfo, true);
        //        //    notify.NotifyUrl = notifyUrl;
        //        //}

        //        if (string.IsNullOrEmpty(notifyUrl))
        //        {
        //            notify.Tmr.Dispose();
        //            notify.Tmr = null;
        //        }
        //        else
        //        {
        //            notify.OrderInfo.notifycount++;

        //            if (notify.Tmr != null)
        //            {
        //                switch (notify.OrderInfo.notifycount)
        //                {
        //                    case 1:
        //                        (notify.Tmr).Change(TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(200));
        //                        break;
        //                    case 2:
        //                        (notify.Tmr).Change(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(200));//1分钟
        //                        break;
        //                    case 3:
        //                        (notify.Tmr).Change(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(200));//2分钟
        //                        break;
        //                    case 4:
        //                        (notify.Tmr).Change(TimeSpan.FromMinutes(10), TimeSpan.FromSeconds(200));//5分钟
        //                        break;
        //                    case 5:
        //                        (notify.Tmr).Change(TimeSpan.FromMinutes(20), TimeSpan.FromSeconds(200));//10分钟
        //                        break;
        //                    case 6:
        //                        (notify.Tmr).Change(TimeSpan.FromMinutes(20), TimeSpan.FromSeconds(200));//20分钟
        //                        break;
        //                    case 7:
        //                        (notify.Tmr).Change(TimeSpan.FromMinutes(30), TimeSpan.FromSeconds(200));//30分钟
        //                        break;
        //                    case 8:
        //                        (notify.Tmr).Change(TimeSpan.FromMinutes(60), TimeSpan.FromSeconds(200));//1小时
        //                        break;
        //                    case 9:
        //                        (notify.Tmr).Change(TimeSpan.FromMinutes(120), TimeSpan.FromSeconds(200));//2小时
        //                        break;
        //                    default:
        //                        (notify.Tmr).Change(TimeSpan.FromMinutes(240), TimeSpan.FromSeconds(200));//4小时
        //                        break;
        //                }
        //            }

        //            string callback = string.Empty;
        //            string status = string.Empty;
        //            string message = string.Empty;
        //            string statusCode = string.Empty;
        //            string statusDesc = string.Empty;

        //            bool isOk = false;


        //            try
        //            {
        //                callback = viviLib.Web.WebClientHelper.GetString(notifyUrl, string.Empty, "GET", Encoding.GetEncoding("GB2312"), 100000);

        //                isOk = Utility.CheckCallBackIsSuccess(notify.OrderInfo.version, callback);
        //            }
        //            catch (WebException e)
        //            {
        //                message = e.Message;
        //                status = e.Status.ToString();

        //                if (e.Status == WebExceptionStatus.ProtocolError)
        //                {
        //                    statusCode = ((HttpWebResponse)e.Response).StatusCode.ToString();
        //                    statusDesc = ((HttpWebResponse)e.Response).StatusDescription;
        //                }
        //            }

        //            if (notify.OrderInfo.notifycount <= 10)// || callback.ToLower() == SuccessFlag)//
        //            {
        //                //bool isOk = Utility.CheckCallBackIsSuccess(notify.OrderInfo.version, callback);// callback.StartsWith(successFlag) || callback.ToLower().StartsWith(successFlag);
        //                int notifystat = isOk ? 2 : 4;

        //                var notifyInfo = new Model.Order.Bank.BankNotify()
        //                {
        //                    orderid = notify.OrderInfo.orderid,
        //                    status = status,
        //                    message = message,
        //                    httpStatusCode = statusCode,
        //                    StatusDescription = statusDesc,
        //                    againNotifyUrl = notifyUrl,
        //                    notifystat = notifystat,
        //                    notifycontext = callback,
        //                    notifytime = DateTime.Now
        //                };

        //                BLL.Order.Bank.BankNotify.Instance.Insert(notifyInfo);
        //            }

        //            if (isOk || notify.OrderInfo.notifycount >= 5)
        //            {
        //                if (notify.Tmr != null)
        //                {
        //                    notify.Tmr.Dispose();
        //                    notify.Tmr = null;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandler.HandleException(ex);
        //    }
        //}
        //#endregion
    }

    
}
