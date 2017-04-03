using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.Channel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ChannelTypeUserInfo
    {
        private int _id;
        private int _typeId;
        private int _userid;
        private int? _suppid;
        private bool? _userisopen;
        private bool? _sysisopen;
        private DateTime? _addtime = DateTime.Now;
        private DateTime? _updatetime = DateTime.Now;
        
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int typeId
        {
            set { _typeId = value; }
            get { return _typeId; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int userId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? userIsOpen
        {
            set { _userisopen = value; }
            get { return _userisopen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? sysIsOpen
        {
            set { _sysisopen = value; }
            get { return _sysisopen; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? addTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? updateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 用户接口商
        /// </summary>
        public int? suppid
        {
            set { _suppid = value; }
            get { return _suppid; }
        }
    }
}
