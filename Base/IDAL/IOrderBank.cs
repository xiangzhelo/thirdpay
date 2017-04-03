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
    public interface IOrderBank
    {
        /// <summary>
        /// 新增银行订单
        /// </summary>
        /// <param name="order"></param>
        long Insert(OrderBankInfo order);

        /// <summary>
        /// 银行订单完成处理
        /// </summary>
        /// <param name="order"></param>
        bool Complete(OrderBankInfo order);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderBankInfo GetModelByOrderId(string orderId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderBankInfo GetModel(long orderId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderBankInfo GetModel(long orderId,int userid);

       

    }
}
