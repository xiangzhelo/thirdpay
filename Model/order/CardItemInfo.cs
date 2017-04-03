using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.Order
{
    /// <summary>
    /// 多卡提交
    /// </summary>
    [Serializable] 
    public class CardItemInfo
    {
        private long _id;
        private int _userid;
        private int _serial;
        private string _porderid;
        private int _suppid;
        private int _cardtype;
        private string _cardno;
        private string _cardpwd;
        private decimal? _refervalue;
        private decimal? _payrate;
        private DateTime _addtime;
        private string _supplierorder;
        private decimal _realvalue;
        private int _status;
        private string _opstate;
        private string _msg;
        private DateTime? _completetime;
        private decimal _supplierrate = 0M;
        private decimal _promrate = 0M;
        private decimal _commission = 0M;
        private int _agent = 0;


        /// <summary>
        /// 
        /// </summary>
        public long id
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
        public int serial
        {
            set { _serial = value; }
            get { return _serial; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string porderid
        {
            set { _porderid = value; }
            get { return _porderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int suppid
        {
            set { _suppid = value; }
            get { return _suppid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int cardtype
        {
            set { _cardtype = value; }
            get { return _cardtype; }
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
        public string cardpwd
        {
            set { _cardpwd = value; }
            get { return _cardpwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? refervalue
        {
            set { _refervalue = value; }
            get { return _refervalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? payrate
        {
            set { _payrate = value; }
            get { return _payrate; }
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
        public string supplierOrder
        {
            set { _supplierorder = value; }
            get { return _supplierorder; }
        }
        /// <summary>
        /// 实际值
        /// </summary>
        public decimal realvalue
        {
            set { _realvalue = value; }
            get { return _realvalue; }
        }
        /// <summary>
        /// 1 初始化初状态 2 成功 4 失败 8 扣量 16 转单
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string opstate
        {
            set { _opstate = value; }
            get { return _opstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string msg
        {
            set { _msg = value; }
            get { return _msg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? completetime
        {
            set { _completetime = value; }
            get { return _completetime; }
        }
        /// <summary>
        /// 供应商 给平台的费率
        /// </summary>
        public decimal supplierrate
        {
            set { _supplierrate = value; }
            get { return _supplierrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal promrate
        {
            set { _promrate = value; }
            get { return _promrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal commission
        {
            set { _commission = value; }
            get { return _commission; }
        }

        /// <summary>
        /// 所属代理
        /// </summary>
        public int agentId
        {
            set { _agent = value; }
            get { return _agent; }
        }
    }
}
