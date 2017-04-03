using System;

namespace viviapi.Model.Order
{
	/// <summary>
	/// cardwithholds:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Cardwithholds
	{
		public Cardwithholds()
		{}
		#region Model
		private int _id;
		private int _withholdid;
		private string _orderid;
		private int _withhold_type;
		private int _method;
		private decimal _refervalue;
		private decimal _settle;
		private decimal _withhold;
		private decimal _backamt;
		private DateTime _addtime= DateTime.Now;
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
		public int withholdid
		{
			set{ _withholdid=value;}
			get{return _withholdid;}
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
		public int withhold_type
		{
			set{ _withhold_type=value;}
			get{return _withhold_type;}
		}
		/// <summary>
		/// 1 系统返回 2 接口返回
		/// </summary>
		public int method
		{
			set{ _method=value;}
			get{return _method;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal refervalue
		{
			set{ _refervalue=value;}
			get{return _refervalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal settle
		{
			set{ _settle=value;}
			get{return _settle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal withhold
		{
			set{ _withhold=value;}
			get{return _withhold;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal backamt
		{
			set{ _backamt=value;}
			get{return _backamt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime addtime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		#endregion Model

	}
}

