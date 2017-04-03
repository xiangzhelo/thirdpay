using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using viviapi.BLL.Sys;
using viviLib.ExceptionHandling;

namespace viviapi.WebComponents
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailHelper
    {
        viviapi.BLL.Sys.SysMailConfig config = new SysMailConfig();

        private EmailSender _sender = null;

        public EmailHelper(string to, string subject, string body, bool isHtml, Encoding encoding)
        {
            var sysMailInfo = config.GetDefaultByCache();
            if (sysMailInfo != null)
            {
                _sender = new EmailSender(to, subject, body, isHtml, encoding);
                _sender.MailConfig = sysMailInfo;
            }
        }

        public bool Send()
        {
            if (_sender != null)
            {
                return _sender.Send();
            }
            return false;
        }

        public bool Send2()
        {
            if (_sender != null)
            {
                return _sender.Send2();
            }
            return false;
        }


    }
}
