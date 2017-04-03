using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Order.Bank;
using viviapi.ETAPI.Common;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviapi.SysConfig;
using viviapi.WebComponents;
using viviapi.WebComponents.GateWay;
using viviLib.Web;

namespace viviAPI.Gateway2018.StandardAPI.HuaQi
{
    public partial class Gateway : BankTransBase
    {
        OrderBank orderBankBLL = new OrderBank();
        OrderCard orderCardBLL = new OrderCard();

        #region 接收API参数 POST/GET方式
        
        #region 商户ID P_UserId
        /// <summary>
        /// 商户ID  P_UserId
        /// </summary>
        public string parter
        {
            get
            {
                return GetParmValue("P_UserId");
            }
        }
        #endregion

        #region 商户订单号 P_OrderId
        /// <summary>
        /// 商户订单号 P_OrderId
        /// </summary>
        public string orderid
        {
            get
            {
                return GetParmValue("P_OrderId");
            }
        }
        #endregion

        #region 卡号 P_CardId
        /// <summary>
        /// 卡号
        /// </summary>
        public string cardno
        {
            get
            {
                return GetParmValue("P_CardId");
            }
        }
        #endregion

        #region 密码 P_CardPass
        /// <summary>
        /// 密码
        /// </summary>
        public string cardpass
        {
            get
            {
                return GetParmValue("P_CardPass");
            }
        }
        #endregion

        #region 面值 P_FaceValue
        /// <summary>
        /// 订单金额 （单位：元） 2位小数，最小支付金额为0.02
        /// </summary>
        public string money
        {
            get
            {
                return GetParmValue("P_FaceValue");
            }
        }
        #endregion

        #region 充值类型 P_ChannelId
        /// <summary>
        /// 银行编号
        /// </summary>
        public string type
        {
            get
            {
                return GetParmValue("P_ChannelId");
            }
        }
        #endregion

