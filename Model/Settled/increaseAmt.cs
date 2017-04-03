using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Settled
{
    public enum optypeenum
    { 
        加款 = 1,
        扣款 = 2
        
    }
    /// <summary>
    /// 加款类
    /// </summary>
    public class IncreaseAmtInfo
    {
        private int _id;
        private int? _userid;
        private decimal? _increaseamt;
        private DateTime? _addtime;
        private int? _mangeid;
        private string _mangename;
        private int? _status;
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
        public int? userId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? increaseAmt
        {
            set { _increaseamt = value; }
            get { return _increaseamt; }
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
        public int? mangeId
        {
            set { _mangeid = value; }
            get { return _mangeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mangeName
        {
            set { _mangename = value; }
            get { return _mangename; }
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
        public string desc
        {
            set { _desc = value; }
            get { return _desc; }
        }

        private optypeenum _optype;
        /// <summary>
        /// 加款还是扣款
        /// </summary>
        public optypeenum optype
        {
            set { _optype = value; }
            get { return _optype; }
        }
    }
}
