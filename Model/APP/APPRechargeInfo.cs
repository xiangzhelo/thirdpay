using System;
namespace viviapi.Model.APP
{
	/// <summary>
	/// 应该充值:实体类(属性说明自动提取数据库字段的描述信息)
    /// 应用充值
	/// </summary>
	[Serializable]
	public partial class APPRechargeInfo
	{
        public APPRechargeInfo()
		{}

		#region Model
		private int _id;
		private int? _rechtype;
		private string _orderid;
		private string _account;
		private int _userid;
		private decimal _rechargeamt;
		private decimal? _realpayamt;
		private DateTime? _addtime;
		private int? _status;
		private int? _processstatus;
		private DateTime? _processtime;
		private bool _smsnotification;
		private string _field1;
		private string _field2;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 充值类型 1 手机 2 游戏
		/// </summary>
		public int? rechtype
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
		public DateTime? addtime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? processstatus
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
		#endregion Model

	}
}

