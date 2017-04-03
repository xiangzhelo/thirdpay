using System;
using viviapi.BLL.Finance;
using viviapi.Model.Finance;
using viviapi.WebComponents.Web;
using viviLib.Security;

namespace viviAPI.WebUI7uka.agentmodule.settlement
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Transfer : AgentPageBase
    {
        protected viviapi.BLL.User.UserSetting setbll = new viviapi.BLL.User.UserSetting();
        protected viviapi.BLL.Finance.Transfer tranBLL = new viviapi.BLL.Finance.Transfer();
        protected TransferScheme schemeBLL = new TransferScheme();

        protected Transferscheme _scheme = null;
        protected Transferscheme scheme
        {
            get
            {
                if (_scheme == null)
                {
                    _scheme = schemeBLL.GetModel(1);
                }
                return _scheme;
            }
        }

        private viviapi.Model.User.UserSettingInfo _setting = null;
        public viviapi.Model.User.UserSettingInfo setting
        {
            get
            {
                _setting = setbll.GetModel(this.UserId);
                if (_setting == null)
                    _setting = new viviapi.Model.User.UserSettingInfo();

                return _setting;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        void InitForm()
        {
            bool checkOk = false;

            if (string.IsNullOrEmpty(CurrentUser.Password2))
            {
                callinfo.InnerHtml = "您尚未设置提现密码,暂时无法提现。<a href=\"/usermodule/safety/cashpass.aspx\" target=\"mainframe\">点击设置</a>";
                this.btnSave.Enabled = false;
            }
            else if (setting == null || setting.istransfer == 0)
            {
                this.btnSave.Enabled = false;
                this.callinfo.InnerText = "你暂未开通转账权限。";
            }
            else if (scheme == null)
            {
                this.btnSave.Enabled = false;
                this.callinfo.InnerText = "规则未设置，请联系管理员。";
            }
            else
            {
                checkOk = true;
            }
            decimal enableAmount = viviapi.BLL.User.UsersAmt.GetUserAvailableBalance(this.UserId);
            litenableAmount.Text = enableAmount.ToString("f2");
            btnSave.Enabled = checkOk && enableAmount > 0;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            string safepass = this.txttocashpwd.Text;
            if (string.IsNullOrEmpty(safepass))
            {
                msg = "请输入您的提现密码";
            }
            else if (Cryptography.MD5(safepass) != CurrentUser.Password2)
            {
                msg = "提现密码不正确";
            }
            else if (setting == null || setting.istransfer == 0)
            {
                msg = "你暂未开通转账权限";
            }
            else if (scheme == null)
            {
                msg = "规则未设置，请联系管理员。";
            }
            else
            {
                string amtstr = txtTransferMoney.Text;
                decimal amt = 0M;
                int touser = 0;
                decimal _amt = 0M;
                if (!int.TryParse(touserid.Value, out touser))
                {
                    msg = "请输入付款账号.";
                }
                if (touser == this.UserId)
                {
                    msg = "";
                }
                else if (!viviapi.BLL.User.Factory.Exists(touser))
                {
                    msg = "此账号不存在。";
                }
                else if (string.IsNullOrEmpty(amtstr))
                {
                    msg = "请输入付款金额。";
                }
                else if (!decimal.TryParse(amtstr.Replace(",", ""), out amt))
                {
                    msg = "请输入正确的付款金额。";
                }
                else
                {
                    decimal enableAmount = viviapi.BLL.User.UsersAmt.GetUserAvailableBalance(this.UserId);

                    decimal monthAmt = schemeBLL.GetUserMonthTotalAmt(this.UserId);
                    //免费流量每个月
                    decimal freeAmt = scheme.monthmaxamt - monthAmt;
                    decimal startAmt = amt;

                    if (freeAmt > 0M)
                    {
                        startAmt = amt - freeAmt;
                    }
                    decimal charges = 0M;
                    if (startAmt > 0M)
                    {
                        charges = scheme.chargerate * startAmt / 100M;
                        if (charges < scheme.chargeleastofeach)
                        {
                            charges = scheme.chargeleastofeach;
                        }
                        else if (charges > scheme.chargemostofeach)
                        {
                            charges = scheme.chargemostofeach;
                        }
                    }

                    if (amt + charges > enableAmount)
                    {
                        msg = "余额不足。";
                    }
                    else
                    {
                        var info = new viviapi.Model.Finance.Transfer
                        {
                            addtime = DateTime.Now,
                            amt = amt,
                            charge = charges,
                            month = DateTime.Now.Month,
                            year = DateTime.Now.Year,
                            userid = CurrentUser.ID,
                            touserid = touser,
                            updatetime = DateTime.Now,
                            remark = this.txtremark.Text,
                            status = 2
                        };

                        if (tranBLL.Add(info) > 0)
                        {
                            msg = "转账成功";
                        }
                        else
                        {
                            msg = "转账失败。";
                        }
                    }
                }
            }

            this.callinfo.InnerText = msg;
        }


    }
}
