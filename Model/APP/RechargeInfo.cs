using System;
namespace viviapi.Model.APP
{
	/// <summary>
	/// recharge:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RechargeInfo
	{
        public RechargeInfo()
		{}
		#region Model
		private int _id;
		private int? _userid;
		private string _orderno;
		private decimal? _rechargeamt;
		private decimal? _balance;
		private DateTime? _addtime;
		private int? _status;
		private DateTime? _paytime;
		private string _transno;
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
		/// 
		/// </summary>
		public int? userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 订单号
		/// </summary>
		public string orderno
		{
			set{ _orderno=value;}
			get{return _orderno;}
		}
		/// <summary>
		/// 充值金额
		/// </summary>
		public decimal? rechargeAmt
		{
			set{ _rechargeamt=value;}
			get{return _rechargeamt;}
		}
		/// <summary>
		/// 充值时的余额
		/// </summary>
		public decimal? balance
		{
			set{ _balance=value;}
			get{return _balance;}
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
		/// 状态
		/// </summary>
		public int? status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 支付时间
		/// </summary>
		public DateTime? paytime
		{
			set{ _paytime=value;}
			get{return _paytime;}
		}
		/// <summary>
		/// 交易号
		/// </summary>
		public string transNo
		{
			set{ _transno=value;}
			get{return _transno;}
		}
		/// <summary>
		/// 备注说明
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

