using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.SessionState;
using viviLib;
using viviapi.Model;
using viviapi.BLL;
using viviapi.Model.User;
using viviLib.Security;

using viviapi.Model.Order;
using viviLib.Web;
using DBAccess;

namespace viviAPI.WebUI7uka.usermodule.Ajax
{

    public class ordersearch_new : IHttpHandler, IReadOnlySessionState
    {
        public UsersAmtInfo _currentUserAmt = null;
        public UsersAmtInfo currentUserAmt
        {
            get
            {
                if (_currentUserAmt == null && viviapi.BLL.User.Login.CurrentMember != null)
                {
                    _currentUserAmt = viviapi.BLL.User.UsersAmt.GetModel(viviapi.BLL.User.Login.CurrentMember.ID);
                }
                return _currentUserAmt;
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            string msg = "";
            if (viviapi.BLL.User.Login.CurrentMember == null)
            {
                msg = "登录信息失效，请重新登录";
            }
            else
            {
                List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();
                listParam.Add(new viviLib.Data.SearchParam("userid", viviapi.BLL.User.Login.CurrentMember.ID));


                DataSet pageData = viviapi.BLL.Order.Card.Factory.Instance.PageSearch(listParam, 8, 1, string.Empty,false);
                DataTable orders = pageData.Tables[1];

                System.Text.StringBuilder html = new System.Text.StringBuilder();
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
                  , GetViewSuccessAmt(dr["status"], dr["realvalue"])
                  , GetViewStatusName(dr["status"])
                  , dr["msg"]
                  , dr["addtime"]
                  , dr["ID"]
                  );
                    }
                }

                msg = html.ToString();
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetViewStatusName(object status)
        {
            if (status == DBNull.Value)
                return string.Empty;
            //viviapi.Model.Order.OrderStatusEnum _stat = (viviapi.Model.Order.OrderStatusEnum)Convert.ToInt32(status);
            if (Convert.ToInt32(status) == 8)
                return "失败";
            else
                return Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum), status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetViewSuccessAmt(object status, object amt)
        {
            if (status == DBNull.Value || amt == DBNull.Value)
                return "0";

            //viviapi.Model.Order.OrderStatusEnum _stat = (viviapi.Model.Order.OrderStatusEnum)Convert.ToInt32(status);
            if (Convert.ToInt32(status) == 2)
                return decimal.Round(Convert.ToDecimal(amt), 2).ToString();
            else
                return "0";
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