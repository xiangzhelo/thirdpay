using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebUI7uka.agentmodule.WS
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class RechargeCalculateIncome : UserHandlerBase
    {
        public decimal Rechargemoney
        {
            get
            {
                return WebBase.GetQueryStringDecimal("rechargemoney", 0M);
            }
        }

        public int Bankcode
        {
            get
            {
                return WebBase.GetQueryStringInt32("bankno", 0);
            }
        }

        public override void OnLoad(HttpContext context)
        {
            string result = "0.00";

            try
            {
                if (CurrentUser != null && Rechargemoney > 0M && Bankcode > 0)
                {
                    int typeid = 102;
                    if (Bankcode == 992)
                        typeid = 101;
                    else if (Bankcode == 993)
                        typeid = 100;

                    decimal payRate = viviapi.BLL.Finance.PayRate.Instance.GetUserPayRate(UserId, typeid);

                    result = decimal.Round(Rechargemoney * payRate, 2).ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                result = "0.00";
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
        }
    }
}
