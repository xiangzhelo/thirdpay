using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Manage
    {
        private int _id;
        private string _username;
        private string _password;
        private string _secondpwd;
        private ManageRole _role;
        private int? _status;
        private string _relname;
        private string _lastloginip;
        private DateTime? _lastlogintime;
        private string _lastloginaddress = string.Empty;
        private string _lastloginremark = string.Empty;    
        private string _sessionid;
        private int _isSuperAdmin = 0;
        private int _isAgent = 0;
       
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
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string secondpwd
        {
            set { _secondpwd = value; }
            get { return _secondpwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public ManageRole role
        {
            set { _role = value; }
            get { return _role; }
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
        public string relname
        {
            set { _relname = value; }
            get { return _relname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastLoginIp
        {
            set { _lastloginip = value; }
            get { return _lastloginip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? lastLoginTime
        {
            set { _lastlogintime = value; }
            get { return _lastlogintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sessionid
        {
            set { _sessionid = value; }
            get { return _sessionid; }
        }

        public int? commissiontype { get; set; }
        public decimal? commission { get; set; }
        public decimal? cardcommission { get; set; }

        #region 最后登录地点
        /// <summary>
        /// 最后登录地点
        /// </summary>
        public string LastLoginAddress
        {
            get
            {
                return this._lastloginaddress;
            }
            set
            {
                this._lastloginaddress = value;
            }
        }
        #endregion

        #region 最后登录信息
        /// <summary>
        /// 最后登录信息类型
        /// </summary>
        public string LastLoginRemark
        {
            get
            {
                return this._lastloginremark;
            }
            set
            {
                this._lastloginremark = value;
            }
        }
        #endregion

        public decimal? balance { get; set; }

        /// <summary>
        /// 是否为超级管理员
        /// </summary>
        public int isSuperAdmin
        {
            set { _isSuperAdmin = value; }
            get { return _isSuperAdmin; }
        }

        /// <summary>
        /// 是否为代理 只有勾了代理才可以进代理后台
        /// </summary>
        public int isAgent
        {
            set { _isAgent = value; }
            get { return _isAgent; }
        }
    }
}
