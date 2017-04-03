using System;
using viviapi.BLL.Communication;
using viviapi.Model;
using viviapi.Model.News;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.News
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MsgView : ManagePageBase
    {
        public string strid = ""; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    int ID = (Convert.ToInt32(strid));
                    ShowInfo(ID);
                }
            }
        }

        private void ShowInfo(int ID)
        {
            var model = viviapi.BLL.Communication.InternalMessage.GetModel(ID);
            this.lblID.Text = model.ID.ToString();
            this.lblsenderUserType.Text = viviapi.BLL.Communication.InternalMessage.GetUserTypeName(model.senderUserType);
            this.lblsendId.Text = model.sendId.ToString();
            this.lblsender.Text = model.sender;
            this.lblreceiverType.Text = viviapi.BLL.Communication.InternalMessage.GetUserTypeName(model.receiverType);
            this.lblreceiverId.Text = model.receiverId.ToString();
            this.lblreceiver.Text = model.receiver;
            this.lblmsgtitle.Text = model.msgtitle;
            this.lblmsgContent.Text = Server.HtmlDecode(model.msgContent);
            this.lbladdtime.Text = model.addtime.ToString();
            this.lblisRead.Text = model.isRead ? "是" : "否";
            this.lblreadTime.Text = model.readTime.ToString();

        }
    }

}