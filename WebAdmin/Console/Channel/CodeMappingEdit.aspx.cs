using System;
using System.Data;
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
    public partial class CodeMappingEdit : ManagePageBase
    {
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public bool isUpdate
        {
            get
            {
                return ItemInfoId > 0;
            }
        }

        public CodeMappingInfo _ItemInfo = null;
        public CodeMappingInfo model
        {
            get
            {
                if (_ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        _ItemInfo = CodeMappingFactory.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        _ItemInfo = new CodeMappingInfo();
                    }
                }
                return _ItemInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                this.ddlpmode.Items.Add(new ListItem("---全部通道---", ""));
                DataTable pmodes = viviapi.BLL.Channel.Channel.GetList(102).Tables[0];
                foreach (DataRow dr in pmodes.Rows)
                {
                    this.ddlpmode.Items.Add(new ListItem(dr["modeName"].ToString(), dr["code"].ToString()));
                }

                string where = "isbank=1";
                DataTable list = viviapi.BLL.Supplier.Factory.GetList(where).Tables[0];
                ddlsupp.Items.Add(new ListItem("--请选择--", ""));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlsupp.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }

                ShowInfo();
            }
        }

        void ShowInfo()
        {
            if (isUpdate && model != null)
            {
                this.ddlpmode.SelectedValue = model.pmodeCode;
                this.ddlsupp.SelectedValue = model.suppId.ToString();
                this.txtsuppCode.Text = model.suppCode;
            }
        }

        void Save(string url)
        {
            string pmodeCode = ddlpmode.SelectedValue;
            int suppId = int.Parse(ddlsupp.SelectedValue);
            string suppCode = this.txtsuppCode.Text.Trim();

            model.pmodeCode = pmodeCode;
            model.suppId = suppId;
            model.suppCode = suppCode;

            if (!this.isUpdate)
            {
                int id = CodeMappingFactory.Add(model);
                if (id > 0)
                {
                    AlertAndRedirect("保存成功！", url);
                }
                else
                {
                    AlertAndRedirect("保存失败！");
                }
            }
            else
            {
                if (CodeMappingFactory.Update(model))
                {
                    AlertAndRedirect("更新成功！", url);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save("CodeMappinglList.aspx");
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
        protected void btnCont_Click(object sender, EventArgs e)
        {
            Save("CodeMappingEdit.aspx");
        }
    }
}
