using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Order.Card
{
    public class CardOrderSupplierResponse
    {

        /// <summary>
        /// 是否为同步提交
        /// </summary>
        public int Sync { get; set; }
        /// <summary>
        /// 供应商接口ID
        /// </summary>
        public int SupplierId { get; set; }
        /// <summary>
        /// 接口系统交易单号
        /// </summary>
        public string SuppTransNo { get; set; }
        /// <summary>
        /// 本系统内部单号
        /// </summary>
        public string SysOrderNo { get; set; }
        /// <summary>
        /// 充值卡面值
        /// </summary>
        public decimal OrderAmt { get; set; }
        /// <summary>
        /// 接口给平台的结算价
        /// </summary>
        public decimal SuppAmt { get; set; }
        /// <summary>
        /// 成功返回2，失败返回4
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 接口返回的代码
        /// </summary>
        public string SuppErrorCode { get; set; }
        /// <summary>
        /// 0:支付成功
        /// -1:卡号或密码错误
        ///-11: 网络出错 系统繁忙
        ///-10:卡余额不足
        /// </summary>
        public string Opstate { get; set; }
        /// <summary>
        /// 接口返回的错误信息
        /// </summary>
        public string SuppErrorMsg { get; set; }
        public string ViewMsg { get; set; }
        public byte ContinueSubmit { get; set; }

        /// <summary>
        /// 系统返回 2
        /// 接口商返回 1
        /// </summary>
        public byte Method { get; set; }


        public CardOrderSupplierResponse()
        {
            Sync = 0;
        }
    }
}
