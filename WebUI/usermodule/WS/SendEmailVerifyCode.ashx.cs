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
using viviapi.WebComponents.Template;
using viviapi.WebComponents;
using System.Web.SessionState;

namespace viviAPI.WebUI7uka.usermodule.WS
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SendEmailVerifyCode : IHttpHandler, IRequiresSessionState
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
                string email = GetValue("phone");
                int emailExists = viviapi.BLL.User.Factory.EmailExists(email);

                if (!string.IsNullOrEmpty(email))
                {
                    if (Validate.IsEmail(email))
                    {
                        if (emailExists == 999)
                        {
                            string cacheKey = string.Format(Constant.EmailRegCodeCacheKey, email);

                            string validcode = (string)WebCache.GetCacheService().RetrieveObject(cacheKey);
                            if (validcode == null)
                            {

                                //validcode = "1000".ToString(CultureInfo.InvariantCulture);
                                //WebCache.GetCacheService().AddObject(cacheKey, validcode);
                                //context.Response.ContentType = "text/plain";
                                //context.Response.Write("true");

                                validcode = new Random().Next(10000, 99999).ToString(CultureInfo.InvariantCulture);
                                WebCache.GetCacheService().AddObject(cacheKey, validcode);
                            }
                            string smscontext = Helper.GetRegCodeTemp();
                            if (!string.IsNullOrEmpty(smscontext))
                            {
                                smscontext = smscontext.Replace("{#regcode#}", validcode);
                                smscontext = smscontext.Replace("{#sitename#}",
                                    viviapi.BLL.WebInfoFactory.CurrentWebInfo.Name);
                                smscontext = smscontext.Replace("{#sitename#}", viviapi.BLL.WebInfoFactory.CurrentWebInfo.Domain);
                            }

                            var emailcom = new EmailHelper(email
                           , email + "邮箱验证"
                           , smscontext
                           , true
                           , System.Text.Encoding.Default);
                            bool result = emailcom.Send();
                            if (result)
                            {
                                msg = "true";
                            }
                            else
                            {
                                msg = "邮件发送失败";
                            }
                        }

                        else if (emailExists == 1)
                        {
                            msg = "该邮箱正在审核";
                        }
                        else if (emailExists == 2)
                        {
                            msg = "该邮箱已注册";
                        }
                        else if (emailExists == 3)
                        {
                            msg = "该邮箱已被锁定";
                        }
                        else if (emailExists == 4)
                        {
                            msg = "您的邮箱审核失败";
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
