﻿using System;

namespace viviapi.Model.User
{
    /// <summary>
    /// 
    /// </summary>
    public enum UserLevelEnum
    {
        普通商家 = 1,
        会员商家 = 2,
        白银商家 = 4,
        黄金商家 = 8,
        白金商家 = 16,
        钻石商家 = 64,
        富豪商家 = 128,
       
        普通代理 = 100,
        初级代理 = 101,
        中级代理 = 102,
        高级代理 = 103,
        超级代理 = 104,
        特级代理 = 105
    }
}

