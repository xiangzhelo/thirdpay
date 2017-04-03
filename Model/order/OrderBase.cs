using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Order
{
    public enum OrderTypeEnum
    { 
        API = 1,
        无来路 = 2,
        有来路 = 4,
        批量销卡 = 8
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class OrderBase
    {
        private long _id;
        private string _orderid;
        private int _ordertype;
        private int _userid;
        private int _typeid;
        private string _paymodeid;
        private string _userorder;
        private decimal _refervalue;
        private decimal? _realvalue;
        private string _notifyurl;
        private string _againnotifyurl;
        private int _notifycount = 0;
        private int _notifystat = 1;
        private string _notifycontext;
        private string _returnurl;
        private string _attach;
        private string _payerip;
        private string _clientip;
        private string _referurl = string.Empty;
        private DateTime _addtime;
        private int _supplierid;
        private string _supplierorder;
        private int _status = 1;
        private byte _deduct = 0;

        private DateTime? _processingtime;
        private DateTime? _completetime;
        private decimal _payrate = 0M;
        private decimal _supplierrate = 0M;
        private decimal _promrate = 0M;
        private decimal _payamt = 0M;
        private decimal _promamt = 0M;
        private decimal _supplieramt = 0M;
        private decimal _profits = 0M;
        private int? _server = 1;

        private string _version = string.Empty;
        
        public string opstate { get; set; }


        private string _cus_subject=string.Empty;
        private string _cus_price = string.Empty;
        private string _cus_quantity = string.Empty;
        private string _cus_description = string.Empty;
        private string _cus_field1 = string.Empty;
        private string _cus_field2 = string.Empty;
        private string _cus_field3 = string.Empty;
        private string _cus_field4 = string.Empty;
        private string _cus_field5 = string.Empty;

        //错误类型
        private string _errtype = string.Empty;
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
        /// 系统ID
        /// </summary>
        public string orderid
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 1 API订单 2 简单连接无来路判决 4 有来路简单连接 
        /// </summary>
        public int ordertype
        {
            set { _ordertype = value; }
            get { return _ordertype; }
        }
        /// <summary>
        /// 用户
        /// </summary>
        public int userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int typeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 通道
        /// </summary>
        public string paymodeId
        {
            set { _paymodeid = value; }
            get { return _paymodeid; }
        }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string userorder
        {
            set { _userorder = value; }
            get { return _userorder; }
        }
        /// <summary>
        /// 用户提交值
        /// </summary>
        public decimal refervalue
        {
            set { _refervalue = value; }
            get { return _refervalue; }
        }
        /// <summary>
        /// 结算金额
        /// 2014.4.30修改
        /// </summary>
        public decimal? realvalue
        {
            set { _realvalue = value; }
            get { return _realvalue; }
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
        /// 下行同步通知地址
        /// </summary>
        public string returnurl
        {
            set { _returnurl = value; }
            get { return _returnurl; }
        }
        /// <summary>
        /// 备注消息
        /// </summary>
        public string attach
        {
            set { _attach = value; }
            get { return _attach; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string payerip
        {
            set { _payerip = value; }
            get { return _payerip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string clientip
        {
            set { _clientip = value; }
            get { return _clientip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string referUrl
        {
            set { _referurl = value; }
            get { return _referurl; }
        }
        /// <summary>
        /// 初始化时间
        /// </summary>
        public DateTime addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 通道厂商
        /// </summary>
        public int supplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 通道商订单号
        /// </summary>
        public string supplierOrder
        {
            set { _supplierorder = value; }
            get { return _supplierorder; }
        }
        /// <summary>
        /// 1 初始化初状态 2 成功 4 失败 
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// 0 未扣量 1 扣量
        /// </summary>
        public byte deduct
        {
            set { _deduct = value; }
            get { return _deduct; }
        }


        /// <summary>
        /// 和处理时间一样
        /// </summary>
        public DateTime? completetime
        {
            set { _completetime = value; }
            get { return _completetime; }
        }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? processingtime
        {
            set { _processingtime = value; }
            get { return _processingtime; }
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
        /// 供应商给平台的结算价
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
        /// 提交的服务器
        /// </summary>
        public int? server
        {
            set { _server = value; }
            get { return _server; }
        }
        //所属业务员
        public int? manageId { get; set; }
        public Decimal? commission { get; set; }

        DateTime _notifytime = viviLib.TimeControl.FormatConvertor.SqlDateTimeMinValue;
        /// <summary>
        /// 最后通知时间
        /// </summary>
        public DateTime notifytime
        {
            set { _notifytime = value; }
            get { return _notifytime; }
        }

        private string _msg = string.Empty;
        public string msg
        {
            set { _msg = value; }
            get { return _msg; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string version
        {
            set { this._version = value; }
            get { return _version; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string cus_subject
        {
            set { _cus_subject = value; }
            get { return _cus_subject; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cus_price
        {
            set { _cus_price = value; }
            get { return _cus_price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cus_quantity
        {
            set { _cus_quantity = value; }
            get { return _cus_quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cus_description
        {
            set { _cus_description = value; }
            get { return _cus_description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cus_field1
        {
            set { _cus_field1 = value; }
            get { return _cus_field1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cus_field2
        {
            set { _cus_field2 = value; }
            get { return _cus_field2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cus_field3
        {
            set { _cus_field3 = value; }
            get { return _cus_field3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cus_field4
        {
            set { _cus_field4 = value; }
            get { return _cus_field4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cus_field5
        {
            set { _cus_field5 = value; }
            get { return _cus_field5; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string errtype
        {
            set { _errtype = value; }
            get { return _errtype; }
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
