using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.ETAPI.Shengpay
{
    public class PayChannelInfo
    {
        //支付类型
        public string PaymentType { get; set; }
        //支付渠道
        public string PayChannel { get; set; }
        //支付机构
        public string InstCode { get; set; }
    }
}
