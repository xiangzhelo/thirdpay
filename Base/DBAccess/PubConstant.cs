using System;
using System.Configuration;

namespace DBAccess
{
    
    public class PubConstant
    {        
        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        public static string ConnectionString
        {           
            get 
            {
                return viviLib.Security.Cryptography.RijndaelDecrypt(viviapi.SysConfig.RuntimeSetting.ConnectString);
            }
        }


        /// <summary>
        /// �õ�web.config������������ݿ������ַ�����
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (ConStringEncrypt == "true")
            {
                connectionString = viviLib.Security.Cryptography.RijndaelDecrypt(connectionString);
            }
            return connectionString;
        }

    }
}
