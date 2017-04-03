using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using viviapi.BLL.Order.Card;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebUI7uka.agentmodule.WS
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class QueryOrders : UserHandlerBase
    {
        public override void OnLoad(HttpContext context)
        {
            string msg = "";
            try
            {
                var listParam = new List<SearchParam>();
                listParam.Add(new SearchParam("userid", CurrentUser.ID));
                listParam.Add(new SearchParam("deduct", 0));

                DataTable orders = Factory.Instance.PageSearch(listParam, 8, 1, string.Empty, false).Tables[1];

                var html = new System.Text.StringBuilder();
                if (orders != null)
                {
                    foreach (DataRow dr in orders.Rows)
                    {
                        html.AppendFormat(@"<tr>
				<td height=""30"" align=""center"" bgcolor=""#FFFFFF"">{0}</td>
				<td height=""30"" align=""center"" bgcolor=""#FFFFFF"">{1:0.00}</td>
				<td id=""paymoney{6}"" height=""30"" align=""center"" bgcolor=""#FFFFFF"">{2:0.00}</td>
				<td id=""orderzt{6}"" height=""30"" align=""center"" bgcolor=""#FFFFFF"">{3}</td>
				<td id=""errorMsg{6}"" height=""30"" align=""center"" bgcolor=""#FFFFFF"">{4}</td>
				<td height=""30"" align=""center"" bgcolor=""#FFFFFF""> {5}</td>
				<td height=""30"" align=""center"" bgcolor=""#FFFFFF""><button class=""button_01"" id=""sub{6}"" style=""margin-right:0"" type=""button"" onClick=""checkflag('{6}')"">刷新</button></td>
			</tr>", dr["cardno"]
                  , dr["refervalue"]
                  , Factory.Instance.GetViewSuccessAmt(dr["status"], dr["realvalue"])
                  , Factory.Instance.GetViewStatusName(dr["status"])
                  , dr["msg"]
                  , dr["addtime"]
                  , dr["ID"]
                  );
                    }
                }

                msg = html.ToString();

            }
            catch(Exception exception)
            {
                msg = exception.Message;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
        }
    }
}
