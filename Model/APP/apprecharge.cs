using System;

namespace viviapi.Model.APP
{
	/// <summary>
	/// apprecharge:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Recharge
	{
        public Recharge()
		{}
		#region Model
		private int _id = 0;
		private int _paytype = 0;
		private int _rechtype = 0;
		private string _orderid = string.Empty;
        private string _account = string.Empty;
		private int _userid = 0;
		private decimal _rechargeamt = 0M;
		private decimal? _realpayamt = 0M;
		private DateTime _addtime = DateTime.Now;
		private int _status = 0;
		private int _processstatus = 0;
		private DateTime? _processtime = DateTime.Now;
		private bool _smsnotification = false;
		private string _field1 = string.Empty;
		private string _field2 = string.Empty;
		private string _remark = string.Empty;
        private int _suppid = 0;

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
		public int paytype
		{
			set{ _paytype=value;}
			get{return _paytype;}
		}
       
		/// <summary>
		/// 充值类型 1 手机 2 游戏
		/// </summary>
		public int rechtype
		{
			set{ _rechtype=value;}
			get{return _rechtype;}
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
		public string account
		{
			set{ _account=value;}
			get{return _account;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal rechargeAmt
		{
			set{ _rechargeamt=value;}
			get{return _rechargeamt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? realPayAmt
		{
			set{ _realpayamt=value;}
			get{return _realpayamt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime addtime
		{
			set{ _addtime=value;}
			get{return _addtime;}
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
		public int processstatus
		{
			set{ _processstatus=value;}
			get{return _processstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? processtime
		{
			set{ _processtime=value;}
			get{return _processtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool smsnotification
		{
			set{ _smsnotification=value;}
			get{return _smsnotification;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string field1
		{
			set{ _field1=value;}
			get{return _field1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string field2
		{
			set{ _field2=value;}
			get{return _field2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int suppid
        {
            set { _suppid = value; }
            get { return _suppid; }
        }
		#endregion Model

	}
}

