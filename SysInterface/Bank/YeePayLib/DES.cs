using System;
using System.Security.Cryptography;
using System.Text;

namespace viviapi.SysInterface.Bank.YeePayLib
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DES
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string a_strString, string a_strKey)
        {
            if (a_strKey.Length < 24)
            {
                string str = a_strKey;
                for (int i = 0; i < (24 / a_strKey.Length); i++)
                {
                    str = str + a_strKey;
                }
                a_strKey = str;
            }
            a_strKey = a_strKey.Substring(0, 24);

            ICryptoTransform transform = new TripleDESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(a_strKey), Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 }.CreateDecryptor();
            string rstr = "";
            try
            {
                byte[] inputBuffer = Convert.FromBase64String(a_strString);
                rstr = Encoding.ASCII.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
            }
            return rstr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Encrypt3DESJW(string a_strString, string a_strKey)
        {
            if (a_strKey.Length < 24)
            {
                a_strKey = a_strKey + "000000000000000000000000";
            }
            a_strKey = a_strKey.Substring(0, 24);
            ICryptoTransform transform = new TripleDESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(a_strKey), Mode = CipherMode.ECB }.CreateEncryptor();
            byte[] bytes = Encoding.ASCII.GetBytes(a_strString);
            return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Encrypt3DESSZX(string a_strString, string a_strKey)
        {
            if (a_strKey.Length < 24)
            {
                string str = a_strKey;
                for (int i = 0; i < (24 / a_strKey.Length); i++)
                {
                    str = str + a_strKey;
                }
                a_strKey = str;
            }
            a_strKey = a_strKey.Substring(0, 24);
            ICryptoTransform transform = new TripleDESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(a_strKey), Mode = CipherMode.ECB }.CreateEncryptor();
            byte[] bytes = Encoding.ASCII.GetBytes(a_strString);
            return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
        }
    }
}

