using System;

namespace viviapi.Model.User
{
	/// <summary>
	/// userspaybank:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SettlementAccount
	{
        public SettlementAccount()
		{}
		#region Model
		private int _id;
		private int _userid;
		private int _accouttype=0;
		private int _pmode=1;
		private string _account;
		private string _payeename;
		private string _bankcode;
		private string _payeebank;
		private string _provincecode;
		private string _bankprovince;
		private string _citycode;
		private string _bankcity;
		private string _bankaddress;
		private int? _status;
		private DateTime _addtime;
		private DateTime? _updatetime;
        
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
		public int Userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 1对公 0对私
		/// </summary>
		public int AccoutType
		{
			set{ _accouttype=value;}
			get{return _accouttype;}
		}
		/// <summary>
		/// 收款方式 1 银行帐户 2 支付宝 3 财付通
		/// </summary>
		public int Pmode
		{
			set{ _pmode=value;}
			get{return _pmode;}
		}
        public string PmodeName
        {
            get
            {
                string _name = string.Empty;
                switch (Pmode)
                {
                    case 1:
                        _name = "银行帐户";
                        break;
                    case 2:
                        _name = "支付宝";
                        break;
                    case 3:
                        _name = "财付通";
                        break;
                }
                return _name;
            }
        }
		/// <summary>
		/// 
		/// </summary>
		public string Account
		{
			set{ _account=value;}
			get{return _account;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PayeeName
		{
			set{ _payeename=value;}
			get{return _payeename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankCode
		{
			set{ _bankcode=value;}
			get{return _bankcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PayeeBank
		{
			set{ _payeebank=value;}
			get{return _payeebank;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProvinceCode
		{
			set{ _provincecode=value;}
			get{return _provincecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankProvince
		{
			set{ _bankprovince=value;}
			get{return _bankprovince;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CityCode
		{
			set{ _citycode=value;}
			get{return _citycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankCity
		{
			set{ _bankcity=value;}
			get{return _bankcity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankAddress
		{
			set{ _bankaddress=value;}
			get{return _bankaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion Model

	}
}

