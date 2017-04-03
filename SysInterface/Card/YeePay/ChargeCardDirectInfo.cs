using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using viviapi.Model.User;

namespace viviapi.SysInterface.Card.YeePay
{
    /// <summary>
    /// 非银行卡专业版（组合支付）支付请求参数列表
    /// </summary>
    [Serializable]
    public class ChargeCardDirectInfo
    {
        #region ChargeCardDirectInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ChargeCardDirectInfo(HttpContext context)
        {
            //string parmlist = viviLib.Web.WebClientHelper.FormatRequestData(context.Request.Form, System.Text.Encoding.GetEncoding("GB2312"));

            //viviLib.Logging.LogHelper.Debug("RawUrl:" + context.Request.RawUrl);

            //viviLib.Logging.LogHelper.Debug("parmlist:" + parmlist);

            p0_Cmd  = Common.GetQueryString(context, "p0_Cmd");

            p1_MerId = Common.GetQueryString(context, "p1_MerId");

            p2_Order = Common.GetQueryString(context, "p2_Order");

            p3_Amt = Common.GetQueryString(context, "p3_Amt");

            p4_verifyAmt = Common.GetQueryString(context, "p4_verifyAmt");
            p5_Pid = Common.GetQueryString(context, "p5_Pid");
            p6_Pcat = Common.GetQueryString(context, "p6_Pcat");
            p7_Pdesc = Common.GetQueryString(context, "p7_Pdesc");

            p8_Url = Common.GetQueryString(context, "p8_Url");

            pa_MP = Common.GetQueryString(context, "pa_MP");

            pa7_cardAmt = Common.GetQueryString(context, "pa7_cardAmt");

            pa8_cardNo = Common.GetQueryString(context, "pa8_cardNo");
            pa9_cardPwd = Common.GetQueryString(context, "pa9_cardPwd");

            pd_FrpId = Common.GetQueryString(context, "pd_FrpId");

            pr_NeedResponse = Common.GetQueryString(context, "pr_NeedResponse");

            pz_userId = Common.GetQueryString(context, "pz_userId");
            pz1_userRegTime = Common.GetQueryString(context, "pz1_userRegTime"); 

            hmac = Common.GetQueryString(context, "hmac");

            Msg = "";
        }
        #endregion

        /// <summary>
        /// ChargeCardDirect
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
        /// 值：true校验金额;  false不校验金额，
        /// </summary>
        public string p4_verifyAmt { get; set; }

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
        /// 商户扩展信息
        /// </summary>
        public string pa_MP { get; set; }

        /// <summary>
        /// 3 DES 加密
        /// </summary>
        public string pa7_cardAmt { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string pa8_cardNo { get; set; }

        /// <summary>
        /// 卡密
        /// </summary>
        public string pa9_cardPwd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pd_FrpId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pr_NeedResponse { get; set; }

        public string pz_userId { get; set; }

        public string pz1_userRegTime { get; set; }

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
        public decimal OrderAmt { get; set; }

        public int CardNum { get; set; }
        public string[] CardAmts { get; set; }
        public decimal[] CardFaceValues { get; set; }
        public string[] CardNos{ get; set; }
        public string[] CardPwds { get; set; }
        
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
