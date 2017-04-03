using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviLib.IO;

namespace viviapi.WebComponents.Template
{
    public class Helper
    {
        public static string BaseDir
        {
            get
            {
                return HttpContext.Current.Request.PhysicalApplicationPath + @"\Merchant\template\";
            }
        }

        /// <summary>
        /// 取注册模板
        /// </summary>
        /// <returns></returns>
        public static string GetEmailTempCont(string cacheKey, string path)
        {
            try
            {
                string filepath = BaseDir + path;
                string filecontent = string.Empty;

                object cacheObj = viviapi.Cache.DefaultCacheStrategy.GetWebCacheObj.Get(cacheKey);
                if (cacheObj == null)
                {
                    filecontent = File.ReadFile(filepath);

                    viviapi.Cache.DefaultCacheStrategy.GetWebCacheObj.Insert(cacheKey
                        , filecontent
                        , new System.Web.Caching.CacheDependency(filepath));
                }
                else
                {
                    filecontent = cacheObj.ToString();
                }

                return filecontent;
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 邮件验证
        /// </summary>
        /// <returns></returns>
        public static string GetEmailAuthenticateTemp()
        {
            return GetEmailTempCont("template_email_authenticate", "email\\authenticate.txt");
        }


        public static string GetEmailRegisterTemp()
        {
            return GetEmailTempCont("template_email_register", "email\\register.txt");
        }

        public static string GetEmailCheckTemp()
        {
            return GetEmailTempCont("template_email_checkmail", "email\\checkemail.txt");
        }

        public static string GetEmailChangeTemp()
        {
            return GetEmailTempCont("template_email_changemail", "email\\change.txt");
        }
        public static string GetRegCodeTemp()
        {
            return GetEmailTempCont("template_email_changemail", "email\\regcode.txt");
        }
    }
}
