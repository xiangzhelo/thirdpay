using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.SysInterface.Withdraw
{
    [Serializable]
    public class AgentDistributionInfo
    {

        //商户ID
        public string parter { get; set; }
        public string orderid { get; set; }
        public string callbackurl { get; set; }

        public string bank_name { get; set; }
        public string bank_site_name { get; set; }
        public string bank_account_name { get; set; }
        public string bank_account_no { get; set; }
        public string amount_str { get; set; }

        public string sign { get; set; }
        public string remark { get; set; }
        public string agent_time { get; set; }
        
    }
}
