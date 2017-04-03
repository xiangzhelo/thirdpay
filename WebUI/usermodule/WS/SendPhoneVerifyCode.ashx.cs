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

namespace viviAPI.WebUI7uka.usermodule.WS
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class
        SendPhoneVerifyCode : UserHandlerBase
    {
        public override void OnLoad(HttpContext context)
        {
            string msg = "";

            try
            {
                #region
                string phone = GetValue("phone");
                string ValidateCode = GetValue("ValidateCode");
                if (!string.IsNullOrEmpty(phone))
                {
                    if (string.IsNullOrEmpty(ValidateCode))
                    {
                        msg = "验证码为空，请输入验证码";
                    }
                    else
                    {
                        //验证验证码
                        if (context.Session["_ValidateCode"] == null)
                        {
                            msg = "验证码过期";


                        }
                        else
                        {
                            string sessionCode = context.Session["_ValidateCode"].ToString();
                            if (sessionCode.ToLower() != ValidateCode.ToLower())
                            {
                                msg = "验证码错误, 请重新输入!";

                            }

                            else
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
                                    api.setAccount("8aaf07085adadc12015adb64bea70074", "547a3c1801b04b6ab2ebedc5d7ed1694");
                                    api.setAppId("8aaf07085adadc12015adb64c05a007b");
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

                                    if (isInit)
                                    {
                                        // Dictionary<string, object> retData = api.SendTemplateSMS(短信接收号码, 短信模板id, 内容数据);

                                        string[] data = { CurrentUser.full_name, validcode };
                                        //  string[] data = { CurrentUser.full_name, validcode };
                                        Dictionary<string, object> retData = api.SendTemplateSMS(phone, "162881", data);
                                        if (retData["statusCode"].ToString() == "000000")
                                        {
                                            msg = "true";
                                        }
                                        else
                                        {
                                            msg = classHelp.getDictionaryData(retData);
                                        }
                                        context.Session["_ValidateCode"] = null;
                                    }
                                    else
                                    {
                                        msg = "初始化失败";
                                    }

                                    #region 原有的发送短信
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

                                //else if (Validate.IsMobileNum(phone))
                                //{
                                //    bool isupdate = (CurrentUser.IsPhonePass == 1);

                                //    string cacheKey = string.Format(Constant.PhoneVerificationCacheKey, phone);

                                //    string validcode = (string)WebCache.GetCacheService().RetrieveObject(cacheKey);
                                //    if (validcode == null)
                                //    {
                                //        validcode = new Random().Next(10000, 99999).ToString(CultureInfo.InvariantCulture);
                                //        WebCache.GetCacheService().AddObject(cacheKey, validcode);
                                //    }

                                //    string smscontext = ""; //string.Format("您的验证码是:{0}[{1}]", validcode, viviapi.BLL.WebInfoFactory.CurrentWebInfo.Name);
                                //    if (isupdate)
                                //        smscontext = SMSTempSettings.SMS_Temp_Modify;
                                //    else
                                //    {

                                //        smscontext = SMSTempSettings.SMS_Temp_Authenticate;
                                //    }

                                //    if (!string.IsNullOrEmpty(smscontext))
                                //    {
                                //        smscontext = smscontext.Replace("{@username}", CurrentUser.full_name);
                                //        smscontext = smscontext.Replace("{@sitename}",
                                //            viviapi.BLL.WebInfoFactory.CurrentWebInfo.Name);
                                //        smscontext = smscontext.Replace("{@authcode}", validcode);
                                //    }


                                //    string result = SMS.SendSmsWithCheck(phone, smscontext, "");
                                //    if (string.IsNullOrEmpty(result))
                                //    {
                                //        msg = "true";
                                //    }
                                //    else
                                //    {
                                //        msg = result;
                                //    }

                                //}
                                #endregion
                                else
                                {
                                    msg = "请输入正确的手机号码";
                                }
                            }
                        }
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
