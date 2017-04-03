using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.Model.Channel;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.channel
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WithdrawChannels : ManagePageBase
    {
        viviapi.BLL.Withdraw.ChannelWithdraw chnlsBLL = new viviapi.BLL.Withdraw.ChannelWithdraw();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.BLL.ManageFactory.CheckSecondPwd();
            setPower();
            if (!this.IsPostBack)
            {
                LoadData();
            }
        }

        void LoadData()
        {
            DataSet ds = chnlsBLL.GetAllList();
            this.rptChnls.DataSource = ds;
            this.rptChnls.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Interfaces);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }


        protected void rptChnls_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlsupp = e.Item.FindControl("ddlsupp") as DropDownList;

                int _id = Convert.ToInt32(e.CommandArgument);
                ChannelWithdraw _model = chnlsBLL.GetModel(_id);

                _model.supplier = int.Parse(ddlsupp.SelectedValue);
                chnlsBLL.Update(_model);

                LoadData();
            }
        }

        protected void rptChnls_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlsupp = e.Item.FindControl("ddlsupp") as DropDownList;
                string dfval = string.Empty;
 
                object supp = DataBinder.Eval(e.Item.DataItem, "supplier");
                if (supp != DBNull.Value)
                    dfval = supp.ToString();

                SupBind(ddlsupp, dfval);

            }
        }

        void SupBind(DropDownList ddl,string dfval)
        {
            DataTable list = Factory.GetList("isdistribution=1").Tables[0];
            ddl.Items.Add(new ListItem("--请选择--", "0"));
            foreach (DataRow dr in list.Rows)
            {
                ListItem _item = new ListItem(dr["name"].ToString(), dr["code"].ToString());
                if (dr["code"].ToString() == dfval)
                    _item.Selected = true;
                ddl.Items.Add(_item);
            }
        }
    }
}