using System;

namespace viviapi.Model.APP
{
	/// <summary>
	/// transfer:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TransferInfo
	{
        public TransferInfo()
		{}
		#region Model
		private int _id;
		private int? _userid;
		private int? _status;
		private string _billingname;
		private int? _bankname;
		private string _province;
		private string _city;
		private string _branch;
		private string _cardnum;
		private string _payee;
		private decimal? _tranamt;
		private decimal? _charges;
		private int? _paybank;
		private string _email;
		private string _mobile;
		private int? _iswarn;
		private int? _warnday;
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
		/// 
		/// </summary>
		public int? userid
		{
			set{ _userid=value;}
			get{return _userid;}
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
		/// 账单名称
		/// </summary>
		public string billingName
		{
			set{ _billingname=value;}
			get{return _billingname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? bankname
		{
			set{ _bankname=value;}
			get{return _bankname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string province
		{
			set{ _province=value;}
			get{return _province;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string city
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string branch
		{
			set{ _branch=value;}
			get{return _branch;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cardnum
		{
			set{ _cardnum=value;}
			get{return _cardnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string payee
		{
			set{ _payee=value;}
			get{return _payee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? tranAmt
		{
			set{ _tranamt=value;}
			get{return _tranamt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? charges
		{
			set{ _charges=value;}
			get{return _charges;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? paybank
		{
			set{ _paybank=value;}
			get{return _paybank;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? iswarn
		{
			set{ _iswarn=value;}
			get{return _iswarn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? warnday
		{
			set{ _warnday=value;}
			get{return _warnday;}
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

