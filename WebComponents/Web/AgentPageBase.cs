using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using viviapi.BLL.User;
using viviapi.Model;
using viviapi.Model.User;
using viviLib.Text;
using viviLib.Web;

namespace viviapi.WebComponents.Web
{
    /// <summary>
    /// viviapi.BLL.Web.UserPageBase 的摘要说明
    /// </summary>
    public class AgentPageBase : PageBase
    {
        private UserInfo _currentUser = null;
        /// <summary>
        /// 当前登录的商户
        /// </summary>
        public UserInfo CurrentUser
        {
            get
            {
                if (BLL.User.Login.IsLogin)
                {
                    int userId = BLL.User.Login.GetCurrent();
                    if (userId > 0)
                    {
                        return _currentUser ?? (_currentUser = Factory.GetModel(userId));
                    }
                }
                return new UserInfo();
            }
        }

        /// <summary>
        /// 结算模式
        /// </summary>
        public int SettlesMode
        {
            get
            {
                return CurrentUser.Settles;
            }
        }
        public int UserId
        {
            get
            {
                return CurrentUser.ID;
            }
        }

        /// <summary>
        /// 商户ID
        /// </summary>
        public bool IsLogin
        {
            get
            {
                if (BLL.User.Login.IsLogin)
                {
                    return UserId > 0;
                }
                return false;
            }
        }

        #region ViewValue
        public string UserViewEmail
        {
            get
            {
                return Strings.Mark(CurrentUser.Email, '@');
            }
        }

        public string UserViewTel
        {
            get
            {
                return Strings.Mark(CurrentUser.Tel);
            }
        }

        public string UserViewID
        {
            get
            {
                if (string.IsNullOrEmpty(CurrentUser.IdCard) || CurrentUser.IdCard.Length < 4)
                    return string.Empty;
                return Strings.ReplaceString(this.CurrentUser.IdCard, 3, CurrentUser.IdCard.Length - 3 - 4, "*");
            }
        }
        /// <summary>
        /// 当前登录的商户
        /// </summary>
        public string UserViewBankAccout
        {
            get
            {
                if (CurrentUser == null)
                    return "";
                return Strings.ReplaceString(CurrentUser.Account, 4, "*");
            }
        }
        public string UserViewIdCard
        {
            get
            {
                if (CurrentUser == null)
                    return "";
                return Strings.Mark(CurrentUser.IdCard);
            }
        }
        /// <summary>
        /// 当前登录的商户
        /// </summary>
        public string UserFullName
        {
            get
            {
                if (CurrentUser == null)
                    return "";
                return CurrentUser.full_name;
            }
        }

        /// <summary>
        /// 当前登录的商户
        /// </summary>
        public string UserName
        {
            get
            {
                if (CurrentUser == null)
                    return "";
                return CurrentUser.UserName;
            }
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetViewStatusName(object status)
        {
            if (status == DBNull.Value)
                return string.Empty;
            //viviapi.Model.Order.OrderStatusEnum _stat = (viviapi.Model.Order.OrderStatusEnum)Convert.ToInt32(status);
            if (Convert.ToInt32(status) == 8)
                return "失败";
            else
                return Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum), status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetViewSuccessAmt(object status, object amt)
        {
            if (status == DBNull.Value || amt == DBNull.Value)
                return "0";

            //viviapi.Model.Order.OrderStatusEnum _stat = (viviapi.Model.Order.OrderStatusEnum)Convert.ToInt32(status);
            if (Convert.ToInt32(status) == 2)
                return decimal.Round(Convert.ToDecimal(amt), 2).ToString();
            else
                return "0";
        }



        /// <summary>
        /// 
        /// </summary>
        public void CheckLogin()
        {
            if (!this.IsLogin)
            {
                const string msg = "对不起！你的登录信息已失效，请重新登录";

                string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
alert({0});
top.location.href=""{1}"";
//--></SCRIPT>", viviLib.Security.AntiXss.JavaScriptEncode(msg), "/agentlogin.aspx");

                HttpContext.Current.Response.Write(script);
                HttpContext.Current.Response.End();
            }
            else
            {
                if (BLL.User.Login.IsManageLogin() == false)
                {
                    var model = new UserAccessTimeInfo { userid = UserId, lastAccesstime = DateTime.Now };
                    BLL.User.UserAccessTime.Add(model);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CheckLogin();
        }

        public string GetValue(string key)
        {
            return WebBase.GetFormString(key, "");
        }

        #region NewUpdateLog
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="field"></param>
        /// <param name="newvalue"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public UsersUpdateLog NewUpdateLog(int userid, string field, string newvalue, string oldValue)
        {
            var item = new UsersUpdateLog
            {
                userid = userid,
                Addtime = DateTime.Now,
                field = field,
                newvalue = newvalue,
                oldValue = oldValue
            };
            return item;
        }
        #endregion
    }
}
