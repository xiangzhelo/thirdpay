using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using viviapi.ETAPI.Weixin;
using viviapi.ETAPI.Zweixin;

namespace viviapi.ETAPI.Zweixin
{
    /**
    'ǩ��������
     ============================================================================/// <summary>
    'api˵����
    'init();
    '��ʼ��������Ĭ�ϸ�һЩ������ֵ��
    'setKey(key_)'�����̻���Կ
    'createMd5Sign(signParams);�ֵ�����Md5ǩ��
    'genPackage(packageParams);��ȡpackage��
    'createSHA1Sign(signParams);����ǩ��SHA1
    'parseXML();���xml
    'getDebugInfo(),��ȡdebug��Ϣ
     * 
     * ============================================================================
     */
    public class RequestHandler
    {

        public RequestHandler(HttpContext httpContext)
        {
            parameters = new Hashtable();

            this.httpContext = httpContext;

        }
        /**  ��Կ */
        private string key;

        protected HttpContext httpContext;

        /** ����Ĳ��� */
        protected Hashtable parameters;

        /** debug��Ϣ */
        private string debugInfo;

        /** ��ʼ������ */
        public virtual void init()
        {
        }
        /** ��ȡdebug��Ϣ */
        public String getDebugInfo()
        {
            return debugInfo;
        }
        /** ��ȡ��Կ */
        public String getKey()
        {
            return key;
        }

        /** ������Կ */
        public void setKey(string key)
        {
            this.key = key;
        }

        /** ���ò���ֵ */
        public void setParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (parameters.Contains(parameter))
                {
                    parameters.Remove(parameter);
                }

                parameters.Add(parameter, parameterValue);
            }
        }


        //��ȡpackage��������ǩ����
        public string getRequestURL()
        {
            this.createSign();
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && System.String.Compare("key", k, System.StringComparison.Ordinal) != 0)
                {
                    sb.Append(k + "=" + ZweixinpayUtil.UrlEncode(v, getCharset()) + "&");
                }
            }

            //ȥ�����һ��&
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }


            return sb.ToString();

        }


        //����md5ժҪ,������:����������a-z����,������ֵ�Ĳ������μ�ǩ����

        public virtual void createSign()
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v
                    && System.String.Compare("", v, System.StringComparison.Ordinal) != 0
                    && System.String.Compare("sign", k, System.StringComparison.Ordinal) != 0
                    && System.String.Compare("key", k, System.StringComparison.Ordinal) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.getKey());

            string sign = MD5Util.GetMD5(sb.ToString(), getCharset()).ToUpper();

            this.setParameter("sign", sign);

            //debug��Ϣ
            this.setDebugInfo(sb.ToString() + " => sign:" + sign);
        }


        //����packageǩ��
        public virtual string createMd5Sign()
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "".CompareTo(v) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            string sign = MD5Util.GetMD5(sb.ToString(), getCharset()).ToLower();

            this.setParameter("sign", sign);
            return sign;
        }

        public virtual string createMd5Sign2()
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "".CompareTo(v) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            sb.Append("&key=" + ZweixinpayUtil.key);

            string sign = MD5Util.GetMD5(sb.ToString(), getCharset()).ToUpper();

            this.setParameter("sign", sign);
            return sign;
        }

        public virtual string CreateSign()
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.getKey());

            //WeiXin.logger("sb =>" + sb.ToString());

            string sign = MD5Util.GetMD5(sb.ToString(), "utf-8").ToUpper();

            //WeiXin.logger("sign =>" + sign);

            return sign;
        }

        //����sha1ǩ��
        public string createSHA1Sign()
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && System.String.Compare("", v, System.StringComparison.Ordinal) != 0
                       && System.String.Compare("sign", k, System.StringComparison.Ordinal) != 0
                       && System.String.Compare("key", k, System.StringComparison.Ordinal) != 0)
                {
                    if (sb.Length == 0)
                    {
                        sb.Append(k + "=" + v);
                    }
                    else
                    {
                        sb.Append("&" + k + "=" + v);
                    }
                }
            }
            //WeiXin.logger("string =>" + sb.ToString());

            string paySign = SHA1Util.getSha1(sb.ToString()).ToString().ToLower();

            //debug��Ϣ
            this.setDebugInfo(sb.ToString() + " => sign:" + paySign);
            return paySign;
        }


        //���XML
        public string parseXML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            foreach (string k in parameters.Keys)
            {
                string v = (string)parameters[k];
                if (Regex.IsMatch(v, @"^[0-9.]$"))
                {
                    sb.Append("<" + k + ">" + v + "</" + k + ">");
                }
                else
                {
                    sb.Append("<" + k + "><![CDATA[" + v + "]]></" + k + ">");
                }
            }
            sb.Append("</xml>");
            return sb.ToString();
        }



        /** ����debug��Ϣ */
        public void setDebugInfo(String debugInfo)
        {
            this.debugInfo = debugInfo;

            //viviapi.ETAPI.tenpay.WeiXinLib.WeiXin.logger("debugInfo=>" + debugInfo);
        }

        public Hashtable getAllParameters()
        {
            return this.parameters;
        }

        protected virtual string getCharset()
        {
            return this.httpContext.Request.ContentEncoding.BodyName;
        }

        private string gateUrl;
        /** ������ڵ�ַ,����������ֵ */
        public void setGateUrl(String gateUrl)
        {
            this.gateUrl = gateUrl;
        }

        public string getGateUrl()
        {
            return this.gateUrl;
        }

    }
}
