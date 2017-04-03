using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using viviapi.Model.Order;
using viviapi.Model.Order.Card;
using viviapi.SysInterface.Bank;
using viviLib.ExceptionHandling;

namespace viviapi.SysInterface
{
    /// <summary>
    /// http://blog.csdn.net/zfrong/article/details/3742462
    /// </summary>
    public class Common
    {
        private static readonly viviapi.IMessaging.IOrderBankNotify BanknotifyQueue = MessagingFactory.QueueAccess.OrderBankNotify();
        private static readonly viviapi.IMessaging.IOrderCardNotify CardnotifyQueue = MessagingFactory.QueueAccess.OrderCardNotify();
        private static readonly viviapi.IMessaging.IOrderCardNotifyX CardnotifyQueueX = MessagingFactory.QueueAccess.OrderCardNotifyX();
        //private static readonly viviapi.IMessaging.IOrderSmsNotify smsnotifyQueue   = MessagingFactory.QueueAccess.OrderSmsNotify();

        const int BufferSize = 1024;
        const int DefaultTimeout = 2 * 60 * 1000; // 2 minutes timeout

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ordertype"></param>
        /// <param name="order"></param>
        /// <param name="notifyUrl"></param>
        public static void AsynchronousNotify(byte ordertype, object order, string notifyUrl)
        {
            try
            {
                var myHttpWebRequest = (HttpWebRequest)WebRequest.Create(notifyUrl);
                //myHttpWebRequest.Method =""
                //myHttpWebRequest.Method =""

                var myRequestState = new NotifyRequestState
                {
                    ordertype = ordertype,
                    order = order,
                    url = notifyUrl,
                    request = myHttpWebRequest
                };

                var result =
                  (IAsyncResult)myHttpWebRequest.BeginGetResponse(new AsyncCallback(RespCallback), myRequestState);

                ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, new WaitOrTimerCallback(TimeoutCallback), myRequestState, DefaultTimeout, true);
            }
            catch (WebException e)
            {
                InsertToDb(ordertype, order, notifyUrl, 1, "", false, e.Status.ToString(), e.Message, "", "");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        #region RespCallback
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asynchronousResult"></param>
        private static void RespCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                var myRequestState = (NotifyRequestState)asynchronousResult.AsyncState;

                HttpWebRequest myHttpWebRequest = myRequestState.request;
                myRequestState.response = (HttpWebResponse)myHttpWebRequest.EndGetResponse(asynchronousResult);

                Stream responseStream = myRequestState.response.GetResponseStream();
                myRequestState.streamResponse = responseStream;

                if (responseStream != null)
                {
                    IAsyncResult asynchronousInputRead = responseStream.BeginRead(myRequestState.BufferRead, 0, BufferSize, new AsyncCallback(ReadCallBack), myRequestState);
                }
            }
            catch (WebException e)
            {
                var myRequestState = (NotifyRequestState)asynchronousResult.AsyncState;
                if (myRequestState != null)
                {
                    string message = e.Status.ToString() + e.Message;
                    //UpdatetoDB(myRequestState.orderclass, myRequestState.order, myRequestState.url, 1, message, false, message);

                    InsertToDb(myRequestState.ordertype, myRequestState.order, myRequestState.url, 1, "", false, e.Status.ToString(), e.Message, "", "");
                }
            }
        }
        #endregion

        #region ReadCallBack
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        private static void ReadCallBack(IAsyncResult asyncResult)
        {
            try
            {
                var myRequestState = (NotifyRequestState)asyncResult.AsyncState;
                Stream responseStream = myRequestState.streamResponse;
                int read = responseStream.EndRead(asyncResult);
                if (read > 0)
                {
                    myRequestState.requestData.Append(Encoding.GetEncoding("GB2312").GetString(myRequestState.BufferRead, 0, read));

                    IAsyncResult asynchronousResult = responseStream.BeginRead(myRequestState.BufferRead, 0, BufferSize, new AsyncCallback(ReadCallBack), myRequestState);
                    return;
                }
                else
                {
                    string stringContent = string.Empty;
                    if (myRequestState.requestData.Length > 1)
                    {
                        stringContent = myRequestState.requestData.ToString();
                    }

                    responseStream.Close();

                    InsertToDb(myRequestState.ordertype, myRequestState.order, myRequestState.url, 1, stringContent, true, "", "", "", "");
                }
            }
            catch (WebException e)
            {
                var myRequestState = (NotifyRequestState)asyncResult.AsyncState;
                if (myRequestState != null)
                {
                    string message = e.Status.ToString() + e.Message;
                    //UpdatetoDB(myRequestState.orderclass, myRequestState.order, myRequestState.url, 1, message, false, message);

                    InsertToDb(myRequestState.ordertype, myRequestState.order, myRequestState.url, 1, message, false, e.Status.ToString(), e.Message, "", "");
                }
            }
        }
        #endregion

        #region TimeoutCallback
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="timedOut"></param>
        private static void TimeoutCallback(object state, bool timedOut)
        {
            if (timedOut)
            {
                var request = state as NotifyRequestState;
                if (request != null)
                {
                    string message = "访问超时";

                    InsertToDb(request.ordertype, request.order, request.url, 1, "", false, "Timeout", "message", "", "");
                }
            }
        }
        #endregion

