using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Settled
{
    [Serializable]
    public class transferscheme
    {
        public transferscheme()
        { }
        #region Model
        private int _id = 0;
        private string _schemename;
        private decimal _minamtlimitofeach;
        private decimal _maxamtlimitofeach;
        private int _dailymaxtimes;
        private decimal _dailymaxamt;
        private int _monthmaxtimes;
        private decimal _monthmaxamt;
        private decimal _chargerate;
        private decimal _chargeleastofeach;
        private decimal _chargemostofeach;
        private int _isdefault;
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
        public string schemename
        {
            set { _schemename = value; }
            get { return _schemename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal minamtlimitofeach
        {
            set { _minamtlimitofeach = value; }
            get { return _minamtlimitofeach; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal maxamtlimitofeach
        {
            set { _maxamtlimitofeach = value; }
            get { return _maxamtlimitofeach; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int dailymaxtimes
        {
            set { _dailymaxtimes = value; }
            get { return _dailymaxtimes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal dailymaxamt
        {
            set { _dailymaxamt = value; }
            get { return _dailymaxamt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int monthmaxtimes
        {
            set { _monthmaxtimes = value; }
            get { return _monthmaxtimes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal monthmaxamt
        {
            set { _monthmaxamt = value; }
            get { return _monthmaxamt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal chargerate
        {
            set { _chargerate = value; }
            get { return _chargerate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal chargeleastofeach
        {
            set { _chargeleastofeach = value; }
            get { return _chargeleastofeach; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal chargemostofeach
        {
            set { _chargemostofeach = value; }
            get { return _chargemostofeach; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int isdefault
        {
            set { _isdefault = value; }
            get { return _isdefault; }
        }
        #endregion Model
    }
}
