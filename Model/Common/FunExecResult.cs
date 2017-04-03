using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Common
{
    public class FunExecResult
    {
        public FunExecResult()
        {
            ErrCode = 0;
            ErrMsg = "";
        }

        public int ErrCode { get; set; }
        public string ErrMsg { get; set; }
        public Object Obj { get; set; }
    }
}
