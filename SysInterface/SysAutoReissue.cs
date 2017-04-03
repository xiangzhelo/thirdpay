using System;
using System.Net;
using System.Text;
using viviapi.SysInterface.Bank;

namespace viviapi.SysInterface
{
    /// <summary>
    /// 系统自动补发(共5次)
    /// 5s
    /// 10s
    /// 60s
    /// 10分
    /// 30分
    /// </summary>
    public class SysAutoReissue
    {
        public int Timeout = 2 * 1000 * 60;//超时时间

        public void Process(Object stateInfo)
        {
            var notifyInfo = (SysAutoReissueInfo)stateInfo;

            if (notifyInfo != null)
            {
                string notifyUrl = notifyInfo.NotifyUrl;

                string callbackText = string.Empty;
                string status = string.Empty;
                string message = string.Empty;
                string statusCode = string.Empty;
                string statusDesc = string.Empty;

                bool notifySucc = false;

                if (!string.IsNullOrEmpty(notifyUrl))
                {
                    #region 执行通知
                    try
                    {
                        callbackText = viviLib.Web.WebClientHelper.GetString(notifyUrl
                            , string.Empty
                            , "GET"
                            , Encoding.GetEncoding(notifyInfo.InputCharset)
                            , Timeout);

                        if (!string.IsNullOrEmpty(callbackText))
                            notifySucc = Utility.CheckCallBackIsSuccess(notifyInfo.InterfaceVersion, callbackText);
                    }
                    catch (WebException e)
                    {
                        status = e.Status.ToString();
                        message = e.Message;

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
                    #endregion

                    notifyInfo.NotifiedTimes++;

                    int notifyStatus = notifySucc ? 2 : 4;

                    if (notifyInfo.OrderType == 1)
                    {
                        #region 网银
                        var bankNotifyInfo = new Model.Order.Bank.BankNotify()
                        {
                            orderid = notifyInfo.OrderId,
                            status = status,
                            message = message,
                            httpStatusCode = statusCode,
                            StatusDescription = statusDesc,
                            againNotifyUrl = notifyUrl,
                            notifystat = notifyStatus,
                            notifycontext = callbackText,
                            notifytime = DateTime.Now
                        };
                        BLL.Order.Bank.BankNotify.Instance.Insert(bankNotifyInfo);
                        #endregion
                    }
                    else if (notifyInfo.OrderType == 2)
                    {
                        #region 点卡
                        var cardNotifyInfo = new Model.Order.Card.CardNotify()
                        {
                            orderid = notifyInfo.OrderId,
                            status = status,
                            message = message,
                            httpStatusCode = statusCode,
                            StatusDescription = statusDesc,
                            againNotifyUrl = notifyUrl,
                            notifystat = notifyStatus,
                            notifycontext = callbackText,
                            notifytime = DateTime.Now
                        };
                        BLL.Order.Card.CardNotify.Instance.Insert(cardNotifyInfo);
                        #endregion
                    }
                    else if (notifyInfo.OrderType == 4)
                    {
                        #region 多卡
                        var cardNotifyInfo = new Model.Order.Card.CardNotify()
                        {
                            orderid = notifyInfo.OrderId,
                            status = status,
                            message = message,
                            httpStatusCode = statusCode,
                            StatusDescription = statusDesc,
                            againNotifyUrl = notifyUrl,
                            notifystat = notifyStatus,
                            notifycontext = callbackText,
                            notifytime = DateTime.Now
                        };
                        BLL.Order.Card.OrderCardTotal.Instance.Notify(notifyInfo.OrderId, notifyUrl, notifyStatus);
                        #endregion
                    }

                    if (notifySucc || notifyInfo.NotifiedTimes >= 10)
                    {
                        if (notifyInfo.TimerReissue != null)
                        {
                            notifyInfo.TimerReissue.Dispose();
                            notifyInfo.TimerReissue = null;
                        }
                    }
                    else
                    {
                        switch (notifyInfo.NotifiedTimes)
                        {
                            case 2:
                                (notifyInfo.TimerReissue).Change(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(200));//10s
                                break;

                            case 3:
                                (notifyInfo.TimerReissue).Change(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(200));//1分钟
                                break;

                            case 4:
                                (notifyInfo.TimerReissue).Change(TimeSpan.FromMinutes(10), TimeSpan.FromSeconds(200));//10分钟
                                break;

                            case 5:
                                (notifyInfo.TimerReissue).Change(TimeSpan.FromMinutes(30), TimeSpan.FromSeconds(200));//30分钟
                                break;

                            default:
                                (notifyInfo.TimerReissue).Change(TimeSpan.FromMinutes(30), TimeSpan.FromSeconds(200));//30分钟
                                break;
                        }
                    }
                }
            }
        }
    }
}
