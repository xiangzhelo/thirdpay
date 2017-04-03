using System;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.AgentWithdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AgentDistsSchemeModi : ManagePageBase
    {
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public string Action
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }
        public bool isUpdate
        {
            get
            {
                return ItemInfoId > 0 && Action == "edit";
            }
        }

        public viviapi.Model.Finance.TocashSchemeInfo _ItemInfo = null;
        public viviapi.Model.Finance.TocashSchemeInfo model
        {
            get
            {
                if (_ItemInfo == null)
                {
                    if (isUpdate)
                    {
                        _ItemInfo = viviapi.BLL.Finance.TocashScheme.GetModel(ItemInfoId);
                    }
                    else
                    {
                        _ItemInfo = new viviapi.Model.Finance.TocashSchemeInfo();
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
                this.InitForm();
            }
        }

        #region InitForm
        /// <summary>
        /// 
        /// </summary>
        private void InitForm()
        {           
            if (isUpdate)
            {
                this.txtschemename.Text = model.schemename;
                this.txtminamtlimitofeach.Text = model.minamtlimitofeach.ToString();
                this.txtmaxamtlimitofeach.Text = model.maxamtlimitofeach.ToString();
                this.txtdailymaxtimes.Text = model.dailymaxtimes.ToString();
                this.txtdailymaxamt.Text = model.dailymaxamt.ToString();
                this.txtchargerate.Text = model.chargerate.ToString();
                this.txtchargeleastofeach.Text = model.chargeleastofeach.ToString();
                this.txtchargemostofeach.Text = model.chargemostofeach.ToString();
                this.rblisdefault.SelectedValue = model.isdefault.ToString();
                this.rblVaiInterface.SelectedValue = model.vaiInterface.ToString();

                this.txtbankdetentiondays.Text = model.bankdetentiondays.ToString();
                this.txtcarddetentiondays.Text = model.carddetentiondays.ToString();
                this.txtotherdetentiondays.Text = model.otherdetentiondays.ToString();
                this.rbltranRequiredAudit.SelectedValue = model.tranRequiredAudit.ToString();
            }
        }

        bool isNum(string _input)
        {
            return viviLib.Text.PageValidate.IsNumber(_input) || viviLib.Text.PageValidate.IsDecimal(_input);
        }
        #endregion

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (this.txtschemename.Text.Trim().Length == 0)
            {                
                strErr += "方案名称不能为空！\n";
            }
            if (!isNum(txtminamtlimitofeach.Text))
            {
                strErr += "最低提现金额限制(每笔)格式错误！\n";
            }
            if (!isNum(txtmaxamtlimitofeach.Text))
            {
                strErr += "最大提现金额限制(每笔)格式错误！\n";
            }
            if (!viviLib.Text.PageValidate.IsNumber(txtdailymaxtimes.Text))
            {
                strErr += "每天最多可提现次数格式错误！\n";
            }
            if (!isNum(txtdailymaxamt.Text))
            {
                strErr += "每天最多可限额格式错误！\n";
            }
            if (!isNum(txtchargerate.Text))
            {
                strErr += "提现手续费格式错误！\n";
            }
            if (!isNum(txtchargeleastofeach.Text))
            {
                strErr += "提现手续费最少每笔格式错误！\n";
            }
            if (!isNum(txtchargemostofeach.Text))
            {
                strErr += "提现手续费最高每笔格式错误！\n";
            }            

            if (strErr != "")
            {
                AlertAndRedirect(strErr);
                return;
            }
            string schemename = this.txtschemename.Text;
            decimal minamtlimitofeach = decimal.Parse(this.txtminamtlimitofeach.Text);
            decimal maxamtlimitofeach = decimal.Parse(this.txtmaxamtlimitofeach.Text);
            int dailymaxtimes = int.Parse(this.txtdailymaxtimes.Text);
            decimal dailymaxamt = decimal.Parse(this.txtdailymaxamt.Text);
            decimal chargerate = decimal.Parse(this.txtchargerate.Text);
            decimal chargeleastofeach = decimal.Parse(this.txtchargeleastofeach.Text);
            decimal chargemostofeach = decimal.Parse(this.txtchargemostofeach.Text);
            int isdefault = int.Parse(this.rblisdefault.SelectedValue);
            int vaiInterface = int.Parse(this.rblVaiInterface.SelectedValue);
            int bankdetentiondays = 0;
            int carddetentiondays = 0;
            int otherdetentiondays = 0;

            int.TryParse(this.txtbankdetentiondays.Text.Trim(), out bankdetentiondays);
            int.TryParse(this.txtcarddetentiondays.Text.Trim(), out carddetentiondays);
            int.TryParse(this.txtotherdetentiondays.Text.Trim(), out otherdetentiondays);

            model.schemename = schemename;
            model.minamtlimitofeach = minamtlimitofeach;
            model.maxamtlimitofeach = maxamtlimitofeach;
            model.dailymaxtimes = dailymaxtimes;
            model.dailymaxamt = dailymaxamt;
            model.chargerate = chargerate;
            model.chargeleastofeach = chargeleastofeach;
            model.chargemostofeach = chargemostofeach;
            model.isdefault = isdefault;
            model.vaiInterface = vaiInterface;
            model.bankdetentiondays = bankdetentiondays;
            model.carddetentiondays = carddetentiondays;
            model.otherdetentiondays = otherdetentiondays;
            model.tranRequiredAudit = Convert.ToByte((string) this.rbltranRequiredAudit.SelectedValue);

            bool success = false;
            if (this.isUpdate)
            {
                if (viviapi.BLL.Finance.TocashScheme.Update(model))
                {
                    success = true;
                }
            }
            else
            {
                model.type = 2;
                if (viviapi.BLL.Finance.TocashScheme.Add(model) > 0)
                {
                    success = true;
                }
            }
            if (success)
            {
                AlertAndRedirect("操作成功", "AgentDistsSchemes.aspx");
            }
            else
            {
                AlertAndRedirect("操作失败");
            }
        }

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

       

    }
}
