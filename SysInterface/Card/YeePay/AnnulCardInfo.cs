using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using viviapi.Model.User;

namespace viviapi.SysInterface.Card.YeePay
{
    /// <summary>
    /// 易宝支付产品通用接口 
    /// </summary>
    [Serializable]
    public class AnnulCardInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AnnulCardInfo(HttpContext context)
        {
            //string parmlist = viviLib.Web.WebClientHelper.FormatRequestData(context.Request.Form,System.Text.Encoding.GetEncoding("GB2312"));

            //viviLib.Logging.LogHelper.Debug("RawUrl:" + context.Request.RawUrl);

            //viviLib.Logging.LogHelper.Debug("parmlist:" + parmlist);

            p0_Cmd  = Common.GetQueryString(context, "p0_Cmd");

           
            p1_MerId = Common.GetQueryString(context, "p1_MerId");
            p2_Order = Common.GetQueryString(context, "p2_Order");
            p3_Amt = Common.GetQueryString(context, "p2_Order");

            p4_Cur = Common.GetQueryString(context, "p4_Cur");
            p5_Pid = Common.GetQueryString(context, "p5_Pid");
            p6_Pcat = Common.GetQueryString(context, "p6_Pcat");
            p7_Pdesc = Common.GetQueryString(context, "p7_Pdesc");

            p8_Url = Common.GetQueryString(context, "p8_Url");
            p9_SAF = Common.GetQueryString(context, "p9_SAF");

            pa_MP = Common.GetQueryString(context, "pa_MP");

            pa0_Mode = Common.GetQueryString(context, "pa0_Mode");


            pa7_cardNo = Common.GetQueryString(context, "pa7_cardNo");
            pa8_cardPwd = Common.GetQueryString(context, "pa8_cardPwd");

            pd_FrpId = Common.GetQueryString(context, "pd_FrpId");

            pr_NeedResponse = Common.GetQueryString(context, "pr_NeedResponse");
            hmac = Common.GetQueryString(context, "hmac");

            Msg = "";
        }

        /// <summary>
        /// AnnulCard
        /// </summary>
        public string p0_Cmd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string p1_MerId { get; set; }

        /// <summary>
        /// 商户平台订单号
        /// </summary>
        public string p2_Order { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public string p3_Amt { get; set; }

        /// <summary>
        /// 交易币种,固定值"CNY".
        /// </summary>
        public string p4_Cur { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string p5_Pid { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public string p6_Pcat { get; set; }

        /// <summary>
        ///  产品描述 
        /// </summary>
        public string p7_Pdesc { get; set; }

        /// <summary>
        /// 商户接收支付成功数据的地址,支付成功后易宝支付会向该地址发送两次成功通知.
        /// </summary>
        public string p8_Url { get; set; }

        /// <summary>
        /// 送货地址
        /// 为“1”: 需要用户将送货地址留在易宝支付系统;为“0”: 不需要，默认为 ”0”.
        /// </summary>
        public string p9_SAF { get; set; }

        /// <summary>
        /// 商户扩展信息
        /// </summary>
        public string pa_MP { get; set; }

        /// <summary>
        /// 3 DES 加密
        /// </summary>
        public string pa0_Mode { get; set; }
               

        /// <summary>
        /// 卡号
        /// </summary>
        public string pa7_cardNo { get; set; }

        /// <summary>
        /// 卡密
        /// </summary>
        public string pa8_cardPwd { get; set; }

        /// <summary>
        /// 银行编码
        /// </summary>
        public string pd_FrpId { get; set; }

        /// <summary>
        /// 应答机制
        /// </summary>
        public string pr_NeedResponse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hmac { get; set; }


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
        /// <summary>
        /// 处理方式
        /// 1 通过接口
        /// 2 自处理
        /// </summary>
        public byte ProcessMode { get; set; }
        public string Msg { get; set; }

        ///// <summary>
        ///// 1
        ///// 2
        ///// </summary>
        //public byte Makeup { get; set; }
    }
}
