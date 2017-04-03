using System;
using System.Web;
using viviapi.Model.User;

namespace viviAPI.WebUI7uka.usermodule.WS.Client
{
    public class OrderJsonResult
    {
        public string Success { get; set; }
        public string realvalue { get; set; }
        public string paymoney { get; set; }
        public string errorMsg { get; set; }
        public int orderstatus { get; set; }
        public bool isok { get; set; }
    }

    public class OrderSearch : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string result = string.Empty;

            var _result = new OrderJsonResult();
            _result.Success = string.Empty;
            _result.paymoney = "0";
            _result.realvalue = "0";
            _result.errorMsg = string.Empty;
            _result.orderstatus = 1;
            _result.isok = false;

            string orderid = viviLib.Web.WebBase.GetFormString("orderid", "");
            string sign = viviLib.Web.WebBase.GetFormString("sign", "");
            int _userid = 0;
            string userId = viviLib.Web.WebBase.GetFormString("userId", "");
            string apikey = string.Empty;

            if (
                string.IsNullOrEmpty(orderid) ||
                string.IsNullOrEmpty(userId) ||
                string.IsNullOrEmpty(sign))
            {
                result = "参数不正确";
            }
            else
            {

                if (!string.IsNullOrEmpty(userId))
                {
                    if (int.TryParse(userId, out _userid))
                    {

                    }
                }

                if (_userid == 0)
                {
                    result = "用户不存在";
                }
                else
                {
                    UserInfo userInfo = viviapi.BLL.User.Factory.GetCacheUserBaseInfo(_userid);

                    if (userInfo == null || userInfo.Status != 2)
                    {
                        result = "用户不存在";
                    }
                    else
                    {
                        apikey = userInfo.APIKey;
                    }
                }
            }
            if (string.IsNullOrEmpty(result))
            {
                string plain = orderid + apikey;                
                string localsign = viviLib.Security.Cryptography.MD5(plain);
                if (localsign != sign)
                {
                    result = "签名不正确";
                }
            }
            if (string.IsNullOrEmpty(result))
            {
                var cardInfo = viviapi.BLL.Order.Card.Factory.Instance.GetModelByOrderId(orderid);
                if (cardInfo != null)
                {
                    if (cardInfo.userid == _userid)
                    {
                        _result.Success = viviapi.BLL.Order.Card.Factory.Instance.GetViewStatusName(cardInfo.status);
                        _result.paymoney = viviapi.BLL.Order.Card.Factory.Instance.GetViewSuccessAmt(cardInfo.status, cardInfo.realvalue);
                        _result.errorMsg = cardInfo.userViewMsg;
                        _result.orderstatus = cardInfo.status;
                        if (cardInfo.realvalue != null) _result.realvalue = cardInfo.realvalue.Value.ToString();
                        _result.isok = true; 
                    }
                }
            }
            else
            {
                _result.errorMsg = result;
                _result.isok = false;
                
            }
            result = Newtonsoft.Json.JsonConvert.SerializeObject(_result, Newtonsoft.Json.Formatting.Indented);
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
