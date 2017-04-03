using System;
using System.Text;
using System.Web;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviLib.Security;
using System.Collections.Generic;

namespace viviapi.SysInterface.Card.MyAPI
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CardInfo
    {
        public CardInfo()
        {
 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CardInfo(HttpContext context)
        {
            type = Utility.GetQueryString(context, "type");
            parter = Utility.GetQueryString(context, "parter");
            cardno = Utility.GetQueryString(context, "cardno");
            cardpwd = Utility.GetQueryString(context, "cardpwd");
            value = Utility.GetQueryString(context, "value");
            restrict = Utility.GetQueryString(context, "restrict");
            orderid = Utility.GetQueryString(context, "orderid");
            callbackurl = Utility.GetQueryString(context, "callbackurl");
            sign = Utility.GetQueryString(context, "sign");
            attach = Utility.GetQueryString(context, "attach");
            agent = Utility.GetQueryString(context, "agent");
            //totalvalue = Utility.GetQueryString(context, "totalvalue");//新加
            //if (cardno.IndexOf(';') > 0)
            //{
            //    cardnoList=cardno.Split(';');
            //}
            //if (cardpwd.IndexOf(';') > 0)
            //{
            //    cardpwdList = cardpwd.Split(';');
            //}
            Msg = "";
            Version = Utility.EnName;
            ProcessMode = 1;
        }

        public string type { get; set; }
        public string parter { get; set; }
        public string cardno { get; set; }
        public string cardpwd { get; set; }
        public string value { get; set; }
        public string restrict { get; set; }
        public string orderid { get; set; }
        public string callbackurl { get; set; }
        public string sign { get; set; }
        public string attach { get; set; }
        public string agent { get; set; }
        //public string totalvalue { get; set; }//新加
        //public string[] cardnoList { get; set; }
        //public string[] cardpwdList { get; set; }

        public int UserId { get; set; }
        //public UserInfo User { get; set; }
        public string APIkey { get; set; }
        public int TypeId { get; set; }
        public int ManageId { get; set; }
        public string ChanelNo { get; set; }
        public int SupplierId { get; set; }
        public int CardType { get; set; }
        public int OrderAmt { get; set; }
        public string CardNo { get; set; }
        public string CardPwd { get; set; }
        //public List<string> CardNoList { get; set; }
        //public List<string> CardPwdList { get; set; }

        public string Version { get; set; }
        /// <summary>
        /// 处理方式
        /// 1 通过接口
        /// 2 自处理
        /// </summary>
        public byte ProcessMode { get; set; }
        public string Msg { get; set; }
    }
}
