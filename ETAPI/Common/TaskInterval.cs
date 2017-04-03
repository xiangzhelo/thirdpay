using System;
using System.Data;
using viviapi.BLL;
using viviapi.Model.Order.Card;
using viviapi.Model.supplier;
using viviLib.ExceptionHandling;
using viviLib.ScheduledTask;

namespace viviapi.ETAPI.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskInterval : IScheduledTaskExecute
    {
        /// <summary>
        /// 执行
        /// </summary>
        public void Execute()
        {
            //超时未返回的卡类订单
            ProcessTimeoutRetrun();

            //超时未返回，切换通道重新提交
           // ProcessToggleInterface();
        }

        #region ProcessTimeoutRetrun
        /// <summary>
        /// 
        /// </summary>
        private static void ProcessTimeoutRetrun()
        {
            try
            {
                DateTime sdt = DateTime.Now.AddHours(-24);
                DateTime edt = DateTime.Now.AddMinutes(-2);

                //超时未返回的卡类订单 通过查询方式
                DataTable orders = BLL.Order.Card.Factory.Instance.GetTimeoutRetrunOrders(sdt, edt); //_bll.GetList("count<30").Tables[0];

                if (orders != null)
                {
                    OrderCardUtils.BatchQueryOrder(orders);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
        #endregion

        #region ProcessToggleInterface
        /// <summary>
        /// 超时未返回，切换通道重新提交
        /// </summary>
        private static void ProcessToggleInterface()
        {
            try
            {
                DateTime sdt = DateTime.Now.AddDays(-1);
                DateTime edt = DateTime.Now;

                DataTable data = BLL.Order.Card.Factory.Instance.GetToggleInterfaceList(sdt, edt);
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        string supporderid = "";
                        string suppererrcode = "";
                        string supperrinfo = "";

                        string sysorderid = row["orderid"].ToString();
                        int suppid = int.Parse(row["supplierID"].ToString());
                        decimal refervalue = decimal.Parse(row["refervalue"].ToString());
                        var supp = (SupplierCode)suppid;

                        var o = new CardOrderSummitArgs()
                        {
                            SysOrderNo = sysorderid,
                            CardTypeId = int.Parse(row["typeId"].ToString()),
                            CardNo = row["cardNo"].ToString(),
                            CardPass = row["cardPwd"].ToString(),
                            FaceValue = decimal.ToInt32(refervalue),
                            Attach = "",
                            Source = 3
                        };
                        
                        OrderCardUtils.SendCard((SupplierCode)suppid, o);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
        #endregion
    }
}
