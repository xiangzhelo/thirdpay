using System;
using System.Collections.Generic;
using System.Data;
using viviapi.Model;
using viviLib.Data;

namespace viviAPI.WebAdmin.Console.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserUpdateLog : viviapi.WebComponents.Web.ManagePageBase
    {
       
              

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();

            if (!this.IsPostBack)
            {
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

                this.LoadData();
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Merchant);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            List<SearchParam> listParam = new List<SearchParam>();
           
            string keyword = KeyWordBox.Text.Trim();
            if (!string.IsNullOrEmpty(this.SeachType.SelectedValue) && !string.IsNullOrEmpty(keyword))
            {
                if (this.SeachType.SelectedValue.ToLower() == "userid")
                {
                    int userId = 0;
                    int.TryParse(keyword, out userId);
                    listParam.Add(new SearchParam("id", userId));
                }
                else if (this.SeachType.SelectedValue == "UserName")
                {
                    listParam.Add(new SearchParam("userName", keyword));
                }
            }

            if (!string.IsNullOrEmpty(this.StimeBox.Text))
            {
                listParam.Add(new SearchParam("stime", this.StimeBox.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.EtimeBox.Text))
            {
                listParam.Add(new SearchParam("etime", Convert.ToDateTime(this.EtimeBox.Text.Trim()).AddDays(1)));
            }

            DataSet pageData = viviapi.BLL.User.Factory.UpdateLogPageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptUsers.DataSource = pageData.Tables[1];
            this.rptUsers.DataBind();
        }
        #endregion

        protected  void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        public string GetViewName(object obj)
        {
            if (obj == DBNull.Value)
                return string.Empty;

            string viewName = obj.ToString();
            switch (viewName.ToLower())
            { 
                case "username":
                    viewName = "用户名";
                    break;
                case "password":
                    viewName = "用户密码";
                    break;
                case "cpsdrate":
                    viewName = "扣率";
                    break;
                case "cvsnrate":
                    viewName = "转率";
                    break;
                case "email":
                    viewName = "邮箱地址";
                    break;
                case "qq":
                    viewName = "QQ号码";
                    break;
                case "tel":
                    viewName = "手机号码";
                    break;
                case "idcard":
                    viewName = "身份证号码";
                    break;
                case "pmode":
                    viewName = "收款方式";
                    break;
                case "account":
                    viewName = "收款帐号";
                    break;
                case "userlevel":
                    viewName = "商户等级";
                    break;
                case "bankaddress":
                    viewName = "支行名称";
                    break;
                case "apikey":
                    viewName = "密钥";
                    break;
                case "maxdaytocashtimes":
                    viewName = "每日提现次数";
                    break;
                case "sitename":
                    viewName = "网站名称";
                    break;
                case "siteurl":
                    viewName = "网站地址";
                    break;
                case "payeename":
                    viewName = "收款人";
                    break;
                case "usertype":
                    viewName = "商户类别";
                    break;
                case "status":
                    viewName = "商户状态";
                    break;
            }
            return viewName;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selids = viviLib.Web.WebBase.GetFormString("chkItem", "");
            if (!string.IsNullOrEmpty(selids))
            {
                foreach (string id in selids.Split(','))
                {
                    int _id = 0;
                    if (int.TryParse(id, out _id))
                    {
                        viviapi.BLL.User.UserLoginLogFactory.Del(_id);
                    }
                }
            }
            this.LoadData();
        }        
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string selids = viviLib.Web.WebBase.GetFormString("chkItem", "");
            if (!string.IsNullOrEmpty(selids))
            {
                foreach (string id in selids.Split(','))
                {
                    int _id = 0;
                    if (int.TryParse(id, out _id))
                    {
                        viviapi.BLL.User.Factory.DelUpdateLog(_id);
                    }
                }
            }
            this.LoadData();
        }
}
}