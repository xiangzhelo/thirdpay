using System;
using viviapi.BLL;
using viviapi.BLL.Finance;
using viviapi.BLL.Settled;
using viviapi.BLL.Sys;
using viviapi.BLL.Withdraw;
using viviapi.Model;
using viviapi.Model.Finance;
using viviapi.Model.Settled;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.Security;
using viviLib.TimeControl;
using Withdraw = viviapi.Model.Finance.Withdraw;
using viviapi.WebComponents;

namespace viviAPI.WebUI7uka.usermodule.settlement
{
    public partial class Applycost : UserPageBase
    {
        private readonly viviapi.BLL.User.SettlementAccount settlAcctBLL = new viviapi.BLL.User.SettlementAccount();
        private ChannelWithdraw chnlBLL = new ChannelWithdraw();

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
                InitForm();
            }
        }


        #region InitForm

        /// <summary>
        /// 
        /// </summary>
        private void InitForm()
        {
            txtUserName.Value = CurrentUser.full_name;

            var acctBLL = new viviapi.BLL.User.SettlementAccount();
            var settlementAccount = acctBLL.GetModel(this.UserId);


            bool checkOk = true;

            if (settlementAccount == null
                || string.IsNullOrEmpty(settlementAccount.PayeeName)
                || string.IsNullOrEmpty(settlementAccount.PayeeBank)
                || string.IsNullOrEmpty(settlementAccount.Account))
            {
                checkOk = false;
                litBankAccout.Text = "请先设置结算账户,<a href=\"/usermodule/account/costinfo.aspx\">点击进入</a>";
            }

            //bool settlAcctsetted = settlAcctBLL.Exists(this.UserId);

            //if (settlAcctsetted == false)
            //{
            //    checkOk = false;
            //    litBankAccout.Text = "请先设置结算账户,<a href=\"/usermodule/account/costinfo.aspx\">点击进入</a>";
            //}
            else
            {
                litBankAccout.Text = UserViewBankAccout;

            }


            if (string.IsNullOrEmpty(CurrentUser.Password2))
            {
                checkOk = false;
                litWithdrawPwd.Text = "您尚未设置提现密码,暂时无法提现。<a href=\"/usermodule/safety/cashpass.aspx\">点击设置</a>";
                txtcashpwd.Visible = false;
            }


            decimal balanceAmt = viviapi.BLL.User.UsersAmt.GetUserAvailableBalance(this.UserId);

            if (balanceAmt <= 0M)
            {
                checkOk = false;
            }

            litAnableAmount.Text = balanceAmt.ToString("f2");
        }

        #endregion

        protected void btnpost_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            WithdrawApply();
        }

        #region WithdrawApply
        /// <summary>
        /// 
        /// </summary>
        private void WithdrawApply()
        {
            string msg = "";
            try
            {
                if (!SettleSettings.OpenWithdraw)
                {
                    msg = SettleSettings.ColseWithdrawReason;
                }
                else
                {
                    //提现金额
                    decimal dwithdrawAmt = 0M, charges = 0M;
                    TocashSchemeInfo scheme = null;

                    string withdrawAmt = txtApplyMoney.Value.ToLower();
                    string safepass = txtcashpwd.Text;

                    if (string.IsNullOrEmpty(withdrawAmt))
                    {
                        msg = "请输入您要提现的金额";
                    }
                    else if (!decimal.TryParse(withdrawAmt, out dwithdrawAmt))
                    {
                        msg = "请输入您正确的金额";
                    }
                    else if (string.IsNullOrEmpty(safepass))
                    {
                        msg = "请输入您的提现密码";
                    }
                    else if (Cryptography.MD5(safepass) != CurrentUser.Password2)
                    {
                        msg = "提现密码不正确";
                    }
                    else
                    {
                        scheme = TocashScheme.GetModelByUser(1, UserId);

                        if (scheme == null)
                        {
                            msg = "未设置提现方案，请联系客服人员!";
                        }
                        else
                        {
                            #region 比较余额
                            //账户可用余额
                            decimal balanceAmt = viviapi.BLL.User.UsersAmt.GetUserAvailableBalance(UserId);

                            if (dwithdrawAmt > balanceAmt)
                            {
                                msg = "余额不足,请修改提现金额";
                            }
                            else if (dwithdrawAmt < scheme.minamtlimitofeach)
                            {
                                msg = "您的提现金额小于最低提现金额限制.";
                            }
                            else if (dwithdrawAmt > scheme.maxamtlimitofeach)
                            {
                                msg = "您的提现金额大于最大提现金额限制.";
                            }
                            else
                            {
                                int todaytimes = viviapi.BLL.Finance.Withdraw.Instance.GetUserDaySettledTimes(UserId,
                                    FormatConvertor.DateTimeToDateString(DateTime.Now));

                                if (todaytimes >= scheme.dailymaxtimes)
                                {
                                    msg = "您今天的提现次数已达到最多限制，请明天再试。";
                                }
                                else
                                {
                                    decimal todayAmt = viviapi.BLL.Finance.Withdraw.Instance.GetUserDaySettledAmt(UserId,
                                        FormatConvertor.DateTimeToDateString(DateTime.Now));

                                    if (todayAmt + dwithdrawAmt >= scheme.dailymaxamt)
                                    {
                                        msg = string.Format("您今天的提现将超过最大限额，你最多还可提现{0:f2}", scheme.dailymaxamt - todayAmt);
                                    }
                                }
                            }

                            if (string.IsNullOrEmpty(msg))
                            {
                                #region 计算手续费
                                charges = scheme.chargerate * dwithdrawAmt;
                                if (scheme.lowerLimit > 0)
                                {
                                    if (charges < scheme.lowerAmt)
                                    {
                                        charges = scheme.lowerAmt;
                                    }
                                }
                                if (scheme.upperLimit > 0)
                                {
                                    if (charges > scheme.upperAmt)
                                    {
                                        charges = scheme.upperAmt;
                                    }
                                }
                                #endregion

                                if (charges >= dwithdrawAmt)
                                {
                                    msg = "余额不足";
                                }
                            }
                            #endregion

                            if (string.IsNullOrEmpty(msg))
                            {
                                #region 保存记录
                                var acctBLL = new viviapi.BLL.User.SettlementAccount();
                                var settlementAccount = acctBLL.GetModel(this.UserId);

                                if (settlementAccount != null)
                                {
                                    var itemInfo = new Withdraw()
                                    {
                                        Tranno = viviapi.BLL.Finance.Withdraw.Instance.GenerateOrderId(),
                                        Addtime = DateTime.Now,
                                        Amount = dwithdrawAmt,
                                        Charges = charges,
                                        Paytime = DateTime.Now,
                                        Status = WithdrawStatus.Auditing,
                                        Tax = 0M,
                                        Userid = UserId,


                                        BankCode = settlementAccount.BankCode,
                                        PayeeBank = settlementAccount.PayeeBank,

                                        ProvinceCode = settlementAccount.ProvinceCode,
                                        BankProvince = settlementAccount.BankProvince,

                                        CityCode = settlementAccount.CityCode,
                                        BankCity = settlementAccount.BankCity,
                                        Payeeaddress = settlementAccount.BankAddress,


                                        PayeeName = settlementAccount.PayeeName,
                                        AccoutType = settlementAccount.AccoutType,
                                        Account = settlementAccount.Account,
                                        Paytype = settlementAccount.Pmode,
                                        Settmode = WithdrawMode.Manual,
                                        Required = DateTime.Now.AddHours(2),
                                        Suppstatus = 0
                                    };

                                    if (DateTime.Now.Hour > 16)
                                    {
                                        itemInfo.Required = DateTime.Now.AddDays(1);
                                    }

                                    if (scheme.vaiInterface > 0)
                                    {
                                        itemInfo.SuppId = chnlBLL.GetSupplier(itemInfo.BankCode);
                                        itemInfo.Suppstatus = 1;
                                    }

                                    int result = viviapi.BLL.Finance.Withdraw.Instance.Apply(itemInfo);
                                    itemInfo.ID = result;
                                    if (result > 0)
                                    {
                                        msg = "提现成功";

                                        #region 通过接口提现
                                        if (itemInfo.Suppstatus == 1
                                            && itemInfo.SuppId > 0
                                            && scheme.tranRequiredAudit == 0)
                                        {
                                            bool audit = viviapi.BLL.Finance.Withdraw.Instance.Audit(itemInfo.Tranno
                                                   , DateTime.Now.ToString("yyyyMMddHHmmssfff")
                                                   , 1
                                                   , "自动确认");

                                            if (audit)
                                            {
                                                viviapi.ETAPI.Common.Withdrawal.InitDistribution(itemInfo);
                                            }
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        msg = "提现失败";
                                    }
                                }
                                else
                                {
                                    msg = "未设置结算账户";
                                }



                                #endregion
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                msg = exception.Message;
            }

            lblMessage.Text = msg;

            string email = System.Web.Configuration.WebConfigurationManager.AppSettings["SysEmail"];
            string useNotice = System.Web.Configuration.WebConfigurationManager.AppSettings["UseEmailNotice"];
            if (useNotice == "1")
            {
                var emailcom = new EmailSender(email
                          , "提现通知"
                          , "ID为" + UserId + "的用户正在申请提现,操作状态：" + msg
                          , true
                          , System.Text.Encoding.GetEncoding("gbk"));
                emailcom.Send();
            }
        }
        #endregion


    }
}
