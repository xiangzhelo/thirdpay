using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using viviapi.BLL.Channel;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviapi.Model.Order.Card;
using viviapi.SysInterface.Card.YeePay.Lib;
using Digest = viviapi.SysInterface.Lib.YeePay.Digest;

namespace viviapi.SysInterface.Card.YeePay
{
    /// <summary>
    /// 非银行卡专业版
    /// </summary>
    public class ChargeCardDirect
    {
        /// <summary>
        /// 
        /// </summary>
        public static string EnName = "vcYee.Dit";
        public static string ChineseName = "非银行卡专业版（组合支付）";
        public static string NotifySuccessflag = "SUCCESS";

        #region CheckParameter
        /// <summary>
        /// -1：签名较验失败或未知错误
        /// </summary>
        /// <param name="directCard"></param>
        /// <returns></returns>
        public static string CheckParameter(ChargeCardDirectInfo directCard)
        {
            if (directCard == null)
            {
                return "-1";
            }

            if (string.IsNullOrEmpty(directCard.p0_Cmd)
                || string.IsNullOrEmpty(directCard.p1_MerId)
                || string.IsNullOrEmpty(directCard.p2_Order)
                || string.IsNullOrEmpty(directCard.p3_Amt)
                || string.IsNullOrEmpty(directCard.p4_verifyAmt)
                || string.IsNullOrEmpty(directCard.p8_Url)
                || string.IsNullOrEmpty(directCard.pa7_cardAmt)
                || string.IsNullOrEmpty(directCard.pa8_cardNo)
                || string.IsNullOrEmpty(directCard.pa9_cardPwd)
                || string.IsNullOrEmpty(directCard.pd_FrpId)
                || string.IsNullOrEmpty(directCard.pr_NeedResponse)
                || string.IsNullOrEmpty(directCard.hmac))
            {
                directCard.Msg = "必要的参数不能为空";
                return "-1";
            }

            int userId = 0;
            if (!int.TryParse(directCard.p1_MerId, out userId))
            {
                directCard.Msg = "账号格式不正确";
                return "-1";
            }
            directCard.UserId = userId;

            decimal p3Amt = 0M;
            if (!decimal.TryParse(directCard.p3_Amt, out p3Amt))
            {
                directCard.Msg = "支付金额不正确";
                return "66";
            }
            directCard.OrderAmt = p3Amt;

            var userInfo = BLL.User.Factory.GetCacheUserBaseInfo(userId);
            if (userInfo == null)
            {
                directCard.Msg = "商户不存在";
                return "-1";//
            }
            //annulCard.User = userInfo;
            directCard.APIkey = userInfo.APIKey;
            directCard.ManageId = userInfo.manageId;

            #region
            directCard.CardNum = 0;
            if (!string.IsNullOrEmpty(directCard.pa8_cardNo))
            {
                directCard.CardNos = directCard.pa8_cardNo.Split(',');
                directCard.CardNum = directCard.CardNos.Length;
            }
            if (directCard.CardNum == 0)
            {
                directCard.Msg = "至少需要一张卡";
                return "5";//
            }
            if (directCard.CardNum > 10)
            {
                directCard.Msg = "卡数量过多，目前最多支持10张卡";
                return "5";//
            }

            if (!string.IsNullOrEmpty(directCard.pa7_cardAmt))
            {
                directCard.CardAmts = directCard.pa7_cardAmt.Split(',');
            }

            if (!string.IsNullOrEmpty(directCard.pa9_cardPwd))
            {
                directCard.CardPwds = directCard.pa9_cardPwd.Split(',');
            }

            if (directCard.CardAmts == null
                || directCard.CardAmts.Length != directCard.CardNum)
            {
                directCard.Msg = "卡面额组填写错误";
                return "8001";//
            }

            if (directCard.CardPwds == null
                || directCard.CardPwds.Length != directCard.CardNum)
            {
                directCard.Msg = "卡号密码为空或者数量不相等";
                return "8002";//
            }

            directCard.CardFaceValues = new decimal[] { directCard.CardNum };

            for (int i = 0; i < directCard.CardNum; i++)
            {
                if (string.IsNullOrEmpty(directCard.CardNos[i]))
                {
                    directCard.Msg = "卡号密码为空或者数量不相等";
                    return "8002";//
                }
            }

            for (int i = 0; i < directCard.CardNum; i++)
            {
                if (string.IsNullOrEmpty(directCard.CardPwds[i]))
                {
                    directCard.Msg = "卡号密码为空或者数量不相等";
                    return "8002";//
                }
            }

            for (int i = 0; i < directCard.CardNum; i++)
            {
                decimal tempAmt = 0M;
                if (!decimal.TryParse(directCard.CardAmts[i], out tempAmt))
                {
                    directCard.Msg = "卡面额组填写错误";
                    return "8001";//
                }
                else
                {
                    directCard.CardFaceValues[i] = tempAmt;
                }
            }
            #endregion

            int typeId = Common.GetChannelTypeId(directCard.pd_FrpId, directCard.CardNos[0]);
            if (typeId == 0)
            {
                directCard.Msg = "支付通道不存在";
                return "-1";//
            }
            directCard.TypeId = typeId;

            if (!CheckSign(directCard, userInfo.APIKey))
            {
                directCard.Msg = "签名失败";
                return "-1";//
            }

            var chanelInfo = ChannelType.GetCacheModel(typeId);
            if (chanelInfo == null)
            {
                directCard.Msg = typeId.ToString(CultureInfo.InvariantCulture) + "通道不存在";
                return "112";//业务状态不可用，未开通此类卡业务
            }
            if (chanelInfo.isOpen == OpenEnum.AllClose)
            {
                directCard.Msg = typeId.ToString(CultureInfo.InvariantCulture) + "未开通此类卡业务";
                return "112";//业务状态不可用，未开通此类卡业务
            }

            int cardType = Utility.CodeMapping(typeId);
            directCard.CardType = cardType;

            return "1";//
        }
        #endregion

