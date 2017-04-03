using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.SysInterface.Card.YeePay.Lib
{
    public class ChargeCardDirentResult
    {
        private string hmac;
        private string r0_Cmd;
        private string r1_Code;
        private string r6_Order;
        private string reqResult;
        private string reqUrl;
        private string rq_ReturnMsg;

        public ChargeCardDirentResult()
        {

        }

        public ChargeCardDirentResult(string r0_Cmd, string r1_Code, string r6_Order, string rq_ReturnMsg, string hmac, string reqUrl, string reqResult)
        {
            this.r0_Cmd = r0_Cmd;
            this.r1_Code = r1_Code;
            this.r6_Order = r6_Order;
            this.rq_ReturnMsg = rq_ReturnMsg;
            this.hmac = hmac;
            this.reqUrl = reqUrl;
            this.reqResult = reqResult;
        }

        public string Hmac
        {
            get
            {
                return this.hmac;
            }
            set
            {
                this.hmac = value;

            }


        }

        public string R0_Cmd
        {
            get
            {
                return this.r0_Cmd;
            }
            set
            {
                this.r0_Cmd = value;

            }
        }

        public string R1_Code
        {
            get
            {
                return this.r1_Code;
            }
            set
            {
                this.r1_Code = value;

            }
        }

        public string R6_Order
        {
            get
            {
                return this.r6_Order;
            }
            set
            {
                this.r6_Order = value;

            }
        }

        public string ReqResult
        {
            get
            {
                return this.reqResult;
            }
            set
            {
                this.reqResult = value;

            }
        }

        public string ReqUrl
        {
            get
            {
                return this.reqUrl;
            }
            set
            {
                this.reqUrl = value;

            }
        }

        public string Rq_ReturnMsg
        {
            get
            {
                return this.rq_ReturnMsg;
            }
            set
            {
                this.rq_ReturnMsg = value;

            }
        }
    }
}
