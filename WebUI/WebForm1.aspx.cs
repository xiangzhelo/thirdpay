using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents;
using viviLib.Web;

namespace viviAPI.WebUI7uka
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string area = "重庆市 联通";// WebUtility.GetIpAddr(clientIP);

            string pro = string.Empty;
            string city = string.Empty;
            Match m = Regex.Match(area, @"(?<pro>.*?)省(?<city>.*?)市");
            if (m.Success)
            {
                pro = m.Groups["pro"].Value;
                city = m.Groups["city"].Value;
            }
            else
            {
                m = Regex.Match(area, @"(?<pro>.*?)市(?<city>.*?)区");
                if (m.Success)
                {
                    pro = m.Groups["pro"].Value;
                    city = m.Groups["city"].Value;
                }
                else
                {
                    m = Regex.Match(area, @"(?<pro>.*?)市");

                    if (m.Success)
                    {
                        pro = m.Groups["pro"].Value;
                        city = m.Groups["pro"].Value;
                    }
                }

            }

            viviLib.Logging.LogHelper.Write(pro);
            viviLib.Logging.LogHelper.Write(city);
            //viviLib.Logging.LogHelper.Write(area);
            //if (!string.IsNullOrEmpty(area))
            //{
            //    int startIndex = area.IndexOf("省", System.StringComparison.Ordinal);
            //    int endIndex = area.IndexOf("  ", System.StringComparison.Ordinal);
            //    if (startIndex > 0)
            //    {
            //        string province = area.Substring(0, startIndex+1);
            //        viviLib.Logging.LogHelper.Write(province);
            //        if (endIndex+1 > startIndex-1)
            //        {
            //            string city = area.Substring(startIndex+1, endIndex-1);
            //            viviLib.Logging.LogHelper.Write(city);
            //        }
            //    }

            //}
        }
    }
}
