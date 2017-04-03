using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.Order
{
    [Serializable]
    public class CheckCardResult
    {
        /// <summary>
        /// 1 未重复
        /// 2
        /// 3
        /// 4 卡正在处理中
        /// 5
        /// 6 商务订单重复
        /// </summary>
        public byte isRepeat { get; set; }

        /// <summary>
        /// 是否关闭
        /// 0 未关闭
        /// 1 已关闭
        /// </summary>
        public byte isclose { get; set; }

        /// <summary>
        /// 是否为补单
        /// 0否 
        /// 1为补单
        /// </summary>
        public byte makeup { get; set; }

        /// <summary>
        /// 原处理接口
        /// </summary>
        public int supplierid { get; set; }

        /// <summary>
        /// 接口商费率
        /// </summary>
        public decimal supprate { get; set; }
       
        /// <summary>
        /// 卡余额
        /// </summary>
        public decimal withhold { get; set; }

        


        /// <summary>
        /// 卡真实卡密
        /// </summary>
        public string cardpwd { get; set; }
    }
}
