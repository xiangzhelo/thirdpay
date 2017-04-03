using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Sys
{
    [Serializable]
    public class debuginfo
    {
        private int _id;
        private debugtypeenum _bugtype;
        private int? _userid;
        private string _url;
        private string _errorcode;
        private string _errorinfo;
        private string _detail;
        private DateTime? _addtime;
        private string _userorder;

        public debuginfo()
        {

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
        /// 错误类型
        /// </summary>
        public debugtypeenum bugtype
        {
            set { _bugtype = value; }
            get { return _bugtype; }
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
        public string url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string errorcode
        {
            set { _errorcode = value; }
            get { return _errorcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string errorinfo
        {
            set { _errorinfo = value; }
            get { return _errorinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string detail
        {
            set { _detail = value; }
            get { return _detail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string userorder
        {
            set { _userorder = value; }
            get { return _userorder; }
        }
    }
}
