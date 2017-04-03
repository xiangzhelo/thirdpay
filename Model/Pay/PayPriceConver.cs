using System;

namespace viviapi.Model.Payment
{
    /// <summary>
    /// PayPriceConver:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class PayPriceConver
    {
        public PayPriceConver()
        { }
        #region Model
        private int _id;
        private int _pri_type;
        private decimal _value;
        private int _conv_pritype = 0;
        private DateTime? _created;
        private bool _IsOpen;

        public bool IsOpen
        {
            set { _IsOpen = value; }
            get { return _IsOpen; }
        }

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
        public int Pri_Type
        {
            set { _pri_type = value; }
            get { return _pri_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Conv_PriType
        {
            set { _conv_pritype = value; }
            get { return _conv_pritype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Created
        {
            set { _created = value; }
            get { return _created; }
        }
        #endregion Model

    }
}

