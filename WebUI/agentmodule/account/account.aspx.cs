using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL;
using System.Data;
using viviapi.BLL.Finance;
using viviapi.BLL.News;
using viviapi.BLL.User;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebUI7uka.agentmodule.account
{
    public partial class Account : AgentPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        private void InitForm()
        {
            litUserEmail.Text = UserViewEmail;

            if (CurrentUser.IsEmailPass == 1)
            {
                a_verify.InnerText = "[已认证]";
                a_verify.Attributes["href"] = "javascript:void(0)";
            }

            DataSet ds = viviapi.BLL.Order.OrderIncome.Instance.TodayIncomeStat(this.UserId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                if (row["bankcount"] != DBNull.Value)
                    ordercount.InnerText = row["bankcount"].ToString();

                if (row["bankamt"] != DBNull.Value)
                    totalmoney.InnerText = string.Format("{0:f2}", row["bankamt"]);

                if (row["cardcount"] != DBNull.Value)
                    succordercount.InnerText = row["cardcount"].ToString();

                if (row["cardamt"] != DBNull.Value)
                    succtotalmoney.InnerText = string.Format("{0:f2}", row["cardamt"]);

            }

            var info = UserLogin.Instance.GetModel(this.UserId);
            if (info != null)
            {
                if (info.lastLoginTime != null)
                    lblgetlastm.InnerText = info.lastLoginTime.Value.ToString("yyyy-MM-dd HH:mm");
                location.InnerText = info.lastLoginIp;
            }
            decimal balanceAmt = 0M, unableAmt = 0M;
            var usersAmt = viviapi.BLL.User.UsersAmt.GetModel(this.UserId);
            if (usersAmt != null)
            {
                balanceAmt = usersAmt.Balance;

                decimal freezeAmt = usersAmt.Freeze;
                decimal withdrawingAmt = usersAmt.Unpayment;
                decimal detentionAmt = Trade.GetUserTotalDetentionAmt(UserId);

                balanceAmt = balanceAmt - freezeAmt - withdrawingAmt - detentionAmt;

                if (balanceAmt < 0M)
                    balanceAmt = 0M;

                unableAmt = freezeAmt + withdrawingAmt + detentionAmt;
                if (unableAmt > balanceAmt)
                    unableAmt = balanceAmt;

                if (unableAmt > 0M)
                {
                    litOtherAmt.Text = string.Format("(暂不可用余额{0:f2})", unableAmt);
                }
            }
            // decimal balanceAmt = viviapi.BLL.User.UsersAmt.GetUserAvailableBalance(this.UserId);
            lblgetmoney.InnerText = balanceAmt.ToString("f2");


            ds = viviapi.BLL.Order.OrderIncome.Instance.TodayIncomeStatGroupByPayType(this.UserId);

            rptdata.DataSource = ds;
            rptdata.DataBind();

            DataTable news = NewsFactory.GetNewsList(5, 1, 0);
            if (news != null && news.Rows.Count > 0)
            {
                lit_news_title.Text = news.Rows[0]["newstitle"].ToString();
                lit_news_publish_time.Text = news.Rows[0]["addTime"].ToString();
                lit_news_content.Text = news.Rows[0]["newscontent"].ToString();
            }
        }
    }
}