        #region CheckChargeCardDirectDetails
        /// <summary>
        /// 
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public static string CheckChargeCardDirectDetails(ChargeCardDirectDetails details)
        {
            //if (!ChannelType.CheckCardFormat(details.TypeId, details.CardNo, details.CardPwd, decimal.ToInt32(details.Refervalue)))
            //{
            //    details.CardStatus = "7";
            //    details.Msg = "卡密格式不正确";

            //    return "7";//
            //}

            string chanelNo = details.CardType.ToString("0000") +
              decimal.ToInt32(details.Refervalue).ToString(CultureInfo.InvariantCulture);

            var chanelInfo = Channel.GetModel(chanelNo, details.UserId, true);
            if (chanelInfo == null)
            {
                details.CardStatus = "1003";
                details.Msg = chanelNo + "通道不存在";

                return "1003";
            }
            else if (chanelInfo.isOpen != 1)
            {
                details.CardStatus = "1003";
                details.Msg = chanelNo + "通道不开放";

                return "1003";
            }
            else if (!chanelInfo.supplier.HasValue)
            {
                details.CardStatus = "10000";
                details.Msg = chanelNo + "未找到处理接口";

                return "10000";//
            }

            details.ChanelNo = chanelNo;
            details.SupplierId = chanelInfo.supplier.Value;

            CheckAPIParameter chkresult = BLL.Order.Card.Factory.Instance.CheckCardInfo(details.UserId
                , details.UserOrderNo
                , details.TypeId
                , details.CardNo
                , details.CardPwd
                , decimal.ToInt32(details.Refervalue));

            #region 数据库 检查
            if (chkresult == null)
            {
                details.CardStatus = "2014";
                details.Msg = "系统繁忙，请稍后再试";
                return "2014";
            }
            else
            {
                details.ProcessMode = 1;

                switch (chkresult.IsRepeat)
                {
                    case 1:
                        if (chkresult.Makeup == 1)
                        {
                            details.SupplierId = chkresult.Supplierid;
                            details.ProcessMode = 2;//自身处理

                            #region 补单
                            if (String.Equals(chkresult.Cardpwd, details.CardPwd, StringComparison.CurrentCultureIgnoreCase))
                            {
                                if (chkresult.Isclose == 0)
                                {
                                    #region 继续补充
                                    int balance = decimal.ToInt32(chkresult.CardBalance);
                                    if (balance > 0)
                                    {
                                        if (details.Refervalue <= balance)
                                        {
                                            details.CardStatus = "0";
                                            details.ProcessMode = 2;//自身处理
                                        }
                                        else if (details.Refervalue > balance)
                                        {
                                            details.CardStatus = "1007";
                                            details.Msg = "卡内余额不足";

                                            return "1007"; //卡余额不足
                                        }
                                    }
                                    else
                                    {
                                        details.CardStatus = "1007";
                                        details.Msg = "卡内余额不足";

                                        return "1007"; //卡余额不足
                                    }
                                    #endregion
                                }
                                else
                                {
                                    details.CardStatus = "1007";
                                    details.Msg = "卡内余额不足";

                                    return "1007";//不可以继续 补充了
                                }
                            }
                            else
                            {
                                details.CardStatus = "1004";
                                details.Msg = "密码错误或充值卡无效";

                                return "1004";//卡密不对
                            }
                            #endregion
                        }
                        else
                        {
                            details.CardStatus = "0";
                            details.ProcessMode = 1;//通过接口处理
                        }
                        break;
                    case 4:
                    case 5:
                        details.CardStatus = "1010";
                        details.Msg = "卡还在处理中，不可重复提交";

                        return "1010";
                    case 6:
                        details.CardStatus = "10000";
                        details.Msg = "订单号重复";

                        return "10000";
                    case 7:
                        details.CardStatus = "1002";
                        details.Msg = "提交次数过于频繁，1小时内不能超过3次";

                        return "1002";

                    case 8:
                        details.CardStatus = "1007";
                        details.Msg = "充值卡无效";
                        return "1007";
                }
            }
            #endregion

            return "0";//
        }
        #endregion

