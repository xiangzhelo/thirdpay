using System;

namespace viviapi.Model
{
    /// <summary>
    /// 结算状态
    /// </summary>
    public enum SettledStatus
    {
        已取消 = 0,
        审核中 = 1,
        支付中 = 2,
        无效   = 4,
        已支付 = 8,
        付款接口支付中 = 16
    }
}

