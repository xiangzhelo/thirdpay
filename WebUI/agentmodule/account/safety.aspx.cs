using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.agentmodule.account
{
    public partial class Safety : AgentPageBase
    {
        /// <summary>
        /// 提现密码
        /// </summary>
        public string gettxpass = "";
        public string gettxbtn = "";
        /// <summary>
        /// 密保设置
        /// </summary>
        public string getmb = "";
        public string getmbbtn = "";
        /// <summary>
        /// 手机认证
        /// </summary>
        public string getphone = "";
        public string getphonebtn = "";
        /// <summary>
        /// 邮箱认证
        /// </summary>
        public string getemail = "";
        public string getemailbtn = "";
        public string getemailurl = "modiemail";
        /// <summary>
        /// 实名认证
        /// </summary>
        public string getidcard = "";
        public string getidcardbtn = "";

        protected string LastLoginTime = "";


        private UserInfo _thisUser = null;
        /// <summary>
        /// 
        /// </summary>
        public UserInfo ThisUser
        {
            get { return _thisUser ?? (_thisUser = Factory.GetModel(UserId)); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void InitForm()
        {
            var loginInfo = viviapi.BLL.User.UserLogin.Instance.GetModel(UserId);

            if (loginInfo != null)
            {
                if (loginInfo.lastLoginTime != null)
                    LastLoginTime = loginInfo.lastLoginTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (ThisUser.Password2.Length > 1)
            {
                gettxpass = "<span class=\"label label-success\">保护中</span>";
                gettxbtn = "修改";
            }
            else
            {
                gettxpass = "<span class=\"label label-warning\">还未设置提现密码</span>";
                gettxbtn = "设置";
            }

            if (ThisUser.answer.Length > 0)
            {
                getmb = "<span class=\"label label-success\">已设置</span>";
                getmbbtn = "修改";
            }
            else
            {
                getmb = "<span class=\"label label-warning\">还未设置密保问题</span>";
                getmbbtn = "设置";
            }

            if (ThisUser.IsPhonePass == 1)
            {
                getphone = "<span class=\"label label-success\">已认证</span>";
                getphonebtn = "修改";
            }
            else
            {
                getphone = "<span class=\"label label-warning\">还未进行手机认证</span>";
                getphonebtn = "认证";
            }

            if (ThisUser.IsEmailPass == 1)
            {
                getemail = "<span class=\"label label-success\">已认证</span>";
                getemailbtn = "修改";
            }
            else
            {
                getemail = "<span class=\"label label-warning\">还未进行邮箱认证</span>";
                getemailbtn = "认证";
                getemailurl = "doEmailauthen";
            }

            if (ThisUser.IsRealNamePass == 1)
            {
                getidcard = "<span class=\"label label-success\">已认证</span>";
                getidcardbtn = "修改";
            }
            else
            {
                getidcard = "<span class=\"label label-warning\">还未进行实名认证</span>";
                getidcardbtn = "认证";
            }
        }
    }
}