        #region InsertToDb
        /// <summary>
        /// 将结果返回到数据库
        /// </summary>
        /// <param name="oclass"></param>
        /// <param name="order"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="times"></param>
        /// <param name="callbacktext"></param>
        /// <param name="success">是否执行成功</param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        /// <param name="statusDesc"></param>
        private static void InsertToDb(byte oclass, object order, string notifyUrl, int times, string callbacktext, bool success, string status, string message, string statusCode, string statusDesc)
        {
            bool isnotifysucc = false;

            switch (oclass)
            {
                case 1:
                    {
                        #region 网银通知

                        var orderInfo = order as OrderBankInfo;
                        if (orderInfo != null)
                        {
                            if (success && !string.IsNullOrEmpty(callbacktext))
                            {
                                isnotifysucc = Utility.CheckCallBackIsSuccess(orderInfo.version, callbacktext);
                            }

                            var notify = new Model.Order.Bank.BankNotify()
                            {
                                orderid = orderInfo.orderid,
                                status = status,
                                message = message,
                                httpStatusCode = statusCode,
                                StatusDescription = statusDesc,
                                againNotifyUrl = notifyUrl,
                                notifystat = isnotifysucc ? 2 : 4,
                                notifycontext = callbacktext,
                                notifytime = DateTime.Now
                            };

                            BLL.Order.Bank.BankNotify.Instance.Insert(notify);

                            if (orderInfo.notifystat != 2)
                            {
                                viviLib.Logging.LogHelper.Write("orderInfo.notifystat fail=>" + orderInfo.orderid);

                                //没有成功将发送到异常队列 多次通知
                                BanknotifyQueue.Send(orderInfo);

                                #region 系统定时补发
                                //var sysAutoReissueNotifyInfo = new SysAutoReissueInfo
                                //{
                                //    OrderType = 1,
                                //    OrderId = orderInfo.orderid,
                                //    InterfaceVersion = orderInfo.version,
                                //    NotifyUrl = notifyUrl
                                //};

                                //var reissue = new SysAutoReissue();

                                ////5s后重试
                                //var timerReissue = new System.Threading.Timer(reissue.Process, sysAutoReissueNotifyInfo, 5000, 0);
                                //sysAutoReissueNotifyInfo.TimerReissue = timerReissue;
                                #endregion
                            }


                        }

                        #endregion
                    }
                    break;

                case 2:
                    {
                        #region 点卡

                        var orderInfo = order as OrderCardInfo;
                        if (orderInfo != null)
                        {
                            if (success)
                            {
                                isnotifysucc = Card.Utility.CheckCallBackIsSuccess(orderInfo.version, callbacktext);
                            }

                            var notify = new Model.Order.Card.CardNotify()
                            {
                                orderid = orderInfo.orderid,
                                status = status,
                                message = message,
                                httpStatusCode = statusCode,
                                StatusDescription = statusDesc,
                                againNotifyUrl = notifyUrl,
                                notifystat = isnotifysucc ? 2 : 4,
                                notifycontext = callbacktext,
                                notifytime = DateTime.Now,
                                InterVersion = orderInfo.version
                            };

                            BLL.Order.Card.CardNotify.Instance.Insert(notify);

                            if (orderInfo.notifystat != 2)
                            {
                                //没有成功将发送到异常队列 多次通知
                              //  CardnotifyQueue.Send(orderInfo);

                                CardnotifyQueueX.Send(notify);

                                #region 系统定时补发
                                //var sysAutoReissueNotifyInfo = new SysAutoReissueInfo
                                //{
                                //    OrderType = 2,
                                //    OrderId = orderInfo.orderid,
                                //    InterfaceVersion = orderInfo.version,
                                //    NotifyUrl = notifyUrl
                                //};

                                //var reissue = new SysAutoReissue();

                                ////5s后重试
                                //var timerReissue = new System.Threading.Timer(reissue.Process, sysAutoReissueNotifyInfo, 5000, 0);
                                //sysAutoReissueNotifyInfo.TimerReissue = timerReissue;
                                #endregion
                            }
                        }

                        #endregion
                    }
                    break;

                case 4:
                    {
                        #region 点卡

                        var orderInfo = order as OrderCardTotal;
                        if (orderInfo != null)
                        {
                            int notifyStatus = 4;

                            if (success)
                            {
                                isnotifysucc = Card.Utility.CheckCallBackIsSuccess(orderInfo.version, callbacktext);

                                if (isnotifysucc)
                                {
                                    notifyStatus = 2;
                                }
                            }

                            var notify = new CardNotify()
                            {
                                orderid = orderInfo.orderid,
                                status = status,
                                message = message,
                                httpStatusCode = statusCode,
                                StatusDescription = statusDesc,
                                againNotifyUrl = notifyUrl,
                                notifystat = isnotifysucc ? 2 : 4,
                                notifycontext = callbacktext,
                                notifytime = DateTime.Now,
                                InterVersion = orderInfo.version
                            };

                            BLL.Order.Card.OrderCardTotal.Instance.Notify(orderInfo.orderid, notifyUrl, notifyStatus);

                            if (isnotifysucc == false)
                            {
                                CardnotifyQueueX.Send(notify);

                                //#region 系统定时补发
                                //var sysAutoReissueNotifyInfo = new SysAutoReissueInfo
                                //{
                                //    OrderType = 4,
                                //    OrderId = orderInfo.orderid,
                                //    InterfaceVersion = orderInfo.version,
                                //    NotifyUrl = notifyUrl
                                //};

                                //var reissue = new SysAutoReissue();

                                ////5s后重试
                                //var timerReissue = new System.Threading.Timer(reissue.Process, sysAutoReissueNotifyInfo, 5000, 0);
                                //sysAutoReissueNotifyInfo.TimerReissue = timerReissue;
                                //#endregion
                            }
                            //BLL.Order.Card.CardNotify.Instance.Insert(notify);

                            //if (orderInfo.notifystat != 2)
                            //{
                            //    //没有成功将发送到异常队列 多次通知
                            //    //CardnotifyQueue.Send(orderInfo);
                            //}
                        }

                        #endregion
                    }
                    break;
            }
        }

        #endregion
    }
}
