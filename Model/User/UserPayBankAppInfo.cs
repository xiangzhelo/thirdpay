using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.User
{
    public enum AcctChangeEnum
    {
        //待审核  Auditing
        待审核 = 1,
        //正常 Normal
        审核成功 = 2,
        //锁定 Locked
        审核失败 = 4
    }
    /// <summary>
    /// 支付账号变更类
    /// </summary>
    [Serializable]
    public class UserPayBankAppInfo
    {
        private int _id;
        private int _userid;
        private int _pmode = 1;
        private string _account = string.Empty;
        private string _payeename = string.Empty;
        private string _payeebank = string.Empty;
        private string _bankprovince = string.Empty;
        private string _bankcity = string.Empty;
        private string _bankaddress = string.Empty;
        private AcctChangeEnum _status = AcctChangeEnum.待审核;
        private DateTime? _addtime = DateTime.Now;
        private DateTime? _suretime = DateTime.Now;
        private int? _sureuser = 0;

        private int _accouttype = 0;
        private string _bankcode = string.Empty;
        private string _provincecode = string.Empty;
        private string _citycode = string.Empty;
        private string _reason = string.Empty;


        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        
        /// <summary>
        /// 1对公 0对私
        /// </summary>
        public int accoutType
        {
            set { _accouttype = value; }
            get { return _accouttype; }
        }

        /// <summary>
        /// 收款方式 1 银行帐户 2 支付宝 3 财付通
        /// </summary>
        public int pmode
        {
            set { _pmode = value; }
            get { return _pmode; }
        }
        public string pmodeName
        {
            get
            {
                string _name = string.Empty;
                switch (pmode)
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
        /// <summary>
        /// 
        /// </summary>
        public string account
        {
            set { _account = value; }
            get { return _account; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string payeeName
        {
            set { _payeename = value; }
            get { return _payeename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string payeeBank
        {
            set { _payeebank = value; }
            get { return _payeebank; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bankProvince
        {
            set { _bankprovince = value; }
            get { return _bankprovince; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string bankCity
        {
            set { _bankcity = value; }
            get { return _bankcity; }
        }

        /// <summary>
        /// 银行地址
        /// </summary>
        public string bankAddress
        {
            set { _bankaddress = value; }
            get { return _bankaddress; }
        }
        /// <summary>
        /// 状态
        /// 1 未审核
        /// 2 审核成功
        /// 4 审核失败
        /// </summary>
        public AcctChangeEnum status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? SureTime
        {
            set { _suretime = value; }
            get { return _suretime; }
        }
        /// <summary>
        /// 审核人
        /// </summary>
        public int? SureUser
        {
            set { _sureuser = value; }
            get { return _sureuser; }
        }
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
        /// 
        /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
    }
}
