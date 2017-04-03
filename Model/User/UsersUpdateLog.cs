using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersUpdateLog
    {
        #region Model
        private int _id;
        private int _userid;
        private string _field;
        private string _oldvalue;
        private string _newvalue;
        private string _editor=string.Empty;
        private string _oIp = string.Empty;
        private string _desc = string.Empty;
        private DateTime _addtime;

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
        public int userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string field
        {
            set { _field = value; }
            get { return _field; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Desc
        {
            set { _desc = value; }
            get { return _desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Editor
        {
            set { _editor = value; }
            get { return _editor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OIp
        {
            set { _oIp = value; }
            get { return _oIp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string oldValue
        {
            set { _oldvalue = value; }
            get { return _oldvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string newvalue
        {
            set { _newvalue = value; }
            get { return _newvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        #endregion Model
    }
}