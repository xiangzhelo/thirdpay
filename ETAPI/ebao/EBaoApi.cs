using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;

namespace viviapi.ETAPI.ebao
{
    public class EBaoApi : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.YeePay;
        public EBaoApi()
            : base(SuppId)
        {

        }
        /// <summary>
        /// 原生的请求,只针对已经使用易宝支付的用户
        /// </summary>
        /// <param name="nav"></param>
        /// <returns></returns>
        public string Pay(NameValueCollection nav,string httpFlag="1")
        {
            string puserid = this.SuppAccount;
            string puserkey = this.SuppKey;
            string postUrl = "https://www.yeepay.com/app-airsupport/AirSupportService.action";
            #region 参数化
            NameValueCollection nvc = new NameValueCollection();
            for (int i = 0; i < nav.Count; i++)
            {
                if (nav.Get(i) != "hmac")
                {
                    nvc.Add(nav.GetKey(i), nav.Get(i));
                }
            }
            //修改商户号
            nvc.Set("p1_MerId", puserid);
            #endregion
            doRequestHttp request = new doRequestHttp();
            string ret = request.RequestResult("Buy", nvc, puserkey, postUrl,httpFlag);
            return ret;
        }

    }
}
