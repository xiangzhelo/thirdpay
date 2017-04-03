using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;

namespace viviapi.BLL.Order.Notify
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestState
    {
        const int BUFFER_SIZE = 1024;
        public StringBuilder requestData;
        public string url;
        public byte[] BufferRead;
        public HttpWebRequest request;
        public HttpWebResponse response;
        public Stream streamResponse;
        /*订单类型 1网银 2卡类 3短信*/
        public int orderclass;
        public object order;
        public RequestState()
        {
            BufferRead = new byte[BUFFER_SIZE];
            requestData = new StringBuilder("");
            request = null;
            streamResponse = null;
        }
    }
}
