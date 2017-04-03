using System;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.agent
{
    public partial class Left : AgentPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            //ManageRole role = ManageRole.Administrator | ManageRole.Financial | ManageRole.UnionAdmin | ManageRole.SuperAdmin | ManageRole.CustomerService;

            //if ((currentManage.role & role) == currentManage.role)
            //{
            //    return;
            //}
            //else
            //{
            //    Response.End();
            //}
        }
    }
}



