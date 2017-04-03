using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserSettingInfo
    {
        private int _userid;
        private int _defaultpay =103;
        private int _special = 0;
        private int _istransfer = 0;
        private int _payrate = 0;
        private byte _isRequireAgentDistAudit = 0;
        private byte _riskWarning = 0;
        private byte _alipayRiskWarning = 0;
        private byte _aliCodeRiskWarning = 0;
        private byte _wxPayRiskWarning = 0;

        public byte AlipayRiskWarning
        {
            set { _alipayRiskWarning = value; }
            get { return _alipayRiskWarning; }
        }

        public byte AliCodeRiskWarning
        {
            set { _aliCodeRiskWarning = value; }
            get { return _aliCodeRiskWarning; }
        }

        public byte WxPayRiskWarning
        {
            set { _wxPayRiskWarning = value; }
            get { return _wxPayRiskWarning; }
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
        /// 开启转账
        /// </summary>
        public int istransfer
        {
            set { _istransfer = value; }
            get { return _istransfer; }
        }

        /// <summary>
        /// 单独配置比率
        /// </summary>
        public int special
        {
            set { _special = value; }
            get { return _special; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int defaultpay
        {
            set { _defaultpay = value; }
            get { return _defaultpay; }
        }

        /// <summary>
        /// 结算比率
        /// </summary>
        public int payrate
        {
            set { _payrate = value; }
            get { return _payrate; }
        }

        /// <summary>
        /// 通过接口付款是否需要 审核
        /// </summary>
        public byte isRequireAgentDistAudit
        {
            get { return _isRequireAgentDistAudit; }
            set { _isRequireAgentDistAudit = value; }
            
        }



        /// <summary>
        /// 
        /// </summary>
        public byte RiskWarning
        {
            set { _riskWarning = value; }
            get { return _riskWarning; }
        }
    }
}
