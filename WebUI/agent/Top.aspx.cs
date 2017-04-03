using System;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.agent
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Top : AgentPageBase
    {
        protected string username;

        protected void Page_Load(object sender, EventArgs e)
        {          
            if (!this.IsPostBack)
            {
                this.username = this.CurrentUser.UserName;
            }
        }        
    }
}

