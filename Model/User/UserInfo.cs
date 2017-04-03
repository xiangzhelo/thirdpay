using System;

namespace viviapi.Model.User
{
    [Serializable]
    public enum NORefCheckStatusEnum
    {
        未知 = 0,
        开启 = 1,
        关闭 = 2
    }

    [Serializable]
    public enum UserFromPartners
    { 
        网站 = 0,
        Tencent = 1,
        Weibo=2
    }

    /// <summary>
    /// 商户类
    /// 2012-02-14
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        private int _id = 0;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _password2 = string.Empty;
        private string _msn = string.Empty;
        private string _qq = string.Empty;
        private string _tel = string.Empty;
        private string _fax = string.Empty;
        private string _email = string.Empty;
        private string _lastloginip = string.Empty;
        private string _lastloginaddress = string.Empty;
        private string _lastloginremark = string.Empty;

        private string _payeebank = string.Empty;
        private string _payeename = string.Empty;
        private string _account;
        private string _bankAddress = string.Empty;
        private string _bankProvince = string.Empty;
        private string _bankCity = string.Empty;
        private string _siteName = string.Empty;
        private string _siteUrl = string.Empty;
        private int _idCardType = 1;
        private string _idCard = string.Empty;
        private int _status = 0;
        private int _cpsdrate = 0;
        private int _cvsnrate = 0;
        private int _integral;
        private int _agentId = 0;
        private int _pmode = 0;
        private int _settles = 1;
        private int _isdebug = 1;
        private DateTime _regtime;
        private DateTime _lastlogintime;
        private decimal _balance;
        private decimal _payment;
        private decimal _unpayment;
        private long _apiaccount = 0;
        private string _apikey = string.Empty;
        private int _maxdayToCashTimes = 0;
        private int _userLevel = 1;
        private UserTypeEnum _userType = UserTypeEnum.会员;

        private string _desc = string.Empty;

        private int _accouttype = 0;
        private string _bankcode = string.Empty;
        private string _provincecode = string.Empty;
        private string _citycode = string.Empty;
        private int _isagentDistribution = 0;
        private int _agentDistscheme = 0;
        private byte _cardversion = 1;
        private byte _RiskWarning = 1;

        private int _parter = 0;
        private string _openid = string.Empty;

        private byte _loginType = 0;

        public UserInfo()
        {
            manageId = 0;
        }

        #region 用户ID
        /// <summary>
        /// 用户ID
        /// </summary>
        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        #endregion

