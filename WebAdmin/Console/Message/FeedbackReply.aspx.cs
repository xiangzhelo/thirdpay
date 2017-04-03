using System;
using viviapi.BLL.Communication;
using viviapi.BLL.User;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Message
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FeedbackReply : ManagePageBase
    {
        /// <summary>
        /// 
        /// </summary>
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }
        private feedbackInfo _itemInfo = null;
        public feedbackInfo ItemInfo
        {
            get
            {
                if (_itemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        feedback bll = new feedback();
                        _itemInfo = bll.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        _itemInfo = new feedbackInfo();
                    }
                }
                return _itemInfo;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private UserInfo _userInfo = null;
        public UserInfo model
        {
            get
            {
                if (_userInfo == null && ItemInfo != null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        _userInfo = Factory.GetModel(this.ItemInfo.userid);
                    }
                    else
                    {
                        _userInfo = new UserInfo();
                    }
                }
                return _userInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();

            if (!this.IsPostBack)
            {
                ShowInfo();
            }
        }

        #region ShowInfo
        /// <summary>
        /// 
        /// </summary>
        void ShowInfo()
        {
            if (model != null)
            {
                this.txtuserid.Text = ItemInfo.userid.ToString();
                this.txttypeid.Text = Enum.GetName(typeof(viviapi.Model.feedbacktype), (int)ItemInfo.typeid);
                this.txttitle.Text = ItemInfo.title;
                this.txtcont.Text = ItemInfo.cont;
                this.txtstatus.Text = Enum.GetName(typeof(viviapi.Model.feedbackstatus), (int)ItemInfo.status);
                this.txtaddtime.Value = ItemInfo.addtime.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtreply.Text = ItemInfo.reply;
                this.txtreplyer.Text = ItemInfo.replyer.ToString();
                this.txtreplytime.Value = ItemInfo.replytime.Value.ToString("yyyy-MM-dd HH:mm:ss");

                txtuserid.Enabled = false;
                txttypeid.Enabled = false;
                txttitle.Enabled = false;
                txtcont.Enabled = false;

            }
        }
        #endregion

        #region Save
        void Save()
        {

            ItemInfo.reply = this.txtreply.Text;
            ItemInfo.status = feedbackstatus.已回复;
            ItemInfo.replytime = DateTime.Now;
            ItemInfo.replyer = ManageId;

            feedback bll = new feedback();
            if (bll.Update(ItemInfo))
            {
                WebUtility.AlertAndClose(this, "操作成功");
                //AlertAndRedirect("操作成功", "UserIdImgList.aspx?s=1");
            }
            else
            {
                WebUtility.AlertAndClose(this, "操作失败");
            }
        }
        #endregion


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

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}