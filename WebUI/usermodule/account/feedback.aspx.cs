using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Communication;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.usermodule.account
{
    public partial class Feedback : UserPageBase
    {
        readonly feedback _bll = new feedback();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        void InitForm()
        {
        }

        protected void b_save_Click(object sender, EventArgs e)
        {
            string typeid = ddltypeid.Value;
            string title = txttitle.Value;
            string content = txtcontent.Value;

            int tempid = 0;
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                AlertAndRedirect("请输入您的建议或意见");
                return;
            }
            int.TryParse(typeid, out tempid);
            var feedback = new viviapi.Model.feedbackInfo
            {
                addtime = DateTime.Now,
                cont = content,
                reply = string.Empty,
                replyer = 0,
                replytime = DateTime.Now,
                status = viviapi.Model.feedbackstatus.等待回复,
                title = title,
                userid = UserId,
                typeid = (viviapi.Model.feedbacktype) tempid,
                clientip = viviLib.Web.ServerVariables.TrueIP
            };
           
            if (_bll.Add(feedback) > 0)
            {
                //AlertAndRedirect("发送成功");
                string script = "<SCRIPT LANGUAGE='javascript'>alert('发送成功');window.parent.location.href='feedbacks.aspx';</SCRIPT>";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "AlertAndRedirect", script);
            }
            else
            {
                AlertAndRedirect("发送失败");
            }
        }
        protected void Page_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is HttpRequestValidationException)
            {
                Response.Write("<script language=javascript>alert('字符串含有非法字符！')</script>");
                Server.ClearError(); // 如果不ClearError()这个异常会继续传到Application_Error()
                Response.Write("<script language=javascript>window.location.href='feedback.aspx';</script>");
            }
        }
    }
}
