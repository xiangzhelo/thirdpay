using System;

namespace viviapi.Model.Finance.Agent
{
	/// <summary>
	/// withdrawAgent:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class WithdrawAgent
	{
		public WithdrawAgent()
		{}

		#region Model
		private int _id;
		private int _mode;
		private string _lotno;
		private int _serial=1;
		private string _trade_no="";
		private string _out_trade_no="";
		private string _service="";
		private string _input_charset="";
		private int _userid=0;
		private string _sign_type="";
		private string _return_url="";
		private string _provincecode="";
		private string _citycode="";
		private string _bankprovince="";
		private string _bankcity="";
		private string _bankcode="";
		private string _bankname="";
		private string _bankbranch="";
		private string _bankaccountname="";
		private int? _bankaccouttype=0;
		private string _bankaccount="";
		private decimal _amount=0M;
		private decimal? _charge=0M;
		private decimal? _totalamt=0M;
		private DateTime _addtime=DateTime.Now;
		private DateTime _processingtime=DateTime.Now;
		private int? _audit_status=0;
		private DateTime? _audittime;
		private int? _audituser=0;
		private string _auditusername="";
		private int? _payment_status=0;
		private bool _is_cancel=false;
		private string _ext1="";
		private string _ext2="";
		private string _ext3="";
		private string _remark="";
		private int _tranapi=0;
		private int? _suppstatus=0;
		private int? _notifytimes=0;
		private int? _notifystatus=0;
		private string _callbacktext="";
		private int? _issure=0;
		private DateTime? _suretime;
		private string _sureclientip="";
		private string _sureuser="";

		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 1 API 2 手动添加
		/// </summary>
		public int mode
		{
			set{ _mode=value;}
			get{return _mode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lotno
		{
			set{ _lotno=value;}
			get{return _lotno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int serial
		{
			set{ _serial=value;}
			get{return _serial;}
		}
		/// <summary>
		/// 系统流水号
		/// </summary>
		public string trade_no
		{
			set{ _trade_no=value;}
			get{return _trade_no;}
		}
		/// <summary>
		/// 商户订单号
		/// </summary>
		public string out_trade_no
		{
			set{ _out_trade_no=value;}
			get{return _out_trade_no;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string service
		{
			set{ _service=value;}
			get{return _service;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string input_charset
		{
			set{ _input_charset=value;}
			get{return _input_charset;}
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
		public string sign_type
		{
			set{ _sign_type=value;}
			get{return _sign_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string return_url
		{
			set{ _return_url=value;}
			get{return _return_url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string provinceCode
		{
			set{ _provincecode=value;}
			get{return _provincecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cityCode
		{
			set{ _citycode=value;}
			get{return _citycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bankProvince
		{
			set{ _bankprovince=value;}
			get{return _bankprovince;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bankCity
		{
			set{ _bankcity=value;}
			get{return _bankcity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bankCode
		{
			set{ _bankcode=value;}
			get{return _bankcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bankName
		{
			set{ _bankname=value;}
			get{return _bankname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bankBranch
		{
			set{ _bankbranch=value;}
			get{return _bankbranch;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bankAccountName
		{
			set{ _bankaccountname=value;}
			get{return _bankaccountname;}
		}
		/// <summary>
		/// 1对公 0对私
		/// </summary>
		public int? bankaccoutType
		{
			set{ _bankaccouttype=value;}
			get{return _bankaccouttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bankAccount
		{
			set{ _bankaccount=value;}
			get{return _bankaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? charge
		{
			set{ _charge=value;}
			get{return _charge;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? totalamt
		{
			set{ _totalamt=value;}
			get{return _totalamt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime addTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime processingTime
		{
			set{ _processingtime=value;}
			get{return _processingtime;}
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
		public int? payment_status
		{
			set{ _payment_status=value;}
			get{return _payment_status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool is_cancel
		{
			set{ _is_cancel=value;}
			get{return _is_cancel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ext1
		{
			set{ _ext1=value;}
			get{return _ext1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ext2
		{
			set{ _ext2=value;}
			get{return _ext2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ext3
		{
			set{ _ext3=value;}
			get{return _ext3;}
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
		public int tranApi
		{
			set{ _tranapi=value;}
			get{return _tranapi;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? suppstatus
		{
			set{ _suppstatus=value;}
			get{return _suppstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? notifyTimes
		{
			set{ _notifytimes=value;}
			get{return _notifytimes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? notifystatus
		{
			set{ _notifystatus=value;}
			get{return _notifystatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string callbackText
		{
			set{ _callbacktext=value;}
			get{return _callbacktext;}
		}
		/// <summary>
		/// 用户确认
		/// </summary>
		public int? issure
		{
			set{ _issure=value;}
			get{return _issure;}
		}
		/// <summary>
		/// 确认时间
		/// </summary>
		public DateTime? suretime
		{
			set{ _suretime=value;}
			get{return _suretime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sureclientip
		{
			set{ _sureclientip=value;}
			get{return _sureclientip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sureuser
		{
			set{ _sureuser=value;}
			get{return _sureuser;}
		}
		#endregion Model

	}
}

