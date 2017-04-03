using System;
using System.Web;
using System.Web.Services;
using viviapi.BLL.Order.Card;
using viviapi.Model.Order;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebUI7uka.agentmodule.WS
{
    public class OrderJsonResult
    {
        public string Success { get; set; }
        public string paymoney { get; set; }
        public string errorMsg { get; set; }
    }

    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class QueryOrderInfo : UserHandlerBase
    {
        public Int64 Oid
        {
            get
            {
                return WebBase.GetQueryStringInt64("oid", 0L);
            }
        }

        public override void OnLoad(HttpContext context)
        {
            var result = new OrderJsonResult {Success = string.Empty, paymoney = "0", errorMsg = string.Empty};

            try
            {
                OrderCardInfo cardInfo = Factory.Instance.GetModel(Oid);
                if (cardInfo != null)
                {
                    result.Success = Factory.Instance.GetViewStatusName(cardInfo.status);
                    result.paymoney = Factory.Instance.GetViewSuccessAmt(cardInfo.status, cardInfo.realvalue);
                    result.errorMsg = cardInfo.msg;
                }
            }
            catch (Exception ex)
            {
                result.errorMsg = ex.Message;
            }

            string text = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);

            context.Response.ContentType = "application/json";
            context.Response.Write(text);
        }

    }
}
