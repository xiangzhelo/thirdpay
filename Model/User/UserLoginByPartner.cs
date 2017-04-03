using System;

namespace viviapi.Model.User
{
	/// <summary>
	/// userLoginByPartner:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserLoginByPartner
	{
        public UserLoginByPartner()
		{}
		#region Model
		private int _id=0;
		private int _userid=0;
		private int _plant=1;
		private string _plantname="QQ";
		private string _openid="";
		private int _available=1;
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
		public int plant
		{
			set{ _plant=value;}
			get{return _plant;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string plantname
		{
			set{ _plantname=value;}
			get{return _plantname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string openid
		{
			set{ _openid=value;}
			get{return _openid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int available
		{
			set{ _available=value;}
			get{return _available;}
		}
		#endregion Model

	}
}

