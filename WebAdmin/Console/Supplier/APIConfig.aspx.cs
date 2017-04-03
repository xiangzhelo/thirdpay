using System;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.supplier
{
    public partial class APIConfig : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                WebUtility.BindBankSupplierDDL(ddlbankurl);
                WebUtility.BindCardSupplierDLL(ddlisurl);
                WebUtility.BindCardSupplierDLL(ddlszx);
                WebUtility.BindCardSupplierDLL(ddlsd);
                WebUtility.BindCardSupplierDLL(ddlzt);
                WebUtility.BindCardSupplierDLL(ddljw);
                WebUtility.BindCardSupplierDLL(ddlqq);
                WebUtility.BindCardSupplierDLL(ddllt);
                WebUtility.BindCardSupplierDLL(ddljy);
                WebUtility.BindCardSupplierDLL(ddlwy);
                WebUtility.BindCardSupplierDLL(ddlwm);
                WebUtility.BindCardSupplierDLL(ddlsh);
                WebUtility.BindCardSupplierDLL(ddlonline);
                WebUtility.BindCardSupplierDLL(ddlking);
                WebUtility.BindCardSupplierDLL(ddlmoko);
                WebUtility.BindCardSupplierDLL(ddl5173);
                WebUtility.BindCardSupplierDLL(ddlrxk);
                WebUtility.BindCardSupplierDLL(ddldx);
                WebUtility.BindSMSSupplierDLL(ddlsms);
                WebUtility.BindSXSupplierDLL(ddlsxk);

            }
        }
        protected void purlok_Click(object sender, EventArgs e)
        {

        }
    }
}
