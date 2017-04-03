using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model.Order;
using viviapi.Model.Order.Card;

namespace viviAPI.Gateway2018
{
    public partial class test : System.Web.UI.Page
    {
        private static readonly viviapi.IMessaging.IOrderBankNotify BanknotifyQueue = viviapi.MessagingFactory.QueueAccess.OrderBankNotify();
        private static readonly viviapi.IMessaging.IOrderCardNotify CardnotifyQueue = viviapi.MessagingFactory.QueueAccess.OrderCardNotify();

        protected void Page_Load(object sender, EventArgs e)
        {
            var orderInfo = new OrderBankInfo();
            BanknotifyQueue.Send(orderInfo);
        }

        protected MessageQueueTransactionType transactionType = MessageQueueTransactionType.Automatic;
        protected MessageQueue queue;
        protected TimeSpan timeout;

        protected void Button1_Click(object sender, EventArgs e)
        {
            var orderInfo = new OrderCardInfo();
            orderInfo = viviapi.BLL.Order.Card.Factory.Instance.GetModelByOrderId("C5764548171599543052");
            CardnotifyQueue.Send(orderInfo);

           //viviapi.Model.Order.Card.CardNotify info = new CardNotify();
           // info.orderid = DateTime.Now.Ticks.ToString();
           // info.againNotifyUrl = "http://www.baidu.com";

            //BanknotifyQueue.Send(info);
        }
    }
}