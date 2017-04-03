using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace viviapi.ETAPI.Zweixin
{
    /// <summary>
    /// MD5Util ��ժҪ˵����
    /// </summary>
    public class MD5Util
    {
        public MD5Util()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        /** ��ȡ��д��MD5ǩ����� */
        public static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //����md5����
            byte[] inputBye;
            byte[] outputBye;

            //ʹ��GB2312���뷽ʽ���ַ���ת��Ϊ�ֽ����飮
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception ex)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);
            retStr=string.Concat(outputBye.Select(b=>b.ToString("X2")).ToArray());
            return retStr;
        }
    }
}
