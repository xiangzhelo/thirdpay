using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace viviapi.Model.Order
{
    /// <summary>
    /// 处理结果
    /// </summary>
    public class CardProcessResultInfo
    {
        public int supplierId { get; set; }
        public string orderid { get; set; }
        public string supplierOrder { get; set; }
        public int status { get; set; }
        public string opstate { get; set; }
        public string msg { get; set; }
        public string userViewMsg { get; set; }
        public decimal tranAMT { get; set; }
        public decimal suppAmt { get; set; }
        public string errtype { get; set; }
        public byte method { get; set; }

        public int count { get; set; }
        public Timer tmr;
    }
}
