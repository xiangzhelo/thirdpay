using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace viviapi.SysInterface
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderNotifyInfo
    {
        public byte  OrderType { get; set; }

        public object OrderInfo { get; set; }

        public string NotifyUrl { get; set; }

        public string InputCharset { get; set; }

        public string Method { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class AutoProcess
    {
        public string NotifyUrl { get; set; }

        public void Process(Object stateInfo)
        {
            var info = (OrderNotifyInfo)stateInfo;

            if (info != null)
            {
                if (!string.IsNullOrEmpty(info.NotifyUrl))
                {
                    string respText =
                        viviLib.Web.WebClientHelper.GetString(info.NotifyUrl, "", info.Method,
                            System.Text.Encoding.GetEncoding(info.InputCharset), 30000);


                }
            }
        }

        public Timer Tmr;
    }
}
