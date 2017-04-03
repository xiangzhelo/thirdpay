using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using viviapi.BLL.Sys;
using viviapi.BLL.Tools;
using viviapi.Cache;
using viviapi.WebComponents.Web;
using viviAPI.WebUI2015.usermodule.safety;
using viviLib.ExceptionHandling;
using viviLib.Text;

namespace viviAPI.WebUI7uka.agentmodule.WS
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SendPhoneVerifyCode : UserHandlerBase
    {
        public override void OnLoad(HttpContext context)
        {
            string msg = "";

            try
            {
                #region
                string phone = GetValue("phone");

                if (!string.IsNullOrEmpty(phone))
                {
                    if (phone == CurrentUser.Tel)
                    {
                        msg = "不能为原手机号码一样";
                    }
                    else if (Validate.IsMobileNum(phone))
                    {

                        #region  设置短信发送信息
                        CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
                        //ip格式如下，不带https://
                        //app.cloopen.com:8883
                        bool isInit = api.init("app.cloopen.com", "8883");
                        api.setAccount("8a48b5515018a0f4015045e342b14990", "07c2d4f927a1443fb4ffffe158ee39b8");
                       // api.setAppId("aaf98f8954939ed50154bdd0afb22d09");
                        #endregion
                        bool isupdate = (CurrentUser.IsPhonePass == 1);

                        string cacheKey = string.Format(Constant.PhoneVerificationCacheKey, phone);

                        string validcode = (string)WebCache.GetCacheService().RetrieveObject(cacheKey);
                        if (validcode == null)
                        {
                            validcode = new Random().Next(10000, 99999).ToString(CultureInfo.InvariantCulture);
                            WebCache.GetCacheService().AddObject(cacheKey, validcode);
                        }

                        string smscontext = ""; //string.Format("您的验证码是:{0}[{1}]", validcode, viviapi.BLL.WebInfoFactory.CurrentWebInfo.Name);
                        if (isupdate)
                            api.setAppId("aaf98f8954939ed50154bdd0afb22d09");
                       // smscontext = SMSTempSettings.SMS_Temp_Modify;
                        else
                        {
                            api.setAppId("aaf98f8954939ed50154bdd0afb22d09");
                            //smscontext = SMSTempSettings.SMS_Temp_Authenticate;
                        }

                        //if (!string.IsNullOrEmpty(smscontext))
                        //{
                        //    smscontext = smscontext.Replace("{@username}", CurrentUser.full_name);
                        //    smscontext = smscontext.Replace("{@sitename}",
                        //        viviapi.BLL.WebInfoFactory.CurrentWebInfo.Name);
                        //    smscontext = smscontext.Replace("{@authcode}", validcode);
                        //}


                        if (isInit)
                        {
                            // Dictionary<string, object> retData = api.SendTemplateSMS(短信接收号码, 短信模板id, 内容数据);

                           string[] data = { "铭云支付商户-大宝", "100" };
                           //  string[] data = { CurrentUser.full_name, validcode };
                            Dictionary<string, object> retData = api.SendTemplateSMS(phone, "87105", data);
                            msg = classHelp.getDictionaryData(retData);
                        }
                        else
                        {
                            msg = "初始化失败";
                        }


                        //string result = SMS.SendSmsWithCheck(phone, smscontext, "");
                        //if (string.IsNullOrEmpty(result))
                        //{
                        //    msg = "true";
                        //}
                        //else
                        //{
                        //    msg = result;
                        //}
                    }
                    else
                    {
                        msg = "请输入正确的手机号码123";
                    }
                }
                else
                {
                    msg = "请输入手机号码";
                }
                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                msg = "error";
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
        }

    }
}
