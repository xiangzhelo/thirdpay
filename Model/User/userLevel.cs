using System;

namespace viviapi.Model.User
{
    /// <summary>
    /// userLevel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class UserLevel
    {
        public UserLevel()
        { }

        #region Model
        private int _id = 0;
        private int? _ratetype = 0;
        private int _level = 0;
        private string _levname;
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
        public int? rateType
        {
            set { _ratetype = value; }
            get { return _ratetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int level
        {
            set { _level = value; }
            get { return _level; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string levName
        {
            set { _levname = value; }
            get { return _levname; }
        }
        #endregion Model

    }
}

