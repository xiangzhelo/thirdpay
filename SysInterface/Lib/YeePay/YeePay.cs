using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.SysInterface.Lib.YeePay
{
    /// <summary>
    /// 
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// 检查
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="p2_Order"></param>
        /// <param name="p3_Amt"></param>
        /// <param name="p4_Cur"></param>
        /// <param name="p5_Pid"></param>
        /// <param name="p6_Pcat"></param>
        /// <param name="p7_Pdesc"></param>
        /// <param name="p8_Url"></param>
        /// <param name="p9_SAF"></param>
        /// <param name="pa_MP"></param>
        /// <param name="pd_FrpId"></param>
        /// <param name="pr_NeedRespone"></param>
        /// <param name="keyValue"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool CheckSign(string merchantId
            , string p2_Order
            , string p3_Amt
            , string p4_Cur
            , string p5_Pid
            , string p6_Pcat
            , string p7_Pdesc
            , string p8_Url
            , string p9_SAF
            , string pa_MP
            , string pd_FrpId
            , string pr_NeedRespone
            , string keyValue
            , string sign)
        {
            string sbOld = "";

            sbOld += "Buy";
            sbOld += merchantId;
            sbOld += p2_Order;
            sbOld += p3_Amt;
            sbOld += p4_Cur;

            sbOld += p5_Pid;
            sbOld += p6_Pcat;
            sbOld += p7_Pdesc;
            sbOld += p8_Url;
            sbOld += p9_SAF;

            sbOld += pa_MP;
            sbOld += pd_FrpId;
            sbOld += pr_NeedRespone;

            string hmac = Digest.HmacSign(sbOld, keyValue);
            return sign == hmac;
        }
    }
}
