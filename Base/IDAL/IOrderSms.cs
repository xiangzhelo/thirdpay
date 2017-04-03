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
    public interface IOrderSms
    {
        /// <summary>
        /// 新增银行订单
        /// </summary>
        /// <param name="order"></param>
        bool Insert(OrderSmsInfo order);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderSmsInfo GetModel(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderSmsInfo GetModel(int id,int userid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderSmsInfo GetModel(string orderId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderSmsInfo GetModel(int suppId,string linkId);

        /// <summary>
        /// 扣单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        bool Deduct(string orderid);

        /// <summary>
        /// 还单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        bool ReDeduct(string orderid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool Notify(OrderSmsInfo order);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby);
    }
}
