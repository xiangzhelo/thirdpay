using System;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PayRateEdit : ManagePageBase
    {
        public byte Type
        {
            get
            {
                byte type = 1;

                string qvalue = WebBase.GetQueryStringString("type", "");

                if (!string.IsNullOrEmpty(qvalue))
                {
                    byte.TryParse(qvalue, out type);
                }

                return type;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int BillId
        {
            get
            {
                return WebBase.GetQueryStringInt32("billid", 0);
            }
        }

        public viviapi.Model.Finance.PayRate _model = null;
        public viviapi.Model.Finance.PayRate model
        {
            get
            {
                if (_model == null)
                {
                    _model = viviapi.BLL.Finance.PayRate.Instance.GetModel(Type, BillId);
                }
                if (_model == null)
                {
                    _model = new viviapi.Model.Finance.PayRate();
                    _model.rateType = Type;

                    _model.billId = BillId;
                    if (BillId != 0)
                    {
                        if (Type == 1)
                        {
                            _model.billame = viviapi.BLL.User.UserLevel.Instance.GetModel(BillId).levName;
                        }
                        else if (Type == 3)
                        {
                            _model.billame = viviapi.BLL.Supplier.Factory.GetSupplierName(BillId);
                        }
                    }
                    //_model.billame
                }

                return _model;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.BLL.ManageFactory.CheckSecondPwd();
            setPower();
            if (!this.IsPostBack)
            {
                hfrbl_type.Value = Type.ToString();
                if (BillId != 0)
                {
                    rbl_type.Enabled = false;

                    txtlevName.Attributes["readonly"] = "true";
                }
                ShowInfo();
            }
        }

        void ShowInfo()
        {
            if (model != null)
            {
                this.txtlevName.Text = model.billame;
                rbl_type.SelectedValue = ((int)model.rateType).ToString();
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
                this.txtp204.Text = (Convert.ToDecimal(model.p204) * 100).ToString("0.00");//wab微信支付
                this.txtp205.Text = (Convert.ToDecimal(model.p205) * 100).ToString("0.00");
               // this.txtp206.Text = (Convert.ToDecimal(model.p206) * 100).ToString("0.00");
                this.txtp207.Text = (Convert.ToDecimal(model.p207) * 100).ToString("0.00");

                this.txtp208.Text = (Convert.ToDecimal(model.p208) * 100).ToString("0.00");
                this.txtp209.Text = (Convert.ToDecimal(model.p209) * 100).ToString("0.00");
                this.txtp210.Text = (Convert.ToDecimal(model.p210) * 100).ToString("0.00");
                this.txtp300.Text = (Convert.ToDecimal(model.p300) * 100).ToString("0.00");
            }
            else
            {
                rbl_type.SelectedValue = (Type).ToString();
            }
        }

        public bool IsNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            decimal tempdt = 0M;
            return decimal.TryParse(input, out tempdt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";

            #region check value

            if (this.txtlevName.Text.Trim().Length == 0)
            {
                strErr += "levName不能为空！\n";
            }
            if (!IsNumber(txtp100.Text))
            {
                strErr += "p100格式错误！\n";
            }
            if (!IsNumber(txtp101.Text))
            {
                strErr += "p101格式错误！\n";
            }
            if (!IsNumber(txtp102.Text))
            {
                strErr += "p102格式错误！\n";
            }
            if (!IsNumber(txtp103.Text))
            {
                strErr += "p103格式错误！\n";
            }
            if (!IsNumber(txtp104.Text))
            {
                strErr += "p104格式错误！\n";
            }
            if (!IsNumber(txtp105.Text))
            {
                strErr += "p105格式错误！\n";
            }
            if (!IsNumber(txtp106.Text))
            {
                strErr += "p106格式错误！\n";
            }
            if (!IsNumber(txtp107.Text))
            {
                strErr += "p107格式错误！\n";
            }
            if (!IsNumber(txtp108.Text))
            {
                strErr += "p108格式错误！\n";
            }
            if (!IsNumber(txtp109.Text))
            {
                strErr += "p109格式错误！\n";
            }
            if (!IsNumber(txtp110.Text))
            {
                strErr += "p110格式错误！\n";
            }
            if (!IsNumber(txtp111.Text))
            {
                strErr += "p111格式错误！\n";
            }
            if (!IsNumber(txtp112.Text))
            {
                strErr += "p112格式错误！\n";
            }
            if (!IsNumber(txtp113.Text))
            {
                strErr += "p113格式错误！\n";
            }
            //if (!IsNumber(txtp114.Text))
            //{
            //    strErr += "p114格式错误！\n";
            //}
            //if (!IsNumber(txtp115.Text))
            //{
            //    strErr += "p115格式错误！\n";
            //}
            //if (!IsNumber(txtp116.Text))
            //{
            //    strErr += "p116格式错误！\n";
            //}
            if (!IsNumber(txtp117.Text))
            {
                strErr += "p117格式错误！\n";
            }
            if (!IsNumber(txtp118.Text))
            {
                strErr += "p118格式错误！\n";
            }
            if (!IsNumber(txtp119.Text))
            {
                strErr += "p119格式错误！\n";
            }

            if (!IsNumber(txtp200.Text))
            {
                strErr += "p200格式错误！\n";
            }
            //if (!IsNumber(txtp201.Text))
            //{
            //    strErr += "p201格式错误！\n";
            //}
            //if (!IsNumber(txtp202.Text))
            //{
            //    strErr += "p202格式错误！\n";
            //}
            if (!IsNumber(txtp203.Text))
            {
                strErr += "p203格式错误！\n";
            }
            if (!IsNumber(txtp204.Text))
            {
                strErr += "p204格式错误！\n";
            }
            if (!IsNumber(txtp205.Text))
            {
                strErr += "p205格式错误！\n";
            }
            if (!IsNumber(txtp208.Text))
            {
                strErr += "p208格式错误！\n";
            }
            if (!IsNumber(txtp209.Text))
            {
                strErr += "p209格式错误！\n";
            }

            if (!IsNumber(txtp300.Text))
            {
                strErr += "p300格式错误！\n";
            }

            #endregion

            if (strErr != "")
            {
                AlertAndRedirect(strErr);
                return;
            }

            string levName = this.txtlevName.Text;

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
           // decimal p206 = decimal.Parse(this.txtp206.Text) / 100;
            decimal p207 = decimal.Parse(this.txtp207.Text) / 100;

            decimal p208 = decimal.Parse(this.txtp208.Text) / 100;
            decimal p209 = decimal.Parse(this.txtp209.Text) / 100;
            decimal p210 = decimal.Parse(this.txtp210.Text) / 100;
            decimal p300 = decimal.Parse(this.txtp300.Text) / 100;

            model.billame = levName;
            model.rateType = int.Parse(this.hfrbl_type.Value);
            model.p100 = p100;
            model.p101 = p101;
            model.p102 = p102;
            model.p103 = p103;
            model.p104 = p104;
            model.p105 = p105;
            model.p106 = p106;
            model.p107 = p107;
            model.p108 = p108;
            model.p109 = p109;
            model.p110 = p110;
            model.p111 = p111;
            model.p112 = p112;
            model.p113 = p113;
            //model.p114 = p114;
            //model.p115 = p115;
            //model.p116 = p116;
            model.p117 = p117;
            model.p118 = p118;
            model.p119 = p119;

            model.p200 = p200;
            //model.p201 = p201;
            //model.p202 = p202;
            model.p203 = p203;
            model.p204 = p204;
            model.p205 = p205;
            //model.p206 = p206;//p207
            model.p207 = p207;//p207

            model.p208 = p208;
            model.p209 = p209;
            model.p210 = p210;
            model.p300 = p300;
            //存储过程中自动判断有无记录，决定进行更新还是插入操作
            int id = viviapi.BLL.Finance.PayRate.Instance.Insert(model);

            if (id > 0)
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
    }
}
