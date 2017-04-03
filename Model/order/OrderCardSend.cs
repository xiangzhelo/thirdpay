using System;

namespace viviapi.Model.Order
{
    /// <summary>
    /// ordercardsend:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class OrderCardSend
    {
        public OrderCardSend()
        { }
        #region Model
        private int _id;
        private string _orderid;
        private byte _source;
        private int? _suppid;
        private int? _success;
        private int? _summitstatus;
        private int? _orderstatus;
        private string _cardno;
        private string _cardpass;
        private string _supptransno;
        private string _responsetext;
        private string _errcode;
        private string _errmsg;
        private DateTime? _inittime;
        private string _message;
        private DateTime? _completetime;
        private int _typeid = 0;
        private int _facevalue = 0;

       
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
        public string orderid
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte source
        {
            set { _source = value; }
            get { return _source; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? suppId
        {
            set { _suppid = value; }
            get { return _suppid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? success
        {
            set { _success = value; }
            get { return _success; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? summitStatus
        {
            set { _summitstatus = value; }
            get { return _summitstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? orderStatus
        {
            set { _orderstatus = value; }
            get { return _orderstatus; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int facevalue
        {
            set { _facevalue = value; }
            get { return _facevalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cardno
        {
            set { _cardno = value; }
            get { return _cardno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cardpass
        {
            set { _cardpass = value; }
            get { return _cardpass; }
        }
        public int typeid
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string suppTransNo
        {
            set { _supptransno = value; }
            get { return _supptransno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string responseText
        {
            set { _responsetext = value; }
            get { return _responsetext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string errCode
        {
            set { _errcode = value; }
            get { return _errcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string errMsg
        {
            set { _errmsg = value; }
            get { return _errmsg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? initTime
        {
            set { _inittime = value; }
            get { return _inittime; }
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
        public DateTime? completeTime
        {
            set { _completetime = value; }
            get { return _completetime; }
        }
        #endregion Model

    }
}

