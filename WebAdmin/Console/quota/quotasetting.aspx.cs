using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.quota
{
    public partial class quotasetting : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var get = Request.Params["type"];
            if (get == "quotaTypesetting") {
                bool ret = viviapi.BLL.Quota.Quotatype.settingIsopen(int.Parse(Request.Params["quotaType"].ToString()), int.Parse(Request.Params["isopen"].ToString()));
                if (ret == true)
                {
                    Response.Write("<script type='text/javascript'>alert('修改成功');location.href='quotatype.aspx';</script>");
                    return;
                }
                else {
                    Response.Write("<script type='text/javascript'>alert('修改失败');location.href='quotatype.aspx';</script>");
                    return;
                }
            }
            if (get == "defaultpayrate") {
                bool ret = viviapi.BLL.Quota.Quotatype.settingDefaultPayrate(int.Parse(Request.Params["quotaType"].ToString()), decimal.Parse(Request.Params["payrate"].ToString())/100M);
                if (ret == true)
                {
                    Response.Write("<script type='text/javascript'>alert('修改成功');location.href='quotatype.aspx';</script>");
                    return;
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('修改失败');location.href='quotatype.aspx';</script>");
                    return;
                }
            }
            if (get == "payrate") {
                viviapi.Model.Quota.quotapayrate model =new viviapi.Model.Quota.quotapayrate();
                model.Quota_type = int.Parse(Request.Params["quotaType"].ToString());
                model.Payrate = decimal.Parse(Request.Params["payrate"].ToString()) / 100M;
                model.Userid = int.Parse(Request.Params["userid"].ToString());
                int ret = viviapi.BLL.Quota.Quotapayrate.settingPayrate(model);
                if (ret > 0)
                {
                    Response.Write("<script type='text/javascript'>alert('修改成功');location.href='quotapayrate.aspx?userid=" + model.Userid.ToString() + "';</script>");
                    return;
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('修改失败');location.href='quotapayrate.aspx?userid=" + model.Userid.ToString() + "';</script>");
                    return;
                }
            }

            if (get == "sysisopen")
            {
                viviapi.Model.Quota.quotapayrate model = new viviapi.Model.Quota.quotapayrate();
                model.Quota_type = int.Parse(Request.Params["quotaType"].ToString());
                model.Sysisopen = int.Parse(Request.Params["isopen"].ToString());
                model.Userid = int.Parse(Request.Params["userid"].ToString());
                int ret = viviapi.BLL.Quota.Quotapayrate.settingSysisopen(model);
                if (ret > 0)
                {
                    Response.Write("<script type='text/javascript'>alert('修改成功');location.href='quotapayrate.aspx?userid=" + model.Userid.ToString() + "';</script>");
                    return;
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('修改失败');location.href='quotapayrate.aspx?userid="+ model.Userid.ToString() + "';</script>");
                    return;
                }
            }
        }
    }
}