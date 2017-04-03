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
using System.Text;

namespace viviAPI.WebUI7uka.usermodule.WS
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class lotsCardSell : UserHandlerBase
    {
        /// <summary>
        /// 卡号密码列表分析,传入格式举例：12343221031751020 221302412235039520-09745220625986581 221973305179797930
        /// </summary>
        public List<KeyValuePair<string, string>> CardPairsList
        {
            get
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                string listStr = WebBase.GetQueryStringString("CardPar", "").Trim();
                if (listStr.Length > 0)
                {
                    if (listStr.IndexOf('-') != -1)
                    {
                        string[] array = listStr.Split('-');
                        foreach (string str in array)
                        {
                            string[] pairArray = str.Split(' ');
                            KeyValuePair<string, string> pair = new KeyValuePair<string, string>(pairArray[0], pairArray[1]);
                            list.Add(pair);
                        }
                    }
                    else
                    {
                        string[] pairArray = listStr.Split(' ');
                        KeyValuePair<string, string> pair = new KeyValuePair<string, string>(pairArray[0], pairArray[1]);
                        list.Add(pair);
                    }

                }
                return list;
            }
        }
        #region 参数
        /// <summary>
        /// 批量卡号密码字符串
        /// </summary>
        ///
        public string CardPar
        {
            get
            {
                return WebBase.GetQueryStringString("CardPar", "").Trim();
            }
        }
        /// <summary>
        /// 面额
        /// </summary>
        public string ParValue
        {
            get
            {
                return WebBase.GetQueryStringString("ParValue", "").Trim();
            }
        }
        /// <summary>
        /// 通道类型（卡类型）
        /// </summary>
        public int CardInfoID
        {
            get
            {
                return WebBase.GetQueryStringInt32("CardInfoID", 0);
            }
        }
        //public int TypeId
        //{
        //    get
        //    {
        //        if (Intelligent == false)
        //        {
        //            return WebBase.GetQueryStringInt32("CardInfo", 0);
        //        }
        //        else
        //        {
        //            return CardUtility.GetTypeIdByCard(CardNo, CardPwd);
        //        }
        //    }
        //}

        //public bool Intelligent
        //{
        //    get
        //    {
        //        return WebBase.GetQueryStringString("Intelligent", "").Trim() == "1";
        //    }
        //}
        private ChannelTypeInfo _typeInfo = null;
        public ChannelTypeInfo TypeInfo
        {
            get
            {
                if (_typeInfo == null && CardInfoID > 0)
                    _typeInfo = ChannelType.GetCacheModel(CardInfoID);

                return _typeInfo;
            }
        }
        #endregion

        public override void OnLoad(HttpContext context)
        {
            string msg = "";

            //if (string.IsNullOrEmpty(CardNo))
            //{
            //    msg = "请输入卡号";
            //}
            //else if (string.IsNullOrEmpty(CardPwd))
            //{
            //    msg = "请输入卡密";
            //}
            if (Convert.ToInt32(ParValue) <= 0)
            {
                msg = "面值不正确";
            }
            else if (CardInfoID <= 0)
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
                ChannelTypeInfo chanelTypeInfo = ChannelType.GetCacheModel(CardInfoID);
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
                        StringBuilder cardno = new StringBuilder();
                        StringBuilder cardpwd = new StringBuilder();
                        int totalValue = CardPairsList.Count * Convert.ToInt32(ParValue);
                        int currentIndex = 1;
                        foreach (KeyValuePair<string, string> pair in CardPairsList)
                        {
                            
                            cardno.Append(pair.Key);
                            cardpwd.Append(pair.Value);
                            if (currentIndex < CardPairsList.Count)
                            {
                                cardno.Append(";");
                                cardpwd.Append(";");
                            }
                            currentIndex++;
                        }
                        string postData =
                            //
                            string.Format(
                                "type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}",
                                card_type, UserId, cardno.ToString(), cardpwd.ToString(), ParValue, 0, DateTime.Now.Ticks.ToString(), callBackurl);

                        string sign = viviLib.Security.Cryptography.MD5(postData + CurrentUser.APIKey);

                        postData += "&sign=" + sign + "&attach=cardsell" + "&totalvalue=" + totalValue.ToString();

                        string postUrl = WebUtility.GetGatewayUrl() + "/CardReceive.aspx";

                        string callback = WebClientHelper.GetString(postUrl, postData, "GET",
                            System.Text.Encoding.GetEncoding("GB2312"), 10000);

                        if (callback == "opstate=1" || callback == "opstate=0")//提交成功，返回状态码true
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
