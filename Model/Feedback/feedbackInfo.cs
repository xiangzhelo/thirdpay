using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model
{
    public enum feedbacktype
    {
        BUG反馈 = 1,
        意见建议 = 2,
        产品咨询 = 3,
        其它 = 4
    }

    public enum feedbackstatus
    {
        等待回复 = 1,
        已回复 = 2
    }

   public  class feedbackInfo
    {
        private int _id;
        private int _userid;
        private feedbacktype _typeid = feedbacktype.BUG反馈;
        private string _title;
        private string _cont;
        private feedbackstatus _status = feedbackstatus.等待回复;
        private DateTime _addtime;
        private string _reply;
        private int? _replyer;
        private DateTime? _replytime;

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public feedbacktype typeid
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cont
        {
            set { _cont = value; }
            get { return _cont; }
        }
        /// <summary>
        /// 
        /// </summary>
        public feedbackstatus status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string reply
        {
            set { _reply = value; }
            get { return _reply; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? replyer
        {
            set { _replyer = value; }
            get { return _replyer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? replytime
        {
            set { _replytime = value; }
            get { return _replytime; }
        }
        private string _clientip;
        /// <summary>
        /// 
        /// </summary>
        public string clientip
        {
            set { _clientip = value; }
            get { return _clientip; }
        }
    }
}
