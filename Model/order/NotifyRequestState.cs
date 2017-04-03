using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace viviapi.Model.Order
{
    /// <summary>
    /// 
    /// </summary>
    public class NotifyRequestState
    {
        const int BUFFER_SIZE = 1024;
        public StringBuilder requestData;
        public string url;
        public byte[] BufferRead;
        public HttpWebRequest request;
        public HttpWebResponse response;
        public Stream streamResponse;

        /*订单类型 1网银 2卡类 3短信*/
        public byte ordertype;
        public object order;

        public NotifyRequestState()
        {
            BufferRead = new byte[BUFFER_SIZE];
            requestData = new StringBuilder("");
            request = null;
            streamResponse = null;
        }
    }
}
