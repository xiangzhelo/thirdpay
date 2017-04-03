using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace viviAPI.WebUI7uka.Merchant.receiveResult
{
    public partial class RechargeReturn : System.Web.UI.Page
    {
        protected String message = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            String internalAPIID = viviapi.SysConfig.RuntimeSetting.InternalAPIID;
            String internalAPIKey = viviapi.SysConfig.RuntimeSetting.InternalAPIKey;

            if (string.IsNullOrEmpty(internalAPIID) ||
                    string.IsNullOrEmpty(internalAPIKey)
                    )
            {
                Response.Write("error");
                Response.End();
            }

            //返回参数
            String orderid = Request["orderid"];//返回订单号
            String opstate = Request["opstate"];//返回处理结果
            String ovalue = Request["ovalue"];//返回实际充值金额
            String sign = Request["sign"];//返回签名
            String sysorderid = Request["sysorderid"];
            String systime = Request["systime"];
            String attach = Request["attach"];
            String msg = Request["msg"];

            String param = String.Format("orderid={0}&opstate={1}&ovalue={2}{3}", orderid, opstate, ovalue, internalAPIKey);

            //比对签名是否有效
            if (sign == viviLib.Security.Cryptography.MD5(param))
            {
                int status = 0;

                decimal successAmt = decimal.Parse(ovalue);

                //执行操作方法
                if (opstate.Equals("0") || opstate.Equals("-3"))
                {
                    

                    status = 2;

                    var model = viviapi.BLL.APP.Recharge.Instance.GetModel(orderid);
                    if (model != null)
                    {
                        successAmt = successAmt * viviapi.BLL.Finance.PayRate.Instance.GetUserPayRate(model.userid, model.paytype);

                        int result = viviapi.BLL.APP.Recharge.Instance.Complete(orderid, sysorderid, successAmt, status);

                        message =
                               string.Format(
                                   "恭喜您，成功充值<span style=\"color: #85b919;\">{0}</span> 元！所得金额<em style=\"font-size: 22px;\">{1}</em>元</span> "
                                   , ovalue, successAmt);
                    }
                }
                else if (opstate.Equals("-1"))
                {
                    //卡号密码错误
                }
                else if (opstate.Equals("-2"))
                {
                    //卡实际面值和提交时面值不符，卡内实际面值未使用
                }
                else if (opstate.Equals("-4"))
                {
                    //卡在提交之前已经被使用
                }
                else if (opstate.Equals("-5"))
                {
                    //失败，原因请查看msg
                }

                Response.Write("opstate=0");
                Response.End();
            }
            else
            {
                //签名无效
                Response.Write("sign error");
            }
        }
    }
}
