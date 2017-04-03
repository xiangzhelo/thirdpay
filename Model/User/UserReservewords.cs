using System;

namespace viviapi.Model.User
{
	/// <summary>
	/// userreservewords:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserReservewords
	{
        public UserReservewords()
		{}
		#region Model
		private int? _userid;
		private string _reservewords;
		private DateTime? _addtime = DateTime.Now;
        private DateTime? _updatetime = DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int? userId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string reservewords
		{
			set{ _reservewords=value;}
			get{return _reservewords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? addTime
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

