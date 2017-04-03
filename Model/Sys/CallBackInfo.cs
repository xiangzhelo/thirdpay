using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model
{
    public class CallBackInfo
    {
        public int error { get; set; }
        public string message { get; set; }

        public CallBackInfo()
        {
            error = 1;
            message = "error";
        }
    }
}
