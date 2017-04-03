using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Communication;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.usermodule.account
{
    public partial class Feedbackview : UserPageBase
    {
        feedback bll = new feedback();
        public int itemid
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("id", 0);
            }
        }

        private viviapi.Model.feedbackInfo _item = null;
        public viviapi.Model.feedbackInfo ItemInfo
        {
            get
            {
                if (_item == null && itemid > 0)
                {
                    _item = bll.GetModel(itemid, this.UserId);
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
                this.lit_typeid.Text = Enum.GetName(typeof(viviapi.Model.feedbacktype), ItemInfo.typeid);
                this.lit_title.Text = ItemInfo.title;
                lit_cont.Text = ItemInfo.cont;
                lit_clientip.Text = ItemInfo.clientip;
                lit_reply.Text = ItemInfo.reply;
            }
        }
    }
}
