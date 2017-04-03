using System;

namespace viviapi.Model.Finance
{
    /// <summary>
    /// payrate:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class PayRate
    {
        public PayRate()
        { }

        #region Model
        private int _id=0;
        private int _ratetype = 1;
        private int _billid=0;
        private string _billame = "";
        private decimal _p100 = 0M;
        private decimal _p101 = 0M;
        private decimal _p102 = 0M;
        private decimal _p103 = 0M;
        private decimal _p104 = 0M;
        private decimal _p105 = 0M;
        private decimal _p106 = 0M;
        private decimal _p107 = 0M;
        private decimal _p108 = 0M;
        private decimal _p109 = 0M;
        private decimal _p110 = 0M;
        private decimal _p111 = 0M;
        private decimal _p112 = 0M;
        private decimal _p113 = 0M;
        private decimal _p114 = 0M;
        private decimal _p115 = 0M;
        private decimal _p116 = 0M;
        private decimal _p117 = 0M;
        private decimal _p118 = 0M;
        private decimal _p119 = 0M;
        private decimal _p300 = 0M;
        private decimal _p200 = 0M;
        private decimal _p201 = 0M;
        private decimal _p202 = 0M;
        private decimal _p203 = 0M;
        private decimal _p204 = 0M;
        private decimal _p205 = 0M;
        private decimal _p207 = 0M;
        private decimal _p208 = 0M;
        private decimal _p209 = 0M;
        private decimal _p210 = 0M;
        private decimal _p206 = 0M;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 1 商户级别
        /// 2 商户
        /// </summary>
        public int rateType
        {
            set { _ratetype = value; }
            get { return _ratetype; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int billId
        {
            set { _billid = value; }
            get { return _billid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string billame
        {
            set { _billame = value; }
            get { return _billame; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p100
        {
            set { _p100 = value; }
            get { return _p100; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p101
        {
            set { _p101 = value; }
            get { return _p101; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p102
        {
            set { _p102 = value; }
            get { return _p102; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p103
        {
            set { _p103 = value; }
            get { return _p103; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p104
        {
            set { _p104 = value; }
            get { return _p104; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p105
        {
            set { _p105 = value; }
            get { return _p105; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p106
        {
            set { _p106 = value; }
            get { return _p106; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p107
        {
            set { _p107 = value; }
            get { return _p107; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p108
        {
            set { _p108 = value; }
            get { return _p108; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p109
        {
            set { _p109 = value; }
            get { return _p109; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p110
        {
            set { _p110 = value; }
            get { return _p110; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p111
        {
            set { _p111 = value; }
            get { return _p111; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p112
        {
            set { _p112 = value; }
            get { return _p112; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p113
        {
            set { _p113 = value; }
            get { return _p113; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p114
        {
            set { _p114 = value; }
            get { return _p114; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p115
        {
            set { _p115 = value; }
            get { return _p115; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p116
        {
            set { _p116 = value; }
            get { return _p116; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p117
        {
            set { _p117 = value; }
            get { return _p117; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p118
        {
            set { _p118 = value; }
            get { return _p118; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p119
        {
            set { _p119 = value; }
            get { return _p119; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p300
        {
            set { _p300 = value; }
            get { return _p300; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p200
        {
            set { _p200 = value; }
            get { return _p200; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p201
        {
            set { _p201 = value; }
            get { return _p201; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p202
        {
            set { _p202 = value; }
            get { return _p202; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p203
        {
            set { _p203 = value; }
            get { return _p203; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p204
        {
            set { _p204 = value; }
            get { return _p204; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p205
        {
            set { _p205 = value; }
            get { return _p205; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p207
        {
            set { _p207 = value; }
            get { return _p207; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p208
        {
            set { _p208 = value; }
            get { return _p208; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p209
        {
            set { _p209 = value; }
            get { return _p209; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p210
        {
            set { _p210 = value; }
            get { return _p210; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal p206
        {
            set { _p206 = value; }
            get { return _p206; }
        }
        #endregion Model

    }
}

