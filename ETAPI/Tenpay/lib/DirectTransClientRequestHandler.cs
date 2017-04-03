using System;
using System.Collections;
using System.Web;
using System.Xml;
/**
 * 后台调用请求类
 * ============================================================================
 * api说明：
 * init(),初始化函数，默认给一些参数赋值，如cmdno,date等。
 * getGateURL()/setGateURL(),获取/设置入口地址,不包含参数值
 * getKey()/setKey(),获取/设置密钥
 * getParameter()/setParameter(),获取/设置参数值,只能获取、设置第一级xml数据
 * setAllParameterFromXml，直接通过xml格式内容设置参数
 * getAllParameters(),获取所有参数
 * getRequestURL(),获取带参数的请求URL
 * doSend(),重定向到财付通支付
 * getDebugInfo(),获取debug信息
 * 
 * ============================================================================
 *
 */

namespace viviapi.ETAPI.Tenpay.lib
{
    public class DirectTransClientRequestHandler : RequestHandler
    {
        public DirectTransClientRequestHandler(HttpContext httpContext)
            : base(httpContext)
        {
            this.setGateUrl("https://mch.tenpay.com/cgi-bin/mchbatchtransfer.cgi");
        }

        public void setAllParameterFromXml(string xmlStr)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStr);
            XmlNode root = xmlDoc.SelectSingleNode("root");
            XmlNodeList xnl = root.ChildNodes;

            foreach (XmlNode xnf in xnl)
            {
                this.setParameter(xnf.Name, xnf.InnerXml);
            }
        }


        public string EncodeBase64(string src, string charset)
        {
            string encode = "";
            byte[] bytes = System.Text.Encoding.GetEncoding(charset).GetBytes(src);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = charset;
            }
            return encode;
        }

        public override string getRequestURL()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"GB2312\" ?><root>";

            ArrayList akeys = new ArrayList(parameters.Keys);

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                xml += "<" + k + ">" + v + "</" + k + ">";
            }

            xml += "</root>";

            string content = EncodeBase64(xml, this.getCharset());

            string md5Res1 = MD5Util.GetMD5(content, this.getCharset()).ToLower();
            string md5Src2 = md5Res1 + this.getKey();

            string abstractStr = MD5Util.GetMD5(md5Src2, this.getCharset()).ToLower();

            this.setDebugInfo(xml + "=>" + content + "=>" + md5Src2 + " => sign:" + abstractStr);

            string requestURL = this.getGateUrl() + "?" + "content=" + TenpayUtil.UrlEncode(content, getCharset()) + "&abstract=" + TenpayUtil.UrlEncode(abstractStr, getCharset());

            return requestURL;

        }

    }
}




