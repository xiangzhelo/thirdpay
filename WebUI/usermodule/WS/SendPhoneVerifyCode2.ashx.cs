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
using viviLib.ExceptionHandling;
using viviLib.Text;
using System.Web.SessionState;

namespace viviAPI.WebUI7uka.usermodule.WS
{
    
    public class SendPhoneVerifyCode2 : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            OnLoad(context);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public string GetValue(string param)
        {
            string val = string.Empty;

            try
            {
                if (HttpContext.Current.Request.Form[param] != null)
                {
                    val = HttpContext.Current.Request.Form[param];
                }
                else if (HttpContext.Current.Request.QueryString[param] != null)
                {
                    val = HttpContext.Current.Request.QueryString[param];
                }
                else
                {
                    val = "";
                }
            }
            catch (Exception exception)
            {
            }
            return val;
        }

        public void OnLoad(HttpContext context)
        {
            string msg = "";

            try
            {
                #region
                string phone = GetValue("phone");

                if (!string.IsNullOrEmpty(phone))
                {
                    //if (phone == CurrentUser.Tel)
                    //{
                    //    msg = "不能为原手机号码一样";
                    //}
                    if (Validate.IsMobileNum(phone))
                    {
                        //bool isupdate = (CurrentUser.IsPhonePass == 1);

                        string cacheKey = string.Format(Constant.PhoneVerificationCacheKey, phone);

                        string validcode = (string)WebCache.GetCacheService().RetrieveObject(cacheKey);
                        if (validcode == null)
                        {
                            validcode = new Random().Next(10000, 99999).ToString(CultureInfo.InvariantCulture);
                            WebCache.GetCacheService().AddObject(cacheKey, validcode);
                        }

                        string smscontext = ""; //string.Format("您的验证码是:{0}[{1}]", validcode, viviapi.BLL.WebInfoFactory.CurrentWebInfo.Name);
                        //if (isupdate)
                        //    smscontext = SMSTempSettings.SMS_Temp_Modify;
                        //else
                        //{
                        smscontext = SMSTempSettings.SMS_Temp_Authenticate;
                        //}

                        if (!string.IsNullOrEmpty(smscontext))
                        {
                            //smscontext = smscontext.Replace("{@username}", CurrentUser.full_name);
                            smscontext = smscontext.Replace("{@sitename}",
                                viviapi.BLL.WebInfoFactory.CurrentWebInfo.Name);
                            smscontext = smscontext.Replace("{@authcode}", validcode);
                        }


                        string result = SMS.SendSmsWithCheck(phone, smscontext, "");
                        if (string.IsNullOrEmpty(result))
                        {
                            msg = "true";
                        }
                        else
                        {
                            msg = result;
                        }
                    }
                    else
                    {
                        msg = "请输入正确的手机号码";
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
