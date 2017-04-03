using System;
using System.Collections.Generic;

namespace viviapi.SysConfig.Menu
{
	/// <summary>
	/// 菜单。
	/// </summary>
	[Serializable]
	public class MenuItem
	{
		private string _text				= string.Empty;
		private string _link				= string.Empty;
		private int _systemId				= 0;
		private bool _onlyForAdmin			= false;
		private int[] _requirePowers		= new int[0];

        private List<MenuItem> _items = new List<MenuItem>();


		/// <summary>
		/// 构造函数。
		/// </summary>
		public MenuItem()
		{
		}

		/// <summary>
		/// 名称。
		/// </summary>
		public string Text
		{
			get{return this._text;}
			set{this._text	= value;}
		}

		/// <summary>
		/// 链接。
		/// </summary>
		public string Link
		{
			get{return this._link;}
			set{this._link	= value;}
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
		/// 是否只对系统管理员有效。
		/// </summary>
		public bool OnlyForAdmin
		{
			get{return this._onlyForAdmin;}
			set{this._onlyForAdmin	= value;}
		}

		/// <summary>
		/// 所需权限编号。
		/// </summary>
		public int[] RequirePowers
		{
			get{return this._requirePowers;}
			set{this._requirePowers	= value;}
		}

		/// <summary>
		/// 下级节点。
		/// </summary>
        public List<MenuItem> Items
		{
			get{return this._items;}
		}
	}
}
