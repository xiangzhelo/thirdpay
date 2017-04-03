using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Demo.Class
{
    public class ProperConst
    {
        /*
            此处用户自己填写
         */
        /// <summary>
        /// 商户号:中联支付平台分配
        /// </summary>
        public const string merchantCode = "1000000183";
        //public const string merchantCode = "1000000001";
        /// <summary>
        /// 商户私钥：商户按照指定格式自己产生一组RSA密钥，私钥商户自己保存并用于商户签名，并将该密钥的公钥发给支付平台用作验签
        /// </summary>
        public const string merPriRsaKey = "46740daa9faa72d9fad56dc34c230401";
        //public const string merPriRsaKey = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAOOIBvaPFQhQ3Mpl8MdOZjDDajADCWxEXP/lXSonV5U04A01XGNNs5yCBxMJbjKS1kmKmQmChkAfmadNlpKmVtc7qsdZq2K90Eg0/9T4cjn/cdKDc/zR0bMwn2NdfVe5Y3uxWB3eRqLTE1TTAXzzLnPm1Ls0uLdFh7PfPnxqCj6DAgMBAAECgYAfDwh0S5/BXNhmwHeXnToR2fr6xs9YehR/0d1fzbME6QzUgL41x/uGl7FDhfwG50hdDZBKXgjZY/bjgZHWPuKHiO390C659Ioy3FmRAk++eoFdTGU46SsBQrMDwqu1AEcxHj0GrP3wSZ7PNOTY6eZb4v/XtlhdRwRcrIyAguKoUQJBAPmtsGOYEHPdCLj5AjbpecUyXWqNn2USwXly4HDe4b8+HLjbqfeeBGAQVuaL14k3LlIVCI7jBJaxqDkfvlyUSW0CQQDpSso1L+8DCPTH2OJToGwla3mC0mklb9ZAo0//Hsj24NLwhtfFVPdJULCN2mFmjYM9E6Mh6YLavlw0tZOL6SGvAkAm8mgUcREH8c+9guJMjIj5MM0PpP3bN1zExB2snafbPCYg0+skfBq0nXfgyKmbducb2LoYB+OcWiQinQgFyv/VAkBs8Ov0YmnutOP53yHxg1x9LO8VVESdotgeXyUgMbQO9XYLtCxWjhLcPb30wCHzzemXP/BSCcV9eJ9+TbyU/U0pAkEA4DShPeSvije4tiQIJ+R/4sn3S/euYehCLzatMRdYfCC+wV/TPUTR/2ZtLJt5XC5AqwKll8aASn0tjPhiwIwA4Q==";
        /// <summary>
        /// 支付平台测试公网地址
        /// </summary>
        public const string payUrl = "https://payment.kklpay.com/ebank/pay.do";
        /// <summary>
        /// 支付平台测试公网订单查询地址
        /// </summary>
        public const string queryUrl = "https://payment.kklpay.com/return/return.do";
        /// <summary>
        /// 支付平台测试公网订单退货地址
        /// </summary>
        public const string returnUrl = "https://payment.kklpay.com/queryOrder.do";
        /// <summary>
        /// 支付成功后通知商户页面地址(同步)--此地址具体配置访问信息由客户端决定，Demo目录随用户服务器配置可能发生变化{IP处修改为客户服务器IP或域名信息}
        /// </summary>
        public const string merUrl = "http://192.168.1.100:8081/Demo/Rec.aspx";
        /// <summary>
        /// 支付成功后通知商户服务器地址(异步)--此地址具体配置访问信息由客户端决定，Demo目录随用户服务器配置可能发生变化{IP处修改为客户服务器IP或域名信息}
        /// </summary>
        public const string noticeUrl = "http://192.168.1.100:8081/Demo/SynNotice.aspx";
        /// <summary>
        /// 尾字符Key定义
        /// </summary>
        public const string LastChar = "KEY";
        /// <summary>
        /// 关键KEY串
        /// </summary>
        public const string Key = "&" + LastChar + "=" + merPriRsaKey;
        /// <summary>
        /// project_id值
        /// </summary>
        public const string project_idValue = "test";
        /// <summary>
        /// project_id标志
        /// </summary>
        public const string project_id = "project_id";


        /*****************************************************
         ********************支付类参数信息*******************
         *****************************************************/
        /// <summary>
        /// 支付类参数
        /// </summary>
        public string[] PayParam ={"merchantCode", "outOrderId"
                                                    , "totalAmount", "goodsName"
                                                    , "goodsExplain", "orderCreateTime"
                                                    , "lastPayTime"//, "autoJump", "waitTime"
                                                    , "merUrl"
                                                    , "noticeUrl"//, "bankInput"
                                                    , "bankCode", "bankCardType"};
        /// <summary>
        /// 支付类签名参数
        /// </summary>
        public string[] PayRSAParam ={"merchantCode", "outOrderId"
                                                    , "totalAmount","orderCreateTime"
                                                    , "lastPayTime"};


        /*************************************************************************
         ********************支付成功后调用客户服务器参数信息*******************
         ************************************************************************/
        /// <summary>
        /// 服务端支付通知调用客户服务器页面传值报文
        /// </summary>
        public string[] NoticeParam ={"merchantCode", "instructCode"
                                    , "transType", "outOrderId"
                                    , "transTime", "totalAmount"};
        /// <summary>
        /// 服务端支付通知调用客户服务器页面传值报文加密参数
        /// </summary>
        public string[] NoticeParamRSA ={"merchantCode", "instructCode"
                                    , "transType", "outOrderId"
                                    , "transTime", "totalAmount"};
        /// <summary>
        /// 服务端支付通知调用客户服务器页面传值，客户端服务器处理结果成功OR失败，返回结果给服务器的参数，并且都参与加密
        /// </summary>
        public string[] NoticeReTurnServerParam ={"code", "msg"};


        /*****************************************************
         ********************订单查询类参数信息*******************
         *****************************************************/
        /// <summary>
        /// 对服务端进行订单查询传值参数,都进行加密传输
        /// </summary>
        public string[] QueryParam = { "merchantCode", "outOrderId" };
        /// <summary>
        /// 对服务端订单查询后返回参数
        /// </summary>
        public string[] QueryReturnParam = { "merchantCode", "outOrderId" 
                                           , "transTime", "transType"
                                           , "instructCode","amount"
                                           ,"replyCode","sign"};
        /// <summary>
        /// 对服务端订单查询后返回参数--参与报文加密的参数
        /// </summary>
        public string[] QueryRetuenMD5Param = { "merchantCode", "outOrderId" 
                                           ,"amount","replyCode"};


        /*****************************************************
         ********************退货类参数信息*******************
         *****************************************************/
        /// <summary>
        /// 对服务端订单退货传值报文
        /// </summary>
        public string[] ReOrderParam ={"merchantCode", "oriInstructCode"
                                                    , "amount"};
        /// <summary>
        /// 对服务端订单退货传值报文进行加密传输参数
        /// </summary>
        public string[] ReOrderParamRSA ={"merchantCode", "oriInstructCode"
                                                    , "amount"};
        /// <summary>
        /// 对服务端订单退货，服务器返回客户端信息
        /// </summary>
        public string[] ReOrderReturnParam ={"merchantCode", "oriInstructCode"
                                             ,"returnInstructCode","returnTransTime"
                                             , "amount", "sign"};
        /// <summary>
        /// 对服务端订单退货，服务器返回客户端加密参数
        /// </summary>
        public string[] ReOrderReturnRSAParam ={"merchantCode", "oriInstructCode"
                                             ,"returnInstructCode","returnTransTime"
                                             , "amount"};
    }
}