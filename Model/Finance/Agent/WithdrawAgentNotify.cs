using System;

namespace viviapi.Model.Finance.Agent
{
	/// <summary>
	/// withdrawAgentNotify:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class WithdrawAgentNotify
	{
		public WithdrawAgentNotify()
		{}
		#region Model
		private int _id;
		private string _notify_id;
		private int? _userid;
		private string _trade_no;
		private string _out_trade_no;
		private int? _notifystatus;
		private string _notifyurl;
		private string _restext;
		private DateTime _addtime;
		private DateTime? _restime;
		private string _ext1;
		private string _ext2;
		private string _ext3;
		private string _remark;
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
		public string notify_id
		{
			set{ _notify_id=value;}
			get{return _notify_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string trade_no
		{
			set{ _trade_no=value;}
			get{return _trade_no;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string out_trade_no
		{
			set{ _out_trade_no=value;}
			get{return _out_trade_no;}
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
		public string notifyurl
		{
			set{ _notifyurl=value;}
			get{return _notifyurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string resText
		{
			set{ _restext=value;}
			get{return _restext;}
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
		public DateTime? resTime
		{
			set{ _restime=value;}
			get{return _restime;}
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
		#endregion Model

	}
}

