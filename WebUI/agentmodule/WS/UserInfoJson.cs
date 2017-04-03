using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace viviAPI.WebUI7uka.agentmodule.WS
{

    [Serializable]
    public class UserInfoJson
    {
        public int result { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string msg { get; set; }
    }
}
