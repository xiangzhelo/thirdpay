using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviapi.web.Manage
{
    public partial class EmailTest : viviapi.WebComponents.Web.ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Test_Click(object sender, EventArgs e)
        {
            string receives = this.txtReceives.Text;
            string subject = this.txtSubject.Text;
            string contect = this.txtcontent.Text;


            //viviapi.WebComponents.EmailHelper emailcom = new viviapi.WebComponents.EmailHelper( receives
            //          , receives + "测试邮件"
            //          , contect
            //          , true
            //          , System.Text.Encoding.GetEncoding("gbk"));

            //emailcom.Send();
        }
    }
}