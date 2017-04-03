using System;
using System.Data;
using viviapi.BLL.Order.Card;
using viviapi.Model;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Reconciliation2 : ManagePageBase
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
                this.StimeBox.Text = DateTime.Now.AddHours(-24).ToString("yyyy-MM-dd HH:mm:ss");
                this.EtimeBox.Text = DateTime.Now.AddSeconds(-2).ToString("yyyy-MM-dd HH:mm:ss");

                this.StimeBox.Attributes.Add("onFocus", "WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})");
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.System);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        void Bind()
        {
            DateTime sdt = DateTime.Parse(this.StimeBox.Text);
            DateTime edt = DateTime.Parse(this.EtimeBox.Text);

            DataTable data = Factory.Instance.GetTimeoutRetrunOrders2(sdt, edt); //_bll.GetList("count<30").Tables[0];
            rptOrders.DataSource = data;
            rptOrders.DataBind();
        }
        protected void btn_search_Click(object sender, EventArgs e)
        {
            Bind();
        }

        #region ProcessNotify
        /// <summary>
        /// 记录在队列中失败列表 重新补发
        /// </summary>
        private  void ProcessNotify()
        {
            DateTime sdt = DateTime.Parse(this.StimeBox.Text);
            DateTime edt = DateTime.Parse(this.EtimeBox.Text);

            DataTable data = Factory.Instance.GetTimeoutRetrunOrders2(sdt, edt); //_bll.GetList("count<30").Tables[0];

            if (data != null)
            {
                viviapi.ETAPI.Common.OrderCardUtils.BatchQueryOrder(data);
            }
        }
        #endregion

        protected void btn_Reconciliation_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread t1 = new System.Threading.Thread(new ThreadStart(ProcessNotify));
            //t1.Start();

            //AlertAndRedirect("操作已提交后台执行。");

            ProcessNotify();
            AlertAndRedirect("执行完成。");
            Bind();
        }
    }
}