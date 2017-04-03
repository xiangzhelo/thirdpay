using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using viviapi.ETAPI.Common;
using viviLib;
using viviLib.Web;
using viviLib.Security;

using viviapi.Model;
using viviapi.Model.User;
using viviapi.Model.Channel;
using viviapi.BLL;
using viviapi.BLL.Channel;
using viviapi.BLL.User;
using viviapi.ETAPI;
using Factory = viviapi.BLL.User.Factory;


namespace viviAPI.WebUI7uka.usermodule.Ajax
{
    public class CardSellOldNew : IHttpHandler, IReadOnlySessionState
    {
        viviapi.BLL.OrderCard bll = new viviapi.BLL.OrderCard();

        #region 参数
        /// <summary>
        /// 
        /// </summary>
        public string cardNo
        {
            get
            {
                return WebBase.GetQueryStringString("CardId", "").Trim();
            }
        }
        public string cardPwd
        {
            get
            {
                return WebBase.GetQueryStringString("CardPass", "").Trim();
            }
        }
        public int faceValue
        {
            get
            {
                return WebBase.GetQueryStringInt32("FaceValue", 0);
            }
        }
        public int typeId
        {
            get
            {
                int _typeid = WebBase.GetQueryStringInt32("ctl00$ContentPlaceHolder1$xk_channelId", 0);
                if (_typeid == 0)
                {
                    _typeid = WebBase.GetQueryStringInt32("ChannelId", 0);
                }
                return _typeid;
            }
        }
        private viviapi.Model.Channel.ChannelTypeInfo _typeInfo = null;
        public viviapi.Model.Channel.ChannelTypeInfo typeInfo
        {
            get
            {
                if (_typeInfo == null && typeId > 0)
                    _typeInfo = viviapi.BLL.Channel.ChannelType.GetCacheModel(typeId);

                return _typeInfo;
            }
        }
        #endregion

        public viviapi.Model.User.UserInfo currentUser
        {
            get
            {
                return viviapi.BLL.User.Login.CurrentMember;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string msg = "";

            if (currentUser == null)
            {
                msg = "登录信息失效，请重新登录";
            }
            else
            {
                if (typeInfo == null)
                {
                    msg = "无效卡";
                }
                else if (string.IsNullOrEmpty(cardNo))
                {
                    msg = "请输入卡号444";
                }
                else if (string.IsNullOrEmpty(cardPwd))
                {
                    msg = "请输入卡密";
                }
                else if (faceValue <= 0)
                {
                    msg = "面值不正确";
                }
                else if (typeId <= 0)
                {
                    msg = "通道不正确";
                }
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
                    viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
        }

        static readonly object obj = "vivisoft_QQ_1780493978_xiaoka_ProcessOrder";

        #region ProcessOrder
        /// <summary>
        /// 处理订单
        /// </summary>
        protected string ProcessOrder(HttpContext context)
        {
            string msg = string.Empty;
            if (typeId == 103 || typeId == 108 || typeId == 113 || (typeId >= 200 && typeId <= 203))
            {
                lock (obj)
                {
                    int interval = 0;

                    try
                    {
                        interval = Convert.ToInt32(decimal.Round(viviapi.SysConfig.RuntimeSetting.xiaoka_time_interval * 1000, 0));
                    }
                    catch { }

                    if (interval > 0)
                    {
                        System.Threading.Thread.Sleep(interval);
                    }
                    msg = SendToSupp();
                }
            }
            else
            {
                msg = SendToSupp();
            }

            return msg;
        }
        #endregion

        #region SendToSupp
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string SendToSupp()
        {
            try
            {
                int userid = currentUser.ID;
                string msg = string.Empty;

                int supplierId = 0;

                bool isxunhuan = typeInfo.runmode == 1;


                ChannelInfo chanelInfo
                             = viviapi.BLL.Channel.Channel.GetModel(typeId, faceValue, userid, isxunhuan);

                if (chanelInfo == null
                    || chanelInfo.supplier.HasValue == false
                    || chanelInfo.supplier == 0)
                {
                    msg = "找不到通道,请联系商务或系统管理员处理。";
                }
                else
                {
                    supplierId = chanelInfo.supplier.Value;
                }

                if (supplierId > 0 && string.IsNullOrEmpty(msg))
                {
                    var order
                            = new viviapi.Model.Order.OrderCardInfo();

                    order.orderid = viviapi.BLL.Order.Card.Factory.Instance.GenerateOrderId("");
                    order.addtime = DateTime.Now;
                    order.attach = "cardsell";
                    order.notifycontext = string.Empty;
                    order.notifycount = 0;
                    order.notifystat = 0;
                    order.notifyurl = string.Empty;
                    order.clientip = ServerVariables.TrueIP;
                    order.completetime = DateTime.Now;
                    order.ordertype = 8;
                    order.typeId = typeId;
                    order.paymodeId = typeId.ToString("0000") + faceValue.ToString();// chanelInfo.code;
                    order.payRate = 0M;
                    order.supplierId = supplierId;
                    order.supplierOrder = string.Empty;
                    order.userid = userid;
                    order.userorder = order.orderid + userid.ToString();
                    order.refervalue = faceValue;
                    order.referUrl = string.Empty;
                    order.cardNo = cardNo;
                    order.cardPwd = cardPwd;
                    order.server = viviapi.SysConfig.RuntimeSetting.ServerId;
                    order.cardnum = 1;
                    order.version = "";
                    order.agentId = Factory.GetPromID(userid);

                    //订单所属业务
                    order.manageId = currentUser.manageId;

                    bll.Insert(order);


                    string supporderid = string.Empty;
                    string errorinfo = string.Empty;
                    string errorCode = string.Empty;

                    msg = "true";

                    var supp = (viviapi.Model.supplier.SupplierCode)supplierId;

                    var callBack = OrderCardUtils.SynchSubmit(supp
                          , order.orderid
                          , typeId
                          , order.cardNo
                          , order.cardPwd
                          , faceValue
                          , ""
                          , 1);

                    if (callBack.SummitStatus == 0)
                    {
                        msg = errorinfo;

                        //bll.ReceiveSuppResult(supplierId
                        //    , order.orderid
                        //    , order.cardNo
                        //    , 4
                        //    , callBack.SuppErrorCode
                        //    , errorinfo
                        //    , 0M
                        //    , 0M
                        //    , string.Empty);
                    }
                }

                return msg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}