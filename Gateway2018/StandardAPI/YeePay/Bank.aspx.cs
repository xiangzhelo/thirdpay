using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Channel;
using viviapi.BLL.Order.Bank;
using viviapi.BLL.User;
using viviapi.ETAPI.Common;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviapi.Model.Sys;
using viviapi.Model.User;
using viviapi.SysConfig;
using viviapi.WebComponents;
using viviapi.WebComponents.GateWay;
using viviLib.Web;
using Factory = viviapi.BLL.Order.Bank.Factory;

namespace viviAPI.Gateway2018.StandardAPI.YeePay
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Bank : BankTransBase
    {
        protected OrderBank NewOrder = new OrderBank();

        #region 接收API参数 POST/GET方式
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parmName"></param>
        /// <returns></returns>
        public string GetParmValue(string parmName)
        {
            string parmValue = WebBase.GetQueryStringString(parmName, "");
            if (string.IsNullOrEmpty(parmValue))
            {
                parmValue = WebBase.GetFormString(parmName, "");
            }
            return parmValue;
        }

        #region p0_Cmd
        /// <summary>
        /// 固定值“Buy”
        /// </summary>
        public string P0Cmd
        {
            get
            {
                return GetParmValue("p0_Cmd");
            }
        }
        #endregion

        #region p4_Cur
        /// <summary>
        ///固定值 ”CNY”
        /// </summary>
        public string P4Cur
        {
            get
            {
                return GetParmValue("p4_Cur");
            }
        }
        #endregion

        #region 商户ID
        /// <summary>
        /// p1_MerId 商户ID 
        /// </summary>
        public string Userid
        {
            get
            {
                return GetParmValue("p1_MerId");
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
                return GetParmValue("pd_FrpId");
            }
        }
        #endregion

        #region 银行编号
        /// <summary>
        /// 银行编号
        /// </summary>
        public string SysBankcode
        {
            get
            {
                return viviapi.SysInterface.Bank.YeePay.ConverBankCode(Bankid);
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
                return GetParmValue("p3_Amt");
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
                return HttpUtility.UrlDecode(GetParmValue("p2_Order"), System.Text.Encoding.GetEncoding("gb2312"));
            }
        }
        #endregion

        #region 商户接收支付成功数据的地址
        /// <summary>
        /// 商户接收支付成功数据的地址
        /// 支付成功后易宝支付会向该地址发送两次成功通知，该地址可以带参数，如:
        ////“ www.yeepay.com/callback.action?test=test”.
        ////注意：如不填p8_Url的参数值支付成功后您将得不到支付成功的通知。
        /// </summary>
        public string Notifyurl
        {
            get
            {
                return HttpUtility.UrlDecode(GetParmValue("p8_Url"), System.Text.Encoding.GetEncoding("gb2312"));
            }
        }
        #endregion

        #region 下行同步通知地址
        /// <summary>
        ///p9_SAF 送货地址
        /// </summary>
        public string Returnurl
        {
            get
            {
                return HttpUtility.UrlDecode(GetParmValue("p9_SAF"), System.Text.Encoding.GetEncoding("gb2312"));
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
        /// 返回时原样返回，此参数如用到中文，请注意转码
        /// </summary>
        public string Attach
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("pa_MP", ""), System.Text.Encoding.GetEncoding("GB2312"));
            }
        }
        #endregion

        #region 版本
        /// <summary>
        ///
        ///</summary>
        public string Version
        {
            get
            {
                return viviapi.SysInterface.Bank.YeePay.VbYee10;
            }
        }
        #endregion

        #region hmac签名
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign
        {
            get
            {
                return GetParmValue("hmac");
            }
        }
        #endregion

        #region p5_Pid
        /// <summary>
        ///商品名称
        /// </summary>
        public string P5Pid
        {
            get
            {
                return HttpUtility.UrlDecode(GetParmValue("p5_Pid"), System.Text.Encoding.GetEncoding("gb2312"));

            }
        }
        #endregion

        #region p6_Pcat
        /// <summary>
        ///p6_Pcat 商品种类
        /// </summary>
        public string P6Pcat
        {
            get
            {
                return HttpUtility.UrlDecode(GetParmValue("p6_Pcat"), System.Text.Encoding.GetEncoding("gb2312"));

            }
        }
        #endregion

        #region p7_Pdesc
        /// <summary>
        ///p7_Pdesc 商品描述
        /// </summary>
        public string P7Pdesc
        {
            get
            {
                return HttpUtility.UrlDecode(GetParmValue("p7_Pdesc"), System.Text.Encoding.GetEncoding("gb2312"));

            }
        }
        #endregion

        #region pr_NeedResponse
        /// <summary>
        ///pr_NeedResponse 1
        /// </summary>
        public string PrNeedResponse
        {
            get
            {
                return HttpUtility.UrlDecode(GetParmValue("pr_NeedResponse"), System.Text.Encoding.GetEncoding("gb2312"));

            }
        }
        #endregion
        #endregion

        bool IsReturnUrlOk()
        {
            if (string.IsNullOrEmpty(this.Returnurl))
                return true;
            bool isUrl = viviLib.Text.Validate.IsUrl(Returnurl);
            return isUrl;
        }

        bool IsNotifyUrlOk()
        {
            if (string.IsNullOrEmpty(this.Notifyurl))
                return false;

            bool isUrl = viviLib.Text.Validate.IsUrl(Notifyurl);

            return isUrl;
        }

        bool IsClientIpOk()
        {
            if (string.IsNullOrEmpty(this.ClientIp))
                return true;
            else return viviLib.Text.Validate.IsIPSect(ClientIp);
        }

        #region 通道类型ID
        /// <summary>
        /// 
        /// </summary>
        public int ChannelTypeId
        {
            get
            {
                int typeid = 102;
                switch (SysBankcode)
                {
                    case "992"://支付宝
                        typeid = 101;
                        break;

                    case "993"://支付宝
                        typeid = 100;
                        break;

                    case "1003"://支付宝
                        typeid = 206;
                        break;

                    default:
                        typeid = 102;
                        break;
                }
                return typeid;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string error = string.Empty, apikey = "";
            bool riskWarning = true;


            #region Step1 必要的参数不能为空
            if (string.IsNullOrEmpty(this.Userid))
            {
                error = "error:1001 商户ID（p1_MerId）不能空！";
            }
            else if (string.IsNullOrEmpty(Bankid))
            {
                error = "error:1002 银行类型（pd_FrpId）不能空！";
            }
            else if (string.IsNullOrEmpty(Money))
            {
                error = "error:1003 订单金额（p3_Amt）不能空！";
            }
            else if (string.IsNullOrEmpty(Orderid))
            {
                error = "error:1004 商户订单号（p2_Order）不能空！";
            }
            else if (string.IsNullOrEmpty(Notifyurl))
            {
                error = "error:1005 下行异步通知地址（p8_Url）不能空！";
            }
            /*else if (string.IsNullOrEmpty(version))
             {
                 error = "error:1006 版本号（version）不能空！";
             }*/
            else if (string.IsNullOrEmpty(Sign))
            {
                error = "error:1006 MD5签名（hmac）不能空！";
            }
            #endregion

            #region Step2 检查参数长度
            else if (Userid.Length > 11)
            {
                error = "error:1020 商户ID（p1_MerId）长度超过11位！";
            }
            else if (Bankid.Length > 50)
            {
                error = "error:1021 支付通道编码（pd_FrpId）长度超过50位！";
            }
            else if (Orderid.Length > 50)
            {
                error = "error:1022 商户订单号（p2_Order）长度超过50位！";
            }
            else if (Money.Length > 20)
            {
                error = "error:1023 订单金额（ p3_Amt）长度超过最长限制！";
            }
            else if (this.Notifyurl.Length > 255)
            {
                error = "error:1024 下行异步通知地址（p8_Url）长度超过255位！";
            }
            else if (this.Returnurl.Length > 2)
            {
                error = "error:1025  p9_SAF（ p9_SAF）长度超过255位！";
            }
            //else if (this.clientIp.Length > 20)
            //{
            //    error = "error:1026 支付用户IP（payerIp）长度超过20位！";
            //}
            else if (this.Attach.Length > 255)
            {
                error = "error:1027 商户扩展信息（pa_MP）长度超过255位！";
            }
            else if (this.Sign.Length != 32)
            {
                error = "error:1028 签名数据（hmac）长度不正确！";
            }
            #endregion

            #region Step3 格式验证
            else if (!viviLib.Text.Validate.IsNumeric(Userid))
            {
                error = "error:1040 商户编号（p1_MerId）格式不正确！";
            }
            //else if (!viviLib.Text.Validate.IsLetterOrNumber(bankid))
            //{
            //    error = "error:1041 支付通道编码（pd_FrpId）格式不正确！";
            //}
            //else if (!viviLib.Text.Validate.IsNumber(money) || !viviLib.Text.Validate.IsNumeric(money))//二位小数
            //{
            //    error = "error:1042 订单金额（value）格式不正确！";
            //}
            else if (!IsNotifyUrlOk())
            {
                error = "error:1043  商户接收支付成功数据的地址（p8_Url）格式不正确！";
            }
            //else if (!isReturnUrlOk())
            //{
            //    error = "error:1044 送货地址（hrefbackurl）格式不正确！";
            //}
            //else if (!isClientIpOk())
            //{
            //    error = "error:1045 支付用户IP（payerIp）格式不正确！";
            //}
            #endregion

            //完成基础资料的验证
            if (!string.IsNullOrEmpty(error))
            {
                WebUtility.ShowErrorMsg(error);
                return;
            }
            UserInfo userInfo = null;
            decimal tranAmt = decimal.Zero;
            int userId = 0;
            if (!int.TryParse(Userid, out userId))
            {
                error = "error:1064 商户编号不存在！";
            }

            #region Step4 数据验证
            else if (!decimal.TryParse(Money, out tranAmt))
            {
                error = "error:1060 支付金额（p3_Amt）有误！";
            }
            else if (tranAmt < this.MinTranAMT)
            {
                error = "error:1061 订单金额（p3_Amt）小于最小允许交易额！";
            }
            else if (tranAmt > this.MaxChargeAMT)
            {
                error = string.Format("error:1062 订单金额（p3_Amt）{0:f2}大于最大允许交易额{1:f2}！", tranAmt, this.MaxChargeAMT);
            }
            else
            {
                var checkResult = Factory.Instance.CheckApiParms(userId, ChannelTypeId, RequiredCheckUserOrderNo, this.Orderid);
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

                    else if (!viviapi.SysInterface.Bank.YeePay.CheckSign(this.Userid
                        , this.Orderid
                        , this.Money
                        , this.P4Cur
                        , this.P5Pid
                        , this.P6Pcat
                        , this.P7Pdesc
                        , Notifyurl
                        , Returnurl
                        , Attach
                        , Bankid
                        , PrNeedResponse
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
            var channelInfo = viviapi.BLL.Channel.Factory.GetModel(ChannelTypeId, SysBankcode, userId, true);

            if (channelInfo == null)
            {
                error = "error:1067:银行编号不存在!";
            }
            else if (channelInfo.isOpen.Value != 1)
            {
                error = "error:1068:通道维护中!";
            }


            if (!string.IsNullOrEmpty(error))
            {
                #region
                if (DebuglogOpen)
                {
                    if (userInfo != null && userInfo.isdebug == 1)
                    {
                        var debugInfo = new debuginfo
                        {
                            addtime = DateTime.Now,
                            bugtype = viviapi.Model.Sys.debugtypeenum.网银订单,
                            detail = string.Empty,
                            errorcode = error,
                            errorinfo = error,
                            userid = userInfo.ID
                        };
                        if (Request.RawUrl != null)
                            debugInfo.url = Request.RawUrl.ToString();
                        else
                            debugInfo.url = string.Empty;

                        viviapi.BLL.Sys.Debuglog.Insert(debugInfo);
                    }
                }
                #endregion

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
                notifyurl = Notifyurl,
                clientip = ServerVariables.TrueIP,
                completetime = DateTime.Now,
                returnurl = Notifyurl,
                ordertype = 1,
                typeId = typeId,
                paymodeId = SysBankcode,
                supplierId = supplierId,
                supplierOrder = string.Empty,
                userid = userId,
                userorder = Orderid,
                refervalue = tranAmt,
                cus_subject = P5Pid,
                cus_field1 = P6Pcat,
                cus_description = P7Pdesc,
                cus_field2 = P4Cur,
                cus_field3 = PrNeedResponse,
                cus_field4 = Returnurl,
                server = RuntimeSetting.ServerId,
                version = Version
            };
            //order.payRate = 0M;
            //order.supplierRate = chanelInfo.supprate;

            if (Request.UrlReferrer != null)
                order.referUrl = Request.UrlReferrer.ToString();
            else
            {
                order.referUrl = string.Empty;
            }

            //订单所属业务
            order.manageId = userInfo.manageId;
            if (!order.manageId.HasValue || order.manageId.Value <= 0)
            {
                order.agentId = viviapi.BLL.User.Factory.GetPromID(userId);
            }
            viviapi.Cache.WebCache.GetCacheService().AddObject(order.orderid, order, ExpiresTime);
            //         
            NewOrder.Insert(order);

            SellFactory.OnlineBankPay(order.userid, apikey, supplierId, order.orderid, order.refervalue, order.paymodeId, riskWarning);
        }
    }
}
