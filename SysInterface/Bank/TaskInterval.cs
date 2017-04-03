using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using viviapi.BLL.Order.Bank;
using viviapi.Model.Order;
using viviapi.SysConfig;
using viviLib.ExceptionHandling;

namespace viviapi.SysInterface.Bank
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskInterval : viviLib.ScheduledTask.IScheduledTaskExecute
    {
        private static int _notifyTransactionTimeout = MSMQSetting.NotifyTransactionTimeout;
        private static readonly int NotifyqueueTimeout = MSMQSetting.NotifyQueueTimeout;
        private static readonly int NotifybatchSize = MSMQSetting.NotifyBatchSize;
        private static int _notifythreadCount = MSMQSetting.NotifyThreadCount;

        /// <summary>
        /// 执行
        /// </summary>
        public void Execute()
        {
            //处理通知事件
            //ProcessNotifyFromDb();

            ProcessNotify();
        }

        //#region ProcessNotify
        ///// <summary>
        ///// 记录在队列中失败列表 重新补发
        ///// </summary>
        //private static void ProcessNotify()
        //{
        //    DataSet ds = BankNotify.Instance.GetNotifyFailList();
        //    if (ds != null)
        //    {
        //        try
        //        {
        //            foreach (var row in ds.Tables[0].Rows)
        //            {
        //                var apiNotify = new APINotification();

        //                var notifyInfo = new OrderNotifyX { Class = 1
        //                    , NotifyUrl = row["againNotifyUrl"].ToString()
        //                    , OrderId = row["orderid"].ToString()
        //                };

        //                var tmr = new Timer(apiNotify.NotifyCheckStatus, notifyInfo, 0, 1000);
        //                notifyInfo.Tmr = tmr;
        //            }

        //        }
        //        catch (Exception exception)
        //        {
        //            ExceptionHandler.HandleException(exception);
        //        }
        //    }
        //    //var apiNotify = new APINotification();

        //    //for (int j = 0; j < NotifybatchSize; j++)
        //    //{
        //    //    try
        //    //    {
        //    //        var order = apiNotify.ReceiveFromQueue(NotifyqueueTimeout);
        //    //        order.notifycount = 0;

        //    //        var notifyInfo = new OrderNotify { OrderInfo = order };

        //    //        var tmr = new Timer(apiNotify.NotifyCheckStatus, notifyInfo, 0, 1000);
        //    //        notifyInfo.Tmr = tmr;
        //    //    }
        //    //    catch (Exception exception)
        //    //    {
        //    //        ExceptionHandler.HandleException(exception);
        //    //    }
        //    //}
        //}
        //#endregion

        #region ProcessNotify
        /// <summary>
        /// 记录在队列中失败列表 重新补发
        /// </summary>
        private static void ProcessNotify()
        {
            var apiNotify = new APINotification();

            for (int j = 0; j < NotifybatchSize; j++)
            {
                try
                {
                    var order = apiNotify.ReceiveFromQueue(NotifyqueueTimeout);
                    if (order != null)
                    {
                        order.notifycount = 0;

                        var notifyInfo = new OrderNotify { OrderInfo = order };

                        //var tmr =
                        notifyInfo.Tmr = new Timer(apiNotify.NotifyCheckStatus, notifyInfo, 0, 1000); ;
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

        //#region ProcessNotifyFromDb
        ///// <summary>
        ///// 记录在队列中失败列表 重新补发
        ///// </summary>
        //private static void ProcessNotifyFromDb()
        //{
        //    DataTable data = BankNotify.Instance.GetNotifyFailList();

        //    foreach (DataRow row in data.Rows)
        //    {
        //        string version = row["version"].ToString();
        //        string orderNo = row["orderid"].ToString();
        //        string notifyUrl = row["NotifyUrl"].ToString();
        //        int notifyCount = 1;

        //        var notifyInfox = new OrderNotifyX { OrderNo = orderNo, NotifyUrl = notifyUrl, Version = version };
        //        notifyInfox.Tmr = new Timer(NotifyCheckStatus, notifyInfox, 0, 1000); ;

        //        BankNotify.Instance.UpdateInQueueStatus(row["orderid"].ToString());
        //    }
        //}
        //#endregion

        //#region NotifyCheckStatus
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="stateInfo"></param>
        //public static void NotifyCheckStatus(Object stateInfo)
        //{
        //    try
        //    {
        //        var notify = (OrderNotifyX)stateInfo;

        //        string notifyUrl = notify.NotifyUrl;

        //        if (string.IsNullOrEmpty(notifyUrl))
        //        {
        //            notify.Tmr.Dispose();
        //            notify.Tmr = null;
        //        }
        //        else
        //        {
        //            notify.NotifyCount++;

        //            if (notify.Tmr != null)
        //            {
        //                #region
        //                switch (notify.NotifyCount)
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
        //                #endregion
        //            }

        //            if (notify.NotifyCount > 10)
        //            {
        //                if (notify.Tmr != null)
        //                {
        //                    notify.Tmr.Dispose();
        //                    notify.Tmr = null;
        //                }
        //            }

        //            StartTask(notify);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandler.HandleException(ex);
        //    }
        //}
        //#endregion

        //#region StartTask
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="info"></param>
        //public static void StartTask(OrderNotifyX info)
        //{
        //    Thread threadHand1 = new Thread(() => Process(info));
        //    threadHand1.IsBackground = true;
        //    threadHand1.SetApartmentState(ApartmentState.STA);

        //    threadHand1.Start();
        //}
        //#endregion

        //#region Process
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="info"></param>
        //private static void Process(OrderNotifyX info)
        //{
        //    try
        //    {
        //        string notifyUrl = info.NotifyUrl;

        //        if (string.IsNullOrEmpty(notifyUrl))
        //            return;

        //        string callback = string.Empty;
        //        string status = string.Empty;
        //        string message = string.Empty;
        //        string statusCode = string.Empty;
        //        string statusDesc = string.Empty;

        //        bool isOk = false;

        //        try
        //        {
        //            callback = viviLib.Web.WebClientHelper.GetString(notifyUrl
        //                , string.Empty
        //                , "GET"
        //                , Encoding.GetEncoding("GB2312")
        //                , 1 * 60 * 1000);

        //            isOk = Utility.CheckCallBackIsSuccess(info.Version, callback);
        //        }
        //        catch (WebException e)
        //        {
        //            message = e.Message;
        //            status = e.Status.ToString();

        //            if (e.Status == WebExceptionStatus.ProtocolError)
        //            {
        //                statusCode = ((HttpWebResponse)e.Response).StatusCode.ToString();
        //                statusDesc = ((HttpWebResponse)e.Response).StatusDescription;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            message = e.Message;
        //        }

        //        int notifystat = isOk ? 2 : 4;

        //        var notifyInfo = new Model.Order.Bank.BankNotify()
        //        {
        //            orderid = info.OrderNo,
        //            status = status,
        //            message = message,
        //            httpStatusCode = statusCode,
        //            StatusDescription = statusDesc,
        //            againNotifyUrl = notifyUrl,
        //            notifystat = notifystat,
        //            notifycontext = callback,
        //            notifytime = DateTime.Now
        //        };

        //        BankNotify.Instance.Insert(notifyInfo);

        //        if (isOk)
        //        {
        //            if (info.Tmr != null)
        //            {
        //                info.Tmr.Dispose();
        //                info.Tmr = null;
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        ExceptionHandler.HandleException(exception);
        //    }

        //}
        //#endregion

    }

    //public class OrderNotifyX
    //{
    //    public OrderNotifyX()
    //    {
    //        NotifyCount = 1;
    //    }

    //    public string OrderNo { get; set; }

    //    public string Version { get; set; }

    //    /// <summary>
    //    /// 下发地址
    //    /// </summary>
    //    public string NotifyUrl { get; set; }

    //    /// <summary>
    //    /// 下发次数
    //    /// </summary>
    //    public int NotifyCount { get; set; }

    //    /// <summary>
    //    /// 定时器
    //    /// </summary>
    //    public Timer Tmr;
    //}
}
