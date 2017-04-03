using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using viviapi.BLL.Sys;
using DBAccess;
using System.Data;

namespace viviAPI.WebUI2015.usermodule.WS
{
    /// <summary>
    /// reg 的摘要说明
    /// </summary>
    public class reg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            try
            {
                string email = context.Request.QueryString["email"];
                string code = context.Request.QueryString["code"];
                string cacheKey = string.Format(Constant.EmailRegCodeCacheKey, email);
                string validcode = (string)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);
                string msg = "验证码不正确";
                if (code.ToUpper() == validcode.ToUpper())
                {
                    viviapi.Model.User.UserInfo user = new viviapi.Model.User.UserInfo();
                    user.Email = email;
                    user.IsEmailPass = 0;
                    user.isagentDistribution = 0;
                    DataTable table = DataBase.ExecuteDataset(CommandType.Text, "select * from emailCode where email='" + email + "'").Tables[0];
                    int id = -1;
                    if (table.Rows.Count > 0)
                    {
                        id = Convert.ToInt32(table.Rows[0]["ID"].ToString());
                    }
                    else
                    {
                        id = DataBase.ExecuteNonQuery(CommandType.Text, string.Format("insert into emailCode (email,count) values ('{0}',1);SELECT @@IDENTITY", email));
                    }

                    msg = id.ToString();
                }

                context.Response.Write(msg);
            }
            catch
            {
                context.Response.Write("-1");
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