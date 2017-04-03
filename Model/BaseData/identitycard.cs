using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.basedata
{
    [Serializable]
    public class identitycard
    {
        #region Model
        private int _id;
        private string _bm;
        private string _dq;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BM
        {
            set { _bm = value; }
            get { return _bm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DQ
        {
            set { _dq = value; }
            get { return _dq; }
        }
        #endregion Model
    }
}
