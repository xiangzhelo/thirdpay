using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;
using System.Data;

namespace viviAPI.WebUI7uka.agentmodule.product
{
    public partial class Cardprice : AgentPageBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadData();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void LoadData()
        {
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();
            int typeId = 0;
            if (fType.Value != "0")
            {
                typeId = int.Parse(this.fType.Value);
            }
            int faceValue = 0;
            if (string.IsNullOrEmpty(this.txtfacevalue.Value))
            {
                int.TryParse(txtfacevalue.Value.Trim(), out faceValue);
            }
            int cstatus = -1;
            if (fState.Value != "-1")
            {
                cstatus = int.Parse(this.fState.Value);
            }
            int pageSize = this.Pager1.PageSize;
            int pageIndex = this.Pager1.CurrentPageIndex;

            DataSet pageData = viviapi.BLL.Channel.Channel.GetCardChanels(pageIndex, pageSize, this.UserId, typeId, faceValue, cstatus); //PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptcardtypes.DataSource = pageData.Tables[1];
            this.rptcardtypes.DataBind();
        }

        protected string GetTogTypeCode(object code)
        {
            if (code == null || code == DBNull.Value)
                return string.Empty;

            string temp = code.ToString();
            if (temp.Length <= 4)
                return temp;

            return temp.Substring(0, 4);

        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void b_search_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected string GetStautsName(object value)
        {
            if (value == null || value == DBNull.Value)
                return string.Empty;

            string temp = value.ToString();
            if (temp == "1")
                return "开启";
            else
                return "关闭";
        }
    }
}
