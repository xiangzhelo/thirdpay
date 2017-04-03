using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviLib.Security;
using viviLib.Web;

namespace viviAPI.WebUI7uka.Merchant
{
    public partial class SelfService : System.Web.UI.Page
    {
        public string Parms
        {
            get
            {
                return WebBase.GetQueryStringString("parms", "");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = "";
            try
            {
                string decodeparams = Cryptography.RijndaelDecrypt(Parms);
                string[] arr = decodeparams.Split('&');
                if (arr.Length == 2)
                {
                    int itemId = 0;

                    if (int.TryParse(arr[0].Split('=')[1], out itemId))
                    {
                        var bll = new EmailCheck();
                        EmailCheckInfo itemInfo = bll.GetModel(itemId);
                        if (
                            itemInfo == null ||
                            itemInfo.status != EmailCheckStatus.提交中 ||
                            itemInfo.Expired < DateTime.Now
                            )
                        {
                            msg = "无效的信息或此链接已使用";
                        }
                        else
                        {
                            itemInfo.checktime = DateTime.Now;
                            itemInfo.status = EmailCheckStatus.已审核;

                            msg = "操作";
                            if (itemInfo.typeid == EmailCheckType.认证)
                                msg = "绑定邮箱";
                            else if (itemInfo.typeid == EmailCheckType.修改)
                                msg = "修改邮箱";
                            else if (itemInfo.typeid == EmailCheckType.注册)
                                msg = "激活邮箱";

                            if (bll.Update(itemInfo))
                            {
                                msg += "成功";
                            }
                            else
                            {
                                msg += "失败";
                            }
                        }
                    }
                    else
                    {
                        msg = "无效参数";
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
", AntiXss.JavaScriptEncode(msg));

            HttpContext.Current.Response.Write(script);
        }
    }
}
