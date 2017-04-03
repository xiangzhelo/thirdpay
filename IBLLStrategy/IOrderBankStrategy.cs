using System;
using System.Collections.Generic;
using System.Text;
using viviapi.Model.Order;

namespace viviapi.IBLLStrategy
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderBankStrategy
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="order"></param>
        void Insert(OrderBankInfo order);

        /// <summary>
        /// 完成订单处理
        /// </summary>
        /// <param name="order"></param>
        void Complete(OrderBankInfo order);
    }
}
