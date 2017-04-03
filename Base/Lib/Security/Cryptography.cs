using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviLib.Security
{

    /// <summary>
    /// 加密解密类
    /// </summary>
    public sealed class Cryptography
    {
        private static byte[] DESIV;
        private const string DESkey = "1234567890";
        private static byte[] DESKey;

        /// <summary>
        /// 构造函数。
        /// </summary>
        private Cryptography()
        {
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strToEncrypt"></param>
        /// <returns></returns>
        public static string MD5(string strToEncrypt)
        {
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(strToEncrypt); //.Default.GetBytes(strToEncrypt);
            bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string encryptStr = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                encryptStr = encryptStr + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return encryptStr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strToEncrypt"></param>
        /// <returns></returns>
        public static string MD5(string strToEncrypt,string encodeing)
        {
            byte[] bytes = Encoding.GetEncoding(encodeing).GetBytes(strToEncrypt); //.Default.GetBytes(strToEncrypt);
            
            bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string encryptStr = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                encryptStr = encryptStr + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return encryptStr;
        }

        /// <summary>
        /// 解密数据连接字符串
        /// </summary>
        /// <param name="connString">加密后的数据库连接字符串</param>
        /// <returns>数据库连接字符串原文</returns>
        public static string DecryptConnString(string connString)
        {
            return DESDecryptString(connString, string.Empty);
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="inputStr"></param> 
        /// <param name="keyStr"></param>
        /// <returns></returns>
        public static string DESDecryptString(string inputStr, string keyStr)
        {
            if ((inputStr == null) || (inputStr.Length == 0))
            {
                return string.Empty;
            }
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            if ((keyStr == null) || (keyStr.Length == 0))
            {
                keyStr = "1234567890";
            }
            byte[] buffer = new byte[inputStr.Length / 2];
            for (int i = 0; i < (inputStr.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(inputStr.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte) num2;
            }
            byte[] bytes = Encoding.Default.GetBytes(keyStr);
            byte[] buffer3 = new SHA1Managed().ComputeHash(bytes);
            DESKey = new byte[8];
            DESIV = new byte[8];
            for (int j = 0; j < 8; j++)
            {
                DESKey[j] = buffer3[j];
            }
            for (int k = 8; k < 0x10; k++)
            {
                DESIV[k - 8] = buffer3[k];
            }
            provider.Key = DESKey;
            provider.IV = DESIV;
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            return Encoding.Default.GetString(stream.ToArray());
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="inputStr"></param> 
        /// <param name="keyStr"></param>
        /// <returns></returns>
        public static string DESEncryptString(string inputStr, string keyStr)
        {
            if ((inputStr == null) || (inputStr.Length == 0))
            {
                return string.Empty;
            }
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            if ((keyStr == null) || (keyStr.Length == 0))
            {
                keyStr = "1234567890";
            }
            byte[] bytes = Encoding.Default.GetBytes(inputStr);
            byte[] buffer = Encoding.Default.GetBytes(keyStr);
            byte[] buffer3 = new SHA1Managed().ComputeHash(buffer);
            DESKey = new byte[8];
            DESIV = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                DESKey[i] = buffer3[i];
            }
            for (int j = 8; j < 0x10; j++)
            {
                DESIV[j - 8] = buffer3[j];
            }
            provider.Key = DESKey;
            provider.IV = DESIV;
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num3 in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num3);
            }
            stream2.Close();
            stream.Close();
            return builder.ToString();
        }

        /// <summary>
        /// 加密数据连接字符串
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>加密后的数据库连接字符串</returns>
        public static string EncryptConnString(string connString)
        {
            return DESEncryptString(connString, string.Empty);
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password">密码原码</param>
        /// <returns>加密后的密码字符串</returns>
        public static string EncryptPassword(string password)
        {
            return MD5(password);
        }

        ///// <summary>
        ///// MD5加密
        ///// </summary>
        ///// <remarks>
        ///// 输入字符串返回字符串
        ///// </remarks>
        ///// <param name="strToEncrypt"></param>
        ///// <returns></returns>
        //public static string MD5(string strToEncrypt)
        //{
        //    return MD5ByteToStr(Encoding.UTF8.GetBytes(strToEncrypt));
        //}

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <remarks>
        /// 输入字节组数返回字节数组
        /// </remarks>
        /// <param name="bytesToEncrypt"></param>
        /// <returns></returns>
        public static byte[] MD5ByteToByte(byte[] bytesToEncrypt)
        {
            return ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(bytesToEncrypt);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <remarks>
        /// 输入字节数组返回字符串
        /// </remarks>
        /// <param name="bytesToEncrypt"></param>
        /// <returns></returns>
        public static string MD5ByteToStr(byte[] bytesToEncrypt)
        {
            return Convert.ToBase64String(MD5ByteToByte(bytesToEncrypt));
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <remarks>
        /// 输入字符串返回字节数组
        /// </remarks>
        /// <param name="strToEncrypt"></param>
        /// <returns></returns>
        public static byte[] MD5StrToByte(string strToEncrypt)
        {
            return MD5ByteToByte(Encoding.UTF8.GetBytes(strToEncrypt));
        }

        /// <summary>
        /// 使用Rijndael算法解密字符串
        /// </summary>
        /// <param name="strToDecrypt"></param>
        /// <returns></returns>
        public static string RijndaelDecrypt(string strToDecrypt)
        {
            byte[] rijndaelKey = RijndaelKey;
            byte[] rijndaelIV = RijndaelIV;
            byte[] buffer = Convert.FromBase64String(strToDecrypt);
            byte[] buffer4 = new byte[buffer.Length];
            MemoryStream stream = new MemoryStream(buffer);
            RijndaelManaged managed = new RijndaelManaged();
            CryptoStream stream2 = new CryptoStream(stream, managed.CreateDecryptor(rijndaelKey, rijndaelIV), CryptoStreamMode.Read);
            try
            {
                stream2.Read(buffer4, 0, buffer4.Length);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                stream.Close();
                stream2.Close();
                return string.Empty;
            }
            return Encoding.UTF8.GetString(buffer4);
        }

        /// <summary>
        /// 使用Rijndael算法加密字符串
        /// </summary>
        /// <param name="strToEncrypt"></param>
        /// <returns></returns>
        public static string RijndaelEncrypt(string strToEncrypt)
        {
            byte[] rijndaelKey = RijndaelKey;
            byte[] rijndaelIV = RijndaelIV;
            byte[] bytes = Encoding.UTF8.GetBytes(strToEncrypt);
            byte[] inArray = new byte[0];
            MemoryStream stream = new MemoryStream();
            RijndaelManaged managed = new RijndaelManaged();
            CryptoStream stream2 = new CryptoStream(stream, managed.CreateEncryptor(rijndaelKey, rijndaelIV), CryptoStreamMode.Write);
            try
            {
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                inArray = stream.ToArray();
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                stream.Close();
                stream2.Close();
                return string.Empty;
            }
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// SHA1加密字符串
        /// </summary>
        /// <param name="strToEncrypt"></param>
        /// <returns></returns>
        public static string SHA1(string strToEncrypt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(strToEncrypt);
            return Convert.ToBase64String(((HashAlgorithm) CryptoConfig.CreateFromName("SHA1")).ComputeHash(bytes));
        }

        /// <summary>
        /// Rijndael算法用到的IV
        /// </summary>
        private static byte[] RijndaelIV
        {
            get
            {
                return MD5StrToByte("广东省中山市能龙软件科技有限公司 | WWW.NENGLONG.COM");
            }
        }

        /// <summary>
        /// Rijndael算法用到的key
        /// </summary>
        private static byte[] RijndaelKey
        {
            get
            {
                byte[] destinationArray = new byte[0x20];
                Array.Copy(MD5StrToByte("广东省中山市能龙软件科技有限公司"), 0, destinationArray, 0, 0x10);
                Array.Copy(MD5ByteToByte(MD5StrToByte("WWW.NENGLONG.COM")), 0, destinationArray, 0x10, 0x10);
                return destinationArray;
            }
        }
    }
}

