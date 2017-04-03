using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using viviapi.Model.Order;
using viviLib.Data;

namespace viviapi.IDAL
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderCard
    {     
        /// <summary>
        /// 新增卡类订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        long Insert(OrderCardInfo order);

        /// <summary>
        /// 添加子订单 用于多卡
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        long InsertItem(CardItemInfo order);
        
        /// <summary>
        /// 卡类订单完成处理
        /// </summary>
        /// <param name="order"></param>
        bool Complete(OrderCardInfo order);

        /// <summary>
        /// 多卡子订单完成
        /// </summary>
        /// <param name="order"></param>
        bool ItemComplete(CardItemInfo order, out bool allCompleted, out string opstate, out string ovalue, out decimal ototalvalue);   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderCardInfo GetModelByOrderId(string orderId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="serial"></param>
        /// <returns></returns>
        CardItemInfo GetItemModel(string orderId, int serial);

        DataTable DataItemsByOrderId(string orderId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderCardInfo GetModel(long ID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderCardInfo GetModel(long ID,int userid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool Notify(OrderCardInfo order);

       
    }
}