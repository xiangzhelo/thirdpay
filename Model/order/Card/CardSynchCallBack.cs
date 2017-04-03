using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Order.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class CardSynchCallBack
    {

        private DateTime _initTime = DateTime.Now;

        public DateTime InitTime
        {
            get
            {
                return _initTime;
            }
            set
            {
                _initTime = value;
            }

        }

        public int SupplierId { get; set; }
        /// <summary>
        /// 成功金额
        /// </summary>
        public decimal SuccAmt { get; set; }

        /// <summary>
        /// 提交成功 
        /// 提交过程是否出现网络通讯错误
        /// </summary>
        public byte Success { get; set; }

        /// <summary>
        /// 订单提交状态
        /// </summary>
        public byte SummitStatus { get; set; }

        /// <summary>
        /// 订单处理状态
        /// </summary>
        public byte OrderStatus { get; set; }


        /// <summary>
        /// 接口商 返回订单号
        /// </summary>
        public string SuppTransNo { get; set; }

        /// <summary>
        /// 接口商返回文本
        /// </summary>
        public string SuppCallBackText { get; set; }

        /// <summary>
        /// 接口商返回状态码
        /// </summary>
        public string SuppErrorCode { get; set; }

        /// <summary>
        /// 接口商返回消息
        /// </summary>
        public string SuppErrorMsg { get; set; }

        /// <summary>
        /// 系统错误日志
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 卡提交异步返回信息
        /// </summary>
        public CardSynchCallBack()
        {
            SupplierId = 0;
            Success = 0;
            SummitStatus = 0;
            SuccAmt = 0M;
            Message = "";
            SuppTransNo = "";
            SuppCallBackText = "";
            OrderStatus = 1;
            SuppErrorCode = "";
            SuppErrorMsg = "";
        }
    }
}
