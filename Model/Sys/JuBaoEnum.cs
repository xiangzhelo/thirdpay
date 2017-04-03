using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model
{
    public enum JuBaoEnum
    {   
        未知 = 0,
        淫秽色情 = 1,
        诈骗 =2,
        病毒 =3,
        其他违法和不良信息=4
    }

    public enum JuBaoStatusEnum
    {
        等待回复 = 1,
        已回复 = 2
    }
}
