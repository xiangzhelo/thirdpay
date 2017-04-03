using System;

namespace viviapi.Model.Sys
{
	/// <summary>
	/// sysMailConfig:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public class SysMailConfig
	{
		public SysMailConfig()
		{}

		#region Model
		private int _id;
		private string _host;
		private int _port;
		private string _username;
		private string _password;
		private byte _enablessl;
        private byte _usedefaultcredentials;
		private int _sort;
        private byte _used = 0;
	    private string _address = "";
	    private string _displayName = "";

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
		public string host
		{
			set{ _host=value;}
			get{return _host;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int port
		{
			set{ _port=value;}
			get{return _port;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string username
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte enableSsl
		{
			set{ _enablessl=value;}
			get{return _enablessl;}
		}
		/// <summary>
		/// 
		/// </summary>
        public byte useDefaultCredentials
		{
			set{ _usedefaultcredentials=value;}
			get{return _usedefaultcredentials;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}


        /// <summary>
        /// 
        /// </summary>
        public byte used
        {
            set { _used = value; }
            get { return _used; }
        }

        public string displayName
        {
            set { _displayName = value; }
            get { return _displayName; }
        }

        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
		#endregion Model

	}
}

