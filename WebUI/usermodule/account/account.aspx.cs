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

namespace viviAPI.WebUI7uka.usermodule.account
{
    public partial class Account2 : UserPageBase
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
            decimal quota_all = 0;
            litUserEmail.Text = UserViewEmail;

            if (CurrentUser.IsEmailPass == 1)
            {
                a_verify.InnerText = "[已认证]";
                a_verify.Attributes["href"] = "javascript:void(0)";
            }

            DataSet ds_ag = viviapi.BLL.Quota.Quotaquery.Quotaqueryin.GetquotabyUserid(this.UserId.ToString(), 1);
            if (ds_ag != null && ds_ag.Tables.Count > 0 && ds_ag.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds_ag.Tables[0].Rows[0];
                decimal balance = 0;
                if (row["quota_balance"] != DBNull.Value)
                {

                    foreach (DataRow dr in ds_ag.Tables[0].Rows)
                    {
                        //获取行中某个字段（列）的数据
                        balance += decimal.Parse(dr["quota_balance"].ToString());
                    }
                    quota_all += balance;
                    type_AG.InnerText = balance.ToString();

                }
            }



            DataSet ds_bbin = viviapi.BLL.Quota.Quotaquery.Quotaqueryin.GetquotabyUserid(this.UserId.ToString(), 2);
            if (ds_bbin != null && ds_bbin.Tables.Count > 0 && ds_bbin.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds_bbin.Tables[0].Rows[0];
                decimal balance = 0;
                if (row["quota_balance"] != DBNull.Value)
                {

                    foreach (DataRow dr in ds_bbin.Tables[0].Rows)
                    {
                        //获取行中某个字段（列）的数据
                        balance += decimal.Parse(dr["quota_balance"].ToString());
                    }
                    quota_all += balance;

                    type_BBIN.InnerText = balance.ToString();

                }
            }


            DataSet ds_MG = viviapi.BLL.Quota.Quotaquery.Quotaqueryin.GetquotabyUserid(this.UserId.ToString(), 3);
            if (ds_MG != null && ds_MG.Tables.Count > 0 && ds_MG.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds_MG.Tables[0].Rows[0];
                decimal balance = 0;
                if (row["quota_balance"] != DBNull.Value)
                {

                    foreach (DataRow dr in ds_MG.Tables[0].Rows)
                    {

                        balance += decimal.Parse(dr["quota_balance"].ToString());
                    }
                    quota_all += balance;

                    type_MG.InnerText = balance.ToString();

                }
            }

            DataSet ds_OG = viviapi.BLL.Quota.Quotaquery.Quotaqueryin.GetquotabyUserid(this.UserId.ToString(), 4);
            if (ds_OG != null && ds_OG.Tables.Count > 0 && ds_OG.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds_OG.Tables[0].Rows[0];
                decimal balance = 0;
                if (row["quota_balance"] != DBNull.Value)
                {

                    foreach (DataRow dr in ds_OG.Tables[0].Rows)
                    {

                        balance += decimal.Parse(dr["quota_balance"].ToString());
                    }
                    quota_all += balance;
                    type_OG.InnerText = balance.ToString();

                }
            }


            DataSet ds_HG = viviapi.BLL.Quota.Quotaquery.Quotaqueryin.GetquotabyUserid(this.UserId.ToString(), 5);
            if (ds_HG != null && ds_HG.Tables.Count > 0 && ds_HG.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds_HG.Tables[0].Rows[0];
                decimal balance = 0;
                if (row["quota_balance"] != DBNull.Value)
                {

                    foreach (DataRow dr in ds_HG.Tables[0].Rows)
                    {

                        balance += decimal.Parse(dr["quota_balance"].ToString());
                    }
                    quota_all += balance;
                    type_HG.InnerText = balance.ToString();

                }
            }



            DataSet ds_PT = viviapi.BLL.Quota.Quotaquery.Quotaqueryin.GetquotabyUserid(this.UserId.ToString(), 6);
            if (ds_PT != null && ds_PT.Tables.Count > 0 && ds_PT.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds_PT.Tables[0].Rows[0];
                decimal balance = 0;
                if (row["quota_balance"] != DBNull.Value)
                {

                    foreach (DataRow dr in ds_PT.Tables[0].Rows)
                    {

                        balance += decimal.Parse(dr["quota_balance"].ToString());
                    }
                    quota_all += balance;
                    type_PT.InnerText = balance.ToString();

                }
            }

            DataSet ds_EBET = viviapi.BLL.Quota.Quotaquery.Quotaqueryin.GetquotabyUserid(this.UserId.ToString(), 7);
            if (ds_EBET != null && ds_EBET.Tables.Count > 0 && ds_EBET.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds_EBET.Tables[0].Rows[0];
                decimal balance = 0;
                if (row["quota_balance"] != DBNull.Value)
                {

                    foreach (DataRow dr in ds_EBET.Tables[0].Rows)
                    {

                        balance += decimal.Parse(dr["quota_balance"].ToString());
                    }
                    quota_all += balance;
                    type_EBET.InnerText = balance.ToString();

                }
            }
            txtquota_all.InnerText = quota_all.ToString();
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
                    // litOtherAmt.Text = string.Format("(暂不可用余额{0:f2})", unableAmt);
                }

                litWithdrawingAmt1.InnerText = withdrawingAmt.ToString("f2");
                litFreezeAmt1.InnerText = freezeAmt.ToString("f2");

                Unavailable.Visible = (detentionAmt > 0M);
                litUnavailable1.InnerText = detentionAmt.ToString("f2");
            }
            // decimal balanceAmt = viviapi.BLL.User.UsersAmt.GetUserAvailableBalance(this.UserId);
            lblgetmoney.InnerText = balanceAmt.ToString("f2");


            DataSet ds = viviapi.BLL.Order.OrderIncome.Instance.TodayIncomeStatGroupByPayType(this.UserId);

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
