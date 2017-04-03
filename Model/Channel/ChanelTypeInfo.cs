using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.Channel
{
    /// <summary>
    /// 
    /// </summary>
    public enum EnumChannelType
    {
        None = 0,
        财付通 = 100,
        支付宝 = 101,
        网上银行 = 102,
        神州行充值卡 = 103,
        盛大一卡通 = 104,
        征途支付卡 = 105,
        骏网一卡通 = 106,
        腾讯Q币卡 = 107,
        联通充值卡 = 108,
        久游一卡通 = 109,
        网易一卡通 = 110,
        完美一卡通 = 111,
        搜狐一卡通 = 112,
        电信充值卡 = 113,
        声讯卡 = 114,
        光宇一卡通 = 115,
        金山一卡通 = 116,
        纵游一卡通 = 117,
        天下一卡通 = 118,
        天宏一卡通 = 119,
        神州行浙江卡 = 200,
        神州行江苏卡 = 201,
        神州行辽宁卡 = 202,
        神州行福建卡 = 203,
        魔兽卡 = 204,
        联华卡 = 205,
        殴飞一卡通 = 208,
        天下一卡通专项 = 209,
        短信 = 300
    }

    /// <summary>
    /// 通道类别
    /// </summary>
    [Serializable]
    public class ChannelTypeInfo
    {
        private int _id;
        private ChannelClassEnum _class;
        private string _modetypename;
        private string _code=string.Empty;
        private int _typeId;
        private OpenEnum _isopen;
        private DateTime _addtime;
        private int? _sort;
        private bool _release;
        private int _supplier;
        private decimal _supprate = 0M;
        private int _runmode = 0;
        private string _runset = string.Empty;
        private int _supplier2;
        private string _suppsWhenExceOccurred = string.Empty;
        private int _timeout;


        public int timeout
        {
            set { _timeout = value; }
            get { return _timeout; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 代号
        /// </summary>
        public int typeId
        {
            set { _typeId = value; }
            get { return _typeId; }
        }

        /// <summary>
        /// 代号
        /// </summary>
        public string code
        {
            set { _code = value; }
            get { return _code; }
        }

        /// <summary>
        /// 供应商费率
        /// </summary>
        public decimal supprate
        {
            set { _supprate = value; }
            get { return _supprate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        /// <summary>
        /// 支付通道类别 1 网银 2游戏点卡 3充值卡
        /// </summary>
        public ChannelClassEnum Class
        {
            set { _class = value; }
            get { return _class; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string modetypename
        {
            set { _modetypename = value; }
            get { return _modetypename; }
        }
        
        /// <summary>
        /// 是否开启
        /// </summary>
        public OpenEnum isOpen
        {
            set { _isopen = value; }
            get { return _isopen; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool release
        {
            set { _release = value; }
            get { return _release; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int runmode
        {
            set { _runmode = value; }
            get { return _runmode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string runset
        {
            set { _runset = value; }
            get { return _runset; }
        }

        /// <summary>
        /// 辅助接口 主接口 返回超时切换通道再提交
        /// </summary>
        public int supplier2
        {
            set { _supplier2 = value; }
            get { return _supplier2; }
        }

        /// <summary>
        /// 当主接口提交发生错误时切换通道
        /// </summary>
        public string SuppsWhenExceOccurred
        {
            set { _suppsWhenExceOccurred = value; }
            get { return _suppsWhenExceOccurred; }
        }
    }
}
