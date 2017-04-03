using System;
using System.Web;
using System.Web.UI;
using viviapi.Model.User;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FindPwd :  PageBase
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string CheckValiDateNo(string str)
        {
            object obj2 = HttpContext.Current.Session["CCode"];
            if (obj2 == null)
            {
                return "验证码失效";
            }
            if (obj2.ToString().ToUpper().Equals(str.ToUpper()))
            {
                return "";
            }
            return "验证码不正确，请重新输入！";
        }


        protected void ibtnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            string msg = CheckValiDateNo(this.txtCode.Value);
            if (!string.IsNullOrEmpty(msg))
            {
                lblMessage.Text = msg;
                lblMessage.Visible = true;
                return;
            }
            string userName = this.newusername.Value.Trim();
            UserInfo userInfo = viviapi.BLL.User.Factory.GetModelByName(userName);
            if (userInfo.ID <= 0)
            {
                lblMessage.Text = "账号不存在,请重新输入";
                lblMessage.Visible = true;
                return;
            }
            else
            {
                Session["findpwduser"] = userName;
                Response.Redirect("/mobao/FindPwd2.aspx", true);
            }
        }
    }
}
