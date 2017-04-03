using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace viviapi.Model.Order
{
    /// <summary>
    /// 订单通知类
    /// </summary>
    public class OrderNotify
    {
        public OrderNotify()
        {
            NotifyCount = 0;
        }

        /// <summary>
        /// 订单资料
        /// </summary>
        public OrderBankInfo OrderInfo {get ;set;}

        /// <summary>
        /// 下发地址
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 下发次数
        /// </summary>
        public int NotifyCount { get; set; }

        /// <summary>
        /// 定时器
        /// </summary>
        public Timer Tmr;
    }
}
