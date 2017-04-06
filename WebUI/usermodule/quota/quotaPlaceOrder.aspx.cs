using DBAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents.Web;
using viviLib.Security;

namespace viviAPI.WebUI2015.usermodule.quota
{
    public partial class quotaPlaceOrder : UserPageBase { 

        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = "";
            try
            {
                var get = Request.Form;
                int quota_type = int.Parse(get["quota_type"].ToString());
                string password = get["password"].ToString();
                decimal quotaValue;
                if (string.IsNullOrEmpty(get["quotaValue"].ToString()))
                {
                    msg = "请输入您要转换的额度";
                }
                else if (!decimal.TryParse(get["quotaValue"].ToString(), out quotaValue))
                {
                    msg = "请输入您正确的额度";
                }
                else if (string.IsNullOrEmpty(password))
                {
                    msg = "请输入您的提现密码";
                }
                else if (Cryptography.MD5(password) != CurrentUser.Password2)
                {
                    msg = "提现密码不正确";
                }
                else
                {
                    decimal balanceAmt = viviapi.BLL.User.UsersAmt.GetUserAvailableBalance(UserId);
                    var strSql = new StringBuilder();
                    strSql.Append("select a.payrate from quotapayrate a left join quotatype b on a.quota_type=b.quota_type where a.sysisopen=1 and a.selfisopen=1 and a.payrate>0 and b.isopen=1 and a.userid="+this.UserId+" and b.quota_type="+quota_type);
                    object obj = DbHelperSQL.GetSingle(strSql.ToString());
                    if (obj == null)
                    {
                        msg = "该类型未开放";
                    }
                    else
                    {
                        decimal payrate= decimal.Parse(obj.ToString());
                        decimal money = payrate * quotaValue;
                        if (balanceAmt < money)
                        {
                            msg = "余额不足,请修改额度";
                        }
                        else {
                            string orderid=new Random().Next(100, 999).ToString(CultureInfo.InvariantCulture) + DateTime.Now.ToString("yyyyMMddHHmmss");
                            viviapi.Model.Quota.quotaOrder model = new viviapi.Model.Quota.quotaOrder();
                            model.addtime = DateTime.Now;
                            model.updatetime = DateTime.Now;
                            model.year = DateTime.Now.Year;
                            model.month = DateTime.Now.Month;
                            model.userid = this.UserId;
                            model.quota_type = quota_type;
                            model.quotaValue = quotaValue;
                            model.orderid = orderid;
                            model.clientip = Page.Request.UserHostAddress;
                            int ret=viviapi.BLL.Quota.quotaOrder.Placeorder(model);
                            if (ret == 0) {
                                msg = "额度转换失败，请联系开发人员";
                            }
                            else {
                                msg = "转换额度成功";
                                Response.Write("<script type='text/javascript'>alert('" + msg + "');window.location.href='/usermodule/quota/quotaorder.aspx';</script>");
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                msg = exception.Message;
            }
            Response.Write("<script type='text/javascript'>alert('"+msg+"');window.history.go(-1);</script>");
            return;
        }
        protected void quotaSettlement(string orderid) {

        }
    }
}