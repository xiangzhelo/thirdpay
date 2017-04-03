using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.SMS
{
    public class MobileSmslog
    {
        #region Model
        private int _id;
        private string _mobile;
        private DateTime _sendtime;
        private int _count;
        private string _clientip;
        private string _code;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime SendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            set { _count = value; }
            get { return _count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClientIP
        {
            set { _clientip = value; }
            get { return _clientip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        #endregion Model
    }
}
