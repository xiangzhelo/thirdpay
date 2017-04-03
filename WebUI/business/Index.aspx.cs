using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using viviapi.Model;
using viviLib.TimeControl;

namespace viviapi.web.Business
{
    public partial class Index : viviapi.WebComponents.Web.BusinessPageBase
    {
        protected string loginip;
        protected string logintime;        
        protected string username;
        protected string userscount;
        protected string todaytotalAmt = "0.00";
        protected string monthtotalAmt = "0.00";
        protected string monthcommission = "0.00";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                try
                {
                    litlinks.Text = "?s=" + currentManage.id.ToString();

                    decimal totalAmt,commissionAmt;
                    userscount = viviapi.BLL.ManageFactory.GetManageUsers(currentManage.id).ToString();
                    if (viviapi.BLL.ManageFactory.GetManagePerformance(currentManage.id
                        , Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"))
                        , DateTime.Now.AddDays(1)
                        , out totalAmt
                        , out commissionAmt))
                    {
                        todaytotalAmt = totalAmt.ToString("f2");
                    }

                    if (viviapi.BLL.ManageFactory.GetManagePerformance(currentManage.id
                        , Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00"))
                        , DateTime.Now.AddDays(1)
                        , out totalAmt
                        , out commissionAmt))
                    {
                        monthtotalAmt = totalAmt.ToString("f2");
                        monthcommission = commissionAmt.ToString("f2");
                    }

                    loginip = currentManage.lastLoginIp;
                    logintime = viviLib.TimeControl.FormatConvertor.DateTimeToTimeString(currentManage.lastLoginTime.Value);
                    username = currentManage.username;

                }
                catch { }
            }
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