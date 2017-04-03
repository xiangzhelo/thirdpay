using System;

namespace viviapi.Model
{
    /// <summary>
    /// PTypeConf:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class PTypeConf
    {
        public PTypeConf()
        { }
        #region Model
        private int _id;
        private int _goodtype;
        private int _gm_id;
        private int _paytype;
        private int _isuse;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GoodType
        {
            set { _goodtype = value; }
            get { return _goodtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GM_ID
        {
            set { _gm_id = value; }
            get { return _gm_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsUse
        {
            set { _isuse = value; }
            get { return _isuse; }
        }

        private bool _payalipay =true;
        /// <summary>
        /// 支付宝
        /// </summary>
        public bool PayAlipay
        {
            set { _payalipay = value; }
            get { return _payalipay; }
        }

        private bool _payTanPay = true;
        /// <summary>
        /// 财富通
        /// </summary>
        public bool PayTanPay
        {
            set { _payTanPay = value; }
            get { return _payTanPay; }
        }

        private bool _payBank = true;
        /// <summary>
        /// 网银
        /// </summary>
        public bool PayBank
        {
            set { _payBank = value; }
            get { return _payBank; }
        }


        private bool _pay103 = true;
        /// <summary>
        /// 
        /// </summary>
        public bool Pay103
        {
            set { _pay103 = value; }
            get { return _pay103; }
        }

        private bool _pay104 = true;
        /// <summary>
        /// 
        /// </summary>
        public bool Pay104
        {
            set { _pay104 = value; }
            get { return _pay104; }
        }

        private bool _pay105 = true;
        /// <summary>
        /// 
        /// </summary>
        public bool Pay105
        {
            set { _pay105 = value; }
            get { return _pay105; }
        }

        private bool _pay106 = true;
        /// <summary>
        /// 
        /// </summary>
        public bool Pay106
        {
            set { _pay106 = value; }
            get { return _pay106; }
        }

        private bool _pay107 = true;
        /// <summary>
        /// 
        /// </summary>
        public bool Pay107
        {
            set { _pay107 = value; }
            get { return _pay107; }
        }

        private bool _pay108 = true;
        /// <summary>
        /// 
        /// </summary>
        public bool Pay108
        {
            set { _pay108 = value; }
            get { return _pay108; }
        }

        private bool _pay109 = true;
        /// <summary>
        /// 
        /// </summary>
        public bool Pay109
        {
            set { _pay109 = value; }
            get { return _pay109; }
        }

        private bool _pay110 = true;
        /// <summary>
        /// 
        /// </summary>
        public bool Pay110
        {
            set { _pay110 = value; }
            get { return _pay110; }
        }

        private bool _pay111= true;
        /// <summary>
        /// 
        /// </summary>
        public bool Pay111
        {
            set { _pay111 = value; }
            get { return _pay111; }
        }

        private bool _pay112 = true;
        /// <summary>
        /// 
        /// </summary>
        public bool Pay112
        {
            set { _pay112 = value; }
            get { return _pay112; }
        }

        private bool _pay113 = true;
        /// <summary>
        /// 
        /// </summary>
        public bool Pay113
        {
            set { _pay113 = value; }
            get { return _pay113; }
        }


        #endregion Model

    }
}

