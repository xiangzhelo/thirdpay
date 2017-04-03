using System;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.Channel;
using viviapi.Model;
using viviapi.Model.Channel;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.channel
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ChannelEdit : ManagePageBase
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

        private ChannelInfo _ItemInfo = null;
        public ChannelInfo model
        {
            get
            {
                if (_ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        _ItemInfo =  Channel.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        _ItemInfo = new ChannelInfo();
                    }
                }
                return _ItemInfo;
            }
        }

        private ChannelTypeInfo _typeInfo = null;
        public ChannelTypeInfo TypeInfo
        {
            get
            {
                if (_typeInfo == null)
                {
                    if (model != null)
                    {
                        _typeInfo = ChannelType.GetModelByTypeId(model.typeId);
                    }
                    else
                    {
                        _typeInfo = new ChannelTypeInfo();
                    }
                }
                return _typeInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            viviapi.BLL.ManageFactory.CheckSecondPwd();
            
            if (!this.IsPostBack)
            {
                this.ddlType.Items.Add(new ListItem("---全部类别---", ""));
                DataTable types = viviapi.BLL.Channel.ChannelType.GetList(null).Tables[0];
                foreach (DataRow dr in types.Rows)
                {
                    this.ddlType.Items.Add(new ListItem(dr["modetypename"].ToString(), dr["typeId"].ToString()));
                }

                string where = string.Empty;
                DataTable list = viviapi.BLL.Supplier.Factory.GetList(where).Tables[0];
                //ddlTypeSupp.Items.Add(new ListItem("--请选择--", ""));
                //foreach (DataRow dr in list.Rows)
                //{
                //    this.ddlTypeSupp.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                //}

                ddlSupp.Items.Add(new ListItem("--默认--", "-1"));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupp.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }



                ShowInfo();
            }
        }

        void ShowInfo()
        {
            if (IsUpdate && model != null)
            {
                this.txtcode.Text = model.code;
                this.ddlType.SelectedValue = model.typeId.ToString();
                this.txtmodeName.Text = model.modeName;
                this.txtfaceValue.Text = model.faceValue.ToString();
                this.rblTypeOpen.SelectedValue = ((int)TypeInfo.isOpen).ToString();
                this.txtsort.Text = model.sort.ToString();
                txtenmodeName.Text = model.modeEnName;
                litTypeSupplier.Text = WebUtility.GetSupplierName(TypeInfo.supplier);
                //ddlTypeSupp.SelectedValue = TypeInfo.supplier.ToString();
                if (model.supplier != null)
                    ddlSupp.SelectedValue = model.supplier.Value.ToString();

                if(model.isOpen.HasValue)
                rblOpen.SelectedValue = model.isOpen.Value.ToString();
            }
            //ddlTypeSupp.Enabled = false;
            rblTypeOpen.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string code = this.txtcode.Text;
            int isOpen = -1;
            int.TryParse(rblOpen.SelectedValue, out isOpen);
            
            int supplier = -1;
            int.TryParse(this.ddlSupp.SelectedValue, out supplier);

            string modeName = this.txtmodeName.Text;
            int faceValue = int.Parse(this.txtfaceValue.Text);
            int typeid = 0;
            int.TryParse(ddlType.SelectedValue, out typeid);
            int sort = int.Parse(this.txtsort.Text);

            model.code = code;
            model.typeId = typeid;
            model.modeName = modeName;
            model.faceValue = faceValue;
            if (isOpen == -1)
                model.isOpen = null;
            else
                model.isOpen = isOpen;

            if(supplier == -1)
                model.supplier = null;
            else
                model.supplier = supplier;
            model.addtime = DateTime.Now;
            model.sort = sort;
            model.modeEnName = this.txtenmodeName.Text;

            string returnUrl = "List.aspx";
            if (Session["selecttype"] != null)
            {
                returnUrl += "?typeid=" + Session["selecttype"].ToString();
            }
            if (!this.IsUpdate)
            {
                int id = Channel.Add(model);
                //todo:能添加成功，但是id返回有误，导致显示保存失败，暂时取消判断
                //if (id > 0)
                //{
                    AlertAndRedirect("保存成功！", returnUrl);
                //}
                //else
                //{
                //    AlertAndRedirect("保存失败！");
                //}
            }
            else
            {
                if (Channel.Update(model))
                {
                    AlertAndRedirect("更新成功！", returnUrl);
                }
                else
                {
                    AlertAndRedirect("更新失败！");
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlTypeSupp.SelectedValue = "";
            int typeId = 0;
            int.TryParse(this.ddlType.SelectedValue, out typeId);
            if (typeId > 0)
            {
                viviapi.Model.Channel.ChannelTypeInfo type = ChannelType.GetModelByTypeId(typeId);
                if (type != null)
                {
                    //ddlTypeSupp.SelectedValue = type.supplier.ToString();
                    litTypeSupplier.Text = WebUtility.GetSupplierName(type.supplier);
                    rblTypeOpen.SelectedValue = ((int)type.isOpen).ToString();
                }
            }
        }
    }
}
