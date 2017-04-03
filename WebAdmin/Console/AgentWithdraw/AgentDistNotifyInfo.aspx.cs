using System;
using System.Globalization;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.AgentWithdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AgentDistNotifyInfo : ManagePageBase
    {
        readonly viviapi.BLL.Finance.Agent.WithdrawAgentNotify bll 
            = new viviapi.BLL.Finance.Agent.WithdrawAgentNotify();

        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    int id = (Convert.ToInt32(strid));
                    ShowInfo(id);
                }
            }
        }

        private void ShowInfo(int id)
        {
            var model = bll.GetModel(id);

            this.lblid.Text = model.id.ToString(CultureInfo.InvariantCulture);
            this.lblnotify_id.Text = model.notify_id;
            this.lbluserid.Text = model.userid.ToString();
            this.lbltrade_no.Text = model.trade_no;
            this.lblout_trade_no.Text = model.out_trade_no;
            this.lblnotifystatus.Text = bll.GetNotifyStatusText(model.notifystatus);
            this.lblnotifyurl.Text = model.notifyurl;
            this.lblresText.Text = Server.HtmlDecode(model.resText);
            this.lbladdTime.Text = model.addTime.ToString("yyyy-MM-dd HH:mm:ss");
            if (model.resTime != null) this.lblresTime.Text = model.resTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblext1.Text = model.ext1;
            this.lblext2.Text = model.ext2;
            this.lblext3.Text = model.ext3;
            this.lblremark.Text = model.remark;

        }

        
    }
}