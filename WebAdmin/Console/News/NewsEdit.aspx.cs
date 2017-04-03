using System;
using System.Web;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.News;
using viviapi.Model;
using viviapi.Model.News;
using viviapi.WebComponents.Web;
using viviLib.TimeControl;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.News
{
    /// <summary>
    /// 
    /// </summary>
    public partial class NewsEdit : ManagePageBase
    {
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public bool IsUpdate
        {
            get
            {
                return ItemInfoId > 0;
            }
        }

        private NewsInfo _itemInfo = null;
        public NewsInfo ItemInfo
        {
            get
            {
                if (_itemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        _itemInfo = NewsFactory.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        _itemInfo = new NewsInfo();
                    }
                }
                return _itemInfo;
            }
        }

        protected string Newscontent = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                ShowInfo();
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.News);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        void ShowInfo()
        {
            this.txtDate.Attributes.Add("onFocus", "WdatePicker()");
            foreach (int num in Enum.GetValues(typeof(NewsType)))
            {
                this.ddl_type.Items.Add(new ListItem(Enum.GetName(typeof(NewsType), num), num.ToString()));
            }

            if (IsUpdate && ItemInfo != null)
            {
                this.ddl_type.SelectedValue = ItemInfo.newstype.ToString();
                this.txtTitle.Text = ItemInfo.newstitle;
                this.txtDate.Value = FormatConvertor.DateTimeToDateString(ItemInfo.addTime);
                this.hfcontent.Value = HttpUtility.HtmlEncode(ItemInfo.newscontent);
                this.cb_red.Checked = ItemInfo.IsRed == 1;
                this.cb_top.Checked = ItemInfo.IsTop == 1;
                this.cb_pop.Checked = ItemInfo.IsPop == 1;
                this.cb_bold.Checked = ItemInfo.Isbold == 1;
                this.rbl_Release.SelectedValue = ItemInfo.release ? "1" : "0";

                this.txtColorCode.Text = ItemInfo.Color;

                Newscontent = HttpUtility.HtmlDecode(ItemInfo.newscontent);
            }
            else
            {
                this.txtDate.Value = FormatConvertor.DateTimeToDateString(DateTime.Now);
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            bool success = false;
            int newstype = int.Parse(this.ddl_type.SelectedValue);
            string newstitle = this.txtTitle.Text;
            DateTime addTime = DateTime.Parse(this.txtDate.Value);
            string newscontent = this.hfcontent.Value;
            int IsRed = this.cb_red.Checked ? 1 : 0;
            int IsTop = this.cb_top.Checked ? 1 : 0;
            int IsPop = this.cb_pop.Checked ? 1 : 0;
            int Isbold = this.cb_bold.Checked ? 1 : 0;
            string color = this.txtColorCode.Text.Trim();

            ItemInfo.newstype = (NewsType) newstype;
            ItemInfo.newstitle = newstitle;
            ItemInfo.addTime = addTime;
            ItemInfo.newscontent = newscontent;
            ItemInfo.IsRed = IsRed;
            ItemInfo.IsTop = IsTop;
            ItemInfo.IsPop = IsPop;
            ItemInfo.Isbold = Isbold;
            ItemInfo.Color = color;
            ItemInfo.release = rbl_Release.SelectedValue == "1";

            if (IsUpdate)
            {
                success = NewsFactory.Update(ItemInfo);
            }
            else
            {
                success = NewsFactory.Add(ItemInfo) > 0;
            }

            if (success)
            {
                AlertAndRedirect("保存成功!", "NewsList.aspx");
            }
            else
            {
               ShowMessageBox("操作失败!");
            }
        }
    }
}
