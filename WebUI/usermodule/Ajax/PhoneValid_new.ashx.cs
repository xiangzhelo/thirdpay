using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using viviapi.BLL.Sys;
using viviLib;
using viviapi.Model;
using viviapi.BLL;

namespace viviAPI.WebUI7uka.usermodule.Ajax
{

    public class PhoneValid_new : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string msg = string.Empty;

            if (viviapi.BLL.User.Login.CurrentMember == null)
            {
                msg = "登录信息失效，请重新登录";
            }
            else
            {
                string phone = viviLib.XRequest.GetString("phone");

                if (!string.IsNullOrEmpty(phone))
                {
                    if (phone == viviapi.BLL.User.Login.CurrentMember.Tel)
                    {
                        msg = "不能为原手机号码一样";
                    }
                    else if (viviLib.Text.Validate.IsMobileNum(phone))
                    {
                        bool isupdate = (viviapi.BLL.User.Login.CurrentMember.IsPhonePass == 1);

                        string cacheKey = "PHONE_VALID_" + phone;

                        string validcode = (string)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);
                        if (validcode == null)
                        {
                            validcode = new Random().Next(10000, 99999).ToString();
                            viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, validcode);
                        }

                        string smscontext = ""; //string.Format("您的验证码是:{0}[{1}]", validcode, viviapi.BLL.WebInfoFactory.CurrentWebInfo.Name);
                        if (isupdate)
                            smscontext = SMSTempSettings.SMS_Temp_Modify;
                        else
                        {
                            smscontext = SMSTempSettings.SMS_Temp_Authenticate;
                        }

                        smscontext = smscontext.Replace("{@username}", viviapi.BLL.User.Login.CurrentMember.UserName);
                        smscontext = smscontext.Replace("{@sitename}", viviapi.BLL.WebInfoFactory.CurrentWebInfo.Name);
                        smscontext = smscontext.Replace("{@authcode}", validcode);

                        string result = viviapi.BLL.Tools.SMS.SendSmsWithCheck(phone, smscontext, "");
                        if (string.IsNullOrEmpty(result))
                        {
                            result = "true";
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

                context.Response.ContentType = "text/plain";
                context.Response.Write(msg);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}