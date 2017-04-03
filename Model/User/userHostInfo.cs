using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.User
{
    public enum UserHostStatus
    {
        未知 = 0,
        已开启 = 1,
        已关闭 = 2
    }
    [Serializable]
    public class UserHostInfo
    {
        private int _id;
        private int? _userid;
        private string _siteip;
        private int? _sitetype;
        private string _hostname;
        private string _hosturl = "http://";
        private string _desc;
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
        public int? userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string siteip
        {
            set { _siteip = value; }
            get { return _siteip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? sitetype
        {
            set { _sitetype = value; }
            get { return _sitetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string hostName
        {
            set { _hostname = value; }
            get { return _hostname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string hostUrl
        {
            set { _hosturl = value; }
            get { return _hosturl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string desc
        {
            set { _desc = value; }
            get { return _desc; }
        }
        //状态
        public UserHostStatus status { get; set; }
    }
}
