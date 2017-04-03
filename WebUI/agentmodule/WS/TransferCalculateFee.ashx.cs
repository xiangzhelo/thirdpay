using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using viviapi.BLL.Finance;
using viviapi.BLL.Settled;
using viviapi.Model.Finance;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebUI7uka.agentmodule.WS
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TransferCalculateFee : UserHandlerBase
    {
        protected TransferScheme schemeBLL = new TransferScheme();
        public decimal TransferMoney
        {
            get
            {
                return WebBase.GetQueryStringDecimal("money", 0M);
            }
        }


        public override void OnLoad(HttpContext context)
        {
            decimal charges = 0M;

            try
            {
                if (TransferMoney > 0)
                {
                    var scheme = schemeBLL.GetModel(1);

                    decimal monthAmt = schemeBLL.GetUserMonthTotalAmt(this.UserId);
                    //免费流量每个月
                    decimal freeAmt = scheme.monthmaxamt - monthAmt;
                    decimal startAmt = TransferMoney;

                    if (freeAmt > 0M)
                    {
                        startAmt = TransferMoney - freeAmt;
                    }
                    if (startAmt > 0M)
                    {
                        charges = scheme.chargerate * startAmt / 100M;
                        if (charges < scheme.chargeleastofeach)
                        {
                            charges = scheme.chargeleastofeach;
                        }
                        else if (charges > scheme.chargemostofeach)
                        {
                            charges = scheme.chargemostofeach;
                        }
                    }
                }
            }
            catch (Exception exception)
            {

            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(charges.ToString("f2"));
        }
    }
}
