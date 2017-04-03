using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Settled
{
    public enum AmtFreezeInfoStatus
    {         
        否 = 1,
        是 = 2
    }

    public enum AmtunFreezeMode
    {
        未处理 = 0,
        解冻到余额 = 1,
        解冻并扣除 = 2
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class UsersAmtFreezeInfo
    {
        private int _id;
        private int _userid;
        private decimal _freezeamt;
        private DateTime? _addtime;
        private int? _manageid;
        private AmtFreezeInfoStatus _status = AmtFreezeInfoStatus.否;
        private DateTime? _checktime;
        private string _why;
        private AmtunFreezeMode _unfreezemode = AmtunFreezeMode.未处理;

        /// <summary>
        /// 
        /// </summary>
        public string why
        {
            set { _why = value; }
            get { return _why; }
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
        public decimal freezeAmt
        {
            set { _freezeamt = value; }
            get { return _freezeamt; }
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
        public int? manageId
        {
            set { _manageid = value; }
            get { return _manageid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public AmtunFreezeMode unfreezemode
        {
            set { _unfreezemode = value; }
            get { return _unfreezemode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public AmtFreezeInfoStatus status
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
    }
}
