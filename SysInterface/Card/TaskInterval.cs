using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using viviapi.Model.Order;
using viviapi.SysConfig;
using viviLib.ExceptionHandling;
using viviLib.Logging;
using viviLib.ScheduledTask;


namespace viviapi.SysInterface.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskInterval : IScheduledTaskExecute
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("TempLogger");

        private static readonly int NotifyTransactionTimeout = MSMQSetting.NotifyTransactionTimeout_Card;
        private static readonly int NotifyqueueTimeout = MSMQSetting.NotifyQueueTimeout_Card;
        private static readonly int NotifybatchSize = MSMQSetting.NotifyBatchSize_Card;
        private static int _notifythreadCount = MSMQSetting.NotifyThreadCount_Card;
        /// <summary>
        /// 执行
        /// </summary>
        public void Execute()
        {
            //处理通知事件

           //ProcessNotifyX();

            ProcessNotify();

           
        }

        #region ProcessNotify
        /// <summary>
        /// http://www.23card.net
        /// </summary>
        private static void ProcessNotify()
        {
            TimeSpan tsTimeout = TimeSpan.FromSeconds(Convert.ToDouble(NotifyTransactionTimeout * NotifybatchSize));
           
            for (int j = 0; j < NotifybatchSize; j++)
            {
                try
                {
                    var apiNotify = new APINotification();

                    OrderCardInfo order = apiNotify.ReceiveFromQueue(NotifyqueueTimeout);
                    if (order != null)
                    {
                        order.notifycount = 0;
                        var notifyInfo = new OrderCardNotify { orderInfo = order };

                        
                        var tmr = new Timer(apiNotify.NotifyCheckStatus, notifyInfo, 0, 1000);
                        notifyInfo.tmr = tmr;
                    }
                    else
                    {
                        break;
                    }
                  
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
            }
        }
        #endregion

        #region ProcessNotify
        /// <summary>
        /// 
        /// </summary>
        private static void ProcessNotifyX()
        {
            TimeSpan tsTimeout = TimeSpan.FromSeconds(Convert.ToDouble(NotifyTransactionTimeout * NotifybatchSize));

            for (int j = 0; j < NotifybatchSize; j++)
            {
                try
                {
                    var apiNotify = new APINotification();

                    viviapi.Model.Order.Card.CardNotify cardNotifyInfo = apiNotify.ReceiveFromQueueX(NotifyqueueTimeout);
                    if (cardNotifyInfo != null)
                    {
                        cardNotifyInfo.notifycount = 1;

                        var notifyInfo = new OrderCardNotifyX { NotifyInfo = cardNotifyInfo };
                        notifyInfo.NotifyUrl = cardNotifyInfo.againNotifyUrl;

                        if (!string.IsNullOrEmpty(notifyInfo.NotifyUrl))
                        {
                            var tmr = new Timer(CheckStatus, notifyInfo, 0, 1000);
                            notifyInfo.tmr = tmr;
                        }

                    }

                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
            }
        }
        #endregion

        #region CheckStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateInfo"></param>
        public static void CheckStatus(Object stateInfo)
        {
            try
            {
                
                var notify = (Model.Order.OrderCardNotifyX)stateInfo;

                log.Info(notify.NotifyInfo.ToString());

                string notifyUrl = notify.NotifyInfo.againNotifyUrl;

                if (string.IsNullOrEmpty(notifyUrl))
                {
                    if (notify.tmr != null)
                    {
                        notify.tmr.Dispose();
                        notify.tmr = null;
                        return;
                    }
                }
                else
                {
                    notify.NotifyInfo.notifycount++;

                    if (notify.tmr != null)
                    {
                        switch (notify.NotifyInfo.notifycount)
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

                    if (notify.NotifyInfo.notifycount >= 10)
                    {
                        if (notify.tmr != null)
                        {
                            notify.tmr.Dispose();
                            notify.tmr = null;
                        }
                        return;
                    }
                    else
                    {
                        Process(notify);
                    }


                    
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
        #endregion

        #region Process
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        private static void Process(Model.Order.OrderCardNotifyX info)
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

                    isOk = Utility.CheckCallBackIsSuccess(info.NotifyInfo.InterVersion, callback);
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
                    orderid = info.NotifyInfo.orderid,
                    status = status,
                    message = message,
                    httpStatusCode = statusCode,
                    StatusDescription = statusDesc,
                    againNotifyUrl = notifyUrl,
                    notifystat = notifystat,
                    notifycontext = callback,
                    notifytime = DateTime.Now
                };

                viviapi.BLL.Order.Card.CardNotify.Instance.Insert(notifyInfo);

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
