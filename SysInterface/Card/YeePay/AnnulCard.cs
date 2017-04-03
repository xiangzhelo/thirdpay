using System;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Web;
using viviapi.BLL.Channel;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviapi.Model.Order.Card;
using viviapi.SysInterface.Card.YeePay.Lib;
using viviLib.ExceptionHandling;
using viviLib.Text;
using DES = viviapi.SysInterface.Lib.YeePay.DES;
using Digest = viviapi.SysInterface.Lib.YeePay.Digest;

namespace viviapi.SysInterface.Card.YeePay
{
    /// <summary>
    /// 
    /// </summary>
    public class AnnulCard
    {
        /// <summary>
        /// 点卡通用接口
        /// </summary>
        public static string EnName = "vcYee.Anl";
        public static string ChineseName = "易宝点卡通用接口";
        public static string NotifySuccessflag = "SUCCESS";

        #region CheckParameter
        /// <summary>
        /// -1：签名较验失败或未知错误
        /// </summary>
        /// <param name="annulCard"></param>
        /// <returns></returns>
        public static string CheckParameter(AnnulCardInfo annulCard)
        {
            if (annulCard == null)
            {
                return "-1";
            }
            if (string.IsNullOrEmpty(annulCard.p0_Cmd)
                || string.IsNullOrEmpty(annulCard.p1_MerId)
                || string.IsNullOrEmpty(annulCard.p2_Order)
                || string.IsNullOrEmpty(annulCard.p3_Amt)
                || string.IsNullOrEmpty(annulCard.p8_Url)
                || string.IsNullOrEmpty(annulCard.pa7_cardNo)
                || string.IsNullOrEmpty(annulCard.pa8_cardPwd)
                || string.IsNullOrEmpty(annulCard.pd_FrpId)
                || string.IsNullOrEmpty(annulCard.pr_NeedResponse)
                || string.IsNullOrEmpty(annulCard.hmac))
            {
                annulCard.Msg = "必要的参数不能为空";
                return "-1";
            }

            int userId = 0;
            if (!int.TryParse(annulCard.p1_MerId, out userId))
            {
                annulCard.Msg = "账号格式不正确";
                return "-1";
            }
            annulCard.UserId = userId;

            decimal p3Amt = 0M;
            if (!decimal.TryParse(annulCard.p3_Amt, out p3Amt))
            {
                annulCard.Msg = "支付金额不正确";
                return "66";
            }
            annulCard.OrderAmt = decimal.ToInt32(p3Amt);

            var userInfo = BLL.User.Factory.GetCacheUserBaseInfo(userId);
            if (userInfo == null)
            {
                annulCard.Msg = "商户不存在";
                return "-1";//
            }

            if (userInfo.Status != 2)
            {
                annulCard.Msg = "商户状态非正常";
                return "-1";//
            }
            //annulCard.User = userInfo;
            annulCard.APIkey = userInfo.APIKey;
            annulCard.ManageId = userInfo.manageId;


            string cardNo = annulCard.pa7_cardNo;
            string cardPwd = annulCard.pa8_cardPwd;

            if (annulCard.pa0_Mode == "3")
            {
                cardNo = DES.Decrypt3DES(annulCard.pa7_cardNo, userInfo.APIKey);
                cardPwd = DES.Decrypt3DES(annulCard.pa8_cardPwd, userInfo.APIKey);

                if (string.IsNullOrEmpty(cardNo) || string.IsNullOrEmpty(cardPwd))
                {
                    annulCard.Msg = "卡号及卡密解码失败";
                    return "-1";//
                }
            }


            annulCard.CardNo = cardNo;
            annulCard.CardPwd = cardPwd;

            int typeId = Common.GetChannelTypeId(annulCard.pd_FrpId, cardNo);
            if (typeId == 0)
            {
                annulCard.Msg = "支付通道不存在";
                return "-1";//
            }
            annulCard.TypeId = typeId;

            //if (!ChannelType.CheckCardFormat(typeId, cardNo, cardPwd, 0))
            //{
            //    annulCard.Msg = "卡密格式不正确";
            //    return "7";//
            //}

            if (!CheckSign(annulCard, userInfo.APIKey))
            {
                annulCard.Msg = "签名失败";
                return "-1";//
            }

            int cardType = Card.Utility.CodeMapping(typeId);

            string chanelNo = cardType.ToString("0000") +
                annulCard.OrderAmt.ToString(CultureInfo.InvariantCulture);

            var chanelInfo = Channel.GetModel(chanelNo, userId, true);
            if (chanelInfo == null)
            {
                annulCard.Msg = chanelNo + "通道不存在";
                return "112";//业务状态不可用，未开通此类卡业务
            }
            else if (chanelInfo.isOpen != 1)
            {
                annulCard.Msg = chanelNo + "通道不开放";
                return "112";//业务状态不可用，未开通此类卡业务
            }
            else if (!chanelInfo.supplier.HasValue)
            {
                return "-1";//
            }

            annulCard.ChanelNo = chanelNo;
            annulCard.CardType = Card.Utility.CodeMapping(typeId);
            annulCard.SupplierId = chanelInfo.supplier.Value;

            CheckAPIParameter chkresult = BLL.Order.Card.Factory.Instance.CheckCardInfo(userId
                    , annulCard.p2_Order
                    , typeId
                    , cardNo
                    , cardPwd
                    , annulCard.OrderAmt);

            #region 数据库 检查
            if (chkresult == null)
            {
                annulCard.Msg = "系统故障，服务器忙";
                return "-1";
            }
            else
            {
                annulCard.ProcessMode = 1;

                switch (chkresult.IsRepeat)
                {
                    case 1:
                        if (chkresult.Makeup == 1)
                        {
                            annulCard.SupplierId = chkresult.Supplierid;
                            annulCard.ProcessMode = 2;//自身处理

                            #region 补单
                            if (String.Equals(chkresult.Cardpwd, annulCard.CardPwd, StringComparison.CurrentCultureIgnoreCase))
                            {
                                if (chkresult.Isclose == 0)
                                {
                                    #region 继续补充
                                    int balance = decimal.ToInt32(chkresult.CardBalance);
                                    if (balance > 0)
                                    {
                                        if (annulCard.OrderAmt <= balance)
                                        {
                                            annulCard.ProcessMode = 2;//自身处理
                                        }
                                        else if (annulCard.OrderAmt > balance)
                                        {
                                            annulCard.Msg = "卡内余额不足";
                                            return "12"; //卡余额不足
                                        }
                                    }
                                    else
                                    {
                                        annulCard.Msg = "卡内余额不足";
                                        return "12";//卡余额不足
                                    }
                                    #endregion
                                }
                                else
                                {
                                    annulCard.Msg = "卡内余额不足";
                                    return "10";//不可以继续 补充了
                                }
                            }
                            else
                            {
                                annulCard.Msg = "卡密码不正确";
                                return "10";//卡密不对
                            }
                            #endregion
                        }
                        else
                        {
                            annulCard.ProcessMode = 1;//通过接口处理
                        }
                        break;
                    case 4:
                    case 5:
                        annulCard.Msg = "卡还在处理中，不可重复提交";
                        return "2";
                    case 6:
                        annulCard.Msg = "订单号重复";
                        return "11";
                    case 7:
                        annulCard.Msg = "提交次数过于频繁，1小时内不能超过3次";
                        return "2";
                    case 8:
                        annulCard.Msg = "充值卡无效";
                        return "12";
                }
            }
            #endregion

            return "1";//
        }
        #endregion

