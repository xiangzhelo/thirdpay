using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public enum WithdrawMode
    {
        None=0,

        Manual=1,

        System=2,

        ByApi=4,

        UpFile=8
    }
}
