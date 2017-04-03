using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace viviapi.Model.Settled
{
    /// <summary>
    /// 提现方案
    /// </summary>
    [Serializable]
    public class TocashSchemeInfo
    {
        private int _id;
        private string _schemename;
        private decimal _minamtlimitofeach = 100M;
        private decimal _maxamtlimitofeach = 50000M;
        private int _dailymaxtimes = 10;
        private decimal _dailymaxamt = 50000M;
        private decimal _chargerate = 0.01M;
        private decimal _chargeleastofeach = 10M;
        private decimal _chargemostofeach = 50M;
        private int _isdefault;
        private int _vaiInterface = 0;
        private int _bankdetentiondays = 0;
        private int _carddetentiondays = 0;
        private int _otherdetentiondays = 0;
        private byte _tranRequiredAudit = 1;
        private int _type = 1;

        private byte _upperlimit = 1;
        private decimal _upperamt = 50M;
        private byte _lowerlimit = 1;
        private decimal _loweramt=2M;
        

        /// <summary>
        /// 1
        /// 2
        /// </summary>
        public int type
        {
            set { _type = value; }
            get { return _type; }
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
        /// 
        /// </summary>
        public string schemename
        {
            set { _schemename = value; }
            get { return _schemename; }
        }
        /// <summary>
        /// 最低提现金额限制(每笔)
        /// </summary>
        public decimal minamtlimitofeach
        {
            set { _minamtlimitofeach = value; }
            get { return _minamtlimitofeach; }
        }
        /// <summary>
        /// 最大提现金额限制(每笔)
        /// </summary>
        public decimal maxamtlimitofeach
        {
            set { _maxamtlimitofeach = value; }
            get { return _maxamtlimitofeach; }
        }
        /// <summary>
        /// 每天最多可提现次数
        /// </summary>
        public int dailymaxtimes
        {
            set { _dailymaxtimes = value; }
            get { return _dailymaxtimes; }
        }
        /// <summary>
        /// 每天最多可限额
        /// </summary>
        public decimal dailymaxamt
        {
            set { _dailymaxamt = value; }
            get { return _dailymaxamt; }
        }
        /// <summary>
        /// 提现手续费
        /// </summary>
        public decimal chargerate
        {
            set { _chargerate = value; }
            get { return _chargerate; }
        }
        /// <summary>
        /// 提现手续费最少每笔
        /// </summary>
        public decimal chargeleastofeach
        {
            set { _chargeleastofeach = value; }
            get { return _chargeleastofeach; }
        }
        /// <summary>
        /// 提现手续费最高每笔
        /// </summary>
        public decimal chargemostofeach
        {
            set { _chargemostofeach = value; }
            get { return _chargemostofeach; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int isdefault
        {
            set { _isdefault = value; }
            get { return _isdefault; }
        }

        /// <summary>
        /// 是否需要走接口
        /// 1 走接口
        /// 0 不走接口
        /// </summary>
        public int vaiInterface
        {
            set { _vaiInterface = value; }
            get { return _vaiInterface; }
        }

        /// <summary>
        /// 网银收入押后几天
        /// </summary>
        public int bankdetentiondays
        {
            set { _bankdetentiondays = value; }
            get { return _bankdetentiondays; }
        }

        /// <summary>
        ///  点卡收入押后几天
        /// </summary>
        public int carddetentiondays
        {
            set { _carddetentiondays = value; }
            get { return _carddetentiondays; }
        }

        /// <summary>
        /// 其它收入押后几天
        /// </summary>
        public int otherdetentiondays
        {
            set { _otherdetentiondays = value; }
            get { return _otherdetentiondays; }
        }  
 
        /// <summary>
        /// 走转换 是否需要审核
        /// 0 不需要审核
        /// 1 需要审核
        /// </summary>
        public byte tranRequiredAudit
        {
            set { _tranRequiredAudit = value; }
            get { return _tranRequiredAudit; }
        }

        /// <summary>
        /// 手继费是否封顶
        /// </summary>
        public byte upperLimit
        {
            set { _upperlimit = value; }
            get { return _upperlimit; }
        }
        /// <summary>
        /// 封顶金额
        /// </summary>
        public decimal upperAmt
        {
            set { _upperamt = value; }
            get { return _upperamt; }
        }
        /// <summary>
        /// 是否最低限制
        /// </summary>
        public byte lowerLimit
        {
            set { _lowerlimit = value; }
            get { return _lowerlimit; }
        }
        /// <summary>
        /// 最低限制金额
        /// </summary>
        public decimal lowerAmt
        {
            set { _loweramt = value; }
            get { return _loweramt; }
        }
    }
}
