using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using viviLib.Web;
using viviapi.Model;
using viviapi.BLL;
using viviapi.Model.User;
using viviapi.BLL.User;
using viviapi.Model.Payment;
using System.Collections.Generic;
using viviLib.Security;

namespace viviapi.web.business
{
    /// <summary>
    /// 
    /// </summary>
    public partial class QuestionEdit : viviapi.WebComponents.Web.BusinessPageBase
    {
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public string Action
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }
        public bool isUpdate
        {
            get
            {
                return ItemInfoId > 0 && Action == "edit";
            }
        }

        public Model.User.QuestionInfo _ItemInfo = null;
        public Model.User.QuestionInfo ItemInfo
        {
            get
            {
                if (_ItemInfo == null)
                {
                    if (isUpdate)
                    {
                        BLL.User.Question bll = new Question();
                        _ItemInfo = bll.GetModel(ItemInfoId);
                    }
                    else
                    {
                        _ItemInfo = new viviapi.Model.User.QuestionInfo();
                    }
                }
                return _ItemInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                this.InitForm();
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.System);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void InitForm()
        {
            this.txtquestion.Text = ItemInfo.question;
            this.chkrelease.Checked = ItemInfo.release;
            this.txtsort.Text = ItemInfo.sort.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BLL.User.Question bll = new Question();
            string question = this.txtquestion.Text;
            bool release = this.chkrelease.Checked;
            int sort = int.Parse(this.txtsort.Text);

            ItemInfo.question = question;
            ItemInfo.release = release;
            ItemInfo.sort = sort;


            bool success = false;
            if (this.isUpdate)
            {
                if (bll.Update(ItemInfo))
                {
                    success = true;
                }
            }
            else
            {
                if (bll.Add(ItemInfo) > 0)
                {
                    success = true;
                }
            }
            if (success)
            {
                AlertAndRedirect("操作成功", "Questions.aspx");
            }
            else
            {
                AlertAndRedirect("操作失败");
            }
        }

        

    }
}
