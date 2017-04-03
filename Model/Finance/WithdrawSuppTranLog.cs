using System;

namespace viviapi.Model.Finance
{
    /// <summary>
    /// withdrawSuppTranLog:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class WithdrawSuppTranLog
    {
        public WithdrawSuppTranLog()
        { }

        #region Model
        private int _id;
        private int _suppid = 1;
        private int? _mode = 1;
        private int? _settledid = 1;
        private string _withdrawNo = "";
        private string _trade_no = "";
        private int _batchno = 1;
        private string _supp_trade_no = "";
        private int _userid = 0;
        private decimal _balance = 0M;
        private string _bankcode = "";
        private string _bankname = "";
        private string _bankbranch = "";
        private string _bankaccountname = "";
        private string _bankaccount = "";
        private decimal _amount = 0M;
        private decimal _charges = 0M;
        private decimal? _balance2 = 0M;
        private DateTime _addtime = DateTime.Now;
        private DateTime _processingtime = DateTime.Now;
        private string _supp_message = "";
        private bool _is_cancel = false;
        private int _status = 1;
        private string _ext1;
        private string _ext2;
        private string _ext3;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 接口商
        /// </summary>
        public int suppid
        {
            set { _suppid = value; }
            get { return _suppid; }
        }
        /// <summary>
        /// 交易模式 
        /// </summary>
        public int? mode
        {
            set { _mode = value; }
            get { return _mode; }
        }
        /// <summary>
        /// 结算ID
        /// </summary>
        public int? settledId
        {
            set { _settledid = value; }
            get { return _settledid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string withdrawNo
        {
            set { _withdrawNo = value; }
            get { return _withdrawNo; }
        }
        /// <summary>
        /// 处理单号
        /// </summary>
        public string trade_no
        {
            set { _trade_no = value; }
            get { return _trade_no; }
        }
        /// <summary>
        /// 批号
        /// </summary>
        public int batchNo
        {
            set { _batchno = value; }
            get { return _batchno; }
        }
        /// <summary>
        /// 接口商处理ID
        /// </summary>
        public string supp_trade_no
        {
            set { _supp_trade_no = value; }
            get { return _supp_trade_no; }
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public int userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 处理之前余额
        /// </summary>
        public decimal balance
        {
            set { _balance = value; }
            get { return _balance; }
        }
        /// <summary>
        /// 银行代号
        /// </summary>
        public string bankCode
        {
            set { _bankcode = value; }
            get { return _bankcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bankName
        {
            set { _bankname = value; }
            get { return _bankname; }
        }
        /// <summary>
        /// 分行名称
        /// </summary>
        public string bankBranch
        {
            set { _bankbranch = value; }
            get { return _bankbranch; }
        }
        /// <summary>
        /// 开户姓名
        /// </summary>
        public string bankAccountName
        {
            set { _bankaccountname = value; }
            get { return _bankaccountname; }
        }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string bankAccount
        {
            set { _bankaccount = value; }
            get { return _bankaccount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 手续费用
        /// </summary>
        public decimal charges
        {
            set { _charges = value; }
            get { return _charges; }
        }
        /// <summary>
        /// 处理之后余额
        /// </summary>
        public decimal? balance2
        {
            set { _balance2 = value; }
            get { return _balance2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime addTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime processingTime
        {
            set { _processingtime = value; }
            get { return _processingtime; }
        }
        /// <summary>
        /// 接口商返回消息
        /// </summary>
        public string supp_message
        {
            set { _supp_message = value; }
            get { return _supp_message; }
        }
        /// <summary>
        /// 取消
        /// </summary>
        public bool is_cancel
        {
            set { _is_cancel = value; }
            get { return _is_cancel; }
        }
        /// <summary>
        /// 状态 1 处理中
        /// 2 处理成功
        /// 4 处理失败
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ext1
        {
            set { _ext1 = value; }
            get { return _ext1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ext2
        {
            set { _ext2 = value; }
            get { return _ext2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ext3
        {
            set { _ext3 = value; }
            get { return _ext3; }
        }
        /// <summary>
        /// 说明
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}

