using System;
using viviapi.BLL;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.supplier
{

    /// <summary>
    /// 
    /// </summary>
    public partial class SupplierEdit : ManagePageBase
    {
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public bool IsUpdate
        {
            get
            {
                return ItemInfoId > 0;
            }
        }

        private SupplierInfo _itemInfo = null;
        public SupplierInfo ItemInfo
        {
            get
            {
                if (_itemInfo != null) return _itemInfo;

                _itemInfo = this.ItemInfoId > 0 ? Factory.GetModel(this.ItemInfoId) : new SupplierInfo();

                return _itemInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //viviapi.BLL.ManageFactory.CheckSecondPwd();
            setPower();
            if (!this.IsPostBack)
            {
                ShowInfo();
            }
        }

        void ShowInfo()
        {
            if (IsUpdate && ItemInfo != null)
            {
                this.txtcode.Text = ItemInfo.code.ToString();
                this.txtname.Text = ItemInfo.name;
                this.txtlogourl.Text = ItemInfo.logourl;
                if (ItemInfo.isbank != null) this.chkisbank.Checked = ItemInfo.isbank.Value;
                if (ItemInfo.iscard != null) this.chkiscard.Checked = ItemInfo.iscard.Value;
                if (ItemInfo.issms != null) this.chkissms.Checked = ItemInfo.issms.Value;
                if (ItemInfo.issx != null) this.chkissx.Checked = ItemInfo.issx.Value;
                this.chkisdistribution.Checked = ItemInfo.isdistribution;

                this.txtpuserid.Text = ItemInfo.puserid;
                this.txtpuserkey.Text = ItemInfo.puserkey;
                this.txtpusername.Text = ItemInfo.pusername;
                this.txtpuserid1.Text = ItemInfo.puserid1;
                this.txtpuserkey1.Text = ItemInfo.puserkey1;
                this.txtpuserid2.Text = ItemInfo.puserid2;
                this.txtpuserkey2.Text = ItemInfo.puserkey2;
                this.txtpuserid3.Text = ItemInfo.puserid3;
                this.txtpuserkey3.Text = ItemInfo.puserkey3;
                this.txtpuserid4.Text = ItemInfo.puserid4;
                this.txtpuserkey4.Text = ItemInfo.puserkey4;
                this.txtpuserid5.Text = ItemInfo.puserid5;
                this.txtpuserkey5.Text = ItemInfo.puserkey5;
                this.txtpurl.Text = ItemInfo.purl;
                this.txtpbakurl.Text = ItemInfo.pbakurl;
                this.txtJumpUrl.Text = ItemInfo.jumpUrl;

                this.rdobtn1.SelectedIndex = ItemInfo.useJump != null && ItemInfo.useJump ? 1 : 0;
                this.txtpostBankUrl.Text = ItemInfo.postBankUrl;
                this.txtpostCardUrl.Text = ItemInfo.postCardUrl;
                this.txtQueryCardUrl.Text = ItemInfo.queryCardUrl;

                this.txtpostSMSUrl.Text = ItemInfo.postSMSUrl;
                this.txtdistributionUrl.Text = ItemInfo.distributionUrl;
                this.txtlimitAmount.Text = ItemInfo.limitAmount.ToString();

                this.txtdesc.Text = ItemInfo.desc;
                this.txtsort.Text = ItemInfo.sort.ToString();
                this.txttimeout.Text = ItemInfo.timeout.ToString();

                txtSynsRetCode.Text = ItemInfo.SynsRetCode;
                txtAsynsRetCode.Text = ItemInfo.AsynsRetCode;

                chkSynsSummitLog.Checked = ItemInfo.SynsSummitLog;
                chkAsynsRetLog.Checked = ItemInfo.AsynsRetLog;
                this.rblmultiacct.SelectedValue = ItemInfo.multiacct ? "1" : "0";

                if (ItemInfo.issys.HasValue) { rblused.SelectedValue = ItemInfo.issys.Value ? "1" : "0"; }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int code = int.Parse(this.txtcode.Text);
            string name = this.txtname.Text;
            string logourl = this.txtlogourl.Text;
            bool isbank = this.chkisbank.Checked;
            bool iscard = this.chkiscard.Checked;
            bool issms = this.chkissms.Checked;
            bool issx = this.chkissx.Checked;
            bool multiacct = this.rblmultiacct.SelectedValue == "1";

            bool isdistribution = this.chkisdistribution.Checked;
            string distributionUrl = this.txtdistributionUrl.Text;
            string queryCardUrl = this.txtQueryCardUrl.Text.Trim();
            int limitAmount = int.Parse(txtlimitAmount.Text.Trim());

            string puserid = this.txtpuserid.Text;
            string puserkey = this.txtpuserkey.Text;
            string pusername = this.txtpusername.Text;
            string puserid1 = this.txtpuserid1.Text;
            string puserkey1 = this.txtpuserkey1.Text;
            string puserid2 = this.txtpuserid2.Text;
            string puserkey2 = this.txtpuserkey2.Text;
            string puserid3 = this.txtpuserid3.Text;
            string puserkey3 = this.txtpuserkey3.Text;
            string puserid4 = this.txtpuserid4.Text;
            string puserkey4 = this.txtpuserkey4.Text;
            string puserid5 = this.txtpuserid5.Text;
            string puserkey5 = this.txtpuserkey5.Text;
            string purl = this.txtpurl.Text;
            string pbakurl = this.txtpbakurl.Text;
            string postBankUrl = this.txtpostBankUrl.Text;
            string postCardUrl = this.txtpostCardUrl.Text;
            string postSMSUrl = this.txtpostSMSUrl.Text;
            string desc = this.txtdesc.Text;
            int sort = int.Parse(this.txtsort.Text);

            ItemInfo.code = code;
            ItemInfo.name = name;
            ItemInfo.logourl = logourl;
            ItemInfo.isbank = isbank;
            ItemInfo.iscard = iscard;
            ItemInfo.issms = issms;
            ItemInfo.issx = issx;
            ItemInfo.puserid = puserid;
            ItemInfo.puserkey = puserkey;
            ItemInfo.pusername = pusername;
            ItemInfo.puserid1 = puserid1;
            ItemInfo.puserkey1 = puserkey1;
            ItemInfo.puserid2 = puserid2;
            ItemInfo.puserkey2 = puserkey2;
            ItemInfo.puserid3 = puserid3;
            ItemInfo.puserkey3 = puserkey3;
            ItemInfo.puserid4 = puserid4;
            ItemInfo.puserkey4 = puserkey4;
            ItemInfo.puserid5 = puserid5;
            ItemInfo.puserkey5 = puserkey5;
            ItemInfo.purl = purl;
            ItemInfo.pbakurl = pbakurl;
            ItemInfo.postBankUrl = postBankUrl;
            ItemInfo.postCardUrl = postCardUrl;
            ItemInfo.postSMSUrl = postSMSUrl;
            ItemInfo.desc = desc;
            ItemInfo.sort = sort;
            ItemInfo.release = true;
            ItemInfo.issys = true;
            ItemInfo.jumpUrl = this.txtJumpUrl.Text.Trim();
            ItemInfo.useJump = rdobtn1.SelectedIndex == 1 ? true : false;
            ItemInfo.isdistribution = isdistribution;
            ItemInfo.distributionUrl = distributionUrl;
            ItemInfo.queryCardUrl = queryCardUrl;

            ItemInfo.SynsRetCode = this.txtSynsRetCode.Text.Trim();
            ItemInfo.AsynsRetCode = this.txtAsynsRetCode.Text.Trim();

            ItemInfo.SynsSummitLog = chkSynsSummitLog.Checked;
            ItemInfo.AsynsRetLog = chkAsynsRetLog.Checked;
            ItemInfo.issys = rblused.SelectedValue == "1" ? true : false;
            ItemInfo.multiacct = multiacct;
            ItemInfo.limitAmount = limitAmount;
            int timeout = 0;
            if (int.TryParse(this.txttimeout.Text.Trim(), out timeout))
            {
                ItemInfo.timeout = timeout;
            }

            if (!this.IsUpdate)
            {
                int id = Factory.Add(ItemInfo);
                if (id > 0)
                {
                    AlertAndRedirect("保存成功！", "List.aspx");
                }
                else
                {
                    ShowMessageBox("保存失败！");
                }
            }
            else
            {
                if (Factory.Update(ItemInfo))
                {
                    viviapi.WebComponents.WebUtility.ClearCache("SUPPLIER_" + ItemInfo.code.Value.ToString());
                    AlertAndRedirect("更新成功！", "List.aspx");
                }
                else
                {
                    ShowMessageBox("更新失败！");
                }
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
