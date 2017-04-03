using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Order.Bank;
using viviLib.Web;

namespace viviAPI.Gateway2018
{
    public partial class PayResult : System.Web.UI.Page
    {
        public string OrderId
        {
            get
            {
                return WebBase.GetQueryStringString("o", "");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(OrderId))
            {
                var model = Factory.Instance.GetModelByOrderId(OrderId);

                if (model != null)
                {
                    litSysOrderId.Text = OrderId;
                    litUserOrderId.Text = model.userorder;
                    litUserId.Text = model.userid.ToString();

                    litTypeId.Text = viviapi.BLL.Channel.ChannelType.GetCacheModel(model.typeId).modetypename;

                    litMoney.Text = model.realvalue.Value.ToString("f2");
                    litStatus.Text = Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum), model.status);
                }
            }
        }
    }
}
