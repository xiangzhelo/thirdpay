using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using viviapi.BLL.Finance;
using viviLib.Web;
using viviapi.Model;
using viviapi.BLL;
using viviapi.Model.User;
using viviapi.BLL.User;
using viviapi.Model.Payment;
using System.Collections.Generic;

namespace viviapi.web.Manage.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserAdd : viviapi.WebComponents.Web.ManagePageBase
    {
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

                rbluserType.Style.Add("display","none");
            }
        }

        #region InitForm
        void InitForm()
        {
            if (isSuperAdmin == false)
            {
                if (model.manageId != this.currentManage.id)
                {
                    Response.Write("Sorry,No authority!");
                    Response.End();
                }
            }

            ddlmemvip.Style.Add("display", "none");
            ddlpromvip.Style.Add("display", "none");
            foreach (int item in Enum.GetValues(typeof(UserStatusEnum)))
            {
                this.ddlStatus.Items.Add(new ListItem(Enum.GetName(typeof(UserStatusEnum), item), item.ToString()));
            }
            DataTable levData = viviapi.BLL.User.UserLevel.Instance.GetAllList().Tables[0];
            ddlmemvip.Items.Add("--商户等级--");
            foreach (DataRow row in levData.Rows)
            {
                ddlmemvip.Items.Add(new ListItem(row["levName"].ToString(), row["level"].ToString()));
            }

            ddlmange.Items.Add(new ListItem("--请选择管理员--",""));
            levData = BLL.ManageFactory.GetList(" status =1").Tables[0];
            foreach (DataRow dr in levData.Rows)
            {
                ddlmange.Items.Add(new ListItem(dr["username"].ToString(), dr["id"].ToString()));
            }
            ddlTocashScheme.Items.Add(new ListItem("--默认--", ""));
            levData = TocashScheme.GetList(string.Empty).Tables[0];
            foreach (DataRow dr in levData.Rows)
            {
                ddlTocashScheme.Items.Add(new ListItem(dr["schemename"].ToString(), dr["id"].ToString()));
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
                    txtGetPromSuperior.Text = promSuper.UserName;
                else
                    txtGetPromSuperior.Text = "无代理";

                rbuserclass.SelectedValue = model.classid.ToString();
             
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
                this.txtaccount.Text = model.Account;
                this.txtpayeeName.Text = model.PayeeName;
                this.txtpayeeBank.Text = model.PayeeBank;
                this.txtbankProvince.Text = model.BankProvince;
                this.txtbankCity.Text = model.BankCity;
                this.txtbankAddress.Text = model.BankAddress;
                this.ddlStatus.SelectedValue = model.Status.ToString();

                
                //this.txtcompany.Text = model.company;
                //this.txtlinkMan.Text = model.linkMan;
                
                //this.txtpayment.Text = model.payment.ToString();
                //this.txtunpayment.Text = model.unpayment.ToString();
                //this.txtagentId.Text = model.agentId.ToString();
                this.txtsiteName.Text = model.SiteName;
                this.txtsiteUrl.Text = model.SiteUrl;
                this.rbluserType.SelectedValue = ((int)model.UserType).ToString();
                txtapiAcct.Text = model.APIAccount.ToString();
                if (model.UserType == UserTypeEnum.会员)
                {
                    ddlmemvip.SelectedValue = ((int)model.UserLevel).ToString();
                }
                if (model.UserType == UserTypeEnum.代理)
                {
                    ddlpromvip.SelectedValue = ((int)model.UserLevel).ToString();
                }

                

                int maxdaytocashTimes = 0;
                if (!string.IsNullOrEmpty(ddlTocashScheme.SelectedValue))
                {
                    maxdaytocashTimes = int.Parse(this.ddlTocashScheme.SelectedValue);
                }
                //this.txtapiaccount.Text = model.apiaccount;
                this.txtapikey.Text = model.APIKey;
            
                this.txtdesc.Text = model.Desc;
                this.ddlmange.SelectedValue = model.manageId.ToString();
                txtquestion.Text = model.question;
                txtanswer.Text = model.answer;
                cb_isRealNamePass.Checked = model.IsRealNamePass == 1;
                cb_isEmailPass.Checked = model.IsEmailPass == 1;
                cb_isPhonePass.Checked = model.IsPhonePass == 1;

               
                //this.txtupdatetime.Text = model.updatetime.ToString();

                rbl_settledmode.SelectedValue = model.Settles.ToString();
                cb_isdebug.Checked = model.isdebug == 1;
            }
        }
        #endregion

        #region Save
        void Save()
        {
            List<UsersUpdateLog> updateList = new List<UsersUpdateLog>();

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
            string payeeBank = this.txtpayeeBank.Text;
            string bankProvince = this.txtbankProvince.Text;
            string bankCity = this.txtbankCity.Text;
            string bankAddress = this.txtbankAddress.Text;
            int status = int.Parse(ddlStatus.SelectedValue);
            model.classid = int.Parse(rbuserclass.SelectedValue);
            //string company = this.txtcompany.Text;
            //string linkMan = this.txtlinkMan.Text;
            string account = this.txtaccount.Text;
            string siteName = this.txtsiteName.Text;
            string siteUrl = this.txtsiteUrl.Text;
            UserTypeEnum userType = (UserTypeEnum)int.Parse(rbluserType.SelectedValue);
            int userLevel = 0;
            if (userType == UserTypeEnum.会员)
            {
                userLevel = int.Parse(this.ddlmemvip.SelectedValue);
            }
            else if (userType == UserTypeEnum.代理)
            {
                userLevel = int.Parse(this.ddlpromvip.SelectedValue);
            }

            //int maxdaytocashTimes = int.Parse(this.txtmaxdaytocashTimes.Text);
            string apikey = this.txtapikey.Text;
            //if (isUpdate && userName != model.UserName)
            //{

            //}
            model.UserName = userName;
            model.APIAccount = int.Parse(this.txtapiAcct.Text);
            model.Settles = Convert.ToInt32(rbl_settledmode.SelectedValue);
            
            //model.classid

            if (!string.IsNullOrEmpty(password))
            {
                password = viviLib.Security.Cryptography.MD5(password);
                if (isUpdate && password != model.Password)
                {
                    updateList.Add(newUpdateLog("password", password, model.Password));
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
                updateList.Add(newUpdateLog("CPSDrate", CPSDrate.ToString(), model.CPSDrate.ToString()));
            }
            model.CPSDrate = CPSDrate;

            if (isUpdate && CVSNrate != model.CVSNrate)
            {
                updateList.Add(newUpdateLog("CVSNrate", CVSNrate.ToString(), model.CVSNrate.ToString()));
            }
            model.CVSNrate = CVSNrate;

            if (isUpdate && email != model.Email)
            {
                updateList.Add(newUpdateLog("Email", email, model.Email));
            }
            model.Email = email;

            if (isUpdate && qq != model.QQ)
            {
                updateList.Add(newUpdateLog("QQ", qq, model.QQ));
            }
            model.QQ = qq;

            if (isUpdate && tel != model.Tel)
            {
                updateList.Add(newUpdateLog("tel", tel, model.Tel));
            }
            model.Tel = tel;

            if (isUpdate && idCard != model.IdCard)
            {
                updateList.Add(newUpdateLog("idCard", idCard, model.IdCard));
            }
            model.IdCard = idCard;

            //收款
            if (isUpdate && pmode != model.PMode)
            {
                updateList.Add(newUpdateLog("pmode", pmode.ToString(), model.PMode.ToString()));
            }
            model.PMode = pmode;

            if (isUpdate && account != model.Account)
            {
                updateList.Add(newUpdateLog("account", account, model.Account));
            }
            model.Account = account;

            if (isUpdate && payeeName != model.PayeeName)
            {
                updateList.Add(newUpdateLog("payeeName", payeeName, model.PayeeName));
            }
            model.PayeeName = payeeName;

            if (isUpdate && payeeBank != model.PayeeBank)
            {
                updateList.Add(newUpdateLog("payeeBank", payeeBank, model.PayeeBank));
            }
            model.PayeeBank = payeeBank;

            if (isUpdate && bankProvince != model.BankProvince)
            {
                updateList.Add(newUpdateLog("BankProvince", bankProvince, model.BankProvince));
            }
            model.BankProvince = bankProvince;

            if (isUpdate && bankCity != model.BankCity)
            {
                updateList.Add(newUpdateLog("BankCity", bankCity, model.BankCity));
            }
            model.BankCity = bankCity;

            if (isUpdate && bankAddress != model.BankAddress)
            {
                updateList.Add(newUpdateLog("bankAddress", bankAddress, model.BankAddress));
            }
            model.BankAddress = bankAddress;

            if (isUpdate && status != model.Status)
            {
                updateList.Add(newUpdateLog("status", status.ToString(), model.Status.ToString()));
            }
            model.Status = status;

            //model.company = company;
            //model.linkMan = linkMan;
            if (isUpdate && siteName != model.SiteName)
            {
                updateList.Add(newUpdateLog("SiteName", siteName, model.SiteName));
            }
            model.SiteName = siteName;

            if (isUpdate && siteUrl != model.SiteUrl)
            {
                updateList.Add(newUpdateLog("siteUrl", siteUrl, model.SiteUrl));
            }
            model.SiteUrl = siteUrl;

            if (isUpdate && userType != model.UserType)
            {
                updateList.Add(newUpdateLog("userType", userType.ToString(), ((int)model.UserType).ToString()));
            }
            model.UserType = (UserTypeEnum)userType;

            if (isUpdate && userLevel != (int)model.UserLevel)
            {
                updateList.Add(newUpdateLog("userLevel", userLevel.ToString(), ((int)model.UserLevel).ToString()));
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
            if (isUpdate && maxdaytocashTimes != model.MaxDayToCashTimes)
            {
                updateList.Add(newUpdateLog("MaxDayToCashTimes", maxdaytocashTimes.ToString(), model.MaxDayToCashTimes.ToString()));
            }
            model.MaxDayToCashTimes = maxdaytocashTimes;

            if (isUpdate && apikey != model.APIKey)
            {
                updateList.Add(newUpdateLog("APIKey", apikey, model.APIKey));
            }
            model.APIKey = apikey;
            model.Desc = this.txtdesc.Text;
            model.IsRealNamePass = cb_isRealNamePass.Checked ? 1 : 0;
            model.IsEmailPass = cb_isEmailPass.Checked ? 1 : 0;
            model.IsPhonePass = cb_isPhonePass.Checked ? 1 : 0;

            model.isdebug = cb_isdebug.Checked ? 1 : 0;
            if (!string.IsNullOrEmpty(ddlmange.SelectedValue))
            {
                model.manageId = int.Parse(ddlmange.SelectedValue);
            }

           // model.IdCardType = 1;
            
            if (!this.isUpdate)
            {
                int id = Factory.Add(model);
                if (id > 0)
                {
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
                    AlertAndRedirect("更新成功！", "UserList.aspx");
                }
                else
                {
                    AlertAndRedirect("更新失败！");
                }
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
            bool result = BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Merchant);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        private UsersUpdateLog newUpdateLog(string f, string n, string o)
        {
            UsersUpdateLog item = new UsersUpdateLog();
            item.userid = this.model.ID;
            item.Addtime = DateTime.Now;
            item.field = f;
            item.newvalue = n;
            item.oldValue = o;
            item.Editor = BLL.ManageFactory.CurrentManage.username;
            item.OIp = viviLib.Web.ServerVariables.TrueIP;
            return item;
        }
    }
}
