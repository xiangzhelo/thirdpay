using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.User
{
    /// <summary>
    /// 
    /// </summary>
    public enum EmailCheckStatus
    { 
        未知 = 0,
        提交中 = 1,
        已审核 = 2,
        失效 = 3
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EmailCheckType
    {        
        注册 = 1,
        认证 = 2,
        修改 = 3
    }

    public class EmailCheckInfo
    {
        private int _id;
        private int _userid;
        private string _email;
        private DateTime? _addtime;
        private EmailCheckStatus _status;
        private DateTime? _checktime;
        private EmailCheckType _typeid;
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
        public string email
        {
            set { _email = value; }
            get { return _email; }
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
        /// 审核状态
        /// </summary>
        public EmailCheckStatus status
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? checktime
        {
            set { _checktime = value; }
            get { return _checktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public EmailCheckType typeid
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        //过期时间
        public DateTime Expired { get; set; }
    }
}