        #region 检查签名 CheckSign
        /// <summary>
        /// 签名较验
        /// </summary>
        /// <returns></returns>
        public static bool CheckSign(ChargeCardDirectInfo directCard, string apikey)
        {
            string sbOld = "";

            sbOld += "ChargeCardDirect";
            sbOld += directCard.p1_MerId;
            sbOld += directCard.p2_Order;
            sbOld += directCard.p3_Amt;
            sbOld += directCard.p4_verifyAmt;
            sbOld += directCard.p5_Pid;
            sbOld += directCard.p6_Pcat;
            sbOld += directCard.p7_Pdesc;
            sbOld += directCard.p8_Url;
            sbOld += directCard.pa_MP;
            sbOld += directCard.pa7_cardAmt;
            sbOld += directCard.pa8_cardNo;
            sbOld += directCard.pa9_cardPwd;
            sbOld += directCard.pd_FrpId;
            sbOld += directCard.pr_NeedResponse;
            sbOld += directCard.pz_userId;
            sbOld += directCard.pz1_userRegTime;

            viviLib.Logging.LogHelper.Debug("sbOld " + sbOld);

            viviLib.Logging.LogHelper.Debug("apikey " + apikey);

            string localhmac = Lib.Digest.HmacSign(sbOld, apikey);

            viviLib.Logging.LogHelper.Debug("localhmac " + localhmac);

            viviLib.Logging.LogHelper.Debug("hmac " + directCard.hmac);

            return localhmac == directCard.hmac;
        }
        #endregion

