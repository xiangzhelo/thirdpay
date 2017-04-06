using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebUI2015.usermodule.quota
{
    public partial class orderview : UserPageBase
    {
        protected string[] quotaType = new string[8];
        protected void Page_Load(object sender, EventArgs e)
        {
            quotaType[0] = "";
            quotaType[1] = "AG额度";
            quotaType[2] = "BBIN额度";
            quotaType[3] = "MG额度";
            quotaType[4] = "OG额度";
            quotaType[5] = "HG额度";
            quotaType[6] = "PT额度";
            quotaType[7] = "EBET额度";
            string orderid = Request.Params["orderid"];
            var listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("userid", UserId));
            listParam.Add(new SearchParam("orderid", orderid));
            DataSet pageData = viviapi.BLL.Quota.quotaOrder.getOrder(listParam);
            DataRow dsc = pageData.Tables[0].Rows[0];
            this.lbladdtime.Text = dsc["addtime"].ToString();
            this.lblcharge.Text = decimal.Parse(dsc["charge"].ToString()).ToString("f2");
            this.lblclientip.Text = dsc["clientip"].ToString();
            this.lblorderid.Text = dsc["orderid"].ToString();
            this.lblpayrate.Text = (decimal.Parse(dsc["payrate"].ToString())*100M).ToString("f2");
            this.lblquotatype.Text = quotaType[int.Parse(dsc["quota_type"].ToString())];
            this.lblquotaValue.Text = decimal.Parse(dsc["quotaValue"].ToString()).ToString("f2");
        }
    }
}