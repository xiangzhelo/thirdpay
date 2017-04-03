using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;

namespace viviapi.BLL.Tools
{
    [Serializable]  
    public class IdcardInfo
    {
        public string code { get; set; }
        public string location { get; set; }
        public string birthday { get; set; }
        public string gender { get; set; }
        public string fullname { get; set; }
    }

    /// <summary>
    /// 身份证
    /// http://www.youdao.com/smartresult-xml/search.s?type=id&q=430381198411137791
    /// </summary>
    public sealed class idcards
    {
        public static IdcardInfo GetIdCardInfo(string id)
        {
            IdcardInfo result = new IdcardInfo();

            try
            {
                System.Text.Encoding gbkEncoding = System.Text.Encoding.GetEncoding("GBK");
                string postParam = "type=id&q=" + id;

                string selectUrl = "http://www.youdao.com/smartresult-xml/search.s";
                string retXml = viviLib.Web.WebClientHelper.GetString(selectUrl, postParam, "GET", gbkEncoding, 5000);

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(retXml);

                string code = doc.GetElementsByTagName("code")[0].InnerText;
                string location = doc.GetElementsByTagName("location")[0].InnerText;
                string birthday = doc.GetElementsByTagName("birthday")[0].InnerText;
                string gender = doc.GetElementsByTagName("gender")[0].InnerText;

                result.code = code;
                result.location = location;
                result.birthday = birthday;
                result.gender = gender == "m"?"男":"女";

                return result;
            }
            catch
            {
                return null;
            }

        }
    }
}
