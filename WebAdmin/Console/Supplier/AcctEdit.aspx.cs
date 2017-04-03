using System;
using System.Collections.Generic;
using System.Data;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Supplier
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AcctEdit : ManagePageBase
    {
        viviapi.BLL.Supplier.SupplierAccount suppAcctBll= new SupplierAccount();
        public int SuppCode
        {
            get
            {
                return WebBase.GetQueryStringInt32("code", 0);
            }
        }


        private SupplierInfo _suppInfo = null;
        public SupplierInfo SuppInfo
        {
            get
            {
                if (_suppInfo != null) return _suppInfo;

                _suppInfo = this.SuppCode > 0 ? Factory.GetModelByCode(SuppCode) : new SupplierInfo();

                return _suppInfo;
            }
        }

        public int ObjId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        private viviapi.Model.Supplier.SupplierAccount _itemInfo = null;
        public viviapi.Model.Supplier.SupplierAccount ItemInfo
        {
            get
            {
                if (_itemInfo != null) return _itemInfo;

                _itemInfo = this.ObjId > 0 ? suppAcctBll.GetModel(ObjId) : new viviapi.Model.Supplier.SupplierAccount();

                return _itemInfo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (SuppInfo != null)
                {
                    this.lblSupplierName.Text = SuppInfo.name;

                    if (ItemInfo != null)
                    {
                        //this.txtcode.Text = model.code.ToString();
                        this.txtname.Text = ItemInfo.name;
                        this.txtapiAccount.Text = ItemInfo.apiAccount;
                        this.txtapiKey.Text = ItemInfo.apiKey;
                        this.txtuserName.Text = ItemInfo.userName;
                       // this.txtemail.Text = model.email;
                        this.txtdomain.Text = ItemInfo.domain;
                        //this.txtjumpdomain.Text = model.jumpdomain;
                        this.ckbisdefault.Checked = ItemInfo.available;
                        //this.txtisdefault.Text = model.isdefault.ToString();
                        this.txtjumpdomain.Text = ItemInfo.jumpdomain;
                    }

                    LoadData();
                }
               
            }
        }

        
        private void LoadData()
        {
            DataSet ds = suppAcctBll.GetList("code=" + SuppCode);
            this.rptdata.DataSource = ds;
            this.rptdata.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (SuppInfo == null)
                return;

            int code = this.SuppInfo.code.Value;
            string name = this.txtname.Text;
            string apiAccount = this.txtapiAccount.Text;
            string apiKey = this.txtapiKey.Text;
            string userName = this.txtuserName.Text;
            string email = string.Empty;
            string domain = this.txtdomain.Text;
            string jumpdomain = this.txtjumpdomain.Text;
            bool available = true;
            int isdefault = this.ckbisdefault.Checked ? 1 : 0;

            ItemInfo.code = code;
            ItemInfo.name = name;
            ItemInfo.apiAccount = apiAccount;
            ItemInfo.apiKey = apiKey;
            ItemInfo.userName = userName;
            ItemInfo.email = email;
            ItemInfo.domain = domain;
            ItemInfo.jumpdomain = jumpdomain;
            ItemInfo.available = available;
            ItemInfo.isdefault = isdefault;

            suppAcctBll.Insert(ItemInfo);

            AlertAndRedirect("操作成功", "AcctEdit.aspx?code=" + code);
        }

        protected void rptdata_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                suppAcctBll.Delete(Convert.ToInt32(e.CommandArgument));
                LoadData();
            }
        }

      
    }
}