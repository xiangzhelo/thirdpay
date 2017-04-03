using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.User
{
    [Serializable]
    public class UsersAmtInfo
    {
        private int _id;
        private int _userid;
        private int _integral =0;
        private decimal _balance = 0M;
        private decimal _payment = 0M;
        private decimal _unpayment = 0M;
        private decimal _freeze = 0M;

        private decimal _enableAmt = 0M;
        //private byte _special = 0;
        //private int _payrate = 0;

        /// <summary>
        /// 可用余额
        /// </summary>
        public decimal enableAmt
        {
            set { _enableAmt = value; }
            get { return _enableAmt; }
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
        /// 用户ID
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        /// 积分
        /// </summary>
        public int Integral
        {
            set { _integral = value; }
            get { return _integral; }
        }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance
        {
            set { _balance = value; }
            get { return _balance; }
        }

        /// <summary>
        /// 已支付总额
        /// </summary>
        public decimal Payment
        {
            set { _payment = value; }
            get { return _payment; }
        }

        /// <summary>
        /// 提现中
        /// </summary>
        public decimal Unpayment
        {
            set { _unpayment = value; }
            get { return _unpayment; }
        }

        /// <summary>
        /// 被冻
        /// </summary>
        public decimal Freeze
        {
            set { _freeze = value; }
            get { return _freeze; }
        }


        ///// <summary>
        ///// 单独配置比率
        ///// </summary>
        //public byte special
        //{
        //    set { _special = value; }
        //    get { return _special; }
        //}

        ///// <summary>
        ///// 结算比率
        ///// </summary>
        //public int payrate
        //{
        //    set { _payrate = value; }
        //    get { return _payrate; }
        //}
    }
}
