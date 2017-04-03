using System;
using viviapi.BLL.Communication;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.agentmodule.settlement
{
    public partial class index :AgentPageBase
    {
        public string getnid = "";
        public string getnm = "";
        public int getmsgcount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        void InitForm()
        {
            getnid = CurrentUser.ID.ToString();
            getnm = CurrentUser.UserName;

            try
            {
                getmsgcount = viviapi.BLL.Communication.InternalMessage.GetUserMsgCount(CurrentUser.ID);

            }
            catch { }
        }
    }
}
