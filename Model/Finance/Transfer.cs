using System;

namespace viviapi.Model.Finance
{
    [Serializable]
    public class Transfer
    {
        private int _id;
        private int? _year;
        private int? _month;
        private int _userid;
        private int _touserid;
        private decimal _amt;
        private decimal _charge;
        private string _remark;
        private int _status;
        private DateTime _addtime;
        private DateTime? _updatetime;
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
        public int? year
        {
            set { _year = value; }
            get { return _year; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? month
        {
            set { _month = value; }
            get { return _month; }
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
        public int touserid
        {
            set { _touserid = value; }
            get { return _touserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal amt
        {
            set { _amt = value; }
            get { return _amt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal charge
        {
            set { _charge = value; }
            get { return _charge; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? updatetime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
    }
}
