using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;

namespace viviapi.ETAPI.YeePay.ZGT
{
    public class Weixin:ETAPIBase
    {
        private static int suppid = (int)SupplierCode.YeePayZGT;
        public Weixin() : base(suppid)
        {
        }

    }
}
