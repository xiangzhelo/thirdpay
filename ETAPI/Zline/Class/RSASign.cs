using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Security.Cryptography;
using System.Text;
using Demo.Class;//类库包
namespace Demo.Class
{
    public class RSASign
    {
        protected static  ProperConst prper = new ProperConst();
        public RSASign()
        { }
        /// <summary>
        /// 得到签名数据
        /// </summary>
        /// <param name="zifu"></param>
        /// <returns></returns>
        public static string GetMD5RSA(string QueryStr)
        {
            try
            {
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider(); //32位MD5
                byte[] by = UTF8Encoding.UTF8.GetBytes(QueryStr);
                string resultPass = System.Text.UTF8Encoding.Unicode.GetString(by);
                byte[] output = MD5.ComputeHash(by);
                string rs = BitConverter.ToString(output);
                string nn = rs.Replace("-", "");
                return nn;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 反回支付类参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetPayParamSort()
        {
            return prper.PayParam;
        }
        /// <summary>
        /// 反回排序后的支付类签名参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetPayRSAParamSort()
        {
            Array.Sort(prper.PayRSAParam); return prper.PayRSAParam;
        }
        /// <summary>
        /// 反回支付通知报文类参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetNoticeParamSort()
        {
            //Array.Sort(prper.NoticeParam); 
            return prper.NoticeParam;
        }
        /// <summary>
        /// 反回排序后的支付通知报文类参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetNoticeRSAParamSort()
        {
            Array.Sort(prper.NoticeParamRSA); return prper.NoticeParamRSA;
        }
        /// <summary>
        /// 反回查询通知报文类参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetQueryParamSort()
        {
            return prper.QueryParam;
        }
        /// <summary>
        /// 反回排序后的查询通知报文类参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetQueryRSAParamSort()
        {
            Array.Sort(prper.QueryParam); return prper.QueryParam;
        }
        /// <summary>
        /// 反回服务器查询请求返回参数类参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetQueryReturnParamSort()
        {
            return prper.QueryReturnParam;
        }
        /// <summary>
        /// 反回服务器查询请求返回参数类RSA参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetQueryRetuenMD5ParamSort()
        {
            return prper.QueryRetuenMD5Param;
        }
        /// <summary>
        /// 反回排序后服务器查询请求返回RSA参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetQueryRetuenMD5RSAParamSort()
        {
            Array.Sort(prper.QueryRetuenMD5Param); return prper.QueryRetuenMD5Param;
        }

        /// <summary>
        /// 反回退货报文类参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetReOrderParamSort()
        {
            return prper.ReOrderParam;
        }
        /// <summary>
        /// 反回排序后的退货报文类参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetReOrderRSAParamSort()
        {
            Array.Sort(prper.ReOrderParamRSA); return prper.ReOrderParamRSA;
        }

        /// <summary>
        /// 反回退货报文类服务器返回参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetReOrderServerParamSort()
        {
            return prper.ReOrderReturnParam;
        }
        /// <summary>
        /// 反回排序后的退货报文类服务器返回需要签名参数数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetReOrderServerRSAParamSort()
        {
            Array.Sort(prper.ReOrderReturnRSAParam); return prper.ReOrderReturnRSAParam;
        }
    }
}
