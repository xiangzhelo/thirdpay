using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviLib.Web;
using viviapi.BLL.Sys;
using viviLib;

namespace viviapi.BLL.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public class SMS
    {
        /// <summary>
        /// �����ֻ�����
        /// </summary>
        /// <param name="mobile">�ֻ�����</param>
        /// <param name="content">����</param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static bool Send(string mobile, string content, string ext)
        {
            //string sn = viviapi.SysConfig.RuntimeSetting.SMSSN;
            //string pwd = viviapi.SysConfig.RuntimeSetting.SMSKEY;
            string sn = SMSTempSettings.SMS_SN;
            string pwd = SMSTempSettings.SMS_KEY;
            string sms_website = SMSTempSettings.SMS_WebSite;
            string encodecontent = HttpUtility.UrlEncode(content, Encoding.GetEncoding("gb2312"));
            string postData = string.Format("Uid={0}&Key={1}&smsMob={2}&smsText={3}&ext={4}" , sn, pwd, mobile, encodecontent, ext);
            //string postData = string.Format("sn={0}&pwd={1}&mobile={2}&content={3}&ext={4}", sn, pwd, mobile, encodecontent, ext);
            string returnStr = WebClientHelper.GetString(sms_website, postData, "get", Encoding.GetEncoding("gb2312"), 10000);
            //viviLib.Logging.LogHelper.Write(returnStr);
            return (returnStr == "1");
        }

        public static string SendSmsWithCheck(string mobile, string content, string ext)
        {
            string sn = SMSTempSettings.SMS_SN;
            string pwd = SMSTempSettings.SMS_KEY;
            int maxSendTimes = BLL.Sys.RegisterSettings.SmsMaxSendTimes;

            return SendSmsWithCheck(sn, pwd, maxSendTimes, mobile, content, ext);
        }

        /// <summary>
        /// 0 �ɹ�
        /// 1 ��������ȷ���ֻ�����
        /// 2 �ֻ��Ѵﵽ�������
        /// 3 ������󣬷���ʧ��
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="pwd"></param>
        /// <param name="maxSendTimes"></param>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string SendSmsWithCheck(string sn, string pwd, int maxSendTimes, string mobile, string content, string ext)
        {
            string result = string.Empty;
            if (!viviLib.Text.PageValidate.IsMobile(mobile))
            {
                result = "��������ֻ����벻��ȷ�����������롣";
            }
            else
            {
                bool isAllowed = false;
                
                bool isLimited = PhoneValidFactory.isLimited(mobile);

                if (!isLimited)
                {
                    isAllowed = true;
                }
                else
                {
                    int sendedCount = PhoneValidFactory.SendCount(mobile);
                    if (sendedCount < maxSendTimes)
                    {
                        isAllowed = true;
                    }
                }

                if (!isAllowed)
                {
                    result = "��Ǹ����������ֻ����ʹ����Ѵﵽ������������";
                }
                else
                {
                    viviLib.Logging.LogHelper.Write(content);
                    string encodecontent = HttpUtility.UrlEncode(content, Encoding.GetEncoding("gb2312"));
                    if (SendtoSupp(1, sn, pwd, mobile, encodecontent, ext))
                    {
                        var validlog = new Model.PhoneValidLog
                        {
                            phone = mobile,
                            sendTime = DateTime.Now,
                            code = content,
                            clientIP = ServerVariables.TrueIP
                        };

                        BLL.PhoneValidFactory.Add(validlog);
                    }
                    else
                    {
                        result = "��֤�뷢��ʧ�ܣ�����ϵ����Ա��";
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="suppid"></param>
        /// <param name="paramlist"></param>
        /// <returns></returns>
        public static bool SendtoSupp(int suppid, params object[] paramlist)
        {
            //string postData = string.Format("sn={0}&pwd={1}&mobile={2}&content={3}&ext={4}", paramlist);
           // string postData = string.Format("sn={0}&pwd={1}&mobile={2}&content={3}&ext={4}", paramlist);
            string postData = string.Format("Uid={0}&Key={1}&smsMob={2}&smsText={3}", paramlist);
            if (suppid == 1)
            {
                string sms_website = SMSTempSettings.SMS_WebSite;
                string returncode = SMSTempSettings.SMS_SendSuccessCode;
                string returnStr = WebClientHelper.GetString(sms_website, postData, "get", Encoding.GetEncoding("gb2312"), 10000);
                viviLib.Logging.LogHelper.Write(returnStr);
                return (returnStr == returncode);
            }
            else
                return false;

        }
        /// <summary>
        /// �����ֻ�����
        /// </summary>
        /// <param name="mobile">�ֻ�����</param>
        /// <param name="content">����</param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static void Sendy(string mobile, string content, string ext)
        {
            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            //req.Extend = "123456";
            //req.SmsType = "normal";
            //req.SmsFreeSignName = "�������";
            //req.SmsParam = "{\"code\":\"1234\",\"product\":\"alidayu\"}";
            //req.RecNum = "13000000000";
            //req.SmsTemplateCode = "SMS_585014";
            //AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
            //Console.WriteLine(rsp.Body);
        }
    }
}