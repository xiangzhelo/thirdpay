using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace viviLib
{
    public class AES
    {
        private static byte[] Keys = new byte[] { 0x41, 0x72, 0x65, 0x79, 0x6f, 0x75, 0x6d, 0x79, 0x53, 110, 0x6f, 0x77, 0x6d, 0x61, 110, 0x3f };

        public static string Decode(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = Utility.GetSubString(decryptKey, 0x20, "");
                decryptKey = decryptKey.PadRight(0x20, ' ');
                RijndaelManaged managed = new RijndaelManaged();
                managed.Key = Encoding.UTF8.GetBytes(decryptKey);
                managed.IV = Keys;
                ICryptoTransform transform = managed.CreateDecryptor();
                byte[] inputBuffer = Convert.FromBase64String(decryptString);
                byte[] bytes = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return "";
            }
        }

        #region DESDecrypt DES解密
        /// <summary>
        /// 进行DES解密。
        /// </summary>
        /// <param name="pToDecrypt">要解密的以Base64</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>已解密的字符串。</returns>
        public static string DESDecrypt(string pToDecrypt, string sKey)
        {
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DoDecrypt(string pToDecrypt, string sKey)
        {
            //return DESDecrypt(pToDecrypt,sKey);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = new byte[pToDecrypt.Length / 2];
            for (int i = 0; i < (pToDecrypt.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            provider.Key = Encoding.ASCII.GetBytes(sKey);
            provider.IV = Encoding.ASCII.GetBytes(sKey);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            return Encoding.Default.GetString(stream.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DoDecrypt1(string pToDecrypt, string sKey)
        {
            return DESDecrypt(pToDecrypt, sKey);
            //DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            //byte[] buffer = new byte[pToDecrypt.Length / 2];
            //for (int i = 0; i < (pToDecrypt.Length / 2); i++)
            //{
            //    int num2 = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 0x10);
            //    buffer[i] = (byte) num2;
            //}
            //provider.Key = Encoding.ASCII.GetBytes(sKey);
            //provider.IV = Encoding.ASCII.GetBytes(sKey);
            //MemoryStream stream = new MemoryStream();
            //CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            //stream2.Write(buffer, 0, buffer.Length);
            //stream2.FlushFinalBlock();
            //StringBuilder builder = new StringBuilder();
            //return Encoding.Default.GetString(stream.ToArray());
        }

        #region DESEncrypt DES加密
        // <summary>
        /// 进行DES加密。
        /// </summary>
        /// <param name="pToEncrypt">要加密的字符串。</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>以Base64格式返回的加密字符串。</returns>
        public static string DESEncrypt(string pToEncrypt, string sKey)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        #endregion



        



        public static string Encode(string encryptString, string encryptKey)
        {
            encryptKey = Utility.GetSubString(encryptKey, 0x20, "");
            encryptKey = encryptKey.PadRight(0x20, ' ');
            RijndaelManaged managed = new RijndaelManaged();
            managed.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 0x20));
            managed.IV = Keys;
            ICryptoTransform transform = managed.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(encryptString);
            return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
        }
    }
}

