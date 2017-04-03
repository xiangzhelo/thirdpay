using System;

namespace viviapi.Model.Supplier
{
	/// <summary>
	/// supplierAccount:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SupplierAccount
	{
		public SupplierAccount()
		{}
		#region Model
		private int _id=0;
		private int _code;
		private string _name;
		private string _apiaccount;
		private string _apikey;
		private string _username;
		private string _email;
		private string _domain;
		private string _jumpdomain;
		private bool _available;
		private int _isdefault;
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
		public int code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string apiAccount
		{
			set{ _apiaccount=value;}
			get{return _apiaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string apiKey
		{
			set{ _apikey=value;}
			get{return _apikey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userName
		{
			set{ _username=value;}
			get{return _username;}
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
		public string domain
		{
			set{ _domain=value;}
			get{return _domain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string jumpdomain
		{
			set{ _jumpdomain=value;}
			get{return _jumpdomain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool available
		{
			set{ _available=value;}
			get{return _available;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int isdefault
		{
			set{ _isdefault=value;}
			get{return _isdefault;}
		}
		#endregion Model

	}
}

