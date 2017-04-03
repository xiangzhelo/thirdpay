using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviapi.WebComponents
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailSender
    {
        private MailMessage mailMessage;

        private Model.Sys.SysMailConfig _mailConfig = null;
        public Model.Sys.SysMailConfig MailConfig
        {
            get
            {
                return _mailConfig;
            }
            set
            {
                _mailConfig = value;
            }
        }

        public EmailSender(string to, string subject, string body, bool isHtml, Encoding encoding)
        {
            var toaddr = new MailAddress(to);

            this.mailMessage = new MailMessage();

            this.mailMessage.To.Add(toaddr);
            this.mailMessage.Subject = subject;
            this.mailMessage.Body = body;
            this.mailMessage.BodyEncoding = encoding;
            this.mailMessage.IsBodyHtml = isHtml;
            this.mailMessage.SubjectEncoding = encoding;
        }

        public bool Send()
        {
            bool result = false;
            try
            {
                if (MailConfig != null)
                {
                    mailMessage.From = new MailAddress(MailConfig.address, MailConfig.displayName);

                    SmtpClient client = new SmtpClient();
                    client.Host = MailConfig.host;//设置发送者邮箱对应的smtpserver
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(MailConfig.username, MailConfig.password);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = MailConfig.enableSsl > 0;


                    client.Send(this.mailMessage);

                    result = true;
                }
            }
            catch (SmtpException sendexc)
            {
                viviLib.Logging.LogHelper.Write(sendexc.StatusCode.ToString());
                //sendexc.
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return result;
        }

        public bool Send2()
        {
            bool result = false;
            try
            {
                if (MailConfig != null)
                {
                    mailMessage.From = new MailAddress(MailConfig.address, MailConfig.displayName);

                    SmtpClient client = new SmtpClient();
                    client.Host = MailConfig.host;//设置发送者邮箱对应的smtpserver
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(MailConfig.username, MailConfig.password);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = MailConfig.enableSsl > 0;

                    client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
                    client.Send(this.mailMessage);
                    //client.SendAsync(this.mailMessage, null);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return result;
        }

        void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ExceptionHandler.HandleException(e.Error);
            }
            //throw new NotImplementedException();
        }
        
    }
}
