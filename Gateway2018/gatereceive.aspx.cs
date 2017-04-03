using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Security;
using System.Configuration;

namespace viviAPI.Gateway2018
{
    public partial class gatereceive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String key = ConfigurationManager.AppSettings["userkey"];//配置文件密钥
            //返回参数
            String orderid = Request["orderid"];//返回订单号
            String opstate = Request["opstate"];//返回处理结果
            String ovalue = Request["ovalue"];//返回实际充值金额
            String sign = Request["sign"];//返回签名
            String ekaorderID = Request["ekaorderid"];//乐收卡录入时产生流水号。
            String ekatime = Request["ekatime"];//乐收卡处理时间。
            String attach = Request["attach"];//上行附加信息
            String msg = Request["msg"];//乐收卡返回订单处理消息

            String param = String.Format("orderid={0}&opstate={1}&ovalue={2}{3}", orderid, opstate, ovalue, key);//组织参数
            //比对签名是否有效
            if (sign.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(param, "MD5").ToLower()))
            {
                //执行操作方法
                if (opstate.Equals("0") || opstate.Equals("-3"))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("支付成功！");
                    builder.Append("<br>");
                    builder.Append("订单号：" + orderid);
                    builder.Append("<br>");
                    builder.Append("充值金额" + ovalue);
                    //builder.Append("<br>");
                    //builder.Append("处理时间"+msg);
                    //builder.Append("<br>");
                    //操作流程成功的情况
                    Label1.Text = builder.ToString();

                }
                else if (opstate.Equals("-1"))
                {
                    //卡号密码错误
                    Label1.Text = "卡号密码错误";
                }
                else if (opstate.Equals("-2"))
                {

                    Label1.Text = "卡实际面值和提交时面值不符，卡内实际面值未使用";
                }
                else if (opstate.Equals("-4"))
                {
                    //
                    Label1.Text = "卡在提交之前已经被使用";
                }
                else if (opstate.Equals("-5"))
                {
                    //失败，原因请查看msg
                    Label1.Text = "失败，原因：" + msg;
                }
            }
            else
            {
                Label1.Text = "签名无效";
                //签名无效
            }


        }
    }
}