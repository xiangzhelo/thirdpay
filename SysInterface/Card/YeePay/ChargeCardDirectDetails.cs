using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.SysInterface.Card.YeePay
{
    [Serializable]
    public class ChargeCardDirectDetails
    {
        public int TypeId { get; set; }
        public int CardType { get; set; }
        public int UserId { get; set; }
        public string APIkey { get; set; }
        public int ManageId { get; set; }
        public string UserOrderNo { get; set; }
        
        public string SysOrderNo { get; set; }

        //提交金额
        public decimal Refervalue { get; set; }
        public string SerialNumber { get; set; }
        public string CardStatus { get; set; }
        public string CardNo { get; set; }
        public string CardPwd { get; set; }
        
        public int SupplierId { get; set; }
        public string ChanelNo { get; set; }
        public byte ProcessMode { get; set; }

        public string Msg { get; set; }

    }
}
