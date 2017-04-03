using System.Web;
using viviapi.BLL.Supplier;
using viviapi.ETAPI.YeePay.Lib.com.yeepay.cmbn;
using viviapi.Model;
using viviapi.SysConfig;
using viviLib.Logging;

namespace viviapi.ETAPI.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ETAPIBase
    {
        viviapi.BLL.Supplier.SupplierAccount suppAcctBll = new SupplierAccount();

        public SupplierInfo SuppInfo = null;

        public ETAPIBase(int suppcode)
        {
            SuppInfo = Factory.GetCacheModel(suppcode);
        }

        private viviapi.Model.Supplier.SupplierAccount _itemInfo = null;
        public viviapi.Model.Supplier.SupplierAccount AccountInfo
        {
            get
            {
                if (_itemInfo != null) return _itemInfo;
                if (SuppInfo.multiacct == true)
                {
                    //LogWrite("code=>" + SuppInfo.code.Value);
                    //LogWrite("Host=>" + HttpContext.Current.Request.Url.Host);

                    _itemInfo = suppAcctBll.GetCacheModelByDomain(SuppInfo.code.Value, HttpContext.Current.Request.Url.Authority);
                }

                if (_itemInfo == null)
                {
                    _itemInfo = new Model.Supplier.SupplierAccount();
                    _itemInfo.apiAccount = SuppInfo.puserid;
                    _itemInfo.apiKey = SuppInfo.puserkey;
                    _itemInfo.userName = SuppInfo.pusername;
                    _itemInfo.jumpdomain = SuppInfo.desc;

                }
                // _itemInfo = this.ObjId > 0 ? suppAcctBll.GetModel(ObjId) : new viviapi.Model.Supplier.SupplierAccount();

                return _itemInfo;
            }
        }

        public string SuppAccount
        {
            get
            {
                if (AccountInfo == null)
                    return string.Empty;

                // LogWrite("SuppAccount=>" + AccountInfo.apiAccount);
                return AccountInfo.apiAccount;
            }
        }

        public string SuppKey
        {
            get
            {
                if (AccountInfo == null)
                    return string.Empty;

                //LogWrite("SuppKey=>" + AccountInfo.apiKey);

                return AccountInfo.apiKey;
            }
        }

        public string SuppUserName
        {
            get
            {
                if (AccountInfo == null)
                    return string.Empty;

                //LogWrite("userName=>" + AccountInfo.userName);
                return AccountInfo.userName;
            }
        }
        /// <summary>
        /// 在数据库中定义的数据提交地址
        /// </summary>
        public string PostCardUrl
        {
            get
            {
                return SuppInfo.postCardUrl;
            }
        }

        public string PostBankUrl
        {
            get
            {
                return SuppInfo.postBankUrl;
            }
        }

        public string SiteDomain
        {
            get
            {
                string gatewayUrl = string.Empty;
                if (SuppInfo.multiacct == false)
                {
                    gatewayUrl = RuntimeSetting.GatewayUrl;

                    if (string.IsNullOrEmpty(gatewayUrl))
                    {
                        var webinfo = BLL.WebInfoFactory.CurrentWebInfo;

                        if (webinfo != null)
                        {
                            gatewayUrl = webinfo.PayUrl;
                        }
                    }
                }
                else
                {
                    gatewayUrl = Host;

                }

                return gatewayUrl;
            }
        }

        public string Host
        {
            get
            {
                return HttpContext.Current.Request.Url.Scheme + "://" +
                                      HttpContext.Current.Request.Url.Authority;
            }
        }

        /// <summary>
        /// 是否记录同步日志
        /// </summary>
        public bool SynsSummitLog
        {
            get
            {
                return SuppInfo.SynsSummitLog;
            }
        }

        public void SynsSummitLogger(string message)
        {
            if (SynsSummitLog)
            {
                LogWrite(message);
            }
        }

        public void AsynsRetLogger(string message)
        {
            if (SuppInfo.AsynsRetLog)
            {
                LogWrite(message);
            }
        }

        public void LogWrite(string message)
        {
            LogHelper.Info("ETAPILogger", message);
        }
    }
}
