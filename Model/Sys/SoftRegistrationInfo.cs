using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Sys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SoftRegistrationInfo
    {
        private string _name = string.Empty;
        private string _hosts = string.Empty;
        private bool _islimittime = true;
        private bool _islimithost = true;
        
        private DateTime _stime = DateTime.Now;
        private DateTime _etime = DateTime.Now;

        public bool islimittime
        {
            get
            {
                return _islimittime;
            }
            set
            {
                _islimittime = value;
            }
        }

        public bool islimithost
        {
            get
            {
                return _islimithost;
            }
            set
            {
                _islimithost = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string hosts
        {
            get
            {
                return _hosts;
            }
            set
            {
                _hosts = value;
            }
        }

        public DateTime stime
        {
            get
            {
                return _stime;
            }
            set
            {
                _stime = value;
            }
        }

        public DateTime etime
        {
            get
            {
                return _etime;
            }
            set
            {
                _etime = value;
            }
        }


    }
}
