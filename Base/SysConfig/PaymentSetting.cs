using System;
using viviLib.Configuration;
using System.Collections.Generic;

namespace viviapi.SysConfig
{
	/// <summary>
	/// RuntimeSetting 的摘要说明。
	/// </summary>
	public sealed class PaymentSetting
	{
        private PaymentSetting()
		{
		}

        private static readonly string _group = "paymentSettings";
		/// <summary>
		/// 设置组。
		/// </summary>
		public static string SettingGroup
		{
			get{return _group;}
		}

      
        /// <summary>
        /// 
        /// </summary>
        public static bool showjubao
        {
            get
            {
                try
                {
                    string _showjubao = ConfigHelper.GetConfig(SettingGroup, "showjubao");
                    if (!string.IsNullOrEmpty(_showjubao))
                    {
                        return Convert.ToInt32(_showjubao) > 0;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string jumpUrl
        {
            get
            {
                try
                {
                    string _jumpUrl = ConfigHelper.GetConfig(SettingGroup, "jumpUrl");
                    return _jumpUrl;
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string alipay_body
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "alipay_body");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string yeepay_pid
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "yeepay_pid");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public static string yeepay_pcat
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "yeepay_pcat");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public static string yeepay_pdesc
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "yeepay_pdesc");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string tftpay_MerLicences
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "tftpay_MerLicences");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public static string tftpay_TBLicences
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "tftpay_TBLicences");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public static string tftpay_PostAdd
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "tftpay_PostAdd");
            }

        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public static string tftpay_MerBusType
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "tftpay_MerBusType");
            }

        }


        /// <summary>
        /// 
        /// </summary>
        public static string mengsmsarrCom
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "mengsmsarrCom");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string shenzhoufucertificate
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "shenzhoufucertificate");
            }

        }
        
        
        /// <summary>
        /// 
        /// </summary>
        public static string alipay_subject
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "alipay_subject");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string yisheng_buyer_realname
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "yisheng_buyer_realname");
            }

        }


        /// <summary>
        /// 
        /// </summary>
        public static string KuaiQian_prikey_path
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "KuaiQian_prikey_path");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public static string KuaiQian_pubkey_path
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "KuaiQian_pubkey_path");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string Gopay_userType
        {
            get
            {
                string _Gopay_userType = ConfigHelper.GetConfig(SettingGroup, "Gopay_userType");
                if (string.IsNullOrEmpty(_Gopay_userType))
                    _Gopay_userType = "1";
                return _Gopay_userType;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string switch_yeepay_form_url
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "switch_yeepay_form_url");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string switch_sdopay_form_url
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "switch_sdopay_form_url");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string switch_alipay_form_url
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "switch_alipay_form_url");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string switch_tenpay_form_url
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "switch_tenpay_form_url");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static string switch_ipspay_form_url
        {
            get
            {
                return ConfigHelper.GetConfig(SettingGroup, "switch_ipspay_form_url");
            }

        }


	}
}
