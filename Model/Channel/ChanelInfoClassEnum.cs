using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.Channel
{
    /// <summary>
    /// 通道类别
    /// </summary>
    [Serializable]
    public enum ChannelClassEnum
    {
        在线支付 = 1,
        充值卡   = 2,
        声讯     = 4,
        短信     = 8,
        代付款     = 16
    }
}
