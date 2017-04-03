using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using viviapi.Model;
using viviapi.Model.Common;
using viviapi.Model.Order;
using viviapi.Model.User;

namespace viviapi.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class CardSynchCallBack
    {
        private DateTime _initTime = DateTime.Now;

        public DateTime InitTime
        {
            get
            {
                return _initTime;
            }
            set
            {
                _initTime = value;
            }

        }

        private int _supplierId = 0;
        public int SupplierId
        {
            get
            {
                return _supplierId;
            }
            set
            {
                _supplierId = value;
            }

        }

        public CardSynchCallBack()
        {
            Success = 0;
            SummitStatus = 0;
            Message = "";
            SuppTransNo = "";
            SuppCallBackText = "";
            OrderStatus = 1;
        }

        /// <summary>
        /// 提交成功 
        /// 提交过程是否出现网络通讯错误
        /// </summary>
        public byte Success { get; set; }

        /// <summary>
        /// 订单提交状态
        /// </summary>
        public byte SummitStatus { get; set; }

        /// <summary>
        /// 订单处理状态
        /// </summary>
        public byte OrderStatus { get; set; }


        /// <summary>
        /// 接口商 返回订单号
        /// </summary>
        public string SuppTransNo { get; set; }

        /// <summary>
        /// 接口商返回文本
        /// </summary>
        public string SuppCallBackText { get; set; }

        /// <summary>
        /// 接口商返回状态码
        /// </summary>
        public string SuppErrorCode { get; set; }

        /// <summary>
        /// 接口商返回消息
        /// </summary>
        public string SuppErrorMsg { get; set; }

        /// <summary>
        /// 系统错误日志
        /// </summary>
        public string Message { get; set; }
    }

    public class CardOrderSummitArgs
    {
        public CardOrderSummitArgs()
        {
            Source = 1;
        }

        public string SysOrderNo { get; set; }
        public byte Source { get; set; }
        public int CardTypeId { get; set; }
        public string CardNo { get; set; }
        public string CardPass { get; set; }
        public int FaceValue { get; set; }
        public string Attach { get; set; }
    }

    public class CardOrderSupplierResponse
    {
        public int SupplierId { get; set; }
        public string SuppTransNo { get; set; }
        public string SysOrderNo { get; set; }
        public decimal OrderAmt { get; set; }
        public decimal SuppAmt { get; set; }
        public int OrderStatus { get; set; }
        public string SuppErrorCode { get; set; }
        public string Opstate { get; set; }
        public string SuppErrorMsg { get; set; }
        public string ViewMsg { get; set; }
        public byte ContinueSubmit { get; set; }
        public byte Method { get; set; }
    }

    public class OrderCardUtils
    {
        static viviapi.BLL.OrderCard bllCard = new OrderCard();
        static DAL.OrderCard dal2 = new viviapi.DAL.OrderCard();
        static DAL.Order.Card.cardwithholds withholdDAL = new viviapi.DAL.Order.Card.cardwithholds();
        static BLL.OrderCardNotify _notify = new viviapi.BLL.OrderCardNotify();

        #region Finish
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        public static bool Finish(CardOrderSupplierResponse response)
        {
            try
            {
                byte continueSubmit = 0;
                if (response.OrderStatus != 2 && response.Method == 1)
                {
                    var suppInfo = SupplierFactory.GetCacheModel(response.SupplierId);
                    if (suppInfo != null)
                    {
                        if (!string.IsNullOrEmpty(suppInfo.AsynsRetCode))
                        {
                            string[] arr = suppInfo.AsynsRetCode.Split(',');
                            if (arr.Any(s => s == response.SuppErrorCode))
                            {
                                continueSubmit = 1;
                            }
                        }
                    }
                }

                int seq = 1,continueSupp=0;
                string cacheKey = "ReceiveSuppResult" + response.SysOrderNo + response.SupplierId.ToString();
                object flag = HttpRuntime.Cache[cacheKey];
                if (flag == null)
                {
                    DataRow resultRow = dal2.CallbackInsert(response.SysOrderNo
                        , response.SupplierId
                        , response.OrderStatus
                        , response.SuppErrorCode
                        , response.SuppErrorMsg
                        , continueSubmit);

                    if (resultRow != null)
                    {
                        seq = Convert.ToInt32(resultRow["seq"]);
                        continueSupp = Convert.ToInt32(resultRow["continueSupp"]);
                    }

                    if (continueSubmit == 1 && continueSupp > 0)
                    {

                    }
                    else
                    {
                        bool processFlag = (seq == 1) || ((seq > 1) && (response.OrderStatus == 2));

                        if (processFlag)
                        {
                            UpdateOrder(seq, response);
                        } 
                    }
                   

                    HttpRuntime.Cache.Insert(cacheKey, response.OrderStatus, null, DateTime.Now.AddSeconds(10.0), TimeSpan.Zero);
                }

                if (seq == 1)
                {
                    _notify.DoNotify(response.SysOrderNo);
                }

                Cache.WebCache.GetCacheService().RemoveObject(response.SysOrderNo);

                return true;

            }
            catch (Exception exception)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(exception);

                return false;

            }
        }
        #endregion

        public static void SuppNotify(CardOrderSupplierResponse response, string succflag)
        {
            bool execResult = Finish(response);

            if (execResult == true)
            {
                HttpContext.Current.Response.Write(succflag);
            }
            else
            {
                HttpContext.Current.Response.Write("fail");
            }
        }

        public static void UpdateOrder(int seq, CardOrderSupplierResponse response)
        {
            var orderInfo = Cache.WebCache.GetCacheService().RetrieveObject(response.SysOrderNo) as OrderCardInfo;
            if (orderInfo == null)
            {
                orderInfo = bllCard.GetModelByOrderId(response.SysOrderNo);
            }
            if (orderInfo != null)
            {
                #region
                orderInfo.supplierId = response.SupplierId;

                UserInfo userInfo
                    = BLL.User.UserFactory.GetCacheUserBaseInfo(orderInfo.userid);

                orderInfo.method = response.Method;
                orderInfo.cardversion = userInfo.cardversion;
                orderInfo.orderid = response.SysOrderNo;
                orderInfo.status = response.OrderStatus;
                orderInfo.opstate = response.Opstate;
                orderInfo.msg = response.SuppErrorMsg;
                orderInfo.supplierOrder = "";
                if (!string.IsNullOrEmpty(response.SuppTransNo))
                    orderInfo.supplierOrder = response.SuppTransNo;

                orderInfo.errtype = response.SuppErrorCode;
                orderInfo.withhold_type = 0;
                orderInfo.withholdAmt = 0M;
                orderInfo.faceValue = response.OrderAmt;
                orderInfo.userViewMsg = response.ViewMsg;

                #region 是否扣单
                if (true && BLL.SysConfig.isOpenDeduct && response.OrderStatus == 2)
                {
                    if (new Random(Guid.NewGuid().GetHashCode()).Next(1, 1000) <= userInfo.CPSDrate)
                    {
                        orderInfo.status = 8;
                    }
                }
                #endregion

                orderInfo.realvalue = response.OrderAmt;
                orderInfo.supplierId = response.SupplierId;
                orderInfo.completetime = DateTime.Now;

                orderInfo.payRate = 0M;
                orderInfo.payAmt = 0M;
                orderInfo.supplierRate = 0M;
                orderInfo.supplierAmt = 0M;
                orderInfo.promRate = 0M;
                orderInfo.promAmt = 0M;
                orderInfo.profits = 0M;

                if (response.OrderStatus == 2)
                {
                    #region 商户费率

                    if (orderInfo.payRate <= 0M)
                    {
                        if (orderInfo.ordertype == 8)
                        {
                            orderInfo.payRate = BLL.Channel.Channelsupplier.GetPayRate(orderInfo.typeId,
                                orderInfo.supplierId);
                        }
                        if (orderInfo.payRate <= 0M)
                        {
                            orderInfo.payRate = BLL.Payment.PayRateFactory.GetUserPayRate(userInfo, orderInfo.userid,
                                orderInfo.typeId);
                        }
                    }
                    orderInfo.payAmt = orderInfo.payRate * response.OrderAmt;

                    #endregion

                    #region 平台费率

                    if (response.SuppAmt > 0)
                    {
                        orderInfo.supplierRate = response.SuppAmt / orderInfo.refervalue;
                        orderInfo.supplierAmt = response.SuppAmt;
                    }
                    else
                    {
                        decimal suppRate = SupplierPayRateFactory.GetRate(response.SupplierId, orderInfo.typeId);
                        orderInfo.supplierRate = suppRate;
                        orderInfo.supplierAmt = suppRate * response.OrderAmt;
                    }

                    #endregion

                    #region 代理

                    //代理
                    if (orderInfo.agentId > 0)
                    {
                        //代理费率
                        orderInfo.promRate = BLL.Payment.PayRateFactory.GetUserPayRate(orderInfo.agentId,
                            orderInfo.typeId);
                        //代理金额
                        orderInfo.promAmt = (orderInfo.promRate - orderInfo.payRate) * response.OrderAmt;

                        if (orderInfo.promAmt < 0)
                            orderInfo.promAmt = 0;
                    }
                    orderInfo.profits = orderInfo.supplierAmt - orderInfo.payAmt - orderInfo.promAmt;

                    #endregion

                    if (seq == 1)
                    {
                        if (orderInfo.cardversion == 1)
                        {
                            #region 扣卡面值

                            if (orderInfo.refervalue < response.OrderAmt) //小提大
                            {
                                #region 小提大

                                orderInfo.realvalue = orderInfo.refervalue;
                                orderInfo.withhold_type = 1;
                                orderInfo.withholdAmt = response.OrderAmt - orderInfo.refervalue;

                                orderInfo.payAmt = orderInfo.payRate * orderInfo.refervalue;
                                orderInfo.promAmt = (orderInfo.promRate - orderInfo.payRate) * orderInfo.refervalue;

                                orderInfo.supplierAmt = orderInfo.supplierRate * orderInfo.refervalue;
                                orderInfo.profits = orderInfo.supplierAmt - orderInfo.payAmt - orderInfo.promAmt;

                                if (orderInfo.promAmt < 0)
                                    orderInfo.promAmt = 0;

                                #endregion
                            }
                            else if (orderInfo.refervalue > response.OrderAmt)
                            {
                                #region 大提小

                                orderInfo.withhold_type = 2;

                                orderInfo.status = 4;
                                orderInfo.errtype = "10";
                                orderInfo.opstate = "10";
                                orderInfo.userViewMsg = "充值卡卡号或者密码无效";
                                orderInfo.realvalue = 0M;

                                orderInfo.withholdAmt = response.OrderAmt;
                                orderInfo.payAmt = 0M;
                                orderInfo.promAmt = 0M;

                                orderInfo.withholdAmt = response.OrderAmt;
                                orderInfo.profits = 0M;

                                if (orderInfo.promAmt < 0)
                                    orderInfo.promAmt = 0;

                                #endregion
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        withholdDAL.Insert(orderInfo);
                    }
                }

                if (seq == 1)
                {
                    bllCard.Complete(orderInfo);
                }
                #endregion
            }
        }
      
    }
}
