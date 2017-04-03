﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace viviapi.Model.Order
{
    /// <summary>
    /// 订单通知类
    /// </summary>
    public class OrderCardNotifyX
    {
        /// <summary>
        /// 订单资料
        /// </summary>
        public viviapi.Model.Order.Card.CardNotify NotifyInfo { get; set; }

        /// <summary>
        /// 下发地址
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 定时器
        /// </summary>
        public Timer tmr;
    }
}
