using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Finance;
using viviapi.BLL.User;
using viviapi.Model.Finance;
using viviapi.WebComponents.Web;
using viviLib;
using viviapi.Model;
using viviapi.Model.User;
using viviLib.Text;
using viviapi.BLL.basedata;
using System.Data;




namespace viviAPI.WebUI7uka.agentmodule.account
{
    public partial class Costinfo : AgentPageBase
    {
        protected viviapi.BLL.User.SettlementAccount PbankBLL = new viviapi.BLL.User.SettlementAccount();

        protected string PayeeBank = string.Empty;
        protected string PayeeName = string.Empty;
        protected string BankAddress = string.Empty;
        protected string FullName = string.Empty;

        private viviapi.Model.User.SettlementAccount _model = null;
        public viviapi.Model.User.SettlementAccount Model
        {
            get
            {
                if (_model == null && CurrentUser != null)
                {
                    _model = PbankBLL.GetModel(CurrentUser.ID);
                }
                return _model;
            }
        }

        TocashSchemeInfo _scheme = null;
        /// <summary>
        /// 
        /// </summary>
        protected TocashSchemeInfo Scheme
        {
            get
            {
                if (_scheme == null)
                    _scheme = TocashScheme.GetModelByUser(1, this.UserId);
                return _scheme;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();

                if (viviapi.BLL.User.SettlementAccountApply.Exists3(this.UserId))
                {
                    callinfo.InnerText = "您提交的资料还未审核，请联系商务审核";
                    this.btnSave.Enabled = false;
                }
            }
        }

        void InitForm()
        {
            tr_oldcard.Visible = false;

            DataSet ds = base_province.GetList("");
            ddlprovince.Items.Clear();
            ddlprovince.Items.Add(new ListItem("--省份--", ""));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ddlprovince.Items.Add(new ListItem(row["ProvinceName"].ToString(), row["ProvinceID"].ToString()));
            }

            if (CurrentUser != null)
            {
                litbankdetentiondays.Text = Scheme.bankdetentiondays.ToString();
                litcarddetentiondays.Text = Scheme.carddetentiondays.ToString();
                litotherdetentiondays.Text = Scheme.otherdetentiondays.ToString();

                lit_username.Text = CurrentUser.full_name;
                if (Model != null && !string.IsNullOrEmpty(Model.Account))
                {
                    if (!string.IsNullOrEmpty(Model.Account))
                        tr_oldcard.Visible = true;

                    rb_bank.Checked = Model.Pmode == 1;
                    rb_alipay.Checked = Model.Pmode == 2;
                    rb_tenpay.Checked = Model.Pmode == 3;

                    rb_accoutType0.Checked = Model.AccoutType == 0;
                    rb_accoutType1.Checked = Model.AccoutType == 1;

                    ddlprovince.SelectedValue = Model.ProvinceCode;
                    LoadCity(Model.CityCode);

                    ddlbankName.Value = Model.BankCode;
                    txtbankAddress.Value = Model.BankAddress;

                    litpmode.Text = Model.PmodeName;
                    litPayeeBank.Text = Model.PayeeBank;
                    litUserViewBankAccout.Text = UserViewBankAccout;
                    litPayeeName.Text = Model.PayeeName;
                    litBankAddress.Text = Model.BankAddress;
                    if (Model.Pmode == 1)
                    {
                        litProvince.Text = string.Format("{0}{1}    ", Model.BankProvince, Model.BankCity);
                    }
                }
            }
            controls();
        }

