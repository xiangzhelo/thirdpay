using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using viviapi.Model.Finance;

namespace viviapi.ETAPI.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class Withdrawal
    {
        #region InitDistribution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemInfo"></param>
        public static void InitDistribution(Model.Finance.Withdraw itemInfo)
        {
            var info = new WithdrawSuppTranLog
            {
                trade_no = BLL.Finance.WithdrawSuppTranLog.Instance.GenerateOrderId(),
                mode = 1,
                settledId = itemInfo.ID,
                withdrawNo = itemInfo.Tranno,
                batchNo = 1,
                userid = itemInfo.Userid,
                balance = 0M,
                suppid = itemInfo.SuppId,
                bankCode = itemInfo.PayeeBank,
                bankName = BLL.Withdraw.ChannelWithdraw.GetSettleBankName(itemInfo.PayeeBank),
                bankBranch = itemInfo.Payeeaddress,
                bankAccountName = itemInfo.PayeeName,
                bankAccount = itemInfo.Account,
                amount = itemInfo.Amount - itemInfo.Charges,
                charges = itemInfo.Charges,
                ext1 = itemInfo.AccoutType.ToString(CultureInfo.InvariantCulture),
                ext2 = itemInfo.BankProvince,
                ext3 = itemInfo.BankCity,
                balance2 = 0,
                status = 1
            };

            int id = BLL.Finance.WithdrawSuppTranLog.Instance.Add(info);

            if (id > 0)
            {
                ReqDistribution(info);
            }
        }
        #endregion

        #region ReqDistribution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public static void ReqDistribution(WithdrawSuppTranLog info)
        {
            bool result = false;
            switch (info.suppid)
            {
                case 101:
                    {
                        var alipay = new Alipay.BatchPay();
                        result = alipay.PayReq(info);
                    }
                    break;
            }
        }
        #endregion

        #region Complete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="suppId"></param>
        /// <param name="tradeNo"></param>
        /// <param name="isCancel"></param>
        /// <param name="status"></param>
        /// <param name="amount"></param>
        /// <param name="suppTradeNo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int Complete(int suppId
           , string tradeNo
           , bool isCancel
           , int status
           , string amount
           , string suppTradeNo
           , string message)
        {
            string billTradeNo;

            int result = viviapi.BLL.Finance.WithdrawSuppTranLog.Instance.Process(suppId
                , tradeNo
                , isCancel
                , status
                , amount
                , suppTradeNo
                , message
                , out billTradeNo);

            if (result == 0)
            {
                //string mode = tradeNo.Substring(0, 1);
                //if (mode == "Y2")
                //{
                //    BLL.Withdraw.settledAgent _bll = new viviapi.BLL.Withdraw.settledAgent();
                //    _bll.DoNotify(billTradeNo);
                //}
            }

            return result;
        }
        #endregion
    }
}
