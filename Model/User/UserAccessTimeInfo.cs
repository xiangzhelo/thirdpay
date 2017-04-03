using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.User
{
    /// <summary>
    /// 用户访问时间
    /// </summary>
    [Serializable]
    public class UserAccessTimeInfo
    {
        private int _userid;
        private DateTime _lastaccesstime = DateTime.MinValue;

        /// <summary>
        /// 
        /// </summary>
        public int userid
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime lastAccesstime
        {
            set { _lastaccesstime = value; }
            get { return _lastaccesstime; }
        }
    }
}
