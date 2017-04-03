using System;
using System.Collections.Generic;
using System.Text;
using viviapi.Model.Order;

namespace viviapi.IBLLStrategy
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderCardStrategy
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="order"></param>
        void Insert(OrderCardInfo order);

        /// <summary>
        /// 初始化子订单
        /// </summary>
        /// <param name="order"></param>
        void InsertItem(CardItemInfo order);

        /// <summary>
        /// 完成订单处理
        /// </summary>
        /// <param name="order"></param>
        void Complete(OrderCardInfo order);

        /// <summary>
        /// 子订单完成
        /// </summary>
        /// <param name="order"></param>
        /// <param name="allCompleted"></param>
        /// <param name="opstate"></param>
        /// <param name="ovalue"></param>
        /// <param name="ototalvalue"></param>
        /// <returns></returns>
        bool ItemComplete(CardItemInfo order, out bool allCompleted, out string opstate, out string ovalue, out decimal ototalvalue);   
    }
}
