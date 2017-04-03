using System;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.agent
{
    public partial class Index : AgentPageBase
    {
        protected string loginip;
        protected string logintime;        
        protected string username;
        protected string userscount;
        protected string todaytotalAmt = "0.00";
        protected string monthtotalAmt = "0.00";
        protected string yeartotalAmt = "0.00";
        protected string monthcommission = "0.00";
        protected string balance = "0.00";

        public int GetUserNum
        {
            get
            {
                return viviapi.BLL.Promotion.Factory.GetUserNum(this.UserId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    litlinks.Text = "?agent=" + CurrentUser.ID.ToString();

                    decimal totalAmt,commissionAmt;
                    userscount = GetUserNum.ToString();

                    DateTime sdt = DateTime.Today;
                    DateTime edt = DateTime.Today.AddDays(1);

                    //商户今日充值量
                    decimal tempNum = viviapi.BLL.Order.Statistics.GetAgentTotalAmt(this.UserId, sdt, edt);
                    todaytotalAmt = tempNum.ToString("f2");

                    //商户当月充值量
                    sdt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00"));
                    tempNum = viviapi.BLL.Order.Statistics.GetAgentTotalAmt(this.UserId, sdt, edt);
                    monthtotalAmt = tempNum.ToString("f2");

                    //商户当年充值量
                    sdt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01 00:00:00"));
                    tempNum = viviapi.BLL.Order.Statistics.GetAgentTotalAmt(this.UserId, sdt, edt);
                    yeartotalAmt = tempNum.ToString("f2");

                    //代理当月提成
                    commissionAmt = viviapi.BLL.Order.Statistics.GetAgentIncome(this.UserId, sdt, edt);
                    monthcommission = commissionAmt.ToString("f2");
                     
                    balance = this.CurrentUser.enableAmt.ToString();



                   /* userscount = viviapi.BLL.ManageFactory.GetManageUsers(currentManage.id).ToString();
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
                    }*/

                    loginip = this.CurrentUser.LastLoginIp;
                    logintime = viviLib.TimeControl.FormatConvertor.DateTimeToTimeString(CurrentUser.LastLoginTime);
                    username = this.CurrentUser.UserName;

                }
                catch { }
            }
        }


       
    }
}