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
    public partial class SendMsg : ManagePageBase
    {
        public string GMName
        {
            get
            {
                return Request.QueryString["UserName"];
            }
        }

        public string GMID
        {
            get
            {
                return Request.QueryString["uid"];
            }
        }

        private viviapi.Model.News.InternalMessage _item = null;
        public viviapi.Model.News.InternalMessage Item
        {
            get
            {
                //if (_item == null)
                //{
                //    _item = IMSGFactory.GetModelByTo(int.Parse(GMID));
                //}
                return _item;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtMsgTo.Text = GMID;               
            }
        }

      //  protected string msg_content = "";

        protected void bt_sub_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string item in GMID.Split(','))
                {
                    int gmid = 0;
                    if (int.TryParse(item, out gmid))
                    {
                        _item = new viviapi.Model.News.InternalMessage();

                        Item.senderUserType = 0;
                        Item.sendId = ManageId;
                        Item.sender = "管理员";

                        Item.receiverType = 1;
                        Item.receiverId = int.Parse(item);
                        Item.receiver = viviapi.BLL.User.Factory.GetModel(Item.receiverId).UserName;

                        
                        Item.msgtitle = this.tb_title.Text;
                        Item.msgContent = Server.HtmlDecode(hfcontent.Value);

                        Item.addtime = DateTime.Now;
                        Item.isRead = false;

                        viviapi.BLL.Communication.InternalMessage.Add(_item);
                    }
                }
                AlertAndRedirect("发送成功", "../User/UserList.aspx");
            }
            catch(Exception ex)
            {
                this.lblMsg.Text = "发送失败";
            }
        }
    }

}