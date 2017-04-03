using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents;

namespace viviAPI.WebUI7uka
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string url = "http://qq.ip138.com/idsearch/index.asp?action=idcard&userid="+this.TextBox1.Text.Trim();

            string html = viviLib.Web.WebClientHelper.GetString(url, null, "GET", System.Text.Encoding.Default, 1000 * 10);

            string pattern = "<td colspan=2 class=tdc1 align=center height=24 bgcolor=#6699cc>\\+\\+\\* 查询结果 \\*\\+\\+</td>";
            Match match = Regex.Match(html, pattern);

            pattern = "<td class=\"tdc2\">(?<ih>.*?)</td>";
            Regex regex3 = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            int id = 0;

          
            
            while (id < 3)
            {
                match = regex3.Match(html, (int)(match.Index + match.Value.Length));

                string _content = match.Groups["ih"].Captures[0].Value;

                Response.Write(_content+"<br>");

                id++;
            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    string area = WebUtility.GetIpAddr(this.TextBox1.Text.Trim());

        //    string pro = string.Empty;
        //    string city = string.Empty;
        //    Match m = Regex.Match(area, @"(?<pro>.*?)省(?<city>.*?)市");
        //    if (m.Success)
        //    {
        //        pro = m.Groups["pro"].Value;
        //        city = m.Groups["city"].Value;
        //    }
        //    else
        //    {
        //        m = Regex.Match(area, @"(?<pro>.*?)市(?<city>.*?)区");
        //        if (m.Success)
        //        {
        //            pro = m.Groups["pro"].Value;
        //            city = m.Groups["city"].Value;
        //        }
        //        else
        //        {
        //            m = Regex.Match(area, @"(?<pro>.*?)市");

        //            if (m.Success)
        //            {
        //                pro = m.Groups["pro"].Value;
        //                city = m.Groups["pro"].Value;
        //            }
        //        }

        //    }

        //    lblpro.Text = pro;
        //    lblcity.Text = city;

        //}
    }
}
