using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.Model.Finance;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.ExceptionHandling;
using viviLib.TimeControl;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Withdraw
{
    /// <summary>
    /// 处理支付
    /// </summary>
    public partial class Pay : ManagePageBase
    {
        protected viviapi.BLL.User.SettlementAccount SettlementAccountBLL = new viviapi.BLL.User.SettlementAccount();

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string TranNo
        {
            get
            {
                return WebBase.GetQueryStringString("tranNo", "");
            }
        }

        private viviapi.Model.Finance.Withdraw _itemInfo = null;
        public viviapi.Model.Finance.Withdraw ItemInfo
        {
            get
            {
                if (_itemInfo == null)
                {
                    if (!string.IsNullOrEmpty(TranNo))
                    {
                        _itemInfo = viviapi.BLL.Finance.Withdraw.Instance.GetModel(TranNo);
                    }
                    else
                    {
                        _itemInfo = new viviapi.Model.Finance.Withdraw();
                    }
                }
                return _itemInfo;
            }
        }
        private UserInfo _userInfo = null;
        public UserInfo UserInfo
        {
            get
            {
                if (_userInfo == null && ItemInfo != null)
                {
                    _userInfo = viviapi.BLL.User.Factory.GetModel(ItemInfo.Userid);
                }
                return _userInfo;
            }
        }
        public string action
        {
            get
            {
                string _string = WebBase.GetQueryStringString("action", "");
                if (_string == "")
                    return "pay";
                return _string;
            }
        }
        #endregion

        private SettlementAccount _model = null;
        public SettlementAccount MSettlementAccount
        {
            get
            {
                if (_model == null && UserInfo != null)
                {
                    _model = SettlementAccountBLL.GetModel(UserInfo.ID);
                }
                return _model;
            }
        }

        private UsersAmtInfo _usersAmt = null;
        public UsersAmtInfo UsersAmt
        {
            get
            {
                if (UserInfo != null)
                {
                    if (_usersAmt != null) return _usersAmt;
                    _usersAmt = viviapi.BLL.User.UsersAmt.GetModel(UserInfo.ID);
                }

                return _usersAmt;
            }
        }

        #region Page_Load
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

                InitForm();


            }
        }
        #endregion

        #region InitForm
        /// <summary>
        /// 
        /// </summary>
        private void InitForm()
        {
            hfaction.Value = action;

            if (UserInfo != null)
            {
                #region 用户基本信息
                this.lblUserId.Text = UserInfo.ID.ToString();
                this.lblUserName.Text = UserInfo.UserName;
                this.lblUserStatus.Text = Enum.GetName(typeof(UserStatusEnum), UserInfo.Status);
                this.lblFullName.Text = UserInfo.full_name;
                #endregion

                if (UsersAmt != null)
                {
                    #region 用户余额
                    decimal balanceAmt = 0M, freezeAmt = 0M, withdrawingAmt = 0M, detentionAmt = 0M;

                    balanceAmt = UsersAmt.Balance;
                    freezeAmt = UsersAmt.Freeze;
                    withdrawingAmt = UsersAmt.Unpayment;

                    detentionAmt = viviapi.BLL.Finance.Trade.GetUserTotalDetentionAmt(UserInfo.ID);

                    balanceAmt = balanceAmt - freezeAmt - withdrawingAmt - detentionAmt;

                    if (balanceAmt < 0M)
                        balanceAmt = 0M;

                    lblTotalBalance.Text = (UsersAmt.Balance).ToString("f2");
                    lblWithdrawingAmt.Text = withdrawingAmt.ToString("f2");
                    lblFreezeAmt.Text = freezeAmt.ToString("f2");
                    lblBalance.Text = (balanceAmt).ToString("f2");

                    lblDeposit.Text = detentionAmt.ToString("f2");
                    #endregion
                }

                if (MSettlementAccount != null)
                {
                    #region 结算账号
                    lblStlAcctAccoutType.Text = viviapi.BLL.User.SettlementAccount.GetAccoutTypeName(MSettlementAccount.AccoutType);
                    lblStlAcctPayee.Text = MSettlementAccount.PayeeName;
                    lblStlAcctBank.Text = MSettlementAccount.PayeeBank;

                    if (MSettlementAccount.Pmode == 1)
                    {
                        lblSetAcctBankAddress.Text = MSettlementAccount.BankProvince + MSettlementAccount.BankCity + " " + MSettlementAccount.BankAddress;
                    }
                    this.lblStlAcctBankAccout.Text = MSettlementAccount.Account;
                    #endregion
                }
            }

            if (ItemInfo.ID > 0)
            {
                lblTranno.Text = ItemInfo.Tranno;
                this.lblsettleAmt.Text = ItemInfo.Amount.ToString("f2");
                this.lblAddTime.Text = FormatConvertor.DateTimeToTimeString(ItemInfo.Addtime);

                lblPayeeName.Text = ItemInfo.PayeeName;
                lblBank.Text = ItemInfo.PayeeBank;

                lblPayeeaddress.Text = ItemInfo.BankProvince + ItemInfo.BankCity + " " + ItemInfo.Payeeaddress;

                lblAccount.Text = ItemInfo.Account;

                ChargesBox.Text = ItemInfo.Charges.ToString("f2");


                if (ItemInfo.Status != WithdrawStatus.Paying)
                {
                    this.TaxBox.Enabled = false;
                    this.TaxBox.ReadOnly = true;
                    this.ChargesBox.Enabled = false;
                    this.ChargesBox.ReadOnly = true;
                    this.btnSure.Text = "已支付";
                    this.btnSure.Enabled = false;
                }
                if (action == "pay")
                {
                    btnSure.Visible = true;
                    btnSave.Visible = false;
                }
                else if (action == "modi")
                {
                    btnSure.Visible = false;
                    btnSave.Visible = true;
                }
            }
        }
        #endregion

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Financial);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        #region DoPay
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string DoPay()
        {
            try
            {
                decimal tax = decimal.Parse(this.TaxBox.Text.Trim());
                decimal charges = decimal.Parse(this.ChargesBox.Text.Trim());
                ItemInfo.Paytime = DateTime.Now;
                ItemInfo.Tax = tax;
                ItemInfo.Charges = charges;

                bool result = viviapi.BLL.Finance.Withdraw.Instance.Complete(ItemInfo);

                if (result == true)
                {
                    if (!string.IsNullOrEmpty(UserInfo.Tel))
                    {
                        #region  设置短信发送信息
                        CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
                        //ip格式如下，不带https://
                        //app.cloopen.com:8883
                        bool isInit = api.init("app.cloopen.com", "8883");
                        api.setAccount("8a48b5515018a0f4015045e342b14990", "07c2d4f927a1443fb4ffffe158ee39b8");
                        api.setAppId("8a216da8567745c001568c78ae030d62");
                        #endregion

                        string[] data = { UserInfo.full_name, ItemInfo.Amount.ToString("f2") };

                        Dictionary<string, object> retData = api.SendTemplateSMS(UserInfo.Tel, "108968", data);
                        //短信发送失败
                        if (retData["statusCode"].ToString() != "000000")
                        {
                            return "操作成功,通知短信发送失败.statusCode:"+ retData["statusCode"]+ ",statusMsg:" + retData["statusMsg"];
                        }
                    }
                    
                    return "操作成功";
                }
                else 
                {
                    return "操作失败";
                }
                //else if (result == 0)
                //{
                //    #region send sms
                //    string smscontext = viviapi.BLL.SysConfig.sms_temp_tocash;

                //    if (!string.IsNullOrEmpty(smscontext) && !string.IsNullOrEmpty(UserInfo.Tel))
                //    {
                //        smscontext = smscontext.Replace("{@username}", UserInfo.UserName);
                //        smscontext = smscontext.Replace("{@settledmoney}", ItemInfo.amount.ToString("f2"));

                //        viviapi.BLL.Tools.SMS.Send(UserInfo.Tel, smscontext, "");
                //    }
                //    #endregion

                //    if (ItemInfo.suppid > 0)
                //    {
                //        //ETAPI.Common.Withdraw.InitDistribution(ItemInfo);
                //    }

                //    return "";
                //}

                //return "err";
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return ex.Message;
            }

        }
        #endregion

        #region btnSave_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                decimal tax = decimal.Parse(this.TaxBox.Text.Trim());
                decimal charges = decimal.Parse(this.ChargesBox.Text.Trim());
                ItemInfo.Tax = tax;
                ItemInfo.Charges = charges;

                if (viviapi.BLL.Finance.Withdraw.Instance.Update(ItemInfo))
                {
                    AlertAndRedirect("修改成功", "Pays.aspx");
                }
                else
                {
                    ShowMessageBox("修改失败");
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
        #endregion



        #region btnSure_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            string message = DoPay();

            AlertAndRedirect(message, "Pays.aspx");
        }
        #endregion
    }
}