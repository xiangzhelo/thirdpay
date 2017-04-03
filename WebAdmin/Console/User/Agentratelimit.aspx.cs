using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using viviLib.Web;
using viviapi.Model;
using viviapi.BLL;
using viviLib.Text;

namespace viviapi.web.Manage.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class agentratelimit :  viviapi.WebComponents.Web.ManagePageBase
    {

        /// <summary>
        /// 
        /// </summary>
        public int ItemInfoId
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("id", 0);
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.ManageFactory.CheckSecondPwd();
            setPower();
            if (!this.IsPostBack)
            {
                ShowInfo();
            }
        }

        #region ShowInfo
        void ShowInfo()
        {
            string config = WebInfoFactory.GetAgent_Payrate_Setconfig();
            if (!string.IsNullOrEmpty(config))
            {
                string[] arr = config.Split('|');
                foreach (string item in arr)
                {
                    string[] arr1 = item.Split(':');
                    if (arr1[0] == "100")
                    {
                        this.txtp100.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1001.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "101")
                    {
                        this.txtp101.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1011.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "102")
                    {
                        this.txtp102.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1021.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");

                    }
                    else if (arr1[0] == "103")
                    {
                        this.txtp103.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1031.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "104")
                    {
                        this.txtp104.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1041.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "105")
                    {
                        this.txtp105.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1051.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "106")
                    {
                        this.txtp106.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1061.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "107")
                    {
                        this.txtp107.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1071.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "108")
                    {
                        this.txtp108.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1081.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "109")
                    {
                        this.txtp109.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1091.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "110")
                    {
                        this.txtp110.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1101.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "111")
                    {
                        this.txtp111.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1111.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "112")
                    {
                        this.txtp112.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1121.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "113")
                    {
                        this.txtp113.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1131.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "114")
                    {
                        this.txtp114.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1141.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "115")
                    {
                        this.txtp115.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1151.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "116")
                    {
                        this.txtp116.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1161.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "117")
                    {
                        this.txtp117.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1171.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "118")
                    {
                        this.txtp118.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1181.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "119")
                    {
                        this.txtp119.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp1191.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "200")
                    {
                        this.txtp200.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp2001.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "201")
                    {
                        this.txtp201.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp2011.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "202")
                    {
                        this.txtp202.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp2021.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "203")
                    {
                        this.txtp203.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp2031.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "204")
                    {
                        this.txtp204.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp2041.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "205")
                    {
                        this.txtp205.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp2051.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "208")
                    {
                        this.txtp208.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp2081.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "209")
                    {
                        this.txtp209.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp2091.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                    else if (arr1[0] == "300")
                    {
                        this.txtp300.Text = (Convert.ToDecimal(arr1[1]) * 100).ToString("0.00");
                        this.txtp3001.Text = (Convert.ToDecimal(arr1[2]) * 100).ToString("0.00");
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region check value
            string strErr = string.Empty;
            if (!viviLib.Text.Validate.IsNumber(txtp100.Text))
            {
                strErr += "p100格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp101.Text))
            {
                strErr += "p101格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp102.Text))
            {
                strErr += "p102格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp103.Text))
            {
                strErr += "p103格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp104.Text))
            {
                strErr += "p104格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp105.Text))
            {
                strErr += "p105格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp106.Text))
            {
                strErr += "p106格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp107.Text))
            {
                strErr += "p107格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp108.Text))
            {
                strErr += "p108格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp109.Text))
            {
                strErr += "p109格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp110.Text))
            {
                strErr += "p110格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp111.Text))
            {
                strErr += "p111格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp112.Text))
            {
                strErr += "p112格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp113.Text))
            {
                strErr += "p113格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp114.Text))
            {
                strErr += "p114格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp115.Text))
            {
                strErr += "p115格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp116.Text))
            {
                strErr += "p116格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp117.Text))
            {
                strErr += "p117格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp118.Text))
            {
                strErr += "p118格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp119.Text))
            {
                strErr += "p119格式错误！\n";
            }

            if (!viviLib.Text.Validate.IsNumber(txtp200.Text))
            {
                strErr += "p200格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp201.Text))
            {
                strErr += "p201格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp202.Text))
            {
                strErr += "p202格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp203.Text))
            {
                strErr += "p203格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp204.Text))
            {
                strErr += "p204格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp205.Text))
            {
                strErr += "p205格式错误！\n";
            }

            if (!viviLib.Text.Validate.IsNumber(txtp208.Text))
            {
                strErr += "p208格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp209.Text))
            {
                strErr += "p209格式错误！\n";
            }

            if (!viviLib.Text.Validate.IsNumber(txtp300.Text))
            {
                strErr += "p300格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1001.Text))
            {
                strErr += "p1001格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1011.Text))
            {
                strErr += "p1011格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1021.Text))
            {
                strErr += "p1021格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1031.Text))
            {
                strErr += "p1031格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1041.Text))
            {
                strErr += "p1041格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1051.Text))
            {
                strErr += "p1051格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1061.Text))
            {
                strErr += "p1061格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1071.Text))
            {
                strErr += "p1071格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1081.Text))
            {
                strErr += "p1081格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1091.Text))
            {
                strErr += "p1091格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1101.Text))
            {
                strErr += "p1101格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1111.Text))
            {
                strErr += "p1111格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1121.Text))
            {
                strErr += "p1121格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1131.Text))
            {
                strErr += "p1131格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1141.Text))
            {
                strErr += "p1141格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1151.Text))
            {
                strErr += "p1151格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1161.Text))
            {
                strErr += "p1161格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1171.Text))
            {
                strErr += "p11711格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1181.Text))
            {
                strErr += "p1181格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp1191.Text))
            {
                strErr += "p1191格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp2001.Text))
            {
                strErr += "p2001格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp2011.Text))
            {
                strErr += "p2011格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp2021.Text))
            {
                strErr += "p2021格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp2031.Text))
            {
                strErr += "p2031格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp2041.Text))
            {
                strErr += "p2041格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp2051.Text))
            {
                strErr += "p2051格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp2081.Text))
            {
                strErr += "p2081格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp2091.Text))
            {
                strErr += "p2091格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(txtp3001.Text))
            {
                strErr += "p3001格式错误！\n";
            }
            #endregion

            if (strErr != "")
            {
                AlertAndRedirect(strErr);
                return;
            }
            decimal p100 = decimal.Parse(this.txtp100.Text) / 100;
            decimal p101 = decimal.Parse(this.txtp101.Text) / 100;
            decimal p102 = decimal.Parse(this.txtp102.Text) / 100;
            decimal p103 = decimal.Parse(this.txtp103.Text) / 100;
            decimal p104 = decimal.Parse(this.txtp104.Text) / 100;
            decimal p105 = decimal.Parse(this.txtp105.Text) / 100;
            decimal p106 = decimal.Parse(this.txtp106.Text) / 100;
            decimal p107 = decimal.Parse(this.txtp107.Text) / 100;
            decimal p108 = decimal.Parse(this.txtp108.Text) / 100;
            decimal p109 = decimal.Parse(this.txtp109.Text) / 100;
            decimal p110 = decimal.Parse(this.txtp110.Text) / 100;
            decimal p111 = decimal.Parse(this.txtp111.Text) / 100;
            decimal p112 = decimal.Parse(this.txtp112.Text) / 100;
            decimal p113 = decimal.Parse(this.txtp113.Text) / 100;
            decimal p114 = decimal.Parse(this.txtp114.Text) / 100;
            decimal p115 = decimal.Parse(this.txtp115.Text) / 100;
            decimal p116 = decimal.Parse(this.txtp116.Text) / 100;
            decimal p117 = decimal.Parse(this.txtp117.Text) / 100;
            decimal p118 = decimal.Parse(this.txtp118.Text) / 100;
            decimal p119 = decimal.Parse(this.txtp119.Text) / 100;

            decimal p200 = decimal.Parse(this.txtp200.Text) / 100;
            decimal p201 = decimal.Parse(this.txtp201.Text) / 100;
            decimal p202 = decimal.Parse(this.txtp202.Text) / 100;
            decimal p203 = decimal.Parse(this.txtp203.Text) / 100;
            decimal p204 = decimal.Parse(this.txtp204.Text) / 100;
            decimal p205 = decimal.Parse(this.txtp205.Text) / 100;

            decimal p208 = decimal.Parse(this.txtp208.Text) / 100;
            decimal p209 = decimal.Parse(this.txtp209.Text) / 100;

            decimal p300 = decimal.Parse(this.txtp300.Text) / 100;

            decimal p1001 = decimal.Parse(this.txtp1001.Text) / 100;
            decimal p1011 = decimal.Parse(this.txtp1011.Text) / 100;
            decimal p1021 = decimal.Parse(this.txtp1021.Text) / 100;
            decimal p1031 = decimal.Parse(this.txtp1031.Text) / 100;
            decimal p1041 = decimal.Parse(this.txtp1041.Text) / 100;
            decimal p1051 = decimal.Parse(this.txtp1051.Text) / 100;
            decimal p1061 = decimal.Parse(this.txtp1061.Text) / 100;
            decimal p1071 = decimal.Parse(this.txtp1071.Text) / 100;
            decimal p1081 = decimal.Parse(this.txtp1081.Text) / 100;
            decimal p1091 = decimal.Parse(this.txtp1091.Text) / 100;
            decimal p1101 = decimal.Parse(this.txtp1101.Text) / 100;
            decimal p1111 = decimal.Parse(this.txtp1111.Text) / 100;
            decimal p1121 = decimal.Parse(this.txtp1121.Text) / 100;
            decimal p1131 = decimal.Parse(this.txtp1131.Text) / 100;
            decimal p1141 = decimal.Parse(this.txtp1141.Text) / 100;
            decimal p1151 = decimal.Parse(this.txtp1151.Text) / 100;
            decimal p1161 = decimal.Parse(this.txtp1161.Text) / 100;
            decimal p1171 = decimal.Parse(this.txtp1171.Text) / 100;
            decimal p1181 = decimal.Parse(this.txtp1181.Text) / 100;
            decimal p1191 = decimal.Parse(this.txtp1191.Text) / 100;

            decimal p2001 = decimal.Parse(this.txtp2001.Text) / 100;
            decimal p2011 = decimal.Parse(this.txtp2011.Text) / 100;
            decimal p2021 = decimal.Parse(this.txtp2021.Text) / 100;
            decimal p2031 = decimal.Parse(this.txtp2031.Text) / 100;
            decimal p2041 = decimal.Parse(this.txtp2041.Text) / 100;
            decimal p2051 = decimal.Parse(this.txtp2051.Text) / 100;

            decimal p2081 = decimal.Parse(this.txtp2081.Text) / 100;
            decimal p2091 = decimal.Parse(this.txtp2091.Text) / 100;

            decimal p3001 = decimal.Parse(this.txtp3001.Text) / 100;

            System.Text.StringBuilder _payRate = new System.Text.StringBuilder();

            _payRate.AppendFormat("{0}:{1}:{2}", 100, p100, p1001);
            _payRate.AppendFormat("|{0}:{1}:{2}", 101, p101, p1011);
            _payRate.AppendFormat("|{0}:{1}:{2}", 102, p102, p1021);
            _payRate.AppendFormat("|{0}:{1}:{2}", 103, p103, p1031);
            _payRate.AppendFormat("|{0}:{1}:{2}", 104, p104, p1041);
            _payRate.AppendFormat("|{0}:{1}:{2}", 105, p105, p1051);
            _payRate.AppendFormat("|{0}:{1}:{2}", 106, p106, p1061);
            _payRate.AppendFormat("|{0}:{1}:{2}", 107, p107, p1071);
            _payRate.AppendFormat("|{0}:{1}:{2}", 108, p108, p1081);
            _payRate.AppendFormat("|{0}:{1}:{2}", 109, p109, p1091);
            _payRate.AppendFormat("|{0}:{1}:{2}", 110, p110, p1101);
            _payRate.AppendFormat("|{0}:{1}:{2}", 111, p111, p1111);

            _payRate.AppendFormat("|{0}:{1}:{2}", 112, p112, p1121);
            _payRate.AppendFormat("|{0}:{1}:{2}", 113, p113, p1131);
            _payRate.AppendFormat("|{0}:{1}:{2}", 114, p114, p1141);
            _payRate.AppendFormat("|{0}:{1}:{2}", 115, p115, p1151);
            _payRate.AppendFormat("|{0}:{1}:{2}", 116, p116, p1161);
            _payRate.AppendFormat("|{0}:{1}:{2}", 117, p117, p1171);
            _payRate.AppendFormat("|{0}:{1}:{2}", 118, p118, p1181);
            _payRate.AppendFormat("|{0}:{1}:{2}", 119, p119, p1191);

            _payRate.AppendFormat("|{0}:{1}:{2}", 200, p200, p2001);
            _payRate.AppendFormat("|{0}:{1}:{2}", 201, p201, p2011);
            _payRate.AppendFormat("|{0}:{1}:{2}", 202, p202, p2021);
            _payRate.AppendFormat("|{0}:{1}:{2}", 203, p203, p2031);
            _payRate.AppendFormat("|{0}:{1}:{2}", 204, p204, p2041);
            _payRate.AppendFormat("|{0}:{1}:{2}", 205, p205, p2051);
            _payRate.AppendFormat("|{0}:{1}:{2}", 208, p208, p2081);
            _payRate.AppendFormat("|{0}:{1}:{2}", 209, p209, p2091);
            _payRate.AppendFormat("|{0}:{1}:{2}", 300, p300, p3001);

            if (WebInfoFactory.SetAgent_Payrate_Setconfig(_payRate.ToString()))
            {
                AlertAndRedirect("设置成功");
            }
            else
            {
                AlertAndRedirect("设置失败");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Interfaces);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            viviapi.Model.Finance.PayRate _model = viviapi.BLL.Finance.PayRate.Instance.GetModelByUser(this.ItemInfoId);
            if (_model != null)
            {
                this.txtp100.Text = (Convert.ToDecimal(_model.p100) * 100).ToString("0.00");
                this.txtp101.Text = (Convert.ToDecimal(_model.p101) * 100).ToString("0.00");
                this.txtp102.Text = (Convert.ToDecimal(_model.p102) * 100).ToString("0.00");
                this.txtp103.Text = (Convert.ToDecimal(_model.p103) * 100).ToString("0.00");
                this.txtp104.Text = (Convert.ToDecimal(_model.p104) * 100).ToString("0.00");
                this.txtp105.Text = (Convert.ToDecimal(_model.p105) * 100).ToString("0.00");
                this.txtp106.Text = (Convert.ToDecimal(_model.p106) * 100).ToString("0.00");
                this.txtp107.Text = (Convert.ToDecimal(_model.p107) * 100).ToString("0.00");
                this.txtp108.Text = (Convert.ToDecimal(_model.p108) * 100).ToString("0.00");
                this.txtp109.Text = (Convert.ToDecimal(_model.p109) * 100).ToString("0.00");
                this.txtp110.Text = (Convert.ToDecimal(_model.p110) * 100).ToString("0.00");
                this.txtp111.Text = (Convert.ToDecimal(_model.p111) * 100).ToString("0.00");
                this.txtp112.Text = (Convert.ToDecimal(_model.p112) * 100).ToString("0.00");
                this.txtp113.Text = (Convert.ToDecimal(_model.p113) * 100).ToString("0.00");
                this.txtp114.Text = (Convert.ToDecimal(_model.p114) * 100).ToString("0.00");
                this.txtp115.Text = (Convert.ToDecimal(_model.p115) * 100).ToString("0.00");
                this.txtp116.Text = (Convert.ToDecimal(_model.p116) * 100).ToString("0.00");
                this.txtp117.Text = (Convert.ToDecimal(_model.p117) * 100).ToString("0.00");
                this.txtp118.Text = (Convert.ToDecimal(_model.p118) * 100).ToString("0.00");
                this.txtp119.Text = (Convert.ToDecimal(_model.p119) * 100).ToString("0.00");

                this.txtp200.Text = (Convert.ToDecimal(_model.p200) * 100).ToString("0.00");
                this.txtp201.Text = (Convert.ToDecimal(_model.p201) * 100).ToString("0.00");
                this.txtp202.Text = (Convert.ToDecimal(_model.p202) * 100).ToString("0.00");
                this.txtp203.Text = (Convert.ToDecimal(_model.p203) * 100).ToString("0.00");
                this.txtp204.Text = (Convert.ToDecimal(_model.p204) * 100).ToString("0.00");
                this.txtp205.Text = (Convert.ToDecimal(_model.p205) * 100).ToString("0.00");

                this.txtp208.Text = (Convert.ToDecimal(_model.p208) * 100).ToString("0.00");
                this.txtp209.Text = (Convert.ToDecimal(_model.p209) * 100).ToString("0.00");

                this.txtp300.Text = (Convert.ToDecimal(_model.p300) * 100).ToString("0.00");
            }
        }
    }
}
