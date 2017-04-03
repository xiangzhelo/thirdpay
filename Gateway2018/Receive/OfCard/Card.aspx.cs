using System;

namespace viviAPI.Gateway2018.Receive.OfCard
{
    public partial class Card : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.ETAPI.OfCard.Card.Default.Notify();
        }
    }
}
