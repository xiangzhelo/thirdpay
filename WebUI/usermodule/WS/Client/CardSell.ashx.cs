using System;
using System.Web;
using viviapi.BLL;
using viviapi.BLL.Channel;
using viviapi.ETAPI.Common;
using viviapi.Model;
using viviapi.Model.Channel;
using viviapi.Model.supplier;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviLib.Web;

namespace viviAPI.WebUI7uka.usermodule.WS.Client
{
    public class CardSell : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string result = string.Empty;

            try
            {
                string orderid = viviLib.Web.WebBase.GetFormString("orderid", "");
                string cardNo = viviLib.Web.WebBase.GetFormString("cardNo", "");
                string cardPwd = viviLib.Web.WebBase.GetFormString("cardPwd", "");
                string faceValue = viviLib.Web.WebBase.GetFormString("faceValue", "");
                string typeId = viviLib.Web.WebBase.GetFormString("typeId", "");
                string userId = viviLib.Web.WebBase.GetFormString("userId", "");
                string sign = viviLib.Web.WebBase.GetFormString("sign", "");
                string apikey = string.Empty;
                int typeid = 0;
                int.TryParse(typeId, out typeid);

                int? _manageId = 0;
                int _faceValue = 0;
                try
                {
                    _faceValue = Convert.ToInt32(decimal.Round(Convert.ToDecimal(faceValue), 0).ToString());
                }
                catch
                {
                }

                int _userid = 0;
                if (
                    string.IsNullOrEmpty(orderid) ||
                    string.IsNullOrEmpty(cardNo) ||
                    string.IsNullOrEmpty(cardPwd) ||
                    string.IsNullOrEmpty(faceValue) ||
                    string.IsNullOrEmpty(typeId) ||
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
                            _manageId = userInfo.manageId;
                        }
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    string plain = orderid + cardNo + cardPwd + faceValue + typeId + userId + apikey;
                    //viviLib.Logging.LogHelper.Write(plain);
                    string localsign = viviLib.Security.Cryptography.MD5(plain);
                    //viviLib.Logging.LogHelper.Write(localsign);
                    //viviLib.Logging.LogHelper.Write(sign);
                    if (localsign != sign)
                    {
                        result = "签名不正确";
                    }
                }

                if (string.IsNullOrEmpty(result))
                {
                    ChannelTypeInfo chanelTypeInfo = ChannelType.GetCacheModel(typeid);
                    if (chanelTypeInfo == null)
                    {
                        result = "找不到通道,请联系商务或系统管理员处理。";
                    }
                    else
                    {
                        #region 提交到网关

                        int card_type = 0;

                        if (int.TryParse(chanelTypeInfo.code, out card_type))
                        {
                            string callBackurl = WebUtility.GetCurrentHost() + "/merchant/receiveResult/cardsell.aspx";

                            string postData =
                                string.Format(
                                    "type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}",
                                    card_type, userId, cardNo, cardPwd, faceValue, 0, DateTime.Now.Ticks.ToString(),
                                    callBackurl);

                            sign = viviLib.Security.Cryptography.MD5(postData + apikey);

                            postData += "&sign=" + sign + "&attach=clientCardsell";

                            string postUrl = WebUtility.GetGatewayUrl() + "/CardReceive.ashx";

                            string callback = WebClientHelper.GetString(postUrl, postData, "GET",
                                System.Text.Encoding.GetEncoding("GB2312"), 10000);

                            if (callback == "opstate=0")
                            {
                                result = "success";
                            }
                            else
                            {
                                result = "提卡失败";
                            }
                        }
                        else
                        {
                            result = "系统故障";
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                result = "系统故障";
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
