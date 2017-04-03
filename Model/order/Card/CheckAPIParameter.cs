using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Order.Card
{
    /// <summary>
    /// 检验点卡结果
    /// </summary>
    [Serializable]
    public class CheckAPIParameter
    {
        /// <summary>
        /// 1 未重复
        /// 2
        /// 3
        /// 4 卡正在处理中
        /// 5
        /// 6 商务订单重复
        /// </summary>
        public byte IsRepeat { get; set; }

        /// <summary>
        /// 是否关闭
        /// 0 未关闭
        /// 1 已关闭
        /// </summary>
        public byte Isclose { get; set; }

        /// <summary>
        /// 是否为补单
        /// 0否 
        /// 1为补单
        /// </summary>
        public byte Makeup { get; set; }

        /// <summary>
        /// 原处理接口
        /// </summary>
        public int Supplierid { get; set; }

        /// <summary>
        /// 接口商费率
        /// </summary>
        public decimal Supprate { get; set; }
       
        /// <summary>
        /// 卡余额
        /// </summary>
        public decimal CardBalance { get; set; }

        /// <summary>
        /// 卡真实卡密
        /// </summary>
        public string Cardpwd { get; set; }
    }
}
