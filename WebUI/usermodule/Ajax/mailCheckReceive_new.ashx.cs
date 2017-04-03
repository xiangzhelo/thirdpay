using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace viviAPI.WebUI7uka.usermodule.Ajax
{

    public class mailCheckReceive_new : IHttpHandler
    {

        public string parms
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("parms", "");
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string msg = "";
            try
            {
                string decodeparams = viviLib.Security.Cryptography.RijndaelDecrypt(parms);
                string[] arr = decodeparams.Split('&');
                if (arr.Length == 2)
                {
                    int _itemId = 0;
                    if (int.TryParse(arr[0].Split('=')[1], out _itemId))
                    {
                        viviapi.BLL.User.EmailCheck bll = new viviapi.BLL.User.EmailCheck();
                        viviapi.Model.User.EmailCheckInfo itemInfo = bll.GetModel(_itemId);
                        if (itemInfo == null || itemInfo.status != viviapi.Model.User.EmailCheckStatus.提交中)
                        {
                            msg = "无效的信息或此链接已使用";
                        }
                        else
                        {
                            itemInfo.checktime = DateTime.Now;
                            itemInfo.status = viviapi.Model.User.EmailCheckStatus.已审核;
                            if (bll.Update(itemInfo))
                            {
                                msg = "验证成功";
                            }
                        }

                    }
                }
            }
            catch
            {
                msg = "提交无效的参数";
            }
            string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
alert({0});
location.href='/index.aspx';
//--></SCRIPT>
",
                   viviLib.Security.AntiXss.JavaScriptEncode(msg));

            HttpContext.Current.Response.Write(script);
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
