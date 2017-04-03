using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace viviAPI.WebUI7uka.usermodule.Ajax
{
    public class OrderJsonResult
    {
        public string Success { get; set; }
        public string paymoney { get; set; }
        public string errorMsg { get; set; }
    }

    public class OrderJson_new : IHttpHandler, IReadOnlySessionState
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public Int64 oid
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt64("oid", 0L);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetViewStatusName(object status)
        {
            if (status == DBNull.Value)
                return string.Empty;
            //viviapi.Model.Order.OrderStatusEnum _stat = (viviapi.Model.Order.OrderStatusEnum)Convert.ToInt32(status);
            if (Convert.ToInt32(status) == 8)
                return "失败";
            else
                return Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum), status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetViewSuccessAmt(object status, object amt)
        {
            if (status == DBNull.Value || amt == DBNull.Value)
                return "0";

            //viviapi.Model.Order.OrderStatusEnum _stat = (viviapi.Model.Order.OrderStatusEnum)Convert.ToInt32(status);
            if (Convert.ToInt32(status) == 2)
                return decimal.Round(Convert.ToDecimal(amt), 2).ToString();
            else
                return "0";
        }

        public void ProcessRequest(HttpContext context)
        {
            string text = string.Empty;

            OrderJsonResult _result = new OrderJsonResult();
            _result.Success = string.Empty;
            _result.paymoney = "0";
            _result.errorMsg = string.Empty;

            if (viviapi.BLL.User.Login.CurrentMember != null)
            {
                var _cardInfo = viviapi.BLL.Order.Card.Factory.Instance.GetModel(oid);
                if (_cardInfo != null)
                {
                    if (_cardInfo.userid == viviapi.BLL.User.Login.CurrentMember.ID)
                    {
                        _result.Success = GetViewStatusName(_cardInfo.status);
                        _result.paymoney = GetViewSuccessAmt(_cardInfo.status, _cardInfo.realvalue);
                        _result.errorMsg = _cardInfo.msg;
                    }
                }
            }
            text = Newtonsoft.Json.JsonConvert.SerializeObject(_result, Newtonsoft.Json.Formatting.Indented);
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
