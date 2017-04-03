using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using viviapi.Model;
using viviapi.Model.Channel;

namespace viviapi.web.Manage
{
    public partial class ChannelTypeList : viviapi.WebComponents.Web.ManagePageBase
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            BLL.ManageFactory.CheckSecondPwd();
            
            if (!this.IsPostBack)
            {
                LoadData();
            }
        }

        void LoadData()
        {
            this.GVChannel.DataSource = BLL.Channel.ChannelType.GetList(null);
            this.GVChannel.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = BLL.ManageFactory.CheckCurrentPermission(false
             , ManageRole.Interfaces);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupplierEdit.aspx", true);
        }
        protected void GVChannel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                Literal ltType = e.Row.FindControl("ltType") as Literal;
                Literal ltrunmode = e.Row.FindControl("ltrunmode") as Literal;
                //Literal litrate = e.Row.FindControl("litrate") as Literal;

                ltType.Text = Enum.GetName(typeof(Model.Channel.ChannelClassEnum), drv["classid"]);

                if (drv["runmode"].ToString() == "1")
                {
                    ltrunmode.Text = "<span style='color:red'>轮询</span>";
                }
                else
                {
                    ltrunmode.Text = "单独";
                }

                int isOpen = int.Parse(drv["isOpen"].ToString());
               
                Literal rblOpen = e.Row.FindControl("litOpen") as Literal;
                if (isOpen == 1)
                {
                    rblOpen.Text = "<span style='color:red'>全部关闭</span>";
                }
                else if (isOpen == 2)
                {
                    rblOpen.Text = "<span style='color:green'>全部开启</span>";
                }
                else if (isOpen == 4)
                {
                    rblOpen.Text = "<span style='color:red'>按配置(默认关闭)</span>";
                }
                else if (isOpen == 8)
                {
                    rblOpen.Text = "<span style='color:green'>按配置(默认开启)</span>";
                }

                RadioButtonList rblRelease = e.Row.FindControl("rblrelease") as RadioButtonList;
                rblRelease.SelectedValue = drv["release"].ToString();
                rblRelease.Enabled = false;

                //string str = drv["p" + drv["typeId"].ToString()].ToString();
                //if (!string.IsNullOrEmpty(str))
                //{
                //    decimal rate = decimal.Zero;
                //    decimal.TryParse(str, out rate);
                //    if (rate != decimal.Zero)
                //    {
                //        litrate.Text = (rate).ToString("p2");
                //    }
                //}
            }
        }
    }
}