using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviapi.Model.Order;
using System.Threading;

namespace viviapi.BLL.Order.Card
{
    public class SystemProcessCard
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateInfo"></param>
        public void Process(Object stateInfo)
        {
            CardProcessResultInfo procRes = (CardProcessResultInfo)stateInfo;

            BLL.OrderCard bll = new viviapi.BLL.OrderCard();

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

            //bll.ReceiveSuppResult(
            //      procRes.supplierId
            //    , procRes.orderid
            //    , procRes.supplierOrder
            //    , procRes.status
            //    , procRes.opstate
            //    , procRes.msg
            //    , procRes.userViewMsg
            //    , procRes.tranAMT
            //    , procRes.suppAmt
            //    , procRes.errtype
            //    , procRes.method);

            procRes.count++;

            bool timerflag = false;
            if (procRes.tmr != null)
            {
                switch (procRes.count)
                {
                    case 1:
                        timerflag = (procRes.tmr).Change(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(200));
                        break;
                    case 2:
                        timerflag = (procRes.tmr).Change(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(200));//1分钟
                        break;
                    case 3:
                        timerflag = (procRes.tmr).Change(TimeSpan.FromMinutes(2), TimeSpan.FromSeconds(200));//2分钟
                        break;
                }
            }

            if (procRes != null)
            {
                if (procRes.count >= 4)
                {
                    if (procRes != null)
                    {
                        procRes.tmr.Dispose();
                        procRes.tmr = null;
                    }
                }
            }
        }
    }
}
