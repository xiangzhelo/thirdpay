using System;
using System.Globalization;
using viviapi.Model;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console
{
    /// <summary>
    /// 是否添加备用通道
    /// </summary>
    public partial class SysMailConfig : ManagePageBase
    {
        readonly viviapi.BLL.Sys.SysMailConfig _bllConfig = new viviapi.BLL.Sys.SysMailConfig();

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        private viviapi.Model.Sys.SysMailConfig _mailConfig = null;
        public viviapi.Model.Sys.SysMailConfig MailConfig
        {
            get
            {
                if (_mailConfig != null) return _mailConfig;

                if (ItemInfoId > 0)
                {
                    _mailConfig = _bllConfig.GetModel(ItemInfoId);
                }
                if (_mailConfig==null)
                    _mailConfig=new viviapi.Model.Sys.SysMailConfig();

                return _mailConfig;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();

            if (!this.IsPostBack)
            {
                if (MailConfig != null)
                {
                    txtEmailServerAddress.Text = MailConfig.host;
                    txtEmailServerAddressPort.Text = MailConfig.port.ToString(CultureInfo.InvariantCulture);
                    txtServerUserName.Text = MailConfig.username;
                    txtServerUserPass.Text = MailConfig.password;
                    txtaddress.Text = MailConfig.address;
                    txtMailDisplayName.Text = MailConfig.displayName;

                    ckb_ssl.Checked = MailConfig.enableSsl == 1;
                    ckb_useDefaultCredentials.Checked = (MailConfig.useDefaultCredentials == 1);
                    ckb_used.Checked = MailConfig.used == 1 ? true : false;
                }
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.System);

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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var mailConfig = MailConfig ?? new viviapi.Model.Sys.SysMailConfig();

            mailConfig.used = 1;
            mailConfig.host = txtEmailServerAddress.Text;
            mailConfig.port = int.Parse(txtEmailServerAddressPort.Text);
            mailConfig.username = txtServerUserName.Text;
            mailConfig.password = txtServerUserPass.Text;
            mailConfig.address = txtaddress.Text;
            mailConfig.displayName = txtMailDisplayName.Text;
            mailConfig.enableSsl = (byte)(ckb_ssl.Checked ? 1 : 0);
            mailConfig.useDefaultCredentials = (byte)(ckb_useDefaultCredentials.Checked ? 1 : 0);
            mailConfig.used = (byte)(ckb_used.Checked ? 1 : 0);

            int id = _bllConfig.Insert(mailConfig);

            if (id > 0)
            {
                ShowMessageBox("恭喜，设置成功!");
            }
            else
            {
                ShowMessageBox("操作失败!");
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string message;

            var emailcom = new EmailSender(txtmailto.Text.Trim()
                      , txtmailto.Text.Trim() + "邮件测试"
                      , this.txtContent.Text.Trim()
                      , true
                      , System.Text.Encoding.GetEncoding("gbk"));
            emailcom.MailConfig = MailConfig;

            if (emailcom.Send())
            {
                message = "发送成功";
            }
            else
            {
                message = "发送失败";
            }
            ShowMessageBox(message);
        }

        protected void btnSend2_Click(object sender, EventArgs e)
        {
            string message;

            var emailcom = new EmailSender(txtmailto.Text.Trim()
                      , txtmailto.Text.Trim() + "邮件测试"
                      , this.txtContent.Text.Trim()
                      , true
                      , System.Text.Encoding.GetEncoding("gbk"));

            emailcom.MailConfig = MailConfig;

            if (emailcom.Send2())
            {
                message = "提交成功";
            }
            else
            {
                message = "提交失败";
            }
            ShowMessageBox(message);
        }
    }
}