using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviapi.Model.Order;
using viviapi.Model.Order.Card;

namespace viviapi.ETAPI.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemProcessCard
    {
        public void Process(Object stateInfo)
        {
            var procRes = (CardProcessResultInfo)stateInfo;

            var response = new CardOrderSupplierResponse()
            {
                SupplierId = procRes.supplierId,
                SuppTransNo = procRes.supplierOrder,
                SysOrderNo = procRes.orderid,
                OrderAmt = procRes.tranAMT,
                SuppAmt = 0M,
                OrderStatus = procRes.status,
                SuppErrorCode = procRes.errtype,
                Opstate = procRes.opstate,
                SuppErrorMsg = procRes.msg,
                ViewMsg = procRes.userViewMsg,
                Method = procRes.method
            };

            OrderCardUtils.Finish(response);

            procRes.tmr.Dispose();
            procRes.tmr = null;
        }
    }
}
