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

namespace viviAPI.WebUI7uka.usermodule.WS
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
            string result = "0";
        decimal limitAmount =Rechargemoney;//默认无限制
            try
            {
                if (CurrentUser != null && Rechargemoney > 0M && Bankcode > 0)
                {
                    int typeid = 102;
                    
                    if (Bankcode == 992)
                        typeid = 101;
                    else if (Bankcode == 993)
                        typeid = 100;
                    else if (Bankcode ==1004)
                        typeid = 207;//微信

                    else if (Bankcode == 1007)
                        typeid = 204;//微信wab
                    else if (Bankcode == 51)
                        typeid = 203;//qq
                    else if (Bankcode == 1008)
                        typeid = 200;//手机支付把wab
                                     //else if (Bankcode == 51)
                                     //       typeid = 203;//qq
                    decimal payRate = viviapi.BLL.Finance.PayRate.Instance.GetUserPayRate(UserId, typeid);



                    viviapi.Model.SupplierInfo modelsu = viviapi.BLL.Supplier.Factory.GetModelByCode(typeid);


                    if (modelsu != null)
                    {
                        limitAmount = modelsu.limitAmount;
                    }
                    if (Rechargemoney > limitAmount)
                    {
                        result = decimal.Round(limitAmount * payRate, 2).ToString(CultureInfo.InvariantCulture);

                    }

                    else
                    { 
                    result = decimal.Round(Rechargemoney * payRate, 2).ToString(CultureInfo.InvariantCulture);
                        limitAmount = Rechargemoney;
                    }
                }
            }
            catch (Exception ex)
            {
                result = "0.00";
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(result+","+ limitAmount);
        }

    }

}
