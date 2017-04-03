using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using ThoughtWorks.QRCode.Codec;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.ETAPI.Weixin
{
    /// <summary>
    /// appid appsercert   partner=mchid  key直接到商户平台设置
    /// </summary>
    public class Utility : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Weixin;

        public Utility()
            : base(SuppId)
        {

        }

        internal string NotifyURL { get { return this.SiteDomain + "/Receive/Weixin/callback.aspx"; } }

        #region GetCodeUrl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="attach"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetCodeUrl(string orderid, decimal orderAmt, string attach, HttpContext context)
        {
            RequestHandler Params = new RequestHandler(context);
            Params.setKey(TenpayUtil.key);

            Params.setParameter("appid", TenpayUtil.appid);
            Params.setParameter("mch_id", TenpayUtil.partner);
            Params.setParameter("nonce_str", TenpayUtil.getNoncestr());
            Params.setParameter("body", "商城储值");
            Params.setParameter("out_trade_no", orderid);
            Params.setParameter("total_fee", (orderAmt * 100).ToString("f0"));
            Params.setParameter("spbill_create_ip", "127.0.0.1");
            Params.setParameter("notify_url", NotifyURL);
            Params.setParameter("trade_type", "NATIVE");
            Params.setParameter("product_id", orderid);

            string sign = Params.CreateTenpaySign();
            Params.setParameter("sign", sign);

            string xml = Params.parseXML();

            LogWrite("Request=>" + xml);

            try
            {
                Hashtable xmlMap;

                string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

                string retXml = TenpayUtil.GetString(url
                    , xml
                    , "POST"
                    , Encoding.UTF8
                    , 5 * 1000);

                LogWrite("retXml=>" + retXml);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(retXml);

                XmlNode root = xmlDoc.SelectSingleNode("xml");
                if (root != null)
                {
                    xmlMap = new Hashtable();
                    XmlNodeList xnl = root.ChildNodes;

                    foreach (XmlNode xnf in xnl)
                    {
                        xmlMap.Add(xnf.Name, xnf.InnerText);
                    }

                    if (xmlMap["return_code"] != null
                   && xmlMap["result_code"] != null
                   && xmlMap["code_url"] != null)
                    {
                        if (xmlMap["return_code"].ToString() == "SUCCESS"
                            && xmlMap["result_code"].ToString() == "SUCCESS")
                        {
                            return xmlMap["code_url"].ToString();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            return string.Empty;
        }
        #endregion

        #region //得到H5支付的连接
        public string GetH5Url(string orderid, decimal orderAmt, string attach, HttpContext context)
        {
            RequestHandler Params = new RequestHandler(context);
            Params.setKey(TenpayUtil.key);

            Params.setParameter("appid", TenpayUtil.appid);
            Params.setParameter("mch_id", TenpayUtil.partner);
            Params.setParameter("nonce_str", TenpayUtil.getNoncestr());
            Params.setParameter("body", "商城储值");
            Params.setParameter("out_trade_no", orderid);
            Params.setParameter("total_fee", (orderAmt * 100).ToString("f0"));
            Params.setParameter("spbill_create_ip", "127.0.0.1");
            Params.setParameter("notify_url", NotifyURL);
            Params.setParameter("trade_type", "MWEB");
            Params.setParameter("product_id", orderid);

            string sign = Params.CreateTenpaySign();
            Params.setParameter("sign", sign);

            string xml = Params.parseXML();

            LogWrite("Request=>" + xml);

            try
            {
                Hashtable xmlMap;

                string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

                string retXml = TenpayUtil.GetString(url
                    , xml
                    , "POST"
                    , Encoding.UTF8
                    , 5 * 1000);

                LogWrite("retXml=>" + retXml);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(retXml);

                XmlNode root = xmlDoc.SelectSingleNode("xml");
                if (root != null)
                {
                    xmlMap = new Hashtable();
                    XmlNodeList xnl = root.ChildNodes;

                    foreach (XmlNode xnf in xnl)
                    {
                        xmlMap.Add(xnf.Name, xnf.InnerText);
                    }

                    if (xmlMap["return_code"] != null
                   && xmlMap["result_code"] != null
                   && xmlMap["code_url"] != null)
                    {
                        if (xmlMap["return_code"].ToString() == "SUCCESS"
                            && xmlMap["result_code"].ToString() == "SUCCESS")
                        {
                            return xmlMap["mweb_url"].ToString();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            return string.Empty;
        }

        #endregion
        #region ShowImg
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeURL"></param>
        public static void ShowImg(string codeURL)
        {
            int widhtHeight = 300;
            string EC_level = "L";
            int margin = 0;

            var qrCodeEncoder = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                QRCodeScale = 4,
                QRCodeVersion = 0,
                QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L
            };


            //String data = UrlEncode1(chl);
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeURL);

            var mStream = new System.IO.MemoryStream();
            image.Save(mStream, System.Drawing.Imaging.ImageFormat.Gif);

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Gif";
            HttpContext.Current.Response.BinaryWrite(mStream.ToArray());
        }
        #endregion

        public static string _3dkey = "wxbb78e42427686b5b";
        public static string Decrypt(string strText)
        {
            try
            {
                int discarded = 0;
                byte[] data = GetBytes(strText, out discarded);//Encoding.GetEncoding("utf-8").GetBytes(strText);

                byte[] keys = Encoding.GetEncoding("utf-8").GetBytes(_3dkey);
                byte[] iv = Encoding.GetEncoding("utf-8").GetBytes("12345678");

                byte[] result = Des3DecodeECB(keys, iv, data);

                if (result != null)
                {
                    return Encoding.Default.GetString(result);
                }
                return string.Empty;
            }
            catch (System.Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return string.Empty;
            }
        }
        public static string Encrypt(string strText)
        {
            try
            {
                byte[] keys = Encoding.GetEncoding("utf-8").GetBytes(_3dkey);
                byte[] data = Encoding.GetEncoding("utf-8").GetBytes(strText);
                byte[] iv = Encoding.GetEncoding("utf-8").GetBytes("12345678");

                byte[] result = Des3.Des3EncodeECB(keys, iv, data);
                if (result != null)
                {
                    return ToHexString(result);
                }

                return string.Empty;
            }
            catch (System.Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return string.Empty;
            }
        }

        public static byte[] Des3DecodeECB(byte[] key, byte[] iv, byte[] data)
        {
            try
            {
                // Create a new MemoryStream using the passed 
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(data);

                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.ECB;
                tdsp.Padding = PaddingMode.PKCS7;

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    tdsp.CreateDecryptor(key, iv),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[data.Length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                //Convert the buffer into a string and return it.
                return fromEncrypt;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        public static byte[] Des3EncodeECB(byte[] key, byte[] iv, byte[] data)
        {
            try
            {
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();

                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.ECB;
                tdsp.Padding = PaddingMode.PKCS7;
                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    tdsp.CreateEncryptor(key, iv),
                    CryptoStreamMode.Write);

                // Write the byte array to the crypto stream and flush it.
                cStream.Write(data, 0, data.Length);
                cStream.FlushFinalBlock();

                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = mStream.ToArray();

                // Close the streams.
                cStream.Close();
                mStream.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }

        }

        private static char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public static string ToHexString(byte[] bytes)
        {
            int num = bytes.Length;
            char[] chars = new char[num * 2];
            for (int i = 0; i < num; i++)
            {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[i * 2 + 1] = hexDigits[b & 0xF];
            }
            return new string(chars);
        }
        public static byte[] GetBytes(string hexString, out int discarded)
        {
            discarded = 0;
            string newString = "";
            char c;
            // remove all none A-F, 0-9, characters
            for (int i = 0; i < hexString.Length; i++)
            {
                c = hexString[i];
                if (Uri.IsHexDigit(c))
                    newString += c;
                else
                    discarded++;
            }
            // if odd number of characters, discard last character
            if (newString.Length % 2 != 0)
            {
                discarded++;
                newString = newString.Substring(0, newString.Length - 1);
            }

            return HexToByte(newString);
        }
        public static byte[] HexToByte(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
            {
                hexString = "00";
            }
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
