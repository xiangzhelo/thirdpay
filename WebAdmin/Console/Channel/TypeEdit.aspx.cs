using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL.Channel;
using viviapi.Model;
using viviapi.Model.Channel;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.channel
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ChannelTypeEdit : ManagePageBase
    {
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public bool IsUpdate
        {
            get
            {
                return ItemInfoId > 0;
            }
        }

        private ChannelTypeInfo _itemInfo = null;
        public ChannelTypeInfo ItemInfo
        {
            get
            {
                if (_itemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        _itemInfo = ChannelType.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        _itemInfo = new ChannelTypeInfo();
                    }
                }
                return _itemInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                txtmodetypename.Enabled = false;
                txttypeId.Enabled = false;
                rblType.Enabled = false;
                txtCode.Enabled = false;

                DataTable list = viviapi.BLL.Supplier.Factory.GetList("").Tables[0];
                ddlSupplier2.Items.Add(new ListItem("--请选择--", ""));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupplier2.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));

                }
                
                ShowInfo();

                LoadData();
            }
        }

        void ShowInfo()
        {
            if (IsUpdate && ItemInfo != null)
            {
                this.rblType.SelectedValue = ((int)ItemInfo.Class).ToString();
                this.txtmodetypename.Text = ItemInfo.modetypename;
                this.txttypeId.Text = ItemInfo.typeId.ToString();

                this.txtCode.Text = ItemInfo.code;
                this.ddlOpen.SelectedValue = ((int)ItemInfo.isOpen).ToString();
                this.txtsort.Text = ItemInfo.sort.ToString();
                this.rblRelease.SelectedValue = ItemInfo.release ? "1" : "0";
                this.rblrunmode.SelectedValue = ItemInfo.runmode.ToString();

                tr_runmode_1.Visible = ItemInfo.runmode == 1;
                tr_runmode_0.Visible = ItemInfo.runmode == 0;

                string where = string.Empty;
                if (ItemInfo.Class == ChannelClassEnum.在线支付)
                {
                    where += "isbank=1";
                }
                else if (ItemInfo.Class == ChannelClassEnum.充值卡)
                {
                    where += "iscard=1";
                }
                else if (ItemInfo.Class == ChannelClassEnum.声讯)
                {
                    where += "issx=1";
                }
                else if (ItemInfo.Class == ChannelClassEnum.短信)
                {
                    where += "issms=1";
                }

                DataTable list = viviapi.BLL.Supplier.Factory.GetList(where).Tables[0];
                ddlSupplier.Items.Add(new ListItem("--请选择--", ""));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupplier.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));

                }
                this.ddlSupplier.SelectedValue = ItemInfo.supplier.ToString();
                this.ddlSupplier2.SelectedValue = ItemInfo.supplier2.ToString();
                txtSuppsWhenExceOccurred.Text = ItemInfo.SuppsWhenExceOccurred;
                txttimeout.Text = ItemInfo.timeout.ToString();

                //if (ItemInfo.typeId == 100 || ItemInfo.typeId == 101)
                //{
                //    this.ddlSupplier.Attributes["disabled"] = "disabled";
                //}
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {            
            string modetypename = this.txtmodetypename.Text;
            int typeId = int.Parse(this.txttypeId.Text);
            int isOpen = 0;
            int.TryParse(ddlOpen.SelectedValue, out isOpen);
            int supplier = 0;
            int.TryParse(this.ddlSupplier.SelectedValue, out supplier);

            int supplier2 = 0;

            if (!string.IsNullOrEmpty(this.ddlSupplier2.SelectedValue))
            {
                int.TryParse(this.ddlSupplier2.SelectedValue, out supplier2);
            }
           
            int sort = int.Parse(this.txtsort.Text);
            bool release = this.rblRelease.SelectedValue == "1" ? true :false ;
            int classId = Convert.ToInt32(this.rblType.SelectedValue);

            if (!IsUpdate)
            {
                ItemInfo.modetypename = modetypename;
                ItemInfo.typeId = typeId;
                ItemInfo.Class = (ChannelClassEnum)classId;
                ItemInfo.addtime = DateTime.Now;
            }

            ItemInfo.isOpen = (OpenEnum)isOpen;

            ItemInfo.supplier = supplier;
            ItemInfo.supplier2 = supplier2;
            ItemInfo.SuppsWhenExceOccurred = txtSuppsWhenExceOccurred.Text.Trim();
            ItemInfo.sort = sort;
            ItemInfo.release = release;
            ItemInfo.runmode = int.Parse(this.rblrunmode.SelectedValue);
            ItemInfo.timeout = int.Parse(this.txttimeout.Text);

          


            System.Text.StringBuilder _set = new System.Text.StringBuilder();


            int count = 1;
            foreach (RepeaterItem item in rptsupp.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlInputCheckBox chkItem = item.FindControl("chkItem") as HtmlInputCheckBox;                  
                    HiddenField hfsuppid = item.FindControl("hfsuppid") as HiddenField;
                    TextBox txtweight = item.FindControl("txtweight") as TextBox;

                    if (chkItem != null && hfsuppid != null && txtweight != null)
                    {
                        if (chkItem.Checked)
                        {
                            int weight = 1;
                            try {
                                weight = Convert.ToInt32(txtweight.Text);
                            }
                            catch {
                                weight = 1;
                            }
                            if (count > 1)
                                _set.AppendFormat("|{0}:{1}", hfsuppid.Value, weight);
                            else
                                _set.AppendFormat("{0}:{1}", hfsuppid.Value, weight);

                            count++;
                        }
                    }

                }
            }
            ItemInfo.runset = _set.ToString();

            if (!this.IsUpdate)
            {
                int id = ChannelType.Add(ItemInfo);
                if (id > 0)
                {
                    AlertAndRedirect("保存成功！", "TypeList.aspx");
                }
                else
                {
                    ShowMessageBox("保存失败！");
                }
            }
            else
            {
                if (ChannelType.Update(ItemInfo))
                {
                    viviapi.WebComponents.WebUtility.ClearCache("CHANNELTYPE_CACHEKEY");

                    AlertAndRedirect("更新成功！", "TypeList.aspx");
                }
                else
                {
                    ShowMessageBox("更新失败！");
                }
            }
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

        protected void rblrunmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblrunmode.SelectedValue == "1")
            {
                tr_runmode_1.Visible = true;
                tr_runmode_0.Visible = false;
            }
            else
            {
                tr_runmode_1.Visible = false;
                tr_runmode_0.Visible = true;
            }
        }

        protected void rptsupp_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rptsupp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (ItemInfo != null && ItemInfo.runmode == 1)
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlInputCheckBox chkItem = e.Item.FindControl("chkItem") as HtmlInputCheckBox;
                    TextBox txtweight = e.Item.FindControl("txtweight") as TextBox;

                    string suppcode = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "code"));

                    if (!string.IsNullOrEmpty(ItemInfo.runset))
                    {
                        foreach (string item in ItemInfo.runset.Split('|'))
                        {
                            string[] arr = item.Split(':');
                            if (arr[0] == suppcode)
                            {
                                chkItem.Checked = true;
                                txtweight.Text = arr[1];
                                break;
                            }
                        }
                    }
                }
            }
        }

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            string where = "release=1";
            if (ItemInfo.Class == ChannelClassEnum.在线支付)
            {
                where += " and isbank=1";
            }
            else if (ItemInfo.Class == ChannelClassEnum.充值卡)
            {
                where += "and iscard=1";
            }
            else if (ItemInfo.Class == ChannelClassEnum.声讯)
            {
                where += "and issx=1";
            }
            else if (ItemInfo.Class == ChannelClassEnum.短信)
            {
                where += " and issms=1";
            }

            DataTable ds = viviapi.BLL.Supplier.Factory.GetList("release=1").Tables[0];

            ds.Columns.Add("weight", typeof(int));

            foreach (DataRow row in ds.Rows)
            {
                row["weight"] = "0";
            }

            rptsupp.DataSource = ds;
            rptsupp.DataBind();
        }
        #endregion
    }
}
