using System;
using System.Web;

namespace viviapi.SysInterface.Card.Card70
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CardInfo
    {
        public CardInfo()
        {
            Msg = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CardInfo(HttpContext context)
        {
            userid = Utility.GetQueryString(context, "userid");
            orderid = Utility.GetQueryString(context, "orderid");
            typeid = Utility.GetQueryString(context, "typeid");
            productid = Utility.GetQueryString(context, "productid");
            cardno = Utility.GetQueryString(context, "cardno");
            cardpwd = Utility.GetQueryString(context, "cardpwd");
            money = Utility.GetQueryString(context, "money");
            url = Utility.GetQueryString(context, "url");
            sign = Utility.GetQueryString(context, "sign");
            ext = Utility.GetQueryString(context, "ext");
            agent = Utility.GetQueryString(context, "agent");

            Msg = "";
            Version = Utility.EnName;
        }

        public string userid { get; set; }
        public string orderid { get; set; }
        public string typeid { get; set; }
        public string productid { get; set; }
        public string cardno { get; set; }
        public string cardpwd { get; set; }
        public string money { get; set; }


        public string url { get; set; }
        public string sign { get; set; }
        public string ext { get; set; }
        public string agent { get; set; }


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
