using System;
using System.Collections;
using System.Web;
using System.Xml;
/**
 * ��̨����������
 * ============================================================================
 * api˵����
 * init(),��ʼ��������Ĭ�ϸ�һЩ������ֵ����cmdno,date�ȡ�
 * getGateURL()/setGateURL(),��ȡ/������ڵ�ַ,����������ֵ
 * getKey()/setKey(),��ȡ/������Կ
 * getParameter()/setParameter(),��ȡ/���ò���ֵ,ֻ�ܻ�ȡ�����õ�һ��xml����
 * setAllParameterFromXml��ֱ��ͨ��xml��ʽ�������ò���
 * getAllParameters(),��ȡ���в���
 * getRequestURL(),��ȡ������������URL
 * doSend(),�ض��򵽲Ƹ�֧ͨ��
 * getDebugInfo(),��ȡdebug��Ϣ
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




