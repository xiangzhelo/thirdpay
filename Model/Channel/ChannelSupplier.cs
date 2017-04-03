using System;
namespace viviapi.Model.Channel
{
	/// <summary>
	/// channelsupplier:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ChannelSupplier
	{
        public ChannelSupplier()
		{}
		#region Model
		private int _typeid;
		private int _suppid;
		private int _userid;
		private bool _isopen;
        private bool _isdefault;
		private decimal _payrate;
		/// <summary>
		/// 
		/// </summary>
		public int typeid
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int suppid
		{
			set{ _suppid=value;}
			get{return _suppid;}
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
		public bool isopen
		{
			set{ _isopen=value;}
			get{return _isopen;}
		}
        /// <summary>
        /// 
        /// </summary>
        public bool isdefault
        {
            set { _isdefault = value; }
            get { return _isdefault; }
        }
		/// <summary>
		/// 
		/// </summary>
		public decimal payrate
		{
			set{ _payrate=value;}
			get{return _payrate;}
		}
		#endregion Model

	}
}

