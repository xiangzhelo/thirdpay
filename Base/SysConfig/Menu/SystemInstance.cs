using System;
using System.Collections.Generic;
namespace viviapi.SysConfig.Menu
{
	/// <summary>
	/// SystemInstance ��ժҪ˵����
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
		/// ���캯����
		/// </summary>
		public SystemInstance()
		{
		}

		/// <summary>
		/// ϵͳ��š�
		/// </summary>
		public int SystemId
		{
			get{return this._systemId;}
			set{this._systemId	= value;}
		}

		/// <summary>
		/// ��ϵͳ��š�
		/// </summary>
		public int ParentId
		{
			get{return this._parentId;}
			set{this._parentId	= value;}
		}

		/// <summary>
		/// ϵͳ���͡�
		/// </summary>
		public SystemType SystemType
		{
			get{return this._systemType;}
			set{this._systemType	= value;}
		}

		/// <summary>
		/// ϵͳ���ơ�
		/// </summary>
		public string Name
		{
			get{return this._name;}
			set{this._name	= value;}
		}

		/// <summary>
		/// �Ƿ�ֻ�й���Ա��Ȩ�ޡ�
		/// </summary>
		public bool OnlyForAdmin
		{
			get{return this._onlyForAdmin;}
			set{this._onlyForAdmin	= value;}
		}

		
		/// <summary>
		/// �Ƿ���á�
		/// </summary>
		public bool Release
		{
			get{return this._release;}
			set{this._release	= value;}
		}


		/// <summary>
		/// �¼�ϵͳ��
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
