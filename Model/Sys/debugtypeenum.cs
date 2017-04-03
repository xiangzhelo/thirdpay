using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public enum debugtypeenum
    {
        未知 = 0,
        网银订单 = 1,
        卡类订单 = 2,
        短信订单 = 4,
        支付宝 = 8,
        财富通 = 16,
        对私代发=32
    }
}
