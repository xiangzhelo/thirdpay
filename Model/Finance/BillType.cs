using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public enum BillType
    {
        /// <summary>
        /// 网银收益
        /// </summary>
        BankOrderEarnings = 1,

        /// <summary>
        /// 点卡收益
        /// </summary>
        CardOrderEarnings = 2,

        /// <summary>
        /// 代理提成
        /// </summary>
        AgentIncome=3,

        /// <summary>
        /// 后台加款
        /// </summary>
        IncreaseMoney=4,

        /// <summary>
        /// 扣量返还
        /// </summary>
        BuckleReturn=5,

        /// <summary>
        /// 转账加款
        /// </summary>
        Transfer=6,

        /// <summary>
        /// 账户充值
        /// </summary>
        Recharge=7,

        /// <summary>
        /// 提现
        /// </summary>
        Withdraw=100,

        /// <summary>
        /// 后台减款
        /// </summary>
        Reduce = 104,

        /// <summary>
        /// 扣单
        /// </summary>
        Buckle=105,

        /// <summary>
        /// 转账扣除
        /// </summary>
        TransferDeduct=106,

        /// <summary>
        /// 冻结
        /// </summary>
        Freeze = 107
    }
}