        #region GetResponseText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static string GetResponseText(ChargeCardDirentResult info, string apiKey)
        {
            string rHmac = info.R0_Cmd + info.R1_Code + info.R6_Order + info.Rq_ReturnMsg;

            rHmac = Lib.Digest.HmacSign(rHmac, apiKey);

            string text = string.Format("r0_Cmd={1}{0}r1_Code={2}{0}r6_Order={3}{0}rq_ReturnMsg={4}{0}hmac={5}", '\n'
                , info.R0_Cmd
                , info.R1_Code
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


            bool verifyAmt = orderinfo.cus_field4 == "true" ? true : false;
            string r0_Cmd = "ChargeCardDirect";

            string r1Code = "0";
            if (orderinfo.status == 2)
                r1Code = "1";

            string p1MerId = orderinfo.userid.ToString(CultureInfo.InvariantCulture);
            string p2Order = orderinfo.userorder;
            string p3Amt = facevalue.ToString(CultureInfo.InvariantCulture);

            string p4FrpId = orderinfo.cus_field2;
            string p5CardNo = orderinfo.cardNo;//多张卡以半角逗号分隔

            string p6ConfirmAmount = facevalue.ToString(CultureInfo.InvariantCulture); //卡支付的金额组
            string p7RealAmount = orderinfo.cus_field3; //卡原有的金额组

            string p8CardStatus = "1006";//p8_cardStatus//状态组
            if (orderinfo.status == 2)
                p8CardStatus = "0";

            if (verifyAmt && p8CardStatus == "0")
            {
                if (orderinfo.refervalue > facevalue)
                {
                    p8CardStatus = "1";
                    r1Code = "2";
                    p3Amt = "0";
                }
                else if (orderinfo.refervalue <= facevalue)
                {
                    p3Amt = orderinfo.refervalue.ToString("f2");
                }
            }


            string p9_MP = orderinfo.attach;
            string pb_BalanceAmt = "0M";
            string pc_BalanceAct = "";

            string sbOld = "";
            sbOld += r0_Cmd;
            sbOld += r1Code;
            sbOld += p1MerId;
            sbOld += p2Order;
            sbOld += p3Amt;
            sbOld += p4FrpId;
            sbOld += p5CardNo;
            sbOld += p6ConfirmAmount;
            sbOld += p7RealAmount;
            sbOld += p8CardStatus;
            sbOld += p9_MP;
            sbOld += pb_BalanceAmt;
            sbOld += pc_BalanceAct;

            string nhmac = Digest.HmacSign(sbOld, apiKey);

            var parms = new StringBuilder();

            parms.AppendFormat("r0_Cmd={0}", Common.UrlEncode(r0_Cmd));
            parms.AppendFormat("&r1_Code={0}", Common.UrlEncode(r1Code));
            parms.AppendFormat("&p1_MerId={0}", Common.UrlEncode(p1MerId));
            parms.AppendFormat("&p2_Order={0}", Common.UrlEncode(p2Order));
            parms.AppendFormat("&p3_Amt={0}", Common.UrlEncode(p3Amt));
            parms.AppendFormat("&p4_FrpId={0}", Common.UrlEncode(p4FrpId));
            parms.AppendFormat("&p5_CardNo={0}", Common.UrlEncode(p5CardNo));
            parms.AppendFormat("&p6_confirmAmount={0}", Common.UrlEncode(p6ConfirmAmount));
            parms.AppendFormat("&p7_realAmount={0}", Common.UrlEncode(p7RealAmount));
            parms.AppendFormat("&p8_cardStatus={0}", Common.UrlEncode(p8CardStatus));
            parms.AppendFormat("&p9_MP={0}", Common.UrlEncode(p9_MP));
            parms.AppendFormat("&pb_BalanceAmt={0}", Common.UrlEncode(pb_BalanceAmt));
            parms.AppendFormat("&pc_BalanceAct={0}", Common.UrlEncode(pc_BalanceAct));
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

        #region CreateMultiNotifyUrl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderinfo"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static string CreateMultiNotifyUrl(OrderCardTotal orderinfo, string apiKey)
        {
            if (orderinfo == null)
                return string.Empty;

            string notifyUrl = orderinfo.notifyUrl;

            if (string.IsNullOrEmpty(notifyUrl))
                return string.Empty;

            decimal facevalue = orderinfo.successAmt;

            bool verifyAmt = orderinfo.filed1 == "true" ? true : false;

            string r0_Cmd = "ChargeCardDirect";

            string r1Code = "0";
            if (orderinfo.status == 2)
                r1Code = "1";

            string p1MerId = orderinfo.userId.ToString(CultureInfo.InvariantCulture);
            string p2Order = orderinfo.userorderid;
            string p3Amt = facevalue.ToString(CultureInfo.InvariantCulture);
            string p4FrpId = orderinfo.filed1;
            string p5CardNo = orderinfo.cardNos;//多张卡以半角逗号分隔
            string p7RealAmount = orderinfo.filed3;

            DataSet details = BLL.Order.Card.Factory.Instance.GetlistBybatno(orderinfo.orderid);

            string p6ConfirmAmount = "";
            string p8CardStatus = "";

            if (details != null)
            {
                foreach (DataRow row in details.Tables[0].Rows)
                {
                    #region
                    int status = Convert.ToInt32(row["status"]);
                    decimal realvalue = Convert.ToDecimal(row["realvalue"]);
                    decimal refervalue = Convert.ToDecimal(row["refervalue"]);

                    if (status == 2)
                    {
                        p6ConfirmAmount += string.Format("{0:f2}", realvalue);
                        if (verifyAmt)
                        {
                            if (realvalue >= refervalue)
                            {
                                p8CardStatus += "0,";
                            }
                            else
                            {
                                p8CardStatus += "1,";
                            }
                        }
                        else
                        {
                            p8CardStatus += "0,";
                        }
                    }
                    else
                    {
                        p6ConfirmAmount += "0,";
                        p8CardStatus += "1004,";
                    }
                    #endregion
                }
            }

            string p9_MP = orderinfo.attach;
            string pb_BalanceAmt = "0M";
            string pc_BalanceAct = "";

            string sbOld = "";
            sbOld += r0_Cmd;
            sbOld += r1Code;
            sbOld += p1MerId;
            sbOld += p2Order;
            sbOld += p3Amt;
            sbOld += p4FrpId;
            sbOld += p5CardNo;
            sbOld += p6ConfirmAmount;
            sbOld += p7RealAmount;
            sbOld += p8CardStatus;
            sbOld += p9_MP;
            sbOld += pb_BalanceAmt;
            sbOld += pc_BalanceAct;

            string nhmac = Digest.HmacSign(sbOld, apiKey);

            var parms = new StringBuilder();

            parms.AppendFormat("r0_Cmd={0}", Common.UrlEncode(r0_Cmd));
            parms.AppendFormat("&r1_Code={0}", Common.UrlEncode(r1Code));
            parms.AppendFormat("&p1_MerId={0}", Common.UrlEncode(p1MerId));
            parms.AppendFormat("&p2_Order={0}", Common.UrlEncode(p2Order));
            parms.AppendFormat("&p3_Amt={0}", Common.UrlEncode(p3Amt));
            parms.AppendFormat("&p4_FrpId={0}", Common.UrlEncode(p4FrpId));
            parms.AppendFormat("&p5_CardNo={0}", Common.UrlEncode(p5CardNo));
            parms.AppendFormat("&p6_confirmAmount={0}", Common.UrlEncode(p6ConfirmAmount));
            parms.AppendFormat("&p7_realAmount={0}", Common.UrlEncode(p7RealAmount));
            parms.AppendFormat("&p8_cardStatus={0}", Common.UrlEncode(p8CardStatus));
            parms.AppendFormat("&p9_MP={0}", Common.UrlEncode(p9_MP));
            parms.AppendFormat("&pb_BalanceAmt={0}", Common.UrlEncode(pb_BalanceAmt));
            parms.AppendFormat("&pc_BalanceAct={0}", Common.UrlEncode(pc_BalanceAct));
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
