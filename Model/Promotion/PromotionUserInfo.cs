using System;

namespace viviapi.Model.Promotion
{ 
    /// <summary>
    /// 
    /// </summary>
    public class Promoter
    {
        private int _promId = 0;
        private int _regid;
        private int _pid;
        private int _promstatus;       
        private decimal _prices;        
        private DateTime _promtime;

        /// <summary>
        /// 代理用户ID
        /// </summary>
        public int PromId
        {
            get
            {
                return this._promId;
            }
            set
            {
                this._promId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PromStatus
        {
            get
            {
                return this._promstatus;
            }
            set
            {
                this._promstatus = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime PromTime
        {
            get
            {
                return this._promtime;
            }
            set
            {
                this._promtime = value;
            }
        }

        /// <summary>
        /// 上级商户ID
        /// </summary>
        public int PID
        {
            get
            {
                return this._pid;
            }
            set
            {
                this._pid = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal Prices
        {
            get
            {
                return this._prices;
            }
            set
            {
                this._prices = value;
            }
        }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int RegId
        {
            get
            {
                return this._regid;
            }
            set
            {
                this._regid = value;
            }
        }
    }
}

