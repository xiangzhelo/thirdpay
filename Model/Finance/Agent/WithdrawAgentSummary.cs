using System;

namespace viviapi.Model.Finance.Agent
{
	/// <summary>
	/// withdrawAgentSummary:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class WithdrawAgentSummary
	{
		public WithdrawAgentSummary()
		{}
		#region Model
		private int _id;
		private int _userid=0;
		private string _lotno="";
		private int _qty=0;
		private int _succqty=0;
		private decimal _amt=0M;
		private decimal _succamt=0M;
		private decimal _fee=0M;
		private decimal _realfee=0M;
		private decimal? _totalamt=0M;
		private decimal? _totalsuccamt=0M;
		private int _status=1;
		private int _success=1;
		private int? _audit_status=1;
		private DateTime? _audittime=DateTime.Now;
		private int? _audituser=1;
		private string _auditusername="";
        private DateTime _addtime = DateTime.Now;
        private DateTime _updatetime = DateTime.Now;
		private string _remark="";
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
		public int userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 批次号
		/// </summary>
		public string lotno
		{
			set{ _lotno=value;}
			get{return _lotno;}
		}
		/// <summary>
		/// 应代发条数
		/// </summary>
		public int qty
		{
			set{ _qty=value;}
			get{return _qty;}
		}
		/// <summary>
		/// 成功条数
		/// </summary>
		public int succqty
		{
			set{ _succqty=value;}
			get{return _succqty;}
		}
		/// <summary>
		/// 应代发金额
		/// </summary>
		public decimal amt
		{
			set{ _amt=value;}
			get{return _amt;}
		}
		/// <summary>
		/// 成功金额
		/// </summary>
		public decimal succamt
		{
			set{ _succamt=value;}
			get{return _succamt;}
		}
		/// <summary>
		/// 应付手续费
		/// </summary>
		public decimal fee
		{
			set{ _fee=value;}
			get{return _fee;}
		}
		/// <summary>
		/// 实付手续费
		/// </summary>
		public decimal realfee
		{
			set{ _realfee=value;}
			get{return _realfee;}
		}
		/// <summary>
		/// 应付金额合计
		/// </summary>
		public decimal? totalamt
		{
			set{ _totalamt=value;}
			get{return _totalamt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? totalsuccamt
		{
			set{ _totalsuccamt=value;}
			get{return _totalsuccamt;}
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
		public int success
		{
			set{ _success=value;}
			get{return _success;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? audit_status
		{
			set{ _audit_status=value;}
			get{return _audit_status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? auditTime
		{
			set{ _audittime=value;}
			get{return _audittime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? auditUser
		{
			set{ _audituser=value;}
			get{return _audituser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string auditUserName
		{
			set{ _auditusername=value;}
			get{return _auditusername;}
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
		public DateTime updatetime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
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

