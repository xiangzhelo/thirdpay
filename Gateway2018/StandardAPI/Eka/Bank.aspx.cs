using System;
using System.Globalization;
using System.Web;
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

namespace viviAPI.Gateway2018.StandardAPI.Eka
{
    public partial class Bank : BankTransBase
    {
        OrderBank newOrder = new OrderBank();

        #region 接收API参数 POST/GET方式
       

        #region 商户ID
        /// <summary>
        /// 商户ID 
        /// </summary>
        public string Userid
        {
            get
            {
                return GetParmValue("parter");
            }
        }
        #endregion

        #region 银行编号
        /// <summary>
        /// 银行编号
        /// </summary>
        public string Bankid
        {
            get
            {
                return GetParmValue("type");
            }
        }
        #endregion

        #region 金额
        /// <summary>
        /// 订单金额 （单位：元） 2位小数，最小支付金额为0.02
        /// </summary>
        public string Money
        {
            get
            {
                return GetParmValue("value");
            }
        }
        #endregion

        #region 商户订单号
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string Orderid
        {
            get
            {
                return GetParmValue("orderid");
            }
        }
        #endregion

        #region 下行异步通知地址
        /// <summary>
        /// 下行异步通知地址
        /// </summary>
        public string Notifyurl
        {
            get
            {
                return GetParmValue("callbackurl");
            }
        }
        #endregion

        #region 下行同步通知地址
        /// <summary>
        /// 商户商城取货地址
        /// </summary>
        public string Returnurl
        {
            get
            {
                return GetParmValue("hrefbackurl");
            }
        }
        #endregion

        #region 支付用户IP
        /// <summary>
        /// 支付用户IP
        /// </summary>
        public string ClientIp
        {
            get
            {
                return GetParmValue("payerIp");
            }
        }
        #endregion

