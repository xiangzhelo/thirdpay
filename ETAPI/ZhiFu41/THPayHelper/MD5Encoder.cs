namespace viviapi.ETAPI.ZhiFu41
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class MD5Encoder
    {
        public static string encode(string str, string charset)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.GetEncoding(charset).GetBytes(str);
            byte[] buffer2 = provider.ComputeHash(bytes);
            string str2 = "";
            for (int i = 0; i < buffer2.Length; i++)
            {
                str2 = str2 + buffer2[i].ToString("x2");
            }
            return str2;
        }
    }
}

