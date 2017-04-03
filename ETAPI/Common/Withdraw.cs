using viviapi.BLL.Withdraw;

namespace viviapi.ETAPI.Common
{
    public class Withdraw
    {
        static ChannelWithdraw _chalBLL = new ChannelWithdraw();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemInfo"></param>
        public static void InitDistribution(viviapi.Model.Finance.Withdraw itemInfo)
        {
            var info = new Model.Finance.WithdrawSuppTranLog()
            {
                trade_no = BLL.Finance.WithdrawSuppTranLog.Instance.GenerateOrderId(),
                mode = 1,
                settledId = itemInfo.ID,
                batchNo = 1,
                userid = itemInfo.Userid,
                balance = 0M,
                bankCode = itemInfo.BankCode,
                suppid = itemInfo.SuppId,
                bankName = itemInfo.PayeeBank,
                bankBranch = itemInfo.Payeeaddress,
                bankAccountName = itemInfo.PayeeName,
                bankAccount = itemInfo.Account,
                charges = itemInfo.Charges,
                amount = itemInfo.Amount - itemInfo.Charges,
                balance2 = 0,
                withdrawNo = itemInfo.Tranno
            };

            int id = BLL.Finance.WithdrawSuppTranLog.Instance.Add(info);

            if (id > 0)
            {
                SellFactory.ReqDistribution(info);
            }
        }


        public static void InitDistribution2(viviapi.Model.Finance.Agent.WithdrawAgent itemInfo)
        {
            //Model.distribution _info = new viviapi.Model.distribution();

            //_info.trade_no = BLL.distribution.GenerateTradeNo(2);
            //_info.suppid = itemInfo.suppid;
            //_info.mode = 2;
            //_info.settledId = itemInfo.id;
            //_info.batchNo = 1;
            //_info.userid = itemInfo.userid;
            //_info.balance = 0M;// balance - unpayment - Freeze;

            //_info.bankCode = itemInfo.bankCode;
            //_info.bankName = itemInfo.bankName;
            //_info.bankBranch = itemInfo.bankBranch;
            //_info.bankAccountName = itemInfo.bankAccountName;
            //_info.bankAccount = itemInfo.bankAccount;
            //_info.amount = itemInfo.amount;
            //_info.charges = itemInfo.charge;

            //_info.balance2 = 0;

            //int id = BLL.distribution.Add(_info);

            //if (id > 0)
            //{
            //    SellFactory.ReqDistribution(_info);
            //}
        }

        public static int Complete(int suppId
            , string trade_no
            , bool is_cancel
            , int status
            , string amount
            , string supp_trade_no
            , string message)
        {
            //string bill_trade_no = string.Empty;

            //int result = viviapi.BLL.distribution.Process(suppId
            //    , trade_no
            //    , is_cancel
            //    , status
            //    , amount
            //    , supp_trade_no
            //    , message
            //    , out bill_trade_no);

            //if (result == 0)
            //{
            //    string mode = trade_no.Substring(0, 1);
            //    if (mode == "2")
            //    {
            //        BLL.Finance.Agent.WithdrawAgent _bll = new BLL.Finance.Agent.WithdrawAgent();
            //        _bll.DoNotify(bill_trade_no);
            //    }
            //}

            //return result;

            return 0;
        }
    }
}
