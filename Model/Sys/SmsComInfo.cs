using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.sys
{
    /// <summary>
    /// 
    /// </summary>
    public class SmsComInfo
    {
        private string _destnumber = string.Empty;
        /// <summary>
        /// 长号码
        /// </summary>
        public string destnumber
        {  
            set
            {
                _destnumber = value;
            }
            get
            {
                return _destnumber;
            }
        }

        private string _cmd = string.Empty;
        /// <summary>
        /// 指令
        /// </summary>
        public string cmd
        {
            set
            {
                _cmd = value;
            }
            get
            {
                return _cmd;
            }
        }

        private int _fee = 0;
        /// <summary>
        /// 金额
        /// </summary>
        public int fee
        {
            set
            {
                _fee = value;
            }
            get
            {
                return _fee;
            }
        }
    }
}
