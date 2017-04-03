using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using viviapi.BLL;
using viviapi.BLL.Channel;
using viviapi.ETAPI.Common;
using viviapi.Model;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib.ExceptionHandling;
using viviLib.Web;
using Factory = viviapi.BLL.User.Factory;

namespace viviAPI.WebUI7uka.usermodule.WS
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CardSell : UserHandlerBase
    {
        #region 参数
        /// <summary>
        /// 
        /// </summary>
        public string CardNo
        {
            get
            {
                return WebBase.GetQueryStringString("CardId", "").Trim();
            }
        }
        public string CardPwd
        {
            get
            {
                return WebBase.GetQueryStringString("CardPass", "").Trim();
            }
        }
        public int FaceValue
        {
            get
            {
                return WebBase.GetQueryStringInt32("FaceValue", 0);
            }
        }
        public int TypeId
        {
            get
            {
                if (Intelligent == false)
                {
                    return WebBase.GetQueryStringInt32("ChannelId", 0);
                }
                else
                {
                    return CardUtility.GetTypeIdByCard(CardNo, CardPwd);
                }
            }
        }

        public bool Intelligent
        {
            get
            {
                return WebBase.GetQueryStringString("Intelligent", "").Trim() == "1";
            }
        }
        private ChannelTypeInfo _typeInfo = null;
        public ChannelTypeInfo TypeInfo
        {
            get
            {
                if (_typeInfo == null && TypeId > 0)
                    _typeInfo = ChannelType.GetCacheModel(TypeId);

                return _typeInfo;
            }
        }
        #endregion

        public override void OnLoad(HttpContext context)
        {
            string msg = "";

            if (string.IsNullOrEmpty(CardNo))
            {
                msg = "请输入卡号333";
            }
            else if (string.IsNullOrEmpty(CardPwd))
            {
                msg = "请输入卡密";
            }
            else if (FaceValue <= 0)
            {
                msg = "面值不正确";
            }
            else if (TypeId <= 0)
            {
                msg = "通道不正确";
            }
            else if (TypeInfo == null)
            {
                msg = "无效卡";
            }
            if (string.IsNullOrEmpty(msg))
            {
                try
                {
                    msg = ProcessOrder(context);
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    ExceptionHandler.HandleException(ex);
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
        }

        #region ProcessOrder
        /// <summary>
        /// 处理订单
        /// </summary>
        protected string ProcessOrder(HttpContext context)
        {
            return SendToSupp();
        }
        #endregion

        #region SendToSupp
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string SendToSupp()
        {
            string msg = string.Empty;

            try
            {
                ChannelTypeInfo chanelTypeInfo = ChannelType.GetCacheModel(TypeId);
                if (chanelTypeInfo == null)
                {
                    msg = "找不到通道,请联系商务或系统管理员处理。";
                }
                else
                {
                    int card_type = 0;

                    if (int.TryParse(chanelTypeInfo.code, out card_type))
                    {
                        string callBackurl = WebUtility.GetCurrentHost() + "/merchant/receiveResult/cardsell.aspx";

                        string postData =
                            string.Format(
                                "type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}",
                                card_type, UserId, CardNo, CardPwd, FaceValue, 0, DateTime.Now.Ticks.ToString(), callBackurl);

                        string sign = viviLib.Security.Cryptography.MD5(postData + CurrentUser.APIKey);

                        postData += "&sign=" + sign + "&attach=cardsell";

                        string postUrl = WebUtility.GetGatewayUrl() + "/CardReceive.aspx";

                        string callback = WebClientHelper.GetString(postUrl, postData, "GET",
                            System.Text.Encoding.GetEncoding("GB2312"), 10000);

                        if (callback == "opstate=0")
                        {
                            msg = "true";
                        }
                        else
                        {
                            msg = callback;
                        }
                    }
                    else
                    {
                        msg = "系统故障";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }

        #endregion

       
    }
}
