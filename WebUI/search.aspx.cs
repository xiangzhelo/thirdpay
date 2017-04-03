using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBAccess;

using viviLib.Web;
using viviapi.Model;
using viviapi.BLL;

namespace viviapi.web
{
    public partial class search : viviapi.WebComponents.Web.PageBase
    {
        public int stype
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("stype", 0);

            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                hfsearchtype.Value = stype.ToString();
            }
        }

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

        protected void ibtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            string orderid = this.txtuserorder.Value.Trim();
            if (string.IsNullOrEmpty(orderid))
            {
                AlertAndRedirect("订单号不能为空");

                return;
            }
            //if (!viviLib.Text.PageValidate.IsNumber(orderid))
            //{
            //    AlertAndRedirect("订单号格式不正确");
            //    return;
            //}


            //System.Data.DataTable data = BLL.Order.Dal.OrderSearch(0, orderid);
            //if (data != null)
            //{
            //    this.order.DataSource = data;
            //    this.order.DataBind();
            //}
        }
    }
}
