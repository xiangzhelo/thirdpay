using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviLib.ExceptionHandling;

namespace viviAPI.Gateway2018
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GoToPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string userid = Request.Form["userid"];
                string suppid = Request.Form["suppid"];
                string orderid = Request.Form["orderid"];
                string orderAmt = Request.Form["orderAmt"];
                string bankcode = Request.Form["bankcode"];
                string time = Request.Form["time"];
                string sign = Request.Form["sign"];

                bool success = false;

                #region

                if (!string.IsNullOrEmpty(userid) && !string.IsNullOrEmpty(time))
                {
                    DateTime sumTime;

                    if (DateTime.TryParse(time, out sumTime))
                    {
                        if (DateTime.Now.Subtract(sumTime).TotalSeconds <= 10)
                        {
                            int intUserid = 0;

                            if (int.TryParse(userid, out intUserid))
                            {
                                var userinfo = viviapi.BLL.User.Factory.GetCacheUserBaseInfo(intUserid);

                                if (userinfo != null)
                                {
                                    string thisSign =
                                        string.Format(
                                            "userid={0}&suppid={1}&orderid={2}&orderAmt={3}&bankcode={4}&time={5}{6}"
                                            , userid
                                            , suppid
                                            , orderid
                                            , orderAmt
                                            , bankcode
                                            , time
                                            , userinfo.APIKey);

                                    thisSign = viviLib.Security.Cryptography.MD5(thisSign);

                                    if (thisSign == sign)
                                    {
                                        success = true;
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                if (success == true)
                {
                    string payform = string.Empty;
                    bool isdcode = (bankcode == "1003") || (bankcode == "1004");
                    if (isdcode)
                    {
                        viviapi.Model.User.UserInfo user = viviapi.BLL.User.Factory.GetCacheUserBaseInfo(int.Parse(userid));

                        if (user != null)
                        {
                            payform = viviapi.ETAPI.Common.SellFactory.GetDCodeForm(int.Parse(userid)
                            , user.APIKey
                            , int.Parse(suppid)
                            , orderid
                            , decimal.Parse(orderAmt)
                            , bankcode);
                        }
                        
                    }
                    else
                    {
                        payform = viviapi.ETAPI.Common.SellFactory.GetPayForm(int.Parse(suppid), orderid,
                       decimal.Parse(orderAmt), bankcode, false);
                    }
                    

                    this.litpayform.Text = payform;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                Response.Write("error");
                Response.End();
            }
        }
    }
}
