using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.Order
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class OrderBankInfo : OrderBase
    {
        /// <summary>
        /// 
        /// </summary>
        public OrderBankInfo()
        {
            /*�����������㷨 ����Ҫ����*/
            //byte[] buffer = Guid.NewGuid().ToByteArray();
            //long long_guid = BitConverter.ToInt64(buffer, 0);
            //this.orderid = long_guid.ToString();
        }
    }
}
