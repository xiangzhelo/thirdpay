using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Channel;
using viviapi.BLL.User;
using viviapi.ETAPI.ebao2;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviapi.SysConfig;
using viviapi.WebComponents;
using viviLib.Security;
using viviLib.Web;
using Factory = viviapi.BLL.Order.Bank.Factory;

namespace viviAPI.Gateway2018
{
    public partial class yeepayNewApi : viviapi.WebComponents.GateWay.BankTransBase
    {
        private const int ChannelTypeId = 212;
        private const string Bankid = "104";
        protected OrderBank NewOrder = new OrderBank();
        protected void Page_Load(object sender, EventArgs e)
        {
            string error = string.Empty, apikey = string.Empty;
            decimal tranAmt = decimal.Zero;
            UserInfo userInfo = null;
            int userId = 1080;
            //请求的集合
            NameValueCollection nvcSource = Request.Form == null && Request.Form.Count > 0 ? Request.Form : Request.QueryString;
            string data = nvcSource["data"];
            string aeskey = "1234567890123456";
            string preEncode = AESUtil.Decrypt(data, aeskey);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(preEncode);
            NameValueCollection nvc = new NameValueCollection();
            foreach (var item in dic)
            {
                nvc.Add(item.Key, item.Value);
            }
            //数据验证
            string Money = nvc["amount"];
            string Orderid = nvc["requestid"];
            Notifyurl = nvc["callbackurl"];
            #region Step4 数据验证
            if (!CheckUrlReferrer(userId))
            {
                string host = string.Empty;
                if (Request.UrlReferrer != null)
                    host = Request.UrlReferrer.Host;

                error = string.Format("error:1070 来路地址不合法！{0}", host);
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
                var checkResult = Factory.Instance.CheckApiParms(userId, ChannelTypeId, RequiredCheckUserOrderNo, Orderid);
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
                    else if (!VerifyHmac(nvc, userInfo.APIKey))
                    {
                        error = "error:1066 签名错误!";
                    }
                    else
                    {
                        apikey = userInfo.APIKey;
                        //riskWarning = userInfo.RiskWarning == 1 ? true : false;
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
            var channelInfo = viviapi.BLL.Channel.Factory.GetModel(ChannelTypeId, Bankid, userId, true);

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

            if (channelInfo != null)
            {
                typeId = channelInfo.typeId;
                if (channelInfo.supplier != null)
                    supplierId = channelInfo.supplier.Value;
            }

            var order = new OrderBankInfo
            {
                orderid = Factory.Instance.GenerateOrderId(OrderPrefix),
                addtime = DateTime.Now,
                attach = Attach,
                notifycontext = string.Empty,
                notifycount = 0,
                notifystat = 0,
                notifyurl = this.Notifyurl,
                clientip = ServerVariables.TrueIP,
                completetime = DateTime.Now,
                returnurl = "",
                ordertype = 1,
                typeId = typeId,
                paymodeId = Bankid,
                supplierId = supplierId,
                supplierOrder = string.Empty,
                userid = userId,
                userorder = Orderid,
                refervalue = tranAmt,
                referUrl = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : string.Empty,
                server = RuntimeSetting.ServerId,
                manageId = 0,
                version = version
            };

            if (userInfo != null) order.manageId = userInfo.manageId; //业务

            if (!order.manageId.HasValue || order.manageId.Value <= 0)
            {

                order.agentId = viviapi.BLL.User.Factory.GetPromID(userId);
            }
            viviapi.Cache.WebCache.GetCacheService().AddObject(order.orderid, order, ExpiresTime);
            //         
            NewOrder.Insert(order);

            //viviapi.ETAPI.ebao.EBaoApi api = new viviapi.ETAPI.ebao.EBaoApi();
            //string ret = string.Empty;
            ////if (nvc.AllKeys.Contains("p8_FrpId") && !string.IsNullOrEmpty(nvc.Get("p8_FrpId")))
            ////{
            ////    ret = api.Pay(nvc, "2");
            ////    Response.Write(ret);
            ////}
            ////else
            ////{
            ////    ret = api.Pay(nvc, "1");
            ////    Response.Redirect(ret);
            ////}
            //ret = api.Pay(nvc, "1");

            viviapi.ETAPI.ebao2.EBaoNewApi api = new EBaoNewApi();
            string ret = api.Pay(nvc, HttpContext.Current);
            return;
        }

        private bool VerifyHmac(NameValueCollection nvc, string apiKey)
        {
            string sign = nvc.Get("hmac");
            string preSign = string.Empty;
            for (int i = 0; i < nvc.Count; i++)
            {
                if (nvc.GetKey(i) != "hmac")
                {
                    preSign += nvc.GetKey(i) + "=" + nvc.Get(i) + "&";
                }
            }
            preSign = preSign.TrimEnd("&".ToArray()) + apiKey;
            string thisSign = Cryptography.MD5(preSign).ToLower();
            return true;
            //return thisSign == sign;

        }

        private bool CheckUrlReferrer(int userId)
        {
            if (RequiredCheckUrlReferrer)
            {
                if (Request.UrlReferrer == null)
                    return false;

                var hostBLL = new UserHost();
                return hostBLL.Exists(userId, Request.UrlReferrer.Host);
            }

            return true;
        }
        #region  需要的参数
        public string Notifyurl { get; set; }
        #endregion
        #region 备注消息
        /// <summary>
        /// 商户扩展信息，返回时原样返回，此参数如用到中文，请注意转码
        /// </summary>
        public string Attach
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("p7_MP", ""), System.Text.Encoding.GetEncoding("GB2312"));
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
                return viviapi.SysInterface.Bank.MyAPI.Utility.EnName; //WebBase.GetQueryStringString("version", "");
            }
        }
        #endregion
    }
}