using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.order
{
    [Serializable]
    public class reconciliation_temp
    {
        public reconciliation_temp()
        { }
        #region Model
        private int _id;
        private string _serverid = string.Empty;
        private string _orderid = string.Empty;
        private int? _count = 0;
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
        public string serverid
        {
            set { _serverid = value; }
            get { return _serverid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string orderid
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? count
        {
            set { _count = value; }
            get { return _count; }
        }
        #endregion Model

    }
}
