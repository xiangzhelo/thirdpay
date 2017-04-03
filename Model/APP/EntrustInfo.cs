using System;
namespace viviapi.Model.APP
{
	/// <summary>
    /// 委托支付:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class EntrustInfo
	{
        public EntrustInfo()
		{}
		#region Model
		private int _id;
		private int _userid;
		private int _status;
		private string _bankcardnum;
		private string _bankname;
		private string _payee;
		private decimal _amount;
		private decimal _rate;
		private decimal _remittancefee;
		private decimal _totalamt;
		private DateTime _addtime;
		private DateTime? _cdate;
		private int? _cadmin;
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
		/// 商户
		/// </summary>
		public int userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 状态1 初始状态 2 成功 4 失败
		/// </summary>
		public int status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 银行卡号
		/// </summary>
		public string bankcardnum
		{
			set{ _bankcardnum=value;}
			get{return _bankcardnum;}
		}
		/// <summary>
		/// 开户银行
		/// </summary>
		public string bankname
		{
			set{ _bankname=value;}
			get{return _bankname;}
		}
		/// <summary>
		/// 开户人
		/// </summary>
		public string payee
		{
			set{ _payee=value;}
			get{return _payee;}
		}
		/// <summary>
		/// 委托金额
		/// </summary>
		public decimal amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 费率
		/// </summary>
		public decimal rate
		{
			set{ _rate=value;}
			get{return _rate;}
		}
		/// <summary>
		/// 汇费
		/// </summary>
		public decimal remittancefee
		{
			set{ _remittancefee=value;}
			get{return _remittancefee;}
		}
		/// <summary>
		/// 合计
		/// </summary>
		public decimal totalAmt
		{
			set{ _totalamt=value;}
			get{return _totalamt;}
		}
		/// <summary>
		/// 申请时间
		/// </summary>
		public DateTime addtime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 确认时间
		/// </summary>
		public DateTime? cdate
		{
			set{ _cdate=value;}
			get{return _cdate;}
		}
		/// <summary>
		/// 确认人
		/// </summary>
		public int? cadmin
		{
			set{ _cadmin=value;}
			get{return _cadmin;}
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

