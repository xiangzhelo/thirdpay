using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using viviapi.WebComponents.Web;
using viviLib.ExceptionHandling;
using viviLib.Web;
using Newtonsoft.Json;
using System.Data;

namespace viviAPI.WebUI2015.usermodule.quota
{
    /// <summary>
    /// GetQuotaInfo 的摘要说明
    /// </summary>
    public class GetQuotaInfo : UserHandlerBase
    {

        public override void OnLoad(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msg = "";

            try
            {
                string get=context.Request.Params["get"].ToString();
                if (get == "payrate") {
                    viviapi.Model.Quota.quotapayrate model=new viviapi.Model.Quota.quotapayrate();
                    model.Userid = this.UserId;
                    string ret=viviapi.BLL.Quota.Quotapayrate.Getpayratelist(model);
                    msg =ret;
                }
                if (get == "password2") {
                    if (string.IsNullOrEmpty(CurrentUser.Password2))
                    {

                        msg = "0";
                    }
                    else {
                        msg = "1";
                    }
                }
                if (get == "quotatype_selfisopen") {
                    viviapi.Model.Quota.quotapayrate model = new viviapi.Model.Quota.quotapayrate();
                    model.Userid = this.UserId;
                    model.Quota_type = int.Parse(context.Request.Params["quota_type"].ToString());
                    model.Selfisopen = int.Parse(context.Request.Params["selfisopen"].ToString());
                    int quotatype_selfisopen = viviapi.BLL.Quota.Quotapayrate.update_selfisopen(model);
                    if (quotatype_selfisopen == 0)
                    {
                        msg = "fail";
                    }
                    else{
                        msg = "success";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                msg = "error1";
            }
            context.Response.Write(msg);
        }

        public string Orderid
        {
            get
            {
                return WebBase.GetQueryStringString("order", "");
            }
        
        }
    }
}