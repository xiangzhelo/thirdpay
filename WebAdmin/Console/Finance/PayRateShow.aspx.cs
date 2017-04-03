using System;
using System.Globalization;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PayRateShow : ManagePageBase
    {
        /// <summary>
        /// 
        /// </summary>
        public int ObjectId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public viviapi.Model.Finance.PayRate _model = null;
        public viviapi.Model.Finance.PayRate model
        {
            get
            {
                if (_model == null)
                {
                    if (ObjectId > 0)
                    {
                        _model = viviapi.BLL.Finance.PayRate.Instance.GetModel(ObjectId);
                    }
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
                ShowInfo();
            }
        }

        #region ShowInfo
        /// <summary>
        /// 
        /// </summary>
        void ShowInfo()
        {
            if (model != null)
            {
                lblType.Text = viviapi.BLL.Finance.PayRate.Instance.GetRateTypeName(model.rateType);

                this.txtlevName.Text = model.billame+"(#"+model.billId.ToString(CultureInfo.InvariantCulture)+")";
              
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
                this.txtp114.Text = (Convert.ToDecimal(model.p114) * 100).ToString("0.00");
                this.txtp115.Text = (Convert.ToDecimal(model.p115) * 100).ToString("0.00");
                this.txtp116.Text = (Convert.ToDecimal(model.p116) * 100).ToString("0.00");
                this.txtp117.Text = (Convert.ToDecimal(model.p117) * 100).ToString("0.00");
                this.txtp118.Text = (Convert.ToDecimal(model.p118) * 100).ToString("0.00");
                this.txtp119.Text = (Convert.ToDecimal(model.p119) * 100).ToString("0.00");

                this.txtp200.Text = (Convert.ToDecimal(model.p200) * 100).ToString("0.00");
                this.txtp201.Text = (Convert.ToDecimal(model.p201) * 100).ToString("0.00");
                this.txtp202.Text = (Convert.ToDecimal(model.p202) * 100).ToString("0.00");
                this.txtp203.Text = (Convert.ToDecimal(model.p203) * 100).ToString("0.00");
                this.txtp204.Text = (Convert.ToDecimal(model.p204) * 100).ToString("0.00");
                this.txtp205.Text = (Convert.ToDecimal(model.p205) * 100).ToString("0.00");

                this.txtp206.Text = (Convert.ToDecimal(model.p206) * 100).ToString("0.00");
                this.txtp207.Text = (Convert.ToDecimal(model.p207) * 100).ToString("0.00");

                this.txtp208.Text = (Convert.ToDecimal(model.p208) * 100).ToString("0.00");
                this.txtp209.Text = (Convert.ToDecimal(model.p209) * 100).ToString("0.00");
                this.txtp210.Text = (Convert.ToDecimal(model.p210) * 100).ToString("0.00");
                this.txtp300.Text = (Convert.ToDecimal(model.p300) * 100).ToString("0.00");
            }

        }
        #endregion

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
