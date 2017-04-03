using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using viviapi.Model.News;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.agentmodule.account
{
    public partial class Msgview : AgentPageBase
    {
        public int msgId
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("id", 0);
            }
        }

        private InternalMessage _item = null;
        public InternalMessage ItemInfo
        {
            get
            {
                if (_item == null && msgId > 0)
                {
                    _item = viviapi.BLL.Communication.InternalMessage.GetModel(msgId);
                }
                return _item;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        void InitForm()
        {
            if (ItemInfo != null)
            {
                this.lit_title.Text = ItemInfo.msgtitle;
                this.lit_addtime.Text = ItemInfo.addtime.ToString("yyyy-MM-dd");
                this.lit_content.Text = Server.HtmlDecode(ItemInfo.msgContent);

                if (!ItemInfo.isRead)
                {
                    ItemInfo.isRead = true;
                    viviapi.BLL.Communication.InternalMessage.Update(ItemInfo);
                }
            }
        }
    }
}