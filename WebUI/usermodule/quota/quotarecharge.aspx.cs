using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI2015.usermodule.quota
{
    public partial class quotarecharge : UserPageBase
    {
        public string hostname;
        public string litAnableAmount;
        protected void Page_Load(object sender, EventArgs e)
        {
            hostname=CurrentUser.full_name;
            decimal balanceAmt = viviapi.BLL.User.UsersAmt.GetUserAvailableBalance(this.UserId);

            if (balanceAmt <= 0M)
            {
                litAnableAmount = "0.00";
            }

            litAnableAmount = balanceAmt.ToString("f2");
        }
    }
}