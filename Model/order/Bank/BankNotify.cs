using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Order.Bank
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class BankNotify
    {
        #region Model
        private string _orderid;
        private string _status;
        private string _message;
        private string _httpstatuscode;
        private string _statusdescription;
        private string _againnotifyurl;
        private int _notifycount = 0;
        private int _notifystat = 1;
        private string _notifycontext;
        private DateTime? _addtime =DateTime.Now;
        private DateTime? _notifytime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public string orderid
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string message
        {
            set { _message = value; }
            get { return _message; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string httpStatusCode
        {
            set { _httpstatuscode = value; }
            get { return _httpstatuscode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StatusDescription
        {
            set { _statusdescription = value; }
            get { return _statusdescription; }
        }
        /// <summary>
        /// 下行异步通知地址
        /// </summary>
        public string againNotifyUrl
        {
            set { _againnotifyurl = value; }
            get { return _againnotifyurl; }
        }
        /// <summary>
        /// 异步通知总次数
        /// </summary>
        public int notifycount
        {
            set { _notifycount = value; }
            get { return _notifycount; }
        }
        /// <summary>
        /// 异步通知状态 1 未通知 2 通知成功 4 通知失败
        /// </summary>
        public int notifystat
        {
            set { _notifystat = value; }
            get { return _notifystat; }
        }
        /// <summary>
        /// 异步返回内容
        /// </summary>
        public string notifycontext
        {
            set { _notifycontext = value; }
            get { return _notifycontext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? notifytime
        {
            set { _notifytime = value; }
            get { return _notifytime; }
        }
        #endregion Model
    }
}
