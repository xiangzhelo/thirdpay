using System;

namespace viviapi.Model.News
{
    /// <summary>
    /// Msg:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class InternalMessage
    {
        public InternalMessage()
        { }

        #region Model
        private int _id;
        private byte _senderusertype;
        private int _sendid;
        private string _sender;
        private byte _receivertype;
        private int _receiverid;
        private string _receiver;
        private string _msgtitle;
        private string _msgcontent;
        private DateTime _addtime;
        private bool _isread = false;
        private DateTime? _readtime;
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
        public byte senderUserType
        {
            set { _senderusertype = value; }
            get { return _senderusertype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int sendId
        {
            set { _sendid = value; }
            get { return _sendid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sender
        {
            set { _sender = value; }
            get { return _sender; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte receiverType
        {
            set { _receivertype = value; }
            get { return _receivertype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int receiverId
        {
            set { _receiverid = value; }
            get { return _receiverid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string msgtitle
        {
            set { _msgtitle = value; }
            get { return _msgtitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string msgContent
        {
            set { _msgcontent = value; }
            get { return _msgcontent; }
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
        public bool isRead
        {
            set { _isread = value; }
            get { return _isread; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? readTime
        {
            set { _readtime = value; }
            get { return _readtime; }
        }
        #endregion Model

    }
}

