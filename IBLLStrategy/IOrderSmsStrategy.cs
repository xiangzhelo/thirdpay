using System;
using System.Collections.Generic;
using System.Text;
using viviapi.Model.Order;

namespace viviapi.IBLLStrategy
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderSmsStrategy
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="order"></param>
        void Insert(OrderSmsInfo order);

        ///// <summary>
        ///// 完成订单处理
        ///// </summary>
        ///// <param name="order"></param>
        //void Complete(OrderSmsInfo order);
    }
}