        #region 备注消息
        /// <summary>
        /// 商户扩展信息，返回时原样返回，此参数如用到中文，请注意转码
        /// </summary>
        public string Attach
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("attach", ""), System.Text.Encoding.GetEncoding("GB2312"));
            }
        }
        #endregion

        #region 版本
        /// <summary>
        ///1.0 暂时不用
        ///</summary>
        public string version
        {
            get
            {
                return viviapi.SysInterface.Bank.Eka.VbYika;
            }
        }
        #endregion

        #region MD5签名
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign
        {
            get
            {
                return GetParmValue("sign");
            }
        }
        #endregion

        #region 代理
        /// <summary>
        /// 
        /// </summary>
        public int AgentId
        {
            get
            {
                int _agentId = 0;
                try
                {
                    string parms = GetParmValue("agent");
                    if (!string.IsNullOrEmpty(parms))
                    {
                        int.TryParse(parms, out _agentId);
                    }
                    else
                    {
                        parms = GetParmValue("hashcode");
                        if (!string.IsNullOrEmpty(parms))
                        {
                            _agentId = Convert.ToInt32(parms, 16);
                        }
                    }
                }
                catch
                {

                }

                return _agentId;
            }
        }
        #endregion
        #endregion

        bool IsReturnUrlOk()
        {
            if (this.Returnurl == null || this.Returnurl.Length == 0)
                return true;
            bool isUrl = viviLib.Text.Validate.IsUrl(Returnurl);
            if (isUrl)
            {
                return !Returnurl.Contains("?") && !Returnurl.Contains("&");
            }
            return isUrl;
        }

        bool IsNotifyUrlOk()
        {
            if (this.Notifyurl == null || this.Notifyurl.Length == 0)
                return false;

            bool isUrl = viviLib.Text.Validate.IsUrl(Notifyurl);
            if (isUrl)
            {
                return !Notifyurl.Contains("?") && !Notifyurl.Contains("&");
            }
            return isUrl;
        }

        bool IsClientIpOk()
        {
            if (this.ClientIp == null || this.ClientIp.Length == 0)
                return true;
            else return viviLib.Text.Validate.IsIPSect(ClientIp);
        }


        /// <summary>
        /// 验证来路
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        bool CheckUrlReferrer(int uid)
        {
            //if (!viviapi.SysConfig.RuntimeSetting.CheckUrlReferrer)
            return true;

            //if (Request.UrlReferrer == null)
            //    return false;

            //viviapi.BLL.User.userHost hostBLL = new userHost();         

            //return hostBLL.Exists(uid, Request.UrlReferrer.Host);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string error = string.Empty, apikey = "";
            bool riskWarning = true;

            //IP验证
            //if (!string.IsNullOrEmpty(this.clientIp))
            //{
            //    if (viviLib.Web.IPHelper.GetValue(this.clientIp) != viviLib.Web.IPHelper.GetValue(viviLib.Web.ServerVariables.TrueIP))
            //    {
            //        return;
            //    }
            //}

            #region Step1 必要的参数不能为空
            if (string.IsNullOrEmpty(this.Userid))
            {
                error = "error:1001 商户ID（parter）不能空！";
            }
            else if (string.IsNullOrEmpty(Bankid))
            {
                error = "error:1002 银行类型（type）不能空！";
            }
            else if (string.IsNullOrEmpty(Money))
            {
                error = "error:1003 订单金额（value）不能空！";
            }
            else if (string.IsNullOrEmpty(Orderid))
            {
                error = "error:1004 商户订单号（orderid）不能空！";
            }
            else if (string.IsNullOrEmpty(Notifyurl))
            {
                error = "error:1005 下行异步通知地址（callbackurl）不能空！";
            }
            /*else if (string.IsNullOrEmpty(version))
             {
                 error = "error:1006 版本号（version）不能空！";
             }*/
            else if (string.IsNullOrEmpty(Sign))
            {
                error = "error:1006 MD5签名（sign）不能空！";
            }
            #endregion

            #region Step2 检查参数长度
            else if (Userid.Length > 5)
            {
                error = "error:1020 商户ID（parter）长度超过5位！";
            }
            else if (Bankid.Length > 4)
            {
                error = "error:1021 银行类型（type）长度超过4位！";
            }
            else if (Orderid.Length > 30)
            {
                error = "error:1022 商户订单号（orderid）长度超过30位！";
            }
            else if (Money.Length > 8)
            {
                error = "error:1023 订单金额（value）长度超过最长限制！";
            }
            else if (this.Notifyurl.Length > 255)
            {
                error = "error:1024 下行异步通知地址（callbackurl）长度超过255位！";
            }
            else if (this.Returnurl.Length > 255)
            {
                error = "error:1025 下行同步通知地址（hrefbackurl）长度超过255位！";
            }
            else if (this.ClientIp.Length > 20)
            {
                error = "error:1026 支付用户IP（payerIp）长度超过20位！";
            }
            else if (this.Attach.Length > 255)
            {
                error = "error:1027 备注消息（attach）长度超过255位！";
            }
            else if (this.Sign.Length != 32)
            {
                error = "error:1028 签名（sign）长度不正确！";
            }
            #endregion

            #region Step3 格式验证
            else if (!viviLib.Text.Validate.IsNumeric(Userid))
            {
                error = "error:1040 商户ID（parter）格式不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(Bankid))
            {
                error = "error:1041 银行类型（type）格式不正确！";
            }
            //else if (!viviLib.Text.Validate.IsNumber(money) || !viviLib.Text.Validate.IsNumeric(money))//二位小数
            //{
            //    error = "error:1042 订单金额（value）格式不正确！";
            //}
            else if (!IsNotifyUrlOk())
            {
                error = "error:1043 下行异步通知地址（callbackurl）格式不正确！";
            }
            else if (!IsReturnUrlOk())
            {
                error = "error:1044 下行同步通知地址（hrefbackurl）格式不正确！";
            }
            else if (!IsClientIpOk())
            {
                error = "error:1045 支付用户IP（payerIp）格式不正确！";
            }
            #endregion

            //完成基础资料的验证
            if (!string.IsNullOrEmpty(error))
            {
                WebUtility.ShowErrorMsg(error);
                return;
            }
            UserInfo userInfo = null;
            decimal tranAmt = decimal.Zero;
            int userId = int.Parse(Userid);

            #region Step4 数据验证
            if (!CheckUrlReferrer(userId))
            {
                string Host = string.Empty;
                if (Request.UrlReferrer != null)
                    Host = Request.UrlReferrer.Host;

                error = string.Format("error:1070 来路地址不合法！{0}", Host);
            }
            else if (!decimal.TryParse(Money, out tranAmt))
            {
                error = "error:1060 订单金额（value）有误！";
            }
            else if (tranAmt < this.MinTranAMT)
            {
                error = "error:1061 订单金额（value）小于最小允许交易额！";
            }
            else if (tranAmt > this.MaxChargeAMT)
            {
                error = string.Format("error:1062 订单金额（value）{0:f2}大于最大允许交易额{1:f2}！", tranAmt, this.MaxChargeAMT);
            }
            else
            {
                var checkResult = Factory.Instance.CheckApiParms(userId,102,RequiredCheckUserOrderNo, this.Orderid);
                if (checkResult == null)
                {
                    error = "error:1063 系统错误";
                }
                else
                {
                    userInfo = checkResult.Obj as UserInfo;
                    if (userInfo == null)
                    {
                        error = "error:1067 系统错误";
                    }
                    else if (checkResult.ErrCode == 1)
                    {
                        error = "error:1064 商户编号不存在";
                    }
                    else if (checkResult.ErrCode == 2)
                    {
                        error = "error:1065 商户状态不正常";
                    }
                    else if (checkResult.ErrCode == 3)
                    {
                        error = "error:1069 商户订单号重复";
                    }
                    else if (!viviapi.SysInterface.Bank.Eka.ReceiveVerify(Userid
                        , this.Bankid
                        , this.Money
                        , this.Orderid
                        , this.Notifyurl
                        , userInfo.APIKey
                        , Sign))
                    {
                        error = "error:1066 签名错误!";
                    }
                    else
                    {
                        apikey = userInfo.APIKey;
                        riskWarning = userInfo.RiskWarning == 1 ? true : false;
                    }
                }
            }
            if (!string.IsNullOrEmpty(error))
            {
                WebUtility.ShowErrorMsg(error);
                return;
            }
            #endregion

            int typeId = 0; int supplierId = 0;
            ChannelInfo channelInfo = viviapi.BLL.Channel.Channel.GetModel(Bankid, userId, true);

            if (channelInfo == null)
            {
                error = "error:1067:银行编号不存在!";
            }
            else if (channelInfo.isOpen != null && channelInfo.isOpen.Value != 1)
            {
                error = "error:1068:通道维护中!";
            }
            if (!string.IsNullOrEmpty(error))
            {
                if (DebuglogOpen)
                {
                    if (userInfo != null && userInfo.isdebug == 1)
                    {
                        var debugInfo = new viviapi.Model.Sys.debuginfo
                        {
                            addtime = DateTime.Now,
                            bugtype = viviapi.Model.Sys.debugtypeenum.网银订单,
                            detail = string.Empty,
                            errorcode = error,
                            errorinfo = error,
                            userid = userInfo.ID,
                            url = Request.RawUrl.ToString(CultureInfo.InvariantCulture)
                        };

                        viviapi.BLL.Sys.Debuglog.Insert(debugInfo);
                    }
                }

                WebUtility.ShowErrorMsg(error);
                return;
            }
            //else if (!UserFactory.CheckUserOrderId(userId, orderid))
            //{
            //    error = "error:1068:商户订单号重复!";
            //}
            typeId = channelInfo.typeId;
            supplierId = channelInfo.supplier.Value;

            

            var order = new OrderBankInfo
            {
                orderid = Factory.Instance.GenerateOrderId(OrderPrefix),
                addtime = DateTime.Now,
                attach = Attach,
                notifycontext = string.Empty,
                notifycount = 0,
                notifystat = 0,
                notifyurl = this.Notifyurl,
                clientip = viviLib.Web.ServerVariables.TrueIP,
                completetime = DateTime.Now,
                returnurl = Returnurl,
                ordertype = 1,
                typeId = typeId,
                paymodeId = this.Bankid,
                supplierId = supplierId,
                supplierOrder = string.Empty,
                userid = userId,
                userorder = Orderid,
                refervalue = tranAmt
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
            order.manageId = 0; //userInfo.manageId; //业务

            if (!order.manageId.HasValue || order.manageId.Value <= 0)
            {
                if (AgentId > 0)
                {
                    if (viviapi.BLL.User.Factory.ChkAgent(AgentId))
                    {
                        order.agentId = AgentId;//代理
                    }
                }
                else
                {
                    order.agentId = viviapi.BLL.User.Factory.GetPromID(userId);
                }
            }

            order.version = version;
            viviapi.Cache.WebCache.GetCacheService().AddObject(order.orderid, order, ExpiresTime);
            //         
            newOrder.Insert(order);

            SellFactory.OnlineBankPay(order.userid, apikey, supplierId, order.orderid, order.refervalue, order.paymodeId, riskWarning); ;
        }
    }
}
