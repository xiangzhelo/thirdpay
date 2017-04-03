using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using viviapi.Model;

namespace viviapi.WebComponents.Web
{
    /// <summary>
    /// MangePageBase 的摘要说明
    /// </summary>
    public class BusinessPageBase : PageBase
    {
        public DateTime sDate = Convert.ToDateTime("2012-01-01");
        public DateTime eDate = Convert.ToDateTime("2012-08-01");

        public bool isSuperAdmin
        {
            get
            {
                return currentManage.isSuperAdmin > 0;
            }
        }
        /// <summary>
        /// 当前登录的商户
        /// </summary>
        public Model.Manage currentManage
        {
            get
            {
                return BLL.ManageFactory.CurrentManage;
            }
        }

        public int ManageId
        {
            get
            {
                return currentManage.id;
            }
        }

        /// <summary>
        /// 商户ID
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return currentManage != null;
            }
        }      

        /// <summary>
        /// 
        /// </summary>
        public void checkLogin()
        {
//            if (HttpContext.Current.Request.Url.Host.IndexOf("leuyo.com") < 0)
//            {
//                string script = string.Format(@"
//<SCRIPT LANGUAGE='javascript'><!--
//alert('您还没有被授权使用');top.location.href=""{0}"";
////--></SCRIPT>", "/Console/Login.aspx");

//                HttpContext.Current.Response.Write(script);
//                HttpContext.Current.Response.End();
//            }
            if (DateTime.Now < sDate && DateTime.Now > eDate)
            {
                string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
alert('试用过期请联系管理员');top.location.href=""{0}"";
//--></SCRIPT>", "/Business/Login.aspx");

                HttpContext.Current.Response.Write(script);
                HttpContext.Current.Response.End();
            }
            if (!this.IsLogin)
            {
                string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
top.location.href=""{0}"";
//--></SCRIPT>", "/Business/Login.aspx");


                if (HttpContext.Current.Request.RawUrl.ToLower().IndexOf("agent") > 0)
                {
                    script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
top.location.href=""{0}"";
//--></SCRIPT>", "/agent/Login.aspx");
                }

                HttpContext.Current.Response.Write(script);
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            checkLogin();
        }
    }
}
