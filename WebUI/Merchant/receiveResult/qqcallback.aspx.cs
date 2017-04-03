using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviapi.WebComponents.QqConnetSDK;
using viviLib.Web;


namespace viviAPI.WebUI7uka.Merchant.receiveResult
{
    public partial class Qqcallback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Req();
        }

        private void Req()
        {
            var qc = new QQConnet();
            if (!qc.VerifyCallback())
            {
                ShowErrMsg("参数缺少.");
                return;
            }
            Session["QzoneOauth"] = null;

            //数据令牌
            string accessToken = qc.GetAccess_Token();
            if (string.IsNullOrEmpty(accessToken))
            {
                ShowErrMsg("Access_Token 为空");
                return;
            }

            //唯一标识 Openid与QQ号一对一对应
            string openid = qc.GetOpenid(accessToken);
            if (string.IsNullOrEmpty(openid))
            {
                ShowErrMsg("Openid 为空");
                return;
            }

            //获取当前登录用户
            QQUserInfo user = qc.GetUserInfo(accessToken, openid);

            if (user != null)
            {
                int userid = viviapi.BLL.User.UserLoginByPartner.Instance.Exists(1, openid);
                if (userid == 0)
                {
                    #region 
                    var info = new UserLoginByPartner
                    {
                        id = 0,
                        available = 1,
                        openid = openid,
                        plant = 1,
                        plantname = "QQ平台",
                        userid = 0
                    };

                    bool init = viviapi.BLL.User.UserLoginByPartner.Instance.Insert(info) >0 ;

                    if (init)
                    {
                        Session["QzoneOauth"] = user;

                        Response.Redirect("/appqqset.aspx?openid=" + openid, false);
                    }
                    else
                    {
                        ShowErrMsg("init.err");
                    }
                    #endregion
                }
                else
                {
                    string lastLoginIp = ServerVariables.TrueIP;
                    string lastLoginAddress = WebUtility.GetIPAddress(lastLoginIp);

                    string message = viviapi.BLL.User.Login.SignIn(1, openid, lastLoginIp, lastLoginAddress);

                    if (message == "success")
                    {
                        Response.Redirect("/usermodule/account/index.aspx", false);
                    }
                    else
                    {
                        ShowErrMsg(message);
                    }
                }

            }
            else
            {
                ShowErrMsg("登录失败 2.");
            }

        }

        public void ShowErrMsg(string ErrMsg)
        {
            HttpContext.Current.Response.Write(ErrMsg);
            HttpContext.Current.Response.End();
        }
    }
}
