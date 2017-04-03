using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using viviapi.BLL.Order.Bank;
using viviLib.Web;

namespace viviAPI.Gateway2018.WS
{
    public class QueryOrderResult
    {
        public QueryOrderResult()
        {
            url = "";
            msg = "";
        }

        public string url { get; set; }
        public string msg { get; set; }
    }

    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class QueryOrder : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string url = "";
            string message = "未知错误";

            try
            {
                string orderid = WebBase.GetQueryStringString("oid", "");

                if (!string.IsNullOrEmpty(orderid))
                {
                    var info = Factory.Instance.GetModelByOrderId(orderid);
                    if (info == null)
                    {
                        message = "不存在此订单";
                    }
                    else
                    {
                        if (info.status == 1)
                        {
                            message = "未完成支付";
                        }
                        else
                        {
                            url = viviapi.SysInterface.Bank.Utility.GetReturnUrl(info);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            var result = new QueryOrderResult {msg = message, url = url};
            string text = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);

            context.Response.ContentType = "application/json";
            context.Response.Write(text);
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
