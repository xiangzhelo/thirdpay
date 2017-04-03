using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents.Web;

namespace viviAPI.Gateway2018.Connect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Report : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnSub_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (this.txtUserName.Value.Trim().Length == 0)
            {
                strErr += "name不能为空！\\n";
            }
            if (this.txtEmail.Value.Trim().Length == 0)
            {
                strErr += "email不能为空！\\n";
            }
            if (this.txtMoblie.Value.Trim().Length == 0)
            {
                strErr += "tel不能为空！\\n";
            }
            if (this.txtUrl.Value.Trim().Length == 0)
            {
                strErr += "url不能为空！\\n";
            }
            if (this.txtReason.Value.Trim().Length == 0)
            {
                strErr += "remark不能为空！\\n";
            }
            if (strErr != "")
            {
                ShowMessageBox(strErr);
                return;
            }
            string name = this.txtUserName.Value;
            string email = this.txtEmail.Value;
            string tel = this.txtMoblie.Value;
            string url = this.txtUrl.Value;
            int type = int.Parse(this.ddlType.Value);
            string remark = this.txtReason.Value;
            DateTime addtime = DateTime.Now;
            int status = 1;
            DateTime checktime = DateTime.Now;
            int check = 0;
            string checkremark = string.Empty;
            string pwd = "J" + DateTime.Now.Ticks.ToString();
            string field1 = viviLib.Web.ServerVariables.TrueIP;
            string field2 = string.Empty;
            string field3 = string.Empty;

            var model = new viviapi.Model.JuBaoInfo
            {
                name = name,
                email = email,
                tel = tel,
                url = url,
                type = (viviapi.Model.JuBaoEnum) type,
                remark = remark,
                addtime = addtime,
                status = (viviapi.Model.JuBaoStatusEnum) status,
                checktime = checktime,
                check = check,
                checkremark = checkremark,
                pwd = pwd,
                field1 = field1,
                field2 = field2,
                field3 = field3
            };

            var bll = new viviapi.BLL.JuBao();
            if (bll.Add(model) > 0)
            {
                lblInfo.InnerText = "举报成功，等候处理。请复制并保存查询密码：" + model.pwd;
            }
            else
            {
                lblInfo.InnerText = "举报失败";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
