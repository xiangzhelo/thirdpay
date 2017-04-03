using System;
namespace viviapi.Model.Order
{
	/// <summary>
	/// cardwithhold:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Cardwithhold
	{
		public Cardwithhold()
		{}
		#region Model
		private int _id;
		private int _userid;
		private int _cardtype;
		private string _cardno;
		private string _cardpwd;
		private int? _source;
		private int _status;
		private decimal _facevalue;
		private decimal _settle;
		private decimal _withhold;
		private decimal _backamt;
		private int? _supplierid;
		private decimal _supprate;
		private DateTime _addtime= DateTime.Now;
		private DateTime? _updatetime= DateTime.Now;
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
		/// 
		/// </summary>
		public int cardtype
		{
			set{ _cardtype=value;}
			get{return _cardtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cardno
		{
			set{ _cardno=value;}
			get{return _cardno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cardpwd
		{
			set{ _cardpwd=value;}
			get{return _cardpwd;}
		}
		/// <summary>
		/// 来源 1 结算时  2卡密回炉 
		/// </summary>
		public int? source
		{
			set{ _source=value;}
			get{return _source;}
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
		/// 真实面值
		/// </summary>
		public decimal facevalue
		{
			set{ _facevalue=value;}
			get{return _facevalue;}
		}
		/// <summary>
		/// 已结算
		/// </summary>
		public decimal settle
		{
			set{ _settle=value;}
			get{return _settle;}
		}
		/// <summary>
		/// 扣压
		/// </summary>
		public decimal withhold
		{
			set{ _withhold=value;}
			get{return _withhold;}
		}
		/// <summary>
		/// 返回金额
		/// </summary>
		public decimal backamt
		{
			set{ _backamt=value;}
			get{return _backamt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? supplierid
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal supprate
		{
			set{ _supprate=value;}
			get{return _supprate;}
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
		public DateTime? updatetime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion Model

	}
}

