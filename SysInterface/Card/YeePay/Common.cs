using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace viviapi.SysInterface.Card.YeePay
{
    public class Common
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="strParaName"></param>
        /// <returns></returns>
        public static string GetQueryString(HttpContext context,string strParaName)
        {
            if (context == null)
                return "";

            string value = context.Request[strParaName];

            if (String.IsNullOrEmpty(value))
                return "";


            return HttpUtility.UrlDecode(value, Encoding.GetEncoding("gb2312"));
        }


        #region 通道大类型

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdFrpId"></param>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public static int GetChannelTypeId(string pdFrpId, string cardno)
        {
            int sysChannelTypeId = 0;

            try
            {
                string temp = ConverCardCode(pdFrpId);
                if (String.IsNullOrEmpty(temp))
                    return sysChannelTypeId;

                sysChannelTypeId = Convert.ToInt32(temp);

                if (sysChannelTypeId == 104)
                {
                    if (Card.Utility.IsShengFuTong(cardno))
                    {
                        sysChannelTypeId = 210;
                    }
                }

                return sysChannelTypeId;
            }
            catch
            {

            }
            return sysChannelTypeId;
        }
        #endregion

        #region ConverCardCode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardtype"></param>
        /// <returns></returns>
        public static string ConverCardCode(string cardtype)
        {
            string code = String.Empty;

            switch (cardtype)
            {
                case "SZX":
                    code = "103"; //神州行充值卡
                    break;
                case "SNDACARD":
                    code = "104"; //盛大一卡通
                    break;
                case "ZHENGTU":
                    code = "105"; //征途支付卡
                    break;
                case "JUNNET":
                    code = "106"; //骏网一卡通
                    break;
                case "QQCARD":
                    code = "107"; //腾讯Q币卡
                    break;
                case "UNICOM":
                    code = "108"; //联通充值卡
                    break;
                case "JIUYOU":
                    code = "109"; //久游一卡通
                    break;
                case "NETEASE":
                    code = "110"; //网易一卡通
                    break;
                case "WANMEI":
                    code = "111"; //完美一卡通
                    break;
                case "SOHU":
                    code = "112"; //搜狐一卡通
                    break;
                case "TELECOM":
                    code = "113"; //电信充值卡
                    break;
                case "ZONGYOU":
                    code = "117"; //纵游一卡通
                    break;
                case "TIANXIA":
                    code = "118"; //天下一卡通
                    break;
                case "TIANHONG":
                    code = "119"; //天宏一卡通
                    break;
            }

            return code;
        }
        #endregion

        public static string UrlEncode(string paramValue)
        {
            if (String.IsNullOrEmpty(paramValue))
                return String.Empty;
            return HttpUtility.UrlEncode(paramValue, Encoding.GetEncoding("GB2312"));
        }


        public static string FormatQueryString(string value)
        {
            return HttpUtility.UrlEncode(value, Encoding.GetEncoding("GB2312"));
        }
    }
}