        #region 检查签名 CheckSign
        /// <summary>
        /// 签名较验
        /// </summary>
        /// <returns></returns>
        public static bool CheckSign(AnnulCardInfo annulCard, string apikey)
        {
            string sbOld = "";
            try
            {
                sbOld += annulCard.p0_Cmd;
                sbOld += annulCard.p1_MerId;
                sbOld += annulCard.p2_Order;
                sbOld += annulCard.p3_Amt;
                sbOld += annulCard.p8_Url;
                sbOld += annulCard.pa_MP;
                sbOld += annulCard.pa7_cardNo;
                sbOld += annulCard.pa8_cardPwd;
                sbOld += annulCard.pd_FrpId;
                sbOld += annulCard.pa0_Mode;
                sbOld += annulCard.pr_NeedResponse;

                string localhmac = Lib.Digest.HmacSign(sbOld, apikey);

                return localhmac == annulCard.hmac;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }
        #endregion

        #region GetResponseText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static string GetResponseText(SZXResult info, string apiKey)
        {
            string rHmac = info.R0_Cmd + info.R1_Code + info.R2_TrxId + info.R6_Order + info.Rq_ReturnMsg;

            rHmac = Lib.Digest.HmacSign(rHmac, apiKey);

            string text = string.Format("r0_Cmd={1}{0}r1_Code={2}{0}r2_TrxId={3}{0}r6_Order={4}{0}rq_ReturnMsg={5}{0}hmac={6}", '\n'
               , info.R0_Cmd
               , info.R1_Code
               , info.R2_TrxId
               , info.R6_Order
               , info.Rq_ReturnMsg
               , rHmac);

            return text;
        }
        #endregion

        #region CreateNotifyUrl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderinfo"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static string CreateNotifyUrl(OrderCardInfo orderinfo, string apiKey)
        {
            if (orderinfo == null)
                return string.Empty;

            string notifyUrl = orderinfo.notifyurl;
            if (string.IsNullOrEmpty(notifyUrl))
                return string.Empty;

            decimal facevalue = 0M;
            if (orderinfo.realvalue.HasValue)
                facevalue = decimal.Round(orderinfo.realvalue.Value, 0);

            string r0_Cmd = "AnnulCard";

            string r1_Code = "0";
            if (orderinfo.status == 2)
                r1_Code = "1";

            string rb_Order = orderinfo.userorder;
            string r2_TrxId = orderinfo.orderid;
            string pa_MP = orderinfo.attach;
            string rc_Amt = facevalue.ToString(CultureInfo.InvariantCulture);
            string rq_CardNo = orderinfo.cardNo;
            string p1_MerId = orderinfo.userid.ToString(CultureInfo.InvariantCulture);

            string sbOld = r0_Cmd + r1_Code + p1_MerId + rb_Order + r2_TrxId + pa_MP + rc_Amt;
            string nhmac = BLL.Sys.Transaction.YeePay.Digest.HmacSign(sbOld, apiKey);

            var parms = new StringBuilder();
            parms.AppendFormat("r0_Cmd={0}", Common.UrlEncode(r0_Cmd));
            parms.AppendFormat("&r1_Code={0}", Common.UrlEncode(r1_Code));
            parms.AppendFormat("&r2_TrxId={0}", Common.UrlEncode(r2_TrxId));
            parms.AppendFormat("&rb_Order={0}", Common.UrlEncode(rb_Order));
            parms.AppendFormat("&pa_MP={0}", Common.UrlEncode(pa_MP));
            parms.AppendFormat("&rc_Amt={0}", Common.UrlEncode(rc_Amt));
            parms.AppendFormat("&rq_CardNo={0}", Common.UrlEncode(rq_CardNo));
            parms.AppendFormat("&hmac={0}", Common.UrlEncode(nhmac));

            if (notifyUrl.IndexOf("?", System.StringComparison.Ordinal) > 0)
            {
                notifyUrl = notifyUrl + "&" + parms.ToString();
            }
            else
            {
                notifyUrl = notifyUrl + "?" + parms.ToString();
            }
            return notifyUrl;
        }
        #endregion
    }
}
