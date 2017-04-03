using System;

namespace viviAPI.WebAdmin
{
    public partial class Tools : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = this.TextBox1.Text;
            this.TextBox2.Text = viviLib.Security.Cryptography.RijndaelEncrypt(str);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string str = this.TextBox1.Text;
            this.TextBox2.Text = viviLib.Security.Cryptography.RijndaelDecrypt(str);
        }
    }
}
