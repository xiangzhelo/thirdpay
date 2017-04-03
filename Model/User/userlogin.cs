using System;

namespace viviapi.Model.User
{
	/// <summary>
	/// userlogin:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserLogin
	{
        public UserLogin()
		{}
		#region Model
		private int _id;
		private int _userid;
		private string _lastloginip;
		private DateTime? _lastlogintime;
		private string _currentloginip;
		private DateTime? _currentlogintime;
		private string _sessionid;
		private int? _logintype;
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
		public int userId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lastLoginIp
		{
			set{ _lastloginip=value;}
			get{return _lastloginip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? lastLoginTime
		{
			set{ _lastlogintime=value;}
			get{return _lastlogintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string currentLoginIp
		{
			set{ _currentloginip=value;}
			get{return _currentloginip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? currentLoginTime
		{
			set{ _currentlogintime=value;}
			get{return _currentlogintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sessionId
		{
			set{ _sessionid=value;}
			get{return _sessionid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? loginType
		{
			set{ _logintype=value;}
			get{return _logintype;}
		}
		#endregion Model

	}
}

