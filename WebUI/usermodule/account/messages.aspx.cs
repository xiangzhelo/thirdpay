using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using viviapi.BLL.Communication;
using viviapi.WebComponents.Web;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;
using System.Web.UI.HtmlControls;

namespace viviAPI.WebUI7uka.usermodule.account
{
    public partial class Messages : UserPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
                LoadData();
            }
        }

        #region InitForm
        void InitForm()
        {
            this.sdate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.edate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }
        #endregion

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();
            listParam.Add(new viviLib.Data.SearchParam("msg_to", UserId)); 
            listParam.Add(new viviLib.Data.SearchParam("receiverId", UserId)); 
             DateTime tempdt = DateTime.MinValue;
            if (DateTime.TryParse(builderdate(this.sdate.Value, "00", "00", "00"), out tempdt))
            {
                if (tempdt > DateTime.MinValue)
                {
                    listParam.Add(new viviLib.Data.SearchParam("stime", tempdt.ToString()));
                }
            }

            if (DateTime.TryParse(builderdate(this.edate.Value, "23", "59", "59"), out tempdt))
            {
                if (tempdt > DateTime.MinValue)
                {
                    listParam.Add(new viviLib.Data.SearchParam("etime", tempdt.ToString()));
                }
            }
            try
            {
                DataSet pageData = viviapi.BLL.Communication.InternalMessage.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
                this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
                this.msg_data.DataSource = pageData.Tables[1];
                this.msg_data.DataBind();
            }
            catch { }

        }
        #endregion

        public string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", date, hour, m, s);
        }
        /// <summary>
        /// 信息是否已读取
        /// </summary>
        /// <returns></returns>
        public string GetMsgTit(string msg, bool isrd)
        {
            string result = "";
            if (!isrd)
            {
                result = "<span style=\"font-weight:bold; color:black;\">" + msg + "</span>";
            }
            else
            {
                result = msg;
            }
            return result;
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void b_search_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnRpt_Click(object sender, EventArgs e)
        {
            //string ids = Request.Form["MsgId"];

           
            bool result = true;
            string ids = "";
            int index=0;
            //遍历repeater控件的itemtemplate模版  
            foreach (RepeaterItem item in msg_data.Items)
            {
                if (index > 1)
                    ids += ",";
                CheckBox cb = (CheckBox)item.FindControl("ckbIndex"); //根据控件id获得控件对象，cdDelete是checkBox控件的id  
                if (cb.Checked == true)
                {
                    Label label = (Label)item.FindControl("lbmsid");
                    string id = label.Text;
                    ids += id;
                }
                
            }
            int success = 0;
            if (!string.IsNullOrEmpty(ids))
            {
                success += (from id in ids.Split(',') where !string.IsNullOrEmpty(id) select InternalMessage.Delete(int.Parse(id))).Count(bl => bl);

                AlertAndRedirect("成功删除" + success + "条记录");
            }
            else
            {
                AlertAndRedirect("请至少选择一条");
            }
            LoadData();
        }
    }
}

