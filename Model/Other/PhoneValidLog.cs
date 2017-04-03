using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model
{
    /// <summary>
    /// 手机验证码日志类
    /// </summary>
    public class PhoneValidLog
    {
        #region Model
        private int _id;
        private string _phone;
        private DateTime _sendtime;
        private string _clientip;
        private string _code;
        private bool _enable = false;

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
        public string phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime sendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string clientIP
        {
            set { _clientip = value; }
            get { return _clientip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string code
        {
            set { _code = value; }
            get { return _code; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool Enable
        {
            set { _enable = value; }
            get { return _enable; }
        }
        #endregion Model
    }
}
