using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public enum WithdrawStatus
    {
        //
        None = 0,

        //已取消
        Canceled = 1,

        //审核中
        Auditing = 2,

        /// <summary>
        /// 支付中
        /// </summary>
        Paying = 4,

        /// <summary>
        /// 已支付
        /// </summary>
        Paid = 8
    }
}
