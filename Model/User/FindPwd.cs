using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.User
{
    /// <summary>
    /// 
    /// </summary>
    public class FindPwd
    {
        #region Model
        private int _id;
        private int? _uid;
        private string _username;
        private string _oldpwd;
        private string _newpwd;
        private int? _status;
        private DateTime? _addtimer;
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
        public int? uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string oldpwd
        {
            set { _oldpwd = value; }
            get { return _oldpwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string newpwd
        {
            set { _newpwd = value; }
            get { return _newpwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? addtimer
        {
            set { _addtimer = value; }
            get { return _addtimer; }
        }
        #endregion Model
    }
}
