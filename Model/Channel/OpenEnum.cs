using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.Channel
{
    /// <summary>
    /// 开户状态
    /// 是否开启1全部关闭 2全部开启4  按配置 默认关闭（如果设置了商户按设置的状态 如果未设置） 8 按配置 默认关闭
    /// </summary>
    [Serializable]
    public enum OpenEnum
    {
        None = 0,
        AllClose = 1,
        AllOpen  = 2,
        Close    = 4,
        Open     = 8
    }
}
