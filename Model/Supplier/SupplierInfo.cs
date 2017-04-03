using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model
{
    /// <summary>
    /// 供应商类
    /// </summary>
    [Serializable]
    public class SupplierInfo
    {
        #region Model
        private int _id;
        private int? _code;
        private string _name;
        private string _name1 = string.Empty;
        private string _logourl;
        private bool? _isbank;
        private bool? _iscard;
        private bool? _issms;
        private bool? _issx;
        private string _puserid;
        private string _puserkey;
        private string _pusername;
        private string _puserid1;
        private string _puserkey1;
        private string _puserid2;
        private string _puserkey2;
        private string _puserid3;
        private string _puserkey3;
        private string _puserid4;
        private string _puserkey4;
        private string _puserid5;
        private string _puserkey5;
        private string _purl;
        private string _pbakurl;
        private string _pcardbakurl;
        private string _postbankurl;
        private string _postcardurl;
        private string _postsmsurl;
        private bool _isdistribution = false;
        private string _distributionUrl = string.Empty;
        private string _desc;
        private int? _sort;
        private bool? _release;
        private bool? _issys;
        private string _queryCardUrl = string.Empty;
        private int _timeout = 0;

        private bool _synsSummitLog = false;
        private bool _asynsRetLog = false;

        private string _synsRetCode = "";
        private string _asynsRetCode = "";
        private string _jumpUrl;

        private bool _multiacct = false;
        public bool multiacct
        {
            set { _multiacct = value; }
            get { return _multiacct; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public int? code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name1
        {
            set { _name1 = value; }
            get { return _name1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string logourl
        {
            set { _logourl = value; }
            get { return _logourl; }
        }
        /// <summary>
        /// 是否支持网银行
        /// </summary>
        public bool? isbank
        {
            set { _isbank = value; }
            get { return _isbank; }
        }
        /// <summary>
        /// 支持游戏点卡
        /// </summary>
        public bool? iscard
        {
            set { _iscard = value; }
            get { return _iscard; }
        }
        /// <summary>
        /// 支持手机短信
        /// </summary>
        public bool? issms
        {
            set { _issms = value; }
            get { return _issms; }
        }
        /// <summary>
        /// 是否声讯
        /// </summary>
        public bool? issx
        {
            set { _issx = value; }
            get { return _issx; }
        }

        /*是否为代款接口*/
        public bool isdistribution
        {
            set { _isdistribution = value; }
            get { return _isdistribution; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string puserid
        {
            set { _puserid = value; }
            get { return _puserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string puserkey
        {
            set { _puserkey = value; }
            get { return _puserkey; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pusername
        {
            set { _pusername = value; }
            get { return _pusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string puserid1
        {
            set { _puserid1 = value; }
            get { return _puserid1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string puserkey1
        {
            set { _puserkey1 = value; }
            get { return _puserkey1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string puserid2
        {
            set { _puserid2 = value; }
            get { return _puserid2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string puserkey2
        {
            set { _puserkey2 = value; }
            get { return _puserkey2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string puserid3
        {
            set { _puserid3 = value; }
            get { return _puserid3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string puserkey3
        {
            set { _puserkey3 = value; }
            get { return _puserkey3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string puserid4
        {
            set { _puserid4 = value; }
            get { return _puserid4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string puserkey4
        {
            set { _puserkey4 = value; }
            get { return _puserkey4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string puserid5
        {
            set { _puserid5 = value; }
            get { return _puserid5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string puserkey5
        {
            set { _puserkey5 = value; }
            get { return _puserkey5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string purl
        {
            set { _purl = value; }
            get { return _purl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pbakurl
        {
            set { _pbakurl = value; }
            get { return _pbakurl; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string pcardbakurl
        {
            set { _pcardbakurl = value; }
            get { return _pcardbakurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string postBankUrl
        {
            set { _postbankurl = value; }
            get { return _postbankurl; }
        }
        /// <summary>
        /// 中转域名
        /// </summary>
        public string jumpUrl
        {
            set { this._jumpUrl = value; }
            get { return _jumpUrl; }
        }
        private bool _useJump;
        /// <summary>
        /// 启用中转
        /// </summary>
        public bool useJump
        {
            set { this._useJump = value; }
            get { return _useJump; }
        }
        /// <summary>
        /// 代付接口提交地址
        /// </summary>
        public string distributionUrl
        {
            set { this._distributionUrl = value; }
            get { return _distributionUrl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string postCardUrl
        {
            set { _postcardurl = value; }
            get { return _postcardurl; }
        }
        /// <summary>
        /// 结果查询地址
        /// </summary>
        public string queryCardUrl
        {
            set { _queryCardUrl = value; }
            get { return _queryCardUrl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string postSMSUrl
        {
            set { _postsmsurl = value; }
            get { return _postsmsurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string desc
        {
            set { _desc = value; }
            get { return _desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? release
        {
            set { _release = value; }
            get { return _release; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? issys
        {
            set { _issys = value; }
            get { return _issys; }
        }


        /// <summary>
        /// 销卡超时间
        /// </summary>
        public int timeout
        {
            set { _timeout = value; }
            get { return _timeout; }
        }

        /// <summary>
        /// 同步返回码
        /// </summary>
        public string SynsRetCode
        {
            set { _synsRetCode = value; }
            get { return _synsRetCode; }
        }

        /// <summary>
        /// 异步返回码
        /// </summary>
        public string AsynsRetCode
        {
            set { _asynsRetCode = value; }
            get { return _asynsRetCode; }
        }

        /// <summary>
        /// 同步日志
        /// </summary>
        public bool SynsSummitLog
        {
            set { _synsSummitLog = value; }
            get { return _synsSummitLog; }
        }
        /// <summary>
        /// 限制充值金额
        /// </summary>
        public int limitAmount
        { get; set; }

        /// <summary>
        /// 异步日志
        /// </summary>
        public bool AsynsRetLog
        {
            set { _asynsRetLog = value; }
            get { return _asynsRetLog; }
        }
        #endregion Model
    }
}
