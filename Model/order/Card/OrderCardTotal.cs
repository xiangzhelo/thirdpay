using System;

namespace viviapi.Model.Order.Card
{
	/// <summary>
	/// ordercardtotal:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderCardTotal
	{
        public OrderCardTotal()
		{}

		#region Model
		private int _id=0;
		private string _orderid="";
		private string _userorderid="";
		private int _userid=0;
		private int _typeid=0;
		private int _cardtype=0;
		private string _cardnos="";
		private string _cardpwds="";
		private int _cardnum=1;
		private int _success=1;
		private decimal _orderamt=0M;
		private decimal _successamt=0M;
		private string _cardstatus="";
		private string _realamts="";
		private int _status=1;
		private int _notifystatus=1;
		private string _version;
		private string _filed1="";
		private string _filed2="";
		private string _filed3="";
		private string _filed4="";
		private DateTime? _addtime=DateTime.Now;
		private DateTime? _completiontime=DateTime.Now;

        
		private string _referurl="";
		private string _notifyurl="";
        private string _attach = "";

		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string orderid
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userorderid
		{
			set{ _userorderid=value;}
			get{return _userorderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int userId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int typeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int cardType
		{
			set{ _cardtype=value;}
			get{return _cardtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cardNos
		{
			set{ _cardnos=value;}
			get{return _cardnos;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cardPwds
		{
			set{ _cardpwds=value;}
			get{return _cardpwds;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int cardNum
		{
			set{ _cardnum=value;}
			get{return _cardnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int success
		{
			set{ _success=value;}
			get{return _success;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal orderAmt
		{
			set{ _orderamt=value;}
			get{return _orderamt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal successAmt
		{
			set{ _successamt=value;}
			get{return _successamt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cardStatus
		{
			set{ _cardstatus=value;}
			get{return _cardstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string realAmts
		{
			set{ _realamts=value;}
			get{return _realamts;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int notifystatus
		{
			set{ _notifystatus=value;}
			get{return _notifystatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string version
		{
			set{ _version=value;}
			get{return _version;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string filed1
		{
			set{ _filed1=value;}
			get{return _filed1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string filed2
		{
			set{ _filed2=value;}
			get{return _filed2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string filed3
		{
			set{ _filed3=value;}
			get{return _filed3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string filed4
		{
			set{ _filed4=value;}
			get{return _filed4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? addTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? completionTime
		{
			set{ _completiontime=value;}
			get{return _completiontime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string referUrl
		{
			set{ _referurl=value;}
			get{return _referurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string notifyUrl
		{
			set{ _notifyurl=value;}
			get{return _notifyurl;}
		}

        /// <summary>
        /// 
        /// </summary>
        public string attach
        {
            set { _attach = value; }
            get { return _attach; }
        }
		#endregion Model

	}
}

