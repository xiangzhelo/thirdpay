using System;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserPayRateEdit : ManagePageBase
    {

        /// <summary>
        /// 
        /// </summary>
        public int UserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }
        
        private UserSettingInfo _amtinfo = null;
        public UserSettingInfo AmtInfo
        {
            get
            {
                if (_amtinfo == null)
                {
                    if (UserId > 0)
                    {
                        _amtinfo = viviapi.BLL.User.UserSetting.Instance.GetModel(UserId);
                    }
                }
                return _amtinfo;
            }
        }

        private viviapi.Model.Finance.PayRate _userPayRate = null;
        public viviapi.Model.Finance.PayRate MuserPayRate
        {
            get
            {
                if (_userPayRate == null)
                {
                    if (UserId > 0)
                    {
                        _userPayRate = viviapi.BLL.Finance.PayRate.Instance.GetModel(2, UserId);
                    }
                }
                return _userPayRate;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.BLL.ManageFactory.CheckSecondPwd();
            setPower();
            if (!this.IsPostBack)
            {
                ShowInfo();
            }
        }

        #region ShowInfo
        /// <summary>
        /// 
        /// </summary>
        private void ShowInfo()
        {
            this.lblUserId.Text = this.UserId.ToString();
            if (AmtInfo != null)
            {
                this.ckb_isopen.Checked = AmtInfo.special > 0;
            }

            btnCopy.Visible = this.ckb_isopen.Checked;

            var model = viviapi.BLL.Finance.PayRate.Instance.GetModelByUser(UserId);

            if (model != null)
            {
                this.txtp100.Text = (Convert.ToDecimal(model.p100) * 100).ToString("0.00");
                this.txtp101.Text = (Convert.ToDecimal(model.p101) * 100).ToString("0.00");
                this.txtp102.Text = (Convert.ToDecimal(model.p102) * 100).ToString("0.00");
                this.txtp103.Text = (Convert.ToDecimal(model.p103) * 100).ToString("0.00");
                this.txtp104.Text = (Convert.ToDecimal(model.p104) * 100).ToString("0.00");
                this.txtp105.Text = (Convert.ToDecimal(model.p105) * 100).ToString("0.00");
                this.txtp106.Text = (Convert.ToDecimal(model.p106) * 100).ToString("0.00");
                this.txtp107.Text = (Convert.ToDecimal(model.p107) * 100).ToString("0.00");
                this.txtp108.Text = (Convert.ToDecimal(model.p108) * 100).ToString("0.00");
                this.txtp109.Text = (Convert.ToDecimal(model.p109) * 100).ToString("0.00");
                this.txtp110.Text = (Convert.ToDecimal(model.p110) * 100).ToString("0.00");
                this.txtp111.Text = (Convert.ToDecimal(model.p111) * 100).ToString("0.00");
                this.txtp112.Text = (Convert.ToDecimal(model.p112) * 100).ToString("0.00");
                this.txtp113.Text = (Convert.ToDecimal(model.p113) * 100).ToString("0.00");
                //this.txtp114.Text = (Convert.ToDecimal(model.p114) * 100).ToString("0.00");
                //this.txtp115.Text = (Convert.ToDecimal(model.p115) * 100).ToString("0.00");
                //this.txtp116.Text = (Convert.ToDecimal(model.p116) * 100).ToString("0.00");
                this.txtp117.Text = (Convert.ToDecimal(model.p117) * 100).ToString("0.00");
                this.txtp118.Text = (Convert.ToDecimal(model.p118) * 100).ToString("0.00");
                this.txtp119.Text = (Convert.ToDecimal(model.p119) * 100).ToString("0.00");

                this.txtp200.Text = (Convert.ToDecimal(model.p200) * 100).ToString("0.00");
                //this.txtp201.Text = (Convert.ToDecimal(model.p201) * 100).ToString("0.00");
                //this.txtp202.Text = (Convert.ToDecimal(model.p202) * 100).ToString("0.00");
                this.txtp203.Text = (Convert.ToDecimal(model.p203) * 100).ToString("0.00");//qq支付
                this.txtp204.Text = (Convert.ToDecimal(model.p204) * 100).ToString("0.00");
                this.txtp205.Text = (Convert.ToDecimal(model.p205) * 100).ToString("0.00");
             //   this.txtp206.Text = (Convert.ToDecimal(model.p206) * 100).ToString("0.00");
                this.txtp207.Text = (Convert.ToDecimal(model.p207) * 100).ToString("0.00");

                this.txtp208.Text = (Convert.ToDecimal(model.p208) * 100).ToString("0.00");
                this.txtp209.Text = (Convert.ToDecimal(model.p209) * 100).ToString("0.00");
                this.txtp210.Text = (Convert.ToDecimal(model.p210) * 100).ToString("0.00");
                this.txtp300.Text = (Convert.ToDecimal(model.p300) * 100).ToString("0.00");
                //this.txtp100.Text = (Convert.ToDecimal(model.p100) * 100).ToString("0.00");
                //this.txtp101.Text = (Convert.ToDecimal(model.p101) * 100).ToString("0.00");
                //this.txtp102.Text = (Convert.ToDecimal(model.p102) * 100).ToString("0.00");
                //this.txtp103.Text = (Convert.ToDecimal(model.p103) * 100).ToString("0.00");
                //this.txtp104.Text = (Convert.ToDecimal(model.p104) * 100).ToString("0.00");
                //this.txtp105.Text = (Convert.ToDecimal(model.p105) * 100).ToString("0.00");
                //this.txtp106.Text = (Convert.ToDecimal(model.p106) * 100).ToString("0.00");
                //this.txtp107.Text = (Convert.ToDecimal(model.p107) * 100).ToString("0.00");
                //this.txtp108.Text = (Convert.ToDecimal(model.p108) * 100).ToString("0.00");
                //this.txtp109.Text = (Convert.ToDecimal(model.p109) * 100).ToString("0.00");
                //this.txtp110.Text = (Convert.ToDecimal(model.p110) * 100).ToString("0.00");
                //this.txtp111.Text = (Convert.ToDecimal(model.p111) * 100).ToString("0.00");
                //this.txtp112.Text = (Convert.ToDecimal(model.p112) * 100).ToString("0.00");
                //this.txtp113.Text = (Convert.ToDecimal(model.p113) * 100).ToString("0.00");
                //this.txtp114.Text = (Convert.ToDecimal(model.p114) * 100).ToString("0.00");
                //this.txtp115.Text = (Convert.ToDecimal(model.p115) * 100).ToString("0.00");
                //this.txtp116.Text = (Convert.ToDecimal(model.p116) * 100).ToString("0.00");
                //this.txtp117.Text = (Convert.ToDecimal(model.p117) * 100).ToString("0.00");
                //this.txtp118.Text = (Convert.ToDecimal(model.p118) * 100).ToString("0.00");
                //this.txtp119.Text = (Convert.ToDecimal(model.p119) * 100).ToString("0.00");

                //this.txtp200.Text = (Convert.ToDecimal(model.p200) * 100).ToString("0.00");
                //this.txtp201.Text = (Convert.ToDecimal(model.p201) * 100).ToString("0.00");
                //this.txtp202.Text = (Convert.ToDecimal(model.p202) * 100).ToString("0.00");
                //this.txtp203.Text = (Convert.ToDecimal(model.p203) * 100).ToString("0.00");
                //this.txtp204.Text = (Convert.ToDecimal(model.p204) * 100).ToString("0.00");
                //this.txtp205.Text = (Convert.ToDecimal(model.p205) * 100).ToString("0.00");

                //this.txtp206.Text = (Convert.ToDecimal(model.p206) * 100).ToString("0.00");
                //this.txtp207.Text = (Convert.ToDecimal(model.p207) * 100).ToString("0.00");


                //this.txtp208.Text = (Convert.ToDecimal(model.p208) * 100).ToString("0.00");
                //this.txtp209.Text = (Convert.ToDecimal(model.p209) * 100).ToString("0.00");
                //this.txtp210.Text = (Convert.ToDecimal(model.p210) * 100).ToString("0.00");
                //this.txtp300.Text = (Convert.ToDecimal(model.p300) * 100).ToString("0.00");
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var userInfo = viviapi.BLL.User.Factory.GetModel(this.UserId);

            if (userInfo == null)
            {
                ShowMessageBox("商户不存在");
                return;
            }

            var amtInfo = AmtInfo;
            if (amtInfo == null)
            {
                amtInfo = new UserSettingInfo { userid = UserId, special = (byte)(this.ckb_isopen.Checked ? 1 : 0) };
            }
            else
            {
                amtInfo.userid = UserId;
                amtInfo.special = (byte) (this.ckb_isopen.Checked ? 1 : 0);
            }
            amtInfo.payrate = 0;
            if (amtInfo.special == 1)
            {
                #region check value

                string strErr = string.Empty;
                if (!viviLib.Text.Validate.IsNumber(txtp100.Text))
                {
                    strErr += "p100格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp101.Text))
                {
                    strErr += "p101格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp102.Text))
                {
                    strErr += "p102格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp103.Text))
                {
                    strErr += "p103格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp104.Text))
                {
                    strErr += "p104格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp105.Text))
                {
                    strErr += "p105格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp106.Text))
                {
                    strErr += "p106格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp107.Text))
                {
                    strErr += "p107格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp108.Text))
                {
                    strErr += "p108格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp109.Text))
                {
                    strErr += "p109格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp110.Text))
                {
                    strErr += "p110格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp111.Text))
                {
                    strErr += "p111格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp112.Text))
                {
                    strErr += "p112格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp113.Text))
                {
                    strErr += "p113格式错误！\n";
                }
                //if (!viviLib.Text.Validate.IsNumber(txtp114.Text))
                //{
                //    strErr += "p114格式错误！\n";
                //}
                //if (!viviLib.Text.Validate.IsNumber(txtp115.Text))
                //{
                //    strErr += "p115格式错误！\n";
                //}
                //if (!viviLib.Text.Validate.IsNumber(txtp116.Text))
                //{
                //    strErr += "p116格式错误！\n";
                //}
                if (!viviLib.Text.Validate.IsNumber(txtp117.Text))
                {
                    strErr += "p117格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp118.Text))
                {
                    strErr += "p118格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp119.Text))
                {
                    strErr += "p119格式错误！\n";
                }

                //if (!viviLib.Text.Validate.IsNumber(txtp200.Text))
                //{
                //    strErr += "p200格式错误！\n";
                //}
                //if (!viviLib.Text.Validate.IsNumber(txtp201.Text))
                //{
                //    strErr += "p201格式错误！\n";
                //}
                //if (!viviLib.Text.Validate.IsNumber(txtp202.Text))
                //{
                //    strErr += "p202格式错误！\n";
                //}
                if (!viviLib.Text.Validate.IsNumber(txtp203.Text))
                {
                    strErr += "p203格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp204.Text))
                {
                    strErr += "p204格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp205.Text))
                {
                    strErr += "p205格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp207.Text))
                {
                    strErr += "p207格式错误！\n";
                }

                if (!viviLib.Text.Validate.IsNumber(txtp208.Text))
                {
                    strErr += "p208格式错误！\n";
                }
                if (!viviLib.Text.Validate.IsNumber(txtp209.Text))
                {
                    strErr += "p209格式错误！\n";
                }

                if (!viviLib.Text.Validate.IsNumber(txtp300.Text))
                {
                    strErr += "p300格式错误！\n";
                }

                #endregion

                if (strErr != "")
                {
                    ShowMessageBox(strErr);
                    return;
                }
                #region

                decimal p100 = decimal.Parse(this.txtp100.Text) / 100;
                decimal p101 = decimal.Parse(this.txtp101.Text) / 100;
                decimal p102 = decimal.Parse(this.txtp102.Text) / 100;
                decimal p103 = decimal.Parse(this.txtp103.Text) / 100;
                decimal p104 = decimal.Parse(this.txtp104.Text) / 100;
                decimal p105 = decimal.Parse(this.txtp105.Text) / 100;
                decimal p106 = decimal.Parse(this.txtp106.Text) / 100;
                decimal p107 = decimal.Parse(this.txtp107.Text) / 100;
                decimal p108 = decimal.Parse(this.txtp108.Text) / 100;
                decimal p109 = decimal.Parse(this.txtp109.Text) / 100;
                decimal p110 = decimal.Parse(this.txtp110.Text) / 100;
                decimal p111 = decimal.Parse(this.txtp111.Text) / 100;
                decimal p112 = decimal.Parse(this.txtp112.Text) / 100;
                decimal p113 = decimal.Parse(this.txtp113.Text) / 100;
                //decimal p114 = decimal.Parse(this.txtp114.Text) / 100;
                //decimal p115 = decimal.Parse(this.txtp115.Text) / 100;
                //decimal p116 = decimal.Parse(this.txtp116.Text) / 100;
                decimal p117 = decimal.Parse(this.txtp117.Text) / 100;
                decimal p118 = decimal.Parse(this.txtp118.Text) / 100;
                decimal p119 = decimal.Parse(this.txtp119.Text) / 100;

                decimal p200 = decimal.Parse(this.txtp200.Text) / 100;
                //decimal p201 = decimal.Parse(this.txtp201.Text) / 100;
                //decimal p202 = decimal.Parse(this.txtp202.Text) / 100;
                decimal p203 = decimal.Parse(this.txtp203.Text) / 100;
                decimal p204 = decimal.Parse(this.txtp204.Text) / 100;
                decimal p205 = decimal.Parse(this.txtp205.Text) / 100;
              //  decimal p206 = decimal.Parse(this.txtp206.Text) / 100;
                decimal p207 = decimal.Parse(this.txtp207.Text) / 100;

                decimal p208 = decimal.Parse(this.txtp208.Text) / 100;
                decimal p209 = decimal.Parse(this.txtp209.Text) / 100;
                decimal p210 = decimal.Parse(this.txtp210.Text) / 100;
                decimal p300 = decimal.Parse(this.txtp300.Text) / 100;

                //decimal p100 = decimal.Parse(this.txtp100.Text) / 100;
                //decimal p101 = decimal.Parse(this.txtp101.Text) / 100;
                //decimal p102 = decimal.Parse(this.txtp102.Text) / 100;
                //decimal p103 = decimal.Parse(this.txtp103.Text) / 100;
                //decimal p104 = decimal.Parse(this.txtp104.Text) / 100;
                //decimal p105 = decimal.Parse(this.txtp105.Text) / 100;
                //decimal p106 = decimal.Parse(this.txtp106.Text) / 100;
                //decimal p107 = decimal.Parse(this.txtp107.Text) / 100;
                //decimal p108 = decimal.Parse(this.txtp108.Text) / 100;
                //decimal p109 = decimal.Parse(this.txtp109.Text) / 100;
                //decimal p110 = decimal.Parse(this.txtp110.Text) / 100;
                //decimal p111 = decimal.Parse(this.txtp111.Text) / 100;
                //decimal p112 = decimal.Parse(this.txtp112.Text) / 100;
                //decimal p113 = decimal.Parse(this.txtp113.Text) / 100;
                ////decimal p114 = decimal.Parse(this.txtp114.Text) / 100;
                ////decimal p115 = decimal.Parse(this.txtp115.Text) / 100;
                ////decimal p116 = decimal.Parse(this.txtp116.Text) / 100;
                //decimal p117 = decimal.Parse(this.txtp117.Text) / 100;
                //decimal p118 = decimal.Parse(this.txtp118.Text) / 100;
                //decimal p119 = decimal.Parse(this.txtp119.Text) / 100;

                ////decimal p200 = decimal.Parse(this.txtp200.Text) / 100;
                ////decimal p201 = decimal.Parse(this.txtp201.Text) / 100;
                ////decimal p202 = decimal.Parse(this.txtp202.Text) / 100;
                //decimal p203 = decimal.Parse(this.txtp203.Text) / 100;
                //decimal p204 = decimal.Parse(this.txtp204.Text) / 100;
                //decimal p205 = decimal.Parse(this.txtp205.Text) / 100;
                //decimal p206 = decimal.Parse(this.txtp206.Text) / 100;
                //decimal p207 = decimal.Parse(this.txtp207.Text) / 100;

                //decimal p208 = decimal.Parse(this.txtp208.Text) / 100;
                //decimal p209 = decimal.Parse(this.txtp209.Text) / 100;
                //decimal p210 = decimal.Parse(this.txtp210.Text) / 100;

                //decimal p300 = decimal.Parse(this.txtp300.Text) / 100;
                #endregion

                #region
                var rateInfo = new viviapi.Model.Finance.PayRate
                {
                    rateType = 2,
                    billId = UserId,
                    billame = userInfo.UserName,
                    p100 = p100,
                    p101 = p101,
                    p102 = p102,
                    p103 = p103,
                    p104 = p104,
                    p105 = p105,
                    p106 = p106,
                    p107 = p107,
                    p108 = p108,
                    p109 = p109,
                    p110 = p110,
                    p111 = p111,
                    p112 = p112,
                    p113 = p113,
                    //p114 = p114,
                    //p115 = p115,
                    //p116 = p116,
                    p117 = p117,
                    p118 = p118,
                    p119 = p119,
                    p200 = p200,
                    //p201 = p201,
                    //p202 = p202,
                    p203 = p203,
                    p204 = p204,
                    p205 = p205,

                   // p206 = p206,
                    p207 = p207,

                    p208 = p208,
                    p209 = p209,
                    p210 = p210,
                    p300 = p300
                };
                if (MuserPayRate != null)
                {
                    rateInfo.id = MuserPayRate.id;
                }
                int id = viviapi.BLL.Finance.PayRate.Instance.Insert(rateInfo);

                if (id <= 0)
                {
                    ShowMessageBox("保存失败");
                    return;
                }
                #endregion

                amtInfo.payrate = id;
            }

            bool result = viviapi.BLL.User.UserSetting.Instance.PayRateConfig(amtInfo);
            if (result)
            {
                ShowMessageBox("操作成功");
            }
            else
            {
                ShowMessageBox("操作失败");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Interfaces);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }


        #region btnCopy_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCopy_Click(object sender, EventArgs e)
        {
            var userInfo = viviapi.BLL.User.Factory.GetModel(this.UserId);
            if (userInfo != null)
            {
                var info = viviapi.BLL.Finance.PayRate.Instance.GetModel(1, userInfo.UserLevel);
                if (info != null)
                {
                    this.txtp100.Text = (Convert.ToDecimal(info.p100) * 100).ToString("0.00");
                    this.txtp101.Text = (Convert.ToDecimal(info.p101) * 100).ToString("0.00");
                    this.txtp102.Text = (Convert.ToDecimal(info.p102) * 100).ToString("0.00");
                    this.txtp103.Text = (Convert.ToDecimal(info.p103) * 100).ToString("0.00");
                    this.txtp104.Text = (Convert.ToDecimal(info.p104) * 100).ToString("0.00");
                    this.txtp105.Text = (Convert.ToDecimal(info.p105) * 100).ToString("0.00");
                    this.txtp106.Text = (Convert.ToDecimal(info.p106) * 100).ToString("0.00");
                    this.txtp107.Text = (Convert.ToDecimal(info.p107) * 100).ToString("0.00");
                    this.txtp108.Text = (Convert.ToDecimal(info.p108) * 100).ToString("0.00");
                    this.txtp109.Text = (Convert.ToDecimal(info.p109) * 100).ToString("0.00");
                    this.txtp110.Text = (Convert.ToDecimal(info.p110) * 100).ToString("0.00");
                    this.txtp111.Text = (Convert.ToDecimal(info.p111) * 100).ToString("0.00");
                    this.txtp112.Text = (Convert.ToDecimal(info.p112) * 100).ToString("0.00");
                    this.txtp113.Text = (Convert.ToDecimal(info.p113) * 100).ToString("0.00");
                    //this.txtp114.Text = (Convert.ToDecimal(info.p114) * 100).ToString("0.00");
                    //this.txtp115.Text = (Convert.ToDecimal(info.p115) * 100).ToString("0.00");
                    //this.txtp116.Text = (Convert.ToDecimal(info.p116) * 100).ToString("0.00");
                    this.txtp117.Text = (Convert.ToDecimal(info.p117) * 100).ToString("0.00");
                    this.txtp118.Text = (Convert.ToDecimal(info.p118) * 100).ToString("0.00");
                    this.txtp119.Text = (Convert.ToDecimal(info.p119) * 100).ToString("0.00");

                    //this.txtp200.Text = (Convert.ToDecimal(info.p200) * 100).ToString("0.00");
                    //this.txtp201.Text = (Convert.ToDecimal(info.p201) * 100).ToString("0.00");
                    //this.txtp202.Text = (Convert.ToDecimal(info.p202) * 100).ToString("0.00");
                    //this.txtp203.Text = (Convert.ToDecimal(info.p203) * 100).ToString("0.00");
                    this.txtp204.Text = (Convert.ToDecimal(info.p204) * 100).ToString("0.00");
                    this.txtp205.Text = (Convert.ToDecimal(info.p205) * 100).ToString("0.00");


                    this.txtp207.Text = (Convert.ToDecimal(info.p207) * 100).ToString("0.00");
                    this.txtp208.Text = (Convert.ToDecimal(info.p208) * 100).ToString("0.00");
                    this.txtp209.Text = (Convert.ToDecimal(info.p209) * 100).ToString("0.00");
                    this.txtp210.Text = (Convert.ToDecimal(info.p210) * 100).ToString("0.00");

                    this.txtp300.Text = (Convert.ToDecimal(info.p300) * 100).ToString("0.00");
                }
            }


        }
        #endregion

        protected void ckb_isopen_CheckedChanged(object sender, EventArgs e)
        {
            btnCopy.Visible = this.ckb_isopen.Checked;
        }
    }
}
