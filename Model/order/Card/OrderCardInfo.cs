using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.Order
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable] 
    public class OrderCardInfo : OrderBase
    {
        private string _tranNo;

        public string TranNo
        {
            set { _tranNo = value; }
            get { return _tranNo; }
        }

        public OrderCardInfo()
        {
            //byte[] buffer = Guid.NewGuid().ToByteArray();
            //long long_guid = BitConverter.ToInt64(buffer, 0);
            //this.orderid = long_guid.ToString();
            //Random random = new Random(Guid.NewGuid().GetHashCode());
            //this.orderid = DateTime.Now.ToString("yyMMddHHmmssff") + "0" + random.Next(1000).ToString("0000");
            makeup = 0;
        }
                
        /// <summary>
        /// YYMMDDHHMMSSFF
        /// </summary>
        public OrderCardInfo(string serverId, string userId, string chanel)
        {
            /*订单号生成算法 还需要讨论
            byte[] buffer = Guid.NewGuid().ToByteArray();
            long long_guid = BitConverter.ToInt64(buffer, 0);
            this.orderid = long_guid.ToString();*/

            Random random = new Random(GetRandomSeed(serverId + userId + chanel));
            this.orderid = DateTime.Now.ToString("yyMMddHHmmssff") + "0" + random.Next(1000).ToString("0000");
        }

        int GetRandomSeed(string factor)
        {
            byte[] bytes = System.Text.UTF8Encoding.UTF8.GetBytes(factor);
            
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }
        
        //卡的种类
        public int cardType { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// 卡密
        /// </summary>
        public string cardPwd { get; set; }        
        //结果
        public string resultcode { get; set; }
        //卡张数
        public int cardnum { get; set; }
        /// <summary>
        /// 是否以多卡方式提交
        /// </summary>

        public int ismulticard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ovalue { get; set; }

        public string Desc { get; set; }



        //处理方式
        //1 接口返回处理
        //2 系统自动处理
        //用于大提小，小提大 
        public byte method { get; set; }


        private decimal _faceValue = 0M;
        /// <summary>
        /// 卡面值
        /// </summary>
        public decimal faceValue
        {
            set { _faceValue = value; }
            get { return _faceValue; }
        }

        private byte _cardversion = 1;
        /// <summary>
        /// 结算方式
        /// 1普及
        /// 2专业
        /// </summary>
        public byte cardversion
        {
            set { _cardversion = value; }
            get { return _cardversion; }
        }

        /// <summary>
        /// 2 小提大
        /// </summary>
        public byte withhold_type { get; set; }
        /// <summary>
        /// 吃卡面值
        /// </summary>
        public decimal withholdAmt { get; set; }


        /// <summary>
        /// 是否是补充
        /// 
        /// </summary>
        public byte makeup { get; set; }
        public string userViewMsg { get; set; }

        public string returnopstate
        {
            get
            {
                if (string.IsNullOrEmpty(opstate))
                    return "opstate=-1";

                System.Text.StringBuilder _returnop = new StringBuilder();
                string[] arr1 = opstate.Split('|');
                for (int i = 0; i < arr1.Length; i++)
                {
                    string[] arr = arr1[i].Split(':');
                    if (arr.Length == 2)
                    {
                        if (_returnop.Length == 0)
                        {
                            _returnop.AppendFormat("opstate={0}", arr[1]);
                        }
                        else
                        {
                            _returnop.AppendFormat(",opstate={0}", arr[1]);
                        }
                    }
                }
                return _returnop.ToString();
            }
        }

        public string returnovalue
        {
            get
            {
                if (string.IsNullOrEmpty(opstate))
                    return string.Empty;

                if (ovalue.EndsWith(","))
                    return ovalue.Substring(0, ovalue.Length - 1);
                return ovalue;
            }
        }

        private string _batno="";
        /// <summary>
        /// 批号，多卡时关联总表
        /// </summary>
        public string Batno
        {
            set { _batno = value; }
            get { return _batno; }
        }
    }
}
