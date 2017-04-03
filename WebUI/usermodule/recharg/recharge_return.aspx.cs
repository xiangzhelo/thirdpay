using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.WebUI7uka.usermodule.recharg
{
    public partial class recharge_return : System.Web.UI.Page
    {
        protected string successAmt = "0.00";
        protected void Page_Load(object sender, EventArgs e)
        {
            bool success = false;

            String orderid = Request["orderid"];//返回订单号
            var bll = new viviapi.BLL.APP.Recharge();
            viviapi.Model.APP.Recharge model = bll.GetModel(orderid);

            //  viviapi.Model.User.UserInfo _systemUser = viviapi.BLL.User.Factory.GetModel(800);
            viviapi.Model.User.UserInfo _systemUser = viviapi.BLL.User.Factory.GetModel(model.userid);
            String key = _systemUser.APIKey;
            //返回参数
      
            String opstate = Request["opstate"];//返回处理结果
            String ovalue = Request["ovalue"];//返回实际充值金额
            String sign = Request["sign"];//返回签名
            String sysorderid = Request["sysorderid"];
            String systime = Request["systime"];
            String attach = Request["attach"];
            String msg = Request["msg"];

            String param = String.Format("orderid={0}&opstate={1}&ovalue={2}{3}", orderid, opstate, ovalue, key);

            //比对签名是否有效
            if (sign == viviLib.Security.Cryptography.MD5(param))
            {
                int status = 0;
                successAmt = ovalue;
                //执行操作方法
                if (opstate.Equals("0") || opstate.Equals("-3"))
                {
                    success = true;
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
            }
            else
            {
            }

            if (success)
            {
                //更新充值订单金额
                //var bll = new viviapi.BLL.APP.Recharge();
                //viviapi.Model.APP.Recharge model = bll.GetModel(orderid);
                   model.status = 1;
                bll.Update(model);
            }
            else
            {

            }
        }
    }
}
