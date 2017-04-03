using System;


namespace viviapi.Model.Payment
{
    /// <summary>
    /// OrderConvern:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class OrderConvern
    {
        public OrderConvern()
        { }
        #region Model
        private int _id;
        private ulong _okxrorderid;
        private string _origoutorderid;
        private int _origpaytype;
        private decimal _origpayprice;
        private decimal _origpromoney;
        private decimal _origagmoney;
        private decimal _origprofit;
        private decimal _amount;
        private DateTime _created;
        private int _convtpaytype;
        private string _convtoutorderid;
        private decimal _convtpayprice;
        private decimal _convtagmoney;
        private decimal _convtpromoney;
        private decimal _convtprofit;
        private decimal? _diffprofit;
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
        public ulong OkxrOrderId
        {
            set { _okxrorderid = value; }
            get { return _okxrorderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrigOutOrderId
        {
            set { _origoutorderid = value; }
            get { return _origoutorderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OrigPayType
        {
            set { _origpaytype = value; }
            get { return _origpaytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OrigPayPrice
        {
            set { _origpayprice = value; }
            get { return _origpayprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OrigPromoney
        {
            set { _origpromoney = value; }
            get { return _origpromoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OrigAgmoney
        {
            set { _origagmoney = value; }
            get { return _origagmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OrigProfit
        {
            set { _origprofit = value; }
            get { return _origprofit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Created
        {
            set { _created = value; }
            get { return _created; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ConvtPayType
        {
            set { _convtpaytype = value; }
            get { return _convtpaytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ConvtOutOrderId
        {
            set { _convtoutorderid = value; }
            get { return _convtoutorderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ConvtPayPrice
        {
            set { _convtpayprice = value; }
            get { return _convtpayprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ConvtAgmoney
        {
            set { _convtagmoney = value; }
            get { return _convtagmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ConvtPromoney
        {
            set { _convtpromoney = value; }
            get { return _convtpromoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ConvtProfit
        {
            set { _convtprofit = value; }
            get { return _convtprofit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DiffProfit
        {
            set { _diffprofit = value; }
            get { return _diffprofit; }
        }
        #endregion Model

    }
}

