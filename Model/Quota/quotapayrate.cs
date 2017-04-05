using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Quota
{
    public class quotapayrate
    {
        private int _id=0;
        private int _userid = 0;
        private int _quota_type = 0;
        private decimal _payrate = 0;
        private int _selfisopen = 0;
        private int _sysisopen = 0;
        public int ID
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
        public int Userid
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
        public int Quota_type
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
        public int Selfisopen
        {
            get
            {
                return this._selfisopen;
            }
            set
            {
                this._selfisopen = value;
            }
        }
        public int Sysisopen
        {
            get
            {
                return this._sysisopen;
            }
            set
            {
                this._sysisopen = value;
            }
        }
        public decimal Payrate
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
    }
}
