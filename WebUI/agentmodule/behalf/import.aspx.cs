using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Finance;
using viviapi.Model.Channel;
using viviapi.Model.Finance;
using viviapi.Model.Finance.Agent;
using viviapi.Model.Settled;

using DBAccess;
using viviapi.WebComponents.Web;
using viviLib.Data;
using Aspose.Cells;
using System.IO;
using System.Data;

namespace viviAPI.WebUI7uka.agentmodule.behalf
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Import : AgentPageBase
    {
        viviapi.BLL.Finance.Agent.WithdrawAgentSummary _bll = new viviapi.BLL.Finance.Agent.WithdrawAgentSummary();
        viviapi.BLL.Withdraw.ChannelWithdraw _chnlbll = new viviapi.BLL.Withdraw.ChannelWithdraw();
        viviapi.BLL.Finance.Agent.WithdrawAgent _setAntBLL = new viviapi.BLL.Finance.Agent.WithdrawAgent();

        TocashSchemeInfo _scheme = null;
        /// <summary>
        /// 
        /// </summary>
        protected TocashSchemeInfo scheme
        {
            get
            {
                if (_scheme == null)
                    _scheme = TocashScheme.GetModelByUser(2, this.UserId);

                return _scheme;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                sure();
            }
        }

        #region check
        //.xlsx 8075 
        //.xls 208207 
        //.xls application/vnd.ms-excel 
        //.xlsx application/vnd.openxmlformats-officedocument.spreadsheetml.sheet 
        string check()
        {
            if (!file_data.HasFile)
                return "请选择要上传的文件";

            bool isOk = false;
            string fileExtension = System.IO.Path.GetExtension(file_data.FileName).ToLower();
            string[] allowExtension = { ".xls", ".xlsx" };
            for (int i = 0; i < allowExtension.Length; i++)
            {
                if (fileExtension == allowExtension[i])
                {
                    isOk = true;
                }
            }
            if (isOk == false)
            {
                return "对不起，文件格式不正确。";
            }

            //string type = file_data.PostedFile.ContentType.ToLower();
            //if (type != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            //{
            //    return "对不起，文件格式不正确。";
            //}

            if (file_data.PostedFile.ContentLength > 4 * 1024 * 1024)
            {
                return "对不起，文件不可大于4M。";
            }



            return "";
        }
        #endregion

        #region
        void sure()
        {
            bool _issure = this.cbx_sure.Checked;

            file_data.Enabled = _issure;
            btnupload.Enabled = _issure;
        }

        protected void cbx_sure_CheckedChanged(object sender, EventArgs e)
        {
            sure();
        }
        #endregion

        #region btnupload_Click
        /// <summary>
        /// 是否开启代发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnupload_Click(object sender, EventArgs e)
        {
            if (CurrentUser.isagentDistribution == 0)
            {
                AlertAndRedirect("未开通此功能！请先提交申请！");
                return;
            }
            if (scheme == null)
            {
                AlertAndRedirect("未设置提现方案，请联系商务！");
                return;
            }
            string message = check();

            Stream fs = file_data.FileContent;
            BinaryReader r = new System.IO.BinaryReader(fs);
            string fileclass = "";
            //这里的位长要具体判断.
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();

            }
            catch
            {

            }
            //r.Close();

            if (fileclass != "8075" && fileclass != "208207")
            {
                AlertAndRedirect("对不起，文件格式不正确。");
                return;
            }

            if (!string.IsNullOrEmpty(message))
            {
                AlertAndRedirect(message);
                return;
            }

            try
            {
                LoadOptions _options = new LoadOptions(LoadFormat.Xlsx);
                Workbook workbook = new Workbook(fs, _options);
                Cells cells = workbook.Worksheets[0].Cells;
                System.Data.DataTable dataTable = cells.ExportDataTable(9, 0, cells.MaxDataRow, 7);
                r.Close();
                fs.Close();

                if (dataTable == null ||
                    dataTable.Rows.Count <= 0)
                {
                    AlertAndRedirect("无代发数据.你检查文件格式是否修改");
                    return;
                }

                WithdrawAgentSummary model = new WithdrawAgentSummary();
                model.userid = UserId;
                model.lotno = _bll.Generatelotno();

                #region items
                List<WithdrawAgent> list = new List<WithdrawAgent>();
                int qty = 0;
                int success = 0;
                decimal tempamt = 0M;
                decimal amt = 0M;
                decimal fee = 0M;
                decimal tempfee = 0M;
                int suppid = 0;
                string bankName_str = string.Empty;
                string bankCode_str = string.Empty;
                string bankBranch_str = string.Empty;
                string bankAccountName_str = string.Empty;
                string bankAccount_str = string.Empty;
                string amount_str = string.Empty;
                string remark = string.Empty;

                foreach (DataRow row in dataTable.Rows)
                {
                    remark = string.Empty;

                    bool is_cancel = false;
                    #region check
                    #region bank 目标银行
                    bankName_str = string.Empty;
                    bankCode_str = string.Empty;
                    //目标银行
                    if (row[1] == null || row[1] == DBNull.Value)
                    {

                        remark = " 目标银行为空";
                        is_cancel = true;
                    }
                    else
                    {
                        bankName_str = row[1].ToString();
                        ChannelWithdraw channel
                                = _chnlbll.GetModelByBankName(bankName_str);
                        if (channel == null)
                        {
                            is_cancel = true;
                            remark = " 不支持目标银行";
                        }
                        else
                        {
                            suppid = channel.supplier;
                            bankCode_str = channel.bankCode;
                        }
                    }
                    #endregion

                    #region bankBranch 开户网点
                    bankBranch_str = string.Empty;
                    if (bankName_str != "财付通")
                    {
                        //开户网点
                        if (row[2] == null || row[2] == DBNull.Value)
                        {

                            remark += " 开户网点为空";
                            is_cancel = true;
                        }
                        else
                        {
                            bankBranch_str = row[2].ToString();

                            if (string.IsNullOrEmpty(bankBranch_str))
                            {
                                remark += " 开户网点为空";
                                is_cancel = true;
                            }
                        }
                    }
                    #endregion

                    #region bankAccountName 开户名
                    //开户名
                    if (row[3] == null || row[3] == DBNull.Value)
                    {
                        bankAccountName_str = string.Empty;
                        remark += " 开户名为空";
                        is_cancel = true;
                    }
                    else
                    {
                        bankAccountName_str = row[3].ToString();
                    }
                    #endregion

                    #region bankAccount 账号
                    //账号
                    if (row[4] == null || row[4] == DBNull.Value)
                    {
                        bankAccount_str = string.Empty;
                        remark += " 账号为空";
                        is_cancel = true;
                    }
                    else
                    {
                        bankAccount_str = row[4].ToString();
                    }
                    #endregion

                    #region amount_str 代发金额
                    tempamt = 0M;
                    tempfee = 0M;

                    //账号
                    if (row[5] == null || row[5] == DBNull.Value)
                    {
                        remark += " 代发金额为空";
                        is_cancel = true;
                    }
                    else
                    {
                        amount_str = row[5].ToString();

                        if (decimal.TryParse(amount_str, out tempamt) == false)
                        {
                            remark += " 代发金额格式不正确";
                            is_cancel = true;
                        }
                        else
                        {
                            amt += tempamt;

                            tempfee = scheme.chargerate * tempamt;
                            if (tempfee < scheme.chargeleastofeach)
                            {
                                tempfee = scheme.chargeleastofeach;
                            }
                            else if (tempfee > scheme.chargemostofeach)
                            {
                                tempfee = scheme.chargemostofeach;
                            }

                            fee += tempfee;
                        }
                    }
                    #endregion
                    #endregion

                    if (
                        string.IsNullOrEmpty(bankName_str)
                        || string.IsNullOrEmpty(bankAccount_str)
                        || string.IsNullOrEmpty(bankAccountName_str)
                        || string.IsNullOrEmpty(amount_str)
                        )
                    {
                        continue;
                    }
                    qty++;

                    WithdrawAgent _item = new WithdrawAgent();
                    _item.lotno = model.lotno;
                    _item.serial = qty;


                    _item.amount = tempamt;
                    _item.bankAccount = bankAccount_str;
                    _item.bankAccountName = bankAccountName_str;
                    _item.bankBranch = bankBranch_str;
                    _item.bankName = bankName_str;
                    _item.bankCode = bankCode_str;
                    if (row[6] != null && row[6] != DBNull.Value)
                    {
                        _item.ext1 = row[6].ToString();
                    }

                    _item.charge = tempfee;
                    _item.mode = 2;
                    _item.out_trade_no = model.lotno;
                    _item.remark = remark;
                    _item.return_url = string.Empty;
                    _item.service = string.Empty;
                    _item.input_charset = string.Empty;
                    _item.sign_type = string.Empty;

                    _item.userid = UserId;

                    _item.trade_no = _setAntBLL.GenerateOrderId();
                    _item.is_cancel = is_cancel;
                    _item.tranApi = 0;
                    if (is_cancel == false)
                    {
                        if (scheme.vaiInterface == 1)
                        {
                            _item.tranApi = suppid;

                            if (scheme.tranRequiredAudit == 0)
                            {
                                _item.audit_status = 2;
                                _item.auditTime = DateTime.Now;
                                _item.auditUser = 0;
                                _item.auditUserName = "自动审核";
                            }
                        }
                    }

                    list.Add(_item);
                }
                #endregion

                model.qty = qty;
                model.amt = amt;
                model.fee = fee;

                if (amt <= 0M || qty <= 0)
                {
                    AlertAndRedirect("无有效代发数据，请检查文件");
                    return;
                }
                message = string.Empty;
                int chkResult = _bll.ChkParms(this.UserId, amt);
                if (chkResult == 1)
                {
                    message = ("用户状态不正常。");
                }
                if (chkResult == 2)
                {
                    message = ("商户未签约当前产品。");

                }
                if (chkResult == 3)
                {
                    message = ("未设置提现方案。");

                }
                if (chkResult == 4)
                {
                    message = ("不能低于最小允许金额。");

                }
                if (chkResult == 5)
                {
                    message = ("不能超过最大允许金额。");

                }
                if (chkResult == 6)
                {
                    message = ("提现金额超过余额。");
                }
                else if (chkResult == 99)
                {
                    message = ("未知错误。");
                }

                if (!string.IsNullOrEmpty(message))
                {
                    model.status = 3;
                    model.success = 4;
                    model.audit_status = 3;
                    model.auditTime = DateTime.Now;
                    model.auditUser = UserId;
                    model.auditUserName = "system";
                    model.remark = message;

                    foreach (WithdrawAgent item in list)
                    {
                        item.is_cancel = true;
                        item.remark = message;
                    }
                }
                else
                {
                    foreach (WithdrawAgent item in list)
                    {
                        if (item.is_cancel == false)
                            success++;
                    }
                    if (success == 0)
                    {
                        model.status = 3;
                        model.success = 4;
                    }
                }

                bool result = _bll.Insert(model, list);
                if (result == false)
                {

                    AlertAndRedirect("上传出错。");
                    return;
                }

                else
                {
                    //走接口
                    #region  走接口
                    if (scheme.vaiInterface == 1 && scheme.tranRequiredAudit == 0)
                    {
                        foreach (WithdrawAgent item in list)
                        {
                            if (item.audit_status == 2)
                            {
                                //viviapi.ETAPI.Withdraw.InitDistribution2(item);
                            }
                        }
                    }
                    #endregion
                }
                AlertAndRedirect("上传成功", "importlist.aspx");
            }
            catch (Exception ex)
            {
                AlertAndRedirect(ex.Message);
            }
        }
        #endregion
    }
}
