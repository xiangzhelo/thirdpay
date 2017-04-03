using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Security.Cryptography;
using viviapi.ETAPI.Common;
using viviapi.Model.Payment;
using viviapi.BLL.Payment;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.Model;
using viviLib.Web;
using viviLib.Logging;

////
namespace viviapi.ETAPI
{
    public class OfSUPSupplyResult
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<OfSUPGetOrderdataList> data { get; set; }
        public string orderids { get; set; }
    }

    public class OfSUPGetOrderdataList
    {
        public string reqId { get; set; }
        public string fields { get; set; }
        public string dataList { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class OfSUP : ETAPIBase
    {
        private static int suppId = (int)SupplierCode.OfSUP;

        public OfSUP()
            : base(suppId)
        {

        }
        private string ApiUrl = "http://supply.api.17sup.com/";

        #region Supply
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reqid"></param>
        /// <returns></returns>
        public OfSUPSupplyResult Supply(string reqid)
        {
            OfSUPSupplyResult _result = new OfSUPSupplyResult();
            _result.status = "-1";
            _result.msg = "未知错误";

            try
            {
                string partner = SuppAccount;
                string tplid = SuppUserName;
                string plain = string.Format("{0}{1}{2}", partner, tplid, SuppKey);
                string sign = viviLib.Security.Cryptography.MD5(plain).ToUpper();
                              
                string reqUrl = string.Format("{0}/supply.do?partner={1}&tplid={2}&sign={3}&reqid={4}"
                    , ApiUrl
                    , SuppAccount
                    , SuppUserName
                    , sign
                    , reqid);

                string retXml = viviLib.Web.WebClientHelper.GetString(reqUrl
                    , null
                    , "GET"
                    , System.Text.Encoding.GetEncoding("gb2312"));

                _result = ConversionToSupplyResult(retXml);
               
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }
            return _result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="retXml"></param>
        /// <returns></returns>
        public OfSUPSupplyResult ConversionToSupplyResult(string retXml)
        {
            OfSUPSupplyResult model = new OfSUPSupplyResult();

            if (!string.IsNullOrEmpty(retXml))
            {
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.LoadXml(retXml);

                model.status = xmlDoc.GetElementsByTagName("status")[0].InnerText;
                model.msg = xmlDoc.GetElementsByTagName("msg")[0].InnerText;

                #region dataList
                StringBuilder suporders = new StringBuilder();

                List<OfSUPGetOrderdataList> dataList = new List<OfSUPGetOrderdataList>();
                XmlNodeList xnl = xmlDoc.GetElementsByTagName("data");
                foreach (XmlNode xnf in xnl)
                {
                    OfSUPGetOrderdataList item = new OfSUPGetOrderdataList();
                    foreach (XmlNode xnf1 in xnf.ChildNodes)
                    {
                        if (xnf1.Name == "reqId")
                        {
                            item.reqId = xnf1.InnerText;
                        }
                        else if (xnf1.Name == "fields")
                        {
                            item.fields = xnf1.InnerText;
                        }
                        else if (xnf1.Name == "dataList")
                        {
                            item.dataList = xnf1.OuterXml;

                            if (!string.IsNullOrEmpty(xnf1.OuterXml))
                            {
                                XmlNodeList xnl2 = xmlDoc.GetElementsByTagName(xnf1.OuterXml);
                                foreach (XmlNode xnf2 in xnf.ChildNodes)
                                {
                                    if (xnf2.Name == "order_id")
                                    {
                                        if (suporders.Length == 0)
                                        {
                                            suporders.AppendFormat("{0}", xnf2.InnerText);
                                        }
                                        else
                                        {
                                            suporders.AppendFormat(",{0}", xnf2.InnerText);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    dataList.Add(item);
                }
                model.orderids = suporders.ToString();
                model.data = dataList;
                #endregion

            }
            return model;
        }
        #endregion

        #region CheckOrder
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reqid"></param>
        /// <returns></returns>
        public void CheckOrder(string reqid, string orderids)
        {
        }
        #endregion

    }
}