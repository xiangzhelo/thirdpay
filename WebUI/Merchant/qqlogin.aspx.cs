using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents.QqConnetSDK;

namespace viviAPI.WebUI7uka.Merchant
{
    public partial class Qqlogin : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GetRequestToken();
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetRequestToken()
        {
            QQConnet qc = new QQConnet();
            string url = qc.GetAuthorization_Code();
            string state = qc.State;

            Response.Redirect(url);
        }
    }
}
