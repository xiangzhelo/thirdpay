using System;

namespace viviapi.Model.Finance
{
    /// <summary>
    /// withdraw:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Withdraw
    {
        public Withdraw()
        { }
        #region Model
        private int _id=0;
        private string _tranno = "";
        private WithdrawMode _settmode = WithdrawMode.None;
        private int _userid = 0;
        private decimal _amount = 0M;
        private WithdrawStatus _status = WithdrawStatus.None;
        private int _suppid = 0;
        private int _suppstatus = 0;
        private DateTime _addtime = DateTime.Now;
        private DateTime _required = DateTime.Now.AddDays(1);
        private DateTime _paytime = DateTime.Now;
        private decimal _tax = 0M;
        private decimal _charges = 0M;
        private int _apptype = 1;
        private int _paytype = 1;
        private string _bankcode = "";
        private string _payeebank = "";
        private string _provincecode = "";
        private string _bankprovince = "";
        private string _citycode = "";
        private string _bankcity = "";
        private string _payeename = "";
        private string _payeeaddress = "";
        private int _accouttype = 0;
        private string _account = "";
        private string _batchno = "";

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 处理号
        /// </summary>
        public string Tranno
        {
            set { _tranno = value; }
            get { return _tranno; }
        }

        /// <summary>
        /// 1--用户提现 
        /// 2--系统自动结算
        /// 4--API
        /// 8--上传文件
        /// </summary>
        public WithdrawMode Settmode
        {
            set { _settmode = value; }
            get { return _settmode; }
        }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int Userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public WithdrawStatus Status
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// 接口ID
        /// </summary>
        public int SuppId
        {
            set { _suppid = value; }
            get { return _suppid; }
        }

        /// <summary>
        /// 处理状态
        /// </summary>
        public int Suppstatus
        {
            set { _suppstatus = value; }
            get { return _suppstatus; }
        }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime Addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }

        /// <summary>
        /// 资金必须支付日 
        /// </summary>
        public DateTime Required
        {
            set { _required = value; }
            get { return _required; }
        }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime Paytime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }

        /// <summary>
        /// 税收
        /// </summary>
        public decimal Tax
        {
            set { _tax = value; }
            get { return _tax; }
        }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal Charges
        {
            set { _charges = value; }
            get { return _charges; }
        }

        /// <summary>
        /// 保留字段
        /// </summary>
        public int Apptype
        {
            set { _apptype = value; }
            get { return _apptype; }
        }

        /// <summary>
        /// 收款方式 1 银行帐户 2 支付宝 3 财付通
        /// </summary>
        public int Paytype
        {
            set { _paytype = value; }
            get { return _paytype; }
        }

        /// <summary>
        /// 银行代号
        /// </summary>
        public string BankCode
        {
            set { _bankcode = value; }
            get { return _bankcode; }
        }

        /// <summary>
        /// 银行
        /// </summary>
        public string PayeeBank
        {
            set { _payeebank = value; }
            get { return _payeebank; }
        }

        /// <summary>
        /// 银行所在省代号
        /// </summary>
        public string ProvinceCode
        {
            set { _provincecode = value; }
            get { return _provincecode; }
        }

        /// <summary>
        /// 银行所在省
        /// </summary>
        public string BankProvince
        {
            set { _bankprovince = value; }
            get { return _bankprovince; }
        }

        /// <summary>
        /// 银行所在城市代号
        /// </summary>
        public string CityCode
        {
            set { _citycode = value; }
            get { return _citycode; }
        }

        /// <summary>
        /// 银行所在城市
        /// </summary>
        public string BankCity
        {
            set { _bankcity = value; }
            get { return _bankcity; }
        }

        /// <summary>
        /// 收款人
        /// </summary>
        public string PayeeName
        {
            set { _payeename = value; }
            get { return _payeename; }
        }

        /// <summary>
        /// 分行地址
        /// </summary>
        public string Payeeaddress
        {
            set { _payeeaddress = value; }
            get { return _payeeaddress; }
        }

        /// <summary>
        ///银行账号类型 1对公 0对私
        /// </summary>
        public int AccoutType
        {
            set { _accouttype = value; }
            get { return _accouttype; }
        }

        /// <summary>
        /// 银行账号
        /// </summary>
        public string Account
        {
            set { _account = value; }
            get { return _account; }
        }

        /// <summary>
        /// 批处理号
        /// </summary>
        public string BatchNo
        {
            set { _batchno = value; }
            get { return _batchno; }
        }
        #endregion Model

    }
}

