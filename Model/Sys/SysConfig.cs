using System;

namespace viviapi.Model.Sys
{
	/// <summary>
	/// sysconfig:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysConfig
	{
		public SysConfig()
		{}
		#region Model
		private int _id;
		private int? _type;
		private string _key;
		private string _value;
		private string _desc;
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
		public int? type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string key
		{
			set{ _key=value;}
			get{return _key;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string value
		{
			set{ _value=value;}
			get{return _value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string desc
		{
			set{ _desc=value;}
			get{return _desc;}
		}
		#endregion Model

	}
}

