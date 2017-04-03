using System;

namespace viviapi.SysConfig.Menu
{
	/// <summary>
	/// SystemType ��ժҪ˵����
	/// </summary>
	[Serializable]
	public enum SystemType
	{
		/// <summary>
		/// ������
		/// </summary>
		Custom					= 0,

		/// <summary>
		/// ��ҳ����
		/// </summary>
		FirstPage				= 1 ,


		/// <summary>
		/// ���Ź���
		/// </summary>
		News					= 2 ,

		/// <summary>
		/// ���Թ���
		/// </summary>
		LeaveWord				= 3 ,

		/// <summary>
		/// �������롣
		/// </summary>
		OnlineApply				= 4

	}
}
