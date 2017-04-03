using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib;

namespace viviAPI.WebAdmin.Console.Cache
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GwCahceManage1 : ManagePageBase
    {
        /// <summary>
        /// 
        /// </summary>
        private WebInfo _objectInfo = null;
        public WebInfo ObjectInfo
        {
            get
            {
                if (_objectInfo == null)
                {
                    _objectInfo = WebInfoFactory.GetWebInfoByDomain(XRequest.GetHost());
                }
                return _objectInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                if(ObjectInfo != null)
                    txtGwUrl.Text = ObjectInfo.PayUrl;
            }
        }

        protected void BtnRemove_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGwUrl.Text.Trim()))
            {
                string cacheKey = viviapi.BLL.Sys.Constant.CacheMark + this.ddlcachetype.SelectedValue;

                if (ddlcachetype.SelectedValue.ToUpper() == "USER_")
                {
                    cacheKey = viviapi.BLL.Sys.Constant.CacheMark + this.ddlsubcache.SelectedValue;
                }

                string apiKey = viviapi.SysConfig.MemCachedConfig.AuthCode;
                string sign = viviLib.Security.Cryptography.MD5(cacheKey + apiKey);

                string apiUrl = this.txtGwUrl.Text.Trim() + string.Format("/tools/SyncLocalCache.ashx?cacheKey={0}&passKey={1}", cacheKey, sign);

                //viviLib.Logging.LogHelper.Write(apiUrl);
                string callback = string.Empty;
                try
                {
                    callback = viviLib.Web.WebClientHelper.GetString(apiUrl, null, "get", System.Text.Encoding.GetEncoding("gbk"));
                }
                catch (Exception ex)
                {
                    callback = ex.Message;
                }
                ShowMessageBox(callback);
            }
        }

        
        protected void ddlcachetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtCacheKey.Text = viviapi.BLL.Sys.Constant.CacheMark + this.ddlcachetype.SelectedValue;
        }
    }
}
