using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace viviapi.SysInterface.Card.MyAPI
{
    public class CardInfoList : List<CardInfo>
    {
        public string cardno { get; set; }
        public string cardpwd { get; set; }
        public CardInfoList(HttpContext context)
        {
            cardno = Utility.GetQueryString(context, "cardno");
            cardpwd = Utility.GetQueryString(context, "cardpwd");

            //多卡
            if (cardno.IndexOf(';') > 0)
            {
                string[] cardnoArr = cardno.Split(';');
                string[] cardpwdArr = cardpwd.Split(';');
                int index = 0;
                foreach (string card in cardnoArr)
                {
                    CardInfo cardModel = new CardInfo(context);
                    cardModel.cardno = card;
                    cardModel.cardpwd = cardpwdArr[index];
                    cardModel.CardNo = card;
                    cardModel.CardPwd = cardpwdArr[index];
                    this.Add(cardModel);
                    index++;
                }
            }
            else
            {
                CardInfo cardModel = new CardInfo(context);
                this.Add(cardModel);
            }
        }

    }
}
