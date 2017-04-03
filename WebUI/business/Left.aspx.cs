using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using viviapi.Model;

namespace viviapi.web.Business
{
    public partial class Left : viviapi.WebComponents.Web.BusinessPageBase
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



