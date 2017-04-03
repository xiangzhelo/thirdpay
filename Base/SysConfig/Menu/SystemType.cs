using System;

namespace viviapi.SysConfig.Menu
{
	/// <summary>
	/// SystemType 的摘要说明。
	/// </summary>
	[Serializable]
	public enum SystemType
	{
		/// <summary>
		/// 其他。
		/// </summary>
		Custom					= 0,

		/// <summary>
		/// 首页管理。
		/// </summary>
		FirstPage				= 1 ,


		/// <summary>
		/// 新闻管理。
		/// </summary>
		News					= 2 ,

		/// <summary>
		/// 留言管理。
		/// </summary>
		LeaveWord				= 3 ,

		/// <summary>
		/// 在线申请。
		/// </summary>
		OnlineApply				= 4

	}
}
