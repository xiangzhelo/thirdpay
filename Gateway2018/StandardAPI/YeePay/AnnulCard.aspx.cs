using System;
using System.Web;
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
    /// 易宝支付产品通用接口 AnnulCard
    /// </summary>
    public partial class AnnulCard : CardTransBase
    {
        protected OrderCard OrderBll = new OrderCard();

        private AnnulCardInfo _info;
        public AnnulCardInfo AnnulCardInfo
        {
            get
            {
                if (_info == null && HttpContext.Current != null)
                {
                    _info = new AnnulCardInfo(HttpContext.Current);
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
                return viviapi.SysInterface.Card.YeePay.AnnulCard.EnName;
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
            var szxresult = new SZXResult
            {
                R0_Cmd = "AnnulCard",
                R2_TrxId = Factory.Instance.GenerateOrderId(OrderPrefix),
                R6_Order = AnnulCardInfo.p2_Order
            };

            string rCode = viviapi.SysInterface.Card.YeePay.AnnulCard.CheckParameter(AnnulCardInfo);
            
            if (rCode != "1")
            {
                #region 记录日志
                if (this.DebuglogOpen)
                {
                    var debugInfo = new viviapi.Model.Sys.debuginfo
                    {
                        userid = AnnulCardInfo.UserId,
                        addtime = DateTime.Now,
                        bugtype = viviapi.Model.Sys.debugtypeenum.卡类订单,
                        errorcode = rCode,
                        errorinfo = AnnulCardInfo.Msg,
                        userorder = AnnulCardInfo.p2_Order,
                        url = Request.RawUrl,
                        detail = ""

                    };
                    viviapi.BLL.Sys.Debuglog.Insert(debugInfo);
                }
                #endregion
            }

            if (rCode == "1")
            {
                var orderInfo = InitOrder(szxresult.R2_TrxId, AnnulCardInfo);
                if (orderInfo == null)
                {
                    rCode = "-1";
                }
                else
                {
                    var suppResponse = new CardSynchCallBack();

                    if (AnnulCardInfo.ProcessMode == 1)
                    {
                        #region 通过接口
                        var supp = (SupplierCode)AnnulCardInfo.SupplierId;

                        suppResponse = OrderCardUtils.SynchSubmit(supp
                               , szxresult.R2_TrxId
                               , AnnulCardInfo.TypeId
                               , AnnulCardInfo.CardNo
                               , AnnulCardInfo.CardPwd
                               , AnnulCardInfo.OrderAmt
                               , string.Empty
                               , 1);

                        if (suppResponse.SummitStatus == 0)
                        {
                            rCode = "-1";

                            string viewMsg = suppResponse.SuppErrorMsg;

                            var response = new CardOrderSupplierResponse()
                            {
                                Sync = 1,
                                SupplierId = AnnulCardInfo.SupplierId,
                                SuppTransNo = suppResponse.SuppTransNo,
                                SysOrderNo = szxresult.R2_TrxId,
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
                            AnnulCardInfo.Msg = "提卡成功，等待处理结果";
                        }
                        #endregion
                    }
                    else
                    {
                        #region 系统自已处理
                        AnnulCardInfo.SupplierId = 0;
                        suppResponse.SuppTransNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        suppResponse.OrderStatus = 2;
                        suppResponse.SuppErrorMsg = "支付成功";
                        suppResponse.SuppErrorCode = "0";
                        suppResponse.SuccAmt = AnnulCardInfo.OrderAmt;
                        #endregion
                    }

                    if (AnnulCardInfo.ProcessMode == 2 || suppResponse.OrderStatus == 2)
                    {
                        #region 系统自处理
                        var resInfo = new CardProcessResultInfo
                        {
                            supplierId = 0,
                            orderid = szxresult.R2_TrxId,
                            supplierOrder = suppResponse.SuppTransNo,
                            status = 2,
                            opstate = "0",
                            msg = suppResponse.SuppErrorMsg,
                            userViewMsg = suppResponse.SuppErrorMsg,
                            tranAMT = suppResponse.SuccAmt,
                            suppAmt = 0M,
                            errtype = "0",
                            method = AnnulCardInfo.ProcessMode,
                            count = 0
                        };
                        AnnulCardInfo.Msg = "提卡成功，等待处理结果";
                        var process = new SystemProcessCard();

                        var tmr = new System.Threading.Timer(process.Process, resInfo, 1000, 0);
                        resInfo.tmr = tmr;
                        #endregion
                    }
                }
                
            }

            szxresult.R1_Code = rCode;
            szxresult.Rq_ReturnMsg = AnnulCardInfo.Msg;

            string text = viviapi.SysInterface.Card.YeePay.AnnulCard.GetResponseText(szxresult,"");

            Response.Write(text);
        }
        #endregion

        #region 初始化订单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        OrderCardInfo InitOrder(string orderid, AnnulCardInfo info)
        {
            try
            {
                var order = new OrderCardInfo
                {
                    ordertype = 1,
                    orderid = orderid,
                    userid = info.UserId,
                    userorder = info.p2_Order,
                    typeId = info.TypeId,
                    cardType = info.CardType,
                    cardNo = info.CardNo,
                    cardPwd = info.CardPwd,
                    paymodeId = info.ChanelNo,
                    refervalue = info.OrderAmt,
                    faceValue = 0M,
                    attach = info.pa_MP,
                    referUrl =
                        HttpContext.Current.Request.UrlReferrer != null
                            ? HttpContext.Current.Request.UrlReferrer.ToString()
                            : string.Empty,
                    clientip = ServerVariables.TrueIP,
                    addtime = DateTime.Now,
                    completetime = DateTime.Now,
                    notifycontext = string.Empty,
                    notifycount = 0,
                    notifystat = 0,
                    notifyurl = info.p8_Url,
                    payRate = 0M,
                    supplierId = info.SupplierId,
                    supplierOrder = string.Empty,
                    server = viviapi.SysConfig.RuntimeSetting.ServerId,
                    manageId = info.ManageId
                };

                order.cardnum = 1;
                order.resultcode = "";
                order.ismulticard = 0;

                order.status = 1;
                order.ovalue = string.Empty;
                order.opstate = "";
                order.msg = info.Msg;
                order.userViewMsg = info.Msg;
                order.errtype = "";
                order.Desc = info.Msg;
                order.version = version;
                order.withhold_type = 0;
                order.makeup = (byte)(info.ProcessMode == 2 ? 1 : 0);

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