        void controls()
        {
            tr_bankselect.Visible = rb_bank.Checked;
            tr_province.Visible = rb_bank.Checked;
            tr_address.Visible = rb_bank.Checked;
            tr_accoutType.Visible = rb_bank.Checked;
            yhkh1.Visible = rb_bank.Checked;
            yhkh2.Visible = rb_alipay.Checked;
            yhkh3.Visible = rb_tenpay.Checked;
            qryhkh1.Visible = rb_bank.Checked;
            qryhkh2.Visible = rb_alipay.Checked;
            qryhkh3.Visible = rb_tenpay.Checked;
            if (Model.PayeeBank.Equals("0002"))
            {
                yyhkh1.Visible = false;
                yyhkh2.Visible = true;
                yyhkh3.Visible = false;

                oyhkh1.Visible = false;
                oyhkh2.Visible = true;
                oyhkh3.Visible = false;

                khzh.Visible = false;
            }
            else if (Model.PayeeBank.Equals("0003"))
            {
                yyhkh1.Visible = false;
                yyhkh2.Visible = false;
                yyhkh3.Visible = true;

                oyhkh1.Visible = false;
                oyhkh2.Visible = false;
                oyhkh3.Visible = true;

                khzh.Visible = false;
            }
            else
            {
                yyhkh1.Visible = true;
                yyhkh2.Visible = false;
                yyhkh3.Visible = false;

                oyhkh1.Visible = true;
                oyhkh2.Visible = false;
                oyhkh3.Visible = false;

                khzh.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            int pmode = 1;
            if (rb_alipay.Checked)
                pmode = 2;
            else if (rb_tenpay.Checked)
                pmode = 3;

            string oldaccount = this.txtoldaccount.Value.Trim();
            string bankname = this.ddlbankName.Items[ddlbankName.SelectedIndex].Text;
            string bankAddress = this.txtbankAddress.Value;
            string account = txtaccount.Value;
            string reaccount = txtreaccount.Value;
            if (Model != null)
            {
                if (!string.IsNullOrEmpty(Model.Account))
                {
                    if (string.IsNullOrEmpty(oldaccount))
                    {
                        msg = "原银行卡号不能为空";
                    }
                    else if (oldaccount != Model.Account)
                    {
                        msg = "原银行卡号不正确";
                    }
                    else if (this.txtoldaccount.Value != Model.Account)
                    {
                        msg = "原银行卡号不正确";
                    }
                }
            }


            if (pmode == 1)
            {
                if (string.IsNullOrEmpty(this.ddlprovince.SelectedValue))
                {
                    msg = "请选择省份";
                }
                else if (string.IsNullOrEmpty(this.ddlcity.SelectedValue))
                {
                    msg = "请选择城市";
                }
                else if (string.IsNullOrEmpty(bankname))
                {
                    msg = "开户银行不能为空";
                }
                else if (string.IsNullOrEmpty(bankAddress))
                {
                    msg = "支行名称不能为空";
                }
            }
            

            if (string.IsNullOrEmpty(account))
            {
                msg = "个人银行帐号不能为空";
            }
            if (account != reaccount)
            {
                msg = "个人银行帐号与确认个人银行帐号必须一致";
            }
            if (string.IsNullOrEmpty(msg))
            {
                int accoutType = 0;
                if (rb_accoutType1.Checked)
                    accoutType = 1;
               

                var appmodel = new UserPayBankAppInfo
                {
                    userid = CurrentUser.ID,
                    accoutType = accoutType,
                    pmode = pmode,
                    account = account,
                    payeeName = this.CurrentUser.full_name,
                    payeeBank = string.Empty,
                    bankProvince = string.Empty,
                    bankCity = string.Empty,
                    bankAddress = string.Empty,
                    BankCode = string.Empty,
                    provinceCode = string.Empty,
                    cityCode = string.Empty
                };

                if (pmode == 1)
                {
                    string bankCode = ddlbankName.Items[ddlbankName.SelectedIndex].Value;
                    if (!string.IsNullOrEmpty(bankCode))
                    {
                        appmodel.BankCode = ddlbankName.Items[ddlbankName.SelectedIndex].Value;
                        appmodel.payeeBank = ddlbankName.Items[ddlbankName.SelectedIndex].Text;
                    }

                    string bankProvince = ddlprovince.Items[ddlprovince.SelectedIndex].Text;
                    if (!string.IsNullOrEmpty(bankProvince))
                    {
                        appmodel.bankProvince = bankProvince;
                        appmodel.provinceCode = ddlprovince.Items[ddlprovince.SelectedIndex].Value;
                    }

                    string bankCity = ddlcity.Items[ddlcity.SelectedIndex].Text;
                    if (!string.IsNullOrEmpty(bankCity))
                    {
                        appmodel.bankCity = ddlcity.Items[ddlcity.SelectedIndex].Text;
                        appmodel.cityCode = ddlcity.Items[ddlcity.SelectedIndex].Value;
                    }
                    appmodel.bankAddress = bankAddress;
                }
                else if (pmode == 2)
                {
                    appmodel.BankCode = "0002";
                    appmodel.payeeBank = "支付宝";
                }
                else if (pmode == 3)
                {
                    appmodel.BankCode = "0003";
                    appmodel.payeeBank = "财付通";
                }

                appmodel.status = AcctChangeEnum.待审核;
                appmodel.AddTime = DateTime.Now;
                appmodel.SureTime = DateTime.Now;
                appmodel.SureUser = 0;


                if (viviapi.BLL.User.SettlementAccountApply.Insert(appmodel) > 0)
                {
                    this.btnSave.Enabled = false;
                    msg = "结算账户添加成功，请联系商务审核！";
                }
                else
                {
                    msg = "操作失败";
                }
            }

            this.callinfo.InnerText = msg;
            string script = "<script language='javascript'>alert('" + msg + "');</script>";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "costinfo", script);
        }

        protected void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCity(string.Empty);
        }

        void LoadCity(string inivalue)
        {
            string province = ddlprovince.SelectedValue;
            if (!string.IsNullOrEmpty(province))
            {
                DataSet ds = base_city.GetList("ProvinceID=" + province);
                ddlcity.Items.Clear();
                ddlcity.Items.Add(new ListItem("--市区--", ""));
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ListItem i = new ListItem(row["CityName"].ToString(), row["CityID"].ToString());
                    if (inivalue == row["CityID"].ToString())
                        i.Selected = true;
                    ddlcity.Items.Add(i);
                }
            }
        }

        protected void rb_tenpay_CheckedChanged(object sender, EventArgs e)
        {
            controls();
        }

        protected void rb_bank_CheckedChanged(object sender, EventArgs e)
        {
            controls();
        }

        protected void rb_alipay_CheckedChanged(object sender, EventArgs e)
        {
            controls();
        }


    }
}