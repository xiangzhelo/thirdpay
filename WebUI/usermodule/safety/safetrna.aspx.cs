using System;
using System.Text.RegularExpressions;
using viviapi.BLL.basedata;
using viviapi.BLL.Sys;
using viviapi.BLL.Tools;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.Text;

namespace viviAPI.WebUI7uka.usermodule.safety
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Safetrna : UserPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
            }
        }

        #region InitForm
        /// <summary>
        /// 
        /// </summary>
        void InitForm()
        {
            if (CurrentUser.IsRealNamePass == 1)
            {
                lblMessage.Visible = true;
                lbtnSave.Enabled = false;
                lbtnSave.Visible = false;
                rzqx.Visible = false;

                txtpername.Value = CurrentUser.full_name;
                txtpernumber.Value = Strings.Mark(CurrentUser.IdCard);
                txtpername.Attributes["readonly"] = "true";
                txtpernumber.Attributes["readonly"] = "true";
                tr_repernumber.Visible = false;

                lblMessage.Text = "您已通过实名认证。";

            }
            else
            {
                txtpername.Value = CurrentUser.full_name;
                tr_repernumber.Visible = true;
            }
        }
        #endregion

        #region lbtnSave_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            string pernumber = txtpernumber.Value;
            string personName = txtpername.Value;
            string rpernumber = txtrpernumber.Value;

            if (string.IsNullOrEmpty(personName))
            {
                msg = "请输入真实姓名";
            }
            else if (string.IsNullOrEmpty(pernumber))
            {
                msg = "请输入身份证号码";
            }
            else if (pernumber != rpernumber)
            {
                msg = "两次身份证号输入不一致";
            }
            else
            {
                string patt = Regular.GetRegularString(RegularType.ChineseIDCard);
                if (!Regex.IsMatch(pernumber, patt))
                {
                    msg = "请输入有效的身份证号码";
                }
                else
                {
                    string birthday, sex;
                    var result = new IdcardInfo { code = pernumber };

                    if (identitycard.GetBirthdayAndSex(pernumber, out birthday, out sex))
                    {
                        result.birthday = birthday;
                        result.gender = sex;

                        var info = identitycard.GetModel(pernumber.Substring(0, 6));
                        if (info == null)
                        {
                            result = null;
                        }
                        else
                        {
                            result.location = info.DQ;
                        }
                    }
                    else
                    {
                        result = null;
                    }

                    if (result == null)
                    {
                        result = GetInfoFromIp138(pernumber);
                    }
                    if (result == null)
                    {
                        msg = "无效身份信息。请重新输入。";
                    }
                    else
                    {
                        result.fullname = personName;
                        if (string.IsNullOrEmpty(msg))
                        {
                            msg = "true";

                            Session[String.Format(Constant.RealNameAuthenticationSessionKey, CurrentUser.ID)] = result;
                        }
                    }
                }
            }

            if (msg.Equals("true"))
            {
                Response.Redirect("safetrna1.aspx");
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = msg;
            }
        }
        #endregion

        #region GetInfoFromIp138
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pernumber"></param>
        /// <returns></returns>
        private IdcardInfo GetInfoFromIp138(string pernumber)
        {
            try
            {
                var result = new IdcardInfo { code = pernumber };

                string url = "http://qq.ip138.com/idsearch/index.asp?action=idcard&userid=" + pernumber;

                string html = viviLib.Web.WebClientHelper.GetString(url
                    , null
                    , "GET"
                    , System.Text.Encoding.Default
                    , 1000 * 10);

                string pattern = "<td colspan=2 class=tdc1 align=center height=24 bgcolor=#6699cc>\\+\\+\\* 查询结果 \\*\\+\\+</td>";
                Match match = Regex.Match(html, pattern);

                pattern = "<td class=\"tdc2\">(?<ih>.*?)</td>";
                var regex3 = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                int id = 0;
                while (id < 3)
                {

                    match = regex3.Match(html, (int)(match.Index + match.Value.Length));

                    string content = match.Groups["ih"].Captures[0].Value;
                    switch (id)
                    {
                        case 0:
                            result.gender = content;
                            break;
                        case 1:
                            result.birthday = content;
                            break;
                        case 2:
                            result.location = content;
                            break;
                    }

                    id++;
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
        #endregion
    }
}
