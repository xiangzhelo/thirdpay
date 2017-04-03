using System;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.Order.Card;
using viviapi.Model.supplier;
using viviapi.SysConfig;
////
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.ETAPI.OfCard
{
    /// <summary>
    /// 欧飞API接口操作相关类
    /// </summary>
    public class Card : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.OfCard;

        public Card()
            : base(SuppId)
        {

        }

        public static Card Default
        {
            get
            {
                var ofpay = new Card();
                return ofpay;
            }
        }

        public string NotifyUrl
        {
            get
            {
                return SiteDomain + "/receive/ofcard/card.aspx";
            }
        }
        /// <summary>
        /// 本系统接收到上级接口发送的异步通知时，需要返回给上级接口的信息，表示本系统已成功接收处理结果。
        /// </summary>
        internal string Succflag = "OK";

        #region CardSend
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public CardSynchCallBack CardSend(CardOrderSummitArgs o)
        {
            var callBack = new CardSynchCallBack();
            try
            {
                string commitUrl = PostCardUrl;
                string usercode = SuppAccount;
                string cardno = o.CardNo;
                string cardpass = o.CardPass;
                string cardcode = GetCardType(o.CardTypeId) + o.FaceValue.ToString();
                string mode = "r";
                string version = "1.0";
                string orderno = o.SysOrderNo;
                string retaction = NotifyUrl;

                string datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string format = "xml";
                string md5key = SuppKey;
                string signsrc = usercode + mode + version + orderno + cardcode + cardno + cardpass + retaction + datetime + format + md5key;
                string sign = md5sign(signsrc).ToLower();
                string postParam = "usercode=" + usercode + "&mode=" + mode + "&version=" + version + "&orderno=" + orderno + "&cardcode=" + cardcode + "&cardno=" + cardno + "&cardpass=" + cardpass + "&retaction=" + retaction + "&datetime=" + datetime + "&format=" + format + "&sign=" + sign;

                SynsSummitLogger(commitUrl);
                SynsSummitLogger(postParam);

                System.Text.Encoding gbkEncoding = System.Text.Encoding.GetEncoding("GBK");
                byte[] bs = gbkEncoding.GetBytes(postParam);
                WebRequest req = (HttpWebRequest)WebRequest.Create(commitUrl);
                req.Timeout = 50000;//设置超时时间(毫秒),最长的等待时间
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = bs.Length;
                using (System.IO.Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }
                var sr = new System.IO.StreamReader(req.GetResponse().GetResponseStream(), gbkEncoding);
                String retXml = sr.ReadToEnd();
                sr.Close();

                SynsSummitLogger(retXml);

                var doc = new System.Xml.XmlDocument();
                doc.LoadXml(retXml);

                string result = doc.GetElementsByTagName("result")[0].InnerText;
                string billid = doc.GetElementsByTagName("billid")[0].InnerText;
                string info = doc.GetElementsByTagName("info")[0].InnerText;


                callBack.Success = 1;
                callBack.SuppTransNo = billid;
                callBack.SuppCallBackText = retXml;
                callBack.SuppErrorCode = result;
                callBack.SuppErrorMsg = info;

                if (!string.IsNullOrEmpty(result))
                {
                    if (result == "2001")
                    {
                        callBack.SummitStatus = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);

                callBack.Success = 0;
                callBack.Message = ex.Message;
            }
            return callBack;
        }
        #endregion

        /// <summary>
        /// 处理完成，返回通知
        /// </summary>
        public void Notify()
        {
            AsynsRetLogger(HttpContext.Current.Request.RawUrl);

            if (HttpContext.Current.Request.Form.Count > 0)
            {
                string data = WebClientHelper.FormatRequestData(HttpContext.Current.Request.Form, System.Text.Encoding.Default);
                AsynsRetLogger(data);
            }

            try
            {
                string md5key = SuppKey;
                int status = 4;
                string opstate = "-1";
                string usercode = HttpContext.Current.Request.Form["usercode"];
                string mode = HttpContext.Current.Request.Form["mode"];
                string version = HttpContext.Current.Request.Form["version"];
                string orderno = HttpContext.Current.Request.Form["orderno"];//本系统内部单号
                string billid = HttpContext.Current.Request.Form["billid"];//欧飞单号
                string result = HttpContext.Current.Request.Form["result"];//返回处理结果代码
                string info = HttpContext.Current.Request.Form["info"];//交易情况说明
                string value = HttpContext.Current.Request.Form["value"];//面值
                string accountvalue = HttpContext.Current.Request.Form["accountvalue"];
                string datetime = HttpContext.Current.Request.Form["datetime"];
                string sign = HttpContext.Current.Request.Form["sign"];

                string srcStr = usercode + mode + version + orderno + billid + result + info + value + accountvalue + datetime + md5key;
                string md5str = md5sign(srcStr);

                if (md5str.ToLower() == sign.ToLower())
                {      //status=2 支付成功 status=4支付失败             
                    status = (result == "2000" || result == "2011") ? 2 : 4;
                    if (status == 2)
                        opstate = "0";
                    else
                        opstate = ConvertCode(result);
                    var response = new CardOrderSupplierResponse()
                    {
                        SupplierId = SuppId,
                        SuppTransNo = billid,
                        SysOrderNo = orderno,
                        OrderAmt = decimal.Parse(value),
                        SuppAmt = decimal.Parse(accountvalue),
                        OrderStatus = status,
                        SuppErrorCode = result,
                        Opstate = opstate,
                        SuppErrorMsg = info,
                        ViewMsg = info,
                        Method = 1
                    };

                    OrderCardUtils.SuppNotify(response, Succflag);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        ///opstate=-1卡号或密码错误
        ///opstate=-2卡号过期
        ///opstate=-4卡号不存在
        ///opstate=-5卡已使用过
        ///opstate=-6卡号被冻结
        ///opstate=-7卡未激活
        ///opstate=-8不支持的卡类型或金额
        ///opstate=-9其他游戏专用卡
        ///opstate=-11其他错误
        /// </summary>
        /// <param name="suppcode"></param>
        /// <returns></returns>
        public string ConvertCode(string suppcode)
        {
            string syscode = string.Empty;
            if (suppcode == "2010" || suppcode == "2016") //充值卡无效 密码错误
            {
                syscode = "-1";//卡号或密码错误
            }
            else if (suppcode == "2999" || suppcode == "2012" || suppcode == "2013")
            {
                syscode = "-11";//网络出错 系统繁忙
            }
            else if (suppcode == "2018")
            {
                syscode = "-10";//卡余额不足
            }
            if (string.IsNullOrEmpty(suppcode))
            {
                syscode = "-1";
            }
            return syscode;
        }

        #region 方法
        public string GetCardType(int _type)
        {
            switch (_type)
            {
                case 103:
                    return "000101";//移动神州行

                case 210:
                case 104:
                    return "000601";//盛大一卡通

                case 105:
                    return "000901";//征途一卡通

                case 106:
                    return "000501";//骏网一卡通

                case 107:
                    return "000701";//QQ币充值卡

                case 108:
                    return "000201";//联通一卡充

                case 109:
                    return "001201";//久游一卡通

                case 110:
                    return "001001";//网易一卡通

                case 111:
                    return "000801";//完美一卡通

                case 112:
                    return "001101";//搜狐一卡通

                case 113:
                    return "000301";//电信国卡

                case 117:
                    return "001601";//纵游一卡通

                case 118:
                    return "002201";//欧飞走天下一卡通接口返回不支持此类型 需要走一卡通专项
                    //return "001401";//天下一卡通

                case 119:
                    return "001301";//天宏一卡通              

                case 208:
                    return "000401";//殴飞一卡通

                case 209:
                    return "002201";//天下一卡通专项
            }
            return _type.ToString();
        }


        public static string md5sign(string str)
        {
            System.Text.Encoding gbkEncoding = System.Text.Encoding.GetEncoding("GBK");
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string a = BitConverter.ToString(md5.ComputeHash(gbkEncoding.GetBytes(str)));
            a = a.Replace("-", "");
            return a.ToLower();
        }
        #endregion

        #region Query
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cardType"></param>
        /// <param name="_money"></param>
        /// <param name="_cardNo"></param>
        /// <param name="_cardpwd"></param>
        /// <param name="_orderid"></param>
        /// <returns></returns>
        public string Query(string orderid)
        {
            #region 返回数据格式
            //<?xml version="1.0" encoding="gbk"?>
            //    <root><usercode>A1081794</usercode>
            //<mode>q</mode>
            //<version>1.0</version>
            //<orderno>C5669548794170966980</orderno>
            //<billid>R151006270963751</billid>
            //<result>2016</result>
            //<info>密码错误</info>
            //<value>0</value>
            //<accountvalue>0</accountvalue>
            //<datetime>20151007002546</datetime>
            //<sign>f9371497b583390bb15c70ccfd152108</sign>
            //    </root>
            #endregion
            orderid = orderid.Trim();
            string callback = string.Empty;

            string commitUrl = "http://card.pay.ofpay.com/querycard.do";
            string usercode = SuppAccount;
            string mode = "q";
            string version = "1.0";
            string orderno = orderid;
            string format = "xml";
            string md5key = SuppKey;

            string signsrc = usercode + mode + version + orderno + format + md5key;

            //viviLib.Logging.LogHelper.Write(signsrc);

            string sign = md5sign(signsrc).ToLower();

            //viviLib.Logging.LogHelper.Write(sign);

            string postParam = "usercode=" + usercode + "&mode=" + mode + "&version=" + version + "&orderno=" + orderno + "&format=" + format + "&sign=" + sign;

            try
            {
                System.Text.Encoding gbkEncoding = System.Text.Encoding.GetEncoding("GBK");
                byte[] bs = gbkEncoding.GetBytes(postParam);
                WebRequest req = (HttpWebRequest)WebRequest.Create(commitUrl);
                req.Timeout = 50000;//设置超时时间(毫秒),最长的等待时间
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = bs.Length;
                using (System.IO.Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }
                System.IO.StreamReader sr = new System.IO.StreamReader(req.GetResponse().GetResponseStream(), gbkEncoding);
                String retXml = sr.ReadToEnd();
                sr.Close();

                callback = retXml;

            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);

                callback = ex.Message;
            }
            return callback;
        }
        #endregion

        #region Finish
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public bool Finish(string callback)
        {
            bool success = false;
            #region 返回数据格式
            //<?xml version="1.0" encoding="gbk"?>
            //    <root><usercode>A1081794</usercode>
            //<mode>q</mode>
            //<version>1.0</version>
            //<orderno>C5669548794170966980</orderno>
            //<billid>R151006270963751</billid>
            //<result>2016</result>
            //<info>密码错误</info>
            //<value>0</value>
            //<accountvalue>0</accountvalue>
            //<datetime>20151007002546</datetime>
            //<sign>f9371497b583390bb15c70ccfd152108</sign>
            //    </root>
            #endregion
            try
            {
                if (!string.IsNullOrEmpty(callback))
                {
                    var doc = new System.Xml.XmlDocument();
                    doc.LoadXml(callback);

                    string usercode = doc.GetElementsByTagName("usercode")[0].InnerText;
                    string mode = doc.GetElementsByTagName("mode")[0].InnerText;
                    string version = doc.GetElementsByTagName("version")[0].InnerText;
                    string orderno = doc.GetElementsByTagName("orderno")[0].InnerText;//翁贝系统生成的订单号
                    string billid = doc.GetElementsByTagName("billid")[0].InnerText;//欧飞订单号
                    string result = doc.GetElementsByTagName("result")[0].InnerText;//返回代码
                    string info = doc.GetElementsByTagName("info")[0].InnerText;//交易情况说明
                    string value = doc.GetElementsByTagName("value")[0].InnerText;//充值卡面值
                    string accountvalue = doc.GetElementsByTagName("accountvalue")[0].InnerText;//结算金额
                    string datetime = doc.GetElementsByTagName("datetime")[0].InnerText;//日期时间，格式：YYYYMMDDHHMMSS
                    string sign = doc.GetElementsByTagName("sign")[0].InnerText;

                    string srcStr = usercode + mode + version + orderno + billid + result + info + value + accountvalue + datetime + SuppKey;
                    string md5Str = md5sign(srcStr);

                    string opstate = "-1";
                    int status = 4;

                    if (md5Str.ToLower() == sign.ToLower())
                    {
                        if (result == "2000" || result == "2011")
                        {
                            opstate = "0";
                            status = 2;
                        }

                        if (result == "2014" || result == "2001")
                        {
                            opstate = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(opstate))
                        {
                            var response = new CardOrderSupplierResponse()
                            {
                                SupplierId = SuppId,
                                SuppTransNo = billid,
                                SysOrderNo = orderno,
                                OrderAmt = decimal.Parse(value),
                                SuppAmt = decimal.Parse(accountvalue),
                                OrderStatus = status,
                                SuppErrorCode = result,
                                Opstate = opstate,
                                SuppErrorMsg = info,
                                ViewMsg = info,
                                Method = 1
                            };

                            OrderCardUtils.Finish(response);

                            success = true;
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }

            return success;
        }
        #endregion
    }
}
