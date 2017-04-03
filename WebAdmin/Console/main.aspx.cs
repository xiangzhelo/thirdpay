using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.WebAdmin.Console
{
    public partial class main : viviapi.WebComponents.Web.ManagePageBase
    {
        protected string loginip;
        protected string logintime;
        protected string username;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //setPower();
            //if (!this.IsPostBack)
            //{
            //    try
            //    {
            //        loginip = currentManage.lastLoginIp;
            //        logintime = viviLib.TimeControl.FormatConvertor.DateTimeToTimeString(currentManage.lastLoginTime.Value);
            //        username = currentManage.username;
            //        //paysouid.InnerText = "欢迎使用第三方支付平台";

            //    }
            //    catch { }
            //}
        }
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