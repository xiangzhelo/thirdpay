using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.Model.supplier;

namespace viviapi.ETAPI.Ebatong.Lib
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// </summary>
    public class Config
    {
        private static string partner = "";
        private static string key = "";
        private static string input_charset = "";
        private static string sign_type = "";


        static Config()
        {
            SupplierInfo suppInfo = Factory.GetCacheModel((int)SupplierCode.Ebatong);

            if (suppInfo != null)
            {
                //合作身份者ID，18位纯数字组成的字符串
                partner = suppInfo.puserid;

                //交易安全检验码，由数字和字母组成的32位字符串
                key = suppInfo.puserkey;
            }

            //字符编码格式 
            input_charset = "UTF-8";

            //签名方式，选择项：MD5
            sign_type = "MD5";
        }

        
        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        /// <summary>
        /// 获取或设交易安全校验码
        /// </summary>
        public static string Key
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }
        
    }
}