using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using viviapi.BLL;
using viviapi.BLL.Order.Card;
using viviapi.BLL.Supplier;
using viviapi.ETAPI.Card51;
using viviapi.Model;
using viviapi.Model.Order;
using viviapi.Model.Order.Card;
using viviapi.Model.supplier;
using viviapi.Model.User;
using viviapi.SysInterface.Card;
using viviLib.ExceptionHandling;
using OrderCardSend = viviapi.Model.Order.OrderCardSend;

namespace viviapi.ETAPI.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderCardUtils
    {
        static readonly BLL.Order.Card.OrderCardSend CardSendBll = new BLL.Order.Card.OrderCardSend();
        static readonly BLL.OrderCard BllCard = new OrderCard();
        static readonly BLL.Order.Cardwithholds WithholdBll = new viviapi.BLL.Order.Cardwithholds();

        #region SendCard
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static CardSynchCallBack SendCard(
              SupplierCode supplier
            , CardOrderSummitArgs o)
        {

            var cardsend = new OrderCardSend()
            {
                orderid = o.SysOrderNo,
                source = o.Source,
                suppId = (int)supplier,
                success = 0,
                summitStatus = 0,
                orderStatus = 1,
                typeid = o.CardTypeId,
                facevalue = o.FaceValue,
                cardno = o.CardNo,
                cardpass = o.CardPass,
                suppTransNo = "",
                errCode = "",
                errMsg = "",
                responseText = "",
                initTime = DateTime.Now,
                message = ""
            };

            var callback = new CardSynchCallBack();

            switch (supplier)
            {
                case SupplierCode.OfCard:
                    callback = OfCard.Card.Default.CardSend(o);
                    break;
                case SupplierCode.Cared70:
                    callback = Cared70.Card.Default.CardSend(o);
                    break;
                #region 暂时用不到的接口
                //case SupplierCode.Card51:
                //    callback = Card.Default.CardSend(o);
                //    break;

                //case SupplierCode.HuiYuan:
                //    callback = HuiYuan.Card.Default.CardSend(o);
                //    break;

                //case SupplierCode.HuiSu:
                //    callback = HuiSu.Card.Default.CardSend(o);
                //    break;

                //case SupplierCode.Shengpay:
                //    callback = Shengpay.Card.Default.CardSend(o);
                //    break;

                //case SupplierCode.Card60866:
                //    callback = Card60866.Card.Default.CardSend(o);
                //    break;

                //case SupplierCode.TaoShang:
                //    callback = TaoShang.Card.Default.CardSend(o);
                //    break;

                case SupplierCode.Card15173:
                    callback = Card15173.Card.Default.CardSend(o);
                    break;
                    #endregion
            }

            cardsend.orderStatus = callback.OrderStatus;
            cardsend.errCode = callback.SuppErrorCode;
            cardsend.errMsg = callback.SuppErrorMsg;
            cardsend.responseText = callback.SuppCallBackText;
            cardsend.suppTransNo = callback.SuppTransNo;
            cardsend.success = callback.Success;
            cardsend.summitStatus = callback.SummitStatus;
            cardsend.completeTime = DateTime.Now;

            CardSendBll.Add(cardsend);

            return callback;
        }
        #endregion

        #region SynchSubmit
        /// <summary>
        /// 提交到点卡供应商接口
        /// </summary>
        /// <param name="supplier">供应商编号</param>
        /// <param name="orderid">订单ID</param>
        /// <param name="cardtype">卡类型</param>
        /// <param name="cardno">卡号</param>
        /// <param name="cardpass">卡密</param>
        /// <param name="attach"></param>
        /// <param name="facevalue">面值</param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static CardSynchCallBack SynchSubmit(
            SupplierCode supplier
            , string orderid
            , int cardtype
            , string cardno
            , string cardpass
            , int facevalue
            , string attach
            , byte source)
        {
            var args = new CardOrderSummitArgs()
            {
                SysOrderNo = orderid,
                CardTypeId = cardtype,
                CardNo = cardno,
                CardPass = cardpass,
                FaceValue = facevalue,
                Attach = attach,
                Source = source
            };

            var callBack = SendCard(supplier, args);

            if (callBack.Success == 0)
            {
                //todo:点卡切换通道提交，修改为按优先级高低排序
                #region 切换通道多次提交
                var supplierInfo = viviapi.BLL.Supplier.Factory.GetCacheModel((int)supplier);
                var channelInfo = BLL.Channel.ChannelType.GetCacheModel(cardtype);

                if (channelInfo != null && supplierInfo != null)
                {
                    if (callBack.Success == 0 || CheckRetCode(callBack.SuppErrorCode, supplierInfo))
                    {
                        string suppList = channelInfo.SuppsWhenExceOccurred;
                        if (!string.IsNullOrEmpty(suppList))
                        {
                            string[] arr = suppList.Split(',');

                            foreach (string s in arr)
                            {
                                try
                                {
                                    callBack.SupplierId = int.Parse(s);

                                    var supplier2 = (SupplierCode)callBack.SupplierId;

                                    if (supplier2 == supplier)
                                    {
                                        continue;
                                    }
                                    callBack = SendCard(supplier2, args);

                                    if (callBack.Success == 1 && !CheckRetCode(callBack.SuppErrorCode, supplierInfo))
                                    {
                                        break;
                                    }
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
                #endregion
            }

            return callBack;
        }

        private static bool CheckRetCode(string code, SupplierInfo supplierInfo)
        {
            bool exists = false;

            if (!string.IsNullOrEmpty(supplierInfo.SynsRetCode))
            {
                string[] arr = supplierInfo.SynsRetCode.Split(',');

                foreach (string s in arr)
                {
                    if (s == code)
                    {
                        exists = true;
                    }
                }
            }

            return exists;
        }

        #endregion

        #region Finish
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        public static bool Finish(CardOrderSupplierResponse response)
        {
            string cacheKey = "OrderBankComplete" + response.SysOrderNo + response.SupplierId.ToString(CultureInfo.InvariantCulture);

            object flag = Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);
            if (flag != null)
                return true;

            var orderInfo = Cache.WebCache.GetCacheService().RetrieveObject(response.SysOrderNo) as OrderCardInfo;
            if (orderInfo == null)
            {
                orderInfo = BLL.Order.Card.Factory.Instance.GetModelByOrderId(response.SysOrderNo);
            }
            if (orderInfo == null)
            {
                return false;
            }

            try
            {
                byte continueSubmit = 0;

                int seq = 1, continueSupp = 0;

                bool processFlag = true;

                if (response.Method == 1)
                {
                    if (response.OrderStatus != 2)
                    {
                        #region 继续提交
                        var suppInfo = BLL.Supplier.Factory.GetCacheModel(response.SupplierId);
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
                        #endregion
                    }

                    DataRow resultRow = BLL.Order.Card.Factory.Instance.CallbackInsert(response.SysOrderNo
                        , response.SupplierId
                        , response.OrderStatus
                        , response.SuppErrorCode
                        , response.SuppErrorMsg
                        , continueSubmit);

                    if (resultRow != null)
                    {
                        seq = Convert.ToInt32(resultRow["seq"]);
                        continueSupp = Convert.ToInt32(resultRow["suppId"]);
                    }

                    if (continueSubmit == 1 && continueSupp > 0)
                    {
                        #region 再次提交
                        try
                        {
                            if (resultRow != null)
                            {
                                var o = new CardOrderSummitArgs()
                                {
                                    SysOrderNo = response.SysOrderNo,
                                    CardTypeId = Convert.ToInt32(resultRow["typeid"]),
                                    CardNo = resultRow["cardno"].ToString(),
                                    CardPass = resultRow["cardpass"].ToString(),
                                    FaceValue = decimal.ToInt32(Convert.ToDecimal(resultRow["facevalue"])),
                                    Attach = "",
                                    Source = 2
                                };
                                var callBack = SendCard((SupplierCode)continueSupp, o);
                                if (callBack.SummitStatus == 1)
                                {
                                    processFlag = false;
                                }
                            }
                        }
                        catch
                        {
                        }
                        #endregion
                    }
                    else
                    {
                        processFlag = (seq == 1) || ((seq > 1) && (response.OrderStatus == 2));
                    }
                }

                if (processFlag)
                {
                    orderInfo = UpdateOrder(seq, orderInfo, response);

                    if (seq == 1 && orderInfo != null)
                    {
                        //APINotification.DoAsynchronousNotify(orderInfo);

                        APINotification.SynchronousNotifyX(orderInfo);
                    }
                }
                Cache.WebCache.GetCacheService().AddObject(cacheKey, response.OrderStatus, 5);

                return true;

            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);

                return false;

            }
        }
        #endregion

        #region SuppNotify
        /// <summary>
        /// 更新订单状态，并返回是否成功的标志码
        /// </summary>
        /// <param name="response"></param>
        /// <param name="succflag"></param>
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
        #endregion

        #region UpdateOrder
        /// <summary>
        /// 
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="orderInfo"></param>
        /// <param name="response"></param>
        public static OrderCardInfo UpdateOrder(int seq, OrderCardInfo orderInfo, CardOrderSupplierResponse response)
        {
            if (orderInfo == null)
                return null;

            if (response.Method == 1)
            {
                #region 接口处理
                orderInfo.supplierId = response.SupplierId;

                UserInfo userInfo
                    = BLL.User.Factory.GetCacheUserBaseInfo(orderInfo.userid);

                orderInfo.method = 1;
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

                if (true && BLL.Sys.TransactionSettings.OpenDeduct && response.OrderStatus == 2)
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
                            orderInfo.payRate = BLL.Finance.PayRate.Instance.GetUserPayRate(orderInfo.userid,
                                orderInfo.typeId);
                        }
                    }
                    orderInfo.payAmt = orderInfo.payRate * response.OrderAmt;

                    #endregion

                    #region 供应商给平台的费率

                    if (response.SuppAmt > 0)
                    {
                        orderInfo.supplierRate = response.SuppAmt / response.OrderAmt;
                        orderInfo.supplierAmt = response.SuppAmt;
                    }
                    else
                    {
                        decimal suppRate = viviapi.BLL.Finance.PayRate.Instance.GetSupplierPayRate(response.SupplierId,
                            orderInfo.typeId);
                        orderInfo.supplierRate = suppRate;
                        orderInfo.supplierAmt = suppRate * response.OrderAmt;
                    }

                    #endregion

                    #region 代理

                    //代理
                    if (orderInfo.agentId > 0)
                    {
                        //代理费率
                        orderInfo.promRate = BLL.Finance.PayRate.Instance.GetUserPayRate(orderInfo.agentId,
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

                            if (orderInfo.refervalue < response.OrderAmt) //提交金额小于实际面额
                            {
                                #region 小提大

                                orderInfo.realvalue = orderInfo.refervalue;//充值金额为实际提交面额
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
                                #region 大提小,用户提交金额大于卡面值时

                                orderInfo.withhold_type = 2;

                                //orderInfo.realvalue = orderInfo.payRate * response.OrderAmt;

                                orderInfo.withholdAmt = response.OrderAmt;

                                //orderInfo.payAmt = orderInfo.payRate * response.OrderAmt;
                                //orderInfo.promAmt = (orderInfo.promRate - orderInfo.payRate) * response.OrderAmt;

                                //orderInfo.supplierAmt = orderInfo.supplierRate * response.OrderAmt;
                                //orderInfo.profits = orderInfo.supplierAmt - orderInfo.payAmt - orderInfo.promAmt;


                                //以下为原来系统的
                                //orderInfo.status = 4;
                                //orderInfo.errtype = "10";
                                //orderInfo.opstate = "10";
                                //orderInfo.userViewMsg = "充值卡卡号或者密码无效";
                                //orderInfo.realvalue = 0M;

                                //orderInfo.withholdAmt = response.OrderAmt;
                                //orderInfo.payAmt = 0M;
                                //orderInfo.promAmt = 0M;

                                //orderInfo.withholdAmt = response.OrderAmt;
                                //orderInfo.profits = 0M;

                                if (orderInfo.promAmt < 0)
                                    orderInfo.promAmt = 0;

                                #endregion
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        WithholdBll.Insert(orderInfo);
                    }
                }

                if (seq == 1)
                {
                    BllCard.Complete(orderInfo);
                }

                #endregion
            }
            else if (response.Method == 2)
            {
                #region 系统处理
                orderInfo.realvalue = orderInfo.refervalue;
                orderInfo.status = response.OrderStatus;
                orderInfo.opstate = response.Opstate;
                orderInfo.errtype = response.SuppErrorCode;
                orderInfo.msg = response.SuppErrorMsg;
                orderInfo.userViewMsg = response.ViewMsg;

                orderInfo = BLL.Order.Card.Factory.Instance.SystemHandleOrder(orderInfo);
                #endregion
            }

            return orderInfo;
        }
        #endregion

        #region FinishForSync
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <param name="response"></param>
        public static bool FinishForSync(OrderCardInfo orderInfo, CardOrderSupplierResponse response)
        {
            if (orderInfo == null)
            {
                return false;
            }

            try
            {
                byte continueSubmit = 0;

                int seq = 1;

                bool processFlag = true;

                UpdateOrder(seq, orderInfo, response);

                Cache.WebCache.GetCacheService().RemoveObject(response.SysOrderNo);

                return true;

            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);

                return false;

            }
        }
        #endregion

        #region QueryOrder
        /// <summary>
        /// 通过接口查询订单
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="facevalue"></param>
        /// <param name="orderid"></param>
        public static void QueryOrder(int supplierId, int facevalue, string orderid)
        {
            if (supplierId > 0 && facevalue > 0 && !string.IsNullOrEmpty(orderid))
            {
                try
                {
                    //int supplierId = Convert.ToInt32(row["supplierID"]);
                    //int facevalue = Convert.ToInt32(row["facevalue"]);
                    //string orderid = row["orderid"].ToString();

                    switch (supplierId)
                    {

                        case 80:
                            {
                                #region
                                string callback = OfCard.Card.Default.Query(orderid);

                                if (!string.IsNullOrEmpty(callback))
                                {
                                    OfCard.Card.Default.Finish(callback);
                                }
                                #endregion
                            }
                            break;
                        case 70:
                            {
                                #region
                                string callback = viviapi.ETAPI.Cared70.Card.Default.Query(orderid);

                                if (!string.IsNullOrEmpty(callback))
                                {
                                    Cared70.Card.Default.Finish(orderid, callback);
                                }
                                #endregion
                            }
                            break;
                            //case 51:
                            //    {
                            //        #region
                            //        QueryResponse response = Card.Default.Query(orderid, facevalue);
                            //        if (response != null)
                            //        {
                            //            Card.Default.Finish(response);
                            //        }
                            //        #endregion
                            //    }
                            //    break;
                            //case 85:
                            //    {
                            #region HuiYuan
                            //        string callback = HuiYuan.Card.Default.Query(orderid);

                            //        if (!string.IsNullOrEmpty(callback))
                            //        {
                            //            HuiYuan.Card.Default.Finish(callback);
                            //        }
                            #endregion
                            //    }
                            //    break;
                            //case 851:
                            //    {
                            //        #region
                            //        string callback = HuiSu.Card.Default.Query(orderid);

                            //        if (!string.IsNullOrEmpty(callback))
                            //        {
                            //            HuiSu.Card.Default.Finish(callback);
                            //        }
                            //        #endregion
                            //    }
                            //    break;
                            //case 60866:
                            //    {
                            //        #region 60866
                            //        string callback = Card60866.Card.Default.Query(orderid);

                            //        if (!string.IsNullOrEmpty(callback))
                            //        {
                            //            Card60866.Card.Default.Finish(orderid, callback);
                            //        }
                            //        #endregion
                            //    }
                            //    break;

                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }

            }
        }

        public static void BatchQueryOrder(DataTable orders)
        {
            foreach (DataRow row in orders.Rows)
            {
                int supplierId = Convert.ToInt32(row["supplierID"]);
                int facevalue = Convert.ToInt32(row["refervalue"]);
                string orderid = row["orderid"].ToString();

                QueryOrder(supplierId, facevalue, orderid);
            }
        }

        #endregion
    }
}
