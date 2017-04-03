using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.ETAPI.Baofoo
{
    public class OrderQueryResult
    {
        public string MerchantID { get; set; }
        public string TransID { get; set; }
        /// <summary>
        /// Y：成功 F：失败 P：处理中 N：没有订单 
        /// </summary>
        public string CheckResult { get; set; }
        public string FactMoney { get; set; }
        public string SuccTime { get; set; }
        public string Md5Sign { get; set; }
        public bool CheckOk { get; set; }
    }
}
