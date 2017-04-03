using System;
using System.Globalization;
using viviapi.Model.User;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.agentmodule.settlement
{
    public partial class Accountmoney : AgentPageBase
    {
        private UsersAmtInfo _usersAmt = null;
        public UsersAmtInfo UsersAmt
        {
            get
            {
                if (_usersAmt != null) return _usersAmt;
                _usersAmt = viviapi.BLL.User.UsersAmt.GetModel(UserId);

                return _usersAmt;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                litUserId.Text = UserId.ToString(CultureInfo.InvariantCulture);
                if (UsersAmt != null)
                {
                    decimal balanceAmt = 0M, freezeAmt = 0M, withdrawingAmt = 0M, detentionAmt = 0M;

                    balanceAmt = UsersAmt.Balance;
                    freezeAmt = UsersAmt.Freeze;
                    withdrawingAmt = UsersAmt.Unpayment;

                    detentionAmt = viviapi.BLL.Finance.Trade.GetUserTotalDetentionAmt(this.UserId);

                    balanceAmt = balanceAmt - freezeAmt - withdrawingAmt - detentionAmt;

                    if (balanceAmt < 0M)
                        balanceAmt = 0M;

                    litWithdrawingAmt.Text = withdrawingAmt.ToString("f2");
                    litFreezeAmt.Text = freezeAmt.ToString("f2");
                    litBalance.Text = (balanceAmt).ToString("f2");

                    tr_Unavailable.Visible = (detentionAmt > 0M);
                    litUnavailable.Text = detentionAmt.ToString("f2");
                }
            }
        }
    }
}
