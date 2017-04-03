using System;

namespace viviapi.ETAPI.YeePay
{
    public class Bank
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymodeId"></param>
        /// <returns></returns>
        public static string GetBankCode(string paymodeId)
        {
            string code = "";
            switch (paymodeId)
            {
                case "970":
                    code = "CMBCHINA-NET-B2C"; //招商银行
                    break;
                case "967":
                    code = "ICBC-NET-B2C"; //中国工商银行
                    break;
                case "964":
                    code = "ABC-NET-B2C"; //中国农业银行
                    break;
                case "965":
                    code = "CCB-NET-B2C"; //中国建设银行
                    break;
                case "963":
                    code = "BOC-NET-B2C"; //中国银行
                    break;
                case "981":
                    code = "BOCO-NET-B2C"; //中国交通银行
                    break;
                case "980":
                    code = "CMBC-NET-B2C"; //中国民生银行
                    break;
                case "974":
                    code = "SDB-NET-B2C"; //深圳发展银行
                    break;
                case "985":
                    code = "GDB-NET-B2C"; //广东发展银行
                    break;
                case "962":
                    code = "ECITIC-NET-B2C"; //中信银行
                    break;
                case "982":
                    code = "HXB-NET-B2C"; //华夏银行
                    break;
                case "972":
                    code = "CIB-NET-B2C"; //兴业银行
                    break;
                case "971":
                    code = "POST-NET-B2C"; //中国邮政
                    break;
                case "989":
                    code = "BCCB-NET-B2C"; //北京银行
                    break;
                case "988":
                    code = "CBHB-NET-B2C"; //渤海银行
                    break;
                case "990":
                    code = "BJRCB-NET-B2C"; //北京农商银行
                    break;
                case "979":
                    code = "NJCB-NET-B2C"; //南京银行
                    break;
                case "986":
                    code = "CEB-NET-B2C"; //中国光大银行
                    break;
                case "987":
                    code = "HKBEA-NET-B2C"; //东亚银行
                    break;
                case "997":
                    code = "NBCB-NET-B2C"; //宁波银行
                    break;
                case "978":
                    code = "PINGANBANK-NET"; //平安银行
                    break;
                case "968":
                    code = "CZ-NET-B2C"; //浙商银行
                    break;
                case "975":
                    code = "SHB-NET-B2C"; //上海银行
                    break;
                case "977":
                    code = "SPDB-NET-B2C"; //上海银行
                    break;
            }
            return code;
        }
        public static string GetBankCode(BankTypeEnum banktype)
        {
            switch (banktype.ToString())
            {
                case "所有银行":
                    return "";

                case "易宝会员支付":
                    return "1000000-NET";

                case "中国农业银行":
                    return "ABC-NET";

                case "北京银行":
                    return "BCCB-NET";

                case "交通银行":
                    return "BOCO-NET";

                case "建设银行":
                    return "CCB-NET";

                case "兴业银行":
                    return "CIB-NET";

                case "招商银行":
                    return "CMBCHINA-NET";

                case "中国民生银行总行":
                    return "CMBC-NET";

                case "光大银行":
                    return "CEB-NET";

                case "中国银行":
                    return "BOC-NET";

                case "中信银行":
                    return "ECITIC-NET";

                case "中国工商银行":
                    return "ICBC-NET";

                case "上海浦东发展银行":
                    return "SPDB-NET";

                case "深圳发展银行":
                    return "SDB-NET";

                case "广东发展银行":
                    return "GDB-NET";

                case "中国邮政":
                    return "POST-NET";

                case "北京农村商业银行":
                    return "BJRCB-NET";

                case "华夏银行":
                    return "HXB-NET";

                case "广州市农信社":
                    return "GNXS-NET";

                case "广州市商业银行":
                    return "GZCB-NET";

                case "顺德农信社":
                    return "SDE-NET";

                case "海农村商业银行":
                    return "SHRCB-NET";

                case "骏网一卡通":
                    return "JUNNET-NET";

                case "联华OK卡":
                    return "LIANHUAOKCARD-NET";

                case "电信聚信卡":
                    return "SHTEL-NET";

                case "盛大卡":
                    return "SNDACARD-NET";

                case "神州行标准版网关":
                    return "SZX-NET";

                case "征途卡":
                    return "ZHENGTU-NET";
            }
            return "";
        }

        public enum BankTypeEnum
        {
            所有银行,
            易宝会员支付,
            中国农业银行,
            北京银行,
            交通银行,
            建设银行,
            兴业银行,
            招商银行,
            中国民生银行总行,
            光大银行,
            中国银行,
            中信银行,
            中国工商银行,
            上海浦东发展银行,
            深圳发展银行,
            广东发展银行,
            中国邮政,
            北京农村商业银行,
            华夏银行,
            广州市农信社,
            广州市商业银行,
            顺德农信社,
            上海农村商业银行,
            骏网一卡通,
            联华OK卡,
            电信聚信卡,
            盛大卡,
            神州行标准版网关,
            征途卡
        }
    }
}

