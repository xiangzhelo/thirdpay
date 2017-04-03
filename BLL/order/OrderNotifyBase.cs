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
    /// 订单通知公共类
    /// 采用异步方式 通知用户网站在
    /// </summary>
    public class OrderNotifyBase
    {
        private static readonly viviapi.IMessaging.IOrderBankNotify banknotifyQueue = MessagingFactory.QueueAccess.OrderBankNotify();
        private static readonly viviapi.IMessaging.IOrderCardNotify cardnotifyQueue = MessagingFactory.QueueAccess.OrderCardNotify();
        private static readonly viviapi.IMessaging.IOrderSmsNotify smsnotifyQueue   = MessagingFactory.QueueAccess.OrderSmsNotify();

        //public static string SuccessFlag = BLL.Sys.Constant.OrderNotifySuccessFlag;
        //public static ManualResetEvent allDone = new ManualResetEvent(false);
        const int BUFFER_SIZE = 1024;        
        const int DefaultTimeout = 2 * 60 * 1000; // 2 minutes timeout

        #region UpdatetoDB
        /// <summary>
        /// 将结果返回到数据库
        /// </summary>
        /// <param name="oclass"></param>
        /// <param name="obj"></param>
        /// <param name="agurl"></param>
        /// <param name="times"></param>
        /// <param name="callbacktext"></param>
        private static void UpdatetoDB(int oclass, object obj, string agurl, int times, string callbacktext,bool success,string errcode)
        {
            if (oclass == 1)
            {
                OrderBankInfo orderInfo = obj as OrderBankInfo;
                if (orderInfo != null)
                {
                    OrderBank dal = new OrderBank();

                    //string SuccessFlag = SystemApiHelper.Successflag(orderInfo.version);

                    bool isnotifysucc = SystemApiHelper.CheckCallBackIsSuccess(orderInfo.version, callbacktext);

                    orderInfo.notifystat = isnotifysucc ? 2 : 4;                    
                    orderInfo.againNotifyUrl = agurl;
                    orderInfo.notifycontext = callbacktext;
                    orderInfo.notifycount = times;
                    orderInfo.notifytime = DateTime.Now;
                    dal.UpdateNotifyInfo(orderInfo);

                    if (orderInfo.notifystat != 2)
                    {
                        //没有成功将发送到异常队列 多次通知
                        banknotifyQueue.Send(orderInfo);
                    }
                }
            }
            else if (oclass == 2)
            {
                OrderCardInfo orderInfo = obj as OrderCardInfo;
                if (orderInfo != null)
                {
                    OrderCard dal = new OrderCard();

                    bool isnotifysucc = SystemApiHelper.CheckCallBackIsSuccess(orderInfo.version, callbacktext);
                    orderInfo.notifystat = isnotifysucc ? 2 : 4;
                    orderInfo.againNotifyUrl = agurl;
                    orderInfo.notifycontext = callbacktext;
                    orderInfo.notifycount = times;
                    orderInfo.notifytime = DateTime.Now;
                 
                    dal.UpdateNotifyInfo(orderInfo);

                    if (orderInfo.notifystat != 2)
                    {
                        //没有成功将发送到异常队列 多次通知
                        cardnotifyQueue.Send(orderInfo);
                    }
                }
            }
            else if (oclass == 3)
            {
                OrderSmsInfo orderInfo = obj as OrderSmsInfo;

                if (orderInfo != null)
                {
                    OrderSms dal = new OrderSms();

                    orderInfo.notifystat = success ? 2:4;
                    orderInfo.issucc = success;
                    orderInfo.errcode = errcode;
                    orderInfo.againNotifyUrl = agurl;
                    orderInfo.notifycontext = callbacktext;
                    orderInfo.notifycount = times;                  
                    dal.UpdateNotifyInfo(orderInfo);

                    if (orderInfo.notifystat != 2)
                    {
                        //没有成功将发送到异常队列 多次通知
                        smsnotifyQueue.Send(orderInfo);
                    }
                }
            }
        }
        #endregion

        #region GetNotifyUrl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oclass"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetNotifyUrl(int oclass, object obj)
        {
            string notifyUrl = string.Empty;
            if (oclass == 1)//网银
            {
                OrderBankInfo orderInfo = obj as OrderBankInfo;
                if (orderInfo == null)
                    return string.Empty;

                OrderBank bll = new OrderBank();
                notifyUrl = "";//bll.GetCallBackUrl(orderInfo);
            }
            else if(oclass == 2)//点卡
            {
                OrderCardInfo orderInfo = obj as OrderCardInfo;
                if (orderInfo == null)
                    return string.Empty;

                OrderCard bll = new OrderCard();
                //notifyUrl = bll.GetCallBackUrl(orderInfo);
            }
            else if (oclass == 3)//短信及声讯
            {
                OrderSmsInfo orderInfo = obj as OrderSmsInfo;
                if (orderInfo == null)
                    return string.Empty;
                OrderSms bll = new OrderSms();
                notifyUrl = bll.GetCallBackUrl(orderInfo);
            }
            return notifyUrl;
        }
        #endregion

        /// <summary>
        /// 完成异步访问
        /// </summary>
        /// <param name="oclass"></param>
        /// <param name="obj"></param>
        /// <param name="url"></param>
        public static void AsynchronousNotify(int oclass, object obj)
        {
            string url = GetNotifyUrl(oclass, obj);
            if (string.IsNullOrEmpty(url))
                return;
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                
                RequestState myRequestState = new RequestState();
                myRequestState.orderclass = oclass;
                myRequestState.order = obj;
                myRequestState.url = url;
                myRequestState.request = myHttpWebRequest;

                IAsyncResult result =
                  (IAsyncResult)myHttpWebRequest.BeginGetResponse(new AsyncCallback(RespCallback), myRequestState);

                ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, new WaitOrTimerCallback(TimeoutCallback), myRequestState, DefaultTimeout, true);                
            }
            catch (WebException e)
            {
                string message = e.Status.ToString() + e.Message;
                UpdatetoDB(oclass, obj, url, 1, message,false,message);
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
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
                RequestState myRequestState = (RequestState)asynchronousResult.AsyncState;

                HttpWebRequest myHttpWebRequest = myRequestState.request;
                myRequestState.response = (HttpWebResponse)myHttpWebRequest.EndGetResponse(asynchronousResult);

                Stream responseStream = myRequestState.response.GetResponseStream();
                myRequestState.streamResponse = responseStream;

                IAsyncResult asynchronousInputRead = responseStream.BeginRead(myRequestState.BufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), myRequestState);
            }
            catch (WebException e)
            {
                RequestState myRequestState = (RequestState)asynchronousResult.AsyncState;
                if (myRequestState != null)
                {
                    string message = e.Status.ToString() + e.Message;
                    UpdatetoDB(myRequestState.orderclass, myRequestState.order, myRequestState.url, 1, message,false,message);
                }
            }
        }
        #endregion

        
    }
}
