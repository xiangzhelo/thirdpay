using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents;
using viviLib.Web;

namespace viviAPI.Gateway2018
{
    /// <summary>
    /// 查询流程
    /// </summary>
    public partial class Search : System.Web.UI.Page
    {
        protected string IntegerOrAbc = "^(B|C)[0-9]{19}$";

        #region
        #region 商户ID
        /// <summary>
        /// 商户ID 
        /// </summary>
        public string userid
        {
            get
            {
                return WebBase.GetQueryStringString("parter", "");
            }
        }
        #endregion

        #region 商户订单号
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string orderid
        {
            get
            {
                return WebBase.GetQueryStringString("orderid", "");
            }
        }
        #endregion

        #region MD5签名
        /// <summary>
        /// 签名
        /// </summary>
        public string sign
        {
            get
            {
                return WebBase.GetQueryStringString("sign", "");
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// opstate 3 签名验证失败
        /// opstate 7 数据非法
        /// opstate 8 非法用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string opstate = "";
            string ovalue = "0";
            string apikey = "";

            if (!CheckParm())
            {
                opstate = "7"; //数据非法
            }
            else
            {
                int uid = int.Parse(this.userid);

                var userInfo = viviapi.BLL.User.Factory.GetCacheUserBaseInfo(uid);
                if (userInfo == null || userInfo.Status != 2)
                {
                    opstate = "8";//非法用户
                }
                else
                {
                    apikey = userInfo.APIKey;

                    if (!viviapi.SysInterface.Card.MyAPI.Utility.SeachMD5Check(orderid, userid, apikey, sign))
                    {
                        opstate = "3";
                    }

                    DataRow row = null;
                    int chkresult = 99;

                    bool isbankorder = orderid.StartsWith("B");
                    if (isbankorder)
                    {
                        chkresult = viviapi.BLL.Order.Bank.Factory.Instance.search_check(uid, orderid, out row);
                    }
                    else
                    {
                        chkresult = viviapi.BLL.Order.Bank.Factory.Instance.search_check(uid, orderid, out row);
                    }
                    
                    opstate = "99"; 

                    if (chkresult == 1 || chkresult == 2)
                    {
                        opstate = "8"; //非法用户
                    }
                    else if (chkresult == 3)
                    {
                        opstate = "14"; //不存在该笔订单
                    }
                    else if (chkresult == 99)
                    {
                        opstate = "99"; //系统繁忙
                    }
                    else if (chkresult == 0)
                    {
                        if (row == null)
                        {
                            opstate = "99"; //
                        }
                        else
                        {
                            if (row["orderstatus"] != DBNull.Value)
                            {
                                int orderstatus = Convert.ToInt32(row["orderstatus"]);
                                if (orderstatus == 1)
                                {
                                    opstate = "1";
                                }
                                else if (orderstatus == 2)
                                {
                                    opstate = "0";
                                    ovalue = string.Format("{0:f2}", row["realvalue"]); 
                                }
                                else if (orderstatus == 4)
                                {
                                    opstate = row["opstate"].ToString();
                                }
                            }
                        }
                    }
                }
            }

            var result = new System.Text.StringBuilder();
            result.AppendFormat("orderid={0}", orderid);
            result.AppendFormat("&opstate={0}", opstate);
            result.AppendFormat("&ovalue={0}", ovalue);

            string retsign = result.ToString() + apikey;
            retsign = viviLib.Security.Cryptography.MD5(retsign).ToLower();

            result.AppendFormat("&sign={0}", retsign);

            Response.Write(result.ToString());
        }

        #region CheckParm
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool CheckParm()
        {
            if (string.IsNullOrEmpty(this.userid))
                return false;

            int userId = 0;
            if (!int.TryParse(this.userid, out userId))
                return false;

            if (string.IsNullOrEmpty(this.orderid))
                return false;

            if (!Regex.IsMatch(orderid, IntegerOrAbc))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.sign))
                return false;


            return true;
        }
        #endregion
    }
}
