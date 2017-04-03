using System;
using System.Security.Cryptography;
using System.Text;

namespace viviapi.ETAPI.YeePay.Lib.com.yeepay
{
    public abstract class DES
    {
        public static string Decrypt3DES(string a_strString, string a_strKey)
        {
            ICryptoTransform transform = new TripleDESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(a_strKey), Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 }.CreateDecryptor();
            string str = "";
            try
            {
                byte[] inputBuffer = Convert.FromBase64String(a_strString);
                str = Encoding.ASCII.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
            }
            return str;
        }

        public static string Encrypt3DESJW(string a_strString, string a_strKey)
        {
            if (a_strKey.Length < 0x18)
            {
                a_strKey = a_strKey + "000000000000000000000000";
            }
            a_strKey = a_strKey.Substring(0, 0x18);
            ICryptoTransform transform = new TripleDESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(a_strKey), Mode = CipherMode.ECB }.CreateEncryptor();
            byte[] bytes = Encoding.ASCII.GetBytes(a_strString);
            return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public static string Encrypt3DESSZX(string a_strString, string a_strKey)
        {
            if (a_strKey.Length < 0x18)
            {
                string str = a_strKey;
                for (int i = 0; i < (0x18 / a_strKey.Length); i++)
                {
                    str = str + a_strKey;
                }
                a_strKey = str;
            }
            a_strKey = a_strKey.Substring(0, 0x18);
            ICryptoTransform transform = new TripleDESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(a_strKey), Mode = CipherMode.ECB }.CreateEncryptor();
            byte[] bytes = Encoding.ASCII.GetBytes(a_strString);
            return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
        }
    }
}

