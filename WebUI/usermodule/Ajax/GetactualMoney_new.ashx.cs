using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using viviLib.Web;

namespace viviAPI.WebUI7uka.usermodule.Ajax
{
    public class GetactualMoneyNew : IHttpHandler, IReadOnlySessionState
    {
        public decimal Rechargemoney
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringDecimal("rechargemoney", 0M);
            }
        }

        public int bankcode
        {
            get
            {
                return WebBase.GetQueryStringInt32("bank", 0);
            }
        }

        public viviapi.Model.User.UserInfo CurrentUser
        {
            get
            {
                return viviapi.BLL.User.Login.CurrentMember;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string result = "0.00";
            if (CurrentUser != null && Rechargemoney > 0M && bankcode > 0)
            {
                int typeid = 102;
                if (bankcode == 992)
                    typeid = 101;
                else if (bankcode == 993)
                    typeid = 100;

                decimal payRate = viviapi.BLL.Finance.PayRate.Instance.GetUserPayRate(CurrentUser.ID, typeid);

                result = decimal.Round(Rechargemoney * payRate, 2).ToString(CultureInfo.InvariantCulture);
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
