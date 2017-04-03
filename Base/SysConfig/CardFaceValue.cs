using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviLib.Configuration;
namespace viviapi.SysConfig
{
    public class CardFaceValue
    {
        private static readonly string _group			= "cardfacevalue";
		/// <summary>
		/// 设置组。
		/// </summary>
		public static string SettingGroup
		{
			get{return _group;}
		}

        //移动充值卡全国
        public static string S0001 { get { return ConfigHelper.GetConfig(SettingGroup, "0001"); } }
        //移动充值卡浙江
        public static string S0001ZJ { get { return ConfigHelper.GetConfig(SettingGroup, "0001ZJ"); } }
        public static string S0001FJ { get { return ConfigHelper.GetConfig(SettingGroup, "0001FJ"); } }
        public static string S0001GD { get { return ConfigHelper.GetConfig(SettingGroup, "0001ZJ"); } }
        public static string S0001LN { get { return ConfigHelper.GetConfig(SettingGroup, "0001LN"); } }
    }
}
