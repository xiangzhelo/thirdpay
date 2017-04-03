using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using viviapi.Model;
using viviLib.TimeControl;
using System.Collections.Generic;
using viviLib;
using viviapi.BLL;

namespace viviapi.web.Manage
{
    public partial class Index : viviapi.WebComponents.Web.ManagePageBase
    {
        protected string loginip;
        protected string logintime;
        protected string username;
        protected int uncheckeduserCount;
        protected int orderCount;
        protected string webdomain;
        protected string paydomain;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                try
                {
                    loginip = currentManage.lastLoginIp;
                    logintime = viviLib.TimeControl.FormatConvertor.DateTimeToTimeString(currentManage.lastLoginTime.Value);
                    username = currentManage.username;
                    //paysouid.InnerText = "欢迎使用第三方支付平台";
                    //未审核商户数量
                    List<int> list = viviapi.BLL.User.Factory.GetUsers(" status=1");
                    uncheckeduserCount = list.Count;

                    DataSet ds = viviapi.BLL.Order.OrderIncome.Instance.TodayIncomeStat(-1);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = ds.Tables[0].Rows[0];
                        int bankcount = 0;
                        int cardcount = 0;
                        if (row["bankcount"] != DBNull.Value)
                            bankcount = Convert.ToInt32(row["bankcount"].ToString());
                        if (row["cardcount"] != DBNull.Value)
                            cardcount = Convert.ToInt32(row["cardcount"].ToString());
                        orderCount = bankcount + cardcount;
                    }
                    WebInfo webInfo = WebInfoFactory.GetWebInfoByDomain(XRequest.GetHost());
                    if (webInfo != null)
                    {
                        webdomain = webInfo.Domain;
                        paydomain = webInfo.PayUrl;
                    }
                    //DataSet ds = viviapi.BLL.Order.OrderIncome.Instance.
                    //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    DataRow row = ds.Tables[0].Rows[0];
                    //    int bankcount = 0;
                    //    int cardcount = 0;
                    //    if (row["bankcount"] != DBNull.Value)
                    //    {
                    //        //ordercount.InnerText = row["bankcount"].ToString();
                    //        bankcount = Convert.ToInt32(row["bankcount"]);
                    //    }
                    //    if (row["bankamt"] != DBNull.Value)
                    //        //totalmoney.InnerText = string.Format("{0:f2}", row["bankamt"]);

                    //        if (row["cardcount"] != DBNull.Value)
                    //        {
                    //            //succordercount.InnerText = row["cardcount"].ToString();
                    //            cardcount = Convert.ToInt32(row["cardcount"]);
                    //        }
                    //    if (row["cardamt"] != DBNull.Value)
                    //        //succtotalmoney.InnerText = string.Format("{0:f2}", row["cardamt"]);
                    //        Span1.InnerText = (bankcount + cardcount).ToString();
                    //}
                }
                catch { }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            //ManageRole role = ManageRole.Administrator | ManageRole.Financial | ManageRole.UnionAdmin | ManageRole.SuperAdmin | ManageRole.CustomerService; 

            //if ((currentManage.role & role) == currentManage.role)
            //{
            //    return;
            //}
            //else
            //{
            //    Response.End();
            //}
        }

    }
}