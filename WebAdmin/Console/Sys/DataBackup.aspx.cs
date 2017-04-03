using System;
using System.IO;
using viviapi.Model;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DataBackup : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            setPower();
            if(!this.IsPostBack)
            {
                if (!this.IsPostBack)
                {
                    this.txtfilname.Text = viviLib.XRequest.GetHost() + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".bak";
                }
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
              , ManageRole.System);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        protected void bt_sub_Click(object sender, EventArgs e)
        {
            string text = this.txtfilepath.Text;
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            if (viviapi.BLL.Tools.db.Backup(text + this.txtfilname.Text))
            {
                this.lbmsg.Text = "备份成功！";
            }
            else
            {
                this.lbmsg.Text = "备份失败！";
            }
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}