        #region 用户账号
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserName
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }
        #endregion

        #region 证件类型
        /// <summary>
        /// 1-- 身份证
        /// 2-- 护照
        /// 3-- 军官证
        /// 4-- 港澳台居民大陆通行证
        /// </summary>
        public int IdCardType
        {
            get
            {
                return this._idCardType;
            }
            set
            {
                this._idCardType = value;
            }
        }
        #endregion

        #region 用户证件
        /// <summary>
        /// 用户证件 身份证
        /// </summary>
        public string IdCard
        {
            get
            {
                return this._idCard;
            }
            set
            {
                this._idCard = value;
            }
        }
        #endregion

        #region 用户密码
        /// <summary>
        /// 用户密码 MD5加密
        /// </summary>
        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }
        #endregion

        #region 提现密码
        /// <summary>
        /// 提现密码 MD5加密
        /// </summary>
        public string Password2
        {
            get
            {
                return this._password2;
            }
            set
            {
                this._password2 = value;
            }
        }
        #endregion

        #region 归属代理
        /// <summary>
        /// 用户归属于代理ID 不归属代理为0 
        /// </summary>
        public int AgentId
        {
            get
            {
                return this._agentId;
            }
            set
            {
                this._agentId = value;
            }
        }
        #endregion

        #region 扣量
        /// <summary>
        /// 扣量机率 千分比 如设置10 表示1000个单里扣10个单子
        /// </summary>
        public int CPSDrate
        {
            get
            {
                return this._cpsdrate;
            }
            set
            {
                this._cpsdrate = value;
            }
        }
        #endregion

        #region 转率
        /// <summary>
        /// 费率低的转费率高的机率
        /// </summary>
        public int CVSNrate
        {
            get
            {
                return this._cvsnrate;
            }
            set
            {
                this._cvsnrate = value;
            }
        }
        #endregion

        #region 邮件
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }
        #endregion

        #region QQ号码
        /// <summary>
        /// 
        /// </summary>
        public string QQ
        {
            get
            {
                return this._qq;
            }
            set
            {
                this._qq = value;
            }
        }
        #endregion

        #region MSN
        /// <summary>
        /// 
        /// </summary>
        public string msn
        {
            get
            {
                return this._msn;
            }
            set
            {
                this._msn = value;
            }
        }
        #endregion

        #region 联系电话
        /// <summary>
        /// 
        /// </summary>
        public string Tel
        {
            get
            {
                return this._tel;
            }
            set
            {
                this._tel = value;
            }
        }
        #endregion

        #region 传真
        /// <summary>
        /// 
        /// </summary>
        public string fax
        {
            get
            {
                return this._fax;
            }
            set
            {
                this._fax = value;
            }
        }
        #endregion

        #region 积分
        /// <summary>
        /// 
        /// </summary>
        public int Integral
        {
            get
            {
                return this._integral;
            }
            set
            {
                this._integral = value;
            }
        }
        #endregion

        #region 最后登录IP
        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIp
        {
            get
            {
                return this._lastloginip;
            }
            set
            {
                this._lastloginip = value;
            }
        }
        #endregion

        #region 最后登录地点
        /// <summary>
        /// 最后登录地点
        /// </summary>
        public string LastLoginAddress
        {
            get
            {
                return this._lastloginaddress;
            }
            set
            {
                this._lastloginaddress = value;
            }
        }
        #endregion

        #region 最后登录信息
        /// <summary>
        /// 最后登录信息类型
        /// </summary>
        public string LastLoginRemark
        {
            get
            {
                return this._lastloginremark;
            }
            set
            {
                this._lastloginremark = value;
            }
        }
        #endregion

        #region 最后登录日期
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastLoginTime
        {
            get
            {
                return this._lastlogintime;
            }
            set
            {
                this._lastlogintime = value;
            }
        }
        #endregion

        #region 用户等级
        /// <summary>
        /// 
        /// </summary>
        public int UserLevel
        {
            get
            {
                return this._userLevel;
            }
            set
            {
                this._userLevel = value;
            }
        }
        #endregion

        #region 账号余额
        /// <summary>
        /// 账号余额
        /// </summary>
        public decimal Balance
        {
            get
            {
                return this._balance;
            }
            set
            {
                this._balance = value;
            }
        }
        #endregion

        #region 已支付总额
        /// <summary>
        /// 已支付总额
        /// </summary>
        public decimal Payment
        {
            get
            {
                return this._payment;
            }
            set
            {
                this._payment = value;
            }
        }
        #endregion

        #region 已申请未支付总额
        /// <summary>
        /// 已申请未支付总额
        /// </summary>
        public decimal Unpayment
        {
            get
            {
                return this._unpayment;
            }
            set
            {
                this._unpayment = value;
            }
        }
        #endregion

        #region 每天最多可以提现
        /// <summary>
        /// 一天最多可以提现多少次
        /// </summary>
        public int MaxDayToCashTimes
        {
            get
            {
                return this._maxdayToCashTimes;
            }
            set
            {
                this._maxdayToCashTimes = value;
            }
        }
        #endregion

        #region 收款方式
        /// <summary>
        /// 收款方式 
        /// 1:银行帐户 
        /// 2:支付宝
        /// 3:财付通
        /// </summary>
        public int PMode
        {
            get
            {
                return this._pmode;
            }
            set
            {
                this._pmode = value;
            }
        }

        public string PModeName
        {
            get
            {
                string _name = string.Empty;
                switch (PMode)
                {
                    case 1:
                        _name = "银行帐户";
                        break;
                    case 2:
                        _name = "支付宝";
                        break;
                    case 3:
                        _name = "财付通";
                        break;
                }
                return _name;
            }
        }
        #endregion

        #region 结算模式
        /// <summary>
        /// 结算模式         
        /// t0:0
        /// t1：1
        /// </summary>
        public int Settles
        {
            get
            {
                return this._settles;
            }
            set
            {
                this._settles = value;
            }
        }
        #endregion

        #region 开户人姓名
        /// <summary>
        /// 开户人姓名
        /// </summary>
        public string PayeeName
        {
            get
            {
                return this._payeename;
            }
            set
            {
                this._payeename = value;
            }
        }
        #endregion

        #region 收款人帐号
        /// <summary>
        /// 收款人帐号
        /// </summary>
        public string Account
        {
            get
            {
                return this._account;
            }
            set
            {
                this._account = value;
            }
        }
        #endregion

        #region 开户银行
        /// <summary>
        /// 开户银行
        /// </summary>
        public string PayeeBank
        {
            get
            {
                return this._payeebank;
            }
            set
            {
                this._payeebank = value;
            }
        }
        #endregion

        #region 开户银行省份
        /// <summary>
        /// 开户银行省份
        /// </summary>
        public string BankProvince
        {
            get
            {
                return this._bankProvince;
            }
            set
            {
                this._bankProvince = value;
            }
        }
        #endregion

        #region 开户银行
        /// <summary>
        /// 开户银行地址
        /// </summary>
        public string BankCity
        {
            get
            {
                return this._bankCity;
            }
            set
            {
                this._bankCity = value;
            }
        }
        #endregion

        #region 开户银行地址
        /// <summary>
        /// 开户银行地址
        /// </summary>
        public string BankAddress
        {
            get
            {
                return this._bankAddress;
            }
            set
            {
                this._bankAddress = value;
            }
        }
        #endregion

        #region 注册时间
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime
        {
            get
            {
                return this._regtime;
            }
            set
            {
                this._regtime = value;
            }
        }
        #endregion

        #region 用户状态
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }
        #endregion

        #region 用户类别
        /// <summary>
        /// 代理 
        /// 会员
        /// </summary>
        public UserTypeEnum UserType
        {
            get
            {
                return this._userType;
            }
            set
            {
                this._userType = value;
            }
        }
        #endregion

        #region 网站名称
        /// <summary>
        /// 网站名称
        /// </summary>
        public string SiteName
        {
            get
            {
                return this._siteName;
            }
            set
            {
                this._siteName = value;
            }
        }
        #endregion

        #region 网址
        /// <summary>
        /// 
        /// </summary>
        public string SiteUrl
        {
            get
            {
                return this._siteUrl;
            }
            set
            {
                this._siteUrl = value;
            }
        }
        #endregion

        #region API账号
        /// <summary>
        /// API账号
        /// </summary>
        public long APIAccount
        {
            get
            {
                return this._apiaccount;
            }
            set
            {
                this._apiaccount = value;
            }
        }
        #endregion

        #region API密钥
        /// <summary>
        /// API密钥
        /// </summary>
        public string APIKey
        {
            get
            {
                return this._apikey;
            }
            set
            {
                this._apikey = value;
            }
        }
        #endregion

        #region 备注
        /// <summary>
        /// A
        /// </summary>
        public string Desc
        {
            get
            {
                return this._desc;
            }
            set
            {
                this._desc = value;
            }
        }
        #endregion

        #region 可用预额
        private decimal _enableAmt = 0M;

        /// <summary>
        /// 可用余额
        /// </summary>
        public decimal enableAmt
        {
            set { _enableAmt = value; }
            get { return _enableAmt; }
        }
        #endregion

        #region 登录方式
        /// <summary>
        /// 
        /// 0 web
        /// 1 client
        /// 
        /// </summary>
        public byte loginType
        {
            get
            {
                return this._loginType;
            }
            set
            {
                this._loginType = value;
            }
        }
        #endregion

        //所属业务员
        public int manageId { get; set; }

        //是否实名认证
        public int IsRealNamePass { get; set; }

        //是否电话认证
        public int IsPhonePass { get; set; }

        //是否邮件实名认证
        public int IsEmailPass { get; set; }

        //安全问题
        public string question { get; set; }

        //问题答案
        public string answer { get; set; }

        //短信通知地址
        public string smsNotifyUrl { get; set; }

        //无来路判决状态
        public NORefCheckStatusEnum noRefCheckStatus { get; set; }

        private string _full_name = string.Empty;
        private string _male = string.Empty;        
        private string _province = string.Empty;
        private string _city = string.Empty;
        private string _zip = string.Empty;
        private string _addtress = string.Empty;
        private string _field1 = string.Empty;

        //类型ID
        public int classid { get; set; }
        //真实名字
        public string full_name { get { return _full_name; } set { _full_name = value; } }
        //性别
        public string male { get { return _male; } set { _male = value; } }
        public string province { get { return _province; } set { _province = value; } }
        public string city { get { return _city; } set { _city = value; } }
        public string zip { get { return _zip; } set { _zip = value; } }
        public string addtress { get { return _addtress; } set { _addtress = value; } }
        public string field1 { get { return _field1; } set { _field1 = value; } } 
                
        //生日
        public DateTime birthday { get ; set; }

        #region 合作伙伴
        private UserFromPartners _Partners =  UserFromPartners.网站;
        /// <summary>
        /// API账号
        /// </summary>
        public UserFromPartners Partners
        {
            get
            {
                return this._Partners;
            }
            set
            {
                this._Partners = value;
            }
        }
        #endregion

        #region 公司联系人
        private string _linkman = string.Empty;

        /// <summary>
        /// 公司联系人
        /// </summary>
        public string LinkMan
        {
            get
            {
                return this._linkman;
            }
            set
            {
                this._linkman = value;
            }
        }
        #endregion

        #region isdebug
        /// <summary>
        /// 是否记录调试信息
        /// </summary>
        public int isdebug
        {
            get
            {
                return this._isdebug;
            }
            set
            {
                this._isdebug = value;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public string BankCode
        {
            set { _bankcode = value; }
            get { return _bankcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string provinceCode
        {
            set { _provincecode = value; }
            get { return _provincecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cityCode
        {
            set { _citycode = value; }
            get { return _citycode; }
        }

        /// <summary>
        /// 1对公 0对私
        /// </summary>
        public int accoutType
        {
            set { _accouttype = value; }
            get { return _accouttype; }
        }

        #region 允许对私代发
        /// <summary>
        /// 允许对私代发
        /// </summary>
        public int isagentDistribution
        {
            get
            {
                return this._isagentDistribution;
            }
            set
            {
                this._isagentDistribution = value;
            }
        }
        #endregion

        #region 对私代发
        /// <summary>
        /// 对私代发
        /// </summary>
        public int agentDistscheme
        {
            get
            {
                return this._agentDistscheme;
            }
            set
            {
                this._agentDistscheme = value;
            }
        }
        #endregion

        #region 点卡版本
        /// <summary>
        /// 1 普及
        /// 2 专业
        /// </summary>
        public byte cardversion
        {
            get
            {
                return this._cardversion;
            }
            set
            {
                this._cardversion = value;
            }
        }
        #endregion



        /// <summary>
        /// 
        /// </summary>
        public byte RiskWarning
        {
            set { _RiskWarning = value; }
            get { return _RiskWarning; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int parter
        {
            set { _parter = value; }
            get { return _parter; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string openid
        {
            set { _openid = value; }
            get { return _openid; }
        }
    }
}