        #region 产品名称 P_Subject
        /// <summary>
        /// 产品名称
        /// </summary>
        public string P_Subject
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("P_Subject", ""), System.Text.Encoding.GetEncoding("GB2312"));
            }
        }
        #endregion

        #region 产品价格 P_Price
        /// <summary>
        /// 产品价格
        /// </summary>
        public string P_Price
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("P_Price", ""), System.Text.Encoding.GetEncoding("GB2312"));
            }
        }
        #endregion

        #region 产品数量 P_Quantity
        /// <summary>
        /// 产品数量
        /// </summary>
        public string P_Quantity
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("P_Quantity", ""), System.Text.Encoding.GetEncoding("GB2312"));
            }
        }
        #endregion

        #region 产品描述 P_Description
        /// <summary>
        /// 产品描述
        /// </summary>
        public string P_Description
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("P_Description", ""), System.Text.Encoding.GetEncoding("GB2312"));
            }
        }
        #endregion

        #region 用户附加信息 P_Notic
        /// <summary>
        /// 商户扩展信息，返回时原样返回，此参数如用到中文，请注意转码
        /// </summary>
        public string attach
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("P_Notic", ""), System.Text.Encoding.GetEncoding("GB2312"));
            }
        }
        #endregion

        #region 充值状态通知地址 P_Result_URL
        /// <summary>
        /// 下行异步通知地址
        /// </summary>
        public string notifyurl
        {
            get
            {
                return GetParmValue("P_Result_URL");
            }
        }
        #endregion

        #region 充值后网页跳转地址 P_Notify_URL
        /// <summary>
        /// 商户商城取货地址
        /// </summary>
        public string returnurl
        {
            get
            {
                return GetParmValue("P_Notify_URL");
            }
        }
        #endregion

        #region 签名认证串 P_PostKey
        /// <summary>
        /// 签名
        /// </summary>
        public string sign
        {
            get
            {
                return GetParmValue("P_PostKey");
            }
        }
        #endregion

        #region 支付用户IP
        /// <summary>
        /// 支付用户IP
        /// </summary>
        public string clientIp
        {
            get
            {
                return GetParmValue("payerIp");
            }
        }
        #endregion

        #region 版本
        /// <summary>
        /// 
        /// </summary>
        public string version
        {
            get
            {
                return viviapi.SysInterface.Bank.HuaQi.Vbhq10;
            }
        }
        #endregion
        #endregion

        #region 自定义参数
        #region 商户ID
        /// <summary>
        /// 商户ID
        /// </summary>
        public int Userid
        {
            get
            {
                int _userid = 0;
                if (!string.IsNullOrEmpty(parter))
                {
                    if (int.TryParse(parter, out _userid))
                    {
                        return _userid;
                    }
                }
                return _userid;
            }
        }
        #endregion
        #region 商户信息
        /// <summary>
        /// 
        /// </summary>
        private UserInfo _userInfo = null;
        public UserInfo UserInfo
        {
            get
            {
                if (Userid > 0 && _userInfo == null)
                {
                    _userInfo = viviapi.BLL.User.Factory.GetCacheUserBaseInfo(Userid);
                }
                if (_userInfo == null)
                    _userInfo = new UserInfo();

                return _userInfo;
            }
        }
        #endregion
        #region 订单金额
        public decimal TranAmt
        {
            get
            {
                decimal _tranAmt = 0M;
                try
                {
                    _tranAmt = Convert.ToDecimal(this.money);
                }
                catch { }
                return _tranAmt;
            }
        }
        #endregion

        #region 通道类型
        /// <summary>
        /// 
        /// </summary>
        public int SysChannelId
        {
            get
            {
                return viviapi.SysInterface.Bank.HuaQi.ConvertChannelCode(type);
            }
        }
        #endregion
        #endregion

        #region isChargeBank
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsChargeBank()
        {
            if (string.IsNullOrEmpty(type))
            {
                return false;
            }
            if (type == "1" || type == "2" || type == "3")
            {
                return true;
            }
            return false;
        }
        #endregion

        #region isChargeCard
        /// <summary>
        /// 
        /// </summary>
        readonly string[] _cardChannels = new string[] { "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };

        public bool IsChargeCard()
        {
            if (string.IsNullOrEmpty(type))
            {
                return false;
            }

            return _cardChannels.Any(channel => type == channel);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsChargeBank())
            {
                ChargeBank();
            }
            else if (IsChargeCard())
            {
                ChargeCard();
            }
            else
            {
                Response.Write("充值类型 P_ChannelId 不存在");
            }
        }

        #region ChargeBank
        /// <summary>
        /// 
        /// </summary>
        void ChargeBank()
        {
            string error = string.Empty;

            #region Step1 必要的参数不能为空
            if (string.IsNullOrEmpty(this.parter))
            {
                error = "error:1001 商户ID（P_UserId）不能空！";
            }
            else if (string.IsNullOrEmpty(orderid))
            {
                error = "error:1004 商户订单号（P_OrderId）不能空！";
            }
            else if (string.IsNullOrEmpty(money))
            {
                error = "error:1003 充值金额（P_FaceValue）不能空！";
            }
            else if (string.IsNullOrEmpty(type))
            {
                error = "error:1002 充值类型（P_ChannelId）不能空！";
            }
            else if (string.IsNullOrEmpty(P_Price))
            {
                error = "error:1007 产品价格（P_Price）不能空！";
            }
            else if (string.IsNullOrEmpty(P_Quantity))
            {
                error = "error:1008 产品数量（P_Quantity）不能空！";
            }
            else if (string.IsNullOrEmpty(notifyurl))
            {
                error = "error:1005 充值状态通知地址（P_Result_URL）不能空！";
            }
            else if (string.IsNullOrEmpty(sign))
            {
                error = "error:1006 MD5签名（sign）不能空！";
            }
            #endregion
            #region Step2 检查参数长度
            else if (this.parter.Length > 10)
            {
                error = "error:1020 商户ID（P_UserId）长度超过10位！";
            }
            else if (orderid.Length > 32)
            {
                error = "error:1022 商户订单号（P_OrderId）长度超过32位！";
            }
            else if (money.Length > 8)
            {
                error = "error:1023 订单金额（P_FaceValue）长度超过最长限制！";
            }
            else if (this.notifyurl.Length > 255)
            {
                error = "error:1024 充值状态通知地址（P_Result_URL）长度超过255位！";
            }
            else if (this.returnurl.Length > 255)
            {
                error = "error:1025 充值后网页跳转地址（P_Notify_URL）长度超过255位！";
            }
            else if (this.attach.Length > 255)
            {
                error = "error:1027 用户附加信息（P_Notic）长度超过255位！";
            }
            else if (this.P_Description.Length > 255)
            {
                error = "error:1029 产品描述（P_Description）长度超过255位！";
            }
            else if (this.P_Subject.Length > 50)
            {
                error = "error:1029 产品描述（P_Subject）长度超过50位！";
            }
            else if (this.sign.Length != 32)
            {
                error = "error:1028 签名认证串（sign）长度不正确！";
            }
            #endregion
            #region Step3 格式验证
            else if (!viviLib.Text.Validate.IsNumeric(parter))
            {
                error = "error:1040 商户ID（P_UserId）格式不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(type))
            {
                error = "error:1041 充值类型（P_ChannelId）格式不正确！";
            }
            else if (!checkOrderMoney())
            {
                error = "error:1042 订单金额（P_FaceValue）格式不正确！";
            }
            else if (!checkPrice())
            {
                error = "error:1045 产品价格（P_Price）格式不正确！";
            }
            else if (!checkPQuantity())
            {
                error = "error:1046 产品数量（P_Quantity）格式不正确！";
            }
            else if (!isNotifyUrlOk())
            {
                error = "error:1043 充值状态通知地址（P_Result_URL）格式不正确！";
            }
            else if (!isReturnUrlOk())
            {
                error = "error:1044 充值后网页跳转地址（P_Notify_URL）格式不正确！";
            }
            #endregion
            #region Step4 数据验证
            else if (SysChannelId <= 0)
            {
                error = "error:1067 不存在此支付通道（P_ChannelId）！";
            }
            else if (TranAmt <= 0M)
            {
                error = "error:1060 订单金额（P_FaceValue）有误！";
            }
            else if (TranAmt < this.MinTranAMT)
            {
                error = "error:1061 订单金额（P_FaceValue）小于最小允许交易额！";
            }
            else if (TranAmt > this.MaxChargeAMT)
            {
                error = string.Format("error:1062 订单金额（P_FaceValue）{0:f2}大于最大允许交易额{1:f2}！", TranAmt, this.MaxChargeAMT);
            }
            else if (UserInfo == null)
            {
                error = "error:1064 商户（P_UserId）不存在";
            }
            else if (UserInfo.Status != 2)
            {
                error = "error:1065 商户（P_UserId）状态不正常";
            }
            else if (!viviapi.SysInterface.Bank.HuaQi.ReceiveVerify(version, sign, new object[] { this.parter, this.orderid, this.cardno, this.cardpass, this.money, this.type, UserInfo.APIKey }))
            {
                error = "error:1066 签名认证串（P_PostKey）错误!";
            }
            #endregion

            if (string.IsNullOrEmpty(error))
            {
                ChannelTypeInfo chanTypeInfo = viviapi.BLL.Channel.ChannelType.GetCacheModel(this.SysChannelId);
                if (chanTypeInfo == null)
                {
                    error = "error:1068:不存在此支付通道（P_ChannelId）!";
                }
                else if (chanTypeInfo.isOpen == OpenEnum.Close)
                {
                    error = "error:1069:通道（P_ChannelId）维护中!";
                }
            }

            if (!string.IsNullOrEmpty(error))
            {
                #region 处理日志
                if (DebuglogOpen)
                {
                    if (UserInfo != null && UserInfo.isdebug == 1)
                    {
                        var debugInfo = new viviapi.Model.Sys.debuginfo
                        {
                            addtime = DateTime.Now,
                            bugtype = viviapi.Model.Sys.debugtypeenum.网银订单,
                            detail = string.Empty,
                            errorcode = error,
                            errorinfo = error,
                            userid = UserInfo.ID
                        };
                        debugInfo.url = Request.RawUrl.ToString(CultureInfo.InvariantCulture);

                        viviapi.BLL.Sys.Debuglog.Insert(debugInfo);
                    }
                }

                WebUtility.ShowErrorMsg(error);
                return;
                #endregion
            }
            else
            {
                #region 初始化订单
                var order = new OrderBankInfo
                {
                    orderid = Factory.Instance.GenerateOrderId(OrderPrefix),
                    addtime = DateTime.Now,
                    attach = attach,
                    notifycontext = string.Empty,
                    notifycount = 0,
                    notifystat = 0,
                    notifyurl = this.notifyurl,
                    clientip = viviLib.Web.ServerVariables.TrueIP,
                    completetime = DateTime.Now,
                    returnurl = returnurl,
                    ordertype = 1,
                    typeId = SysChannelId,
                    paymodeId = string.Empty,
                    supplierId = 0,
                    supplierOrder = string.Empty,
                    userid = Userid,
                    userorder = orderid,
                    refervalue = TranAmt
                };
                //order.payRate = 0M;
                //order.supplierRate = chanelInfo.supprate;
                if (Request.UrlReferrer != null)
                    order.referUrl = Request.UrlReferrer.ToString();
                else
                {
                    order.referUrl = string.Empty;
                }
                order.server = RuntimeSetting.ServerId;
                order.manageId = UserInfo.manageId;

                order.version = version;
                order.cus_subject = this.P_Subject;
                order.cus_price = this.P_Price;
                order.cus_quantity = this.P_Quantity;
                order.cus_description = this.P_Description;
                order.cus_field2 = type;

                viviapi.Cache.WebCache.GetCacheService().AddObject(order.orderid, order, ExpiresTime);
                //         
                orderBankBLL.Insert(order);
                #endregion

                string redirectUrl = "/SelectBank.aspx?sysorderid=" + order.orderid;
                Response.Redirect(redirectUrl, true);
            }
        }
        #endregion

        void ChargeCard()
        {
            string error = string.Empty;
            #region Step1 必要的参数不能为空
            if (string.IsNullOrEmpty(this.parter))
            {
                error = "101";//商户ID为空
            }
            else if (string.IsNullOrEmpty(orderid))
            {
                error = "118";//商户订单号为空
            }
            else if (string.IsNullOrEmpty(money))
            {
                error = "120";//充值金额不合法
            }
            else if (string.IsNullOrEmpty(type))
            {
                error = "107";//充值类型错误
            }
            else if (string.IsNullOrEmpty(P_Price))
            {
                error = "123";//产品单价不是数字
            }
            #endregion
            #region Step2 检查参数长度
            else if (orderid.Length > 32)
            {
                error = "119";//商户订单号太长
            }
            else if (money.Length > 8)
            {
                error = "120";//充值金额不合法
            }
            else if (this.notifyurl.Length > 255)
            {
                error = "126";//result_url太长
            }
            else if (this.returnurl.Length > 255)
            {
                error = "125";//notify_url太长
            }
            else if (this.attach.Length > 255)
            {
                error = "124";//用户自定义信息文字太多
            }
            else if (this.P_Description.Length > 255)
            {
                error = "122";//产品描述文字太多
            }
            else if (this.P_Subject.Length > 50)
            {
                error = "121";//产品名称太长
            }
            else if (this.sign.Length != 32)
            {
                error = "110";//加密串postKey错误
            }
            #endregion
            #region Step3 格式验证
            else if (!viviLib.Text.Validate.IsNumeric(parter))
            {
                error = "109";//用户不存在
            }
            else if (!viviLib.Text.Validate.IsNumeric(type))
            {
                error = "112";//卡号类型不存在
            }
            else if (!checkOrderMoney())
            {
                error = "120";//充值金额不合法
            }
            /* else if (!checkPrice())
             {
                 error = "error:1045 产品价格（P_Price）格式不正确！";
             }
             else if (!checkPQuantity())
             {
                 error = "error:1046 产品数量（P_Quantity）格式不正确！";
             }
             else if (!isNotifyUrlOk())
             {
                 error = "error:1043 充值状态通知地址（P_Result_URL）格式不正确！";
             }
             else if (!isReturnUrlOk())
             {
                 error = "error:1044 充值后网页跳转地址（P_Notify_URL）格式不正确！";
             }*/
            #endregion
            #region Step4 数据验证
            else if (SysChannelId <= 0)
            {
                error = "112";
            }
            else if (TranAmt <= 0M)
            {
                error = "120";
            }
            else if (UserInfo == null)
            {
                error = "109";
            }
            else if (UserInfo.Status != 2)
            {
                error = "109";
            }
            else if (!viviapi.SysInterface.Bank.HuaQi.ReceiveVerify(version, sign, new object[] { this.parter, this.orderid, this.cardno, this.cardpass, this.money, this.type, UserInfo.APIKey }))
            {
                error = "110";
            }
            #endregion

            int suppid = 0;
            if (string.IsNullOrEmpty(error))
            {
                ChannelTypeInfo _chanTypeInfo = viviapi.BLL.Channel.ChannelType.GetCacheModel(this.SysChannelId);
                if (_chanTypeInfo == null)
                {
                    error = "112";
                }
                else if (_chanTypeInfo.isOpen == OpenEnum.Close)
                {
                    error = "112";
                }
                else
                {
                    suppid = _chanTypeInfo.supplier;
                }
            }

            if (!string.IsNullOrEmpty(error))
            {
                #region 处理日志
                if (DebuglogOpen)
                {
                    if (UserInfo.isdebug == 1)
                    {
                        viviapi.Model.Sys.debuginfo _debugInfo = new viviapi.Model.Sys.debuginfo();
                        _debugInfo.addtime = DateTime.Now;
                        _debugInfo.bugtype = viviapi.Model.Sys.debugtypeenum.网银订单;
                        _debugInfo.detail = string.Empty;
                        _debugInfo.errorcode = error;
                        _debugInfo.errorinfo = error;
                        _debugInfo.userid = UserInfo.ID;
                        if (Request.RawUrl != null)
                            _debugInfo.url = Request.RawUrl.ToString();
                        else
                            _debugInfo.url = string.Empty;

                        viviapi.BLL.Sys.Debuglog.Insert(_debugInfo);
                    }
                }

                WebUtility.ShowErrorMsg(error);
                return;
                #endregion
            }
            else
            {

                string _supporderid = string.Empty;
                string supperrinfo = string.Empty;

                string sysOrderId = viviapi.BLL.Order.Card.Factory.Instance.GenerateOrderId(OrderPrefix);

                string cardStatus = Sell(suppid
                            , sysOrderId
                            , 0
                            , cardno
                            , cardpass
                            , this.SysChannelId
                            , this.money
                            , out _supporderid
                            , out supperrinfo);



                InitOrder(sysOrderId
                            , cardStatus == "0" ? 1 : 4
                            , string.Empty
                            , string.Empty
                            , suppid
                            , string.Empty);

                if (cardStatus == "0")
                    error = "0";
                else
                {
                    error = "117";
                }

            }

            Response.Write("errCode=" + error);
            Response.End();
        }


        #region 主订单 InitOrder
        #region GetCardType
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paytype"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public int GetCardType(int paytype)
        {
            int _cardType = paytype;
            if (paytype == 103)
            {
                _cardType = 13;
            }
            else if (paytype == 104)
            {
                _cardType = 2;
            }
            else if (paytype == 105)
            {
                _cardType = 7;
            }
            else if (paytype == 106)
            {
                _cardType = 3;
            }
            else if (paytype == 107)
            {
                _cardType = 1;
            }
            else if (paytype == 108)
            {
                _cardType = 14;
            }
            else if (paytype == 109)
            {
                _cardType = 8;
            }
            else if (paytype == 110)
            {
                _cardType = 9;
            }
            else if (paytype == 111)
            {
                _cardType = 5;
            }
            else if (paytype == 112)
            {
                _cardType = 6;
            }
            else if (paytype == 113)
            {
                _cardType = 12;
            }
            else if (paytype == 118)
            {
                _cardType = 21;
            }
            return _cardType;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_orderid"></param>
        /// <param name="_status"></param>
        /// <param name="_opstate"></param>
        /// <param name="_msg"></param>
        /// <param name="_suppId"></param>
        /// <param name="_returncode"></param>
        void InitOrder(string _orderid, int _status, string _opstate, string _msg, int _suppId, string _returncode)
        {
            string _chanelno = GetCardType(this.SysChannelId).ToString("0000");

            if (!string.IsNullOrEmpty(_returncode))
            {
                if (_returncode.EndsWith("|"))
                {
                    _returncode = _returncode.Substring(0, _returncode.Length - 1);
                }
            }

            var order = new OrderCardInfo();
            order.ordertype = 1;
            order.orderid = _orderid;
            order.userid = Userid;
            order.userorder = this.orderid;
            order.typeId = this.SysChannelId;
            order.cardType = int.Parse(type);
            order.cardNo = this.cardno;
            order.cardPwd = this.cardpass;
            order.paymodeId = _chanelno;
            order.refervalue = Convert.ToDecimal(this.TranAmt);
            order.clientip = viviLib.Web.ServerVariables.TrueIP;
            order.addtime = DateTime.Now;
            order.completetime = DateTime.Now;
            order.notifycontext = string.Empty;
            order.notifycount = 0;
            order.notifystat = 0;
            order.notifyurl = this.notifyurl;
            order.payRate = 0M;
            order.supplierId = _suppId;
            order.supplierOrder = string.Empty;
            order.server = RuntimeSetting.ServerId;

            order.cardnum = 1;
            order.resultcode = _returncode;
            order.ismulticard = 0;

            order.status = _status;
            order.ovalue = string.Empty;
            order.opstate = _opstate;
            order.msg = _msg;

            order.Desc = string.Empty;

            order.attach = attach;
            if (Request.UrlReferrer != null)
                order.referUrl = Request.UrlReferrer.ToString();
            else
                order.referUrl = string.Empty;
            order.server = RuntimeSetting.ServerId;
            order.manageId = UserInfo.manageId;

            order.version = version;
            order.cus_subject = this.P_Subject;
            order.cus_price = this.P_Price;
            order.cus_quantity = this.P_Quantity;
            order.cus_description = this.P_Description;
            order.cus_field2 = type;

            //订单所属业务
            order.manageId = UserInfo.manageId;
            if (!order.manageId.HasValue || order.manageId.Value <= 0)
            {
                order.agentId = viviapi.BLL.User.Factory.GetPromID(Userid);
            }

            viviapi.Cache.WebCache.GetCacheService().AddObject(order.orderid, order, ExpiresTime);

            orderCardBLL.Insert(order);
        }
        #endregion

        #region check
        bool checkOrderMoney()
        {
            decimal _orderMoney = 0M;
            try
            {
                _orderMoney = Convert.ToDecimal(this.money);
            }
            catch
            { }
            return _orderMoney > 0M;
        }
        bool checkPrice()
        {
            decimal _price = 0M;
            try
            {
                _price = Convert.ToDecimal(this.P_Price);
                return true;
            }
            catch
            {
                return false;
            }

        }
        bool checkPQuantity()
        {
            int _quantity = 0;
            try
            {
                _quantity = Convert.ToInt32(this.P_Quantity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool isReturnUrlOk()
        {
            if (this.returnurl == null || this.returnurl.Length == 0)
                return true;
            bool isUrl = viviLib.Text.Validate.IsUrl(returnurl);
            if (isUrl)
            {
                return !returnurl.Contains("?") && !returnurl.Contains("&");
            }
            return isUrl;
        }
        bool isNotifyUrlOk()
        {
            if (this.notifyurl == null || this.notifyurl.Length == 0)
                return false;

            bool isUrl = viviLib.Text.Validate.IsUrl(notifyurl);
            if (isUrl)
            {
                return !notifyurl.Contains("?") && !notifyurl.Contains("&");
            }
            return isUrl;
        }
        bool isClientIpOk()
        {
            if (this.clientIp == null || this.clientIp.Length == 0)
                return true;
            else return viviLib.Text.Validate.IsIPSect(clientIp);
        }
        #endregion

        string Sell(int _suppId, string _sysorderid, int _serial, string _cardno, string _cardpwd, int _typeid, string _cardvalue, out string _supporderid, out string _errmsg)
        {
            string retruncode = "";
            _supporderid = string.Empty;
            _errmsg = string.Empty;
            string suppErrCode = string.Empty;

            decimal _temp_num = 0;
            decimal.TryParse(_cardvalue, out _temp_num);

            var supp = (viviapi.Model.supplier.SupplierCode)_suppId;

            //string retruncode = ETAPI.SellFactory.SellCard(supp
            //    , _sysorderid
            //    , _typeid
            //    , _cardno
            //    , _cardpwd
            //    , string.Empty
            //    , Convert.ToInt32(decimal.Round(_temp_num, 0))
            //    , out _supporderid
            //    , out suppErrCode
            //    , out _errmsg);

            int faceValue = Convert.ToInt32(decimal.Round(_temp_num, 0));

            var callBack = OrderCardUtils.SynchSubmit(supp, _sysorderid, _typeid, _cardno, _cardpwd, faceValue, "", 1);

            if (callBack.SummitStatus == 1)
            {
                retruncode = "0";
            }
            else
            {
                retruncode = "-1";
            }

            return retruncode;
        }
    }
}
