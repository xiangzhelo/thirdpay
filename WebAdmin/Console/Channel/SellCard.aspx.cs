using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL.Channel;
using viviapi.Model;
using viviapi.Model.Channel;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.channel
{
    /// <summary>
    /// 
    /// </summary>
    public partial class mutisupp : viviapi.WebComponents.Web.ManagePageBase
    {
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }


        public ChannelTypeInfo _ItemInfo = null;
        public ChannelTypeInfo ItemInfo
        {
            get
            {
                if (_ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        _ItemInfo = ChannelType.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        _ItemInfo = new ChannelTypeInfo();
                    }
                }
                return _ItemInfo;
            }
        }


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
                txttypename.Text = ItemInfo.modetypename;
                hftypeid.Value = ItemInfo.typeId.ToString();

                this.LoadData();
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Financial);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            DataSet ds = viviapi.BLL.Channel.Channelsupplier.GetList(ItemInfo.typeId, 0);
            rptsupp.DataSource = ds;
            rptsupp.DataBind();
        }
        #endregion

        protected void rptsupp_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rptsupp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlInputCheckBox chkItem = e.Item.FindControl("chkItem") as HtmlInputCheckBox;
                bool isopen = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "isopen"));

                chkItem.Checked = isopen;


                HtmlInputCheckBox chkisdefault = e.Item.FindControl("chkisdefault") as HtmlInputCheckBox;
                bool isdefault = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "isdefault"));
                chkisdefault.Checked = isdefault;
            }

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptsupp.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlInputCheckBox chkItem = item.FindControl("chkItem") as HtmlInputCheckBox;
                    HtmlInputCheckBox chkisdefault = item.FindControl("chkisdefault") as HtmlInputCheckBox;
                    HiddenField hfsuppid = item.FindControl("hfsuppid") as HiddenField;
                    TextBox txtpayrate  = item.FindControl("txtpayrate") as TextBox;


                    if (chkItem != null && hfsuppid != null && txtpayrate != null)
                    { 
                        decimal payrate = 0M;
                        decimal.TryParse(txtpayrate.Text.Trim(),out payrate);

                        viviapi.Model.Channel.ChannelSupplier info = new ChannelSupplier();
                        info.userid = 0;
                        info.typeid = ItemInfo.typeId;
                        info.suppid = int.Parse(hfsuppid.Value);
                        info.payrate = payrate/100M;
                        info.isopen = chkItem.Checked;
                        info.isdefault = chkisdefault.Checked;
                        viviapi.BLL.Channel.Channelsupplier.Insert(info);
                    }

                }
            }

            AlertAndRedirect("设置成功","ChannelTypeList.aspx");
        }


    }
}