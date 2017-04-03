using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.Finance;
using viviapi.BLL.User;
using viviapi.Model;
using viviapi.Model.Promotion;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserEdits : ManagePageBase
    {
        protected viviapi.BLL.User.SettlementAccount pbankBLL = new viviapi.BLL.User.SettlementAccount();
        protected viviapi.BLL.User.UserSetting setbll = new UserSetting();
        viviapi.BLL.User.UserLoginByPartner loginByPartnerbll = new viviapi.BLL.User.UserLoginByPartner();

        /// <summary>
        /// 
        /// </summary>
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public viviapi.Model.User.SettlementAccount _settleaccoutmodel = null;
        public viviapi.Model.User.SettlementAccount settleaccoutmodel
        {
            get
            {
                if (_settleaccoutmodel == null && ItemInfoId > 0)
                {
                    _settleaccoutmodel = pbankBLL.GetModel(ItemInfoId);
                }
                return _settleaccoutmodel;
            }
        }

        private viviapi.Model.User.UserSettingInfo _setting = null;
        public viviapi.Model.User.UserSettingInfo setting
        {
            get
            {
                if (_setting == null)
                {
                    _setting = setbll.GetModel(ItemInfoId);
                }
                if (_setting == null)
                {
                    _setting = new UserSettingInfo();
                    _setting.userid = ItemInfoId;

                }
                return _setting;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public bool isUpdate
        {
            get
            {
                return ItemInfoId > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public UserInfo _ItemInfo = null;
        public UserInfo model
        {
            get
            {
                if (_ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        _ItemInfo = Factory.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        _ItemInfo = new UserInfo();
                    }
                }
                return _ItemInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                InitForm();
                ShowInfo();

                //rbluserType.Style.Add("display","none");
            }
        }

        #region InitForm
        void InitForm()
        {
            DataSet ds = viviapi.BLL.basedata.base_province.GetList("");
            ddlprovince.Items.Clear();
            ddlprovince.Items.Add(new ListItem("--省份--", ""));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ddlprovince.Items.Add(new ListItem(row["ProvinceName"].ToString(), row["ProvinceID"].ToString()));
            }

            if (isSuperAdmin == false)
            {
                if (model.manageId != this.currentManage.id)
                {
                    Response.Write("Sorry,No authority!");
                    Response.End();
                }
            }

            foreach (int item in Enum.GetValues(typeof(UserStatusEnum)))
            {
                this.ddlStatus.Items.Add(new ListItem(Enum.GetName(typeof(UserStatusEnum), item), item.ToString()));
            }

            DataTable levData = viviapi.BLL.User.UserLevel.Instance.GetAllList().Tables[0];
            ddlUserLevel.Items.Add("--商户等级--");
            foreach (DataRow row in levData.Rows)
            {
                ddlUserLevel.Items.Add(new ListItem(row["levName"].ToString(), row["level"].ToString()));
            }

            ddlmange.Items.Add(new ListItem("--请选择管理员--", ""));
            levData = viviapi.BLL.ManageFactory.GetList(" status =1").Tables[0];
            foreach (DataRow dr in levData.Rows)
            {
                ddlmange.Items.Add(new ListItem(dr["username"].ToString(), dr["id"].ToString()));
            }

            ddlagents.Items.Add(new ListItem("--请选择代理员--", ""));
            levData = viviapi.BLL.User.Factory.GetAgentList();
            foreach (DataRow dr in levData.Rows)
            {
                ddlagents.Items.Add(new ListItem(dr["username"].ToString(), dr["id"].ToString()));
            }

            ddlTocashScheme.Items.Add(new ListItem("--默认--", ""));
            levData = TocashScheme.GetList("type=1").Tables[0];
            foreach (DataRow dr in levData.Rows)
            {
                ddlTocashScheme.Items.Add(new ListItem(dr["schemename"].ToString(), dr["id"].ToString()));
            }

            ddlagentDistscheme.Items.Add(new ListItem("--默认--", ""));
            levData = TocashScheme.GetList("type=2").Tables[0];
            foreach (DataRow dr in levData.Rows)
            {
                ddlagentDistscheme.Items.Add(new ListItem(dr["schemename"].ToString(), dr["id"].ToString()));
            }

            if (ItemInfoId > 0)
            {
                if (model.parter > 0)
                {
                    btnUnbind.Visible = (model.parter == 1);
                    txtqq.Attributes["readonly"] = "true";
                }
            }
        }
        #endregion

        #region ShowInfo
        /// <summary>
        /// 
        /// </summary>
        void ShowInfo()
        {
            if (isUpdate && model != null)
            {
                UserInfo promSuper = Factory.GetPromSuperior(model.ID);
                if (promSuper != null && promSuper.ID > 0)
                {
                    ddlagents.SelectedValue = promSuper.ID.ToString();
                }

                rbuserclass.SelectedValue = model.classid.ToString();
                this.lblid.Text = model.ID.ToString();
                this.txtuserName.Text = model.UserName;
                txtfullname.Text = model.full_name;
                this.txtCPSDrate.Text = model.CPSDrate.ToString();
                this.txtCVSNrate.Text = model.CVSNrate.ToString();
                this.txtemail.Text = model.Email;
                this.txtqq.Text = model.QQ;
                this.txttel.Text = model.Tel;
                this.txtidCard.Text = model.IdCard;
                this.rblsettlemode.SelectedValue = model.PMode.ToString();
                this.ddlTocashScheme.SelectedValue = model.MaxDayToCashTimes.ToString();
                this.ddlagentDistscheme.SelectedValue = model.agentDistscheme.ToString();

                this.ddlStatus.SelectedValue = model.Status.ToString();

                this.lblregTime.Text = model.RegTime.ToString("yyyy-MM-dd HH:mm");
                //this.txtcompany.Text = model.company;
                //this.txtlinkMan.Text = model.linkMan;
                this.lblbalance.Text = model.Balance.ToString("0.00");
                //this.txtpayment.Text = model.payment.ToString();
                //this.txtunpayment.Text = model.unpayment.ToString();
                //this.txtagentId.Text = model.agentId.ToString();
                this.txtsiteName.Text = model.SiteName;
                this.txtsiteUrl.Text = model.SiteUrl;
                this.rbluserType.SelectedValue = ((int)model.UserType).ToString();
                txtapiAcct.Text = model.APIAccount.ToString();

                ddlUserLevel.SelectedValue = ((int)model.UserLevel).ToString();

                //this.lblIntegral.Text = model.Integral.ToString();

                int maxdaytocashTimes = 0;
                if (!string.IsNullOrEmpty(ddlTocashScheme.SelectedValue))
                {
                    maxdaytocashTimes = int.Parse(this.ddlTocashScheme.SelectedValue);
                }

                //this.txtapiaccount.Text = model.apiaccount;
                this.txtapikey.Text = model.APIKey;
                this.lbllastLoginIp.Text = model.LastLoginIp;
                this.lbllastLoginTime.Text = model.LastLoginTime.ToString("yyyy-MM-dd HH:mm");
                this.txtdesc.Text = model.Desc;
                this.ddlmange.SelectedValue = model.manageId.ToString();
                txtquestion.Text = model.question;
                txtanswer.Text = model.answer;
                cb_isRealNamePass.Checked = model.IsRealNamePass == 1;
                cb_isEmailPass.Checked = model.IsEmailPass == 1;
                cb_isPhonePass.Checked = model.IsPhonePass == 1;
                cb_isagentDistribution.Checked = model.isagentDistribution == 1;

                ckb_rw_bank.Checked = setting.RiskWarning == 1;
                ckb_rw_alipay.Checked = setting.AlipayRiskWarning == 1;
                ckb_rw_alicode.Checked = setting.AliCodeRiskWarning == 1;
                ckb_rw_wxpay.Checked = setting.WxPayRiskWarning == 1;

                txtUrlNoRefPayUrl.Text = viviapi.BLL.WebInfoFactory.CurrentWebInfo.PayUrl + string.Format("links/pay.aspx?u={0}&k={1}", model.ID, viviLib.Security.Cryptography.MD5(model.ID.ToString() + viviapi.BLL.Sys.Constant.ParameterEncryptionKey));
                txtsmsNotifyUrl.Text = model.smsNotifyUrl;
                //this.txtupdatetime.Text = model.updatetime.ToString();

                rbl_settledmode.SelectedValue = model.Settles.ToString();
                cb_isdebug.Checked = model.isdebug == 1;

                ddlpayeeBank.SelectedValue = model.BankCode;

                if (settleaccoutmodel != null)
                {
                    rblsettlemode.SelectedValue = this.settleaccoutmodel.Pmode.ToString();

                    rblaccoutType.SelectedValue = this.settleaccoutmodel.AccoutType.ToString();

                    ddlprovince.SelectedValue = settleaccoutmodel.ProvinceCode;
                    LoadCity(settleaccoutmodel.CityCode);

                    this.txtaccount.Text = model.Account;
                    this.txtpayeeName.Text = model.PayeeName;
                    //this.txtpayeeBank.Text = model.PayeeBank;
                    //this.txtbankProvince.Text = model.BankProvince;
                    //this.txtbankCity.Text = model.BankCity;
                    this.txtbankAddress.Text = model.BankAddress;

                }

                this.cb_istransfer.Checked = setting.istransfer == 1 ? true : false;
                ddlcardversion.SelectedValue = model.cardversion.ToString();

                txtProvince.Text = model.province;
                txtCity.Text = model.city;
            }
        }
        void LoadCity(string inivalue)
        {
            string province = ddlprovince.SelectedValue;
            if (!string.IsNullOrEmpty(province))
            {
                DataSet ds = viviapi.BLL.basedata.base_city.GetList("ProvinceID=" + province);
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
        #endregion

        #region Save
        void Save()
        {
            var updateList = new List<UsersUpdateLog>();

            string userName = this.txtuserName.Text;
            string password = this.txtpassword.Text;
            int CPSDrate = 0;
            int.TryParse(this.txtCPSDrate.Text, out CPSDrate);
            int CVSNrate = 0;
            int.TryParse(this.txtCVSNrate.Text, out CVSNrate);
            string email = this.txtemail.Text;
            string qq = this.txtqq.Text;
            string tel = this.txttel.Text;
            string idCard = this.txtidCard.Text;
            int pmode = 0;
            int.TryParse(this.rblsettlemode.SelectedValue, out pmode);

            string payeeName = this.txtpayeeName.Text;

            int status = int.Parse(ddlStatus.SelectedValue);
            model.classid = int.Parse(rbuserclass.SelectedValue);
            //string company = this.txtcompany.Text;
            //string linkMan = this.txtlinkMan.Text;
            string account = this.txtaccount.Text;
            string siteName = this.txtsiteName.Text;
            string siteUrl = this.txtsiteUrl.Text;
            UserTypeEnum userType = (UserTypeEnum)int.Parse(rbluserType.SelectedValue);
            int userLevel = 0;
            userLevel = int.Parse(this.ddlUserLevel.SelectedValue);

            //int maxdaytocashTimes = int.Parse(this.txtmaxdaytocashTimes.Text);
            string apikey = this.txtapikey.Text;
            //if (isUpdate && userName != model.UserName)
            //{

            //}
            model.UserName = userName;
            model.APIAccount = int.Parse(this.txtapiAcct.Text);
            model.Settles = Convert.ToInt32(rbl_settledmode.SelectedValue);
            model.smsNotifyUrl = this.txtsmsNotifyUrl.Text;
            //model.classid

            if (!string.IsNullOrEmpty(password))
            {
                password = viviLib.Security.Cryptography.MD5(password);
                if (isUpdate && password != model.Password)
                {
                    updateList.Add(NewUpdateLog("password", password, model.Password));
                }
                model.Password = password;
            }
            if (!string.IsNullOrEmpty(this.txtpassword2.Text.Trim()))
            {
                string password2 = viviLib.Security.Cryptography.MD5(this.txtpassword2.Text.Trim());
                //if (isUpdate && password != model.Password)
                //{
                //    updateList.Add(newUpdateLog("password", password, model.Password));
                //}
                model.Password2 = password2;
            }
            if (isUpdate && CPSDrate != model.CPSDrate)
            {
                updateList.Add(NewUpdateLog("CPSDrate", CPSDrate.ToString(), model.CPSDrate.ToString()));
            }
            model.CPSDrate = CPSDrate;

            if (isUpdate && CVSNrate != model.CVSNrate)
            {
                updateList.Add(NewUpdateLog("CVSNrate", CVSNrate.ToString(), model.CVSNrate.ToString()));
            }
            model.CVSNrate = CVSNrate;

            if (isUpdate && email != model.Email)
            {
                updateList.Add(NewUpdateLog("Email", email, model.Email));
            }
            model.Email = email;

            if (isUpdate && qq != model.QQ)
            {
                updateList.Add(NewUpdateLog("QQ", qq, model.QQ));
            }
            model.QQ = qq;

            if (isUpdate && tel != model.Tel)
            {
                updateList.Add(NewUpdateLog("tel", tel, model.Tel));
            }
            model.Tel = tel;

            if (isUpdate && idCard != model.IdCard)
            {
                updateList.Add(NewUpdateLog("idCard", idCard, model.IdCard));
            }
            model.IdCard = idCard;

            //收款
            if (isUpdate && pmode != model.PMode)
            {
                updateList.Add(NewUpdateLog("pmode", pmode.ToString(), model.PMode.ToString()));
            }
            model.PMode = pmode;

            if (isUpdate && account != model.Account)
            {
                updateList.Add(NewUpdateLog("account", account, model.Account));
            }
            model.Account = account;

            if (isUpdate && payeeName != model.PayeeName)
            {
                updateList.Add(NewUpdateLog("payeeName", payeeName, model.PayeeName));
            }
            model.PayeeName = payeeName;
            model.accoutType = int.Parse(rblaccoutType.SelectedValue);

            #region payeeBank
            string bankCode = this.ddlpayeeBank.SelectedValue;
            string payeeBank = "";
            
            string provinceCode = "";
            string bankProvince = "";

            string cityCode = "";
            string bankCity = "";

            string bankAddress = this.txtbankAddress.Text;
            if (pmode == 1)
            {
                if (!string.IsNullOrEmpty(bankCode))
                {
                    payeeBank = ddlpayeeBank.Items[ddlpayeeBank.SelectedIndex].Text;
                }
                provinceCode = this.ddlprovince.SelectedValue;
                if (!string.IsNullOrEmpty(provinceCode))
                {
                    bankProvince = ddlprovince.Items[ddlprovince.SelectedIndex].Text;
                }

                cityCode = this.ddlcity.SelectedValue;
                if (!string.IsNullOrEmpty(cityCode))
                {
                    bankCity = ddlcity.Items[ddlcity.SelectedIndex].Text;
                }
            }
            else if (pmode == 2)
            {
                bankCode = "0002";
                payeeBank = "支付宝";
            }
            else if (pmode == 3)
            {
                bankCode = "0003";
                payeeBank = "财付通";
            }
            if (isUpdate && bankCode != model.BankCode)
            {
                updateList.Add(NewUpdateLog("bankCode", bankCode, model.BankCode));
            }
            if (isUpdate && payeeBank != model.PayeeBank)
            {
                updateList.Add(NewUpdateLog("payeeBank", payeeBank, model.PayeeBank));
            }
            if (isUpdate && bankProvince != model.BankProvince)
            {
                updateList.Add(NewUpdateLog("BankProvince", bankProvince, model.BankProvince));
            }
            if (isUpdate && bankCity != model.BankCity)
            {
                updateList.Add(NewUpdateLog("BankCity", bankCity, model.BankCity));
            }

            if (isUpdate && bankAddress != model.BankAddress)
            {
                updateList.Add(NewUpdateLog("bankAddress", bankAddress, model.BankAddress));
            }

            model.BankCode = bankCode;
            model.PayeeBank = payeeBank;
            model.provinceCode = provinceCode;
            model.BankProvince = bankProvince;
            model.cityCode = cityCode;
            model.BankCity = bankCity;

            model.BankAddress = bankAddress;
            #endregion

            if (isUpdate && status != model.Status)
            {
                updateList.Add(NewUpdateLog("status", status.ToString(), model.Status.ToString()));
            }
            model.Status = status;

            //model.company = company;
            //model.linkMan = linkMan;
            if (isUpdate && siteName != model.SiteName)
            {
                updateList.Add(NewUpdateLog("SiteName", siteName, model.SiteName));
            }
            model.SiteName = siteName;

            if (isUpdate && siteUrl != model.SiteUrl)
            {
                updateList.Add(NewUpdateLog("siteUrl", siteUrl, model.SiteUrl));
            }
            model.SiteUrl = siteUrl;

            if (isUpdate && userType != model.UserType)
            {
                updateList.Add(NewUpdateLog("userType", userType.ToString(), ((int)model.UserType).ToString()));
            }
            model.UserType = (UserTypeEnum)userType;

            if (isUpdate && userLevel != (int)model.UserLevel)
            {
                updateList.Add(NewUpdateLog("userLevel", userLevel.ToString(), ((int)model.UserLevel).ToString()));
            }
            model.UserLevel = userLevel;


            //if (isUpdate && maxdaytocashTimes != model.MaxDayToCashTimes)
            //{
            //    updateList.Add(newUpdateLog("MaxDayToCashTimes", maxdaytocashTimes.ToString(), model.MaxDayToCashTimes.ToString()));
            //}
            //model.MaxDayToCashTimes = maxdaytocashTimes;

            int maxdaytocashTimes = 0;
            if (!string.IsNullOrEmpty(ddlTocashScheme.SelectedValue))
            {
                maxdaytocashTimes = int.Parse(this.ddlTocashScheme.SelectedValue);
            }
            model.MaxDayToCashTimes = maxdaytocashTimes;
            if (isUpdate && maxdaytocashTimes != model.MaxDayToCashTimes)
            {
                updateList.Add(NewUpdateLog("MaxDayToCashTimes", maxdaytocashTimes.ToString(), model.MaxDayToCashTimes.ToString()));
            }


            int agentDistscheme = 0;
            if (!string.IsNullOrEmpty(this.ddlagentDistscheme.SelectedValue))
            {
                agentDistscheme = int.Parse(this.ddlagentDistscheme.SelectedValue);
            }
            model.agentDistscheme = agentDistscheme;


            if (isUpdate && apikey != model.APIKey)
            {
                updateList.Add(NewUpdateLog("APIKey", apikey, model.APIKey));
            }
            model.APIKey = apikey;
            model.Desc = this.txtdesc.Text;
            model.IsRealNamePass = cb_isRealNamePass.Checked ? 1 : 0;
            model.IsEmailPass = cb_isEmailPass.Checked ? 1 : 0;
            model.IsPhonePass = cb_isPhonePass.Checked ? 1 : 0;
            model.isagentDistribution = cb_isagentDistribution.Checked ? 1 : 0;

            model.isdebug = cb_isdebug.Checked ? 1 : 0;
            if (!string.IsNullOrEmpty(ddlmange.SelectedValue))
            {
                model.manageId = int.Parse(ddlmange.SelectedValue);
            }
            else
            {
                model.manageId = 0;
            }
            model.cardversion = byte.Parse(ddlcardversion.SelectedValue);

            model.province = this.txtProvince.Text.Trim();
            model.city = this.txtCity.Text.Trim();

            // model.IdCardType = 1;
            int result = 0;
            if (!this.isUpdate)
            {
                int id = Factory.Add(model);
                if (id > 0)
                {
                    setting.userid = id;
                    result = id;
                    AlertAndRedirect("保存成功！", "UserList.aspx");
                }
                else
                {
                    AlertAndRedirect("保存失败！");
                }
            }
            else
            {
                if (Factory.Update(model, updateList))
                {
                    result = ItemInfoId;


                    AlertAndRedirect("更新成功！", "UserList.aspx");
                }
                else
                {
                    AlertAndRedirect("更新失败！");
                }
            }

            if (result > 0)
            {
                if (!string.IsNullOrEmpty(this.ddlagents.SelectedValue))
                {
                    var promUser = new Promoter
                    {
                        PID = int.Parse(this.ddlagents.SelectedValue),
                        Prices = 0.5M,
                        RegId = result,
                        PromTime = DateTime.Now,
                        PromStatus = 1
                    };

                    viviapi.BLL.Promotion.Factory.Insert(promUser);
                }
                else
                {
                    viviapi.BLL.Promotion.Factory.Delete(model.ID);
                }

                setting.istransfer = this.cb_istransfer.Checked ? 1 : 0;

                setting.RiskWarning = (byte)(this.ckb_rw_bank.Checked ? 1 : 0);
                setting.AlipayRiskWarning = (byte)(this.ckb_rw_alipay.Checked ? 1 : 0);
                setting.AliCodeRiskWarning = (byte)(this.ckb_rw_alicode.Checked ? 1 : 0);
                setting.WxPayRiskWarning = (byte)(this.ckb_rw_wxpay.Checked ? 1 : 0);

                this.setbll.Insert(_setting);
            }
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Save();
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Merchant);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        #region NewUpdateLog
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        private UsersUpdateLog NewUpdateLog(string f, string n, string o)
        {
            var item = new UsersUpdateLog
            {
                userid = this.model.ID,
                Addtime = DateTime.Now,
                field = f,
                newvalue = n,
                oldValue = o,
                Editor = currentManage.username,
                OIp = ServerVariables.TrueIP
            };
            return item;
        }
        #endregion

        protected void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCity(string.Empty);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.txtapikey.Text = viviapi.BLL.User.Factory.GenerateAPIKey();
        }

        #region btnUnbind_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUnbind_Click(object sender, EventArgs e)
        {
            string message = "";

            bool result= loginByPartnerbll.Unbind(1, this.ItemInfoId);
            if (result)
            {
                message = "解绑成功";
            }
            else
            {
                message = "解绑失败";
            }

            AlertAndRedirect(message);
        }
        #endregion

        protected void btnClearCache_Click(object sender, EventArgs e)
        {
            string cacheKey = string.Format(viviapi.BLL.User.Factory.USER_CACHE_KEY, ItemInfoId);

            viviapi.WebComponents.WebUtility.ClearCache(cacheKey);

            cacheKey = string.Format(viviapi.BLL.Channel.ChannelTypeUsers.ChannelTypeUsers_CACHEKEY, ItemInfoId);

            viviapi.WebComponents.WebUtility.ClearCache(cacheKey);
        }
    }
}
