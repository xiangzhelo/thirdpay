using System;

namespace viviAPI.Gateway2018.Receive.Card51esales
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Card : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.ETAPI.Card51.Card.Default.CardNotify();
        }
    }
}
