using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Quota
{
    public class quotaOrder
    {
        private int _id = 0;
        private int _userid = 0;
        private string _orderid = string.Empty;
        private decimal _quotaValue = 0M;
        private decimal _charge = 0M;
        private decimal _payrate = 0M;
        private int _quota_type = 0;
        private string _remark = string.Empty;
        private int _status = 0;
        private int _year = 0;
        private int _month = 0;
        private string _clientip = string.Empty;
        private DateTime _addtime = DateTime.Now;
        private DateTime _updatetime = DateTime.Now;
        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        public int userid
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }
        public string orderid
        {
            get
            {
                return this._orderid;
            }
            set
            {
                this._orderid = value;
            }
        }
        public decimal quotaValue
        {
            get
            {
                return this._quotaValue;
            }
            set
            {
                this._quotaValue = value;
            }
        }
        public decimal charge
        {
            get
            {
                return this._charge;
            }
            set
            {
                this._charge = value;
            }
        }
        public decimal payrate
        {
            get
            {
                return this._payrate;
            }
            set
            {
                this._payrate = value;
            }
        }
        public int quota_type
        {
            get
            {
                return this._quota_type;
            }
            set
            {
                this._quota_type = value;
            }
        }
        public string clientip
        {
            get
            {
                return this._clientip;
            }
            set
            {
                this._clientip = value;
            }
        }
        public string remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }
        public int status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }
        public DateTime updatetime
        {
            get
            {
                return this._updatetime;
            }
            set
            {
                this._updatetime = value;
            }
        }
        public DateTime addtime
        {
            get
            {
                return this._addtime;
            }
            set
            {
                this._addtime = value;
            }
        }
        public int year
        {
            get
            {
                return this._year;
            }
            set
            {
                this._year = value;
            }
        }

        public int month
        {
            get
            {
                return this._month;
            }
            set
            {
                this._month = value;
            }
        }
    }
}
