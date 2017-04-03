using System;
using System.Web;

namespace viviapi.SysInterface.Bank.MyAPI
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class BankInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BankInfo(HttpContext context)
        {
            type = Utility.GetQueryString(context, "type");
            parter = Utility.GetQueryString(context, "parter");
            
            
            value = Utility.GetQueryString(context, "value");
            
            orderid = Utility.GetQueryString(context, "orderid");
            callbackurl = Utility.GetQueryString(context, "callbackurl");
            hrefbackurl = Utility.GetQueryString(context, "hrefbackurl");
            sign = Utility.GetQueryString(context, "sign");
            attach = Utility.GetQueryString(context, "attach");
            agent = Utility.GetQueryString(context, "agent");
            payerIp = Utility.GetQueryString(context, "payerIp");

            ErrCode = "";
            Msg = "";
            Version = MyAPI.Utility.EnName;
        }

        public string type { get; set; }
        public string parter { get; set; }
        public string hrefbackurl { get; set; }
       
        public string value { get; set; }
       
        public string orderid { get; set; }
        public string callbackurl { get; set; }
        public string sign { get; set; }
        public string attach { get; set; }
        public string agent { get; set; }
        public string payerIp { get; set; }

        public int UserId { get; set; }
        //public UserInfo User { get; set; }
        public string APIkey { get; set; }
        public int TypeId { get; set; }
        public int ManageId { get; set; }
        public string ChanelNo { get; set; }
        public int SupplierId { get; set; }
        public decimal OrderAmt { get; set; }
     
        public string Version { get; set; }

        public string ErrCode { get; set; }
        public string Msg { get; set; }
    }
}
