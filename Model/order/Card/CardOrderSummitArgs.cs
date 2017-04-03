using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Order.Card
{
    public class CardOrderSummitArgs
    {
        public string SysOrderNo { get; set; }
        public byte Source { get; set; }
        public int CardTypeId { get; set; }
        public string CardNo { get; set; }
        public string CardPass { get; set; }
        public int FaceValue { get; set; }
        public string Attach { get; set; }

        public CardOrderSummitArgs()
        {
            Source = 1;
        }
    }
}
