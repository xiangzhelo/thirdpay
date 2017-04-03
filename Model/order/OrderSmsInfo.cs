using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Order
{
    /// <summary>
    /// 短信订单
    /// </summary>
    [Serializable]
    public class OrderSmsInfo
    {
        private int _id;
        private string _orderid;
        private string _userorder;
        private int _supplierid;
        private int _userid;
        private string _mobile;
        private decimal _fee;
        private string _message;
        private string _servicenum;
        private string _linkid;
        private string _gwid;
        private decimal _payrate;
        private decimal _supplierrate;
        private decimal _promrate;
        private decimal _payamt;
        private decimal _promamt;
        private decimal _supplieramt;
        private decimal _profits;
        private int _server;
        private DateTime _addtime;
        private DateTime _completetime;

        private string _notifyurl;
        private string _againnotifyurl;
        private int _notifycount = 0;
        private int _notifystat = 1;
        private string _notifycontext;

        public int status { get; set; }
        public string opstate { get; set; }
        public string msg { get; set; }
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
        public string userorder
        {
            set { _userorder = value; }
            get { return _userorder; }
        }
        /// <summary>
        /// 接口商
        /// </summary>
        public int supplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
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
        /// 手机号码
        /// </summary>
        public string mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 资费
        /// </summary>
        public decimal fee
        {
            set { _fee = value; }
            get { return _fee; }
        }
        /// <summary>
        /// 上行信息内容
        /// </summary>
        public string message
        {
            set { _message = value; }
            get { return _message; }
        }
        /// <summary>
        /// 长号码
        /// </summary>
        public string servicenum
        {
            set { _servicenum = value; }
            get { return _servicenum; }
        }
       
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string linkid
        {
            set { _linkid = value; }
            get { return _linkid; }
        }
        /// <summary>
        /// 1:移动 2:联通3:电信
        /// </summary>
        public string gwid
        {
            set { _gwid = value; }
            get { return _gwid; }
        }
        /// <summary>
        /// 平台给商家的费率
        /// </summary>
        public decimal payRate
        {
            set { _payrate = value; }
            get { return _payrate; }
        }
        /// <summary>
        /// 供应商 给平台的费率
        /// </summary>
        public decimal supplierRate
        {
            set { _supplierrate = value; }
            get { return _supplierrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal promRate
        {
            set { _promrate = value; }
            get { return _promrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal payAmt
        {
            set { _payamt = value; }
            get { return _payamt; }
        }
        /// <summary>
        /// 代理费用
        /// </summary>
        public decimal promAmt
        {
            set { _promamt = value; }
            get { return _promamt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal supplierAmt
        {
            set { _supplieramt = value; }
            get { return _supplieramt; }
        }
        /// <summary>
        /// 平台利润
        /// </summary>
        public decimal profits
        {
            set { _profits = value; }
            get { return _profits; }
        }
        /// <summary>
        /// 提交服务器
        /// </summary>
        public int server
        {
            set { _server = value; }
            get { return _server; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime completetime
        {
            set { _completetime = value; }
            get { return _completetime; }
        }

        /// <summary>
        /// 下行异步通知地址
        /// </summary>
        public string notifyurl
        {
            set { _notifyurl = value; }
            get { return _notifyurl; }
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
        /// 指令
        /// </summary>
        public string Cmd { get; set; }
        //用户信息内容
        public string userMsgContenct { get; set; }
        //所属业务员
        public int? manageId { get; set; }
        public Decimal? commission { get; set; }

        private bool _issucc = false;
        /// <summary>
        /// 是否返回成功
        /// </summary>
        public bool issucc
        {
            set { _issucc = value; }
            get { return _issucc; }
        }

        public string _errcode = string.Empty;
        /// <summary>
        /// 返回错误信息
        /// </summary>
        public string errcode
        {
            set { _errcode = value; }
            get { return _errcode; }
        }
    }
}
