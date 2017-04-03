using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace viviapi.Model.Order
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderSmsNotify
    {
        /// <summary>
        /// 订单资料
        /// </summary>
        public OrderSmsInfo orderInfo {get; set;}

        /// <summary>
        /// 定时器
        /// </summary>
        public Timer tmr;
    }
}
