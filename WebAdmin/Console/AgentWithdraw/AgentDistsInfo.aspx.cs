using System;
using viviapi.BLL.Finance.Agent;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.AgentWithdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AgentDistsInfo : ManagePageBase
    {
        protected WithdrawAgent bll = new WithdrawAgent();

        public int id
        {
            get
            {
                return WebBase.GetQueryStringInt32("id",0);
            }
        }

        viviapi.Model.Finance.Agent.WithdrawAgent _model = null;
        public viviapi.Model.Finance.Agent.WithdrawAgent model
        {
            get
            {
                if (_model == null && id > 0)
                {
                    _model = bll.GetModel(id);
                }
                return _model;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
       , ManageRole.Orders);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        #region ShowInfo
        /// <summary>
        /// 
        /// </summary>
        private void ShowInfo()
        {
            if (model != null)
            {
                this.lblid.Text = model.id.ToString();
                this.lblmode.Text = bll.GetModeText(model.mode);
                this.lbltrade_no.Text = model.trade_no;
                this.lblout_trade_no.Text = model.out_trade_no;
                this.lblservice.Text = model.service;
                this.lbluserid.Text = model.userid.ToString();
                this.lblsign_type.Text = model.sign_type;
                this.lblreturn_url.Text = model.return_url;
                this.lblbankCode.Text = model.bankCode;
                this.lblbankName.Text = model.bankName;
                this.lblbankBranch.Text = model.bankBranch;
                this.lblbankAccountName.Text = model.bankAccountName;
                this.lblbankAccount.Text = model.bankAccount;
                this.lblamount.Text = model.amount.ToString();
                this.lblcharge.Text = model.charge.ToString();
                this.lbladdTime.Text = model.addTime.ToString();
                this.lblprocessingTime.Text = model.processingTime.ToString();
                this.lblaudit_status.Text = bll.GetAuditStatusText(model.audit_status);
                this.lblpayment_status.Text = bll.GetPaymentStatusText(model.payment_status);
                this.lblis_cancel.Text = model.is_cancel ? "是" : "否";
                //this.lblext1.Text = model.ext1;
                //this.lblext2.Text = model.ext2;
                //this.lblext3.Text = model.ext3;
                this.lblremark.Text = model.remark;
                this.lbltranApi.Text = model.tranApi.ToString();
                this.lblnotifyTimes.Text = model.notifyTimes.ToString();
                this.lblnotifystatus.Text = model.notifystatus.ToString();
                this.lblcallbackText.Text = model.callbackText;

                this.lbllotno.Text = model.lotno;
                this.lblserial.Text = model.serial.ToString();



                if (model.is_cancel == false)
                {

                    btn_cancel.Visible = (model.issure == 1) && (model.audit_status == 1);

                    btnAudits.Visible = (model.issure == 1) && (model.audit_status == 1);
                    btnRefuse.Visible = (model.issure == 1) && (model.audit_status == 1);

                    if (model.audit_status == 2)
                    {
                        btnpaysuccess.Visible = (model.payment_status == 1);
                        btnpayfail.Visible    = (model.payment_status == 1);
                    }
                }
            }
        }
        #endregion

        #region 审核
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudits_Click(object sender, EventArgs e)
        {
            string message = bll.doAudit(model.trade_no,  this.currentManage.id, this.currentManage.username);
            if (message == "审核成功")
            {
                if (_model != null && model.tranApi >0)
                {
                   // viviapi.ETAPI.Common.Withdrawal.InitDistribution2(model);
                }
            }
            AlertAndRedirect(message);
        }
        #endregion

        #region 取消
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            string message = bll.doCancel(model.trade_no);           
            AlertAndRedirect(message);
        }
        #endregion

        #region 拒绝
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefuse_Click(object sender, EventArgs e)
        {
            string message = bll.doRefuse(model.trade_no,this.currentManage.id, this.currentManage.username);
           
            AlertAndRedirect(message);
        }
        #endregion

        #region 付款成功
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnpaysuccess_Click(object sender, EventArgs e)
        {
            string message = bll.PaySuccess(model.trade_no);
          
            AlertAndRedirect(message);
        }
        #endregion

        #region btnpayfail
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnpayfail_Click(object sender, EventArgs e)
        {
            string message = bll.PayFail(model.trade_no);
            
            AlertAndRedirect(message);
        }
        #endregion

        protected void btnreNotify_Click(object sender, EventArgs e)
        {
            bll.DoNotify(model.trade_no);
            AlertAndRedirect("已提交");
        }


    }
}