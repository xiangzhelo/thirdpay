using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Order.Card;
using viviapi.ETAPI.Common;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviapi.Model.Order.Card;
using viviapi.Model.supplier;
using viviapi.Model.User;
using viviapi.SysInterface.Card.YeePay;
using viviapi.SysInterface.Card.YeePay.Lib;
using viviapi.WebComponents.GateWay;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviAPI.Gateway2018.StandardAPI.YeePay
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ChargeCardDirect : CardTransBase
    {
        protected OrderCard OrderBll = new OrderCard();

        private ChargeCardDirectInfo _info;
        public ChargeCardDirectInfo DirectCardInfo
        {
            get
            {
                if (_info == null && HttpContext.Current != null)
                {
                    _info = new ChargeCardDirectInfo(HttpContext.Current);
                }
                return _info;
            }
        }

        #region 版本

        /// <summary>
        /// 1.0
        /// </summary>
        public string version
        {
            get
            {
                return viviapi.SysInterface.Card.YeePay.ChargeCardDirect.EnName;
            }
        }

        #endregion

        /// <summary>
        /// 0：销卡成功，订单成功
        ///1：销卡成功，订单失败
        ///7：卡号卡密或卡面额不符合规则
        ///1002：本张卡密您提交过于频繁，请您稍后再试
        ///1003：不支持的卡类型（比如电信地方卡）
        ///1004：密码错误或充值卡无效
        ///1006：充值卡无效
        ///1007：卡内余额不足
        ///1008：余额卡过期（有效期1个月）
        ///1010：此卡正在处理中
        ///10000：未知错误
        ///2005：此卡已使用
        ///2006：卡密在系统处理中
        ///2007：该卡为假卡
        ///2008：该卡种正在维护
        ///2009：浙江省移动维护
        ///2010：江苏省移动维护
        ///2011：福建省移动维护
        ///2012：辽宁省移动维护
        ///2013：该卡已被锁定
        ///2014：系统繁忙，请稍后再试

        ///下面为易宝e卡通返回的错误代码
        ///3001：卡不存在
        ///3002：卡已使用过
        ///3003：卡已作废
        ///3004：卡已冻结
        ///3005：卡未激活
        ///3006：密码不正确
        ///3007：卡正在处理中
        ///3101：系统错误
        ///3102：卡已过期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Process();
        }

        #region Process
        /// <summary>
        /// 
        /// </summary>
        private void Process()
        {
            var szxresult = new ChargeCardDirentResult { R0_Cmd = "ChargeCardDirect", R6_Order = DirectCardInfo.p2_Order };

            string rCode = viviapi.SysInterface.Card.YeePay.ChargeCardDirect.CheckParameter(DirectCardInfo);

            if (rCode != "1")
            {
                #region 记录日志

                if (this.DebuglogOpen)
                {
                    var debugInfo = new viviapi.Model.Sys.debuginfo
                    {
                        userid = DirectCardInfo.UserId,
                        addtime = DateTime.Now,
                        bugtype = viviapi.Model.Sys.debugtypeenum.卡类订单,
                        errorcode = rCode,
                        errorinfo = DirectCardInfo.Msg,
                        userorder = DirectCardInfo.p2_Order,
                        url = Request.RawUrl,
                        detail = ""

                    };
                    viviapi.BLL.Sys.Debuglog.Insert(debugInfo);
                }

                #endregion
            }
            else
            {
                string sysOrderNo = Factory.Instance.GenerateOrderId(OrderPrefix);

                bool initTotal = true;

                if (DirectCardInfo.CardNum > 1)
                {
                    initTotal = InitTotalOrder(sysOrderNo, DirectCardInfo);
                }
                //成功提交个数
                int succSummit = 0;

                if (initTotal)
                {
                    for (int i = 0; i < DirectCardInfo.CardNum; i++)
                    {
                        #region 明细项

                        var item = new ChargeCardDirectDetails
                        {
                            TypeId = DirectCardInfo.TypeId,
                            CardType = DirectCardInfo.CardType,
                            UserId = DirectCardInfo.UserId,
                            ManageId = DirectCardInfo.ManageId,
                            APIkey = DirectCardInfo.APIkey,
                            CardNo = DirectCardInfo.CardNos[i],
                            CardPwd = DirectCardInfo.CardPwds[i],
                            Refervalue = DirectCardInfo.CardFaceValues[i],
                            SerialNumber = i.ToString("00")
                        };

                        if (DirectCardInfo.CardNum > 1)
                        {
                            item.UserOrderNo = DirectCardInfo.p2_Order + "_" + item.SerialNumber;
                            item.SysOrderNo = sysOrderNo + "_" + item.SerialNumber;
                        }
                        else
                        {
                            item.UserOrderNo = DirectCardInfo.p2_Order;
                            item.SysOrderNo = sysOrderNo;
                        }

                        string chk =
                            viviapi.SysInterface.Card.YeePay.ChargeCardDirect.CheckChargeCardDirectDetails(item);

                        if (chk == "0")
                        {
                            var orderInfo = InitOrder(DirectCardInfo.CardNum,sysOrderNo, item.SysOrderNo, item);
                            if (orderInfo == null)
                            {
                                rCode = "-1";
                                DirectCardInfo.Msg = "系统故障，请联系商务";
                                break;
                            }
                            else
                            {
                                var suppResponse = new CardSynchCallBack();

                                if (item.ProcessMode == 1)
                                {
                                    #region 通过接口

                                    var supp = (SupplierCode) item.SupplierId;

                                    suppResponse = OrderCardUtils.SynchSubmit(supp
                                        , item.SysOrderNo
                                        , item.TypeId
                                        , item.CardNo
                                        , item.CardPwd
                                        , decimal.ToInt32(item.Refervalue)
                                        , string.Empty
                                        , 1);

                                    if (suppResponse.SummitStatus == 0)
                                    {
                                        item.CardStatus = "-1";

                                        string viewMsg = suppResponse.SuppErrorMsg;

                                        var response = new CardOrderSupplierResponse()
                                        {
                                            Sync = 1,
                                            SupplierId = item.SupplierId,
                                            SuppTransNo = suppResponse.SuppTransNo,
                                            SysOrderNo = item.SysOrderNo,
                                            OrderAmt = 0M,
                                            SuppAmt = 0M,
                                            OrderStatus = 4,
                                            SuppErrorCode = suppResponse.SuppErrorCode,
                                            Opstate = rCode,
                                            SuppErrorMsg = suppResponse.SuppErrorMsg,
                                            ViewMsg = viewMsg,
                                            Method = 1
                                        };

                                        OrderCardUtils.FinishForSync(orderInfo, response);
                                    }
                                    else
                                    {
                                        succSummit++;

                                        item.CardStatus = "0";
                                        item.Msg = "提卡成功，等待处理结果";
                                    }

                                    #endregion
                                }
                                else
                                {
                                    succSummit++;

                                    #region 系统自已处理

                                    item.SupplierId = 0;
                                    suppResponse.SuppTransNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                                    suppResponse.OrderStatus = 2;
                                    suppResponse.SuppErrorMsg = "支付成功";
                                    suppResponse.SuppErrorCode = "0";
                                    suppResponse.SuccAmt = decimal.ToInt32(item.Refervalue);

                                    #endregion
                                }

                                if (item.ProcessMode == 2 || suppResponse.OrderStatus == 2)
                                {
                                    #region 系统自处理

                                    var resInfo = new CardProcessResultInfo
                                    {
                                        supplierId = 0,
                                        orderid = item.SysOrderNo,
                                        supplierOrder = suppResponse.SuppTransNo,
                                        status = 2,
                                        opstate = "0",
                                        msg = suppResponse.SuppErrorMsg,
                                        userViewMsg = suppResponse.SuppErrorMsg,
                                        tranAMT = suppResponse.SuccAmt,
                                        suppAmt = 0M,
                                        errtype = "0",
                                        method = item.ProcessMode,
                                        count = 0
                                    };
                                    item.Msg = "提卡成功，等待处理结果";
                                    var process = new SystemProcessCard();

                                    var tmr = new System.Threading.Timer(process.Process, resInfo, 1000, 0);
                                    resInfo.tmr = tmr;

                                    #endregion
                                }
                            }
                        }
                        
                        #endregion
                    }
                    if (succSummit == 0)
                    {
                        rCode = "-1";
                        DirectCardInfo.Msg = "未有成功提卡记录";
                    }
                    else
                    {
                        rCode = "1";
                        DirectCardInfo.Msg = "接收成功，等待结果";
                    }
                }
                else
                {
                    rCode = "-1";
                    DirectCardInfo.Msg = "系统繁忙，请稍后再试";
                }

            }

            szxresult.R1_Code = rCode;
            szxresult.Rq_ReturnMsg = DirectCardInfo.Msg;

            string text = viviapi.SysInterface.Card.YeePay.ChargeCardDirect.GetResponseText(szxresult, "");

            Response.Write(text);
        }
        #endregion

        #region 初始化总表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="directInfo"></param>
        /// <returns></returns>
        bool InitTotalOrder(string orderid, ChargeCardDirectInfo directInfo)
        {
            var total = new viviapi.Model.Order.Card.OrderCardTotal
            {
                orderid = orderid,
                status = 1,
                userorderid = directInfo.p2_Order,
                userId = directInfo.UserId,
                typeId = directInfo.TypeId,
                cardType = directInfo.CardType,
                cardNos = directInfo.pa8_cardNo,
                cardPwds = directInfo.pa9_cardPwd,
                cardNum = directInfo.CardNum,
                orderAmt = directInfo.OrderAmt,
                success = 0,
                successAmt = 0M,
                cardStatus = "",
                realAmts = "",
                notifystatus = 1,
                notifyUrl = directInfo.p8_Url,
                referUrl = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : string.Empty,
                version = version,
                filed1 = directInfo.p4_verifyAmt,
                filed2 = directInfo.pd_FrpId,
                filed3 = directInfo.pa7_cardAmt,
                attach = directInfo.pa_MP
            };

            return viviapi.BLL.Order.Card.OrderCardTotal.Instance.Add(total) > 0;
        }
        #endregion

        #region 初始化订单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="batno"></param>
        /// <param name="orderid"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        OrderCardInfo InitOrder(int cardNum,string batno,string orderid, ChargeCardDirectDetails info)
        {
            try
            {
                var order = new OrderCardInfo
                {
                    ordertype = 1,
                    orderid = orderid,
                    Batno=batno,
                    userid = info.UserId,
                    userorder = info.UserOrderNo,
                    typeId = info.TypeId,
                    cardType = info.CardType,
                    cardNo = info.CardNo,
                    cardPwd = info.CardPwd,
                    paymodeId = info.ChanelNo,
                    refervalue = info.Refervalue,
                    faceValue = 0M,
                    attach = "",
                    referUrl = "",
                    clientip = ServerVariables.TrueIP,
                    addtime = DateTime.Now,
                    completetime = DateTime.Now,
                    notifycontext = string.Empty,
                    notifycount = 0,
                    notifystat = 0,
                    notifyurl = "",
                    payRate = 0M,
                    supplierId = info.SupplierId,
                    supplierOrder = string.Empty,
                    server = viviapi.SysConfig.RuntimeSetting.ServerId,
                    manageId = info.ManageId,
                    cardnum = cardNum,
                    resultcode = "",
                    ismulticard = 1,
                    status = 1,
                    ovalue = string.Empty,
                    opstate = "",
                    msg = info.Msg,
                    userViewMsg = info.Msg,
                    errtype = "",
                    Desc = info.Msg,
                    version = version,
                    withhold_type = 0,
                    makeup = (byte) (info.ProcessMode == 2 ? 1 : 0)

                };

                if (cardNum == 1)
                {
                    order.attach = DirectCardInfo.pa_MP;

                    order.notifyurl = DirectCardInfo.p8_Url;
                    order.ismulticard = 0;

                    order.cus_field1 = DirectCardInfo.p4_verifyAmt;
                    order.cus_field2 = DirectCardInfo.pd_FrpId;
                    order.cus_field3 = DirectCardInfo.pa7_cardAmt;

                }

                if (order.manageId <= 0)
                {
                    order.agentId = viviapi.BLL.User.Factory.GetPromID(info.UserId);
                }

                if (info.ProcessMode == 1)
                {
                    viviapi.Cache.WebCache.GetCacheService().AddObject(order.orderid, order, ExpiresTime);
                }

                OrderBll.Insert(order);

                return order;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion
    }
}