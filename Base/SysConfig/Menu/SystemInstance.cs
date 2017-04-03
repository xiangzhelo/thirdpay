using System;
using System.Collections.Generic;
namespace viviapi.SysConfig.Menu
{
	/// <summary>
	/// SystemInstance 的摘要说明。
	/// </summary>
	[Serializable]
	public class SystemInstance
	{
		private int _systemId					= 0;
		private int _parentId					= 0;
		private SystemType _systemType			= SystemType.Custom;
		private string _name					= string.Empty;
		private bool _onlyForAdmin				= false;
		private bool _release					= true;


        private List<SystemInstance> _items = new List<SystemInstance>();

		/// <summary>
		/// 构造函数。
		/// </summary>
		public SystemInstance()
		{
		}

		/// <summary>
		/// 系统编号。
		/// </summary>
		public int SystemId
		{
			get{return this._systemId;}
			set{this._systemId	= value;}
		}

		/// <summary>
		/// 父系统编号。
		/// </summary>
		public int ParentId
		{
			get{return this._parentId;}
			set{this._parentId	= value;}
		}

		/// <summary>
		/// 系统类型。
		/// </summary>
		public SystemType SystemType
		{
			get{return this._systemType;}
			set{this._systemType	= value;}
		}

		/// <summary>
		/// 系统名称。
		/// </summary>
		public string Name
		{
			get{return this._name;}
			set{this._name	= value;}
		}

		/// <summary>
		/// 是否只有管理员有权限。
		/// </summary>
		public bool OnlyForAdmin
		{
			get{return this._onlyForAdmin;}
			set{this._onlyForAdmin	= value;}
		}

		
		/// <summary>
		/// 是否可用。
		/// </summary>
		public bool Release
		{
			get{return this._release;}
			set{this._release	= value;}
		}


		/// <summary>
		/// 下级系统。
		/// </summary>
        public List<SystemInstance> Items
		{
			get
			{
				return _items;
			}
		}
	}
}
