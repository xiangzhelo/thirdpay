using System;
using viviapi.BLL.Finance;
using viviapi.Model;
using viviapi.Model.Finance;

namespace viviAPI.WebAdmin.Console.settled
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TransferschemeModi : viviapi.WebComponents.Web.ManagePageBase
    {
        protected TransferScheme tsBLL = new TransferScheme();

        //public int ItemInfoId
        //{
        //    get
        //    {
        //        return WebBase.GetQueryStringInt32("id", 1);
        //    }
        //}

        //public string Action
        //{
        //    get
        //    {
        //        return WebBase.GetQueryStringString("cmd", "");
        //    }
        //}
        public bool isUpdate
        {
            get
            {
                return model.id > 0;
            }
        }

        public Transferscheme _ItemInfo = null;
        public Transferscheme model
        {
            get
            {
                if (_ItemInfo == null)
                {
                    _ItemInfo = tsBLL.GetModel(1);
                }
                if (_ItemInfo == null)
                {
                    _ItemInfo = new Transferscheme();
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
                txtmonthmaxamt.Text = model.monthmaxamt.ToString();
            }
        }

        bool isNum(string input)
        {
            return viviLib.Text.PageValidate.IsNumber(input) || viviLib.Text.PageValidate.IsDecimal(input);
        }

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
            if (!isNum(txtmonthmaxamt.Text.Trim()))
            {
                strErr += "每月免费流量 格式错误\n";
            }
            if (!isNum(txtminamtlimitofeach.Text.Trim()))
            {
                strErr += "最低提现金额限制(每笔)格式错误！\n";
            }
            if (!isNum(txtmaxamtlimitofeach.Text.Trim()))
            {
                strErr += "最大提现金额限制(每笔)格式错误！\n";
            }
            if (!viviLib.Text.PageValidate.IsNumber(txtdailymaxtimes.Text.Trim()))
            {
                strErr += "每天最多可提现次数格式错误！\n";
            }
            if (!isNum(txtdailymaxamt.Text.Trim()))
            {
                strErr += "每天最多可限额格式错误！\n";
            }
            if (!isNum(txtchargerate.Text.Trim()))
            {
                strErr += "提现手续费格式错误！\n";
            }
            if (!isNum(txtchargeleastofeach.Text.Trim()))
            {
                strErr += "提现手续费最少每笔格式错误！\n";
            }
            if (!isNum(txtchargemostofeach.Text.Trim()))
            {
                strErr += "提现手续费最高每笔格式错误！\n";
            }            

            if (strErr != "")
            {
                AlertAndRedirect(strErr);
                return;
            }
            string schemename = this.txtschemename.Text.Trim();
            decimal minamtlimitofeach = decimal.Parse(this.txtminamtlimitofeach.Text.Trim());
            decimal maxamtlimitofeach = decimal.Parse(this.txtmaxamtlimitofeach.Text.Trim());
            int dailymaxtimes = int.Parse(this.txtdailymaxtimes.Text.Trim());
            decimal dailymaxamt = decimal.Parse(this.txtdailymaxamt.Text.Trim());
            decimal chargerate = decimal.Parse(this.txtchargerate.Text.Trim());
            decimal chargeleastofeach = decimal.Parse(this.txtchargeleastofeach.Text.Trim());
            decimal chargemostofeach = decimal.Parse(this.txtchargemostofeach.Text.Trim());
            int isdefault = int.Parse(this.rblisdefault.SelectedValue);
            
            model.schemename = schemename;
            model.minamtlimitofeach = minamtlimitofeach;
            model.maxamtlimitofeach = maxamtlimitofeach;
            model.monthmaxamt = decimal.Parse(this.txtmonthmaxamt.Text);
            model.monthmaxtimes = 0;
            model.dailymaxtimes = dailymaxtimes;
            model.dailymaxamt = dailymaxamt;
            model.chargerate = chargerate;
            model.chargeleastofeach = chargeleastofeach;
            model.chargemostofeach = chargemostofeach;
            model.isdefault = isdefault;

            bool success = false;
            if (this.isUpdate)
            {
                if (tsBLL.Update(model))
                {
                    success = true;
                }
            }
            else
            {
                if (tsBLL.Add(model) > 0)
                {
                    success = true;
                }
            }
            if (success)
            {
                AlertAndRedirect("操作成功");
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
