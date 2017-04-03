using System;

namespace viviapi.Model.Order.Bank
{
	/// <summary>
	/// orderbankcodepay:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderBankCodePay
	{
        public OrderBankCodePay()
		{}
		#region Model
		private int _id;
		private string _sysorderno;
		private int _channel;
		private string _codeimgurl;
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
		public string sysOrderNo
		{
			set{ _sysorderno=value;}
			get{return _sysorderno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int channel
		{
			set{ _channel=value;}
			get{return _channel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string codeImgUrl
		{
			set{ _codeimgurl=value;}
			get{return _codeimgurl;}
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
		public DateTime? updateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion Model

	}
}

